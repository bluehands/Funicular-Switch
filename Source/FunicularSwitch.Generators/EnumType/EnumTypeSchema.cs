using FunicularSwitch.Generators.Generation;

namespace FunicularSwitch.Generators.EnumType;

public sealed record EnumTypeSchema(string? Namespace, string TypeName, string FullTypeName, IReadOnlyCollection<DerivedType> Cases, bool IsInternal)
{
    public string? Namespace { get; } = Namespace;
    public string FullTypeName { get; } = FullTypeName;
    public string TypeName { get; } = TypeName;
    public IReadOnlyCollection<DerivedType> Cases { get; } = Cases;
    public bool IsInternal { get; } = IsInternal;

    public bool Equals(EnumTypeSchema? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return Namespace == other.Namespace && FullTypeName == other.FullTypeName && TypeName == other.TypeName && IsInternal == other.IsInternal && Cases.SequenceEqual(other.Cases);
    }

    public override int GetHashCode()
    {
	    unchecked
	    {
		    var hashCode = (Namespace != null ? Namespace.GetHashCode() : 0);
		    hashCode = (hashCode * 397) ^ FullTypeName.GetHashCode();
		    hashCode = (hashCode * 397) ^ TypeName.GetHashCode();
		    hashCode = (hashCode * 397) ^ Cases.GetHashCodeByItems();
		    hashCode = (hashCode * 397) ^ IsInternal.GetHashCode();
		    return hashCode;
	    }
    }
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

internal static class EnumerableExtension
{
	internal static int GetHashCodeByItems<T>(this IEnumerable<T> lst)
	{
		unchecked
		{
			int hash = 19;
			foreach (var item in lst)
			{
				hash = hash * 31 + (item != null ? item.GetHashCode() : 1);
			}
			return hash;
		}
	}
}