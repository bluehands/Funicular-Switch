using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class OptionSpecs
{
	[TestMethod]
	public void NullCoalescingWithOptionBoolBehavesAsExpected()
	{
		bool? foo = null;

		var implicitTypedOption = foo ?? Option<bool>.None;
		implicitTypedOption.GetType().Should().Be(typeof(None<bool>));

		Option<bool> option = foo ?? Option<bool>.None;
		option.Equals(Option<bool>.None).Should().BeTrue();
	}

	[TestMethod]
	public void ToOptionForNullable()
	{
		var none = ((bool?)null).ToOption();
		none.Equals(Option<bool>.None).Should().BeTrue();

		var some = ((bool?)false).ToOption();
		some.Equals(Option.Some(false)).Should().BeTrue();

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
		result.Equals(Result<bool>.Error("Value is missing")).Should().BeTrue();
	}
}