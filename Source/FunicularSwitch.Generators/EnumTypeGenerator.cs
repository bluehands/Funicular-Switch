using FunicularSwitch.Generators.EnumType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class EnumTypeGenerator : IIncrementalGenerator
{
	internal const string EnumTypeAttribute = "FunicularSwitch.Generators.EnumTypeAttribute";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
			"Attributes.g.cs",
			Templates.EnumTypeTemplates.StaticCode));

		var enumTypeClasses =
			context.SyntaxProvider
				.CreateSyntaxProvider(
					predicate: static (s, _) => s is EnumDeclarationSyntax && s.IsTypeDeclarationWithAttributes(),
					transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx, EnumTypeAttribute)
				)
				.Where(static target => target != null)
				.Select(static (target, _) => target!);

		context.RegisterSourceOutput(
			enumTypeClasses,
			static (spc, source) => Execute(source, spc));
	}

	static void Execute(EnumTypeSchema enumTypeSchema, SourceProductionContext context)
	{
		var (filename, source) = Generator.Emit(enumTypeSchema, context.ReportDiagnostic, context.CancellationToken);
		context.AddSource(filename, source);
	}

	static EnumTypeSchema? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, string expectedAttributeName)
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

		//TODO: return class dec with semantic model and parse / generate in later stage
		var schema = Parser.GetEnumTypeSchema(classDeclarationSyntax, context.SemanticModel, _ => { });

		return schema;
	}
}

internal class FileLog
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