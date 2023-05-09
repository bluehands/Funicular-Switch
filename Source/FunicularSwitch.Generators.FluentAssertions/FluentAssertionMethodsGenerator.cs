using System.Collections.Immutable;
using System.Diagnostics;
using FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.FluentAssertions;

[Generator]
public class FluentAssertionMethodsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(
            ctx => ctx.AddSource("Attributes.g.cs", Templates.GenerateFluentAssertionsForTemplates.StaticCode));

        var generateMethodsAttributes = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => node is AttributeSyntax,
                transform: static (context, _) =>
                    GeneratorHelper.GetSemanticTargetForGeneration(context, GenerateFluentAssertionsForAttribute)
            )
            .Where(static target => target is not null)
            .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(generateMethodsAttributes.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses,
            static (sourceProductionContext, source) => Execute(source.Left, source.Right.OfType<AttributeSyntax>().ToImmutableArray(), sourceProductionContext));
    }

    private static void Execute(
        Compilation compilation,
        ImmutableArray<AttributeSyntax> attributes,
        SourceProductionContext context)
    {
        if (attributes.IsDefaultOrEmpty)
        {
            return;
        }

        var resultTypeSchemata = Parser
            .GetResultTypes(
                compilation,
                attributes,
                context.ReportDiagnostic,
                context.CancellationToken)
            .ToImmutableArray();

        var generated = resultTypeSchemata.SelectMany(
                r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken))
            .ToImmutableArray();

        foreach (var (filename, source) in generated)
        {
            context.AddSource(filename, source);
        }
    }

    internal const string GenerateMethodsForAttributeNamespace = "FunicularSwitch.Generators.FluentAssertions.";
    internal const string GenerateFluentAssertionsForAttribute = "GenerateFluentAssertionsFor";
}