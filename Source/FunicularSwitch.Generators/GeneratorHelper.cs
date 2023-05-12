using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

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

	public static EnumTypesAttributeInfo? GetSemanticTargetForGeneration2(GeneratorSyntaxContext context, string expectedAttributeName)
	{
		var attributeSyntax = (AttributeSyntax)context.Node;
		var semanticModel = context.SemanticModel;
		var attributeFullName = attributeSyntax.GetAttributeFullName(semanticModel);
		if (attributeFullName != expectedAttributeName) return null;

		IAssemblySymbol enumFromAssembly;
		if (attributeSyntax.ArgumentList?.Arguments.Count > 0)
		{
			var typeofExpression = (TypeOfExpressionSyntax)attributeSyntax.ArgumentList.Arguments[0].Expression;
			enumFromAssembly = semanticModel.GetSymbolInfo(typeofExpression.Type).Symbol!.ContainingAssembly;
		}
		else
		{
			enumFromAssembly = semanticModel.GetSymbolInfo(attributeSyntax).Symbol!.ContainingAssembly;
		}

		return new EnumTypesAttributeInfo(enumFromAssembly);
	}

	public class EnumTypesAttributeInfo
	{
		public EnumTypesAttributeInfo(IAssemblySymbol assemblySymbol)
		{
			AssemblySymbol = assemblySymbol;
		}

		public IAssemblySymbol AssemblySymbol { get; }

		protected bool Equals(EnumTypesAttributeInfo other) => SymbolEqualityComparer.Default.Equals(AssemblySymbol, other.AssemblySymbol);

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((EnumTypesAttributeInfo)obj);
		}

		public override int GetHashCode() => SymbolEqualityComparer.Default.GetHashCode(AssemblySymbol);
	}
}