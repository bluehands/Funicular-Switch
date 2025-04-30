//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeWithConstraintsOfTFirst_TSecond_TThird_TFourth_Derived_DerivedAssertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class GenericUnionTypeWithConstraintsAssertions<TFirst, TSecond, TThird, TFourth>
    {
        public AndWhichConstraint<GenericUnionTypeWithConstraintsAssertions<TFirst, TSecond, TThird, TFourth>, global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>.Derived> BeDerived(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>.Derived)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GenericUnionTypeWithConstraints.Derived{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>.Derived)!);
        }
    }
}