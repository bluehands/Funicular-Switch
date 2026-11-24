//HintName: GlobalNamespaceTypesConsumer.ResultWithGlobalErrorAssertions.g.cs
#nullable enable
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;
using AwesomeAssertions;
using GlobalNamespaceTypesConsumer;

namespace GlobalNamespaceTypesConsumer
{
    internal partial class ResultWithGlobalErrorAssertions<T> : ObjectAssertions<global::GlobalNamespaceTypesConsumer.ResultWithGlobalError<T>, ResultWithGlobalErrorAssertions<T>>
    {
        public ResultWithGlobalErrorAssertions(global::GlobalNamespaceTypesConsumer.ResultWithGlobalError<T> value) : base(value, AssertionChain.GetOrCreate())
        {
        }
        
        public AndWhichConstraint<ResultWithGlobalErrorAssertions<T>, T> BeOk(string because = "", params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::GlobalNamespaceTypesConsumer.ResultWithGlobalError<T>.Ok_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetValueOrThrow());
        }
        
        public AndWhichConstraint<ResultWithGlobalErrorAssertions<T>, GlobalError> BeError(string because = "", params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::GlobalNamespaceTypesConsumer.ResultWithGlobalError<T>.Error_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());
                
            return new(this, this.Subject.GetErrorOrDefault()!);
        }
    }
}