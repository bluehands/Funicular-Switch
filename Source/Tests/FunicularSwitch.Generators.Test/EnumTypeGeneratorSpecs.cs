using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_enum_match_method_generator : VerifySourceGenerator
{
	[TestMethod]
	public Task For_enum_type()
	{
		var code = @"
using FunicularSwitch.Generators;

[assembly: ExtendEnums(typeof(FunicularSwitch.Test.test), CaseOrder = EnumCaseOrder.Alphabetic, Accessibility = ExtensionAccessibility.Internal)]
[assembly: ExtendEnum(typeof(FunicularSwitch.Test.test), CaseOrder = EnumCaseOrder.AsDeclared, Accessibility = ExtensionAccessibility.Internal)]

namespace FunicularSwitch.Test;

[ExtendedEnum]
public enum test {
	one,
	two
}";

		return Verify(code);
	}
	
	
	[TestMethod]
	public Task For_enum_type_with_order()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

public class OtherAttribute : System.Attribute
{
}

[Other]
[ExtendedEnum(CaseOrder = EnumCaseOrder.Alphabetic)]
public enum test {
	one,
	two
}";

		return Verify(code);
	}
	
	[TestMethod]
	public Task For_enum_type_embedded()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

public class Outer {

[ExtendedEnum]
public enum test {
	one,
	two
}
}
";
		return Verify(code);
	}
}