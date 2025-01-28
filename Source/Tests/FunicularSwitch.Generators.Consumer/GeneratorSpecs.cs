using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunicularSwitch.Generators.Consumer.Extensions;
using static FunicularSwitch.Generators.Consumer.OperationResult;

namespace FunicularSwitch.Generators.Consumer;





[TestClass]
public class When_using_generated_result_type
{
    [TestMethod]
    public void Then_it_feels_good()
    {
        static OperationResult<decimal> Divide(decimal i, decimal divisor) => divisor == 0
            ? Error<decimal>(Error.Generic("Division by zero"))
            : i / divisor;

        OperationResult<int> result = 42;

        var calc = result
            .Bind(i => Divide(i, 0))
            .Map(i => (i * 2).ToString(CultureInfo.InvariantCulture));

        calc.Should().BeEquivalentTo(OperationResult<string>.Error(Error.Generic("Division by zero")));

        var combinedError = calc.Aggregate(Error<int>(Error.NotFound()));
        var combinedErrorStatic = Aggregate(calc, Error<int>(Error.NotFound()), (_, i) => i);
        var combinedOk = Ok(42).Aggregate(Ok(" is the answer"));
        var combinedOkStatic = Aggregate(Ok(42), Ok(" is the answer"));

        var transformedToInt = combinedOkStatic.As<int>(() => Error.Generic("Unexpected type"));

        static IEnumerable<Error> IsGreaterThanFive(int i)
        {
            if (i <= 5)
                yield return Error.Generic("To small");
            if (i == 3)
                yield return Error.Generic("Uuh, it's three...");
        }

        (3.Validate(IsGreaterThanFive) is OperationResult<int>.Error_
        {
            Details: Error.Aggregated_
            {
                Errors:
                {
                    Count: 2
                }
            }
        }).Should().BeTrue();
    }

    [TestMethod]
    public async Task Void_switches_are_generated()
    {
        var error = Error.Generic("This is wrong");

        static void DoNothing<T>(T item) { }
        error.Switch(
            generic: DoNothing,
            notFound: DoNothing,
            notAuthorized: DoNothing,
            aggregated: DoNothing
        );

        static Task DoNothingAsync<T>(T item) => Task.CompletedTask;
        await error.Switch(
            generic: DoNothingAsync,
            notFound: DoNothingAsync,
            notAuthorized: DoNothingAsync,
            aggregated: DoNothingAsync
        );
    }

    [TestMethod]
    public async Task ExceptionsAreTurnedIntoErrors()
    {
        var ok = Ok(42);

        // ReSharper disable once IntDivisionByZero
        var result = ok.Map(i => i / 0);
        result.IsError.Should().BeTrue();

        // ReSharper disable once IntDivisionByZero
        result = await ok.Map(async i =>
        {
            await Task.Delay(100);
            return i / 0;
        });
        result.IsError.Should().BeTrue();

        42.Validate(BuggyValidate).IsError.Should().BeTrue();

        static IEnumerable<string> BuggyValidate(int number) => throw new InvalidOperationException("Boom");

    }

    [TestMethod]
    public void QueryExpressionSelect()
    {
        Result<int> subject = 42;
        var result =
            from r in subject
            select r;
        result.Should().BeEquivalentTo(Result.Ok(42));
    }

    [TestMethod]
    public void QueryExpressionSelectMany()
    {
        Result<int> ok = 42;
        var error = Result.Error<int>("fail");

        (
            from r in ok
            from r1 in error
            select r1
        ).Should().BeEquivalentTo(error);

        (
            from r in error
            from r1 in ok
            select r1
        ).Should().BeEquivalentTo(error);

        (
            from r in ok
            let x = r * 2
            from r1 in ok
            select x
        ).Should().BeEquivalentTo(ok.Map(r => r * 2));
    }

    [TestMethod]
    public async Task QueryExpressionSelectManyAsync()
    {
        Task<Result<int>> okAsync = Task.FromResult(Result.Ok(42));
        var errorAsync = Task.FromResult(Result.Error<int>("fail"));

        var ok = Result.Ok(1);

        (await (
            from r in okAsync
            from r1 in errorAsync
            select r1
        )).Should().BeEquivalentTo(await errorAsync);

        (await (
            from r in errorAsync
            from r1 in okAsync
            select r1
        )).Should().BeEquivalentTo(await errorAsync);

        (await (
            from r in okAsync
            let x = r * 2
            from r1 in okAsync
            select x
        )).Should().BeEquivalentTo(await okAsync.Map(r => r * 2));

        (await (
            from r in ok
            let x = r * 2
            from r1 in okAsync
            select x
        )).Should().BeEquivalentTo(ok.Map(r => r * 2));

        (await (
            from r in okAsync
            let x = r * 2
            from r1 in ok
            select x
        )).Should().BeEquivalentTo(await okAsync.Map(r => r * 2));
    }

    [TestMethod]
    public void TestFactoryMethodsForClassesWithPrimaryConstructor()
    {
        var x = WithPrimaryConstructor.Derived("Hallo", 42);
        Console.WriteLine($"Created {x.Match(d => $"{d.Message} {d.Test}")}");
    }

    [TestMethod]
    public void TestFactoryMethodsForInterfaceUnion()
    {
        var x = IUnion.Case1();
    }
}

[ResultType(ErrorType = typeof(string))]
public abstract partial class Result<T>
{
}

