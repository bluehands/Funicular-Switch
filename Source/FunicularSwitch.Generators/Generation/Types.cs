namespace FunicularSwitch.Generators.Generation;

internal static class Types
{
    public static string Func(params string[] typeParameters) => GenericType("global::System.Func", typeParameters);

    public static string Task(string typeParameter) => GenericType("global::System.Threading.Tasks.Task", typeParameter);

    private static string GenericType(string name, params string[] typeParameters) =>
        $"{name}<{string.Join(", ", typeParameters)}>";
}
