namespace FunicularSwitch.Generators.Transformer;

internal record TypeInfo(string Name, IReadOnlyList<TypeInfo> TypeParameters)
{
    public override string ToString() =>
        TypeParameters.Count > 0
            ? $"{Name}<{string.Join(", ", TypeParameters)}>"
            : Name;
}