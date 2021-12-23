using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

static class Helpers
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
        return tokens.Any(t => { return t.Text == token; });
    }
}