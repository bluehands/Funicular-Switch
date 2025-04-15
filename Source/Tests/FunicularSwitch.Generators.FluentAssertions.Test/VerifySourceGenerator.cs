using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Generators.FluentAssertions.Test;

public abstract class VerifySourceGenerator : VerifyBase
{
    // ReSharper disable once ExplicitCallerInfoArgument => pipe it through from the deriving class
    protected VerifySourceGenerator([CallerFilePath] string sourceFile = "") : base(sourceFile: sourceFile)
    {
    }

    protected Task Verify(IEnumerable<Assembly> assemblies, Action<Compilation, ImmutableArray<Diagnostic>>? verifyCompilation)
    {
        var assemblyDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        var references = new[]
            {
                typeof(object).Assembly,
                typeof(Enumerable).Assembly,
                typeof(global::FluentAssertions.AssertionOptions).Assembly,
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

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new FluentAssertionMethodsGenerator());

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);

        verifyCompilation?.Invoke(updatedCompilation, diagnostics);

        return Verify(driver)
            .UseDirectory("Snapshots");
    }

    protected Task Verify(IEnumerable<Assembly> assemblies) =>
        Verify(assemblies, (compilation, _) =>
        {
            var diagnostics = compilation.GetDiagnostics();
            var errors = string.Join(Environment.NewLine, diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error));
            errors.Should().BeNullOrEmpty($"Compilation failed: {compilation.SyntaxTrees.LastOrDefault(s => !s.ToString().Contains("ReSharper disable once CheckNamespace"))}");
        });
}