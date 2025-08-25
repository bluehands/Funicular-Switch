using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Parser
{
    public static GenerationResult<TransformMonadData> GetTransformedMonadSchema(
        INamedTypeSymbol transformedMonadSymbol,
        TransformMonadAttribute transformMonadAttribute,
        CancellationToken cancellationToken)
    {
        var typeModifier = DetermineTypeModifier(transformedMonadSymbol);
        var accessModifier = DetermineAccessModifier(transformedMonadSymbol);
        var isRecord = transformedMonadSymbol.IsRecord;
        var outerMonadType = transformMonadAttribute.MonadType;
        var outerMonadData = ResolveMonadDataFromMonadType(outerMonadType);
        var transformedMonadData = new[] {transformMonadAttribute.TransformerType}
            .Concat(transformMonadAttribute.ExtraTransformerTypes)
            .Aggregate(outerMonadData, Combine);

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
            outerMonadData.Data,
            transformedMonadData.ReturnMethodName,
            transformedMonadData.BindMethodName,
            transformedMonadData.Data);

        string FullGenericType(string typeParameter) => $"global::{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }

    private static MonadData2 Combine(MonadData2 outerMonad, INamedTypeSymbol transformerType)
    {
        var innerMonad = ResolveMonadDataFromTransformerType(transformerType);

        Func<string, string> genericTypeName = t => outerMonad.Data.GenericTypeName(innerMonad.Data.GenericTypeName(t));
        Func<string, string, string> returnMethodInvoke = (t, x) => outerMonad.Data.ReturnMethodInvoke(innerMonad.Data.GenericTypeName(t), innerMonad.Data.ReturnMethodInvoke(t, x));
        Func<string, string, string, string, string> bindMethodInvoke = (ta, tb, ma, fn) => $"global::{transformerType.FullTypeNameWithNamespace()}.Bind<{ta}, {tb}, {genericTypeName(ta)}, {genericTypeName(tb)}>({ma}, x => {fn}(x), {outerMonad.Data.ReturnMethodFunc(innerMonad.Data.GenericTypeName(tb))}, {outerMonad.Data.BindMethodFunc(innerMonad.Data.GenericTypeName(ta), innerMonad.Data.GenericTypeName(tb))})";
        return new(
            new(
                genericTypeName,
                t => $"x => {returnMethodInvoke(t, "x")}",
                returnMethodInvoke,
                (ta, tb) => $"(ma, fn) => {bindMethodInvoke(ta, tb, "ma", "fn")}",
                bindMethodInvoke),
            DetermineReturnName(outerMonad, innerMonad),
            DetermineBindName(outerMonad, innerMonad));
    }

    private static MonadData2 CreateMonadData(INamedTypeSymbol? staticType, INamedTypeSymbol genericType, IMethodSymbol returnMethod, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = $"global::{genericType.FullTypeNameWithNamespace()}";
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
                ? (method.Name, $"global::{method.ContainingType.FullTypeNameWithNamespace()}.{method.Name}")
                : (defaultName, $"global::{staticType}.{defaultName}");

        (string Name, Func<string, string, string> Func, Func<string, string, string, string, string> Invoke) GetBindMethod()
        {
            if (staticType is not null)
            {
                var (name, fullName) = GetMethodFullName(bindMethod, "Bind");
                return (name, (_, _) => fullName, (_, _, ma, fn) => $"{fullName}({ma}, {fn})");
            }
            else
            {
                var name = "Bind";
                var invoke = (string _, string _, string ma, string fn) => $"{ma}.Bind({fn})";
                var func = (string _, string _) => $"(ma, fn) => {invoke(string.Empty, string.Empty, "ma", "fn")}";
                return (name, func, invoke);
            }
        }

        (string Name, Func<string, string> Func, Func<string, string, string> Invoke) GetReturnMethod()
        {
            var name = returnMethod.Name;
            var func = returnMethod.ContainingType.IsGenericType
                ? new Func<string, string>(t => $"global::{returnMethod.ContainingType.FullTypeNameWithNamespace()}<{t}>.{name}")
                : _ => $"global::{returnMethod.ContainingType.FullTypeNameWithNamespace()}.{name}";
            var invoke = (string t, string a) => $"{func(t)}({a})";
            return (name, func, invoke);
        }
    }

    private static string DetermineAccessModifier(INamedTypeSymbol type) =>
        type.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
        };

    static string DetermineBindName(MonadData2 outerMonad, MonadData2 innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.BindMethodName, "Bind");

    static string DetermineMethodName(MonadData2 outerMonad, MonadData2 innerMonad, Func<MonadData2, string> selector, string defaultName)
    {
        var outerMonadReturn = selector(outerMonad);
        var innerMonadReturn = selector(innerMonad);

        if (outerMonadReturn == innerMonadReturn)
            return outerMonadReturn;

        if (outerMonadReturn == defaultName)
            return innerMonadReturn;

        if (innerMonadReturn == defaultName)
            return outerMonadReturn;

        return $"{outerMonadReturn}{innerMonadReturn}";
    }

    private static string DetermineReturnName(MonadData2 outerMonad, MonadData2 innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.ReturnMethodName, "Return");

    private static string DetermineTypeModifier(INamedTypeSymbol type)
    {
        var modifiers = new List<string>();

        if (type.IsReadOnly)
            modifiers.Add("readonly");

        modifiers.Add("partial");

        if (type.IsRecord)
            modifiers.Add("record");

        if (!type.IsReferenceType)
            modifiers.Add("struct");
        else if (!type.IsRecord)
            modifiers.Add("class");

        return string.Join(" ", modifiers);
    }

    private static MonadData2 ResolveMonadDataFromGenericMonadType(INamedTypeSymbol genericMonadType)
    {
        var transformMonadAttribute = genericMonadType
            .GetAttributes()
            .FirstOrDefault(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.TransformMonadAttribute");
        if (transformMonadAttribute is not null)
        {
            var transformedMonadData = GetTransformedMonadSchema(genericMonadType, TransformMonadAttribute.From(transformMonadAttribute), CancellationToken.None).Value!;
            var returnMethodFunc = $"global::{transformedMonadData.FullTypeName}.{transformedMonadData.ReturnName}";
            Func<string, string, string, string, string> bindMethodInvoke = (_, _, ma, fn) => $"{ma}.{transformedMonadData.BindName}({fn})";
            return new MonadData2(
                new MonadData(
                    transformedMonadData.FullGenericType,
                    _ => returnMethodFunc,
                    (_, a) => $"{returnMethodFunc}({a})",
                    (_, _) => $"(ma, fn) => {bindMethodInvoke(string.Empty, string.Empty, "ma", "fn")}",
                    bindMethodInvoke),
                transformedMonadData.ReturnName,
                transformedMonadData.BindName);
        }

        var returnMethod = genericMonadType.OriginalDefinition
            .GetMembers()
            .OfType<IMethodSymbol>()
            .First(IsReturnMethod);

        return CreateMonadData(default, genericMonadType, returnMethod);

        bool IsReturnMethod(IMethodSymbol method)
        {
            if (method.TypeParameters.Length != 0) return false;
            if (method.Parameters.Length != 1) return false;
            if (method.ReturnType is not INamedTypeSymbol {IsGenericType: true, TypeArguments.Length: 1} genericReturnType) return false;
            if (!SymbolEqualityComparer.IncludeNullability.Equals(genericReturnType.ConstructUnboundGenericType(), genericMonadType)) return false;
            if (genericReturnType.TypeArguments[0].Name != genericMonadType.OriginalDefinition.TypeParameters[0].Name) return false;
            return true;
        }
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
        Func<string, string> genericTypeName = t => $"global::{resultType.FullTypeNameWithNamespace()}<{t}>";
        Func<string, string> returnMethodFunc = t => $"{genericTypeName(t)}.Ok";
        Func<string, string, string, string, string> bindMethodInvoke = (_, _, ma, fn) => $"{ma}.Bind({fn})";
        return new MonadData2(
            new MonadData(
                genericTypeName,
                returnMethodFunc,
                (t, a) => $"{returnMethodFunc(t)}({a})",
                (ta, tb) => $"(ma, fn) => {bindMethodInvoke(ta, tb, "ma", "fn")}",
                bindMethodInvoke),
            "Ok",
            "Bind");
    }

    static MonadData2 ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
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
            if (method.ReturnType is not INamedTypeSymbol {IsGenericType: true, TypeArguments.Length: 1} genericReturnType) return false;
            if (!SymbolEqualityComparer.IncludeNullability.Equals(genericReturnType.ConstructUnboundGenericType(), genericMonadType)) return false;
            if (genericReturnType.TypeArguments[0].Name != method.TypeParameters[1].Name) return false;
            return true;
        }
    }

    private static MonadData2 ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType)
    {
        var monadTransformerAttribute = MonadTransformerAttribute.From(transformerType.GetAttributes()[0]);
        var staticMonadType = monadTransformerAttribute.MonadType;
        var monadData = ResolveMonadDataFromMonadType(staticMonadType);
        return monadData;
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
    Func<string, string, string> BindMethodFunc,
    Func<string, string, string, string, string> BindMethodInvoke);

internal record TypeInfo();
