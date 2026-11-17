using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.UnionType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class UnionTypeGenerator : IIncrementalGenerator
{
    internal const string UnionTypeAttribute = "FunicularSwitch.Generators.UnionTypeAttribute";
    internal const string ExtendUnionsAttribute = "FunicularSwitch.Generators.ExtendUnionsAttribute";
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
                )
                .WithTrackingName(TrackingNames.UnionTransform);

        var referencesJetBrainsAnnotationsAssembly = context.CompilationProvider
            .SelectMany((c, _) => c.SourceModule.ReferencedAssemblySymbols)
            .Where(a => a.Name == "JetBrains.Annotations")
            .Collect()
            .Select((a, _) => a.Length > 0);

        //one pipeline item per [UnionType] declaration, so editing one union only regenerates that union
        context.RegisterSourceOutput(
            unionTypeClasses.Combine(referencesJetBrainsAnnotationsAssembly).WithTrackingName(TrackingNames.UnionOutput),
            static (spc, source) => Execute(source.Left, source.Right, spc));

        var explicitUnionNames = unionTypeClasses
            .Select(static (r, _) => r.HasValue ? r.Value!.FullTypeNameWithTypeParameters : null)
            .Where(static n => n != null)
            .Collect()
            .Select(static (names, _) => names.Select(n => n!).ToImmutableArray().AsEquatableArray())
            .WithTrackingName(TrackingNames.ExplicitUnionNames);

        var unionsFromAssemblyAttribute = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ExtendUnionsAttribute,
                predicate: static (_, _) => true,
                transform: static (ctx, _) => ctx.Attributes.SelectMany(GetUnionSchemas).ToImmutableArray().AsEquatableArray())
            .WithTrackingName(TrackingNames.ExtendUnionsAttribute)
            .Collect()
            //every union is generated once, even if several attributes cover it
            .SelectMany(static (all, _) => all
                .SelectMany(a => a)
                .GroupBy(s => s.FullTypeNameWithTypeParameters)
                .Select(g => g.First())
                .ToImmutableArray())
            .WithTrackingName(TrackingNames.ExtendedUnions);

        //one output per union, so changing one native union only regenerates that union
        context.RegisterSourceOutput(
            unionsFromAssemblyAttribute.Combine(explicitUnionNames)
                .Where(static s => !s.Right.AsImmutableArray().Contains(s.Left.FullTypeNameWithTypeParameters))
                .Select(static (s, _) => s.Left)
                .Combine(referencesJetBrainsAnnotationsAssembly)
                .WithTrackingName(TrackingNames.ExtendedUnionOutput),
            static (spc, source) =>
            {
                if (source.Left.Cases.IsEmpty) return;
                Emit(source.Left, source.Right, spc);
            });
    }

    //Native unions and hand-written [Union] structs (both report IsUnion) are covered by [assembly: ExtendUnions].
    static IEnumerable<UnionTypeSchema> GetUnionSchemas(AttributeData attribute)
    {
        var attributeAssembly = attribute.AttributeClass!.ContainingAssembly;
        var assembly = attribute.ConstructorArguments.FirstOrDefault().Value is INamedTypeSymbol specifier
            ? specifier.ContainingAssembly
            : attributeAssembly;
        var forceInternal = attribute.GetEnumNamedArgument("Accessibility", ExtensionAccessibility.Public) == ExtensionAccessibility.Internal;

        return EnumType.Parser.GetAccessibleTypeSymbols(assembly.GlobalNamespace,
                SymbolEqualityComparer.Default.Equals(attributeAssembly, assembly),
                t => t.IsUnion)
            .Select(u => NativeUnionParser.GetSchema(u, Location.None, forceInternal))
            .Where(r => r.HasValue)
            .Select(r => r.Value!);
    }

    static void Execute(GenerationResult<UnionTypeSchema> target, bool hasJetbrainsAnnotationsReference, SourceProductionContext context)
    {
        var (unionTypeSchema, errors, hasValue) = target;
        foreach (var error in errors) context.ReportDiagnostic(error);

        if (!hasValue || unionTypeSchema!.Cases.IsEmpty) return;

        Emit(unionTypeSchema, hasJetbrainsAnnotationsReference, context);
    }

    static void Emit(UnionTypeSchema schema, bool hasJetbrainsAnnotationsReference, SourceProductionContext context)
    {
        var (filename, source) = Generator.Emit(schema, context.ReportDiagnostic, hasJetbrainsAnnotationsReference, context.CancellationToken);
        context.AddSource(filename, source);
    }
}

public static class TrackingNames
{
    public const string UnionTransform = nameof(UnionTransform);
    public const string UnionOutput = nameof(UnionOutput);
    public const string ExplicitUnionNames = nameof(ExplicitUnionNames);
    public const string ExtendUnionsAttribute = nameof(ExtendUnionsAttribute);
    public const string ExtendedUnions = nameof(ExtendedUnions);
    public const string ExtendedUnionOutput = nameof(ExtendedUnionOutput);
    public const string ResultTypeTransform = nameof(ResultTypeTransform);
    public const string ResultTypeOutput = nameof(ResultTypeOutput);
    public const string EnumTransform = nameof(EnumTransform);
    public const string EnumOutput = nameof(EnumOutput);
}
