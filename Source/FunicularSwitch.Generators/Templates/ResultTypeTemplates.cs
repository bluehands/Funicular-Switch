namespace FunicularSwitch.Generators.Templates;

static class ResultTypeTemplates
{
    public static string ResultType => Resources.ReadResource("ResultType.cs");
    public static string ResultTypeWithMerge => Resources.ReadResource("ResultTypeWithMerge.cs");
    public static string StaticCode => Resources.ReadResource("ResultTypeAttributes.cs");
    public static string FunicularTypes => Resources.ReadResource("FunicularTypes.cs");
}

static class Resources
{
    static readonly string s_Namespace = $"{typeof(ResultTypeTemplates).Namespace}";

    public static string ReadResource(string filename)
    {
        var resourcePath = $"{s_Namespace}.{filename}";
        using var stream = typeof(ResultTypeTemplates).Assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }
}