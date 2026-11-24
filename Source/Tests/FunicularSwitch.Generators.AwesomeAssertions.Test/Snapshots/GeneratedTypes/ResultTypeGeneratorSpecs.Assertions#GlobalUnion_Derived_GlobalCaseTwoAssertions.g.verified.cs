//HintName: GlobalUnion_Derived_GlobalCaseTwoAssertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;



    internal partial class GlobalUnionAssertions
    {
        public AndWhichConstraint<GlobalUnionAssertions, global::GlobalCaseTwo> BeGlobalCaseTwo(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::GlobalCaseTwo)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GlobalCaseTwo{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::GlobalCaseTwo)!);
        }
    }
