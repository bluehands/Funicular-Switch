using System.Globalization;
using FluentAssertions;
using FunicularSwitch;
using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;
using Xunit.Sdk;

//[assembly: GenerateExtensionsForInternalTypes(typeof(ExampleResult))]

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

    [Fact]
    public void GenericUnionType_Match_WorksCorrectly()
    {
        // ARRANGE
        var first = GenericUnionType<int>.First(5);

        // ASSERT
        first.Match(
                first: f => f.Value,
                second: _ => -1)
            .Should()
            .Be(5);

        // ARRANGE
        var second = GenericUnionType<int>.Second();

        // ASSERT
        second.Match(
                first: f => -1,
                second: _ => 42)
            .Should()
            .Be(42);
    }

    [Fact]
    public void GenericUnionType_Should_Be_X()
    {
        // ARRANGE
        var first = GenericUnionType<int>.First(5);

        // ASSERT
        first.Should().BeFirst().Which.Value.Should().Be(5);
        var firstShouldBeSecond = () => first.Should().BeSecond();
        firstShouldBeSecond.Should().Throw<XunitException>();

        // ARRANGE
        var second = GenericUnionType<int>.Second();
        second.Should().BeSecond().Which.Should().BeSameAs(second);
        var secondShouldBeFirst = () => second.Should().BeFirst();
        secondShouldBeFirst.Should().Throw<XunitException>();
    }

    [Fact]
    public void MultiGenericUnionType_Match_WorksCorrectly()
    {
        // ARRANGE
        var one = MultiGenericUnionType<int, string, float>.One(5, "Test");

        // ASSERT
        one.Match(
                one: o => o.First + o.Second,
                two: t => "Two")
            .Should()
            .Be("5Test");

        // ARRANGE
        var two = MultiGenericUnionType<int, string, float>.Two(3.14f);

        // ASSERT
        two.Match(
                one: o => "One",
                two: t => t.Third.ToString("0.00", CultureInfo.InvariantCulture))
            .Should()
            .Be("3.14");
    }

    [Fact]
    public void MultiGenericUnionType_Should_Be_X()
    {
        // ARRANGE
        var one = MultiGenericUnionType<int, string, float>.One(5, "Test");

        // ASSERT
        one.Should().BeOne().Which.First.Should().Be(5);
        var oneShouldBeTwo = () => one.Should().BeTwo();
        oneShouldBeTwo.Should().Throw<XunitException>();

        // ARRANGE
        var two = MultiGenericUnionType<int, string, float>.Two(3.14f);

        // ASSERT
        two.Should().BeTwo().Which.Third.Should().BeApproximately(3.14f, float.Epsilon);
        var twoShouldBeOne = () => two.Should().BeOne();
        twoShouldBeOne.Should().Throw<XunitException>();
    }
}