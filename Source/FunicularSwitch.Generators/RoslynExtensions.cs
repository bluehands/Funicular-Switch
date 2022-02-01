using System.Collections.Immutable;
using FunicularSwitch.Generators.ResultType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

public static class RoslynExtensions
{
    public static string GetContainingNamespace(this SyntaxNode node)
    {
        var current = node;
        do
        {
            if (current is NamespaceDeclarationSyntax n)
                return n.Name.ToFullString();
            if (current is FileScopedNamespaceDeclarationSyntax f)
                return f.Name.ToFullString();
            current = current.Parent;
        } while (current != null);

        throw new InvalidOperationException($"No containing namespace found for node {node}");
    }

    public static bool IsTypeDeclarationWithAttributes(this SyntaxNode syntaxNode) =>
        syntaxNode is BaseTypeDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };

    public static bool IsAnyKeyWord(this string identifier) =>
        SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetContextualKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetReservedKeywordKinds().Contains(SyntaxFactory.ParseToken(identifier).Kind());


    public static bool InheritsFrom(this INamedTypeSymbol symbol, ITypeSymbol type)
    {
        var baseType = symbol.BaseType;
        while (baseType != null)
        {
            if (type.Equals(baseType))
                return true;

            baseType = baseType.BaseType;
        }

        return false;
    }

    public static string GetFullNamespace(this INamedTypeSymbol namedType)
    {
        var parentNamespaces = new List<string>();
        var current = namedType.ContainingNamespace;
        do
        {
            parentNamespaces.Add(current!.Name);
            current = current.ContainingNamespace;
        } while (!string.IsNullOrEmpty(current?.Name));

        parentNamespaces.Reverse();
        return parentNamespaces.ToSeparatedString(".");
    }


    public static QualifiedTypeName QualifiedName(this BaseTypeDeclarationSyntax dec)
    {
        var current = dec.Parent as BaseTypeDeclarationSyntax;
        var typeNames = new Stack<string>();
        while (current != null)
        {
            typeNames.Push(current.Name());
            current = current.Parent as BaseTypeDeclarationSyntax;
        }

        return new(dec.Name(), typeNames);
    }


    public static string Name(this BaseTypeDeclarationSyntax declaration) => declaration.Identifier.ToString();

    public static string Name(this MethodDeclarationSyntax declaration) => declaration.Identifier.ToString();

    public static string Name(this TypeSyntax typeSyntax) => typeSyntax.ToString();

    public static string Name(this PropertyDeclarationSyntax syntax) => syntax.Identifier.ToString();

    public static string? GetAttributeFullName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        string? attributeFullName = null;
        var symbol = ModelExtensions.GetSymbolInfo(semanticModel, attributeSyntax).Symbol;
        if (symbol is IMethodSymbol attributeSymbol)
        {
            var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
            attributeFullName = attributeContainingTypeSymbol.ToDisplayString();
        }

        return attributeFullName;
    }

    public static bool HasModifier(this SyntaxTokenList tokens, SyntaxKind syntaxKind)
    {
        var token = SyntaxFactory.Token(syntaxKind).Text;
        return tokens.Any(t => t.Text == token);
    }

    public static string GetFullTypeName(this Compilation compilation, SyntaxNode typeSyntax)
    {
        var semanticModel = compilation.GetSemanticModel(typeSyntax.SyntaxTree);
        return GetFullTypeName(semanticModel, typeSyntax);
    }

    public static string GetFullTypeName(this SemanticModel semanticModel, SyntaxNode typeSyntax)
    {
        var typeInfo = semanticModel.GetTypeInfo(typeSyntax);
        return typeInfo.Type?.ToDisplayString(
            new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
            )
        ) ?? typeSyntax.ToString();
    }
}

public sealed class QualifiedTypeName : IEquatable<QualifiedTypeName>
{
    public static QualifiedTypeName NoParents(string name) => new QualifiedTypeName(name, Enumerable.Empty<string>());

    readonly string m_FullName;
    public ImmutableArray<string> NestingParents { get; }
    public string Name { get; }

    public string QualifiedName(string separator = ".") => NestingParents.Length > 0 ? $"{NestingParents.ToSeparatedString(separator)}{separator}{Name}" : Name;

    public QualifiedTypeName(string name, IEnumerable<string> nestingParents)
    {
        Name = name;
        NestingParents = nestingParents.ToImmutableArray();
        m_FullName = QualifiedName(".");
    }

    public override string ToString() => m_FullName;

    public bool Equals(QualifiedTypeName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return m_FullName == other.m_FullName;
    }

    public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is QualifiedTypeName other && Equals(other);

    public override int GetHashCode() => m_FullName.GetHashCode();

    public static bool operator ==(QualifiedTypeName left, QualifiedTypeName right) => Equals(left, right);

    public static bool operator !=(QualifiedTypeName left, QualifiedTypeName right) => !Equals(left, right);
}