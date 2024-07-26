using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

static class GeneratorHelper
{
    public static BaseTypeDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, string expectedAttributeName)
    {
        var classDeclarationSyntax = (BaseTypeDeclarationSyntax)context.Node;
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

    public static T GetNamedEnumAttributeArgument<T>(this AttributeSyntax attribute, string name, T defaultValue) where T : struct
    {
        var memberAccess = attribute.ArgumentList?.Arguments
            .Where(a => a.NameEquals?.Name.ToString() == name)
            .Select(a => a.Expression)
            .OfType<MemberAccessExpressionSyntax>()
            .FirstOrDefault();


        if (memberAccess == null) return defaultValue;

        return (T)Enum.Parse(typeof(T), memberAccess.Name.ToString());
    }
}