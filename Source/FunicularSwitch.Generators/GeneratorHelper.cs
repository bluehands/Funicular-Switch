using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

static class GeneratorHelper
{
    public static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, string expectedAttributeName)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        var hasAttribute = false;
        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var semanticModel = context.SemanticModel;
                var attributeFullName = attributeSyntax.GetAttributeFullName(semanticModel);
                if (attributeFullName != expectedAttributeName) continue;
                hasAttribute = true;
                goto Return;
            }
        }

        Return:
        return hasAttribute ? classDeclarationSyntax : null;
    }
}