[ResultType(typeof(Error))]
abstract partial class OperationResult<T>
{
}

public static partial class ErrorExtension
{
    [MergeError]
    public static Error MergeErrors(this Error error, Error other) => error.Merge(other);

    [MergeError]
    public static string MergeErrors(this string error, string other) => $"{error}{Environment.NewLine}{other}";

    [ExceptionToError]
    public static string UnexpectedToStringError(Exception exception) => $"Unexpected error occurred: {exception}";
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial class Error
{
    [ExceptionToError]
    public static Error Generic(Exception exception) => Generic(exception.ToString());

    public Error Merge(Error other) => this is Aggregated_ a
        ? a.Add(other)
        : other is Aggregated_ oa
            ? oa.Add(this)
            : Aggregated(ImmutableList.Create(this, other));

    public class Generic_ : Error
    {
        public string Message { get; }

        public Generic_(string message) : base(UnionCases.Generic)
        {
            Message = message;
        }
    }

    public class NotFound_ : Error
    {
        public NotFound_() : base(UnionCases.NotFound)
        {
        }
    }

    public class NotAuthorized_ : Error
    {
        public NotAuthorized_() : base(UnionCases.NotAuthorized)
        {
        }
    }

    public class Aggregated_ : Error
    {
        public ImmutableList<Error> Errors { get; }

        public Aggregated_(ImmutableList<Error> errors) : base(UnionCases.Aggregated) => Errors = errors;

        public Error Add(Error other) => Aggregated(Errors.Add(other));
    }

    internal enum UnionCases
    {
        Generic,
        NotFound,
        NotAuthorized,
        Aggregated
    }

    internal UnionCases UnionCase { get; }
    Error(UnionCases unionCase) => UnionCase = unionCase;

    public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
    bool Equals(Error other) => UnionCase == other.UnionCase;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Error)obj);
    }

    public override int GetHashCode() => (int)UnionCase;
}

[UnionType]
public abstract partial class WithPrimaryConstructor(int test)
{
    public int Test { get; } = test;
}

public class DerivedWithPrimaryConstructor(string message, int test) : WithPrimaryConstructor(test)
{
    public string Message { get; } = message;
}

[UnionType] //if you are on .net standard use StaticFactoryMethods=false because default interface implementations are not supported
public partial interface IUnion
{
    public record Case1_ : IUnion;
    public record Case2_ : IUnion;
}

[UnionType]
public abstract partial record Failure
{
    public record NotFound_(int Id) : Failure;
}

public record InvalidInputFailure(string Message) : Failure;

class ExampleConsumer
{
    public static void UseGeneratedFactoryMethods()
    {
        var notFound = Failure.NotFound(42); //static factory method generated without underscore used in typename NotFound_ 
        var invalid = Failure.InvalidInput("I don't like it"); //static factory method generated without base typename postfix 
    }
}

[FunicularSwitch.Generators.UnionType]
abstract partial record CardType //declare partial to allow source generator to add static factory methods
{
    public record MaleCardType(int Age) : CardType;
    public record FemaleCardType : CardType;
}

[TestClass]
public class Try
{
    [TestMethod]
    public void It()
    {
        var male = CardType.Male(42); //static factories added by source generator
        var female = CardType.Female();

        Console.WriteLine(Print(male));
        Console.WriteLine(Print(female));
        return;

        static string Print(CardType cardType) =>
            cardType
                .Match( //exhaustive pattern matching
                    female: _ => "Female",
                    male: m => $"Male with age {m.Age}"
                );
    }
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record GenericResult<T, TFailure>(bool IsOk)
{
    public record Ok_(T Value) : GenericResult<T, TFailure>(true);
    public record Error_(TFailure Failure) : GenericResult<T, TFailure>(false);
}

[TestClass]
public class TestGenericResult
{
    [TestMethod]
    public async Task MatchIt()
    {
        var okResult = GenericResult<int, string>.Ok(42);
        var errorResult = GenericResult<int, string>.Error("Ups...");
        var taskOkResult = Task.FromResult(okResult);
        var taskErrorResult = Task.FromResult(errorResult);

        okResult.Match(ok => ok.Value, _ => 0).Should().Be(42);
        (await taskOkResult.Match(ok => ok.Value, _ => 0)).Should().Be(42);
        errorResult.Match(ok => ok.Value, _ => 0).Should().Be(0);
        (await taskErrorResult.Match(ok => ok.Value, _ => 0)).Should().Be(0);
        (await taskErrorResult.Match(ok => Task.FromResult(ok.Value), _ => Task.FromResult(0))).Should().Be(0);

        okResult.Switch(ok => Assert.AreEqual(ok.Value, 42), err => Assert.AreEqual("Ups...", err.Failure));
        await okResult.Switch(ok => AreEqualAsync(ok.Value, 42), err => AreEqualAsync("Ups...", err.Failure));

        var okOrDefault = await okResult.Match(ok => Task.FromResult(ok.Value), _ => Task.FromResult(0));
        okOrDefault.Should().Be(42);

        var errorOrDefault = await errorResult.Match(ok => Task.FromResult(ok.Value), _ => Task.FromResult(0));
        errorOrDefault.Should().Be(0);

        return;

        static async Task AreEqualAsync<T>(T expected, T actual)
        {
            await Task.Delay(10);
            Assert.AreEqual(expected, actual);
        }
    }
}