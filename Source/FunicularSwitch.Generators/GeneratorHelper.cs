using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FunicularSwitch.Generators.EnumType;

namespace FunicularSwitch.Generators;

static class GeneratorHelper
{
	public static BaseTypeDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, string expectedAttributeName)
	{
		var classDeclarationSyntax = (BaseTypeDeclarationSyntax)context.Node;
		var hasAttribute = false;
		foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
		{
			foreach (var attributeSyntax in attributeListSyntax.Attributes)
			{
				var semanticModel = context.SemanticModel;
				var attributeFullName = attributeSyntax.GetAttributeFullName(semanticModel);
				if (attributeFullName != expectedAttributeName) continue;
				hasAttribute = true;
				goto Return;
			}
		}

	Return:
		return hasAttribute ? classDeclarationSyntax : null;
	}

	public static T GetNamedEnumAttributeArgument<T>(this AttributeSyntax attribute, string name, T defaultValue) where T : struct
	{
		var memberAccess = attribute.ArgumentList?.Arguments
			.Where(a => a.NameEquals?.Name.ToString() == name)
			.Select(a => a.Expression)
			.OfType<MemberAccessExpressionSyntax>()
			.FirstOrDefault();


		if (memberAccess == null) return defaultValue;

		return (T)Enum.Parse(typeof(T), memberAccess.Name.ToString());
	}
}

public class SymbolWrapper
{
	public static SymbolWrapper<T> Create<T>(T symbol) where T : ISymbol => new(symbol);
}

public class SymbolWrapper<T> : IEquatable<SymbolWrapper<T>> where T : ISymbol
{
	public SymbolWrapper(T symbol) => Symbol = symbol;

	public T Symbol { get; }

	public bool Equals(SymbolWrapper<T>? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return SymbolEqualityComparer.Default.Equals(Symbol, other.Symbol);
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SymbolWrapper<T>)obj);
	}

	public override int GetHashCode() => SymbolEqualityComparer.Default.GetHashCode(Symbol);
}