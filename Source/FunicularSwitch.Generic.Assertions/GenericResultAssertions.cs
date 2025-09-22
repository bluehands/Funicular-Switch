using AwesomeAssertions;
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;

namespace FunicularSwitch.Generic.Assertions;

public class GenericResultAssertions<TOk, TError>(GenericResult<TOk, TError> value, AssertionChain assertionChain)
    : ObjectAssertions<GenericResult<TOk, TError>, GenericResultAssertions<TOk, TError>>(value, assertionChain)
{
    public AndWhichConstraint<GenericResultAssertions<TOk, TError>, TOk> BeOk(
        string because = "",
        params object[] becauseArgs)
    {
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsOk())
            .FailWith("Expected {context} to be ok {reason}, but found an error with error value {0}", Subject.MapError(e => e?.ToString()).GetErrorOrThrow);

        return new AndWhichConstraint<GenericResultAssertions<TOk, TError>, TOk>(this, Subject.GetValueOrThrow());
    }
    
    public AndWhichConstraint<GenericResultAssertions<TOk, TError>, TError> BeError(
        string because = "",
        params object[] becauseArgs)
    {
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.IsError())
            .FailWith("Expected {context} to be an error {reason}, but found ok value {0}", Subject.Map(e => e?.ToString()).GetValueOrThrow);

        return new AndWhichConstraint<GenericResultAssertions<TOk, TError>, TError>(this, Subject.GetErrorOrThrow());
    }
}