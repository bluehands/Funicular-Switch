using System.Collections;
using System.Diagnostics;
using FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.FluentAssertions;

[Generator]
public class FluentAssertionMethodsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var refAssemblies = context.CompilationProvider
            .SelectMany((c, _) => c.SourceModule.ReferencedAssemblySymbols);

        context.RegisterSourceOutput(
            refAssemblies,
            static (sourceProductionContext, assembly) => Execute(assembly, sourceProductionContext));
    }

    private static void Execute(
        IAssemblySymbol assembly,
        SourceProductionContext context)
    {
        IEnumerable<(string filename, string source)> generated;
        if (assembly.Identity.Name == "FunicularSwitch")
        {
            var resultType = assembly.GetTypeByMetadataName("FunicularSwitch.Result");
            generated = Generator.EmitForResultType(
                new ResultTypeSchema(resultType!, null),
                context.ReportDiagnostic,
                context.CancellationToken);
        }
        else
        {
            var resultTypeSchemata = Parser.GetResultTypes(
                assembly,
                context.ReportDiagnostic,
                context.CancellationToken);
            var unionTypeSchemata = Parser.GetUnionTypes(
                assembly,
                context.ReportDiagnostic,
                context.CancellationToken);
            generated = resultTypeSchemata.SelectMany(
                    r => Generator.EmitForResultType(
                        r,
                        context.ReportDiagnostic,
                        context.CancellationToken))
                .Concat(unionTypeSchemata.SelectMany(u =>
                    Generator.EmitForUnionType(
                        u,
                        context.ReportDiagnostic,
                        context.CancellationToken)));
        }

        foreach (var (filename, source) in generated)
        {
            context.AddSource(filename, source);
        }
    }
}