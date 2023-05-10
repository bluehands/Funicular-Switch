using FunicularSwitch.Generators.Generation;

namespace FunicularSwitch.Generators.EnumType;

public sealed record EnumTypeSchema(string? Namespace, string TypeName, string FullTypeName, IReadOnlyCollection<DerivedType> Cases, bool IsInternal)
{
    public string? Namespace { get; } = Namespace;
    public string FullTypeName { get; } = FullTypeName;
    public string TypeName { get; } = TypeName;
    public IReadOnlyCollection<DerivedType> Cases { get; } = Cases;
    public bool IsInternal { get; } = IsInternal;
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