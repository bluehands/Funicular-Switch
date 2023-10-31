using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class ResultSpecs
{
    [TestMethod]
    public void Equality()
    {
        // ReSharper disable EqualExpressionComparison
        Result.Ok("hallo").Equals(Result.Ok("hallo")).Should().BeTrue();
        Result.Ok(("hallo", 42)).Equals(Result.Ok(("hallo", 42))).Should().BeTrue();
        Result.Ok("hallo").Equals(Result.Error<string>("hallo")).Should().BeFalse();
        Result.Error<int>("error").Equals(Result.Error<int>("error")).Should().BeTrue();
        Result.Error<int>("error").Equals(Result.Error<int>("another error")).Should().BeFalse();
        // ReSharper disable once SuspiciousTypeConversion.Global
        Result.Error<long>("error").Equals(Result.Error<int>("error")).Should().BeFalse();
        // ReSharper restore EqualExpressionComparison

        // ReSharper disable SuspiciousTypeConversion.Global
        Result.Ok(42).Equals(Result.Ok(42L)).Should().BeFalse();
        Equals(Result.Error<int>("hallo"), Result.Error<long>("hallo")).Should().BeFalse();
        // ReSharper restore SuspiciousTypeConversion.Global

        var resultSet = ImmutableHashSet.Create(
            Result.Ok(42),
            Result.Ok(23),
            Result.Ok(42),
            Result.Error<int>("42"),
            Result.Error<int>("42")
        );

        resultSet.Should().HaveCount(3);
        resultSet.Should().BeEquivalentTo(new[] { Result.Ok(42), Result.Ok(23), Result.Error<int>("42") });
    }

    [TestMethod]
    public void EqualityOperators()
    {
        // ReSharper disable EqualExpressionComparison
        (Result.Ok("hallo") == Result.Ok("hallo")).Should().BeTrue();
        (Result.Ok("hallo") == Result.Ok("hallo there")).Should().BeFalse();
        (Result.Ok("hallo") != Result.Ok("hallo there")).Should().BeTrue();
        (Result.Ok(("hallo", 42)) == Result.Ok(("hallo", 42))).Should().BeTrue();
        (Result.Ok("hallo") == Result.Error<string>("hallo")).Should().BeFalse();
        (Result.Error<int>("error") == Result.Error<int>("error")).Should().BeTrue();
        (Result.Error<int>("error") == Result.Error<int>("another error")).Should().BeFalse();

        object left = Result.Ok("hallo");
        left.Equals(Result.Ok("hallo")).Should().BeTrue();
    }

    [TestMethod]
    public void Map()
    {
        Result.Ok(42).Map(r => r * 2).Should().Equal(Result.Ok(84));
        var error = Result.Error<int>("oh no");
        error.Map(r => r * 2).Should().Equal(error);
    }

    [TestMethod]
    public void Bind()
    {
        var ok = Result.Ok(42);
        var error = Result.Error<int>("operation failed");

        static Result<int> Ok(int input) => input * 2;
        Result<int> Error(int input) => error;

        ok.Bind(Ok).Should().Equal(Result.Ok(84));
        ok.Bind(Error).Should().Equal(error);
        error.Bind(Ok).Should().Equal(error);
        error.Bind(Error).Should().Equal(error);
    }

    [TestMethod]
    public void BindMany()
    {
        var okFive = Result.Ok(5);
        var errorMessage = "operation failed";
        var error = Result.Error<int>(errorMessage);

        static Result<int> Ok(int input) => input * 2;
        Result<int> Error(int input) => error;

        okFive.Bind(five => Enumerable.Range(0, five).Select(Ok)).Should()
            .BeEquivalentTo(Result.Ok(Enumerable.Range(0, 5).Select(i => i * 2)));

        okFive.Bind(five => Enumerable.Range(0, five).Select(Error)).Should()
            .BeEquivalentTo(Result.Error<List<int>>(string.Join(Environment.NewLine, errorMessage)));
    }

    [TestMethod]
    public void OptionResultConversion()
    {
        const string notThere = "it's not there";
        Option.Some(42).ToResult(() => notThere).Should().Equal(Result.Ok(42));
        Option.None<int>().ToResult(() => notThere).Should().Equal(Result.Error<int>(notThere));

        var something = new Something();
        something.ToOption().Should().Equal(Option.Some(something));
        ((Something?)null).ToOption().Should().Equal(Option.None<Something>());

        var option = Result.Ok(something).ToOption();
        option.Should().Equal(Option.Some(something));
        Result.Error<Something>(notThere).ToOption().Should().Equal(Option.None<Something>());

        var errorLogged = false;
        void LogError(string error) => errorLogged = true;
        Result.Error<Something>(notThere).ToOption(LogError).Should().Equal(Option.None<Something>());
        errorLogged.Should().BeTrue();
    }

    class Something
    {
    }

    [TestMethod]
    public void AsTest()
    {
        var obj = Result.Ok<object>(42);
        var intResult = obj.As<int>();
        intResult.IsOk.Should().BeTrue();
        intResult.GetValueOrThrow().Should().Be(42);

        var stringResult = obj.As<string>();
        stringResult.IsError.Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitCastTest()
    {
        Result<int> result = 42;
        result.Equals(Result.Ok(42)).Should().BeTrue();
        Option<int> option = 42;
        option.Equals(Option.Some(42)).Should().BeTrue();

        var odds = Enumerable.Range(0, 10).Choose(i => i % 2 != 0 ? i * 10 : Option<int>.None).ToList();
        odds.Should().BeEquivalentTo(Enumerable.Range(0, 10).Where(i => i % 2 != 0).Select(i => i * 10));
    }

    [TestMethod]
    public void BoolConversionTest()
    {
        if (Result.Ok(42))
        {
        }
        else Assert.Fail();

        if (!Result.Error<int>("Fail"))
        {
        }
        else Assert.Fail();

        if (!Result.Ok(42))
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public async Task AsyncAggregateTest()
    {
        static Task<Result<int>> AsyncOperation(int i) => Task.FromResult(Result.Ok(i * 2));

        var result = await Enumerable.Range(0, 3)
            .Select(AsyncOperation)
            .Aggregate();

        result.Should().BeOfType<Ok<IReadOnlyCollection<int>>>();
        result.GetValueOrThrow().Should().BeEquivalentTo(new[] { 0, 2, 4 });
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
        Task<Result<int>> ok = Task.FromResult(Result.Ok(42));
        var error = Task.FromResult(Result.Error<int>("fail"));

        var okSync = Result.Ok(1);

        (await (
            from r in ok
            from r1 in error
            select r1
        )).Should().BeEquivalentTo(await error);

        (await (
            from r in error
            from r1 in ok
            select r1
        )).Should().BeEquivalentTo(await error);

        (await (
            from r in ok
            let x = r * 2
            from r1 in ok
            select x
        )).Should().BeEquivalentTo(await ok.Map(r => r * 2));        
        
        (await (
            from r in okSync
            let x = r * 2
            from r1 in ok
            select x
        )).Should().BeEquivalentTo( okSync.Map(r => r * 2));        
        
        (await (
            from r in ok
            let x = r * 2
            from r1 in okSync
            select x
        )).Should().BeEquivalentTo(await ok.Map(r => r * 2));
    }
}

public static class CombineExtension
{
    public static Result<(T, T1)> BindCombine<T, T1>(this Result<T> result, Func<T, Result<T1>> operation) =>
        result.Bind(r => operation(r).Map(r1 => (r, r1)));
}