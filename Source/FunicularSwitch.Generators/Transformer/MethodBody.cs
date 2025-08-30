namespace FunicularSwitch.Generators.Transformer;

internal readonly record struct MethodBody(string Value)
{
    public static implicit operator MethodBody(string value) => new(value);

    public override string ToString() => Value;
}