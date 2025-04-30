//HintName: FunicularSwitch.OptionAssertions.g.cs
#nullable enable
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;
using System;

namespace FunicularSwitch;

internal class OptionAssertions<T> : ObjectAssertions<FunicularSwitch.Option<T>, OptionAssertions<T>>
{
    public OptionAssertions(FunicularSwitch.Option<T> value) : base(value)
    {
    }

    public AndWhichConstraint<OptionAssertions<T>, T> BeSome(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(this.Subject.IsSome())
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {content} to be Some{reason}, but found {0}", this.Subject.ToString());

        return new(this, this.Subject.GetValueOrDefault()!);
    }

    public AndConstraint<OptionAssertions<T>> BeNone(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(this.Subject.IsNone())
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {content} to be None{reason}, but found {0}", this.Subject.ToString());

        return new(this);
    }
}