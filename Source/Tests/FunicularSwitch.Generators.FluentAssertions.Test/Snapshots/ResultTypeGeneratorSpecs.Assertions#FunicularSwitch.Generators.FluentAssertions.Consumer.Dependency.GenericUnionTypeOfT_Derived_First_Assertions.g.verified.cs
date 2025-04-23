//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeOfT_Derived_First_Assertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class GenericUnionTypeAssertions<T>
    {
        public AndWhichConstraint<GenericUnionTypeAssertions<T>, global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.First_> BeFirst(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.First_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GenericUnionType.First_{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionType<T>.First_)!);
        }
    }
}