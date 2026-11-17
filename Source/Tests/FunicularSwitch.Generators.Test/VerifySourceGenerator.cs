using System.Collections.Immutable;
using System.Reflection;
using FluentAssertions;
using FunicularSwitch.Transformers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Generators.Test;

public abstract class VerifySourceGenerator()
    : BaseVerifySourceGenerators(new ResultTypeGenerator(), new UnionTypeGenerator(), new EnumTypeGenerator());

public abstract class VerifySourceGenerator<T>() : BaseVerifySourceGenerators(new T()) where T : IIncrementalGenerator, new();

public abstract class BaseVerifySourceGenerators(params IIncrementalGenerator[] generators) : VerifyBase
{
    static string Net11ReferenceDirectory()
    {
        var packDirectory = typeof(BaseVerifySourceGenerators).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
            .First(a => a.Key == "NetCoreAppRefPackDirectory").Value!;
        var version = Directory.GetDirectories(packDirectory, "11.*").OrderBy(d => d, StringComparer.Ordinal).Last();
        return Path.Combine(version, "ref", "net11.0");
    }

    /// <summary>Compiles as C# 15 against net11 reference assemblies.</summary>
    protected Task VerifyCSharp15(string source) => Verify(source, languageVersion: LanguageVersion.CSharp15, useNet11References: true);

    protected Task Verify(string source, IReadOnlyList<Assembly> additionalAssemblies, Action<Compilation, ImmutableArray<Diagnostic>>? verifyCompilation, LanguageVersion? languageVersion = null, bool useNet11References = false)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(languageVersion ?? LanguageVersion.Default));

        IEnumerable<MetadataReference> references;
        if (useNet11References)
        {
            // net11 reference assemblies (location comes from the test project), so C# 15 types like UnionAttribute resolve
            var refDir = Net11ReferenceDirectory();
            references = Directory.GetFiles(refDir, "*.dll")
                .Concat(new[] { typeof(Monad<>) }.Select(t => t.Assembly.Location).Concat(additionalAssemblies.Select(a => a.Location)))
                .Select(f => MetadataReference.CreateFromFile(f));
        }
        else
        {
            var assemblyDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
            references =
                new[]
                {
                    typeof(object),
                    typeof(Enumerable),
                    typeof(Monad<>),
                }
                .Select(t => t.Assembly)
                .Concat(additionalAssemblies)
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .Concat([
                    MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "netstandard.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Collections.dll")),
                ]);
        }

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

    protected Task Verify(string source, IReadOnlyList<Assembly>? additionalAssemblies = null, LanguageVersion? languageVersion = null, bool useNet11References = false) =>
        Verify(source, additionalAssemblies: additionalAssemblies ?? [], languageVersion: languageVersion, useNet11References: useNet11References, verifyCompilation: (compilation, _) =>
        {
            var diagnostics = compilation.GetDiagnostics();
            var errors = string.Join(Environment.NewLine, diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error || d.Id is "CS8509" or "CS8524")); // non exhaustive switch expressions are failures too
            // errors.Should().BeNullOrEmpty($"Compilation failed: {compilation.SyntaxTrees.LastOrDefault(s => !s.ToString().Contains("ReSharper disable once CheckNamespace"))}");
            errors.Should().BeNullOrEmpty($"Compilation failed: {string.Join("\n", compilation.SyntaxTrees.Skip(1).Select(x => $"---\n{x.FilePath}\n---\n{x}"))}");
        });
}