//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeWithConstraintsOfTFirst_TSecond_TThird_TFourthAwesomeAssertionExtensions.g.cs
#nullable enable
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal static class GenericUnionTypeWithConstraintsAwesomeAssertionExtensions
    {
        public static GenericUnionTypeWithConstraintsAssertions<TFirst, TSecond, TThird, TFourth> Should<TFirst, TSecond, TThird, TFourth>(this global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth> unionType) where TFirst : struct, global::System.Collections.Generic.IEnumerable<int> where TSecond : class, new() where TThird : global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.WrapperClass where TFourth : notnull => new(unionType);
    }
}