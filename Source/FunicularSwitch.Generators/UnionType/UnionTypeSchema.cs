using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.UnionType;

public sealed record UnionTypeSchema(string? Namespace,
	string TypeName,
	string FullTypeName,
	IReadOnlyCollection<DerivedType> Cases,
	bool IsInternal,
	bool IsPartial,
	UnionTypeTypeKind TypeKind,
	StaticFactoryMethodsInfo? StaticFactoryInfo);

public enum UnionTypeTypeKind
{
	Class,
	Record,
	Interface
}

public record StaticFactoryMethodsInfo(
	IReadOnlyCollection<MemberInfo> ExistingStaticMethods, 
	IReadOnlyCollection<string> ExistingStaticFields,
	SyntaxTokenList Modifiers
);

public sealed record DerivedType
{
	public string FullTypeName { get; }
    public IReadOnlyCollection<MemberInfo> Constructors { get; }
    public string ParameterName { get; }
	public string StaticFactoryMethodName { get; }
	
    public DerivedType(string fullTypeName, string parameterName, string staticFactoryMethodName, IReadOnlyCollection<MemberInfo>? constructors = null)
    {
        FullTypeName = fullTypeName;
        ParameterName = parameterName;
        StaticFactoryMethodName = staticFactoryMethodName;
        Constructors = constructors ?? [];
    }
}