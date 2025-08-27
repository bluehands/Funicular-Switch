namespace FunicularSwitch.Generators.Transformer;

internal record MethodGenerationInfo(
    string ReturnType,
    IReadOnlyList<string> TypeParameters,
    IReadOnlyList<ParameterGenerationInfo> Parameters,
    string Name,
    MethodBody Body)
{
    public MethodGenerationInfo(string ReturnType,
        IReadOnlyList<string> TypeParameters,
        IReadOnlyList<ParameterGenerationInfo> Parameters,
        MethodInfo info) : this(
        ReturnType,
        TypeParameters,
        Parameters,
        info.Name,
        info.Invoke(TypeParameters, Parameters.Select(x => x.Name).ToList()))
    {
    }
}