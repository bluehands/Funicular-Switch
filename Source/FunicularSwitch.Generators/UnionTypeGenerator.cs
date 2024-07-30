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
                .ForAttributeWithMetadataName(
                    UnionTypeAttribute,
                    predicate: static (_, _) => true,
                    transform: static (ctx, cancellationToken) => 
                        Parser.GetUnionTypeSchema(ctx.SemanticModel.Compilation, cancellationToken, (BaseTypeDeclarationSyntax)ctx.TargetNode))
                .Select(static (target, _) => target);

        var compilationAndClasses = unionTypeClasses;

        context.RegisterSourceOutput(
            compilationAndClasses, 
            static (spc, source) => Execute(source, spc));
    }

    static void Execute(GenerationResult<UnionTypeSchema> target, SourceProductionContext context)
    {
        var (unionTypeSchema, errors, hasValue) = target;
        foreach (var error in errors) context.ReportDiagnostic(error);
        
        if (!hasValue || unionTypeSchema!.Cases.IsEmpty) return;

        var (filename, source) = Generator.Emit(unionTypeSchema, context.ReportDiagnostic, context.CancellationToken);
        context.AddSource(filename, source);
    }
}