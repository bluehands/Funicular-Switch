using FunicularSwitch.Generators.Transformer;

namespace FunicularSwitch.Generators.Monad;

internal record ExtendMonadInfo(
    string FullTypeName,
    string Namespace,
    StaticMonadGenerationInfo StaticMonadGenerationInfo);
