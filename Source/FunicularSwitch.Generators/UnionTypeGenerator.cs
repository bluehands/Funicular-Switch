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
                );
        
        var referencesJetBrainsAnnotationsAssembly = context.CompilationProvider
            .SelectMany((c, _) => c.SourceModule.ReferencedAssemblySymbols)
            .Where(a => a.Name == "JetBrains.Annotations")
            .Collect()
            .Select((a, _) => a.Length > 0);
        
        context.RegisterSourceOutput(
            unionTypeClasses.Combine(referencesJetBrainsAnnotationsAssembly),
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(GenerationResult<UnionTypeSchema> target, bool hasJetbrainsAnnotationsReference, SourceProductionContext context)
    {
        var (unionTypeSchema, errors, hasValue) = target;
        foreach (var error in errors) context.ReportDiagnostic(error);
        
        if (!hasValue || unionTypeSchema!.Cases.IsEmpty) return;

        var (filename, source) = Generator.Emit(unionTypeSchema, context.ReportDiagnostic, hasJetbrainsAnnotationsReference, context.CancellationToken);
        context.AddSource(filename, source);
    }
}