using System.Collections.Immutable;
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
        var outerMonadData = ResolveMonadDataFromMonadType(outerMonadType);
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
        var monadData = ResolveMonadDataFromMonadType(staticMonadType);
        return monadData;
    }

    static MonadData2 ResolveMonadDataFromMonadType(INamedTypeSymbol monadType)
    {
        if (monadType.GetAttributes().Any(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.ResultTypeAttribute"))
            return ResolveMonadDataFromResultType(monadType);
        if (monadType.IsUnboundGenericType)
            return ResolveMonadDataFromGenericMonadType(monadType);
        return ResolveMonadDataFromStaticMonadType(monadType);
    }

    static MonadData2 ResolveMonadDataFromResultType(INamedTypeSymbol resultType)
    {
        Func<string, string> genericTypeName = t => $"{resultType.FullTypeNameWithNamespace()}<{t}>";
        Func<string, string> returnMethodFunc = t => $"{genericTypeName(t)}.Ok";
        Func<string, string, string> bindMethodInvoke = (ma, fn) => $"{ma}.Bind({fn})";
        return new MonadData2(
            new MonadData(
                genericTypeName,
                returnMethodFunc,
                (t, a) => $"{returnMethodFunc(t)}({a})",
                $"(ma, fn) => {bindMethodInvoke("ma", "fn")}",
                bindMethodInvoke),
            "Ok",
            "Bind");
    }

    static MonadData2 ResolveMonadDataFromGenericMonadType(INamedTypeSymbol genericMonadType)
    {
        var returnMethod = genericMonadType.OriginalDefinition
            .GetMembers()
            .OfType<IMethodSymbol>()
            .First(IsReturnMethod);

        return CreateMonadData(default, genericMonadType, returnMethod, default);

        bool IsReturnMethod(IMethodSymbol method)
        {
            if (method.TypeParameters.Length != 0) return false;
            if (method.Parameters.Length != 1) return false;
            if (method.ReturnType is not INamedTypeSymbol { IsGenericType: true, TypeArguments.Length: 1 } genericReturnType) return false;
            if (!SymbolEqualityComparer.IncludeNullability.Equals(genericReturnType.ConstructUnboundGenericType(), genericMonadType)) return false;
            if (genericReturnType.TypeArguments[0].Name != genericMonadType.OriginalDefinition.TypeParameters[0].Name) return false;
            return true;
        }
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

    private static MonadData2 CreateMonadData(INamedTypeSymbol? staticType, INamedTypeSymbol genericType, IMethodSymbol? returnMethod = default, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = genericType.FullTypeNameWithNamespace();
        var (returnMethodName, returnMethodFunc, returnMethodInvoke) = GetReturnMethod();
        var (bindMethodName, bindMethodFunc, bindMethodInvoke) = GetBindMethod();

        return new MonadData2(
            new MonadData(
                typeParameter => $"{genericTypeName}<{typeParameter}>",
                returnMethodFunc,
                returnMethodInvoke,
                bindMethodFunc,
                bindMethodInvoke),
            returnMethodName,
            bindMethodName);
        
        (string Name, string FullName) GetMethodFullName(IMethodSymbol? method, string defaultName) =>
            method is not null
                ? (method.Name, $"{method.ContainingType.FullTypeNameWithNamespace()}.{method.Name}")
                : (defaultName, $"{staticType}.{defaultName}");

        (string Name, string Func, Func<string, string, string> Invoke) GetBindMethod()
        {
            if (staticType is not null)
            {
                var (name, fullName) = GetMethodFullName(bindMethod, "Bind");
                return (name, fullName, (ma, fn) => $"{fullName}({ma}, {fn})");
            }
            else
            {
                var name = "Bind";
                var invoke = (string ma, string fn) => $"{ma}.Bind({fn})";
                var func = $"(ma, fn) => {invoke("ma", "fn")}";
                return (name, func, invoke);
            }
        }

        (string Name, Func<string, string> Func, Func<string, string, string> Invoke) GetReturnMethod()
        {
            if (returnMethod is null)
            {
                var func = (string _) => $"{staticType}.Return";
                var invoke = (string _, string a) => $"{func}({a})";
                return ("Return", func, invoke);
            }
            else
            {
                var name = returnMethod.Name;
                var func = returnMethod.ContainingType.IsGenericType
                    ? new Func<string, string>((string t) => $"{returnMethod.ContainingType.FullTypeNameWithNamespace()}<{t}>.{name}")
                    : (string _) => $"{returnMethod.ContainingType.FullTypeNameWithNamespace()}.{name}";
                var invoke = (string t, string a) => $"{func(t)}.{name}({a})";
                return (name, func, invoke);
            }
        }
    }
}

internal record MonadData2(
    MonadData Data,
    string ReturnMethodName,
    string BindMethodName);

internal record MonadData(
    Func<string, string> GenericTypeName,
    Func<string, string> ReturnMethodFunc,
    Func<string, string, string> ReturnMethodInvoke,
    string BindMethodFunc,
    Func<string, string, string> BindMethodInvoke);