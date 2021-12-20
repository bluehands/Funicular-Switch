namespace FunicularSwitch.Generators.Templates;

static class Templates
{
    static readonly string s_Namespace = $"{typeof(Templates).Namespace}";

    public static string ResultType => ReadResource("ResultType.cs");
    public static string ResultTypeWithMerge => ReadResource("ResultTypeWithMerge.cs");

    static string ReadResource(string filename)
    {
        var resourcePath = $"{s_Namespace}.{filename}";
        using var stream = typeof(Templates).Assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }
}