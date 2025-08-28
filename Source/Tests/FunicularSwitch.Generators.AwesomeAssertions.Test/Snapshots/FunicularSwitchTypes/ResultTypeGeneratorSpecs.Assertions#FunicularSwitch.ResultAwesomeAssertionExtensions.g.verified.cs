//HintName: FunicularSwitch.ResultAwesomeAssertionExtensions.g.cs
#nullable enable
using FunicularSwitch;

namespace FunicularSwitch
{
    internal static class ResultAwesomeAssertionExtensions
    {
        public static ResultAssertions<T> Should<T>(this global::FunicularSwitch.Result<T> result) => new(result);
    }
}