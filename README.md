# Funicular-Switch

[![Build status](https://bluehands.visualstudio.com/bluehands%20Funicular%20Switch/_apis/build/status/bluehands%20Funicular%20Switch%20.NET%20Core-CI)](https://bluehands.visualstudio.com/bluehands%20Funicular%20Switch/_build/latest?definitionId=85)

Funicular-Switch is a lightweight C# railway oriented programming pattern oriented on F#'s result types.

By using Funicular-Switch Result we can achieve the following benefits:
- Avoid deep nesting.
- Avoid null checks, use Result or Option instead.
- Comfortably write async code pipelines.
- Ensure async is used properly.
- Easily combine async with synchronous code.
- Wrap third party library exceptions into results.
- Avoid throwing exceptions. Instead create a Result.Error of a specific type with a specific message.

Funicular-Switch contains two solutions for this approach: **Result** and **Option**.

## Getting Started

### Prerequisites

- Install the latest version of [NuGet.exe](https://www.nuget.org/)
- Install Visual Studio 2019 (optional)

### Installing

[NuGet: FunicularSwitch](https://www.nuget.org/packages/FunicularSwitch/)

Using dotnet CLI:
[Install Package using dotnet CLI](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-using-the-dotnet-cli)
```
dotnet add package FunicularSwitch
```

Using Visual Studio:
[Install Package in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio)

```
<PackageReference Include="FunicularSwitch" Version="x.x.x" />
```
## Usage

[Check out the additional Tutorial markdown here](https://github.com/bluehands/Funicular-Switch/blob/master/TUTORIAL.md)

[Check out the additional Tutorial source here](https://github.com/bluehands/Funicular-Switch/tree/master/Source/Tutorial)

First let's define two functions to Assert 42 is the answer to everything.
One synchronous, the other asynchronous (it has to ask for the correct answer first, which might take time ;)):

**Note**: When you will write an an async function that, returns a Task\<Result\<T>> and you integrate this function in your pipeline, you will see that the whole execution pipeline will be async but you can still use synchronous functions inside. 



```cs --region result-creation --source-file Source/DocSamples/Samples.cs --project Source/DocSamples/DocSamples.csproj
// Synchronous:
public Result<int> AssertItIsTheAnswerToEverything(int answer)
    => answer == 42
        ? Result.Ok(answer) //There is an implicit cast to Ok, so you could omit Result.Ok and just write: return answer;
        : Result.Error<int>($"Nah, {answer} is not THE answer!");

// Asynchronous:
public async Task<Result<int>> AsyncAssertItIsTheAnswerToEverything(int answer)
    => answer == await ComputeAnswer()
            ? Result.Ok(answer)
            : Result.Error<int>($"Nah, {answer} is not THE answer!");
```

### **Matching**

Match will check wether a given result is ok or erroneous. In case it is ok it will pass the content to the ok lambda and in case it is erroneous it will pass the error string to the error lambda. Nice thing about match is that to satisfy the compiler one has to handle both, the ok *and* the error case. That is a huge advantage compared to `if` or `switch` statements.

**Note**: match is usually used at the end of your execution pipeline 

*Synchronous match*:

```cs --region match-simple --source-file Source/DocSamples/Samples.cs --project Source/DocSamples/DocSamples.csproj
Result<int> theAnswer = AssertItIsTheAnswerToEverything(42);

theAnswer.Match(
	ok => Console.WriteLine($"{ok} no more sefsefsfd needed!"),
	error => Console.WriteLine(error)
);
```
Obviously, 42 is THE answer so we hit the ok case here.

*Async match*:

**Note**: Now we take advantage of all the extension methods by not assigning the Result to a variable.
You can always choose between those two options inside your code but in the most cases using the extensions directly on a parent Result will improve the readability of your code.

```cs --region match-simple-async --source-file Source/DocSamples/Samples.cs --project Source/DocSamples/DocSamples.csproj

```
Definitely, 0 is not the answer so we hit the error case here.

### **Map**

Map executes the given lambda inside map if previous result was ok and maps the given value to the previous result, otherwise pass the error from the previous result to the next node.

Map is can be used for operations, that can not fail e.g. multiply answer with two.

**Note**: If your operation can fail e.g. divide the answer with zero. use Bind instead and wrap the result of the division into a Result.Ok on success and otherwise wrap it into a Result.Error.

*As synchronous pipeline*:

```csharp
Result<int> answerResult = AssertItIsTheAnswerToEverything(90)
    .Map(answer => answer*2);
```

*As asynchronous pipeline*:

```csharp
Result<int> answerResult = await AsyncAssertItIsTheAnswerToEverything(42)
    .Map(answer => answer*2);
```

### **Bind**

Bind execute the given lambda inside bind if previous result was ok and binds the new result, which can either be ok or error, to the previous result, otherwise pass the error from the previous result to the next node.

*As synchronous pipeline*:

```csharp
Result<string> answerResult = AssertItIsTheAnswerToEverything(57)
    .Bind(answer => 
        answer == 42 
            ? Result.Ok("It is THE answer!")
            : Result.Error<string>("That does not solve any problems!")));
```

**Note**: We just changed the Result type of the Assertion from Result\<int> to Result\<string>. 

*As asynchronous pipeline*:

```csharp
Result<string> answerResult = await AsyncAssertItIsTheAnswerToEverything(42)
    .Bind(answer =>
        answer == 42
            ? Result.Ok("It is THE answer!")
            : Result.Error<string>("That answer will NOT solve any problems!"));
```

### **Aggregating**

Aggregate will take several results of different or equal types and merge them together to one result.
Aggregate the resulting result is ok if all aggregated results were ok.
In case any aggregated result was erroneous, the resulting result will be erroneous too.

> Errors will be combined with the default new line separator if not explicitly specified.

The following use cases are possible:

- 1.: We have two results of different type (or more) and we want to combine them to a tuple if all results are ok.
- 2.: We have two results of different type (or more) and we want to combine them with a specified lambda (rule) if all results are ok.
- 3.: We have a collection of results and we want to aggregate them to one result containing a list of content if all results are ok.

#### Use case 1:

*As synchronous pipeline*:

```csharp
var answerResult = Result.Ok("It is THE answer!");
// The aggregation will return Result<(int, string)>.
Result<(int, string)> tupleResult =
    AssertItIsTheAnswerToEverything(42)
        .Aggregate(answerResult);
```

*As asynchronous pipeline*:

```csharp
var answerResult = Result.Ok("It is THE answer!");
Result<(string, int)> tupleResult = 
    await AsyncAssertItIsTheAnswerToEverything(42)
        .Aggregate(answerResult);
```

#### Use case 2:

*As synchronous pipeline*:

```csharp
var answerResult = Result.Ok("It is THE answer!");
// The aggregation will return Result<string>.
Result<string> proofOfAnswerResult = 
    AssertItIsTheAnswerToEverything(42)
        .Aggregate(answerResult, (answer, answerToEverything) => 
        {
            if (answer == "It is THE answer!" && answerToEverything == 42) 
            {
                return Result.Ok("THE answer is unambiguously 42!!");
            }
            return Result.Error<string>("Black Holes started to consume the whole universe..!!!");
        });
```

*As asynchronous pipeline*:

> await the aggregation as showed before or pass the result to the next pipeline node.

#### Use case 3:

**Note**: Aggregate will use the default error separator: Environment.NewLine if not specified.

*As synchronous pipeline*:

```csharp
var possibleAnswers = new List<Result<int>> 
{
    AssertItIsTheAnswerToEverything(42),
    AssertItIsTheAnswerToEverything(10),
    AssertItIsTheAnswerToEverything(55),
};
// The aggregation will return Result<List<int>>.
Result<List<int>> possibleAnswersResult = possibleAnswers.Aggregate();
```

*As asynchronous pipeline*:

```csharp
var possibleAnswers = new List<Task<Result<int>>> 
{
    AsyncAssertItIsTheAnswerToEverything(42),
    AsyncAssertItIsTheAnswerToEverything(10),
    AsyncAssertItIsTheAnswerToEverything(55),
};
// The aggregation will return Result<List<int>>.
Result<List<int>> possibleAnswersResult = await possibleAnswers.Aggregate();
```

> You can also pass a maximum degree of maxDegreeOfParallelism to the aggregate function to limit the possible parallelism during execution.

> await the aggregation as showed before or pass the result to the next pipeline node.

### **Choosing**

Choose will take all ok results or those, matching the specified rule from a collection, combine and return them and processes all errors inside the lambda.

```csharp
var possibleAnswers = new List<Result<int>> 
{
    AssertItIsTheAnswerToEverything(42),
    AssertItIsTheAnswerToEverything(10),
    AssertItIsTheAnswerToEverything(55),
};
// Take all oks
IEnumerable<int> rightOkAnswers = possibleAnswers.Choose(onError => 
    {
        // Custom on error logic.
    });

// Or with a specified rule:
IEnumerable<int> rightAnswersMatchingTheRule = possibleAnswers.Choose(
    choose => 
    {
        choose == 42
    },
    onError => 
    {
        // Custom on error logic.
    });
```

## Running the tests

Not yet implemented

## Contributing

We accept Pull Requests (PR).

## Versioning

We use [SemVer](http://semver.org/) for versioning.

## Authors

bluehands.de

## License

[MIT License](https://github.com/bluehands/Funicular-Switch/blob/master/LICENSE)

## Acknowledgments

[F# for fun and profit: Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)

[F# for fun and profit: Map and Bind and Apply, Oh my!](https://fsharpforfunandprofit.com/series/map-and-bind-and-apply-oh-my.html)

FSharp Project: [FSharp](https://fsharp.org/)

## Known issues

- On Azure Build pipeline it might be required to set the LangVersion inside the .csproj to >= C# 7.0:
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <LangVersion>latest</LangVersion>
    </PropertyGroup>
</Project>
```

## Changelog

- Version 1.0.0