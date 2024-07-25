namespace FunicularSwitch.Generators.Templates;

static class ResultTypeTemplates
{
    public static string ResultType => Resources.ReadResource("ResultType.cs");
    public static string ResultTypeWithMerge => Resources.ReadResource("ResultTypeWithMerge.cs");
    public static string StaticCode => Resources.ReadResource("ResultTypeAttributes.cs");
}