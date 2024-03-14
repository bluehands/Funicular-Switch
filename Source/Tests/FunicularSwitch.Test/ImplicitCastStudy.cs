using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class ImplicitCastStudy  
{
// Does not compile anymore because no auto conversion is possible
// 	[TestMethod]
// 	[Ignore] //this test fails. The implicit case is never called here due to a legacy compiler behaviour regarding Nullable<T> 
// 	public void ImplicitCastWithNullableStruct()
// 	{
// 		long? l = null;
// #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//         Option<long> converted = l;
// #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//         converted.Should().NotBeNull();
// 	}

	[TestMethod]
	public void ImplicitCastWithClass()
	{
		object? l = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Option<object> converted = l;
#pragma warning restore CS8604 // Possible null reference argument.
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