using System.Runtime.CompilerServices;

namespace FunicularSwitch.Generators.AwesomeAssertions.Test;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}