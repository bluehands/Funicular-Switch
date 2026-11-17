using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;

namespace FunicularSwitch.Generators.UnionType;

public sealed record UnionTypeSchema(string? Namespace,
    string TypeName,
    string FullTypeName,
    string FullTypeNameWithTypeParameters,
    EquatableArray<DerivedType> Cases,
    EquatableArray<string> TypeParameters,
    EquatableArray<string> TypeConstraints,
    bool IsInternal,
    bool IsPartial,
    bool IsExhaustive,
    bool IsNativeUnion,
    UnionTypeTypeKind TypeKind,
    EquatableArray<string> Modifiers,
    StaticFactoryMethodsInfo? StaticFactoryInfo);

public enum UnionTypeTypeKind
{
    Class,
    Record,
    Interface
}

public record StaticFactoryMethodsInfo(
    EquatableArray<CallableMemberInfo> ExistingStaticMethods,
    EquatableArray<string> ExistingStaticFields
);

public sealed record DerivedType
{
    public string FullTypeName { get; }
    public EquatableArray<CallableMemberInfo> Constructors { get; }
    public EquatableArray<PropertyOrFieldInfo> RequiredMembers { get; }
    public string ParameterName { get; }
    public string StaticFactoryMethodName { get; }
    /// <summary>Type used as handler parameter type. Differs from <see cref="FullTypeName"/> (the pattern type) for nullable cases of native unions.</summary>
    public string HandlerTypeName { get; }
    /// <summary>Native unions only: case type admits null (<c>string?</c>, <c>int?</c>).</summary>
    public bool IsNullableCase { get; }
    /// <summary>Native unions only: more specific cases have to be matched before the cases they derive from.</summary>
    public int ArmRank { get; }

    public DerivedType(string fullTypeName, string parameterName, string staticFactoryMethodName, EquatableArray<CallableMemberInfo>? constructors = null, EquatableArray<PropertyOrFieldInfo>? requiredMembers = null, string? handlerTypeName = null, bool isNullableCase = false, int armRank = 0)
    {
        HandlerTypeName = handlerTypeName ?? fullTypeName;
        IsNullableCase = isNullableCase;
        ArmRank = armRank;
        FullTypeName = fullTypeName;
        ParameterName = parameterName;
        StaticFactoryMethodName = staticFactoryMethodName;
        RequiredMembers = requiredMembers ?? [];
        Constructors = constructors ?? [];
    }
}