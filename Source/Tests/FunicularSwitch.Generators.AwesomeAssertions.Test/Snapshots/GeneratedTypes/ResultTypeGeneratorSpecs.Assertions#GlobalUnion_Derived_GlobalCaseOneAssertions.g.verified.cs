//HintName: GlobalUnion_Derived_GlobalCaseOneAssertions.g.cs
#nullable enable
using AwesomeAssertions;
using AwesomeAssertions.Execution;



    internal partial class GlobalUnionAssertions
    {
        public AndWhichConstraint<GlobalUnionAssertions, global::GlobalCaseOne> BeGlobalCaseOne(
            string because = "",
            params object[] becauseArgs)
        {
            CurrentAssertionChain
                .ForCondition(this.Subject is global::GlobalCaseOne)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to be GlobalCaseOne{reason}, but found {0}",
                    this.Subject.ToString());

            return new(this, (this.Subject as global::GlobalCaseOne)!);
        }
    }
