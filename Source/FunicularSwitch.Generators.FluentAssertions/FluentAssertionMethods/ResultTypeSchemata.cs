using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

public class ResultTypeSchema
{
    public INamedTypeSymbol ResultType { get; }
    public INamedTypeSymbol? ErrorType { get; }
    public IList<INamedTypeSymbol> ErrorTypeDerivedTypes { get; }

    public ResultTypeSchema(INamedTypeSymbol resultType, INamedTypeSymbol? errorType, IEnumerable<INamedTypeSymbol> errorTypeDerivedTypes)
    {
        ResultType = resultType;
        ErrorType = errorType;
        ErrorTypeDerivedTypes = errorTypeDerivedTypes.ToList();
    }

    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}";
}