//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeOfT_Derived_Second_Assertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class GenericUnionTypeAssertions<T>
    {
        public AndWhichConstraint<GenericUnionTypeAssertions<T>, FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.Second_> BeSecond(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.Second_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.Second_{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.Second_)!);
        }
    }
}