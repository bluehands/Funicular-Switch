//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError_Derived_FirstCase_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class MyErrorAssertions
    {
        public AndWhichConstraint<MyErrorAssertions, global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_> BeFirstCase(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be MyError.FirstCase_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.MyError.FirstCase_)!);
        }
    }
}