using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Generators.Common;

/// <summary>
/// Detects C# 15 language features (closed hierarchies, native unions) available to the compilation being processed.
/// </summary>
public static class LanguageFeatures
{
    public const string UnionAttributeMetadataName = "System.Runtime.CompilerServices.UnionAttribute";

    /// <summary>True if the compilation is C# 15 or newer. The compiler synthesizes the closed attribute if the runtime lacks it, so only the language version matters.</summary>
    public static bool SupportsClosed(Compilation compilation) => IsCSharp15OrNewer(compilation);

    /// <summary>True if the compilation is C# 15 or newer and the runtime provides <c>UnionAttribute</c>.</summary>
    public static bool SupportsUnions(Compilation compilation) =>
        IsCSharp15OrNewer(compilation) && compilation.GetTypeByMetadataName(UnionAttributeMetadataName) != null;

    public static bool IsCSharp15OrNewer(Compilation compilation) =>
        compilation is CSharpCompilation { LanguageVersion: var v } && v >= LanguageVersion.CSharp15 && v != LanguageVersion.Default;

    /// <summary>Works for source and metadata symbols.</summary>
    public static bool IsClosed(ITypeSymbol type) => type.IsClosed;

    public static bool IsUnion(ITypeSymbol type) => type.IsUnion;
}
