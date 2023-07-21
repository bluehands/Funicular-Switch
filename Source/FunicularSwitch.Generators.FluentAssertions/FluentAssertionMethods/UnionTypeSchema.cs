using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

public class UnionTypeSchema
{
    public UnionTypeSchema(INamedTypeSymbol unionTypeBaseType, IEnumerable<INamedTypeSymbol> derivedTypes)
    {
        UnionTypeBaseType = unionTypeBaseType;
        DerivedTypes = derivedTypes.ToList();
    }

    public INamedTypeSymbol UnionTypeBaseType { get; }
    public IReadOnlyList<INamedTypeSymbol> DerivedTypes { get; }
}