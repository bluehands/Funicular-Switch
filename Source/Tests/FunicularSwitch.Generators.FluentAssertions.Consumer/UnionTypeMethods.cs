using FluentAssertions;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;
using Xunit.Sdk;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer;

public class UnionTypeMethods
{
    [Fact]
    public void MyError_FirstCase()
    {
        // ARRANGE
        var result = MyError.FirstCase(5);

        // ASSERT
        result.Should().BeFirstCase().Which.Number.Should().Be(5);
    }

    [Fact]
    public void MyError_SecondCase()
    {
        // ARRANGE
        var result = MyError.SecondCase("Text");

        // ASSERT
        result.Should().BeSecondCase().Which.Text.Should().Be("Text");
    }

    [Fact]
    public void NestedUnionType_FirstCase()
    {
        // ARRANGE
        var result = WrapperClass.NestedUnionType.Derived(7);

        // ASSERT
        result.Should().BeDerivedNestedUnionType().Which.Number.Should().Be(7);
    }

    [Fact]
    public void NestedUnionType_OtherCase()
    {
        // ARRANGE
        var result = WrapperClass.NestedUnionType.Other("Test");

        // ASSERT
        result.Should().BeOtherDerivedNestedUnionType().Which.Message.Should().Be("Test");
    }

    private static Action Action(Action action) => action;

    [Fact]
    public void MyError_FirstCase_IsNotSecondCase()
    {
        // ARRANGE
        var result = MyError.FirstCase(5);

        // ASSERT
        Action(() => result.Should().BeSecondCase())
            .Should().Throw<XunitException>();
    }

    [Fact]
    public void MyError_SecondCase_IsNotFirstCase()
    {
        // ARRANGE
        var result = MyError.SecondCase("Text");

        // ASSERT
        Action(() => result.Should().BeFirstCase())
            .Should().Throw<XunitException>();
    }

    [Fact]
    public void NestedUnionType_FirstCase_IsNotOtherCase()
    {
        // ARRANGE
        var result = WrapperClass.NestedUnionType.Derived(7);

        // ASSERT
        Action(() => result.Should().BeOtherDerivedNestedUnionType())
            .Should().Throw<XunitException>();
    }

    [Fact]
    public void NestedUnionType_OtherCase_IsNotFirstCase()
    {
        // ARRANGE
        var result = WrapperClass.NestedUnionType.Other("Test");

        // ASSERT
        Action(() => result.Should().BeDerivedNestedUnionType())
            .Should().Throw<XunitException>();
    }
}