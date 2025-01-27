﻿using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;

namespace FunicularSwitch.Generators.UnionType;

public sealed record UnionTypeSchema(string? Namespace,
	string TypeName,
	string FullTypeName,
	string FullTypeNameWithTypeParameters,
	EquatableArray<DerivedType> Cases,
	EquatableArray<string> TypeParameters,
	bool IsInternal,
	bool IsPartial,
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
    EquatableArray<MemberInfo> ExistingStaticMethods, 
    EquatableArray<string> ExistingStaticFields
);

public sealed record DerivedType
{
	public string FullTypeName { get; }
    public EquatableArray<MemberInfo> Constructors { get; }
    public string ParameterName { get; }
	public string StaticFactoryMethodName { get; }
	
    public DerivedType(string fullTypeName, string parameterName, string staticFactoryMethodName, EquatableArray<MemberInfo>? constructors = null)
    {
        FullTypeName = fullTypeName;
        ParameterName = parameterName;
        StaticFactoryMethodName = staticFactoryMethodName;
        Constructors = constructors ?? [];
    }
}