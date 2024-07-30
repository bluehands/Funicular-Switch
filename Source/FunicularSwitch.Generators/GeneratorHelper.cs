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

    public static T GetEnumNamedArgument<T>(this AttributeData attributeData, string name, T defaultValue) where T : struct
    {
        foreach (var kv in attributeData.NamedArguments)
        {
            if (kv.Key != name)
                continue;

            return (T)(object)((int)kv.Value.Value!);
        }

        return defaultValue;
    }

    public static T GetNamedArgument<T>(this AttributeData attributeData, string name, T defaultValue)
    {
        foreach (var kv in attributeData.NamedArguments)
        {
            if (kv.Key != name)
                continue;

            return (T)kv.Value.Value!;
        }

        return defaultValue;
    }
}