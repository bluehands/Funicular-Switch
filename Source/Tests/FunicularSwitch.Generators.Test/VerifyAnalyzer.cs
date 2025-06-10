using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace FunicularSwitch.Generators.Test;

public class VerifyAnalyzer : VerifyBase
{
    private readonly string sourceFile;
    private static readonly string assemblyDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
    private static readonly MetadataReference CorlibReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
    private static readonly MetadataReference SystemCoreReference = MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);
    private static readonly MetadataReference RuntimeReference = MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Runtime.dll"));
    private static readonly MetadataReference CollectionsReference = MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Collections.dll"));
    private static readonly MetadataReference CSharpSymbolsReference = MetadataReference.CreateFromFile(typeof(CSharpCompilation).Assembly.Location);
    private static readonly MetadataReference CodeAnalysisReference = MetadataReference.CreateFromFile(typeof(Compilation).Assembly.Location);
    
    // ReSharper disable once ExplicitCallerInfoArgument
    protected VerifyAnalyzer([CallerFilePath] string sourceFile = "")
    {
        this.sourceFile = sourceFile;
    }

    protected async Task Verify(
        string source,
        DiagnosticAnalyzer analyzer,
        CodeFixProvider codeFixProvider,
        Action<ImmutableArray<Diagnostic>> verifyDiagnostics,
        Action<Diagnostic, CodeAction>? verifyCodeAction = null,
        [CallerMemberName] string callingMethod = "")
    {
        const string TestProjectName = "Test";
        
        var projectId = ProjectId.CreateNewId(debugName: TestProjectName);
        var fileName = "File.cs";
        var documentId = DocumentId.CreateNewId(projectId, debugName: fileName);
        
        var solution = new AdhocWorkspace()
            .CurrentSolution
            .AddProject(projectId, TestProjectName, TestProjectName, LanguageNames.CSharp)
            .AddMetadataReference(projectId, CorlibReference)
            .AddMetadataReference(projectId, SystemCoreReference)
            .AddMetadataReference(projectId, RuntimeReference)
            .AddMetadataReference(projectId, CollectionsReference)
            .AddMetadataReference(projectId, CSharpSymbolsReference)
            .AddMetadataReference(projectId, CodeAnalysisReference)
            .WithProjectParseOptions(projectId, new CSharpParseOptions(LanguageVersion.Preview))
            .WithProjectCompilationOptions(projectId, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddDocument(documentId, fileName, SourceText.From(source));

        var project = solution.GetProject(projectId)!;
        var compilationWithAnalyzers = (await project.GetCompilationAsync())!.WithAnalyzers([analyzer]);
        compilationWithAnalyzers.Compilation.GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .Should().BeEmpty();
        var diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
        verifyDiagnostics(diagnostics);
        var document = project.Documents.First();

        var index = 0;
        foreach (var d in diagnostics
                     .OrderBy(d => d.Location.GetLineSpan().StartLinePosition.Line)
                     .ThenBy(d => d.Location.GetLineSpan().StartLinePosition.Character))
        {
            var actions = new List<CodeAction>();
            var codeFixContext = new CodeFixContext(document, d, (a, _) => actions.Add(a), CancellationToken.None);
            await codeFixProvider.RegisterCodeFixesAsync(codeFixContext);
            actions.Should().NotBeEmpty();
            verifyCodeAction?.Invoke(d, actions[0]);
            var updatedDocument = await ApplyFix(document, actions[0]);
            var syntaxTree = await updatedDocument.GetSyntaxRootAsync();
            var updatedCode = syntaxTree.ToFullString();
            var settings = new VerifySettings();
            settings.UseFileName($"{Path.GetFileNameWithoutExtension(this.sourceFile)}_{callingMethod}_{d.Id}_{index}.cs");
            await Verify(updatedCode, settings)
                .UseDirectory("Snapshots");
            index += 1;
        }
    }
    
    private static async Task<Document> ApplyFix(Document document, CodeAction codeAction)
    {
        var operations = await codeAction.GetOperationsAsync(CancellationToken.None);
        var solution = operations.OfType<ApplyChangesOperation>().Single().ChangedSolution;
        return solution.GetDocument(document.Id)!;
    }
}