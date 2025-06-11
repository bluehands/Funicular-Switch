//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeWithConstraintsOfTFirst_TSecond_TThird_TFourth_Derived_DerivedAssertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class GenericUnionTypeWithConstraintsAssertions<TFirst, TSecond, TThird, TFourth>
    {
        public AndWhichConstraint<GenericUnionTypeWithConstraintsAssertions<TFirst, TSecond, TThird, TFourth>, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>.Derived> BeDerived(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>.Derived)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GenericUnionTypeWithConstraints.Derived{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>.Derived)!);
        }
    }
}