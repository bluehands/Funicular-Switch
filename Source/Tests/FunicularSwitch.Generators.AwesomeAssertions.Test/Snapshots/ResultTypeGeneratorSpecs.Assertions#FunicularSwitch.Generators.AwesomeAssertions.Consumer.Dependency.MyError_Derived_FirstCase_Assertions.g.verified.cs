//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MyError_Derived_FirstCase_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class MyErrorAssertions
    {
        public AndWhichConstraint<MyErrorAssertions, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MyError.FirstCase_> BeFirstCase(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MyError.FirstCase_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be MyError.FirstCase_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MyError.FirstCase_)!);
        }
    }
}