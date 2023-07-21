//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal static class MyUnionTypeFluentAssertionExtensions
    {
        public static MyUnionTypeAssertions Should(this MyUnionType unionType) => new(unionType);
    }
}