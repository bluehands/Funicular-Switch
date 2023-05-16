using FunicularSwitch.Generators;
using FluentAssertions.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: ExtendEnumTypes(typeof(FluentAssertions.AtLeast), CaseOrder = EnumCaseOrder.Alphabetic, Accessibility = ExtensionAccessibility.Internal)]
[assembly: ExtendEnumTypes]

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class EnumSpecs
{
	[EnumType(CaseOrder = EnumCaseOrder.Alphabetic)]
	public enum PlatformIdentifier
	{
		LinuxDevice,
		DeveloperMachine,
		WindowsDevice
	}

	[TestMethod]
	public void EnumMatchWorks()
	{
		var i = RowMatchMode.Index;

		var xy = 2;

		var p = PlatformIdentifier.DeveloperMachine;

		var isGraphicalLinux = p.Match(
			() => false,
			() => true,
			() => true
		);

		Assert.IsFalse(isGraphicalLinux);
	}

	[TestMethod]
	public void EnumSwitchWorks()
	{
		var p = PlatformIdentifier.LinuxDevice;

		p.Switch(
			Assert.Fail,
			() => { },
			Assert.Fail
		);
	}
}

public class Wrapper
{
	class PrivateEnumParent
	{
		public enum HiddenEnum
		{
			One,
			Two
		}
	}
}

class InternalEnumParent
{
	public enum InternalEnum
	{
		One,
		Two
	}
}
