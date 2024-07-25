namespace FunicularSwitch.Generators.Templates;

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