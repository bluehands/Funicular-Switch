using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunicularSwitch.Option;

namespace FunicularSwitch.Test;

[TestClass]
public class OptionSpecs
{
	[TestMethod]
	public void Match_ActionTAction_Some_SomeCalled()
	{
		// Given
		var target = Some(5);
		bool someCalled = false;
		bool noneCalled = false;
		
		// When
		target.Match(
			some: _ =>
			{
				someCalled = true;
			},
			none: () =>
			{
				noneCalled = true;
			});
		
		// Then
		someCalled.Should().BeTrue();
		noneCalled.Should().BeFalse();
	}

	[TestMethod]
	public void Match_ActionTAction_None_NoneCalled()
	{
		// Given
		var target = None<int>();
		bool someCalled = false;
		bool noneCalled = false;
		
		// When
		target.Match(
			some: x =>
			{
				someCalled = true;
			},
			none: () =>
			{
				noneCalled = true;
			});
		
		// Then
		someCalled.Should().BeFalse();
		noneCalled.Should().BeTrue();
	}

	[TestMethod]
	public async Task Match_FuncTTaskFuncTask_Some_SomeCalled()
	{
		// Given
		var target = Some(5);
		bool someCalled = false;
		int numberPassed = -1;
		bool noneCalled = false;
		
		// When
		await target.Match(
			some: x =>
			{
				someCalled = true;
				numberPassed = x;
				return Task.CompletedTask;
			}, none: () =>
			{
				noneCalled = true;
				return Task.CompletedTask;
			});
		
		// Then
		someCalled.Should().BeTrue();
		numberPassed.Should().Be(5);
		noneCalled.Should().BeFalse();
	}

	[TestMethod]
	public async Task Match_FuncTTaskFuncTask_None_NoneCalled()
	{
		// Given
		var target = None<int>();
		bool someCalled = false;
		int numberPassed = -1;
		bool noneCalled = false;
		
		// When
		await target.Match(
			some: x =>
			{
				someCalled = true;
				numberPassed = x;
				return Task.CompletedTask;
			}, none: () =>
			{
				noneCalled = true;
				return Task.CompletedTask;
			});
		
		// Then
		
		// Then
		someCalled.Should().BeFalse();
		numberPassed.Should().Be(-1);
		noneCalled.Should().BeTrue();
	}

	[TestMethod]
	public async Task Match_FuncTTaskTResultFuncTaskTResult_Some_SomeCalled()
	{
		// Given
		var target = Some(5);
		
		// When
		var result = await target.Match(some: x => Task.FromResult(x + 2), none: () => Task.FromResult(-4));
		
		// Then
		result.Should().Be(7);
	}

	[TestMethod]
	public async Task Match_FuncTTaskTResultFuncTaskTResult_None_NoneCalled()
	{
		// Given
		var target = None<int>();
		
		// When
		var result = await target.Match(some: x => Task.FromResult(x * 2), none: () => Task.FromResult(-3));
		
		// Then
		result.Should().Be(-3);
	}

	[TestMethod]
	public async Task Match_FuncTTaskTResultTResult_Some_SomeCalled()
	{
		// Given
		var target = Some(17);
		
		// When
		var result = await target.Match(some: x => Task.FromResult("Hi"), none: "Hello");
		
		// Then
		result.Should().Be("Hi");
	}

	[TestMethod]
	public async Task Match_FuncTTaskTResultTResult_None_NoneCalled()
	{
		// Given
		var target = None<int>();
		
		// When
		var result = await target.Match(some: x => Task.FromResult("Hi"), none: "Hello");
		
		// Then
		result.Should().Be("Hello");
	}

	[TestMethod]
	public async Task GetValueOrDefault_FuncTaskT_Some_GetsValue()
	{
		// Given
		var target = Some(123);
		
		// When
		var result = await target.GetValueOrDefault(defaultValue: () => Task.FromResult(3));
		
		// Then
		result.Should().Be(123);
	}

	[TestMethod]
	public async Task GetValueOrDefault_FuncTaskT_None_GetsDefault()
	{
		// Given
		var target = None<int>();
		
		// When
		var result = await target.GetValueOrDefault(defaultValue: () => Task.FromResult(37));
		
		// Then
		result.Should().Be(37);
	}

