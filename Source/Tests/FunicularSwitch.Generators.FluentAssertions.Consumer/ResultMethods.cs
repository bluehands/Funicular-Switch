using FluentAssertions;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;
using Xunit.Sdk;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer;

public class ResultMethods
{
    [Fact]
    public void StockResult_OkResult_ShouldBeOk()
    {
        // ARRANGE
        var result = Result.Ok(25);

        // ASSERT
        result.Should().BeOk()
            .Which.Should().Be(25);
    }

    [Fact]
    public void StockResult_Error_ShouldBeError()
    {
        // ARRANGE
        var result = Result.Error<int>("SomeError");

        // ASSERT
        result.Should().BeError()
            .Which.Should().Be("SomeError");
    }

    private static Action Action(Action action) => action;

    [Fact]
    public void StockResult_Ok_ShouldNotBeError()
    {
        // ARRANGE
        var result = Result.Ok("Test");

        // ASSERT
        Action(() => result.Should().BeError())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("Test");
    }

    [Fact]
    public void StockResult_Error_ShouldNotBeOk()
    {
        // ARRANGE
        var result = Result.Error<string>("ErrorText");

        // ASSERT
        Action(() => result.Should().BeOk())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("ErrorText");
    }

    [Fact]
    public void ExampleResult_OkResult_ShouldBeOk()
    {
        // ARRANGE
        var result = ExampleResult.Ok("Test");

        // ASSERT
        result.Should().BeOk()
            .Which.Should().Be("Test");
    }

    [Fact]
    public void ExampleResult_ErrorResult_ShouldBeError()
    {
        // ARRANGE
        var error = MyError.FirstCase(17);
        var result = ExampleResult.Error<string>(error);

        // ASSERT
        result.Should().BeError()
            .Which.Should().Be(error);
    }

    [Fact]
    public void ExampleResult_FirstCaseResult_ShouldWork()
    {
        // ARRANGE
        var result = ExampleResult.Error<string>(MyError.FirstCase(5));

        // ASSERT
        result.Should().BeError()
            .Which.Should().BeFirstCase()
            .Which.Number.Should().Be(5);
    }

    [Fact]
    public void ExampleResult_SecondCaseResult_ShouldWork()
    {
        // ARRANGE
        var result = ExampleResult.Error<string>(MyError.SecondCase("Some Error Text"));

        // ASSERT
        result.Should().BeError()
            .Which.Should().BeSecondCase()
            .Which.Text.Should().Be("Some Error Text");
    }

    [Fact]
    public void ExampleResult_OkResult_ShouldNotBeError()
    {
        // ARRANGE
        var result = ExampleResult.Ok("Test");

        // ASSERT
        Action(() => result.Should().BeError())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("Test");
    }

    [Fact]
    public void ExampleResult_OkResult_ShouldNotBeFirstCase()
    {
        // ARRANGE
        var result = ExampleResult.Ok("Test");

        // ASSERT
        Action(() => result.Should().BeError()
                .Which.Should().BeFirstCase())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("Test");
    }

    [Fact]
    public void ExampleResult_OkResult_ShouldNotBeSecondCase()
    {
        // ARRANGE
        var result = ExampleResult.Ok("Test");

        // ASSERT
        Action(() => result.Should().BeError()
                .Which.Should().BeSecondCase())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("Test");
    }

    [Fact]
    public void ExampleResult_FirstCaseResult_ShouldNotBeOk()
    {
        // ARRANGE
        var result = ExampleResult.Error<string>(MyError.FirstCase(5));

        // ASSERT
        Action(() => result.Should().BeOk())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("FirstCase").And.Contain("5");
    }

    [Fact]
    public void ExampleResult_FirstCaseResult_ShouldNotBeSecondCase()
    {
        // ARRANGE
        var result = ExampleResult.Error<string>(MyError.FirstCase(5));

        // ASSERT
        Action(() => result.Should().BeError()
                .Which.Should().BeSecondCase())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("FirstCase").And.Contain("5");
    }


    [Fact]
    public void ExampleResult_SecondCaseResult_ShouldNotBeOk()
    {
        // ARRANGE
        var result = ExampleResult.Error<string>(MyError.SecondCase("Test"));

        // ASSERT
        Action(() => result.Should().BeOk())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("SecondCase").And.Contain("Test");
    }

    [Fact]
    public void ExampleResult_SecondCaseResult_ShouldNotBeSecondCase()
    {
        // ARRANGE
        var result = ExampleResult.Error<string>(MyError.SecondCase("Test"));

        // ASSERT
        Action(() => result.Should().BeError()
                .Which.Should().BeFirstCase())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("SecondCase").And.Contain("Test");
    }
}