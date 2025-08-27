namespace FunicularSwitch.Generators.Transformer;

internal record MonadImplementationGenerationInfo(
    Func<string, string> GenericTypeName,
    MonadData Monad);