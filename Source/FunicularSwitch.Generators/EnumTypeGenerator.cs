using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.EnumType;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

[Generator]
public class EnumTypeGenerator : IIncrementalGenerator
{
    const string ExtendedEnumAttribute = "FunicularSwitch.Generators.ExtendedEnumAttribute";
    const string ExtendEnumsAttribute = "FunicularSwitch.Generators.ExtendEnumsAttribute";
    const string ExtendEnumAttribute = "FunicularSwitch.Generators.ExtendEnumAttribute";

    const string FunicularSwitchGeneratorsNamespace = "FunicularSwitch.Generators";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "Attributes.g.cs",
            Templates.EnumTypeTemplates.StaticCode));

        var attributedEnums = GetEnumSymbols(ExtendedEnumAttribute);
        var enumsFromExtendEnumAttribute = GetEnumSymbols(ExtendEnumAttribute);
        var enumsFromExtendEnumsAttribute = GetEnumSymbols(ExtendEnumsAttribute);

        var enumSymbols = attributedEnums
            .Combine(enumsFromExtendEnumsAttribute)
            .Combine(enumsFromExtendEnumAttribute)
            .SelectMany((t, _) => t.Left.Left
                .SelectMany(l => l)
                .Concat(t.Left.Right.SelectMany(l => l))
                .Concat(t.Right.SelectMany(l => l)));

        context.RegisterSourceOutput(
            enumSymbols.Collect(),
            static (spc, source) =>
                Execute(source, spc));

        IncrementalValueProvider<ImmutableArray<EquatableArray<(EnumTypeSchema? enumTypeSchema, DiagnosticInfo? error)>>> GetEnumSymbols(string attributeName) =>
            context.SyntaxProvider
                .ForAttributeWithMetadataName(attributeName,
                    predicate: static (s, _) => true,
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)
                        .Select(ToEnumTypeSchema)
                        .ToImmutableArray()
                        .AsEquatableArray()
                )
                .Collect()
            ;
    }

    static (EnumTypeSchema? enumTypeSchema, DiagnosticInfo? error) ToEnumTypeSchema(EnumSymbolInfo enumSymbolInfo)
    {
        var enumSymbol = enumSymbolInfo.EnumTypeSymbol.Symbol;

        var acc = enumSymbol.GetActualAccessibility();
        if (acc is Accessibility.Private or Accessibility.Protected)
        {
            var diagnostic = Diagnostics.EnumTypeIsNotAccessible($"{enumSymbol.FullTypeNameWithNamespace()} needs at least internal accessibility",
                enumSymbol.Locations.FirstOrDefault() ?? Location.None);
            return (null, new(diagnostic));
        }

        var isFlags = enumSymbol.GetAttributes().Any(a => a.AttributeClass?.FullTypeNameWithNamespace() == "System.FlagsAttribute");
        if (isFlags)
            return (null, null); //TODO: report diagnostics in case of explicit EnumType attribute

#pragma warning disable RS1024
        var hasDuplicates = enumSymbol.GetMembers()
            .OfType<IFieldSymbol>()
            .GroupBy(f => f.ConstantValue ?? 0)
#pragma warning restore RS1024
            .Any(g => g.Count() > 1);

        if (hasDuplicates)
            return (null, null); //TODO: report diagnostics in case of explicit EnumType attribute

        var enumTypeSchema = enumSymbolInfo.ToEnumTypeSchema();
        return (enumTypeSchema, null);
    }

    static void Execute(IReadOnlyCollection<(EnumTypeSchema? enumTypeSchema, DiagnosticInfo? error)> enumSymbolInfos, SourceProductionContext context)
    {
        foreach (var (_, diagnostic) in enumSymbolInfos)
        {
            if (diagnostic == null)
                continue;
            context.ReportDiagnostic(diagnostic);
        }

        foreach (var enumSymbolInfo in enumSymbolInfos
                     .Select(s => s.enumTypeSchema)
                     .Where(s => s != null)
                     .GroupBy(s => s!.FullTypeName)
                     .Select(g => g
                         .OrderByDescending(s => s!.Precedence)
                         .First()))
        {
            var (filename, source) = Generator.Emit(enumSymbolInfo!, context.ReportDiagnostic, context.CancellationToken);
            context.AddSource(filename, source);
        }
    }

    static IEnumerable<EnumSymbolInfo> GetSemanticTargetForGeneration(GeneratorAttributeSyntaxContext context) =>
        context.Attributes.SelectMany(attributeData =>
        {
            var attributeFullName = attributeData.AttributeClass?.FullTypeNameWithNamespace();
            return attributeFullName switch
            {
                ExtendedEnumAttribute => GetSymbolInfoFromEnumDeclaration(attributeData, context.TargetSymbol as INamedTypeSymbol),
                ExtendEnumsAttribute => GetSymbolInfosForExtendEnumTypesAttribute(attributeData),
                ExtendEnumAttribute => GetSymbolInfosForExtendEnumTypeAttribute(attributeData),
                _ => []
            };
        });

    static IEnumerable<EnumSymbolInfo> GetSymbolInfosForExtendEnumTypeAttribute(AttributeData extendEnumTypesAttribute)
    {
        if (extendEnumTypesAttribute.ConstructorArguments[0].Value 
                is not INamedTypeSymbol typeSymbol || typeSymbol.EnumUnderlyingType == null)
            yield break;

        var (caseOrder, visibility) = GetAttributeNamedArguments(extendEnumTypesAttribute);

        yield return new(SymbolWrapper.Create(typeSymbol), visibility, caseOrder, AttributePrecedence.Medium);
    }

    static (EnumCaseOrder caseOrder, ExtensionAccessibility visibility) GetAttributeNamedArguments(
        AttributeData extendEnumTypesAttribute)
    {
        var caseOrder = extendEnumTypesAttribute.GetEnumNamedArgument("CaseOrder", EnumCaseOrder.AsDeclared);
        var visibility = extendEnumTypesAttribute.GetEnumNamedArgument("Accessibility", ExtensionAccessibility.Public);
        return (caseOrder, visibility);
    }

    static IEnumerable<EnumSymbolInfo> GetSymbolInfosForExtendEnumTypesAttribute(AttributeData extendEnumTypesAttribute)
    {
        var attributeSymbol = extendEnumTypesAttribute.AttributeClass!;

        var enumFromAssembly = extendEnumTypesAttribute.ConstructorArguments.FirstOrDefault().Value 
            is INamedTypeSymbol typeFromAssembly
            ? typeFromAssembly.ContainingAssembly
            : attributeSymbol.ContainingAssembly;

        var (caseOrder, visibility) = GetAttributeNamedArguments(extendEnumTypesAttribute);

        return Parser.GetAccessibleEnumTypeSymbols(enumFromAssembly.GlobalNamespace,
                SymbolEqualityComparer.Default.Equals(attributeSymbol.ContainingAssembly, enumFromAssembly))
            .Where(e =>
                (e.Name != "ExtensionAccessibility" || e.GetFullNamespace() != FunicularSwitchGeneratorsNamespace) &&
                (e.Name != "EnumCaseOrder" || e.GetFullNamespace() != FunicularSwitchGeneratorsNamespace) &&
                (e.Name != "CaseOrder" || e.GetFullNamespace() != FunicularSwitchGeneratorsNamespace))
            .Select(e => new EnumSymbolInfo(SymbolWrapper.Create(e), visibility, caseOrder, AttributePrecedence.Low));
    }

    static IEnumerable<EnumSymbolInfo> GetSymbolInfoFromEnumDeclaration(AttributeData extendedEnum, INamedTypeSymbol? targetSymbol)
    {
        var enumDeclarationSyntax = extendedEnum.ApplicationSyntaxReference?.GetSyntax();
        if (enumDeclarationSyntax == null || targetSymbol == null)
            yield break;

        var (enumCaseOrder, visibility) = GetAttributeNamedArguments(extendedEnum);

        yield return new(SymbolWrapper.Create(targetSymbol), visibility, enumCaseOrder, AttributePrecedence.High);
    }
}