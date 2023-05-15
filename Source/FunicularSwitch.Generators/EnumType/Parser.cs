using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;

namespace FunicularSwitch.Generators.EnumType;

static class Parser
{
	public static IEnumerable<EnumTypeSchema> GetEnumTypes(Compilation compilation,
		ImmutableArray<BaseTypeDeclarationSyntax> enumTypeClasses, Action<Diagnostic> reportDiagnostic,
		CancellationToken cancellationToken) =>
		enumTypeClasses
			.OfType<EnumDeclarationSyntax>()
			.Select(enumTypeClass =>
			{
				var semanticModel = compilation.GetSemanticModel(enumTypeClass.SyntaxTree);

				return GetEnumTypeSchema(enumTypeClass, semanticModel, reportDiagnostic);
			})
			.Where(enumTypeClass => enumTypeClass != null)!;

	public static EnumTypeSchema? GetEnumTypeSchema(EnumDeclarationSyntax enumTypeClass, SemanticModel semanticModel,
		Action<Diagnostic> reportDiagnostic)
	{
		var enumTypeSymbol = semanticModel.GetDeclaredSymbol(enumTypeClass);

		if (enumTypeSymbol == null) //TODO: report diagnostics
			return null;

		var fullTypeName = enumTypeSymbol.FullTypeNameWithNamespace();
		var acc = enumTypeSymbol.DeclaredAccessibility;
		if (acc is Accessibility.Private or Accessibility.Protected)
		{
			reportDiagnostic(Diagnostics.EnumTypeIsNotAccessible($"{fullTypeName} needs at least internal accessibility",
				enumTypeClass.GetLocation()));
			return null;
		}

		var attribute = enumTypeClass.AttributeLists
			.Select(l =>
				l.Attributes.First(a => a.GetAttributeFullName(semanticModel) == EnumTypeGenerator.EnumTypeAttribute))
			.First();

		var enumCaseOrder = TryGetCaseOrder(attribute, reportDiagnostic);
		var fullTypeNameWithNamespace = enumTypeSymbol.FullTypeNameWithNamespace();
		var enumCases = enumTypeClass.Members
			.Select(m => new DerivedType($"{fullTypeNameWithNamespace}.{m.Identifier.Text}", m.Identifier.Text));

		return new EnumTypeSchema(
			Namespace: enumTypeSymbol.GetFullNamespace(),
			TypeName: enumTypeSymbol.Name,
			FullTypeName: fullTypeName,
			Cases: (enumCaseOrder == EnumCaseOrder.AsDeclared
				? enumCases
				: enumCases.OrderBy(m => m.FullTypeName))
			.ToImmutableArray(),
			acc is Accessibility.NotApplicable or Accessibility.Internal
		);
	}

	static EnumCaseOrder TryGetCaseOrder(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
	{
		if ((attribute.ArgumentList?.Arguments.Count ?? 0) < 1)
			return EnumCaseOrder.AsDeclared;

		var expressionSyntax = attribute.ArgumentList?.Arguments[0].Expression;
		if (expressionSyntax is not MemberAccessExpressionSyntax l)
		{
			//TODO: report diagnostics
			return EnumCaseOrder.AsDeclared;
		}

		return (EnumCaseOrder)Enum.Parse(typeof(EnumCaseOrder), l.Name.ToString());
	}

	public static IEnumerable<EnumTypeSchema> GetEnumTypes(INamespaceSymbol globalNamespace, ImmutableArray<GeneratorHelper.EnumTypesAttributeInfo> enumTypesAttributes, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
	{
		static IEnumerable<INamedTypeSymbol> GetTypes(INamespaceOrTypeSymbol namespaceSymbol)
		{
			foreach (var namedTypeSymbol in namespaceSymbol.GetTypeMembers())
			{
				yield return namedTypeSymbol;

				foreach (var subTypeSymbol in GetTypes(namedTypeSymbol))
				{
					yield return subTypeSymbol;
				}
			}
		}

		var enumTypes = GetTypes(globalNamespace)
			.Where(t => t.EnumUnderlyingType != null
						&& t.DeclaredAccessibility == Accessibility.Public //TODO: handle nested types correctly: https://stackoverflow.com/questions/54480727/finding-the-effective-accessibility-of-fields-and-types-in-a-roslyn-analyzer
						&& enumTypesAttributes.Any(e => SymbolEqualityComparer.Default.Equals(e.AssemblySymbol, t.ContainingAssembly)))
			.ToList();

		return enumTypes.Select(e =>
		{
			var fullNamespace = e.GetFullNamespace();
			var fullTypeNameWithNamespace = e.FullTypeNameWithNamespace();
			return new EnumTypeSchema(fullNamespace, e.Name, fullTypeNameWithNamespace,
				e.GetMembers().OfType<IFieldSymbol>().Select(f => new DerivedType($"{fullTypeNameWithNamespace}.{f.Name}", f.Name))
					.ToList()
				, false
			);
		});
	}
}

enum EnumCaseOrder
{
	Alphabetic,
	AsDeclared
}