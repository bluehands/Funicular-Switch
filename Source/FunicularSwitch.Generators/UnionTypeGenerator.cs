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
                    {
                        var symbol = (INamedTypeSymbol)context.TargetSymbol;
                        if (symbol.IsUnion)
                            //C# 15 union declaration: cases are the union case types, no derived types to look for
                            return NativeUnionParser.GetSchema(symbol, context.TargetNode.GetLocation());

                        //[UnionType] on a plain struct has no meaning
                        if (symbol.TypeKind == TypeKind.Struct)
                            return GenerationResult<UnionTypeSchema>.Empty.AddDiagnostics(
                                Diagnostics.UnionTypeOnNonUnionStruct(
                                    $"{symbol.Name} is a struct but neither a union declaration nor marked with [Union]",
                                    context.TargetNode.GetLocation()));
                        if (context.TargetNode is not BaseTypeDeclarationSyntax typeDeclaration)
                            return GenerationResult<UnionTypeSchema>.Empty;

                        return Parser.GetUnionTypeSchema(
                            context.SemanticModel.Compilation,
                            cancellationToken,
                            typeDeclaration,
                            symbol,
                            context.Attributes[0]
                        );
                    }
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