//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeOfT_Derived_First_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class GenericUnionTypeAssertions<T>
    {
        public AndWhichConstraint<GenericUnionTypeAssertions<T>, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionType<T>.First_> BeFirst(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionType<T>.First_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GenericUnionType.First_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionType<T>.First_)!);
        }
    }
}