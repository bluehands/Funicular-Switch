using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

public class ResultTypeSchema
{
    public INamedTypeSymbol ResultType { get; }
    public INamedTypeSymbol? ErrorType { get; }

    public ResultTypeSchema(INamedTypeSymbol resultType, INamedTypeSymbol? errorType)
    {
        ResultType = resultType;
        ErrorType = errorType;
    }

    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}";
}