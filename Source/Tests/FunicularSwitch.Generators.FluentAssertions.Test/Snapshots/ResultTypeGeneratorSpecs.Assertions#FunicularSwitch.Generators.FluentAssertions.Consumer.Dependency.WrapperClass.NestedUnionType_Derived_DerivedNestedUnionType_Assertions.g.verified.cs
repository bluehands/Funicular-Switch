//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType_Derived_DerivedNestedUnionType_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class WrapperClass_NestedUnionTypeAssertions
    {
        public AndWhichConstraint<WrapperClass_NestedUnionTypeAssertions, global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.DerivedNestedUnionType_> BeDerivedNestedUnionType(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.DerivedNestedUnionType_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be WrapperClass.NestedUnionType.DerivedNestedUnionType_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.DerivedNestedUnionType_)!);
        }
    }
}