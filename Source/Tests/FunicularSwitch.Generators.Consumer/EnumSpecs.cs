using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class EnumSpecs
{
    [EnumType]
    public enum PlatformIdentifier
    {
        DeveloperMachine,
        LinuxDevice,
        WindowsDevice
    }

    [TestMethod]
    public void EnumMatchWorks()
    {
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