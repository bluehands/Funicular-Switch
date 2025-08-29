using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal record StaticMonadGenerationInfo(
    string TypeName,
    Accessibility Accessibility,
    IReadOnlyList<MonadImplementationGenerationInfo> MonadsWithoutImplementation,
    IReadOnlyList<MethodGenerationInfo> Methods);
