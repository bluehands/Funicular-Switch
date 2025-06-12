//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MultiGenericUnionTypeOfTFirst_TSecond_TThird_Derived_Two_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class MultiGenericUnionTypeAssertions<TFirst, TSecond, TThird>
    {
        public AndWhichConstraint<MultiGenericUnionTypeAssertions<TFirst, TSecond, TThird>, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.Two_> BeTwo(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.Two_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be MultiGenericUnionType.Two_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MultiGenericUnionType<TFirst, TSecond, TThird>.Two_)!);
        }
    }
}