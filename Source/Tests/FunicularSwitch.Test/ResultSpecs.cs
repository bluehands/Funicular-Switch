using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test
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
            ((Something) null).ToOption().Should().Equal(Option.None<Something>());

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
    }
}
