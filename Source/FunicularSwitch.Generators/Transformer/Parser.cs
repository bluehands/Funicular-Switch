using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Parser
{
    public static GenerationResult<TransformMonadData> GetTransformedMonadSchema(
        INamedTypeSymbol transformedMonadSymbol,
        AttributeData transformMonadAttribute,
        CancellationToken cancellationToken)
    {
        var typeModifier = transformedMonadSymbol.IsReadOnly
            ? "readonly partial record struct"
            : "partial record";
        
        var transformerType = (INamedTypeSymbol)transformMonadAttribute.ConstructorArguments[1].Value!;
        
        var innerMonadType = (INamedTypeSymbol)transformerType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var innerMonadGenericType = (INamedTypeSymbol)innerMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var innerMonadData = MonadData.From(innerMonadType, innerMonadGenericType);

        var outerMonadType = (INamedTypeSymbol)transformMonadAttribute.ConstructorArguments[0].Value!;
        var outerMonadGenericType = (INamedTypeSymbol)outerMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var outerMonadData = MonadData.From(outerMonadType, outerMonadGenericType);

        var typeParameter = transformedMonadSymbol.TypeArguments[0].Name;
        
        return new TransformMonadData(
            transformedMonadSymbol.GetFullNamespace()!,
            typeModifier,
            transformedMonadSymbol.Name,
            $"{transformedMonadSymbol.Name}<{typeParameter}>",
            typeParameter,
            transformedMonadSymbol.FullTypeNameWithNamespace(),
            FullGenericType,
            transformerType.FullTypeNameWithNamespace(),
            innerMonadData,
            outerMonadData);

        string FullGenericType(string typeParameter) => $"{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }
}

internal record MonadData(
    string StaticTypeName,
    Func<string, string> GenericTypeName)
{
    public static MonadData From(INamedTypeSymbol staticType, INamedTypeSymbol genericType)
    {
        var genericTypeName = genericType.FullTypeNameWithNamespace();
        return new MonadData(staticType.FullTypeNameWithNamespace(),
            typeParameter => $"{genericTypeName}<{typeParameter}>");
    }
}