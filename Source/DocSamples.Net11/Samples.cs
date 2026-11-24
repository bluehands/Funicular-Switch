using FunicularSwitch.Generators;

// begin-snippet: extendUnions
[assembly: ExtendUnions]
// end-snippet

namespace DocSamples.Net11;

// begin-snippet: closedUnion
[UnionType]
public closed record Error;

public sealed record NotFound(string Message) : Error;
public sealed record Failure(string Message, Exception Exception) : Error;
public sealed record InvalidInput(string Message) : Error;
// end-snippet

public static class ClosedUnionSample
{
    // begin-snippet: closedUnionUsage
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
    // end-snippet

    public static void Run()
    {
        Console.WriteLine(PrintError(new NotFound("The answer")));
        Console.WriteLine(PrintErrorNative(new InvalidInput("Empty name")));
    }
}

public record Cat(string Name);
public record Dog(string Name);

// begin-snippet: nativeUnion
[UnionType]
public union Pet(Cat, Dog);
// end-snippet

public static class NativeUnionSample
{
    public static void Run()
    {
        // begin-snippet: nativeUnionUsage
        Pet pet = new Dog("Rex");

        var sound = pet.Match(
            cat => $"{cat.Name} says meow",
            dog => $"{dog.Name} says woof");
        // end-snippet
        Console.WriteLine(sound);
    }
}

// begin-snippet: closedResult
[ResultType]
public closed partial class Result<T>;
// end-snippet

public static class ClosedResultSample
{
    public static void Run()
    {
        // begin-snippet: closedResultUsage
        Result<int> result = Result.Ok(42);

        var text = result switch
        {
            Result<int>.Ok_ ok => $"Ok {ok.Value}",
            Result<int>.Error_ error => $"Error {error.Details}",
        };
        // end-snippet
        Console.WriteLine(text);
    }
}

public static class Program
{
    public static void Main()
    {
        ClosedUnionSample.Run();
        NativeUnionSample.Run();
        ClosedResultSample.Run();
        DocSamples.Net11.Classic.AbstractUnionSample.Run();
    }
}
