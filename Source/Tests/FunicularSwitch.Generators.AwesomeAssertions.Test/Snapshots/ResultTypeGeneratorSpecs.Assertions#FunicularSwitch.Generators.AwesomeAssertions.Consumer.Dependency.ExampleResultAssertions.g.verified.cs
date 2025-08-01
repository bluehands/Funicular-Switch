﻿//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExampleResultAssertions.g.cs
#nullable enable
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;
using AwesomeAssertions;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class ExampleResultAssertions<T> : ObjectAssertions<global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExampleResult<T>, ExampleResultAssertions<T>>
    {
        public ExampleResultAssertions(global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExampleResult<T> value) : base(value, AssertionChain.GetOrCreate())
        {
        }
        
        public AndWhichConstraint<ExampleResultAssertions<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetValueOrThrow());
        }
        
        public AndWhichConstraint<ExampleResultAssertions<T>, FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.MyError> BeError(string because = "", params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}