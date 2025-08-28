//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericResultAssertions.g.cs
#nullable enable
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;
using AwesomeAssertions;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class GenericResultAssertions<T> : ObjectAssertions<global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericResult<T>, GenericResultAssertions<T>>
    {
        public GenericResultAssertions(global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericResult<T> value) : base(value, AssertionChain.GetOrCreate())
        {
        }
        
        public AndWhichConstraint<GenericResultAssertions<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetValueOrThrow());
        }
        
        public AndWhichConstraint<GenericResultAssertions<T>, object> BeError(string because = "", params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}