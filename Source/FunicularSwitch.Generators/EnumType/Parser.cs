using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.EnumType;

static class Parser
{
	public static EnumSymbolInfo? GetEnumSymbolInfo(EnumDeclarationSyntax enumTypeClass, AttributeSyntax attribute, SemanticModel semanticModel)
	{
		var enumTypeSymbol = semanticModel.GetDeclaredSymbol(enumTypeClass);
		if (enumTypeSymbol == null)
			return null;

		var (enumCaseOrder, visibility) = GetAttributeParameters(attribute);

		return new(SymbolWrapper.Create(enumTypeSymbol), visibility, enumCaseOrder, AttributePrecedence.High);
	}

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

		var acc = symbolInfo.EnumTypeSymbol.Symbol.GetActualAccessibility();
		var extensionAccessibility = acc is Accessibility.NotApplicable or Accessibility.Internal
			? ExtensionAccessibility.Internal
			: symbolInfo.ExtensionAccessibility;

		return new(fullNamespace, enumSymbol.FullTypeName(), fullTypeNameWithNamespace,
			OrderEnumCases(derivedTypes, symbolInfo.CaseOrder)
				.ToList(),
			extensionAccessibility == ExtensionAccessibility.Internal
		);
	}

	public static (EnumCaseOrder caseOrder, ExtensionAccessibility visibility) GetAttributeParameters(AttributeSyntax attribute)
	{
		var caseOrder = attribute.GetNamedEnumAttributeArgument("CaseOrder", EnumCaseOrder.AsDeclared);
		var visibility = attribute.GetNamedEnumAttributeArgument("Visibility", ExtensionAccessibility.Public);
		return (caseOrder, visibility);
	}
}

record EnumSymbolInfo(
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