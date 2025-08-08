using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

public record TransformMonadData(
    INamedTypeSymbol TargetTypeSymbol,
    INamedTypeSymbol MonadType,
    INamedTypeSymbol TransformerType);