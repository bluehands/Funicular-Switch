//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError_Derived_FirstCase_Assertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class MyErrorAssertions
    {
        public AndWhichConstraint<MyErrorAssertions, FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_> BeFirstCase(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_)!);
        }
    }
}