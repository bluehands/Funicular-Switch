using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.EnumType;

static class Parser
{
    static IEnumerable<EnumCase> OrderEnumCases(IEnumerable<EnumCase> enumCases, EnumCaseOrder enumCaseOrder) =>
        (enumCaseOrder == EnumCaseOrder.AsDeclared
            ? enumCases
            : enumCases.OrderBy(m => m.FullCaseName));

    public static IEnumerable<INamedTypeSymbol> GetAccessibleEnumTypeSymbols(INamespaceSymbol @namespace, bool includeInternalEnums)
    {
        static IEnumerable<INamedTypeSymbol> GetTypes(INamespaceOrTypeSymbol namespaceSymbol)
        {
            foreach (var namedTypeSymbol in namespaceSymbol.GetTypeMembers())
            {
                yield return namedTypeSymbol;
                foreach (var typeSymbol in GetTypes(namedTypeSymbol))
                {
                    yield return typeSymbol;
                }
            }

            if (namespaceSymbol is INamespaceSymbol ns)
                foreach (var subNamespace in ns.GetNamespaceMembers())
                {
                    foreach (var namedTypeSymbol in GetTypes(subNamespace))
                    {
                        yield return namedTypeSymbol;
                    }
                }
        }

        var enumTypes = GetTypes(@namespace)
            .Where(t => t.EnumUnderlyingType != null
                        && IsAccessible(t, includeInternalEnums)
            );

        return enumTypes;
    }

    static bool IsAccessible(INamedTypeSymbol t, bool includeInternalEnums)
    {
        var actualAccessibility = t.GetActualAccessibility();

        return actualAccessibility == Accessibility.Public ||
               includeInternalEnums && actualAccessibility == Accessibility.Internal;
    }

    public static EnumTypeSchema ToEnumTypeSchema(this EnumSymbolInfo symbolInfo)
    {
        var enumSymbol = symbolInfo.EnumTypeSymbol.Symbol;

        var fullNamespace = enumSymbol.GetFullNamespace();
        var fullTypeNameWithNamespace = enumSymbol.FullTypeNameWithNamespace();

        var derivedTypes = enumSymbol.GetMembers()
            .OfType<IFieldSymbol>()
            .Select(f => new EnumCase($"{fullTypeNameWithNamespace}.{f.Name}", f.Name));

        var acc = enumSymbol.GetActualAccessibility();
        var extensionAccessibility = acc is Accessibility.NotApplicable or Accessibility.Internal
            ? ExtensionAccessibility.Internal
            : symbolInfo.ExtensionAccessibility;

        return new(fullNamespace, 
            enumSymbol.FullTypeName(), 
            fullTypeNameWithNamespace,
            OrderEnumCases(derivedTypes, symbolInfo.CaseOrder).ToImmutableArray(),
            extensionAccessibility == ExtensionAccessibility.Internal,
            symbolInfo.Precedence
        );
    }
}

sealed record EnumSymbolInfo(
    SymbolWrapper<INamedTypeSymbol> EnumTypeSymbol,
    ExtensionAccessibility ExtensionAccessibility,
    EnumCaseOrder CaseOrder,
    AttributePrecedence Precedence
);

enum AttributePrecedence
{
    Low,
    Medium,
    High
}