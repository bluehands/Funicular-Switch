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
            .Aggregate(new MonadDataWithSymbolEx(outerMonadData, []), Combine);

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
            transformedMonadData.Data.Data,
            transformedMonadData.MonadsWithoutImplementation);

        string FullGenericType(string typeParameter) => $"global::{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }

    private static MonadDataWithSymbolEx Combine(MonadDataWithSymbolEx outerMonadWithSymbolEx, INamedTypeSymbol transformerType)
    {
        var innerMonadWithSymbol = ResolveMonadDataFromTransformerType(transformerType);
        var outerMonad = outerMonadWithSymbolEx.Data.Data;
        var innerMonad = innerMonadWithSymbol.Data;
        var implementations =
            ImplementsMonadInterface(outerMonadWithSymbolEx.Data.Symbol)
                ? outerMonadWithSymbolEx.MonadsWithoutImplementation
                :
                [
                    ..outerMonadWithSymbolEx.MonadsWithoutImplementation,
                    outerMonad,
                ];

        Func<string, string> genericTypeName = t => outerMonad.GenericTypeName(innerMonad.GenericTypeName(t));
        Func<string, string, string> returnMethodInvoke = (t, x) => outerMonad.ReturnMethod.Invoke([innerMonad.GenericTypeName(t)], [innerMonad.ReturnMethod.Invoke([t], [x])]);
        Func<string, string, string, string, string> bindMethodInvoke = (ta, tb, ma, fn) => $"global::{transformerType.FullTypeNameWithNamespace()}.Bind<{ta}, {tb}, {genericTypeName(ta)}, {genericTypeName(tb)}>({ma}, x => {fn}(x), {outerMonad.ReturnMethod.Construct([innerMonad.GenericTypeName(tb)])}, {outerMonad.BindMethod.Construct([innerMonad.GenericTypeName(ta), innerMonad.GenericTypeName(tb)])})";

        var returnMethodName = DetermineReturnName(outerMonad, innerMonad);
        var bindMethodName = DetermineBindName(outerMonad, innerMonad);

        var returnMethodInfo = new MethodInfo(
            returnMethodName,
            t => $"x => {returnMethodInvoke(t[0], "x")}",
            (t, p) => returnMethodInvoke(t[0], p[0]));
        var bindMethodInfo = new MethodInfo(
            bindMethodName,
            t => $"(ma, fn) => {bindMethodInvoke(t[0], t[1], "ma", "fn")}",
            (t, p) => bindMethodInvoke(t[0], t[1], p[0], p[1]));

        return new(
            new MonadDataWithSymbol(
                new MonadData(
                    genericTypeName,
                    returnMethodInfo,
                    bindMethodInfo),
                innerMonadWithSymbol.Symbol),
            implementations);
    }

    private static MonadDataWithSymbol CreateMonadData(INamedTypeSymbol? staticType, INamedTypeSymbol genericType, IMethodSymbol returnMethod, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = $"global::{genericType.FullTypeNameWithNamespace()}";
        var (returnMethodName, returnMethodFunc, returnMethodInvoke) = GetReturnMethod();
        var (bindMethodName, bindMethodFunc, bindMethodInvoke) = GetBindMethod();

        var returnMethodInfo = new MethodInfo(
            returnMethodName,
            t => returnMethodFunc(t[0]),
            (t, p) => returnMethodInvoke(t[0], p[0]));
        var bindMethodInfo = new MethodInfo(
            bindMethodName,
            t => bindMethodFunc(t[0], t[1]),
            (t, p) => bindMethodInvoke(t[0], t[1], p[0], p[1]));

        return new MonadDataWithSymbol(
            new MonadData(
                typeParameter => $"{genericTypeName}<{typeParameter}>",
                returnMethodInfo,
                bindMethodInfo),
            genericType);

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

    private static string DetermineBindName(MonadData outerMonad, MonadData innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.BindMethod, "Bind");

    private static string DetermineMethodName(MonadData outerMonad, MonadData innerMonad, Func<MonadData, MethodInfo> selector, string defaultName)
    {
        var outerMonadReturn = selector(outerMonad).Name;
        var innerMonadReturn = selector(innerMonad).Name;

        if (outerMonadReturn == innerMonadReturn)
            return outerMonadReturn;

        if (outerMonadReturn == defaultName)
            return innerMonadReturn;

        if (innerMonadReturn == defaultName)
            return outerMonadReturn;

        return $"{outerMonadReturn}{innerMonadReturn}";
    }

    private static string DetermineReturnName(MonadData outerMonad, MonadData innerMonad) =>
        DetermineMethodName(outerMonad, innerMonad, x => x.ReturnMethod, "Return");

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

    private static bool ImplementsMonadInterface(INamedTypeSymbol type) => type.Interfaces.Any(x => x.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.Monad");

    private static MonadDataWithSymbol ResolveMonadDataFromGenericMonadType(INamedTypeSymbol genericMonadType)
    {
        var transformMonadAttribute = genericMonadType
            .GetAttributes()
            .FirstOrDefault(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.TransformMonadAttribute");
        if (transformMonadAttribute is not null)
        {
            var transformedMonadData = GetTransformedMonadSchema(genericMonadType, TransformMonadAttribute.From(transformMonadAttribute), CancellationToken.None).Value!;
            var returnMethodInfo = new MethodInfo(
                transformedMonadData.Monad.ReturnMethod.Name,
                t => $"global::{transformedMonadData.FullTypeName}.{transformedMonadData.Monad.ReturnMethod.Name}",
                (t, p) => $"global::{transformedMonadData.FullTypeName}.{transformedMonadData.Monad.ReturnMethod.Name}({p[0]})");
            var bindMethodInfo = new MethodInfo(
                transformedMonadData.Monad.BindMethod.Name,
                t => $"(ma, fn) => ma.{transformedMonadData.Monad.BindMethod.Name}(fn)",
                (t, p) => $"{p[0]}.{transformedMonadData.Monad.BindMethod.Name}({p[1]})");
            return new MonadDataWithSymbol(
                new MonadData(
                    transformedMonadData.FullGenericType,
                    returnMethodInfo,
                    bindMethodInfo),
                genericMonadType);
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

    private static MonadDataWithSymbol ResolveMonadDataFromMonadType(INamedTypeSymbol monadType)
    {
        if (monadType.GetAttributes().Any(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.ResultTypeAttribute"))
            return ResolveMonadDataFromResultType(monadType);
        if (monadType.IsUnboundGenericType)
            return ResolveMonadDataFromGenericMonadType(monadType);
        return ResolveMonadDataFromStaticMonadType(monadType);
    }

    private static MonadDataWithSymbol ResolveMonadDataFromResultType(INamedTypeSymbol resultType)
    {
        Func<string, string> genericTypeName = t => $"global::{resultType.FullTypeNameWithNamespace()}<{t}>";
        var returnMethod = new MethodInfo(
            "Ok",
            t => $"{genericTypeName(t[0])}.Ok",
            (t, p) => $"{genericTypeName(t[0])}.Ok({p[0]})");
        var bindMethod = new MethodInfo(
            "Bind",
            t => "(ma, fn) => ma.Bind(fn)",
            (t, p) => $"{p[0]}.Bind({p[1]})");
        return new MonadDataWithSymbol(
            new MonadData(
                genericTypeName,
                returnMethod,
                bindMethod),
            resultType);
    }

    private static MonadDataWithSymbol ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
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

    private static MonadDataWithSymbol ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType)
    {
        var monadTransformerAttribute = MonadTransformerAttribute.From(transformerType.GetAttributes()[0]);
        var staticMonadType = monadTransformerAttribute.MonadType;
        var monadData = ResolveMonadDataFromMonadType(staticMonadType);
        return monadData;
    }
}

internal record MonadData(
    Func<string, string> GenericTypeName,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod);

internal record MethodInfo(
    string Name,
    ConstructDelegate Construct,
    InvokeMethod Invoke);

internal record MonadDataWithSymbol(
    MonadData Data,
    INamedTypeSymbol Symbol);

internal record MonadDataWithSymbolEx(
    MonadDataWithSymbol Data,
    IReadOnlyList<MonadData> MonadsWithoutImplementation);

internal delegate string ConstructDelegate(IReadOnlyList<string> typeParameters);

internal delegate string InvokeMethod(IReadOnlyList<string> typeParameters, IReadOnlyList<string> parameters);
