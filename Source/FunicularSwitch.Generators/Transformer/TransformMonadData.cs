namespace FunicularSwitch.Generators.Transformer;

internal record TransformMonadData(
    string Namespace,
    string AccessModifier,
    string Modifier,
    string TypeName,
    string TypeNameWithTypeParameters,
    string TypeParameter,
    string FullTypeName,
    Func<string, string> FullGenericType,
    bool IsRecord,
    MonadData Monad,
    StaticMonadGenerationInfo StaticMonadGenerationInfo);
