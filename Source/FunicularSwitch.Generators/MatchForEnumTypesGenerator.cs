using FunicularSwitch.Generators.EnumType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

enum ExtensionVisibility
{
	Internal,
	Public
}

[Generator]
public class MatchForEnumTypesGenerator : IIncrementalGenerator
{
	internal const string EnumTypeAttribute = "FunicularSwitch.Generators.ExtendEnumTypesAttribute";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var enumTypeClasses =
			context.SyntaxProvider
				.CreateSyntaxProvider(
					predicate: static (s, _) => s is AttributeSyntax,
					transform: static (ctx, _) => GetSemanticTargetForGeneration2(ctx, EnumTypeAttribute)
				)
				.Where(static target => target != null)
				.Select(static (target, _) => target!)
				.SelectMany(static (target, _) => 
					Parser.GetEnumTypes(
					target.AssemblySymbol.GlobalNamespace,
					target
				));

		context.RegisterSourceOutput(
			enumTypeClasses,
			static (spc, source) => Execute(source, spc)
		);
	}
	
	static void Execute(EnumTypeSchema enumTypeSchema, SourceProductionContext spc)
	{
		var (filename, source) = Generator.Emit(enumTypeSchema, spc.ReportDiagnostic, spc.CancellationToken);
		spc.AddSource(filename, source);
	}

	static EnumTypesAttributeInfo? GetSemanticTargetForGeneration2(GeneratorSyntaxContext context, string expectedAttributeName)
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

		var caseOrder = attributeSyntax.GetNamedEnumAttributeArgument("CaseOrder", EnumCaseOrder.AsDeclared);
		var visibility = attributeSyntax.GetNamedEnumAttributeArgument("Visibility", ExtensionVisibility.Public);

		return new(enumFromAssembly, caseOrder, visibility);
	}
}

class EnumTypesAttributeInfo : IEquatable<EnumTypesAttributeInfo>
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