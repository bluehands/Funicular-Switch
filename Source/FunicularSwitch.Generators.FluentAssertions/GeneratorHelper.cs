using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.FluentAssertions;

public static class GeneratorHelper
{
    public static AttributeSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, string expectedAttributeName)
    {
        var attributeSyntax = (AttributeSyntax)context.Node;
        var semanticModel = context.SemanticModel;
        var attributeFullName = attributeSyntax.GetAssemblyAttributeName(semanticModel);

        var isRightAttribute = attributeFullName == expectedAttributeName
            || attributeFullName == expectedAttributeName + "Attribute";
        return isRightAttribute ? attributeSyntax : null;
    }
}