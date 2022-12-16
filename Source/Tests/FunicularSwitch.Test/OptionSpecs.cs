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

		var option = foo ?? Option<bool>.None;
		option.Should().BeSameAs(Option<bool>.None);
	}
}