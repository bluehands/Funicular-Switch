//HintName: FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.WrapperClass.NestedUnionType_Derived_OtherDerivedNestedUnionType_Assertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency
{
    internal partial class WrapperClass_NestedUnionTypeAssertions
    {
        public AndWhichConstraint<WrapperClass_NestedUnionTypeAssertions, global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_> BeOtherDerivedNestedUnionType(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency.WrapperClass.NestedUnionType.OtherDerivedNestedUnionType_)!);
        }
    }
}