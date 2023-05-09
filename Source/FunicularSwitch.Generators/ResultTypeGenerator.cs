using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.ResultType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class ResultTypeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource(
                "Attributes.g.cs",
                Templates.ResultTypeTemplates.StaticCode);
        });

        var resultTypeClasses =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => s.IsTypeDeclarationWithAttributes(),
                    transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration(ctx, ResultTypeAttribute)

                )
                .Where(static target => target != null)
                .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(resultTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses, 
            //TODO: support record result types one day
            static (spc, source) => Execute(source.Left, source.Right.OfType<ClassDeclarationSyntax>().ToImmutableArray(), spc));
    }

    static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, SourceProductionContext context)
    {
        if (resultTypeClasses.IsDefaultOrEmpty) return;

        var resultTypeSchemata = Parser.GetResultTypes(compilation, resultTypeClasses, context.ReportDiagnostic, context.CancellationToken).ToImmutableArray();

        var generated = resultTypeSchemata.SelectMany(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken)).ToImmutableArray();

        foreach (var (filename, source) in generated) context.AddSource(filename, source);
    }

    internal const string ResultTypeAttribute = "FunicularSwitch.Generators.ResultTypeAttribute";
}
