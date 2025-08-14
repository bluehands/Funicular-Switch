using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using AwesomeAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Generators.AwesomeAssertions.Test;

public abstract class VerifySourceGenerator : VerifyBase
{
    public const string SnapshotFolder = "Snapshots";
    // ReSharper disable once ExplicitCallerInfoArgument => pipe it through from the deriving class
    protected VerifySourceGenerator([CallerFilePath] string sourceFile = "") : base(sourceFile: sourceFile)
    {
    }

    protected Task Verify(IEnumerable<Assembly> assemblies, Action<Compilation, ImmutableArray<Diagnostic>>? verifyCompilation, string? subfolder = null)
    {
        var assemblyDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        var references = new[]
            {
                typeof(object).Assembly,
                typeof(Enumerable).Assembly,
                typeof(global::AwesomeAssertions.AssertionExtensions).Assembly,
            }
            .Concat(assemblies)
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Concat([
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "netstandard.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Runtime.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Collections.dll")),
            ]);
        
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [CSharpSyntaxTree.ParseText("")],
            references: references,
            options: new(OutputKind.DynamicallyLinkedLibrary));

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AssertionMethodsGenerator());

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);

        verifyCompilation?.Invoke(updatedCompilation, diagnostics);

        var folder = subfolder is not null ? Path.Combine(SnapshotFolder, subfolder) : SnapshotFolder;
        return Verify(driver)
            .UseDirectory(folder);
    }

    protected Task Verify(IEnumerable<Assembly> assemblies, string? subfolder = null) =>
        Verify(
            assemblies, 
            (compilation, _) =>
            {
                var diagnostics = compilation.GetDiagnostics()
                    .Where(d => d.Severity == DiagnosticSeverity.Error)
                    .ToList();
                var errors = string.Join(Environment.NewLine, diagnostics);
                var syntaxTrees = string.Join(Environment.NewLine, diagnostics.Select(d => d.Location.SourceTree));
                errors.Should().BeNullOrEmpty($"Compilation failed: {syntaxTrees}");
            },
            subfolder: subfolder);
}