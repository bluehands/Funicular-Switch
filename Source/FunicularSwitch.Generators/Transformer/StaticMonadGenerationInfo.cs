namespace FunicularSwitch.Generators.Transformer;

internal record StaticMonadGenerationInfo(
    string TypeName,
    string AccessModifier,
    IReadOnlyList<MonadImplementationGenerationInfo> MonadsWithoutImplementation,
    IReadOnlyList<MethodGenerationInfo> Methods);