	[TestMethod]
	public async Task GetValueOrDefault_FuncValueTaskT_Some_GetsValue()
	{
		// Given
		var target = Some(123);
		
		// When
		var result = await target.GetValueOrDefault(defaultValue: () => ValueTask.FromResult(3));
		
		// Then
		result.Should().Be(123);
	}

	[TestMethod]
	public async Task GetValueOrDefault_FuncValueTaskT_None_GetsDefault()
	{
		// Given
		var target = None<int>();
		
		// When
		var result = await target.GetValueOrDefault(defaultValue: () => ValueTask.FromResult(37));
		
		// Then
		result.Should().Be(37);
	}

	[TestMethod]
	public void GetValueOrThrow_Some_GetsValue()
	{
		// Given
		var target = Some(5);
		var action = () => target.GetValueOrThrow("message");
		
		// When
		var result = action.Should().NotThrow().Which;
		
		// Then
		result.Should().Be(5);
	}

	[TestMethod]
	public void GetValueOrThrow_None_Throws()
	{
		// Given
		var target = None<int>();
		var action = () => target.GetValueOrThrow("message");
		
		// When
		var result = action.Should().Throw<Exception>().Which;
		
		// Then
		result.Message.Should().Contain("message");
	}

	[TestMethod]
	public void GetValueOrThrow_None_NoMessagePassed_ThrowsWithADefaultMessage()
	{
		// Given
		var target = None<int>();
		var action = () => target.GetValueOrThrow();
		
		// When
		var result = action.Should().Throw<Exception>().Which;
		
		// Then
		result.Message.Should().NotBeNullOrWhiteSpace();
	}

	[TestMethod]
	public void ToString_Some_GetsSomeToString()
	{
		// Given
		var target = Some(5);
		
		// When
		var result = target.ToString();
		
		// Then
		result.Should().Be("5");
	}

	[TestMethod]
	public void ToString_None_ReturnsNonePlusTypeString()
	{
		// Given
		var target = None<int>();
		
		// When
		var result = target.ToString();
		
		// Then
		result.Should().Be("None Int32");
	}

	[TestMethod]
	public void GetHashCode_EqualObjects_ReturnsTheSameHashCode()
	{
		// Given
		var left = Some(13);
		var right = Some(13);
		
		// When
		var equals = left.Equals(right);
		var leftHashCode = left.GetHashCode();
		var rightHashCode = right.GetHashCode();
		
		// Then
		equals.Should().BeTrue();
		leftHashCode.Should().Be(rightHashCode);
	}

	[TestMethod]
	public void GetHashCode_EqualObjectsButDifferentType_ReturnsDifferentHashCodes()
	{
		// Given
		var left = Some<string>("Hi");
		var right = Some<object>("Hi");
		
		// When
        // ReSharper disable once SuspiciousTypeConversion.Global
        var equals = left.Equals(right);
		var leftHashCode = left.GetHashCode();
		var rightHashCode = right.GetHashCode();
		
		// Then
		equals.Should().BeFalse();
		leftHashCode.Should().NotBe(rightHashCode);
	}

	[TestMethod]
	public void GetHashCode_DifferentValuesWithSameType_ReturnsDifferentHashCodes()
	{
		// Given
		var left = Some("Hi");
		var right = Some("Hello");
		
		// When
		var equals = left.Equals(right);
		var leftHashCode = left.GetHashCode();
		var rightHashCode = right.GetHashCode();
		
		// Then
		equals.Should().BeFalse();
		leftHashCode.Should().NotBe(rightHashCode);
	}
	
	[TestMethod]
	public void NullCoalescingWithOptionBoolBehavesAsExpected()
	{
		bool? foo = null;

		var implicitTypedOption = foo ?? Option<bool>.None;
		implicitTypedOption.GetType().Should().Be(typeof(Option<bool>));

		Option<bool> option = foo ?? Option<bool>.None;
		option.Equals(Option<bool>.None).Should().BeTrue();
	}

