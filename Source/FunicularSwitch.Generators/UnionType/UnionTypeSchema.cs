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
	bool IsEnum,
	bool IsPartial,
	bool IsRecord,
	StaticFactoryMethodsInfo? StaticFactoryInfo);

public record StaticFactoryMethodsInfo(
	IReadOnlyCollection<MemberInfo> ExistingStaticMethods, 
	IReadOnlyCollection<string> ExistingStaticFields,
	SyntaxTokenList Modifiers
);

public sealed record DerivedType
{
	public string FullTypeName { get; }
    public string ParameterName { get; }

	public IReadOnlyCollection<MemberInfo> Constructors { get; }

    public DerivedType(string fullTypeName, string typeName, IReadOnlyCollection<MemberInfo>? constructors = null)
    {
        FullTypeName = fullTypeName;
        Constructors = constructors ?? ImmutableList<MemberInfo>.Empty;
        ParameterName = (typeName.Any(c => c != '_') ? typeName.TrimEnd('_') : typeName).ToParameterName();
    }
}