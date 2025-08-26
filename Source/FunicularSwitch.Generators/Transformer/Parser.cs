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
        return GetTransformedMonadSchema_New(transformedMonadSymbol, transformMonadAttribute, cancellationToken);
        // return transformMonadAttribute.ExtraTransformerTypes.Length > 0
        //     ? GetTransformedMonadSchema_New(transformedMonadSymbol, transformMonadAttribute, cancellationToken)
        //     : GetTransformedMonadSchema_Old(transformedMonadSymbol, transformMonadAttribute, cancellationToken);
    }
    
    public static GenerationResult<TransformMonadData> GetTransformedMonadSchema_New(
        INamedTypeSymbol transformedMonadSymbol,
        TransformMonadAttribute transformMonadAttribute,
        CancellationToken cancellationToken)
    {
        var typeModifier = DetermineTypeModifier(transformedMonadSymbol);
        var accessModifier = DetermineAccessModifier(transformedMonadSymbol);
        var isRecord = transformedMonadSymbol.IsRecord;
        var outerMonadType = transformMonadAttribute.MonadType;
        var outerMonadData = ResolveMonadDataFromMonadType(outerMonadType);

        var transformerTypes = new[] {transformMonadAttribute.TransformerType}
            .Concat(transformMonadAttribute.ExtraTransformerTypes)
            .ToList();

        var chainedMonads = transformerTypes
            .Aggregate<INamedTypeSymbol, IReadOnlyList<MonadData>>([outerMonadData.Data], (acc, cur) => [..acc, TransformMonad(acc.Last(), cur)]);
        var chainedMonad = chainedMonads.Last();

        var implementations = chainedMonads
            .Take(chainedMonads.Count - 1)
            .Select(GenerateImplementationForMonad)
            .ToList();
        
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
            transformedMonadData.MonadsWithoutImplementation,
            BuildStaticMonad(
                transformedMonadSymbol.Name,
                FullGenericType,
                accessModifier,
                implementations,
                chainedMonad,
                outerMonadData.Data,
                outerMonadData.Data // TODO: determine actual inner monad
            ));

        string FullGenericType(string typeParameter) => $"global::{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";

        static Func<string, string> ChainGenericTypeName(Func<string, string> outer, Func<string, string> inner) =>
            x => outer(inner(x));

        static MethodInfo CombineReturn(MethodInfo outer, MonadData inner) =>
            new MethodInfo(
                DetermineMethodName(outer.Name, inner.ReturnMethod.Name, "Return"),
                (t, p) => $"{outer.Invoke([inner.GenericTypeName(t[0])], [inner.ReturnMethod.Invoke(t, p)])}");

        static MethodInfo TransformBind(MonadData outer, MonadData inner, string transformerTypeName, Func<string, string> outerInterfaceImplName)
        {
            var chainedGenericType = ChainGenericTypeName(outer.GenericTypeName, inner.GenericTypeName);
            
            return new MethodInfo(
                DetermineMethodName(outer.BindMethod.Name, inner.BindMethod.Name, "Bind"),
                (t, p) =>
                {
                    var ma = $"({outerInterfaceImplName(inner.GenericTypeName(t[0]))}){p[0]}";
                    var fn = $"a => ({outerInterfaceImplName(inner.GenericTypeName(t[1]))})(new global::System.Func<{t[0]}, {chainedGenericType(t[1])}>({p[1]}).Invoke(a))"; // A -> Monad<X<B>>

                    var call = $"{transformerTypeName}.Bind<{t[0]}, {t[1]}>({ma}, {fn}).Cast<{chainedGenericType(t[1])}>()";
                    return call;
                });
        }

        static MonadData TransformMonad(MonadData outer, INamedTypeSymbol transformerType)
        {
            var innerMonad = ResolveMonadDataFromTransformerType(transformerType).Data;
            var transformerTypeName = $"global::{transformerType.FullTypeNameWithNamespace()}";
            var outerInterfaceImplementation = GenerateImplementationForMonad(outer);

            var transformedMonad = new MonadData(
                ChainGenericTypeName(outer.GenericTypeName, innerMonad.GenericTypeName),
                default,
                default,
                CombineReturn(outer.ReturnMethod, innerMonad),
                TransformBind(outer, innerMonad, transformerTypeName, outerInterfaceImplementation.GenericTypeName));
            return transformedMonad;
        }
    }
    
    public static GenerationResult<TransformMonadData> GetTransformedMonadSchema_Old(
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
            transformedMonadData.MonadsWithoutImplementation,
            null);

        string FullGenericType(string typeParameter) => $"global::{transformedMonadSymbol.FullTypeNameWithNamespace()}<{typeParameter}>";
    }

    private static StaticMonadGenerationInfo BuildStaticMonad(
        string typeName,
        Func<string, string> genericTypeName,
        string accessModifier,
        IReadOnlyList<MonadImplementationGenerationInfo> monadImplementations,
        MonadData chainedMonad,
        MonadData outerMonad,
        MonadData innerMonad)
    {
        var staticMonadInfo = new StaticMonadGenerationInfo(
            typeName,
            accessModifier,
            monadImplementations,
            BuildReturnMethod(),
            BuildBindMethod(),
            BuildLiftMethod()
        );
        return staticMonadInfo;

        MethodGenerationInfo BuildReturnMethod() => new MethodGenerationInfo(
            genericTypeName("A"),
            ["A"],
            [new("A", "a")],
            chainedMonad.ReturnMethod // implicit cast
        );

        MethodGenerationInfo BuildBindMethod() => new(
            genericTypeName("B"),
            ["A", "B"],
            [
                new(genericTypeName("A"), "ma", true),
                new($"global::System.Func<A, {genericTypeName("B")}>", "fn"),
            ],
            new(
                chainedMonad.BindMethod.Name,
                (t, p) => $"{chainedMonad.BindMethod.Invoke(t, [$"(({chainedMonad.GenericTypeName(t[0])}){p[0]})", $"a => {p[1]}(a)"])}" // implicit cast
            )
        );

        MethodGenerationInfo BuildLiftMethod() => new(
            genericTypeName("A"),
            ["A"],
            [new(outerMonad.GenericTypeName("A"), "ma")],
            new(
                "Lift",
                (t, p) => $"{outerMonad.BindMethod.Invoke([t[0], $"{innerMonad.GenericTypeName("A")}"], ["ma", $"a => {chainedMonad.ReturnMethod.Invoke(t, ["a"])}"])}" // implicit cast
            )
        );
    }

    private static MonadDataWithSymbolEx Combine(MonadDataWithSymbolEx outerMonadWithSymbolEx, INamedTypeSymbol transformerType)
    {
        var innerMonadWithSymbol = ResolveMonadDataFromTransformerType(transformerType);
        var outerMonad = outerMonadWithSymbolEx.Data.Data;
        var innerMonad = innerMonadWithSymbol.Data;

        Func<string, string> genericTypeName = t => outerMonad.GenericTypeName(innerMonad.GenericTypeName(t));
        Func<string, string, string> returnMethodInvoke = (t, x) => outerMonad.ReturnMethod.Invoke([innerMonad.GenericTypeName(t)], [innerMonad.ReturnMethod.Invoke([t], [x])]);
        InvokeMethod bindMethodInvoke = (t, p) =>
        {
            var ma = p[0];
            var fn = p[1];
            
            var outerMonadName = outerMonad.GenericTypeName("_");
            var innerMonadName = innerMonad.GenericTypeName("_");
            var outerMonadGen = GenerateImplementationForMonad(new (genericTypeName, default!, default!, default!, default!));
            // var implA = outerMonad.MonadImplementation.GenericTypeName(innerMonad.GenericTypeName(t[0]));
            // var implB = outerMonad.MonadImplementation.GenericTypeName(innerMonad.GenericTypeName(t[1]));
            // var implA = outerMonadGen.GenericTypeName(t[0]);
            // var implB = outerMonadGen.GenericTypeName(t[1]);
            var implA = outerMonad.GenericImplementationName(innerMonad.GenericTypeName(t[0]));
            var implB = outerMonad.GenericImplementationName(innerMonad.GenericTypeName(t[1]));
            // var implA = outerMonad.GenericImplementationName(t[0]);
            // var implB = outerMonad.GenericImplementationName(t[1]);
            return
                $"(({implB})global::{transformerType.FullTypeNameWithNamespace()}.Bind<{string.Join(", ", t)}>(({implA}){ma}, a => ({implB}){fn}(a).M)).M";
        };
        
        var returnMethodName = DetermineReturnName(outerMonad, innerMonad);
        var bindMethodName = DetermineBindName(outerMonad, innerMonad);

        var returnMethodInfo = new MethodInfo(
            returnMethodName,
            (t, p) => returnMethodInvoke(t[0], p[0]));
        var bindMethodInfo = new MethodInfo(
            bindMethodName,
            bindMethodInvoke);

        // var implementationInfo = GenerateImplementationForType(outerMonadWithSymbolEx.Data.Symbol);
        // var implementationInfoEx = new MonadImplementationInfoEx(
        //     implementationInfo,
        //     t => $"{implementationInfo.FullName}<{innerMonad.GenericTypeName(t)}>",
        //     genericTypeName,
        //     innerMonad.ReturnMethod,
        //     innerMonad.BindMethod);
        var monadImplementationGenerationInfo = GenerateImplementationForMonad(outerMonad);
        // var monadImplementationGenerationInfo = GenerateImplementationForMonad(new(genericTypeName, default!, default!, returnMethodInfo, bindMethodInfo));
        var implementationInfoEx = outerMonad.MonadImplementation with
        {
            GenericTypeName = t => monadImplementationGenerationInfo.GenericTypeName(innerMonad.GenericTypeName(t)),
        };

        var monadData = new MonadData(
            genericTypeName,
            t => implementationInfoEx.GenericTypeName(t),
            implementationInfoEx,
            returnMethodInfo,
            bindMethodInfo);
        return new(
            new MonadDataWithSymbol(
                monadData,
                innerMonadWithSymbol.Symbol),
            [
                ..outerMonadWithSymbolEx.MonadsWithoutImplementation,
                monadImplementationGenerationInfo,
            ]);
    }

    private static MonadDataWithSymbol CreateMonadData(INamedTypeSymbol? staticType, INamedTypeSymbol genericType, IMethodSymbol returnMethod, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = $"global::{genericType.FullTypeNameWithNamespace()}";
        var (returnMethodName, returnMethodFunc, returnMethodInvoke) = GetReturnMethod();
        var (bindMethodName, bindMethodFunc, bindMethodInvoke) = GetBindMethod();

        var returnMethodInfo = new MethodInfo(
            returnMethodName,
            (t, p) => returnMethodInvoke(t[0], p[0]));
        var bindMethodInfo = new MethodInfo(
            bindMethodName,
            (t, p) => bindMethodInvoke(t[0], t[1], p[0], p[1]));

        Func<string, string> typeName = typeParameter => $"{genericTypeName}<{typeParameter}>";
        var implementationInfo = GenerateImplementationForType(genericType);
        var monadImplementationEx = new MonadImplementationInfoEx(
            implementationInfo,
            t => $"{implementationInfo.FullName}<{t}>");
        return new MonadDataWithSymbol(
            new MonadData(
                typeName,
                monadImplementationEx.GenericTypeName,
                monadImplementationEx,
                returnMethodInfo,
                bindMethodInfo),
            staticType ?? genericType);

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
    
    private static string DetermineMethodName(string outerName, string innerName, string defaultName)
    {
        if (outerName == innerName)
            return outerName;

        if (outerName == defaultName)
            return innerName;

        if (innerName == defaultName)
            return outerName;

        return $"{outerName}{innerName}";
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
                (t, p) => $"global::{transformedMonadData.FullTypeName}.{transformedMonadData.Monad.ReturnMethod.Name}({p[0]})");
            var bindMethodInfo = new MethodInfo(
                transformedMonadData.Monad.BindMethod.Name,
                (t, p) => $"{p[0]}.{transformedMonadData.Monad.BindMethod.Name}({p[1]})");
            var monadImplementation = new MonadImplementationInfoEx(
                GenerateImplementationForType(genericMonadType),
                transformedMonadData.FullGenericType);
            return new MonadDataWithSymbol(
                new MonadData(
                    transformedMonadData.FullGenericType,
                    transformedMonadData.Monad.GenericImplementationName,
                    monadImplementation,
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
            (t, p) => $"{genericTypeName(t[0])}.Ok({p[0]})");
        var bindMethod = new MethodInfo(
            "Bind",
            (t, p) => $"{p[0]}.Bind({p[1]})");
        var implementationInfo = GenerateImplementationForType(resultType);
        var monadImplementationEx = new MonadImplementationInfoEx(
            implementationInfo,
            t => $"{implementationInfo.FullName}<{t}>");
        return new MonadDataWithSymbol(
            new MonadData(
                genericTypeName,
                monadImplementationEx.GenericTypeName,
                monadImplementationEx,
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

    private static MonadImplementationInfo GenerateImplementationForType(INamedTypeSymbol type)
    {
        var substituteName = $"Impl__{type.FullTypeNameWithNamespace().Replace('.', '_')}";
        return new MonadImplementationInfo(substituteName, substituteName);
    }

    private static MonadImplementationGenerationInfo GenerateImplementationForMonad(MonadData data)
    {
        var baseName = data.GenericTypeName("_")
            .Replace('.', '_')
            .Replace('<', '_')
            .Replace('>', '_')
            .Replace(':', '_')
            .TrimEnd('_')
            [8..];
        return new MonadImplementationGenerationInfo(
            t => $"Impl__{baseName}<{t}>",
            data);
    }
}

internal record MonadData(
    Func<string, string> GenericTypeName,
    Func<string, string> GenericImplementationName,
    MonadImplementationInfoEx MonadImplementation,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod);

internal record MethodInfo(
    string Name,
    InvokeMethod Invoke);

internal record MonadDataWithSymbol(
    MonadData Data,
    INamedTypeSymbol Symbol);

internal record MonadDataWithSymbolEx(
    MonadDataWithSymbol Data,
    IReadOnlyList<MonadImplementationGenerationInfo> MonadsWithoutImplementation);

internal record MonadImplementationInfo(
    string Name,
    string FullName);

internal record MonadImplementationInfoEx(
    MonadImplementationInfo Info,
    Func<string, string> GenericTypeName);

internal record MonadImplementationGenerationInfo(
    Func<string, string> GenericTypeName,
    MonadData Monad);

internal delegate string ConstructDelegate(IReadOnlyList<string> typeParameters);

internal delegate string InvokeMethod(IReadOnlyList<string> typeParameters, IReadOnlyList<string> parameters);

internal record TypeInfo(string Name, IReadOnlyList<TypeInfo> TypeParameters)
{
    public override string ToString() =>
        TypeParameters.Count > 0
            ? $"{Name}<{string.Join(", ", TypeParameters)}>"
            : Name;
}