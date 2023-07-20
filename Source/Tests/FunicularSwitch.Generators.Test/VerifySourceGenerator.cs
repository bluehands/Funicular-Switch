using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VerifyMSTest;

namespace FunicularSwitch.Generators.Test;

public abstract class VerifySourceGenerator : VerifyBase
{
    protected Task Verify(string source, Action<Compilation, ImmutableArray<Diagnostic>>? verifyCompilation)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var assemblyDirectory = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        var references = new[]
        {
            typeof(object),
            typeof(Enumerable)
        }.Select(t => MetadataReference.CreateFromFile(t.Assembly.Location))
            .Concat(new []
            {
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Runtime.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyDirectory, "System.Collections.dll")),
            });

        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new(OutputKind.DynamicallyLinkedLibrary));

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new ResultTypeGenerator(), new UnionTypeGenerator(), new EnumTypeGenerator());

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);

        verifyCompilation?.Invoke(updatedCompilation, diagnostics);

        return Verify(driver)
            .UseDirectory("Snapshots");
    }

    protected Task Verify(string source) =>
        Verify(source, (compilation, _) =>
        {
            var diagnostics = compilation.GetDiagnostics();
            var errors = string.Join(Environment.NewLine, diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error));
            errors.Should().BeNullOrEmpty($"Compilation failed: {compilation.SyntaxTrees.Last()}");
        });
}