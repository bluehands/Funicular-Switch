namespace FunicularSwitch.Generators.Transformer;

internal record ParameterGenerationInfo(
    TypeInfo Type,
    string Name,
    bool IsExtension = false);
