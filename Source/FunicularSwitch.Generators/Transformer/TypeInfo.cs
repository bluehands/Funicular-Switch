using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal record TypeInfo(
    string TypeName,
    bool IsFullType,
    bool IsParameter,
    EquatableArray<TypeInfo> Parameters)
{
    public ConstructType Construct => parameters => this with {Parameters = parameters.ToImmutableArray()};

    public static TypeInfo From(INamedTypeSymbol type) => new(
        type.FullTypeNameWithNamespace(),
        true,
        false,
        type.TypeArguments.Select(From).ToImmutableArray());

    public static TypeInfo From(ITypeSymbol type) =>
        type switch
        {
            INamedTypeSymbol namedTypeSymbol => From(namedTypeSymbol),
            ITypeParameterSymbol typeParameterSymbol => From(typeParameterSymbol),
            _ => throw new NotImplementedException(),
        };

    public static TypeInfo From(ITypeParameterSymbol type) => Parameter(type.Name);

    public static TypeInfo FullType(string name, IReadOnlyList<TypeInfo> typeParameters) => new(
        name,
        true,
        false,
        typeParameters.ToImmutableArray());

    public static TypeInfo LocalType(string name,
        IReadOnlyList<TypeInfo> typeParameters) => new(
        name,
        false,
        false,
        typeParameters.ToImmutableArray());

    public static implicit operator TypeInfo(string typeParameter) => Parameter(typeParameter);

    public static TypeInfo Parameter(string name) => new(
        name,
        false,
        true,
        ImmutableArray<TypeInfo>.Empty);

    public override string ToString() => $"{(IsFullType ? "global::" : string.Empty)}{TypeName}{(Parameters.Length > 0 ? $"<{string.Join(", ", Parameters)}>" : string.Empty)}";
}
