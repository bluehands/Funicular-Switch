using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Parser
{
    public static GenerationResult<TransformMonadData> GetTransformedMonadSchema(
        INamedTypeSymbol transformedMonadSymbol,
        AttributeData transformMonadAttribute,
        CancellationToken cancellationToken)
    {
        return new TransformMonadData(
            transformedMonadSymbol,
            (INamedTypeSymbol)transformMonadAttribute.ConstructorArguments[0].Value!,
            (INamedTypeSymbol)transformMonadAttribute.ConstructorArguments[1].Value!);
    }
}