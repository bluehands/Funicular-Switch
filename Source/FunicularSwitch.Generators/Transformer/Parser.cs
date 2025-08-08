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
            ? transformedMonadSymbol.IsRecord
                ? "readonly partial record struct"
                : "readonly partial struct"
            : transformedMonadSymbol.IsReferenceType
                ? transformedMonadSymbol.IsRecord
                    ? "partial record"
                    : "partial class"
                : transformedMonadSymbol.IsRecord
                    ? "partial record struct"
                    : "partial struct";

        var accessModifier = transformedMonadSymbol.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
        };

        var isRecord = transformedMonadSymbol.IsRecord;
        
        var outerMonadType = (INamedTypeSymbol)transformMonadAttribute.ConstructorArguments[0].Value!;
        var outerMonadData = ResolveMonadDataFromStaticMonadType(outerMonadType);
        var transformerType = (INamedTypeSymbol)transformMonadAttribute.ConstructorArguments[1].Value!;
        var innerMonadData = ResolveMonadDataFromTransformerType(transformerType);

        var typeParameter = transformedMonadSymbol.TypeArguments[0].Name;
        
        return new TransformMonadData(
            transformedMonadSymbol.GetFullNamespace()!,
            accessModifier,
            typeModifier,
            transformedMonadSymbol.Name,
            $"{transformedMonadSymbol.Name}<{typeParameter}>",
            typeParameter,
            transformedMonadSymbol.FullTypeNameWithNamespace(),
            FullGenericType,
            isRecord,
            transformerType.FullTypeNameWithNamespace(),
            innerMonadData,
            outerMonadData);

        string FullGenericType(string typeParameter) => $"{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }

    static MonadData ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType)
    {
        var staticMonadType = (INamedTypeSymbol)transformerType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var monadData = ResolveMonadDataFromStaticMonadType(staticMonadType);
        return monadData;
    }

    static MonadData ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
    {
        var genericMonadType = (INamedTypeSymbol)staticMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var monadData = MonadData.From(staticMonadType, genericMonadType);
        return monadData;
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