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
    internal const string DerivedTypeShapeAttribute = "DerivedTypeShapeAttribute";

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
                    transform: static (context, cancellationToken) =>
                        Parser.GetUnionTypeSchema(
                            context.SemanticModel.Compilation,
                            cancellationToken,
                            (BaseTypeDeclarationSyntax)context.TargetNode,
                            (INamedTypeSymbol)context.TargetSymbol,
                            context.Attributes[0]
                        )
                )
                .Combine(context.CompilationProvider
                    .SelectMany(static (compilation, _) => compilation.GlobalNamespace.GetNamespaceMembers())
                    .Where(static (namespaceMember) => namespaceMember.Name == "PolyType")
                    .Select(static (namespaceMember, _) =>
                    {
                        foreach (var namedTypeSymbol in namespaceMember.GetTypeMembers())
                        {
                           if (namedTypeSymbol.Name == DerivedTypeShapeAttribute)
                               return true;
                        }
                        return false;
                    })
                    .Where(t => t)
                    .Collect());
        
        context.RegisterSourceOutput(
            unionTypeClasses, 
            static (spc, source) => Execute(source.Left, source.Right.Length > 0, spc));
    }

    static void Execute(GenerationResult<UnionTypeSchema> target, bool hasPolyTypeReference, SourceProductionContext context)
    {
        var (unionTypeSchema, errors, hasValue) = target;
        foreach (var error in errors) context.ReportDiagnostic(error);
        
        if (!hasValue || unionTypeSchema!.Cases.IsEmpty) return;

        var (filename, source) = Generator.Emit(unionTypeSchema, hasPolyTypeReference, context.ReportDiagnostic, context.CancellationToken);
        context.AddSource(filename, source);
    }
}