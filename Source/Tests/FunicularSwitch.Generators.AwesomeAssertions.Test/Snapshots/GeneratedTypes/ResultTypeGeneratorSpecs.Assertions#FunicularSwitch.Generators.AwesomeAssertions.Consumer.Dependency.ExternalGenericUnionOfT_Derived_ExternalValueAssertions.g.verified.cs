//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExternalGenericUnionOfT_Derived_ExternalValueAssertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class ExternalGenericUnionAssertions<T>
    {
        public AndWhichConstraint<ExternalGenericUnionAssertions<T>, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExternalValue<T>> BeExternalValue(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExternalValue<T>)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be ExternalValue{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.ExternalValue<T>)!);
        }
    }
}
