using FluentAssertions;
using Xunit.Sdk;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer;

public class OptionMethods
{
    [Fact]
    public void Option_Some_ShouldBeSome()
    {
        // ARRANGE
        var option = Option.Some(23);

        // ASSERT
        option.Should().BeSome()
            .Which.Should().Be(23);
    }

    private static Action Action(Action action) => action;

    [Fact]
    public void Option_Some_ShouldNotBeNone()
    {
        // ARRANGE
        var option = Option.Some(23);


        // ASSERT
        Action(() => option.Should().BeNone())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("23");
    }

    [Fact]
    public void Option_None_ShouldBeNone()
    {
        // ARRANGE
        var option = Option.None<int>();

        // ASSERT
        option.Should().BeNone();
    }

    [Fact]
    public void Option_None_ShouldNotBeSome()
    {
        // ARRANGE
        var option = Option.None<int>();

        // ASSERT
        Action(() => option.Should().BeSome())
            .Should().Throw<XunitException>()
            .Which.Message.Should().Contain("None");
    }
}