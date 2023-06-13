using System;
using FluentAssertions;
using FunicularSwitch.Generators;
using FluentAssertions.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: ExtendEnums(typeof(FluentAssertions.AtLeast), CaseOrder = EnumCaseOrder.Alphabetic, Accessibility = ExtensionAccessibility.Internal)]
[assembly: ExtendEnums]
[assembly: ExtendEnum(typeof(DateTimeKind), CaseOrder = EnumCaseOrder.Alphabetic)]

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class EnumSpecs
{
	[ExtendedEnum(CaseOrder = EnumCaseOrder.Alphabetic)] //direct EnumType attribute should have higher precedence compared to ExtendEnumTypes attribute,
													 //so case oder should be Alphabetic for Match methods of PlatformIdentifier
	public enum PlatformIdentifier
	{
		LinuxDevice,
		DeveloperMachine,
		WindowsDevice
	}

	[TestMethod]
	public void EnumMatchWorks()
	{
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

	[TestMethod]
	public void MatchMethodsForSingleTypeInReferencedAssemblyWorks()
	{
		var kindName = DateTimeKind.Local.Match(
			() => "local",
			() => "unspecified",
			() => "utc"
		);

		kindName.Should().Be("local");
	}

	[TestMethod]
	public void MatchMethodsForAllTypesInReferencedAssemblyWorks()
	{
		var matchMode = RowMatchMode.PrimaryKey.Match(
			() => "index",
			() => "primaryKey"
		);

		matchMode.Should().Be("primaryKey");
	}
}

//this one is here to make sure no match method is generated for non accessibly enum type
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

//this one is here to make sure accessibility for nested types is handled correctly
class InternalEnumParent
{
	public enum InternalEnum
	{
		One,
		Two
	}
}

//no match methods should be generated here
public enum DuplicateValues
{
	One = 1,
	Two = 1
}
