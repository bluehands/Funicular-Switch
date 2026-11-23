//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ReorderedGenericUnionOfTFirst_TSecond_Derived_ReorderedValueAssertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class ReorderedGenericUnionAssertions<TFirst, TSecond>
    {
        public AndWhichConstraint<ReorderedGenericUnionAssertions<TFirst, TSecond>, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ReorderedValue<TSecond, TFirst>> BeReorderedValue(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ReorderedValue<TSecond, TFirst>)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be ReorderedValue{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ReorderedValue<TSecond, TFirst>)!);
        }
    }
}
