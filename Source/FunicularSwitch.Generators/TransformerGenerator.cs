using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Templates;
using FunicularSwitch.Generators.Transformer;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

[Generator]
public class TransformerGenerator : IIncrementalGenerator
{
    private const string TransformMonadAttributeFullname = "FunicularSwitch.Generators.TransformMonadAttribute";
    private const string AttributesFilename = "Attributes.g.cs";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx => ctx.AddSource(
            AttributesFilename,
            TransformerTemplates.StaticCode));

        var transformedMonadTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                TransformMonadAttributeFullname,
                static (_, _) => true,
                static (syntaxContext, token) => Parser.GetTransformedMonadSchema(
                    (INamedTypeSymbol)syntaxContext.TargetSymbol,
                    syntaxContext.Attributes[0],
                    token));
        
        context.RegisterSourceOutput(
            transformedMonadTypes,
            Execute);
    }

    private static void Execute(SourceProductionContext context, TransformMonadData data)
    {
        var (filename, source) = Generator.Emit(data, context.ReportDiagnostic, context.CancellationToken);
        context.AddSource(filename, source);
    }
}