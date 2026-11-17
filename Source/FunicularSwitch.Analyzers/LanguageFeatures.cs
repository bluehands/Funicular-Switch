using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Analyzers;

/// <summary>Language feature checks and union helpers shared by the analyzers.</summary>
public static class LanguageFeatures
{
    public const string UnionTypeAttributeName = "FunicularSwitch.Generators.UnionTypeAttribute";
    private const LanguageVersion CSharp15 = (LanguageVersion)1500; // enum values are major*100 (CSharp14 = 1400)

    /// <summary>True if the compilation uses C# 15 or preview, i.e. the <c>closed</c> modifier is available.</summary>
    /// <remarks>
    /// Only the language version is checked: the compiler synthesizes <c>ClosedAttribute</c> when it is
    /// not part of the reference assemblies, so its presence can't be required.
    /// </remarks>
    public static bool SupportsClosed(Compilation compilation) =>
        compilation is CSharpCompilation csharp
        && csharp.LanguageVersion.MapSpecifiedToEffectiveVersion() >= CSharp15;

    /// <summary>
    /// True for <c>closed</c> types. Uses the public Roslyn 5.12 <c>ITypeSymbol.IsClosed</c>, which works for
    /// source and metadata symbols; falls back to a <c>ClosedAttribute</c> check.
    /// </summary>
    public static bool IsClosed(INamedTypeSymbol type) =>
        type.IsClosed
        || type.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == "System.Runtime.CompilerServices.ClosedAttribute");

    public static bool HasUnionTypeAttribute(INamedTypeSymbol type) =>
        type.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == UnionTypeAttributeName);

    /// <summary>True if <paramref name="type"/> or one of its base types is a [UnionType] that is closed.</summary>
    public static bool IsInClosedUnion(INamedTypeSymbol type)
    {
        for (var t = type; t != null; t = t.BaseType)
        {
            if (IsClosed(t) && HasUnionTypeAttribute(t)) return true;
        }
        return false;
    }

    /// <summary>All named types declared in the compilation's own assembly (including nested), keyed by original base type.</summary>
    public static ILookup<ISymbol, INamedTypeSymbol> CreateDerivedTypeLookup(Compilation compilation)
    {
        var all = new List<INamedTypeSymbol>();
        Collect(compilation.Assembly.GlobalNamespace, all);
        return all
            .Where(t => t.BaseType != null)
            .ToLookup(t => (ISymbol)t.BaseType!.OriginalDefinition, SymbolEqualityComparer.Default);

        static void Collect(INamespaceOrTypeSymbol container, List<INamedTypeSymbol> result)
        {
            foreach (var member in container.GetMembers())
            {
                switch (member)
                {
                    case INamespaceSymbol ns: Collect(ns, result); break;
                    case INamedTypeSymbol t: result.Add(t); Collect(t, result); break;
                }
            }
        }
    }

    /// <summary>
    /// The compiler rule for closed bases: every type parameter of a direct derived type must appear
    /// in its base type specification.
    /// </summary>
    public static bool SatisfiesClosedGenericRule(INamedTypeSymbol derived) =>
        derived.TypeParameters.All(p => derived.BaseType != null && Mentions(derived.BaseType, p));

    private static bool Mentions(ITypeSymbol type, ITypeParameterSymbol parameter) => type switch
    {
        ITypeParameterSymbol p => SymbolEqualityComparer.Default.Equals(p, parameter),
        IArrayTypeSymbol a => Mentions(a.ElementType, parameter),
        INamedTypeSymbol n => n.TypeArguments.Any(a => Mentions(a, parameter)) || (n.ContainingType != null && Mentions(n.ContainingType, parameter)),
        _ => false
    };
}


