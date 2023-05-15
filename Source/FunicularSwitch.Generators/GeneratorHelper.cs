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

	public static EnumTypeSchema? GetSemanticTargetForGeneration3(GeneratorSyntaxContext context, string expectedAttributeName)
	{
		var classDeclarationSyntax = (EnumDeclarationSyntax)context.Node;
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
		if (!hasAttribute)
			return null;

		var schema = Parser.GetEnumTypeSchema(classDeclarationSyntax, context.SemanticModel, _ => { });

		return schema;
	}

	public static EnumTypesAttributeInfo? GetSemanticTargetForGeneration2(GeneratorSyntaxContext context, string expectedAttributeName)
	{
		var attributeSyntax = (AttributeSyntax)context.Node;
		var semanticModel = context.SemanticModel;
		var attributeFullName = attributeSyntax.GetAttributeFullName(semanticModel);
		if (attributeFullName != expectedAttributeName) return null;

		var typeofExpression = attributeSyntax.ArgumentList?.Arguments
			.Select(a => a.Expression)
			.OfType<TypeOfExpressionSyntax>()
			.FirstOrDefault();

		var enumFromAssembly = typeofExpression != null
				? semanticModel.GetSymbolInfo(typeofExpression.Type).Symbol!.ContainingAssembly
				: semanticModel.GetSymbolInfo(attributeSyntax).Symbol!.ContainingAssembly;

		var caseOrder = GetNamedEnumAttributeArgument(attributeSyntax, "CaseOrder", EnumCaseOrder.AsDeclared);
		var visibility = GetNamedEnumAttributeArgument(attributeSyntax, "Visibility", ExtensionVisibility.Public);

		return new(enumFromAssembly, caseOrder, visibility);
	}

	public static T GetNamedEnumAttributeArgument<T>(AttributeSyntax attribute, string name, T defaultValue) where T : struct
	{
		var memberAccess = attribute.ArgumentList?.Arguments
			.Where(a => a.NameEquals?.Name.ToString() == name)
			.Select(a => a.Expression)
			.OfType<MemberAccessExpressionSyntax>()
			.FirstOrDefault();


		if (memberAccess == null) return defaultValue;

		return (T)Enum.Parse(typeof(T), memberAccess.Name.ToString());
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


	public class EnumTypesAttributeInfo : IEquatable<EnumTypesAttributeInfo>
	{
		public EnumTypesAttributeInfo(IAssemblySymbol assemblySymbol, 
			EnumCaseOrder caseOrder, 
			ExtensionVisibility visibility)
		{
			AssemblySymbol = assemblySymbol;
			CaseOrder = caseOrder;
			Visibility = visibility;
		}

		public IAssemblySymbol AssemblySymbol { get; }
		public EnumCaseOrder CaseOrder { get; }
		public ExtensionVisibility Visibility { get; }

		public bool Equals(EnumTypesAttributeInfo other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return SymbolEqualityComparer.Default.Equals(AssemblySymbol, other.AssemblySymbol) && CaseOrder == other.CaseOrder && Visibility == other.Visibility;
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((EnumTypesAttributeInfo)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = SymbolEqualityComparer.Default.GetHashCode(AssemblySymbol);
				hashCode = (hashCode * 397) ^ (int)CaseOrder;
				hashCode = (hashCode * 397) ^ (int)Visibility;
				return hashCode;
			}
		}
	}
}