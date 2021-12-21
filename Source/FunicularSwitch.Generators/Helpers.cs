using Microsoft.CodeAnalysis;
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
}