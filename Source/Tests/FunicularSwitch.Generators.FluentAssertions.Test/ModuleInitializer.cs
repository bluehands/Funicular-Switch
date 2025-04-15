using System.Runtime.CompilerServices;

namespace FunicularSwitch.Generators.FluentAssertions.Test;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}