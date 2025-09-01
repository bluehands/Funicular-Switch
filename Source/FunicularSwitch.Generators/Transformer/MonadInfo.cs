namespace FunicularSwitch.Generators.Transformer;

internal record MonadInfo(
    ConstructType GenericTypeName,
    int ExtraArity,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod,
    bool ImplementsMonadInterface = false);
