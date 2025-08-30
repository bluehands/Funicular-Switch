namespace FunicularSwitch.Generators.Transformer;

internal record TransformMonadInfo(
    string Namespace,
    string FullTypeName,
    ConstructType FullGenericType,
    MonadInfo Monad,
    GenericMonadGenerationInfo? GenericMonadGenerationInfo,
    StaticMonadGenerationInfo StaticMonadGenerationInfo);
