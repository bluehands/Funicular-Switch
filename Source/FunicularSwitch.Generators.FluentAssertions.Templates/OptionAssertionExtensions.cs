namespace FunicularSwitch.Generators.FluentAssertions.Templates;

internal static class OptionAssertionExtensions
{
    public static OptionAssertions<T> Should<T>(this FunicularSwitch.Option<T> option) => new(option);
}