using Microsoft.CodeAnalysis;
using TypeInfo = FunicularSwitch.Generators.Transformer.TypeInfo;

namespace FunicularSwitch.Generators.Generation;

internal static class Types
{
    // TODO: should probably be moved to another type
    public static string DetermineAccessModifier(Accessibility accessibility) =>
        accessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            _ => throw new ArgumentOutOfRangeException(),
        };

    public static TypeInfo Func(params TypeInfo[] typeParameters) =>
        TypeInfo.FullType("System.Func", typeParameters);

    public static TypeInfo Task(TypeInfo typeParameter) =>
        TypeInfo.FullType("System.Threading.Tasks.Task", [typeParameter]);

    public static TypeInfo ValueTask(TypeInfo typeParameter) =>
        TypeInfo.FullType("System.Threading.Tasks.ValueTask", [typeParameter]);
}