	[TestMethod]
	public void ToOptionForNullable()
	{
		var none = ((bool?)null).ToOption();
		none.Equals(Option<bool>.None).Should().BeTrue();

		var some = ((bool?)false).ToOption();
		some.Equals(Some(false)).Should().BeTrue();

		var nullableNull = none.ToNullable();
		nullableNull.Should().BeNull();

		var nullableWithValue = some.ToNullable();
		nullableWithValue.Should().Be(false);
	}

	[TestMethod]
	public void NullCoalescingWithResultBoolBehavesAsExpected()
	{
		bool? foo = null;

		var result = foo ?? Result<bool>.Error("Value is missing");
		if (result)
		{

		}
		result.Should().BeError().Subject.Should().Be("Value is missing");
	}

	[TestMethod]
	public void QueryExpressionSelect()
	{
		Option<int> subject = 42;
		var result =
			from r in subject
			select r + 3;
		result.Should().BeSome()
			.Subject.Should().Be(45);
	}

	[TestMethod]
	public void QueryExpressionSelectMany()
	{
		Option<int> some = 42;
		var none = Option.None<int>();

		(
			from r in some
			from r1 in none
			select r1
		).Should().BeNone();

		(
			from r in none
			from r1 in some
			select r1
		).Should().BeNone();

		(
			from r in some
			let x = r * 2
			from r1 in some
			select x
		).Should().BeEquivalentTo(some.Map(r => r * 2));
	}

	[TestMethod]
	public async Task QueryExpressionSelectManyAsync()
	{
		Task<Option<int>> someAsync = Task.FromResult(Option.Some(42));
		var noneAsync = Task.FromResult(Option.None<int>());
		var some = Option.Some(1);

		(await (
			from r in someAsync
			from r1 in noneAsync
			select r1
		)).Should().BeEquivalentTo(await noneAsync);

		(await (
			from r in noneAsync
			from r1 in someAsync
			select r1
		)).Should().BeEquivalentTo(await noneAsync);

		(await (
			from r in someAsync
			let x = r * 2
			from r1 in someAsync
			select x
		)).Should().BeEquivalentTo(await someAsync.Map(r => r * 2));

		(await (
			from r in some
			let x = r * 2
			from r1 in someAsync
			select x
		)).Should().BeEquivalentTo(some.Map(r => r * 2));

		(await (
			from r in someAsync
			let x = r * 2
			from r1 in some
			select x
		)).Should().BeEquivalentTo(await someAsync.Map(r => r * 2));
	}

	[TestMethod]
	public void ShouldHaveDifferentHashCodeIfBothValuesAreDifferent()
	{
		var hashcode1 = Some(1).GetHashCode();
		var hashcode2 = Some(2).GetHashCode();
		hashcode1.Should().NotBe(hashcode2);
	}

    [TestMethod]
    public void ShouldNotBeEqualIfValuesDifferent()
    {
        var some1 = Some(1);
        var some2 = Some(2);
        some1.Equals(some2).Should().BeFalse();
        (some1 == some2).Should().BeFalse();
        (some1 != some2).Should().BeTrue();
    }
	
	[TestMethod]
	public void ShouldHaveSameHashCodeIfBothValueAreSame()
	{
		var hashcode1 = Some(1).GetHashCode();
		var hashcode2 = Some(1).GetHashCode();
		hashcode1.Should().Be(hashcode2);
	}
	
	[TestMethod]
	public void ShouldHaveDifferentHashCodeIfOneIsNone()
	{
		var hashcode1 = Some(1).GetHashCode();
		var hashcode2 = None<int>().GetHashCode();
		hashcode1.Should().NotBe(hashcode2);
	}
	
	[TestMethod]
	public void ShouldBeEqualIfBothValuesAreEqual()
	{
		var some1 = Some(1);
		var some2 = Some(1);
		some1.Equals(some2).Should().BeTrue();
        (some1 == some2).Should().BeTrue();
        (some1 != some2).Should().BeFalse();
    }
	
	[TestMethod]
	public void ShouldNotBeEqualIfOneIsNone()
	{
		var some = Some(1);
		var none = None<int>();
		some.Equals(none).Should().BeFalse();
	}

