using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.EnumType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        var enumTypeClasses =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => s is EnumDeclarationSyntax && s.IsTypeDeclarationWithAttributes()
                                                || s.IsAssemblyAttribute(),
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)
                        .Select(ToEnumTypeSchema)
                        .ToImmutableArray()
                        .AsEquatableArray()
                );

        context.RegisterSourceOutput(
            enumTypeClasses.Collect(),
            static (spc, source) =>
                Execute(source.SelectMany(s => s).ToImmutableArray(), spc));
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

    static IEnumerable<EnumSymbolInfo> GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        switch (context.Node)
        {
            case EnumDeclarationSyntax enumDeclarationSyntax:
            {
                return GetSymbolInfoFromEnumDeclaration(context, enumDeclarationSyntax);
            }
            case AttributeSyntax extendEnumTypesAttribute:
            {
                var semanticModel = context.SemanticModel;
                var attributeFullName = extendEnumTypesAttribute.GetAttributeFullName(semanticModel);

                return attributeFullName switch
                {
                    ExtendEnumsAttribute => GetSymbolInfosForExtendEnumTypesAttribute(extendEnumTypesAttribute, semanticModel),
                    ExtendEnumAttribute => GetSymbolInfosForExtendEnumTypeAttribute(extendEnumTypesAttribute, semanticModel),
                    _ => []
                };
            }
            default:
                throw new ArgumentException($"Unexpected node of type {context.Node.GetType()}");
        }
    }

    static IEnumerable<EnumSymbolInfo> GetSymbolInfosForExtendEnumTypeAttribute(AttributeSyntax extendEnumTypesAttribute, SemanticModel semanticModel)
    {
        var typeofExpression = extendEnumTypesAttribute.ArgumentList?.Arguments
            .Select(a => a.Expression)
            .OfType<TypeOfExpressionSyntax>()
            .FirstOrDefault();

        if (typeofExpression == null)
            return [];

        if (semanticModel.GetSymbolInfo(typeofExpression.Type).Symbol is not INamedTypeSymbol typeSymbol)
            return [];

        if (typeSymbol.EnumUnderlyingType == null)
            return [];

        var (caseOrder, visibility) = Parser.GetAttributeParameters(extendEnumTypesAttribute);
        return new[] { new EnumSymbolInfo(SymbolWrapper.Create(typeSymbol), visibility, caseOrder, AttributePrecedence.Medium) };
    }

    static IEnumerable<EnumSymbolInfo> GetSymbolInfosForExtendEnumTypesAttribute(AttributeSyntax extendEnumTypesAttribute, SemanticModel semanticModel)
    {
        var typeofExpression = extendEnumTypesAttribute.ArgumentList?.Arguments
            .Select(a => a.Expression)
            .OfType<TypeOfExpressionSyntax>()
            .FirstOrDefault();

        var attributeSymbol = semanticModel.GetSymbolInfo(extendEnumTypesAttribute).Symbol!;
        var enumFromAssembly = typeofExpression != null
            ? semanticModel.GetSymbolInfo(typeofExpression.Type).Symbol!.ContainingAssembly
            : attributeSymbol.ContainingAssembly;

        var (caseOrder, visibility) = Parser.GetAttributeParameters(extendEnumTypesAttribute);

        return Parser.GetAccessibleEnumTypeSymbols(enumFromAssembly.GlobalNamespace,
                SymbolEqualityComparer.Default.Equals(attributeSymbol.ContainingAssembly, enumFromAssembly))
            .Where(e =>
                (e.Name != "ExtensionAccessibility" || e.GetFullNamespace() != FunicularSwitchGeneratorsNamespace) &&
                (e.Name != "EnumCaseOrder" || e.GetFullNamespace() != FunicularSwitchGeneratorsNamespace) &&
                (e.Name != "CaseOrder" || e.GetFullNamespace() != FunicularSwitchGeneratorsNamespace))
            .Select(e => new EnumSymbolInfo(SymbolWrapper.Create(e), visibility, caseOrder, AttributePrecedence.Low));
    }

    static IEnumerable<EnumSymbolInfo> GetSymbolInfoFromEnumDeclaration(GeneratorSyntaxContext context,
        EnumDeclarationSyntax enumDeclarationSyntax)
    {
        AttributeSyntax? enumTypeAttribute = null;
        foreach (var attributeListSyntax in enumDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var semanticModel = context.SemanticModel;
                var attributeFullName = attributeSyntax.GetAttributeFullName(semanticModel);
                if (attributeFullName != ExtendedEnumAttribute) continue;
                enumTypeAttribute = attributeSyntax;
                goto Return;
            }
        }

        Return:
        if (enumTypeAttribute == null)
            return [];

        var schema = Parser.GetEnumSymbolInfo(enumDeclarationSyntax, enumTypeAttribute, context.SemanticModel);

        return schema == null ? Enumerable.Empty<EnumSymbolInfo>() : new[] { schema };
    }
}