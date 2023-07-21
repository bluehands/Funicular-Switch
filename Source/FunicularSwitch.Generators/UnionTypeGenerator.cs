using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.UnionType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class UnionTypeGenerator : IIncrementalGenerator
{
    internal const string UnionTypeAttribute = "FunicularSwitch.Generators.UnionTypeAttribute";
    internal const string UnionCaseAttribute = "FunicularSwitch.Generators.UnionCaseAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "Attributes.g.cs",
            Templates.UnionTypeTemplates.StaticCode));

        var unionTypeClasses =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => s.IsTypeDeclarationWithAttributes(),
                    transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration(ctx, UnionTypeAttribute)
                )
                .Where(static target => target != null)
                .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(unionTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses, 
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(Compilation compilation, ImmutableArray<BaseTypeDeclarationSyntax> unionTypeClasses, SourceProductionContext context)
    {
        if (unionTypeClasses.IsDefaultOrEmpty) return;

        var resultTypeSchemata = 
            Parser.GetUnionTypes(compilation, unionTypeClasses, context.ReportDiagnostic, context.CancellationToken)
                .ToImmutableArray();

        var generation =
            resultTypeSchemata.Select(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken));

        foreach (var (filename, source)  in generation) context.AddSource(filename, source);
    }
}