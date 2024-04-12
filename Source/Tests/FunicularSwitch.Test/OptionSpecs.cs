using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunicularSwitch.Option;

namespace FunicularSwitch.Test;

[TestClass]
public class OptionSpecs
{
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

    class MyClass;

    class MyOtherClass;
}