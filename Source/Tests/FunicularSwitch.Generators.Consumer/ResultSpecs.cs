using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FunicularSwitch.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Consumer
{
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
            resultSet.Should().BeEquivalentTo(new[]{Result.Ok(42), Result.Ok(23), Result.Error<int>("42")});
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
            Blubs(() => Console.Write("Hallo"));

            Result.Ok(42).Map(r =>
            {
                return r * 2;
            }).Should().Equal(Result.Ok(84));
            var error = Result.Error<int>("oh no");
            error.Map(r => r * 2).Should().Equal(error);
        }

        [DebuggerNonUserCode]
        void Blubs(Action bla)
        {
            Blubs2([DebuggerNonUserCode]() => bla());
        }

        [DebuggerNonUserCode]
        void Blubs2(Action bla)
        {
            bla();
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
        public void AsTest()
        {
            var obj = Result.Ok<object>(42);
            var intResult = obj.As<int>(() => "not an int");
            intResult.IsOk.Should().BeTrue();
            intResult.GetValueOrThrow().Should().Be(42);

            var stringResult = obj.As<string>(() => "not a string");
            stringResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void ImplicitCastTest()
        {
            Result<int> result = 42;
            result.Equals(Result.Ok(42)).Should().BeTrue();
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

            result.Should().BeOfType<Result<IReadOnlyCollection<int>>.Ok_>();
            result.GetValueOrThrow().Should().BeEquivalentTo(new[] { 0, 2, 4 });
        }

        [TestMethod]
        public async Task TryTest()
        {
            Result.Try(() => 42, _ => "error").Should().BeOfType<Result<int>.Ok_>();
            Result.Try(() => Result.Ok(42), _ => "error").Should().BeOfType<Result<int>.Ok_>();
            (await Result.Try(() => Task.FromResult(Result.Ok(42)), _ => "error")).Should().BeOfType<Result<int>.Ok_>();
            (await Result.Try(() => Task.FromResult(42), _ => "error")).Should().BeOfType<Result<int>.Ok_>();

            var zero = 0;
            Result.Try(() => 42 / zero, e => e.Message).Should().BeOfType<Result<int>.Error_>();
            (await Result.Try(async () =>
            {
                await Task.Delay(1);
                return 42 / zero;
            }, e => e.Message)).Should().BeOfType<Result<int>.Error_>();

            Result.Try(() => Result.Ok(42 / zero), e => e.Message).Should().BeOfType<Result<int>.Error_>();
            (await Result.Try(async () =>
            {
                await Task.Delay(10);
                return Result.Ok(42 / zero);
            }, e => e.Message)).Should().BeOfType<Result<int>.Error_>();
        }
    }
}