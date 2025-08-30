namespace FunicularSwitch.Generators.Transformer;

internal record MonadInfo(
    ConstructType GenericTypeName,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod,
    bool ImplementsMonadInterface = false);
