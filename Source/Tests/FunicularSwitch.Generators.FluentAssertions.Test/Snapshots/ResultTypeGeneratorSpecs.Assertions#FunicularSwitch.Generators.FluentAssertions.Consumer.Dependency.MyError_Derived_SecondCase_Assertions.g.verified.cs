//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError_Derived_SecondCase_Assertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class MyErrorAssertions
    {
        public AndWhichConstraint<MyErrorAssertions, global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.SecondCase_> BeSecondCase(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.SecondCase_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be MyError.SecondCase_{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.SecondCase_)!);
        }
    }
}