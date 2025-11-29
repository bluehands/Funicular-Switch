using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal record GenericMonadGenerationInfo(
    Accessibility Accessibility,
    string Modifier,
    string TypeName,
    IReadOnlyList<string> TypeParameters,
    string TypeNameWithTypeParameters,
    bool IsRecord,
    MonadInfo Monad);
