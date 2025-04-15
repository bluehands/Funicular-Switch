//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MultiGenericUnionTypeOfTFirst_TSecond_TThird_Derived_One_Assertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class MultiGenericUnionTypeAssertions<TFirst, TSecond, TThird>
    {
        public AndWhichConstraint<MultiGenericUnionTypeAssertions<TFirst, TSecond, TThird>, FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.One_> BeOne(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.One_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.One_{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.One_)!);
        }
    }
}