//HintName: FunicularSwitch.ResultFluentAssertionExtensions.g.cs
#nullable enable
using FunicularSwitch;

namespace FunicularSwitch
{
    internal static class ResultFluentAssertionExtensions
    {
        public static ResultAssertions<T> Should<T>(this FunicularSwitch.Result<T> result) => new(result);
    }
}