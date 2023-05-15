using System.Collections.Immutable;
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
					transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration3(ctx, EnumTypeAttribute)
				)
				.Where(static target => target != null)
				.Select(static (target, _) => target!);

		context.RegisterSourceOutput(
			enumTypeClasses,
			static (spc, source) => Execute(source, spc));
	}

	static void Execute(EnumTypeSchema enumTypeSchema, SourceProductionContext context)
	{
		//FileLog.LogAccess();

		var (filename, source) = Generator.Emit(enumTypeSchema, context.ReportDiagnostic, context.CancellationToken);

		context.AddSource(filename, source);
	}

	static void Execute(Compilation compilation, ImmutableArray<BaseTypeDeclarationSyntax> enumTypeClasses, SourceProductionContext context)
	{
		if (enumTypeClasses.IsDefaultOrEmpty) return;

		//FileLog.LogAccess();

		var resultTypeSchemata =
			Parser.GetEnumTypes(compilation, enumTypeClasses, context.ReportDiagnostic, context.CancellationToken)
				.ToImmutableArray();

		var generation =
			resultTypeSchemata.Select(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken));

		foreach (var (filename, source) in generation) context.AddSource(filename, source);
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