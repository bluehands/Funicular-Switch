//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionTypeOfT_Derived_Second_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class GenericUnionTypeAssertions<T>
    {
        public AndWhichConstraint<GenericUnionTypeAssertions<T>, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionType<T>.Second_> BeSecond(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionType<T>.Second_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GenericUnionType.Second_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.GenericUnionType<T>.Second_)!);
        }
    }
}