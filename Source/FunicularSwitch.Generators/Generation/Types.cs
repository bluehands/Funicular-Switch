using Microsoft.CodeAnalysis;

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

    public static string Func(params string[] typeParameters) => GenericType("global::System.Func", typeParameters);

    public static string Task(string typeParameter) => GenericType("global::System.Threading.Tasks.Task", typeParameter);

    private static string GenericType(string name, params string[] typeParameters) =>
        $"{name}<{string.Join(", ", typeParameters)}>";
}
