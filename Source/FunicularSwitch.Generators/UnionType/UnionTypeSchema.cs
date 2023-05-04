using FunicularSwitch.Generators.Generation;

namespace FunicularSwitch.Generators.UnionType;

public sealed record UnionTypeSchema(string? Namespace, string TypeName, string FullTypeName, IReadOnlyCollection<DerivedType> Cases, bool IsInternal, bool IsEnum)
{
    public string? Namespace { get; } = Namespace;
    public string FullTypeName { get; } = FullTypeName;
    public string TypeName { get; } = TypeName;
    public IReadOnlyCollection<DerivedType> Cases { get; } = Cases;
    public bool IsInternal { get; } = IsInternal;
    
    public bool IsEnum { get; } = IsEnum;
}

public sealed record DerivedType
{
    public string FullTypeName { get; }
    public string ParameterName { get; }

    public DerivedType(string fullTypeName, string typeName)
    {
        FullTypeName = fullTypeName;
        ParameterName = (typeName.Any(c => c != '_') ? typeName.TrimEnd('_') : typeName).ToParameterName();
    }
}