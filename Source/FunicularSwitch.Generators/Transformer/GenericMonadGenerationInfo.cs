using System;

namespace FunicularSwitch.Generators.Transformer;

internal record GenericMonadGenerationInfo(
    string AccessModifier,
    string Modifier,
    string TypeName,
    string TypeParameter,
    string TypeNameWithTypeParameters,
    bool IsRecord,
    MonadData Monad);
