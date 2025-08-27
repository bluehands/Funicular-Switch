namespace FunicularSwitch.Generators.Transformer;

internal record ParameterGenerationInfo(
    string Type,
    string Name,
    bool IsExtension = false);