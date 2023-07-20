using FluentAssertions;
using FluentAssertions.Execution;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal partial class MyUnionTypeAssertions
    {
        public AndWhichConstraint<MyUnionTypeAssertions, MyDerivedUnionType> BeFriendlyDerivedUnionTypeName(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is MyDerivedUnionType)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error with MyDerivedErrorType MyErrorType{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as MyDerivedUnionType)!);
        }
    }
}