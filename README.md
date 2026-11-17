# FunicularSwitch

[![NuGet](https://img.shields.io/nuget/v/FunicularSwitch.svg?label=FunicularSwitch)](https://www.nuget.org/packages/FunicularSwitch/)
[![NuGet](https://img.shields.io/nuget/v/FunicularSwitch.Generators.svg?label=FunicularSwitch.Generators)](https://www.nuget.org/packages/FunicularSwitch.Generators/)
[![NuGet](https://img.shields.io/nuget/v/FunicularSwitch.Analyzers.svg?label=FunicularSwitch.Analyzers)](https://www.nuget.org/packages/FunicularSwitch.Analyzers/)
[![NuGet](https://img.shields.io/nuget/v/FunicularSwitch.Transformers.svg?label=FunicularSwitch.Transformers)](https://www.nuget.org/packages/FunicularSwitch.Transformers/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/bluehands/Funicular-Switch/blob/main/LICENSE)

FunicularSwitch is a lightweight C# port of F# result and option types.

FunicularSwitch helps you to:

- Focus on the 'happy path', but collect all error information.
- Be more explicit in what our methods return.
- Avoid deep nesting.
- Avoid null checks and eventual properties (properties only relevant for a certain state of an object), use Result or Option instead.
- Comfortably write async code pipelines.
- Wrap third party library exceptions / return values into results at the code level where we really understand what is happening.

# Getting Started

### Packages

 - [NuGet: FunicularSwitch](https://www.nuget.org/packages/FunicularSwitch/)
 - [NuGet: FunicularSwitch.Analyzers](https://www.nuget.org/packages/FunicularSwitch.Analyzers/)
 - [NuGet: FunicularSwitch.Generators](https://www.nuget.org/packages/FunicularSwitch.Generators/)
 - [NuGet: FunicularSwitch.Transformers](https://www.nuget.org/packages/FunicularSwitch.Transformers/)
 - [NuGet: FunicularSwitch.Generic](https://www.nuget.org/packages/FunicularSwitch.Generic/): a Result type with a generic error type, no source generator needed
 - [NuGet: FunicularSwitch.Generic.Assertions](https://www.nuget.org/packages/FunicularSwitch.Generic.Assertions/): AwesomeAssertions extensions for the types of FunicularSwitch.Generic
 - [NuGet: FunicularSwitch.Generators.AwesomeAssertions](https://www.nuget.org/packages/FunicularSwitch.Generators.AwesomeAssertions/): generates AwesomeAssertions extensions for your own result and union types

To get started add the first two packages to your project:

```
dotnet add package FunicularSwitch
dotnet add package FunicularSwitch.Generators
```

If you just want to try `Result`, the first one is enough, the [first sample](#funicular_usage) shows how to create one.

[**FunicularSwitch**](https://github.com/bluehands/Funicular-Switch#funicularswitch-usage) is a library containing the Result and Option type. Usage and the general idea is described in the following sections. The 'Error' type is always string, which allows natural concatenation and is sufficient in many cases.

[**FunicularSwitch.Generators**](https://github.com/bluehands/Funicular-Switch#funicularswitchgenerators-usage) is a C# source generator package (projects consuming it, will have no runtime dependency to any FunicularSwitch dll). With this source generator you can have a result type with the very same behaviour as FunicularSwitch.Result but a custom error type (instead of string) by just annotating a class with the [`ResultType`](https://github.com/bluehands/Funicular-Switch#resulttype-attribute) attribute. That means you are free to represent failures in a way suitable for your needs. 
A second thing coming with this package is the [`UnionType`](https://github.com/bluehands/Funicular-Switch#-uniontype-attribute) attribute. This offers a way to have F# like unions with generated Match methods, forcing consumers to handle all cases (exhaustive matching). They allow for compiler safe switches handling all concrete subtypes of a base class. As a third thing the same Match methods are also generated for enum types annotated with the [`ExtendedEnum`](https://github.com/bluehands/Funicular-Switch#extendedenum-attribute) attribute.

The generators and analyzers also support the C# 15 features `closed` classes and `union` declarations, see [UnionType](#uniontype) and [docs/csharp15.md](docs/csharp15.md).

Building a project that references FunicularSwitch.Generators 5.0, FunicularSwitch.Analyzers 2.0 or FunicularSwitch 8.0 (which includes both) or newer requires the .NET 11 SDK (Visual Studio 2026, Roslyn 5.12 or newer). That is true even if you still target an older framework or language version. If you can't use the new SDK yet, stay on the old line: FunicularSwitch 7.x, FunicularSwitch.Generators 4.x and FunicularSwitch.Analyzers 1.x.

# <a name="funicular_usage"></a>FunicularSwitch Usage

*The code samples in this document live in [Source/DocSamples](https://github.com/bluehands/Funicular-Switch/tree/main/Source/DocSamples) and are inserted with [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets), so they are always compiled. To play around with them clone the repo and run `dotnet run --project Source/DocSamples -- resultMap`. The argument is the name of the sample, without an argument all samples are run. See [Contributing](#contributing) if you want to change a sample.*

This following section mainly focuses on `Result`. `Result` is a union type representing either Ok or the Error case just like F#s Result type. For FunicularSwitch the error type is `String` for sake of simplicity (Using types with multiple generic arguments is quite verbose in C#).

Result should be used in all places where something can go wrong. Doing so it replaces exceptions and null/default return values.

Creating a `Result` is easy:

<!-- snippet: resultCreation -->
```cs
//Ok result:
var fortyTwo = Result.Ok(42);
//or using implicit cast operator
Result<string> ok = "Ok";

//Error result:
var error = Result.Error<int>("Could not find the answer");
```
<!-- endSnippet -->

Now let's follow the happy path, do something, if everything was ok. `Map`:

<!-- snippet: resultMap -->
```cs
static Result<int> Ask() => 42;

Result<int> answerTransformed = Ask()
    .Map(answer => answer * 2);

Console.WriteLine(answerTransformed);
```
<!-- endSnippet -->

```
Ok 84
```

or do something that might fail, if everything was ok. `Bind`:

<!-- snippet: resultBind -->
```cs
static Result<int> Ask() => 42;

Result<int> answerTransformed = Ask()
    .Bind(answer => answer == 0 ? Result.Error<int>("Division by zero") : 42 / answer);

Console.WriteLine(answerTransformed);
```
<!-- endSnippet -->

```
Ok 1
```

The lambdas passed to `Map` and `Bind` are only invoked if everything went well so far, otherwise you are on the error track where error information is passed on 'invisibly':

<!-- snippet: errorPropagation -->
```cs
static Result<int> Transform(Result<int> result) =>
    result
        .Bind(answer => answer == 0 ? Result.Error<int>("Division by zero") : 42 / answer)
        .Map(transformed => transformed * 2);

Result<int> firstLevelError = Transform(Result.Error<int>("I don't know"));
Console.WriteLine($"First level: {firstLevelError}");

Result<int> secondLevelError = Transform(Result.Ok(0));
Console.WriteLine($"Second level: {secondLevelError}");
```
<!-- endSnippet -->

```
First level: Error I don't know
Second level: Error Division by zero
```

Finally you might want to leave the `Result` world, so you have to take care of the error case as well (that's a good thing!). `Match`:

<!-- snippet: resultMatch -->
```cs
static Result<int> Ask() => 42;

string whatIsIt =
    Ask().Match(
        answer => $"The answer is: {answer}",
        error => $"Ups: {error}"
    );

Console.WriteLine(whatIsIt);
```
<!-- endSnippet -->

```
The answer is: 42
```

Result also works with C# query syntax. Every `from` is a `Bind` and the final `select` is a `Map`. As soon as one step fails, the remaining steps are skipped and you get that error:

<!-- snippet: resultLinq -->
```cs
static Result<int> ParseInt(string text) =>
    int.TryParse(text, out var number) ? number : Result.Error<int>($"'{text}' is not a number");

static Result<int> Divide(int dividend, int divisor) =>
    divisor == 0 ? Result.Error<int>("Division by zero") : dividend / divisor;

static Result<int> DivideTexts(string a, string b) =>
    from x in ParseInt(a)
    from y in ParseInt(b)
    from quotient in Divide(x, y)
    select quotient * 2;

Console.WriteLine(DivideTexts("84", "2"));
Console.WriteLine(DivideTexts("84", "zero"));
```
<!-- endSnippet -->

```
Ok 84
Error 'zero' is not a number
```

The same works for `Option` and for `Task<Result<T>>` (also mixed with plain `Result` steps in the same query, which then gives you a `Task<Result<T>>`).

Those are basically the four main operations on `Result`: create, `Map`, `Bind` and `Match`. There are a lot of overloads and other helpers in FunicularSwitch to avoid repetition of `Result` specific patterns like:

- 'Combine results to Ok if everything is Ok otherwise collect errors' - `Aggregate`, `Map` and `Bind` overloads on collections
- 'Ok if at least one item passes certain validations, otherwise collect info why no one matched' - `FirstOk`
- 'Ok if item from a dictionary was found, otherwise (nice) error' - `TryGetValue` extension on Dictionary
- 'Ok if type T is `as` convertible to T1, error otherwise' - 'As' extension returning Result
- 'Ok if item is valid regarding custom validations, error otherwise' - `Validate`
- 'Async support' - `Map` `Bind` and `Aggregate` overloads with async lambdas and extensions defined on Task<...>
- ...

If you miss functionality it can be added easily by writing your own extension methods. If it is useful for us all don't hesitate to make a pull request. Finally a little example demonstrating some of the functionality mentioned above (validation, aggregation, async pipeline). Let's cook:

<!-- snippet: fruitSalad -->
```cs
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
        await ingredients.Select(ingredient =>
                stock
                    .Where(fruit => fruit.Name == ingredient)
                    .FirstOk(CheckFruit, onEmpty: () => $"No {ingredient} in stock")
            )
            .Aggregate()
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
<!-- endSnippet -->

```
No salad today:
Apple is not fresh
No Banana in stock
No Pear in stock
Stink fruit, I do not serve that
```

As you can see, all errors are collected as far as possible. Feel free to play around with the cook's skill level, fruits in stock and the ingredients list to finally get your fruit salad.

## Option

`Option<T>` is the type to use if a value may be missing and there is nothing to say about why. It has the same operations as `Result`: `Map`, `Bind`, `Match` and so on. `GetValueOrDefault` leaves the Option world, `ToResult` turns an `Option` into a `Result` and `ToOption` goes the other way.

<!-- snippet: option -->
```cs
Option<int> some = Option.Some(42);
Option<int> none = Option.None<int>();

Console.WriteLine(some.Map(i => i * 2));
Console.WriteLine(none.Map(i => i * 2));

//Match works like on Result
Console.WriteLine(some.Match(i => $"The answer is {i}", () => "No answer"));

//leave the Option world with a fallback value
int answer = none.GetValueOrDefault(0);
Console.WriteLine(answer);

//convert to Result, the error is only created if the option is None
Result<int> result = none.ToResult(() => "No answer found");
Console.WriteLine(result);
```
<!-- endSnippet -->

```
84
None Int32
The answer is 42
0
Error No answer found
```

# <a name="analyzers_usage"></a>FunicularSwitch.Analyzers.Usage

The FunicularSwitch.Analyzers package is automatically included when you reference the FunicularSwitch package starting from version 6.2.0.

The analyzers are designed to provide useful refactoring and to show usage opportunities that can help people using the functional concepts for the first time to just experiment with the basics and get recommendations on which other functions and concepts may apply to their situation.

## Analyzers

### FS0001: MatchNullAnalyzer

Consider the following code

```csharp
public string? ToLower(Option<string> textOption)
{
    return textOption.Match(some: some => some.ToLower(), none: () => null);
}
```

While this works as intended, the intention of
- convert the text to lowercase, if there is text
- return the converted text, or null if none

can be also expressed using the ``.Map(...)`` and ``.GetValueOrDefault()`` operators which will more explicitly tell someone familiar with functional concepts the above two steps. The analyzer points this out, and offers a code fix that will refactor the code to:
```csharp
public string? ToLower(Option<string> textOption)
{
    return textOption.Map(some => some.ToLower()).GetValueOrDefault();
}
```

The other analyzers (FS0002 to FS0007) only appear if the project uses C# 15 with .NET 11 references. They suggest to declare a `[UnionType]` base or a `[ResultType]` class as `closed`, to make cases `sealed`, and to use `InferClosedTypePolymorphism` instead of a hand written `[JsonDerivedType]` list. All of them are described in [docs/analyzers.md](docs/analyzers.md).

# <a name="generators_usage"></a>FunicularSwitch.Generators Usage

## ResultType attribute

After adding the FunicularSwitch.Generators package you can mark a class as result type using the `ResultType` attribute. The class has to be abstract (or `closed` with C# 15, see below) and partial with a single generic argument. Ok and Error cases, Map, Bind, Match and some other methods will be generated so you can use your Result just like the one from the FunicularSwitch package. We recommend using a [UnionType](https://github.com/bluehands/Funicular-Switch#uniontype) as error type but you are free to use any type you want to represent failures.

``` cs
  [FunicularSwitch.Generators.ResultType(ErrorType = typeof(MyCustomError))]
  public abstract partial class Result<T> {}
```

With C# 15 you can declare the class `closed` instead of `abstract`. The generated base type is closed then too, so you can write a native `switch` expression over the nested `Ok_` and `Error_` cases and the compiler checks that you handled both. This is not switched on automatically, because `abstract` and `closed` can't be combined and the compiler wants to see `closed` on your own part of the partial class. Analyzer FS0007 suggests the change.

<!-- snippet: closedResult -->
```cs
[ResultType]
public closed partial class Result<T>;
```
<!-- endSnippet -->

<!-- snippet: closedResultUsage -->
```cs
Result<int> result = Result.Ok(42);

var text = result switch
{
    Result<int>.Ok_ ok => $"Ok {ok.Value}",
    Result<int>.Error_ error => $"Error {error.Details}",
};
```
<!-- endSnippet -->

### Exceptions

To turn all exceptions that might happen during your map, bind, validate, etc. calls into error results, write a static conversion method and mark it with the `ExceptionToError` attribute:
``` cs
public static class MyCustomErrorExtension
{
  [FunicularSwitch.Generators.ExceptionToError]
  public static MyCustomError ToGenericError(Exception ex) => ...
}
```
Having the ExceptionToError method, a call like `Result.Ok(42).Map(i => 42 / 0)` will return an error result with an error produced by your custom method instead of throwing a DivisionByZero exception.  

##### Considerations:
Using the `ExceptionToError` attribute is actually a decision that points into a direction that is different from the way Result is implemented in F#, where Result and the corresponding Error type are meant to model expected domain errors (see [fsharpforfunandprofit blog post](https://fsharpforfunandprofit.com/posts/against-railway-oriented-programming/)). You will still have to handle exceptions on the highest parts of your system and there is no 'fail fast' because early exceptions always travel through your whole Result chain.

### Combine results

If your errors can be combined, write an attributed extension method or a member method on your error type that combines two errors into one
``` cs
public static class MyCustomErrorExtension
{
  [FunicularSwitch.Generators.MergeError]
  public static MyCustomError Merge(this MyCustomError error, MyCustomError other) => ...
}
```
and a bunch of methods like `Aggregate`, `Validate`, `AllOk`, `FirstOk` and more will appear that make use of the fact that errors can be merged.

## <a name="uniontype"> UnionType attribute

There is another useful generator coming with the package. Adding the `UnionType` attribute to a base record / class or interface makes `Match` extension methods appear for this type. They are also inspired by F# where a match expression has to cover all cases and the compiler helps you with that. Assuming you implemented an error type as a base type and one derived type for every kind of error:

<!-- snippet: abstractUnion -->
```cs
[UnionType]
public abstract class Error
{
    public string Message { get; }
    protected Error(string message) => Message = message;
}

public sealed class NotFound(string message) : Error(message);
public sealed class Failure(string message, Exception exception) : Error(message)
{
    public Exception Exception { get; } = exception;
}
public sealed class InvalidInput(string message) : Error(message);
```
<!-- endSnippet -->

the generator detecting the `[UnionType]` adds Match methods so you can write:

<!-- snippet: abstractUnionMatch -->
```cs
static string PrintError(Error error) =>
    error.Match(
        failure => $"Ups, something went wrong: {failure.Message} - {failure.Exception}",
        invalidInput => $"Name was invalid: {invalidInput.Message}",
        notFound => $"Not found: {notFound.Message}"
    );
```
<!-- endSnippet -->

If you decide to add a case to your Error union all consuming switches break and you never miss a case at runtime!

Match methods are also provided for async case handlers and as extensions on `Task<Error>`.

There are also `Switch` extension methods generated which are the 'void' versions of `Match`, although this is not recommended from a functional point of view :).

<!-- snippet: abstractUnionSwitch -->
```cs
static void PrintIfNotFound(Error error) =>
    error.Switch(
        failure => { /*ignore*/ },
        invalidInput => { /*ignore*/ },
        notFound => Console.WriteLine($"Not found: {notFound.Message}")
    );
```
<!-- endSnippet -->

To avoid bad surprises a well defined order of parameters of Match methods is crucial. By default parameters are generated in alphabetical order. This behaviour can be adapted using the `CaseOrder` argument on `UnionType` attribute (FunicularSwitch.Generators namespace omitted):

``` cs
//default
[UnionType(CaseOrder = CaseOrder.Alphabetical)]
public abstract class Error{...}

//useful for union types that define their cases as nested subclasses in a well defined order
[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract class Error{...}

//order defined explicitly. Case sort index with [UnionCase] attribute on derived types is expected (generator warning if missing or ambiguous)
[UnionType(CaseOrder = CaseOrder.Explicit)]
public abstract class Error{...}

[UnionCase(index: 0)]
public sealed class NotFound : Error {...}
[UnionCase(index: 20)]
public sealed class Failure : Error {...}
[UnionCase(index: 10)]
public sealed class InvalidInput : Error {...}
```

#### Static factory methods
If your base type is a partial record or class, static factory methods for your derived cases are added: 

<!-- snippet: staticFactories -->
```cs
[UnionType]
public abstract partial record Failures;

public record Missing(int Id, string Message = "Not found") : Failures;
public record Invalid(string Message) : Failures;

class StaticFactoriesConsumer
{
    public static void UseGeneratedFactoryMethods()
    {
        var missing = Failures.Missing(42); //default value is pulled up to factory methods.
        var invalid = Failures.Invalid("I don't like it");
    }
}
```
<!-- endSnippet -->

Those factory methods are not generated if they would conflict with an existing field, property or method on the base type. 
So you can always decide to implement them by yourself. Generation of factory methods on a partial base type can be suppressed 
by setting StaticFactoryMethods argument to false: `[UnionType(StaticFactoryMethods=false)]`. Currently default values in 
constructor parameters from namespaces other than System need full qualification.
If you like to declare your cases as nested types of your base types you can use an underscore prefix or postfix with your nested type name to avoid conflicts with factory methods. Static factory method will then be generated without the underscore. This also works if you use the base type name as prefix or postfix.

<!-- snippet: staticFactoriesNested -->
```cs
[UnionType]
public abstract partial record Problem
{
    public record NotFound_(int Id) : Problem;
}

public record InvalidInputProblem(string Message) : Problem;

class NestedStaticFactoriesConsumer
{
    public static void UseGeneratedFactoryMethods()
    {
        var notFound = Problem.NotFound(42); //static factory method generated without underscore used in typename NotFound_
        var invalid = Problem.InvalidInput("I don't like it"); //static factory method generated without base typename postfix
    }
}
```
<!-- endSnippet -->

Base types of unions may also be generic types with arbitrary number of type parameters. Case types with their own generic arguments are only supported if they are declared outside of the base type and hand all type parameters on to it, like `record Leaf<T> : Tree<T>`.

### Closed unions

With C# 15 a base type can be declared `closed`. The compiler then knows all derived types, so it checks `switch` expressions over the base type for exhaustiveness. FunicularSwitch uses that: for a `closed` union the generated `Match` and `Switch` methods have no default arm that throws, the compiler checks the generated code instead. You just replace `abstract` with `closed` (this needs the .NET 11 SDK, C# 15 and a project referencing .NET 11):

<!-- snippet: closedUnion -->
```cs
[UnionType]
public closed record Error;

public sealed record NotFound(string Message) : Error;
public sealed record Failure(string Message, Exception Exception) : Error;
public sealed record InvalidInput(string Message) : Error;
```
<!-- endSnippet -->

`Match` works as before and you can also use a native `switch` expression:

<!-- snippet: closedUnionUsage -->
```cs
public static string PrintError(Error error) =>
    error.Match(
        failure => $"Ups, something went wrong: {failure.Message}",
        invalidInput => $"Name was invalid: {invalidInput.Message}",
        notFound => $"Not found: {notFound.Message}"
    );

//the compiler checks this switch for exhaustiveness as well
public static string PrintErrorNative(Error error) =>
    error switch
    {
        NotFound notFound => $"Not found: {notFound.Message}",
        Failure failure => $"Ups, something went wrong: {failure.Message}",
        InvalidInput invalidInput => $"Name was invalid: {invalidInput.Message}",
    };
```
<!-- endSnippet -->

If the hierarchy is not completely closed, for example because an abstract intermediate type is not `closed`, the generator keeps the default arm. Analyzer FS0003 warns about that case, see [docs/analyzers.md](docs/analyzers.md). Closed hierarchies with several levels, generic base types and the combination with `System.Text.Json` (`InferClosedTypePolymorphism`) are described in [docs/csharp15.md](docs/csharp15.md).

### Native C# 15 unions

`UnionType` can also be put on a native C# 15 `union` declaration. The cases are the case types of the union, in the order of declaration. `Match`, `Switch` and their async variants and `Task<>` extensions are generated, but no static factory methods, the implicit conversions of the union cover that.

<!-- snippet: nativeUnion -->
```cs
[UnionType]
public union Pet(Cat, Dog);
```
<!-- endSnippet -->

<!-- snippet: nativeUnionUsage -->
```cs
Pet pet = new Dog("Rex");

var sound = pet.Match(
    cat => $"{cat.Name} says meow",
    dog => $"{dog.Name} says woof");
```
<!-- endSnippet -->

Instead of marking each union, `[assembly: ExtendUnions]` generates the same extension methods for all native unions of the assembly (public and internal ones; use `Accessibility = ExtensionAccessibility.Internal` to make the extensions internal, or `[assembly: ExtendUnions(typeof(SomeTypeInOtherAssembly))]` for the public unions of another assembly). A union that also has `[UnionType]` is generated once.

<!-- snippet: extendUnions -->
```cs
[assembly: ExtendUnions]
```
<!-- endSnippet -->

An instance of a union struct that was never assigned (`default`) has no value. In that case `Match` calls the handler of the first case with a nullable type, if there is one (`int?`, `string?`). Otherwise `Match` and `Switch` throw an `InvalidOperationException` with the message 'Union has no value'. Hand written structs marked with the `Union` attribute work the same way, if you add `UnionType` to them (`ExtendUnions` covers them as well). Note that native unions box value type cases. More details are in [docs/csharp15.md](docs/csharp15.md).

## ExtendedEnum attribute

The `ExtendedEnum` attribute works like `UnionType` but for enums:

``` cs
[FunicularSwitch.Generators.ExtendedEnum]
public enum PlatformIdentifier
{
    LinuxDevice,
    DeveloperMachine,
    WindowsDevice
}
```

the generator detecting the `[ExtendedEnum]` adds Match methods so you can write:

``` cs
var isGraphicalLinux = PlatformIdentifier.LinuxDevice
    .Match(
        developerMachine: () => false,
        linuxDevice: () => true,
        windowsDevice: () => true
    );
```

The default case order for `ExtendedEnum` is AsDeclared. To avoid problems with changing case orders, one should always use named parameters in Match and Switch calls!

To generate Match extensions for all types in an assembly use the `ExtendEnums` attribute. Flags enums and enums with duplicate values are omitted:

``` cs
//generate internal Match extension methods for all enums in System (Containing assembly of System.DateTime). 
[assembly: ExtendEnums(typeof(System.DateTime), Accessibility = ExtensionAccessibility.Internal)]

//shortcut to generate Match extension methods for all enums in current assembly
[assembly: ExtendEnums]
```

To generate Match extensions for a specific type in an assembly write:

```
[assembly: ExtendEnum(typeof(DateTimeKind), CaseOrder = EnumCaseOrder.Alphabetic)]
```

## TransformMonad attribute

Attaching the `TransformMonad` attribute to a type will generate return, bind and lift methods for a nested monad type.

The attributes first argument must be a monad type.

A monad type is either
- a generic type implementing a static return and an instance bind method
- a static class implementing a static return and a static bind method

Every argument after that needs to be a so-called transformer type.
A transformer type is any static class with a `MonadTransformer` attribute pointing to a monad type and implementing a BindT method with a certain signature.

If `TransformMonad` is attached to a generic type, this type will function as a wrapper type around the nested monad type. If it's a static class it will only generate extension methods for the nested monad type. 

Let's take `ResultT` from the [FunicularSwitch.Transformers](https://www.nuget.org/packages/FunicularSwitch.Transformers/) package to demonstrate this:

```cs
[MonadTransformer(typeof(Result<>))]
public static class ResultT
{
    public static Monad<Result<B>> BindT<A, B>(Monad<Result<A>> ma, Func<A, Monad<Result<B>>> fn) =>
        ma.Bind(aResult => aResult.Match(fn, e => ma.Return(Result.Error<B>(e))));
}

[TransformMonad(typeof(Option<>), typeof(ResultT))]
public static partial class OptionResult;
```

This will generate extension methods for `Option<Result<>>` to be able to bind this directly to another `Option<Result<>>`.
It also adds a static return method `OptionResult.SomeOk` (It's called `SomeOk` in this case because the name of the return method of `Option<>` is `Some` and of `Result<>` is `Ok`) and a lift method to raise an `Option<>` to the `Result<Option<>>` domain.
Additionally, it adds async variants, a map method and also methods to support LINQ query syntax.

## ExtendMonad attribute

Attaching the `ExtendMonad` attribute to a monad type will generate a map method, methods to allow using LINQ query
syntax and also async variants.

### Additional documentation

[Tutorial markdown](https://github.com/bluehands/Funicular-Switch/blob/main/TUTORIAL.md)

[Tutorial source](https://github.com/bluehands/Funicular-Switch/tree/main/Source/Tutorial)

[FunicularSwitch and C# 15](docs/csharp15.md)

[Analyzers](docs/analyzers.md)

# Contributing

We're looking forward to pull requests.

The code samples in this file are not written by hand. They are taken from the `// begin-snippet: name` markers in [Source/DocSamples](Source/DocSamples) and [Source/DocSamples.Net11](Source/DocSamples.Net11) by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets). After changing a sample, or the surrounding text, run

```
dotnet tool restore
dotnet mdsnippets
```

on top level. To check in a build that README.md is up to date, run the same command and then `git diff --exit-code README.md`. A missing snippet makes `mdsnippets` fail. The console output shown below some samples can be reproduced with `dotnet run --project Source/DocSamples -- <sample name>`.

# Versioning

We use [SemVer](http://semver.org/) for versioning.

# Authors

bluehands.de

# License

[MIT License](https://github.com/bluehands/Funicular-Switch/blob/main/LICENSE)

# Acknowledgments

[F# for fun and profit: Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)

[F# for fun and profit: Map and Bind and Apply, Oh my!](https://fsharpforfunandprofit.com/series/map-and-bind-and-apply-oh-my.html)

