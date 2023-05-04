namespace FunicularSwitch.Generators.Consumer;

public class EnumSpecs
{
    [UnionType]
    public enum PlatformIdentifier
    {
        DeveloperMachine,
        LinuxDevice1,
        LinuxTft,
        LinuxBox,
        LinuxBox2,
        LinuxLed,
        WindowsXp,
        WindowsDevice1,
        WindowsDevice2,
    }
    
    private bool IsGraphicalLinux(PlatformIdentifier platformIdentifier)
    {
        return platformIdentifier.Match(
            () => false,
            () => false,
            () => true,
            () => false,
            () => true,
            () => true,
            () => false,
            () => false,
            () => false);
    }
}