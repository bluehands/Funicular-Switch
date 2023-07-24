//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal static class MyAssertionExtensions_Result
    {
        public static MyAssertions_Result<T> Should<T>(this MyResult<T> result) => new(result);
    }
}