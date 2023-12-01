using System.Runtime.CompilerServices;
using VerifyTests;

namespace FunicularSwitch.Generators.Test;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}