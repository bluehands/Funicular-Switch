using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.AwesomeAssertions.AssertionMethods;

public record ResultTypeSchema(
    INamedTypeSymbol ResultType,
    INamedTypeSymbol? ErrorType)
{
    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}";
}