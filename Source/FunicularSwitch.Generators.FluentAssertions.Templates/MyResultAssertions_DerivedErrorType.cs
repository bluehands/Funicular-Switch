using FluentAssertions;
using FluentAssertions.Execution;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates;

public partial class MyResultAssertions<T>
{
    public AndWhichConstraint<MyResultAssertions<T>, MyErrorType.MyDerivedErrorType> BeFriendlyDerivedErrorTypeNameResult(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(this.Subject.GetErrorOrDefault() is MyErrorType.MyDerivedErrorType)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context} to be Error with MyDerivedErrorType MyErrorType{reason}, but found {0}",
                this.Subject);

        return new(this, (this.Subject.GetErrorOrDefault() as MyErrorType.MyDerivedErrorType)!);
    }
}