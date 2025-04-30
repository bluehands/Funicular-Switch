//HintName: FunicularSwitch.ResultAssertions.g.cs
#nullable enable
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;
using FunicularSwitch;

namespace FunicularSwitch
{
    internal partial class ResultAssertions<T> : ObjectAssertions<global::FunicularSwitch.Result<T>, ResultAssertions<T>>
    {
        public ResultAssertions(global::FunicularSwitch.Result<T> value) : base(value)
        {
        }
        
        public AndWhichConstraint<ResultAssertions<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetValueOrThrow());
        }
        
        public AndWhichConstraint<ResultAssertions<T>, System.String> BeError(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}