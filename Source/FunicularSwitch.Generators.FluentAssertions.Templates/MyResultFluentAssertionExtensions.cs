//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal static class MyResultFluentAssertionExtensions
    {
        public static MyResultAssertions<T> Should<T>(this MyResult<T> result) => new(result);
    }
}