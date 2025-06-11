namespace FunicularSwitch.Generators.AwesomeAssertions.Templates;

internal class GenerateAssertionsForTemplates
{
    public static string MyResultAssertionExtensions =>
        Resources.ReadResource($"MyAssertionExtensions_Result.cs");
    public static string MyResultAssertions => Resources.ReadResource($"MyAssertions_Result.cs");
    public static string MyUnionTypeAssertionExtensions =>
        Resources.ReadResource($"MyAssertionExtensions_UnionType.cs");
    public static string MyUnionTypeAssertions => Resources.ReadResource($"MyAssertions_UnionType.cs");
    public static string MyDerivedUnionTypeAssertions => Resources.ReadResource("MyUnionTypeAssertions_DerivedUnionType.cs");
    public static string OptionAssertions => Resources.ReadResource("OptionAssertions.cs");
    public static string OptionAssertionExtensions => Resources.ReadResource("OptionAssertionExtensions.cs");
    public static string GenerateExtensionsForInternalTypesAttribute => Resources.ReadResource("GenerateExtensionsForInternalTypesAttribute.cs");
}

internal static class Resources
{
    private static readonly string Namespace = $"{typeof(GenerateAssertionsForTemplates).Namespace}";

    public static string ReadResource(string filename)
    {
        var resourcePath = $"{Namespace}.{filename}";
        using var stream = typeof(GenerateAssertionsForTemplates).Assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }
}