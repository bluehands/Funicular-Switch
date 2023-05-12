using System.Collections.Immutable;
using System.Diagnostics;
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
                    transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration(ctx, EnumTypeAttribute)
                )
                .Where(static target => target != null)
                .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(enumTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses, 
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(Compilation compilation, ImmutableArray<BaseTypeDeclarationSyntax> enumTypeClasses, SourceProductionContext context)
    {
        if (enumTypeClasses.IsDefaultOrEmpty) return;

        var resultTypeSchemata = 
            Parser.GetEnumTypes(compilation, enumTypeClasses, context.ReportDiagnostic, context.CancellationToken)
                .ToImmutableArray();

        var generation =
            resultTypeSchemata.Select(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken));

        foreach (var (filename, source)  in generation) context.AddSource(filename, source);
    }
}

[Generator]
public class MatchForEnumTypesGenerator : IIncrementalGenerator
{
	internal const string EnumTypeAttribute = "FunicularSwitch.Generators.ExtendEnumTypesAttribute";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		//context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
		//	"Attributes.g.cs",
		//	Templates.EnumTypeTemplates.StaticCode));

		var enumTypeClasses =
			context.SyntaxProvider
				.CreateSyntaxProvider(
					predicate: static (s, _) => s is AttributeSyntax,
					transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration2(ctx, EnumTypeAttribute)
				)
				.Where(static target => target != null)
				.Select(static (target, _) => target!);

		var compilationAndClasses = context.CompilationProvider.Combine(enumTypeClasses.Collect());

		context.RegisterSourceOutput(
			compilationAndClasses, 
			static (spc, source) => Execute(source.Left, source.Right, spc));
	}

	static void Execute(Compilation compilation, ImmutableArray<GeneratorHelper.EnumTypesAttributeInfo> enumTypesAttributes, SourceProductionContext context)
	{
		if (enumTypesAttributes.IsDefaultOrEmpty) return;

		if (!Debugger.IsAttached)
		{
			Debugger.Launch();
		}

		var resultTypeSchemata = 
			Parser.GetEnumTypes(compilation, enumTypesAttributes, context.ReportDiagnostic, context.CancellationToken)
				.ToImmutableArray();

		var generation =
			resultTypeSchemata.Select(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken));

		foreach (var (filename, source)  in generation) context.AddSource(filename, source);
	}
}