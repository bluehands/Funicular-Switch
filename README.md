# Funicular-Switch

![BuildStatus](https://bluehands.visualstudio.com/bluehands%20Funicular%20Switch/_apis/build/status/bluehandsFunicularSwitch-CI?branchName=master)
![Try_.NET Enabled](https://img.shields.io/badge/Try_.NET-Enabled-501078.svg)

Funicular-Switch is a lightweight C# port of F#'s result and option types.

Funicular-Switch helps you to:

- Focus on the 'happy path', but collect all error information.
- Be more explicit in what our methods return.
- Avoid deep nesting.
- Avoid null checks and eventual properties (properties only relevaent for a certain state of an object), use Result or Option instead.
- Comfortably write async code pipelines.
- Wrap third party library exceptions / return values into results at the code level were we really understand what is happening.

## Getting Started

### Package

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

*This document is created using [dotnet try](https://github.com/dotnet/try/blob/main/DotNetTryLocal.md). If you have dotnet try global tool installed, just clone the repo, type `dotnet try` on top level and play around with all code samples in your browser while reading.*

This following section mainly focuses on `Result`. `Result` is a union type representing either Ok or the Error case just like F#s Result type. For Funicular-Switch the error type is `String` for sake of simplicity (Using types with multiple generic arguments is quite verbose in C#).

Result should be used in all places, were something can go wrong. Doing so it replaces exceptions and null/default return values.

Creating a `Result` is easy:

``` cs --region resultCreation --source-file Source/DocSamples/ReadmeSamples.cs --project Source/DocSamples/DocSamples.csproj
//Ok result:
var fortyTwo = Result.Ok(42);
//or using implicit cast operator
Result<string> ok = "Ok";

//Error result:
var error = Result.Error<int>("Could not find the answer");
```

Now lets follow the happy path, do something, if everything was ok. `Map`:

``` cs --region map --source-file Source/DocSamples/ReadmeSamples.cs --project Source/DocSamples/DocSamples.csproj --session map
static Result<int> Ask() => 42;

Result<int> answerTransformed = Ask()
    .Map(answer => answer * 2);

Console.WriteLine(answerTransformed);
```

``` console --session map
Ok 84

```

or do something that might fail, if everything was ok. `Bind`:

``` cs --region bind --source-file Source/DocSamples/ReadmeSamples.cs --project Source/DocSamples/DocSamples.csproj --session bind
static Result<int> Ask() => 42;

Result<int> answerTransformed = Ask()
    .Bind(answer => answer == 0 ? Result.Error<int>("Division by zero") : 42 / answer);

Console.WriteLine(answerTransformed);
```

``` console --session bind
Ok 1

```

The lamdas passed to `Map` and `Bind` are only invoked if everything went well so far, otherwise you are on the error track were error information is passed on 'invisibly':
b

``` cs --region errorPropagation --source-file Source/DocSamples/ReadmeSamples.cs --project Source/DocSamples/DocSamples.csproj --session errorPropagation
static Result<int> Transform(Result<int> result) =>
                result
                    .Bind(answer => answer == 0 ? Result.Error<int>("Division by zero") : 42 / answer)
                    .Map(transformed => transformed * 2);

Result<int> firstLevelError = Transform(Result.Error<int>("I don't know"));
Console.WriteLine($"First level: {firstLevelError}");

Result<int> secondLevelError = Transform(Result.Ok(0));
Console.WriteLine($"Second level: {secondLevelError}");
```

``` console --session errorPropagation
First level: Error I don't know
Second level: Error Division by zero

```

Finally you might want to leave the `Result` world, so you have to take care of the error case as well (that's a good thing!). `Match`:

``` cs --region match --source-file Source/DocSamples/ReadmeSamples.cs --project Source/DocSamples/DocSamples.csproj --session match
static Result<int> Ask() => 42;

string whatIsIt =
    Ask().Match(
        answer => $"The answer is: {answer}",
        error => $"Ups: {error}"
    );

Console.WriteLine(whatIsIt);
```

``` console --session match
The answer is: 42

```

Those are basically the four (actually three) main operations on `Result` - `Create`, `Bind`, `Map` and `Match`. There are a lot of overloads and other helpers in Funicular-Switch to avoid repetition of `Result` specific patterns like:

- 'Combine results to Ok if everything is Ok otherwise collect errors' - `Aggregate`, `Map` and `Bind` overloads on collections
- 'Ok if at least one item passes certain validations, otherwise collect info why no one matched' - `FirstOk`
- 'Ok if item from a dictionary was found, ohterwise (nice) error' - `TryGetValue` extension on Dictionary
- 'Ok if type T is `as` convertible to T1, error otherwies' - 'As' extension returning Result
- 'Ok if item is valid regarind custom validations, error otherwise' - `Validate`
- 'Async support' - `Map` `Bind` and `Aggregate` overloads with async lamdas and extensions defined on Task<...>
- ...

If you miss functionality it easy to add it by writing your own extension methods. If it is useful for us all don't hesitate to make pull request. Finally a little example demonstrating some of the functionality mentioned above (validation, aggregation, async pipeline). Lets cook:

``` cs --region fruitSalad --source-file Source/DocSamples/ReadmeSamples.cs --project Source/DocSamples/DocSamples.csproj --session fruitSalad
public static async Task FruitSalad()
{
    var stock = ImmutableList.Create(
        new Fruit("Orange", 155),
        new Fruit("Orange", 12),
        new Fruit("Apple", 132),
        new Fruit("Stink fruit", 1));

    var ingredients = ImmutableList.Create("Apple", "Banana", "Pear", "Stink fruit");

    const int cookSkillLevel = 3;

    static IEnumerable<string> CheckFruit(Fruit fruit)
    {
        if (fruit.AgeInDays > 20)
            yield return $"{fruit.Name} is not fresh";

        if (fruit.Name == "Stink fruit")
            yield return "Stink fruit, I do not serve that";
    }

    var salad =
        await ingredients
            .Select(ingredient =>
                stock
                    .Where(fruit => fruit.Name == ingredient)
                    .FirstOk(CheckFruit, onEmpty: () => $"No {ingredient} in stock")
                )
            .Bind(fruits => CutIntoPieces(fruits, cookSkillLevel))
            .Map(Serve);

    Console.WriteLine(salad.Match(ok => "Salad served successfully!", error => $"No salad today:{Environment.NewLine}{error}"));
}

static Result<Salad> CutIntoPieces(IEnumerable<Fruit> fruits, int skillLevel = 5)
{
    try
    {
        return CutFruits(fruits, skillLevel);
    }
    catch (Exception e)
    {
        return Result.Error<Salad>($"Ouch: {e.Message}");
    }
}

static Salad CutFruits(IEnumerable<Fruit> fruits, int skillLevel) => skillLevel > 5 ? new Salad(fruits) : throw new Exception("Cut my fingers");
static Task<Salad> Serve(Salad salad) => Task.FromResult(new Salad(salad.Fruits, true));

class Salad
{
    public IReadOnlyCollection<Fruit> Fruits { get; }
    public bool Served { get; }

    public Salad(IEnumerable<Fruit> fruits, bool served = false)
    {
        Fruits = fruits.ToList();
        Served = served;
    }
}

class Fruit
{
    public string Name { get; }
    public int AgeInDays { get; }

    public Fruit(string name, int ageInDays)
    {
        Name = name;
        AgeInDays = ageInDays;
    }
}
```

``` console --session fruitSalad
No salad today:
Apple is not fresh
No Banana in stock
No Pear in stock
Stink fruit, I do not serve that


```

As you can see, all errors are collected as far as possible. Feel free to play around with the cooks skill level, fruits in stock and the ingredients list to finally get your fruit salad.

#### Additional documentation

[Tutorial markdown](https://github.com/bluehands/Funicular-Switch/blob/master/TUTORIAL.md)

[Tutorial source](https://github.com/bluehands/Funicular-Switch/tree/master/Source/Tutorial)

## Contributing

We're looking forward to pull requests.

## Versioning

We use [SemVer](http://semver.org/) for versioning.

## Authors

bluehands.de

## License

[MIT License](https://github.com/bluehands/Funicular-Switch/blob/master/LICENSE)

## Acknowledgments

[F# for fun and profit: Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)

[F# for fun and profit: Map and Bind and Apply, Oh my!](https://fsharpforfunandprofit.com/series/map-and-bind-and-apply-oh-my.html)

