using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.Common;

public static class RoslynExtensions
{
    public static string? GetContainingNamespace(this SyntaxNode node)
    {
        var current = node;
        do
        {
            if (current is NamespaceDeclarationSyntax n)
                return n.Name.ToString();
            if (current is FileScopedNamespaceDeclarationSyntax f)
                return f.Name.ToString();
            current = current.Parent;
        } while (current != null);

        return null;
    }

    public static bool IsTypeDeclarationWithAttributes(this SyntaxNode syntaxNode) =>
        syntaxNode is BaseTypeDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };

    public static bool IsAssemblyAttribute(this SyntaxNode s) =>
	    s is AttributeSyntax
	    {
		    Parent: AttributeListSyntax l
	    } && (l.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword)).GetValueOrDefault();

    public static bool IsAnyKeyWord(this string identifier) =>
        SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetContextualKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetReservedKeywordKinds().Contains(SyntaxFactory.ParseToken(identifier).Kind());


    public static bool InheritsFrom(this INamedTypeSymbol symbol, ITypeSymbol type)
    {
        var baseType = symbol.BaseType;
        while (baseType != null)
        {
            if (type.Equals(baseType, SymbolEqualityComparer.Default))
                return true;

            baseType = baseType.BaseType;
        }

        return false;
    }

    public static bool Implements(this INamedTypeSymbol symbol, ITypeSymbol interfaceType)
        => symbol.Interfaces.Any(i => interfaceType.Equals(i, SymbolEqualityComparer.Default));

    public static string? GetFullNamespace(this INamedTypeSymbol namedType)
    {
        var parentNamespaces = new List<string>();
        var current = namedType.ContainingNamespace;
        while (!current?.IsGlobalNamespace ?? false)
        {
            // ReSharper disable once RedundantSuppressNullableWarningExpression
            parentNamespaces.Add(current!.Name);
            current = current.ContainingNamespace;
        }

        if (parentNamespaces.Count == 0)
            return null;

        parentNamespaces.Reverse();
        return parentNamespaces.ToSeparatedString(".");
    }

    static readonly SymbolDisplayFormat s_FullTypeWithNamespaceDisplayFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);
    static readonly SymbolDisplayFormat s_FullTypeDisplayFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes);


    public static string FullTypeNameWithNamespace(this INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.ToDisplayString(s_FullTypeWithNamespaceDisplayFormat);
    public static string FullTypeName(this INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.ToDisplayString(s_FullTypeDisplayFormat);

    public static string FullNamespace(this INamespaceSymbol namespaceSymbol) =>
        namespaceSymbol.ToDisplayString(s_FullTypeDisplayFormat);

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

    public static string? GetAssemblyAttributeName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        string? attributeFullName = null;
        var typeInfo = semanticModel.GetTypeInfo(attributeSyntax);
        if (typeInfo.Type is not null)
        {
            return typeInfo.Type.Name;
        }
        else
        {
            return attributeSyntax.GetAttributeFullName(semanticModel);
        }
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

    public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamespaceOrTypeSymbol root)
    {
        foreach (var symbol in root.GetMembers())
        {
            switch (symbol)
            {
                case INamespaceSymbol namespaceSymbol:
                    foreach (var nested in GetAllTypes(namespaceSymbol))
                    {
                        yield return nested;
                    }
                    break;
                case INamedTypeSymbol typeSymbol:
                    yield return typeSymbol;
                    foreach (var nested in GetAllTypes(typeSymbol))
                    {
                        yield return nested;
                    }
                    break;
            }
        }
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