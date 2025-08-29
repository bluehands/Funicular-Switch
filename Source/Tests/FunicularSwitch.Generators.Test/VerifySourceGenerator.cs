using System.Collections.Immutable;
using System.Reflection;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using PolyType;

namespace FunicularSwitch.Generators.Test;

public abstract class VerifySourceGenerator : VerifyBase
{
    protected Task Verify(string source, IReadOnlyList<Assembly> additionalAssemblies, Action<GeneratorDriver, Compilation, ImmutableArray<Diagnostic>>? verifyCompilation)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var assemblyDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        var references =
            new[]
            {
                typeof(object),
                typeof(Enumerable),
            }
            .Select(t => t.Assembly)
            .Concat(additionalAssemblies)
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Concat([
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Runtime.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Collections.dll"))
            ]);

        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [syntaxTree],
            references: references,
            options: new(
                outputKind: OutputKind.DynamicallyLinkedLibrary,
                nullableContextOptions: NullableContextOptions.Enable));

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new ResultTypeGenerator(), new UnionTypeGenerator(), new EnumTypeGenerator());

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);

        verifyCompilation?.Invoke(driver, updatedCompilation, diagnostics);

        return Verify(driver)
            .UseDirectory("Snapshots");
    }

    protected Task Verify(string source, int numberOfGeneratedFiles = 1, bool referencePolyType = false) =>
        Verify(source, additionalAssemblies: referencePolyType ? [typeof(DerivedTypeShapeAttribute).Assembly] : [], (driver, compilation, _) =>
        {
            const int numberOfAttributeFiles = 3;
            driver.GetRunResult().GeneratedTrees.Should().HaveCount(numberOfAttributeFiles + numberOfGeneratedFiles);
            var diagnostics = compilation.GetDiagnostics();
            var errors = string.Join(Environment.NewLine, diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error));
            errors.Should().BeNullOrEmpty($"Compilation failed: {compilation.SyntaxTrees.LastOrDefault(s => !s.ToString().Contains("ReSharper disable once CheckNamespace"))}");
        });
}