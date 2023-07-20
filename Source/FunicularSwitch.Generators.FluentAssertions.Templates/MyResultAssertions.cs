﻿using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal partial class MyResultAssertions<T> : ObjectAssertions<MyResult<T>, MyResultAssertions<T>>
    {
        public MyResultAssertions(MyResult<T> value) : base(value)
        {
        }

        public AndWhichConstraint<MyResultAssertions<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, bout found {0}", this.Subject);

            return new(this, this.Subject.GetValueOrDefault()!);
        }

        public AndWhichConstraint<MyResultAssertions<T>, MyErrorType> BeError(string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject);

            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}