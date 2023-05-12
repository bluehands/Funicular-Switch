using FluentAssertions.Primitives;
using FunicularSwitch.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: ExtendEnumTypes(typeof(FluentAssertions.AtLeast))]

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
	    var x = TimeSpanCondition.Within;

	    var p = PlatformIdentifier.DeveloperMachine;
        
        var isGraphicalLinux = p.Match(
            () => true,
            () => false,
            () => true
        );
        
        Assert.IsTrue(isGraphicalLinux);
    }
    
    [TestMethod]
    public void EnumSwitchWorks()
    {
        var p = PlatformIdentifier.DeveloperMachine;

        p.Switch(
            () => { },
            Assert.Fail,
            Assert.Fail
        );
    }
}