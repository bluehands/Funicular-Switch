using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace FunicularSwitch.Analyzers.Tests;

public class VerifyAnalyzer : VerifyBase
{
    private readonly string sourceFile;
    // Compiling C# 15 sources needs the net11 reference assemblies (the test host runs on net10, whose
    // runtime assemblies must not be mixed in). They come from the .NET 11 targeting pack of the SDK.
    private static readonly ImmutableArray<MetadataReference> References = LoadReferences();
    protected static ImmutableArray<MetadataReference> ReferenceAssemblies() => References;
    private static readonly MetadataReference FunicularSwitchReference = MetadataReference.CreateFromFile(typeof(Unit).Assembly.Location);

    private static ImmutableArray<MetadataReference> LoadReferences()
    {
        var dotnetRoot = Path.GetFullPath(Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "..", "..", ".."));
        var packDirectory = Path.Combine(dotnetRoot, "packs", "Microsoft.NETCore.App.Ref");
        var refDirectory = Directory.GetDirectories(packDirectory, "11.*")
            .Select(d => Path.Combine(d, "ref", "net11.0"))
            .Where(Directory.Exists)
            .OrderDescending()
            .FirstOrDefault()
            ?? throw new InvalidOperationException($"Microsoft.NETCore.App.Ref 11.x targeting pack not found in {packDirectory}");
        return [.. Directory.GetFiles(refDirectory, "*.dll").Select(f => (MetadataReference)MetadataReference.CreateFromFile(f))];
    }

    // ReSharper disable once ExplicitCallerInfoArgument
    protected VerifyAnalyzer([CallerFilePath] string sourceFile = "") : base(sourceFile: sourceFile)
    {
        this.sourceFile = sourceFile;
    }

    protected async Task Verify(
        string source,
        DiagnosticAnalyzer analyzer,
        CodeFixProvider? codeFixProvider,
        Action<ImmutableArray<Diagnostic>> verifyDiagnostics,
        Action<Diagnostic, CodeAction>? verifyCodeAction = null,
        LanguageVersion languageVersion = LanguageVersion.Preview,
        string? actionTitleContains = null,
        [CallerMemberName] string callingMethod = "")
    {
        const string TestProjectName = "Test";

        var projectId = ProjectId.CreateNewId(debugName: TestProjectName);
        var fileName = "File.cs";
        var documentId = DocumentId.CreateNewId(projectId, debugName: fileName);

        var solution = new AdhocWorkspace()
            .CurrentSolution
            .AddProject(projectId, TestProjectName, TestProjectName, LanguageNames.CSharp)
            .AddMetadataReferences(projectId, References)
            .AddMetadataReference(projectId, FunicularSwitchReference)
            .WithProjectParseOptions(projectId, new CSharpParseOptions(languageVersion))
            .WithProjectCompilationOptions(projectId, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable))
            .AddDocument(documentId, fileName, SourceText.From(source));
        var project = solution.GetProject(projectId)!;
        var compilationWithAnalyzers = (await CheckCompilation(project))!.WithAnalyzers([analyzer]);
        var diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
        verifyDiagnostics(diagnostics);
        var document = project.Documents.First();

        var index = 0;
        foreach (var d in diagnostics
                     .OrderBy(d => d.Location.GetLineSpan().StartLinePosition.Line)
                     .ThenBy(d => d.Location.GetLineSpan().StartLinePosition.Character))
        {
            var actions = new List<CodeAction>();
            if (codeFixProvider == null) continue;
            var codeFixContext = new CodeFixContext(document, d, (a, _) => actions.Add(a), CancellationToken.None);
            await codeFixProvider.RegisterCodeFixesAsync(codeFixContext);
            actions.Should().NotBeEmpty();
            var action = actionTitleContains == null ? actions[0] : actions.Single(a => a.Title.Contains(actionTitleContains));
            verifyCodeAction?.Invoke(d, action);
            var updatedDocument = await ApplyFix(document, action);
            var syntaxTree = await updatedDocument.GetSyntaxRootAsync();
            syntaxTree.Should().NotBeNull();
            var updatedCode = syntaxTree.ToFullString();
            await CheckCompilation(solution.RemoveDocument(documentId).AddDocument(documentId, fileName, SourceText.From(updatedCode)).GetProject(projectId)!);

            var settings = new VerifySettings();
            settings.UseFileName($"{Path.GetFileNameWithoutExtension(this.sourceFile)}_{callingMethod}_{d.Id}_{index}.cs");
            await Verify(updatedCode, settings)
                .UseDirectory("Snapshots");
            index += 1;
        }
        return;

        async Task<Compilation> CheckCompilation(Project p)
        {
            var c = await p.GetCompilationAsync();
            c.Should().NotBeNull();
            c.GetDiagnostics()
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Should().BeEmpty();
            return c;
        }
    }

    private static async Task<Document> ApplyFix(Document document, CodeAction codeAction)
    {
        var operations = await codeAction.GetOperationsAsync(CancellationToken.None);
        var solution = operations.OfType<ApplyChangesOperation>().Single().ChangedSolution;
        return solution.GetDocument(document.Id)!;
    }
}


