namespace FunicularSwitch.Generators.Transformer;

internal record TransformMonadInfo(
    string Namespace,
    string FullTypeName,
    Func<string, string> FullGenericType,
    MonadInfo Monad,
    GenericMonadGenerationInfo? GenericMonadGenerationInfo,
    StaticMonadGenerationInfo StaticMonadGenerationInfo);
