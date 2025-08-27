namespace FunicularSwitch.Generators.Transformer;

internal record MonadData(
    Func<string, string> GenericTypeName,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod,
    bool ImplementsMonadInterface = false);