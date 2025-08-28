namespace FunicularSwitch.Generators.Transformer;

internal record MonadInfo(
    Func<string, string> GenericTypeName,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod,
    bool ImplementsMonadInterface = false);
