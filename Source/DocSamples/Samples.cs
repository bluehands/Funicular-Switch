using System;
using System.Threading.Tasks;
using FunicularSwitch;

namespace DocSamples
{
    class Samples
    {
        [Runner("result-creation")]
        public static void ResultCreation()
        {
            static void Ask(int underQuestion) => Console.WriteLine($"Is it {underQuestion}? {new Samples().AssertItIsTheAnswerToEverything(underQuestion)}");

            Ask(23);
            Ask(42);
        }

        #region result-creation
        // Synchronous:
        public Result<int> AssertItIsTheAnswerToEverything(int answer)
            => answer == 42
                ? Result.Ok(answer) //There is an implicit cast to Ok, so you could omit Result.Ok and just write: return answer;
                : Result.Error<int>($"Naaaaaaaaaaaaaaaaaaaaaaaaah, {answer} is not THE answer!");

        // Asynchronous:
        public async Task<Result<int>> AsyncAssertItIsTheAnswerToEverything(int answer)
            => answer == await ComputeAnswer()
                    ? Result.Ok(answer)
                    : Result.Error<int>($"Nah, {answer} is not THE answer!");
        #endregion

        static Task<int> ComputeAnswer() => Task.FromResult(42);

        [Runner("match-simple")]
        public static void MatchSimpleRunner()
        {
            new Samples().MatchSimple();
        }

        public void MatchSimple()
        {
            #region match-simple
            Result<int> theAnswer = AssertItIsTheAnswerToEverything(42);

            theAnswer.Match(
                ok => Console.WriteLine($"{ok} no more words needed!"),
                error => Console.WriteLine(error)
            );
            #endregion
        }

        [Runner("match-simple-async")]
        public static void MatchSimpleAsyncRunner()
        {
            new Samples().MatchSimple();
        }

        public async Task MatchSimpleAsync()
        {
            #region match-simple-async
            var answerOutput = 
                AsyncAssertItIsTheAnswerToEverything(42)
                .Match(
                    ok => $"{ok} no more words needed!",
                    error => error
                );

            Console.WriteLine(await answerOutput);
            #endregion
        }
    }
}