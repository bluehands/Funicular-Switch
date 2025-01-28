using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

public record ResultTypeSchema(
    INamedTypeSymbol ResultType,
    INamedTypeSymbol? ErrorType)
{
    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}";
}