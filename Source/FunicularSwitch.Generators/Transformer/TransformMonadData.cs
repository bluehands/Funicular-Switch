namespace FunicularSwitch.Generators.Transformer;

internal record TransformMonadData(
    string Namespace,
    string FullTypeName,
    Func<string, string> FullGenericType,
    MonadData Monad,
    GenericMonadGenerationInfo? GenericMonadGenerationInfo,
    StaticMonadGenerationInfo StaticMonadGenerationInfo);
