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
    IReadOnlyList<string> Parameters)
{
    public ConstructType Construct => parameters => this with {Parameters = parameters};

    public static TypeInfo From(INamedTypeSymbol type) => new(
        type.FullTypeNameWithNamespace(),
        true,
        type.TypeArguments.Select(x => x.ToString()).ToList());

    public static implicit operator string(TypeInfo type) => type.ToString();

    public override string ToString() => $"{(IsFullType ? "global::" : string.Empty)}{TypeName}<{string.Join(", ", Parameters)}>";
}

internal delegate TypeInfo ConstructType(IReadOnlyList<string> typeParameters);
