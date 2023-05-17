using System.Collections.Immutable;
using FunicularSwitch.Generators.EnumType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class EnumTypeGenerator : IIncrementalGenerator
{
	const string EnumTypeAttribute = "FunicularSwitch.Generators.EnumTypeAttribute";
	const string ExtendEnumTypesAttribute = "FunicularSwitch.Generators.ExtendEnumTypesAttribute";
	const string ExtendEnumTypeAttribute = "FunicularSwitch.Generators.ExtendEnumTypeAttribute";

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
				)
				.SelectMany(static (target, _) => target!)
				.Where(static target => target != null);

		context.RegisterSourceOutput(
			enumTypeClasses.Collect(),
			static (spc, source) => Execute(source!, spc));
	}

	static void Execute(ImmutableArray<EnumSymbolInfo> enumSymbolInfos, SourceProductionContext context)
	{
		foreach (var enumSymbolInfo in enumSymbolInfos
					 .GroupBy(s => s.EnumTypeSymbol)
					 .Select(g => g.OrderByDescending(s => s.Precedence).First()))
		{
			var acc = enumSymbolInfo.EnumTypeSymbol.Symbol.GetActualAccessibility();
			if (acc is Accessibility.Private or Accessibility.Protected)
			{
				context.ReportDiagnostic(Diagnostics.EnumTypeIsNotAccessible($"{enumSymbolInfo.EnumTypeSymbol.Symbol.FullTypeNameWithNamespace()} needs at least internal accessibility",
						enumSymbolInfo.EnumTypeSymbol.Symbol.Locations.FirstOrDefault() ?? Location.None));
				continue;
			}

			var enumTypeSchema = enumSymbolInfo.ToEnumTypeSchema();
			var (filename, source) = Generator.Emit(enumTypeSchema, context.ReportDiagnostic, context.CancellationToken);
			context.AddSource(filename, source);
		}
	}

	static IEnumerable<EnumSymbolInfo?> GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
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
						ExtendEnumTypesAttribute => GetSymbolInfosForExtendEnumTypesAttribute(extendEnumTypesAttribute, semanticModel),
						ExtendEnumTypeAttribute => GetSymbolInfosForExtendEnumTypeAttribute(extendEnumTypesAttribute, semanticModel),
						_ => Enumerable.Empty<EnumSymbolInfo>()
					};
				}
			default:
				throw new ArgumentException($"Unexpected node of type {context.Node.GetType()}");
		}
	}

	static IEnumerable<EnumSymbolInfo?> GetSymbolInfosForExtendEnumTypeAttribute(AttributeSyntax extendEnumTypesAttribute, SemanticModel semanticModel)
	{
		var typeofExpression = extendEnumTypesAttribute.ArgumentList?.Arguments
			.Select(a => a.Expression)
			.OfType<TypeOfExpressionSyntax>()
			.FirstOrDefault();

		if (typeofExpression == null)
			return Enumerable.Empty<EnumSymbolInfo?>();

		if (semanticModel.GetSymbolInfo(typeofExpression.Type).Symbol is not INamedTypeSymbol typeSymbol)
			return Enumerable.Empty<EnumSymbolInfo?>();
		
		if (typeSymbol.EnumUnderlyingType == null)
			return Enumerable.Empty<EnumSymbolInfo?>();

		var (caseOrder, visibility) = Parser.GetAttributeParameters(extendEnumTypesAttribute);
		return new[] { new EnumSymbolInfo(SymbolWrapper.Create(typeSymbol), visibility, caseOrder, AttributePrecedence.Medium) };
	}

	static IEnumerable<EnumSymbolInfo?> GetSymbolInfosForExtendEnumTypesAttribute(AttributeSyntax extendEnumTypesAttribute, SemanticModel semanticModel)
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

	static IEnumerable<EnumSymbolInfo?> GetSymbolInfoFromEnumDeclaration(GeneratorSyntaxContext context,
		EnumDeclarationSyntax enumDeclarationSyntax)
	{
		AttributeSyntax? enumTypeAttribute = null;
		foreach (var attributeListSyntax in enumDeclarationSyntax.AttributeLists)
		{
			foreach (var attributeSyntax in attributeListSyntax.Attributes)
			{
				var semanticModel = context.SemanticModel;
				var attributeFullName = attributeSyntax.GetAttributeFullName(semanticModel);
				if (attributeFullName != EnumTypeAttribute) continue;
				enumTypeAttribute = attributeSyntax;
				goto Return;
			}
		}

		Return:
		if (enumTypeAttribute == null)
			return Enumerable.Empty<EnumSymbolInfo?>();

		var schema = Parser.GetEnumSymbolInfo(enumDeclarationSyntax, enumTypeAttribute, context.SemanticModel);

		return new[] { schema };
	}
}

class FileLog
{
	public static void LogAccess(TimeSpan elapsed, string message)
	{
		var filePath = @"c:\temp\callcount.txt";
		var line = $"{DateTime.Now:HH:mm:ss fff}: {message} ({elapsed})";
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, line);
		}
		else
		{
			File.AppendAllLines(filePath, new[] { line });
		}
	}
}