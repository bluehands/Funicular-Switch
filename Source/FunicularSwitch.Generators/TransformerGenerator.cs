using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Templates;
using FunicularSwitch.Generators.Transformer;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

[Generator]
public class TransformerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(TransformMonadAttribute.AddTo);

        var transformedMonadTypes = TransformMonadAttribute.Find(
            context.SyntaxProvider,
            static (_, _) => true,
            Parse);
        
        context.RegisterSourceOutput(
            transformedMonadTypes,
            Execute);
    }

    private static GenerationResult<TransformMonadData> Parse(GeneratorAttributeSyntaxContext syntaxContext, IReadOnlyList<TransformMonadAttribute> attributes, CancellationToken token) =>
        Parser.GetTransformedMonadSchema(
            (INamedTypeSymbol) syntaxContext.TargetSymbol,
            attributes.First(),
            token);
    
    private static void Execute(SourceProductionContext context, GenerationResult<TransformMonadData> target)
    {
        var (data, errors, hasValue) = target;
        foreach (var error in errors) context.ReportDiagnostic(error);
        
        if (!hasValue) return;
        
        var (filename, source) = Generator.Emit(data!, context.ReportDiagnostic, context.CancellationToken);
        context.AddSource(filename, source);
    }
}