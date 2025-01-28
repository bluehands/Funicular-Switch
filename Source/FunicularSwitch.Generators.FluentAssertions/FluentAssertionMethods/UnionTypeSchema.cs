using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

public record UnionTypeSchema(
    INamedTypeSymbol UnionTypeBaseType,
    IEnumerable<INamedTypeSymbol> DerivedTypes)
{
    public override string ToString()
    {
        var derivedTypes = string.Join(", ", DerivedTypes.Select(d => d.ToString()));
        return $"{nameof(UnionTypeBaseType)}: {UnionTypeBaseType}, {nameof(DerivedTypes)}, {derivedTypes}";
    }
}