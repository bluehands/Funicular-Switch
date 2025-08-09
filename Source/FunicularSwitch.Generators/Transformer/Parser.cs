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
            outerMonadData,
            DetermineReturnName(outerMonadData, innerMonadData),
            DetermineBindName(outerMonadData, innerMonadData));

        string FullGenericType(string typeParameter) => $"{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }

    static string DetermineReturnName(MonadData outerMonad, MonadData innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.ReturnMethod, "Return");

    static string DetermineBindName(MonadData outerMonad, MonadData innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.BindMethod, "Bind");

    static string DetermineMethodName(MonadData outerMonad, MonadData innerMonad, Func<MonadData, IMethodSymbol?> selector, string defaultName)
    {
        var outerMonadReturn = ReturnName(outerMonad);
        var innerMonadReturn = ReturnName(innerMonad);

        if (outerMonadReturn == innerMonadReturn)
            return outerMonadReturn;
        
        if(outerMonadReturn == defaultName)
            return innerMonadReturn;

        if (innerMonadReturn == defaultName)
            return outerMonadReturn;

        return $"{outerMonadReturn}{innerMonadReturn}";
        
        string ReturnName(MonadData data) =>
            selector(data) is {} method
                ? method.Name
                : defaultName;
    }
    
    static MonadData ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType)
    {
        var staticMonadType = (INamedTypeSymbol)transformerType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var monadData = ResolveMonadDataFromStaticMonadType(staticMonadType);
        return monadData;
    }

    static MonadData ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
    {
        var monadAttribute = staticMonadType.GetAttributes().FirstOrDefault(x => x.AttributeClass?.Name == "MonadAttribute");
        if (monadAttribute is not null)
        {
            var genericMonadType = (INamedTypeSymbol) staticMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
            return MonadData.From(staticMonadType, genericMonadType);
        }
        else
        {
            var returnMethod = staticMonadType
                .GetMembers()
                .OfType<IMethodSymbol>()
                .First(IsStaticReturnMethod);
            var genericMonadType = ((INamedTypeSymbol) returnMethod.ReturnType).ConstructUnboundGenericType();

            var bindMethod = staticMonadType
                .GetMembers()
                .OfType<IMethodSymbol>()
                .First(x => IsStaticBindMethod(genericMonadType, x));
            return MonadData.From(staticMonadType, genericMonadType, returnMethod, bindMethod);
        }

        bool IsStaticReturnMethod(IMethodSymbol method)
        {
            if (method.TypeParameters.Length != 1) return false;
            if (method.Parameters.Length != 1) return false;
            if (!IsGenericMonadType(method.ReturnType, method.TypeParameters[0])) return false;
            return method.Parameters[0].Type.Name == method.TypeParameters[0].Name;
        }

        bool IsGenericMonadType(ITypeSymbol type, ITypeParameterSymbol typeParameter)
        {
            if (type is not INamedTypeSymbol {IsGenericType: true, TypeParameters.Length: 1} genericType) return false;
            return genericType.TypeArguments[0].Name == typeParameter.Name;
        }

        bool IsStaticBindMethod(INamedTypeSymbol genericMonadType, IMethodSymbol method)
        {
            if (method.TypeParameters.Length != 2) return false;
            if (method.Parameters.Length != 2) return false;
            if (method.ReturnType is not INamedTypeSymbol { IsGenericType: true, TypeArguments.Length: 1 } genericReturnType) return false;
            if (!SymbolEqualityComparer.IncludeNullability.Equals(genericReturnType.ConstructUnboundGenericType(), genericMonadType)) return false;
            if (genericReturnType.TypeArguments[0].Name != method.TypeParameters[1].Name) return false; 
            return true;
        }
    }
}

internal record MonadData(
    string StaticTypeName,
    Func<string, string> GenericTypeName,
    IMethodSymbol? ReturnMethod,
    IMethodSymbol? BindMethod)
{
    public static MonadData From(INamedTypeSymbol staticType, INamedTypeSymbol genericType, IMethodSymbol? returnMethod = default, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = genericType.FullTypeNameWithNamespace();
        return new MonadData(staticType.FullTypeNameWithNamespace(),
            typeParameter => $"{genericTypeName}<{typeParameter}>",
            returnMethod,
            bindMethod);
    }
}