using FunicularSwitch.Generators.Transformer;

namespace FunicularSwitch.Generators.ExtendMonad;

internal record ExtendMonadInfo(
    string FullTypeName,
    string Namespace,
    StaticMonadGenerationInfo StaticMonadGenerationInfo);
