namespace FunicularSwitch.Generators.Transformer;

internal record MonadImplementationGenerationInfo(
    Func<string, string> GenericTypeName,
    MonadInfo Monad);
