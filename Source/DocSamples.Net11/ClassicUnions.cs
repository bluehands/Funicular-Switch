using FunicularSwitch.Generators;

namespace DocSamples.Net11.Classic;

// Hand-written (abstract) union types, as shown in the UnionType section of the README.

// begin-snippet: abstractUnion
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
// end-snippet

public static class AbstractUnionSample
{
    // begin-snippet: abstractUnionMatch
    static string PrintError(Error error) =>
        error.Match(
            failure => $"Ups, something went wrong: {failure.Message} - {failure.Exception}",
            invalidInput => $"Name was invalid: {invalidInput.Message}",
            notFound => $"Not found: {notFound.Message}"
        );
    // end-snippet

    // begin-snippet: abstractUnionSwitch
    static void PrintIfNotFound(Error error) =>
        error.Switch(
            failure => { /*ignore*/ },
            invalidInput => { /*ignore*/ },
            notFound => Console.WriteLine($"Not found: {notFound.Message}")
        );
    // end-snippet

    public static void Run()
    {
        Console.WriteLine(PrintError(new NotFound("The answer")));
        PrintIfNotFound(new NotFound("The question"));
    }
}

// begin-snippet: staticFactories
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
// end-snippet

// begin-snippet: staticFactoriesNested
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
// end-snippet
