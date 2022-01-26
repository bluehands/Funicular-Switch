using FunicularSwitch.Generators.Generation;

namespace FunicularSwitch.Generators.UnionType;

public sealed record UnionTypeSchema(string Namespace, string TypeName, IReadOnlyCollection<DerivedType> Cases)
{
    public string Namespace { get; } = Namespace;
    public string TypeName { get; } = TypeName;
    IReadOnlyCollection<DerivedType> Cases { get; } = Cases;

    public IEnumerable<DerivedType> OrderedCases() => Cases.OrderBy(c => c.FullTypeName);
}

public sealed record DerivedType
{
    public string FullTypeName { get; }
    public string ParameterName { get; }

    public DerivedType(string fullTypeName, string typeName)
    {
        FullTypeName = fullTypeName;
        ParameterName = typeName.ToParameterName().TrimEnd('_');
    }
}