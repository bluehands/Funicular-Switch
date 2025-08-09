using System.Text;
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
        var typeModifier = DetermineTypeModifier(transformedMonadSymbol);
        var accessModifier = DetermineAccessModifier(transformedMonadSymbol);
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
            innerMonadData.Data,
            outerMonadData.Data,
            DetermineReturnName(outerMonadData, innerMonadData),
            DetermineBindName(outerMonadData, innerMonadData));

        string FullGenericType(string typeParameter) => $"{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }

    private static string DetermineAccessModifier(INamedTypeSymbol type) =>
        type.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
        };

    static string DetermineTypeModifier(INamedTypeSymbol type)
    {
        var modifiers = new List<string>();
            
        if(type.IsReadOnly)
            modifiers.Add("readonly");

        modifiers.Add("partial");
            
        if(type.IsRecord)
            modifiers.Add("record");
            
        if(!type.IsReferenceType)
            modifiers.Add("struct");
        else if(!type.IsRecord)
            modifiers.Add("class");
            
        return string.Join(" ", modifiers);
    }

    static string DetermineReturnName(MonadData2 outerMonad, MonadData2 innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.ReturnMethodName, "Return");

    static string DetermineBindName(MonadData2 outerMonad, MonadData2 innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.BindMethodName, "Bind");

    static string DetermineMethodName(MonadData2 outerMonad, MonadData2 innerMonad, Func<MonadData2, string> selector, string defaultName)
    {
        var outerMonadReturn = selector(outerMonad);
        var innerMonadReturn = selector(innerMonad);

        if (outerMonadReturn == innerMonadReturn)
            return outerMonadReturn;
        
        if(outerMonadReturn == defaultName)
            return innerMonadReturn;

        if (innerMonadReturn == defaultName)
            return outerMonadReturn;

        return $"{outerMonadReturn}{innerMonadReturn}";
    }
    
    static MonadData2 ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType)
    {
        var staticMonadType = (INamedTypeSymbol)transformerType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var monadData = ResolveMonadDataFromStaticMonadType(staticMonadType);
        return monadData;
    }

    static MonadData2 ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
    {
        var monadAttribute = staticMonadType.GetAttributes().FirstOrDefault(x => x.AttributeClass?.Name == "MonadAttribute");
        if (monadAttribute is not null)
        {
            var genericMonadType = (INamedTypeSymbol) staticMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
            return CreateMonadData(staticMonadType, genericMonadType);
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
            return CreateMonadData(staticMonadType, genericMonadType, returnMethod, bindMethod);
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

    private static MonadData2 CreateMonadData(INamedTypeSymbol staticType, INamedTypeSymbol genericType, IMethodSymbol? returnMethod = default, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = genericType.FullTypeNameWithNamespace();
        var (returnMethodName, returnMethodFullName) = GetMethodFullName(returnMethod, "Return");
        var (bindMethodName, bindMethodFullName) = GetMethodFullName(bindMethod, "Bind");

        return new MonadData2(
            new MonadData(
                typeParameter => $"{genericTypeName}<{typeParameter}>",
                returnMethodFullName,
                bindMethodFullName),
            returnMethodName,
            bindMethodName);
        
        (string Name, string FullName) GetMethodFullName(IMethodSymbol? method, string defaultName) =>
            method is not null
                ? (method.Name, $"{method.ContainingType.FullTypeNameWithNamespace()}.{method.Name}")
                : (defaultName, $"{staticType}.{defaultName}");
    }
}

internal record MonadData2(
    MonadData Data,
    string ReturnMethodName,
    string BindMethodName);

internal record MonadData(
    Func<string, string> GenericTypeName,
    string ReturnMethodFullName,
    string BindMethodFullName);