using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal partial class MyAssertions_Result<T> : ObjectAssertions<MyResult<T>, MyAssertions_Result<T>>
    {
        public MyAssertions_Result(MyResult<T> value) : base(value)
        {
        }

        public AndWhichConstraint<MyAssertions_Result<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());

            return new(this, this.Subject.GetValueOrDefault()!);
        }

        public AndWhichConstraint<MyAssertions_Result<T>, MyErrorType> BeError(string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());

            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}