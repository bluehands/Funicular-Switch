using System.Collections.Immutable;
using System.Reflection;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using PolyType;

namespace FunicularSwitch.Generators.Test;

public abstract class VerifySourceGenerator()
    : BaseVerifySourceGenerators(new ResultTypeGenerator(), new UnionTypeGenerator(), new EnumTypeGenerator());

public abstract class VerifySourceGenerator<T>() : BaseVerifySourceGenerators(new T()) where T : IIncrementalGenerator, new();

public abstract class BaseVerifySourceGenerators(params IIncrementalGenerator[] generators) : VerifyBase
{
    protected Task Verify(string source, IReadOnlyList<Assembly> additionalAssemblies, Action<Compilation, ImmutableArray<Diagnostic>>? verifyCompilation)
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

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generators);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);

        verifyCompilation?.Invoke(updatedCompilation, diagnostics);

        return Verify(driver)
            .UseDirectory("Snapshots");
    }

    protected Task Verify(string source, bool referencePolyType = false) =>
        Verify(source, additionalAssemblies: referencePolyType ? [typeof(DerivedTypeShapeAttribute).Assembly] : [], (compilation, _) =>
        {
            var diagnostics = compilation.GetDiagnostics();
            var errors = string.Join(Environment.NewLine, diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error));
            errors.Should().BeNullOrEmpty($"Compilation failed: {compilation.SyntaxTrees.LastOrDefault(s => !s.ToString().Contains("ReSharper disable once CheckNamespace"))}");
        });
}