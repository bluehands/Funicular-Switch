using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.FluentAssertions;

[Generator]
public class FluentAssertionMethodsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            const string funicularSwitchFluentAssertionsNamespace = "FunicularSwitch";
    
            var optionAssertionsText = Templates.GenerateFluentAssertionsForTemplates.OptionAssertions.Replace(Generator.TemplateNamespace, funicularSwitchFluentAssertionsNamespace);
            ctx.AddSource($"{funicularSwitchFluentAssertionsNamespace}.OptionAssertions.g.cs", optionAssertionsText);
            var optionAssertionExtensionsText = Templates.GenerateFluentAssertionsForTemplates.OptionAssertionExtensions.Replace(Generator.TemplateNamespace, funicularSwitchFluentAssertionsNamespace);
            ctx.AddSource($"{funicularSwitchFluentAssertionsNamespace}.OptionAssertionExtensions.g.cs", optionAssertionExtensionsText);
            var generateExtensionsForInternalTypeAttributesText = Templates.GenerateFluentAssertionsForTemplates.GenerateExtensionsForInternalTypesAttribute.Replace(Generator.TemplateNamespace, funicularSwitchFluentAssertionsNamespace);
            ctx.AddSource($"{funicularSwitchFluentAssertionsNamespace}.GenerateExtensionsForInternalTypesAttribute.g.cs", generateExtensionsForInternalTypeAttributesText);
        });

        var refAssemblies = context.CompilationProvider
            .SelectMany((c, _) => c.SourceModule.ReferencedAssemblySymbols);

        var generateForInternalTypesAttributes = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => s.IsAssemblyAttribute(),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static target => target is not null)
            .Select(static (target, _) => target!);

        context.RegisterSourceOutput(
            refAssemblies.Combine(generateForInternalTypesAttributes.Collect()),
            static (sourceProductionContext, tuple) => Execute(tuple.Left, tuple.Right.Contains(tuple.Left), sourceProductionContext));
    }

    private static void Execute(
        IAssemblySymbol assembly,
        bool generateForInternalTypes,
        SourceProductionContext context)
    {
        IEnumerable<(string filename, string source)> generated;
        if (assembly.Identity.Name == "FunicularSwitch")
        {
            var resultType = assembly.GetTypeByMetadataName("FunicularSwitch.Result");
            generated = Generator.EmitForResultType(
                new ResultTypeSchema(resultType!, null),
                context.ReportDiagnostic,
                context.CancellationToken);
        }
        else
        {
            var resultTypeSchemata = Parser.GetResultTypes(
                assembly,
                generateForInternalTypes,
                context.ReportDiagnostic,
                context.CancellationToken);
            var unionTypeSchemata = Parser.GetUnionTypes(
                assembly,
                generateForInternalTypes,
                context.ReportDiagnostic,
                context.CancellationToken);
            generated = resultTypeSchemata.SelectMany(
                    r => Generator.EmitForResultType(
                        r,
                        context.ReportDiagnostic,
                        context.CancellationToken))
                .Concat(unionTypeSchemata.SelectMany(u =>
                    Generator.EmitForUnionType(
                        u,
                        context.ReportDiagnostic,
                        context.CancellationToken)));
        }

        foreach (var (filename, source) in generated)
        {
            context.AddSource(filename, source);
        }
    }

    private static IAssemblySymbol? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        if (context.Node is not AttributeSyntax attribute)
        {
            return null;
        }

        var semanticModel = context.SemanticModel;
        var attributeFullName = attribute.GetAttributeFullName(semanticModel);

        if (attributeFullName != "FunicularSwitch.GenerateExtensionsForInternalTypesAttribute")
        {
            return null;
        }

        var typeofExpression = attribute.ArgumentList?.Arguments
            .Select(a => a.Expression)
            .OfType<TypeOfExpressionSyntax>()
            .FirstOrDefault();

        if (typeofExpression is null)
        {
            return null;
        }

        if (semanticModel.GetSymbolInfo(typeofExpression.Type).Symbol is not INamedTypeSymbol typeSymbol)
        {
            return null;
        }

        return typeSymbol.ContainingAssembly;
    }
}