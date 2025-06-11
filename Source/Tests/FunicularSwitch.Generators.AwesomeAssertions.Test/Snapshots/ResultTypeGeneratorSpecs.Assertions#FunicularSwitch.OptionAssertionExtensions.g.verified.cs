//HintName: FunicularSwitch.OptionAssertionExtensions.g.cs
#nullable enable
namespace FunicularSwitch;

internal static class OptionAssertionExtensions
{
    public static OptionAssertions<T> Should<T>(this FunicularSwitch.Option<T> option) => new(option);
}