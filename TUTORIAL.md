# Bluehands.FunicularSwitch Tutorial

In this tutorial we describe the basic usage of Funicular-Switch step by step.

[Source](https://github.com/bluehands/Funicular-Switch/tree/master/Source/Tutorial)

```csharp
using System.Threading.Tasks;

namespace Tutorial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FunicularSwitch;
    public class Main
    {
        // This es a small tutorial to show the advantage of using FunicularSwitch
        
        private Dictionary<int, string> usersDictionary;

        public async void Tutorial()
        {
            // First, we are generating some lists and dictionaries to work later on.
            usersDictionary = new Dictionary<int, string>
            {
                {1, "UserOne"},
                {2, "UserTwo"},
                {3, "UserThree"},
                {4, "UserFour"},
                {5, "UserFive"},
                {6, "UserSix"},
                {7, "UserSeven"},
                {8, "UserEight"},
                {9, "UserNine"},
                {10, "UserTen"},
                {11, "UserEleven"},
            };

            var aListWithNumbers = new List<string>()
            {
                "1",
                "2",
                "3",
                "11"
            };


            // Okay lets start!
            // We are now creating our first FunicularSwitch result!

            Result<string> ourFirstResult = Result.Ok("Hello World!");

            // as you can see our Result is generic typed to string,
            // which represents the type of the value our result is holding.
            // In our case "Hello World!"

            // Now we will create another result but now we set it to error:

            Result<string> ourSecondResult = Result.Error<string>("Error!");

            // You can see that we had to define the type explicit.

            // Now we come to our first fundamental operation, matching:
            // In case our result is ok we are now printing the value, our result is holding.
            // Otherwise, we are going to print the error message, our result is holding.

            ourFirstResult.Match(ok =>
            {
                Console.WriteLine(ok);
            }, error =>
            {
                Console.WriteLine(error);
            });

            ourSecondResult.Match(ok =>
            {
                Console.WriteLine(ok);
            }, error =>
            {
                Console.WriteLine(error);
            });

            // Well that is it our first usage of matching results.


            // Digging a little deeper:

            // We create a function that parses a number from a string and retrieves a result
            // that represents the status of the parsing operation.

            Result<int> parsedNumber = ParseInt("3");

            // So what we did was: we wrapped both cases, the failure and the success into our result.
            // We can now match it and print it to the console,
            // but we are not going to do that because i have already showed you how that works, above.

            // Now think about a unreliable data source such as a http request.
            // We could request and check if it was successful inside an if condition,
            // where we put the hole "success" logic inside the success block
            // and some other logic to our "fail" block of the condition, but then we might end in a huge tree
            // of nested conditions that are hard to cover in tests or even difficult to understand.

            // We want to get some data from our unreliable source now and parse the data with our function before:
            // To achieve this, we are using our second fundamental operation the "binding" operation:

            Result<string> dataResult = GetDataFromUnreliableSource("3");

            Result<int> parsedIntFromDataResult = dataResult.Bind(data => ParseInt(data));

            // result.Bind(lambda => ...) checks if a given result is ok and executes the lambda if so.
            // It also changes the value type of the value, the result is holding if it is required.
            // In our case it changes from string to int.
            // If the given result "dataResult" is not ok, bind will still change the value type
            // but will not execute the lambda. Instead we will give the error to the next node,
            // until someone will evaluate it e.g. inside a match block, as showed above.

            // Now we are only parsing an int if GetDataFromUnreliableSource() returned ok
            // and we can only access the int if parsing was successful.
            // Otherwise we can access the error message of our "parsedIntFromDataResult",
            // which could either be "Input was no valid int!" or "Something went wrong!".

            // We are coming to our third fundamental operation now, "mapping":

            // Now somehow we want to transform our "parsedInt" from "parsedIntFromDataResult" back to string again:

            Result<string> stringFromDataResult = parsedIntFromDataResult.Map(theParsedInt => theParsedInt.ToString());

            // Well it is not the best example but it visualizes the usage of mapping quite well

            // result.Map(lambda => ...), again, checks if a given result is ok and executes the lambda if so.
            // But inside the lambda we do not return a result,
            // as showed before in the "binding" example, instead "ToString()" returns a string
            // and we just map this string to new result.
            // This means inside the lambda we perform an action that is suppose not to fail.
            // We could also say that inside the lambda, we are doing something
            // that will not change the state of our result chain (e.g. from ok to error).

            // Coming to the fourth fundamental operation, "aggregation":

            // We want to get a list of strings that represent a number from an unreliable data source now,
            // parse the string and use the int to get a user from a dictionary:

            Result<List<string>> stringListDataFromUnreliableSourceResult = GetDataFromUnreliableSource(aListWithNumbers);
            Result<List<int>> parsedIntsResult = stringListDataFromUnreliableSourceResult.Bind(stringList =>
            {
                IEnumerable<Result<int>> intResults = stringList.Select(stringNumber => ParseInt(stringNumber));
                return intResults.Aggregate();
                // results.Aggregate() will take all results and combine them to one, which will be ok if all aggregated results itself were ok.
                // Otherwise results.Aggregate() will combine all the errors from ParseInt() and set the state to error.
            });

            Result<List<string>> usersResult = parsedIntsResult.Bind(parsedInts =>
            {
                var useResults = parsedInts.Select(parsedInt => GetUserNameById(parsedInt));
                return useResults.Aggregate();
                // Again we aggregate all results.
            });

            // Finally we are matching our users and print them to the console if all nodes of our chain were ok.
            // Otherwise we print the first error or errors that lead to a failure:
            usersResult.Match(users =>
            {
                users.ForEach(user => Console.WriteLine(user));
            }, errors =>
            {
                Console.WriteLine(errors);
            });

            // You might have recognized that the assignments of the results above are redundant.
            // We can also write the whole code like this, which is technically one line of code:
            // You can clearly see that there is no if nesting at all.
            // Even though we are using a lot of conditional operations, inside the functions below.

            GetDataFromUnreliableSource(aListWithNumbers)
                .Bind(stringList => stringList.Select(stringNumber => ParseInt(stringNumber)).Aggregate())
                .Bind(parsedIntegers => parsedIntegers.Select(parsedInt => GetUserNameById(parsedInt)).Aggregate())
                .Match(users =>
                {
                    users.ForEach(user => Console.WriteLine(user));
                }, errors =>
                {
                    Console.WriteLine(errors);
                });

            // Play around a little with these examples. Make it async! Make it awesome!


            var theAnswerToEverythingResult = Task.FromResult(Result.Ok(42));
            var answerResult = await theAnswerToEverythingResult.Bind(answer =>
                answer == 42
                    ? Result.Ok("It is THE answer!")
                    : Result.Error<string>("That does not solve any problems!"))
                .ConfigureAwait(false);
        }

        public Result<int> ParseInt(string input)
        {
            var success = int.TryParse(input, out var number);
            return success 
                ? Result.Ok(number)
                : Result.Error<int>("Input was no valid int!");
        }

        public Result<string> GetDataFromUnreliableSource(string data)
        {
            Random rnd = new Random();
            bool success = rnd.Next(0, 2) == 0;

            return success 
                ? Result.Ok(data) 
                : Result.Error<string>("Something went wrong!");
        }

        public Result<List<string>> GetDataFromUnreliableSource(List<string> data)
        {
            Random rnd = new Random();
            bool success = rnd.Next(0, 2) == 0;

            return success
                ? Result.Ok(data)
                : Result.Error<List<string>>("Something went wrong!");
        }

        public Result<string> GetUserNameById(int id)
        {
            bool contained = usersDictionary.TryGetValue(id, out string user);
            return contained
                ? Result.Ok(user)
                : Result.Error<string>($"There is no user for id {id}");
        }
    }
}

```

