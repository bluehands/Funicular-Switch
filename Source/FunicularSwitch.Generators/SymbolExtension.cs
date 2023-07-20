using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

static class SymbolExtension
{
	public static Accessibility GetActualAccessibility(this INamedTypeSymbol t)
	{
		var actualAccessibility = Accessibility.Public;
		var currentSymbol = t;

		do
		{
			if (actualAccessibility > currentSymbol.DeclaredAccessibility)
				actualAccessibility = currentSymbol.DeclaredAccessibility;
			currentSymbol = currentSymbol.ContainingType;
		} while (currentSymbol != null);

		return actualAccessibility;
	}
}