    [TestMethod]
    public void ShouldBeEqualIfBothAreNoneOfSameType()
    {
        None<int>().Equals(None<int>()).Should().BeTrue();
    }

    [TestMethod]
    public void ShouldHaveSameHashCodeIfBothAreNoneOfSameTypes()
    {
        None<string>().GetHashCode().Equals(None<string>().GetHashCode()).Should().BeTrue();
    }

    [TestMethod]
    public void ShouldNotBeEqualIfBothAreNoneOfDifferentTypes()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        None<int>().Equals(None<string>()).Should().BeFalse();
    }

    [TestMethod]
    public void ShouldNotHaveSameHashCodeIfBothAreNoneOfDifferentTypes()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        None<int>().GetHashCode().Equals(None<string>().GetHashCode()).Should().BeFalse();
    }

    [TestMethod]
    public void NullOptionsWork()
    {
        var nullOption = Option<MyClass?>.Some(null);
        var nullOption2 = Option<MyOtherClass?>.Some(null);
		nullOption.GetHashCode().Should().NotBe(nullOption2.GetHashCode());
        nullOption.Equals(Option<MyClass?>.Some(new())).Should().BeFalse();
        // ReSharper disable once SuspiciousTypeConversion.Global
        nullOption.Equals(nullOption2).Should().BeFalse();
        nullOption.Equals(Option<MyClass?>.Some(null)).Should().BeTrue();
    }

    [TestMethod]
    public void IntermediateNoneOptionIsFine()
    {
        GetTuple().Should().BeNone();
        return;

        Option<(int x, int y)> GetTuple() => None();
    }

    [TestMethod]
    public void Flatten_SomeSome_Some()
    {
	    var optionOfOption = Some(Some(5));
	    var flattened = optionOfOption.Flatten();
	    flattened.Should().BeSome().Which.Should().Be(5);
    }

    [TestMethod]
    public void Flatten_SomeNone_None()
    {
	    var optionOfNone = Some(None<int>());
	    var flattened = optionOfNone.Flatten();
	    flattened.Should().BeNone();
    }

    [TestMethod]
    public void Flatten_None_None()
    {
	    var none = Option.None<Option<int>>();
	    var flattened = none.Flatten();
	    flattened.Should().BeNone();
    }

    [TestMethod]
    public void As_Matches_IsExtracted()
    {
	    object boxedString = "Hi";
	    var asString = boxedString.As<string>();
	    asString.Should().BeSome().Which.Should().Be("Hi");
    }

    [TestMethod]
    public void As_DoesNotMatch_None()
    {
	    object boxedString = "Hi";
	    var asList = boxedString.As<List<int>>();
	    asList.Should().BeNone();
    }

    [TestMethod]
    public async Task Match_TaskOptionT_FuncTTout_FuncTOut_Some_Matched()
    {
	    var target = Task.FromResult(Some(6));
	    var matched = await target.Match(
		    some: number => number * 2,
		    none: () => 3);
	    matched.Should().Be(12);
    }

    [TestMethod]
    public async Task Match_TaskOptionT_FuncTTout_FuncTOut_None_Matched()
    {
	    var target = Task.FromResult(None<int>());
	    var matched = await target.Match(
		    some: number => number * 2,
		    none: () => 3);
	    matched.Should().Be(3);
    }

    [TestMethod]
    public async Task Map_TaskOptionT_FuncTTaskTOut_Some_Mapped()
    {
	    var target = Task.FromResult(Some(13));
	    var mapped = await target.Map(number => Task.FromResult(number + 5));
	    mapped.Should().BeSome().Which.Should().Be(18);
    }

    [TestMethod]
    public async Task Map_TaskOptionT_FuncTTaskTOut_None_Mapped()
    {
	    var target = Task.FromResult(None<int>());
	    var mapped = await target.Map(number => Task.FromResult(number + 5));
	    mapped.Should().BeNone();
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTOptionTOut_Some_BoundToSome()
    {
	    var target = Task.FromResult(Some(89));
	    var bound = await target.Bind(number => Some(number + 1));
	    bound.Should().BeSome().Which.Should().Be(90);
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTOptionTOut_Some_BoundToNone()
    {
	    var target = Task.FromResult(Some(89));
	    var bound = await target.Bind(number => None<int>());
	    bound.Should().BeNone();
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTOptionTOut_None_BoundToSome()
    {
	    var target = Task.FromResult(None<int>());
	    var bound = await target.Bind(number => Some(number + 1));
	    bound.Should().BeNone();
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTOptionTOut_None_BoundToNone()
    {
	    var target = Task.FromResult(None<int>());
	    var bound = await target.Bind(_ => None<int>());
	    bound.Should().BeNone();
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTTaskOptionTOut_Some_BoundToSome()
    {
	    var target = Task.FromResult(Some(14));
	    var bound = await target.Bind(number => Task.FromResult(Some(number / 2)));
	    bound.Should().BeSome().Which.Should().Be(7);
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTTaskOptionTOut_Some_BoundToNone()
    {
	    var target = Task.FromResult(Some(14));
	    var bound = await target.Bind(_ => Task.FromResult(None<int>()));
	    bound.Should().BeNone();
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTTaskOptionTOut_None_BoundToSome()
    {
	    var target = Task.FromResult(None<int>());
	    var bound = await target.Bind(number => Task.FromResult(Some(number / 2)));
	    bound.Should().BeNone();
    }

    [TestMethod]
    public async Task Bind_TaskOptionT_FuncTTaskOptionTOut_None_BoundToNone()
    {
	    var target = Task.FromResult(None<int>());
	    var bound = await target.Bind(_ => Task.FromResult(None<int>()));
	    bound.Should().BeNone();
    }

    [TestMethod]
    public void Choose_PartiallySome_ExpectedCollectionIsReturned()
    {
	    var target = Enumerable.Range(0, 10);
	    var odds = target.Choose(i => i % 2 != 0 ? i * 10 : Option<int>.None).ToList();
	    odds.Should().Equal(10, 30, 50, 70, 90);
    }

    [TestMethod]
    public void ToOption_HasValueFunc_Class()
    {
	    string? nullTarget = null;
        // ReSharper disable once VariableCanBeNotNullable
        string? valueTarget = "Hi";

	    nullTarget.ToOption(x => x == "Hi").Should().BeNone();
	    nullTarget.ToOption(x => x == "Hi").Should().BeNone();
	    valueTarget.ToOption(x => x == "Hello").Should().BeNone();
	    valueTarget.ToOption(x => x == "Hi").Should().BeSome().Which.Should().Be("Hi");
    }

    [TestMethod]
    public void ToOption_HasValueFunc_Struct()
    {
	    int? nullTarget = null;
	    int? valueTarget = 8;

	    nullTarget.ToOption(x => x == 8).Should().BeNone();
	    nullTarget.ToOption(x => x == 8).Should().BeNone();
	    valueTarget.ToOption(x => x == 5).Should().BeNone();
	    valueTarget.ToOption(x => x == 8).Should().BeSome().Which.Should().Be(8);
    }

    [TestMethod]
    public void NoneIfEmpty_Null_None()
    {
	    string? target = null;
	    var result = target.NoneIfEmpty();
	    result.Should().BeNone();
    }

    [TestMethod]
    public void NoneIfEmpty_Empty_None()
    {
        // ReSharper disable once VariableCanBeNotNullable
        string? target = "";
	    var result = target.NoneIfEmpty();
	    result.Should().BeNone();
    }

    [TestMethod]
    public void NoneIfEmpty_Text_Some()
    {
        // ReSharper disable once VariableCanBeNotNullable
        string? target = "Hi";
	    var result = target.NoneIfEmpty();
	    result.Should().BeSome().Which.Should().Be("Hi");
    }

    [TestMethod]
    public void WhereSome_CorrectCollectionReturned()
    {
	    IEnumerable<Option<int>> target = [1, None(), 2, None(), 3];
	    var result = target.WhereSome();
	    result.Should().Equal([1, 2, 3]);
    }

    class MyClass;

    class MyOtherClass;
}