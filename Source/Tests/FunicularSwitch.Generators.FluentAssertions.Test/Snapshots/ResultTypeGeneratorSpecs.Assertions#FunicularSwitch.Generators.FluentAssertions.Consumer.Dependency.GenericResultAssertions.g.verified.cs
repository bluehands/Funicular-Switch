//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericResultAssertions.g.cs
#nullable enable
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class GenericResultAssertions<T> : ObjectAssertions<global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericResult<T>, GenericResultAssertions<T>>
    {
        public GenericResultAssertions(global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.GenericResult<T> value) : base(value)
        {
        }
        
        public AndWhichConstraint<GenericResultAssertions<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetValueOrThrow());
        }
        
        public AndWhichConstraint<GenericResultAssertions<T>, object> BeError(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}