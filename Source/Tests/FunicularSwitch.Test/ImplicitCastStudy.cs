using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class ImplicitCastStudy  
{
	[TestMethod]
	[Ignore] //this test fails. The implicit case is never called here due to a legacy compiler behaviour regarding Nullable<T> 
	public void ImplicitCastWithNullableStruct()
	{
		long? l = null;
		Option<long> converted = l;
		converted.Should().NotBeNull();
	}

	[TestMethod]
	public void ImplicitCastWithClass()
	{
		object? l = null;
		Option<object> converted = l;
		converted.Should().NotBeNull();
	}

	[TestMethod]
	public void ImplicitCastWithNullableStructToOptionOfNullable()
	{
		long? l = null;
		Option<long?> converted = l;
		converted.Should().NotBeNull();
	}
}