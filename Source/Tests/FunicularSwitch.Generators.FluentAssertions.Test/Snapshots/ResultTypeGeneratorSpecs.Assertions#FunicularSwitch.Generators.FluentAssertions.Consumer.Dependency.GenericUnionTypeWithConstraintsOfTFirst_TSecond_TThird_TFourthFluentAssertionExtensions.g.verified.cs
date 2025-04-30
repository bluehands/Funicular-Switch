//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeWithConstraintsOfTFirst_TSecond_TThird_TFourthFluentAssertionExtensions.g.cs
#nullable enable
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal static class GenericUnionTypeWithConstraintsFluentAssertionExtensions
    {
        public static GenericUnionTypeWithConstraintsAssertions<TFirst, TSecond, TThird, TFourth> Should<TFirst, TSecond, TThird, TFourth>(this global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth> unionType) where TFirst : struct, global::System.Collections.Generic.IEnumerable<int> where TSecond : class, new() where TThird : global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass where TFourth : notnull => new(unionType);
    }
}