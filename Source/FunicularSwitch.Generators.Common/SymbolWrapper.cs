using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Common;

public static class SymbolWrapper
{
    internal static readonly SymbolDisplayFormat FullTypeWithNamespaceDisplayFormat = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);
    internal static readonly SymbolDisplayFormat FullTypeWithNamespaceAndGenericsDisplayFormat = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included);

    public static SymbolWrapper<T> Create<T>(T symbol) where T : ISymbol => new(symbol);
}

public class SymbolWrapper<T> : IEquatable<SymbolWrapper<T>> where T : ISymbol
{
    public SymbolWrapper(T symbol)
    {
        Symbol = symbol;
        FullNameWithNamespace = symbol.ToDisplayString(SymbolWrapper.FullTypeWithNamespaceDisplayFormat);
    }

    public string FullNameWithNamespace { get; }
    public T Symbol { get; }

    public bool Equals(SymbolWrapper<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FullNameWithNamespace == other.FullNameWithNamespace;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SymbolWrapper<T>)obj);
    }

    public override int GetHashCode() => FullNameWithNamespace.GetHashCode();
}