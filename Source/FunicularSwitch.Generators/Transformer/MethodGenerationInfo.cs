using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal record MethodGenerationInfo(
    string ReturnType,
    IReadOnlyList<string> TypeParameters,
    IReadOnlyList<ParameterGenerationInfo> Parameters,
    string Name,
    MethodBody Body,
    bool IsAsync = false)
{
    public class SignatureComparer : IEqualityComparer<MethodGenerationInfo>
    {
        public static SignatureComparer Instance { get; } = new();

        public bool Equals(MethodGenerationInfo? x, MethodGenerationInfo? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name
                   && x.Parameters.Select(x => x.Type).SequenceEqual(y.Parameters.Select(x => x.Type));
        }

        public int GetHashCode(MethodGenerationInfo obj) =>
            obj.Name.GetHashCode();
    }
}

internal record TypeInfo(
    string TypeName,
    bool IsFullType,
    // bool IsParameter,
    IReadOnlyList<TypeInfo> Parameters)
{
    public ConstructType Construct => parameters => this with {Parameters = parameters};

    public static TypeInfo From(INamedTypeSymbol type) => new(
        type.FullTypeNameWithNamespace(),
        true,
        // false,
        type.TypeArguments.Select(From).ToList());

    public static TypeInfo From(ITypeSymbol type) =>
        type switch
        {
            INamedTypeSymbol namedTypeSymbol => From(namedTypeSymbol),
            ITypeParameterSymbol typeParameterSymbol => From(typeParameterSymbol),
            _ => throw new NotImplementedException(),
        };

    public static TypeInfo From(ITypeParameterSymbol type) => Parameter(type.Name);

    public static TypeInfo LocalType(string name,
        IReadOnlyList<TypeInfo> typeParameters) => new(
        name,
        false,
        // false,
        typeParameters);

    // TODO: remove
    public static implicit operator string(TypeInfo type) => type.ToString();

    public static implicit operator TypeInfo(string typeParameter) => Parameter(typeParameter);

    public static TypeInfo Parameter(string name) => new(
        name,
        false,
        // true,
        []);

    public override string ToString() => $"{(IsFullType ? "global::" : string.Empty)}{TypeName}{(Parameters.Count > 0 ? $"<{string.Join(", ", Parameters)}>" : string.Empty)}";
}

internal delegate TypeInfo ConstructType(IReadOnlyList<TypeInfo> typeParameters);
