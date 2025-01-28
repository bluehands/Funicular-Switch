//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal static class MyAssertionExtensions_UnionType
    {
        public static MyAssertions_UnionType Should<TTypeParameters>(this MyUnionType unionType) => new(unionType);
    }
}