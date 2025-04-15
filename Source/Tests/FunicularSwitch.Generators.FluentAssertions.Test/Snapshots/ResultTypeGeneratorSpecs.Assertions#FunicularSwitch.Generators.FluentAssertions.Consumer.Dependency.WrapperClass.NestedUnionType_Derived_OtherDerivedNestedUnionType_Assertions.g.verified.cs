//HintName: FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType_Derived_OtherDerivedNestedUnionType_Assertions.g.cs
#nullable enable
using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency
{
    internal partial class WrapperClass_NestedUnionTypeAssertions
    {
        public AndWhichConstraint<WrapperClass_NestedUnionTypeAssertions, FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_> BeOtherDerivedNestedUnionType(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(this.Subject is FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_{reason}, but found {0}",
                    this.Subject);

            return new(this, (this.Subject as FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_)!);
        }
    }
}