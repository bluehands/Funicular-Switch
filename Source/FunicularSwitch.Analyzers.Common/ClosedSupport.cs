using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Common;

/// <summary>
/// Single definition of "can this compilation use <c>closed</c>?", linked into analyzers and generators.
/// Verified against Roslyn 5.12: the compiler does NOT synthesize the support types, <c>closed</c> in a compilation
/// without them fails with CS0656 (netstandard2.0, net10.0 and older), even with LangVersion 15.
/// </summary>
internal static class ClosedSupport
{
    private const LanguageVersion CSharp15 = (LanguageVersion)1500; // enum values are major*100 (CSharp14 = 1400)

    public static bool IsCSharp15OrNewer(Compilation compilation) =>
        compilation is CSharpCompilation csharp && csharp.LanguageVersion.MapSpecifiedToEffectiveVersion() >= CSharp15;

    /// <summary>C# 15 (or preview) AND the types the compiler requires for closed types are available (referenced or declared in source).</summary>
    public static bool SupportsClosed(Compilation compilation) =>
        IsCSharp15OrNewer(compilation)
        && compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.IsClosedTypeAttribute") != null
        && compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute") != null;

    /// <summary>Works for source and metadata symbols (Roslyn 5.12 public API).</summary>
    public static bool IsClosed(ITypeSymbol type) => type.IsClosed;
}
