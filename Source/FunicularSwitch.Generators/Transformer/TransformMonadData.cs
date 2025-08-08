namespace FunicularSwitch.Generators.Transformer;

internal record TransformMonadData(
    string Namespace,
    string Modifier,
    string TypeName,
    string TypeNameWithTypeParameters,
    string TypeParameter,
    string FullTypeName,
    Func<string, string> FullGenericType,
    bool IsRecord,
    string TransformerTypeName,
    MonadData InnerMonad,
    MonadData OuterMonad);