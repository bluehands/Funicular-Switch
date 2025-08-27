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

        return
            from outerMonadData in ResolveMonadDataFromMonadType(outerMonadType)
            let transformerTypes = new[] { transformMonadAttribute.TransformerType }
                .Concat(transformMonadAttribute.ExtraTransformerTypes)
                .ToList()
            from chainedMonads in transformerTypes
                .Aggregate(
                    (GenerationResult<IReadOnlyList<MonadData>>)new[] { outerMonadData },
                    (acc, cur) =>
                        acc.Bind(acc_ =>
                            TransformMonad(acc_.Last(), cur).Map<IReadOnlyList<MonadData>>(transformMonad =>
                                [..acc_, transformMonad])))
            let chainedMonad = chainedMonads.Last()
            let implementations = chainedMonads
                .Take(chainedMonads.Count - 1)
                .Where(x => !x.ImplementsMonadInterface)
                .Select(GenerateImplementationForMonad)
                .ToList()
            let typeParameter = transformedMonadSymbol.TypeArguments[0].Name
            let transformMonadData = new TransformMonadData(
                transformedMonadSymbol.GetFullNamespace()!,
                accessModifier,
                typeModifier,
                transformedMonadSymbol.Name,
                $"{transformedMonadSymbol.Name}<{typeParameter}>",
                typeParameter,
                transformedMonadSymbol.FullTypeNameWithNamespace(),
                FullGenericType,
                isRecord,
                chainedMonad,
                BuildStaticMonad(
                    transformedMonadSymbol.Name,
                    FullGenericType,
                    accessModifier,
                    implementations,
                    chainedMonad,
                    outerMonadData,
                    outerMonadData // TODO: determine actual inner monad
                ))
            select transformMonadData;


        string FullGenericType(string t) => $"global::{transformedMonadSymbol.FullTypeNameWithNamespace()}<{t}>";

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

                    var call = $"{transformerTypeName}.BindT<{t[0]}, {t[1]}>({ma}, {fn}).Cast<{chainedGenericType(t[1])}>()";
                    return call;
                });
        }

        static GenerationResult<MonadData> TransformMonad(MonadData outer, INamedTypeSymbol transformerType) =>
            ResolveMonadDataFromTransformerType(transformerType)
                .Map(innerMonad =>
                {
                    var transformerTypeName = $"global::{transformerType.FullTypeNameWithNamespace()}";
                    var outerInterfaceImplementation =
                        !outer.ImplementsMonadInterface ? GenerateImplementationForMonad(outer) : null;
                    var outerInterfaceName =
                        outerInterfaceImplementation?.GenericTypeName ?? outer.GenericTypeName;

                    var transformedMonad = new MonadData(
                        ChainGenericTypeName(outer.GenericTypeName, innerMonad.GenericTypeName),
                        CombineReturn(outer.ReturnMethod, innerMonad),
                        TransformBind(outer, innerMonad, transformerTypeName, outerInterfaceName));
                    return transformedMonad;
                });
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
            [
                BuildReturnMethod(),
                ..BindMethods(),
                BuildLiftMethod(),
                ..MapMethods(),
            ]
        );
        return staticMonadInfo;

        MethodGenerationInfo BuildReturnMethod() => new MethodGenerationInfo(
            genericTypeName("A"),
            ["A"],
            [new("A", "a")],
            chainedMonad.ReturnMethod // implicit cast
        );

        IEnumerable<MethodGenerationInfo> BindMethods()
        {
            return
            [
                Build(chainedMonad.BindMethod.Name, genericTypeName("B")),
                Build(chainedMonad.BindMethod.Name, chainedMonad.GenericTypeName("B")),
                Build("SelectMany", genericTypeName("B")),
                Build("SelectMany", chainedMonad.GenericTypeName("B")),
                Build2("SelectMany", genericTypeName("B")),
                Build2("SelectMany", chainedMonad.GenericTypeName("B")),
            ];

            MethodGenerationInfo Build(string name, string fnReturnType) => new(
                genericTypeName("B"),
                ["A", "B"],
                [
                    new(genericTypeName("A"), "ma", true),
                    new(FuncType("A", fnReturnType), "fn"),
                ],
                new(
                    name,
                    (t, p) =>
                        $"{chainedMonad.BindMethod.Invoke(t, [$"(({chainedMonad.GenericTypeName(t[0])}){p[0]})", $"a => {p[1]}(a)"])}" // implicit cast
                )
            );

            MethodGenerationInfo Build2(string name, string fnReturnType) => new(
                genericTypeName("C"),
                ["A", "B", "C"],
                [
                    new(genericTypeName("A"), "ma", true),
                    new(FuncType("A", fnReturnType), "fn"),
                    new(FuncType("A", "B", "C"), "selector"),
                ],
                name,
                $"ma.SelectMany(a => (({genericTypeName("B")})fn(a)).Map(b => selector(a, b)))"
            );
        }

        MethodGenerationInfo BuildLiftMethod() => new(
            genericTypeName("A"),
            ["A"],
            [new(outerMonad.GenericTypeName("A"), "ma")],
            new(
                "Lift",
                (t, _) => $"{outerMonad.BindMethod.Invoke([t[0], $"{innerMonad.GenericTypeName("A")}"], ["ma", $"a => {chainedMonad.ReturnMethod.Invoke(t, ["a"])}"])}" // implicit cast
            )
        );

        IEnumerable<MethodGenerationInfo> MapMethods()
        {
            return
            [
                Build("Map"),
                Build("Select"),
            ];

            MethodGenerationInfo Build(string name) => new(
                genericTypeName("B"),
                ["A", "B"],
                [
                    new(genericTypeName("A"), "ma", true),
                    new("global::System.Func<A, B>", "fn"),
                ],
                name,
                $"ma.{chainedMonad.BindMethod.Name}(a => {typeName}.{chainedMonad.ReturnMethod.Name}(fn(a)))"
            );
        }

        static string FuncType(params string[] typeParameters) =>
            GenericType("global::System.Func", typeParameters);
        
        static string TaskType(string typeParameter) =>
            GenericType("global::System.Threading.Tasks.Task", typeParameter);
        
        static string GenericType(string name, params string[] typeParameters) =>
            $"{name}<{string.Join(", ", typeParameters)}>";
    }

    private static MonadData CreateMonadData(INamedTypeSymbol? staticType, INamedTypeSymbol genericType, IMethodSymbol returnMethod, IMethodSymbol? bindMethod = default)
    {
        var genericTypeName = $"global::{genericType.FullTypeNameWithNamespace()}";
        var (returnMethodName, returnMethodInvoke) = GetReturnMethod();
        var (bindMethodName, bindMethodInvoke) = GetBindMethod();

        var returnMethodInfo = new MethodInfo(
            returnMethodName,
            (t, p) => returnMethodInvoke(t[0], p[0]));
        var bindMethodInfo = new MethodInfo(
            bindMethodName,
            (t, p) => bindMethodInvoke(t[0], t[1], p[0], p[1]));

        return new MonadData(
            TypeName,
            returnMethodInfo,
            bindMethodInfo,
            ImplementsMonadInterface(genericType));

        string TypeName(string typeParameter) => $"{genericTypeName}<{typeParameter}>";

        (string Name, string FullName) GetMethodFullName(IMethodSymbol? method, string defaultName) =>
            method is not null
                ? (method.Name, $"global::{method.ContainingType.FullTypeNameWithNamespace()}.{method.Name}")
                : (defaultName, $"global::{staticType}.{defaultName}");

        (string Name, Func<string, string, string, string, string> Invoke) GetBindMethod()
        {
            if (staticType is not null)
            {
                var (name, fullName) = GetMethodFullName(bindMethod, "Bind");
                return (name, (_, _, ma, fn) => $"{fullName}({ma}, {fn})");
            }
            else
            {
                var name = "Bind";
                return (name, Invoke);

                string Invoke(string s, string s1, string ma, string fn) => $"{ma}.Bind({fn})";
            }
        }

        (string Name, Func<string, string, string> Invoke) GetReturnMethod()
        {
            var name = returnMethod.Name;
            var func = returnMethod.ContainingType.IsGenericType
                ? new Func<string, string>(t => $"global::{returnMethod.ContainingType.FullTypeNameWithNamespace()}<{t}>.{name}")
                : _ => $"global::{returnMethod.ContainingType.FullTypeNameWithNamespace()}.{name}";
            return (name, Invoke);

            string Invoke(string t, string a) => $"{func(t)}({a})";
        }
    }

    private static string DetermineAccessModifier(INamedTypeSymbol type) =>
        type.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            _ => throw new ArgumentOutOfRangeException(),
        };

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

    private static bool ImplementsMonadInterface(INamedTypeSymbol genericType) =>
        genericType.GetAttributes().Any(x =>
            x.AttributeClass?.FullTypeNameWithNamespace() == TransformMonadAttribute.ATTRIBUTE_NAME) ||
        genericType.OriginalDefinition.AllInterfaces.Any(x => x.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.Monad");

    private static GenerationResult<MonadData> ResolveMonadDataFromGenericMonadType(INamedTypeSymbol genericMonadType)
    {
        var transformMonadAttribute = genericMonadType
            .GetAttributes()
            .FirstOrDefault(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.TransformMonadAttribute");
        if (transformMonadAttribute is not null)
        {
            var transformedMonadData = GetTransformedMonadSchema(genericMonadType, TransformMonadAttribute.From(transformMonadAttribute), CancellationToken.None).Value!;
            var returnMethodInfo = transformedMonadData.Monad.ReturnMethod with
            {
                Invoke = (_, p) => $"global::{transformedMonadData.FullTypeName}.{transformedMonadData.Monad.ReturnMethod.Name}({p[0]})",
            };
            var bindMethodInfo = transformedMonadData.Monad.BindMethod with
            {
                Invoke = (_, p) => $"{p[0]}.{transformedMonadData.Monad.BindMethod.Name}({p[1]})",
            };
            return new MonadData(
                transformedMonadData.FullGenericType,
                returnMethodInfo,
                bindMethodInfo,
                true);
        }

        var returnMethod = genericMonadType.OriginalDefinition
            .GetMembers()
            .OfType<IMethodSymbol>()
            .FirstOrDefault(IsReturnMethod);

        if (returnMethod is null)
            return new DiagnosticInfo(Diagnostics.MissingReturnMethod(
                $"{genericMonadType.FullTypeNameWithNamespace()} is missing a return method",
                genericMonadType.Locations.FirstOrDefault()));

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

    private static GenerationResult<MonadData> ResolveMonadDataFromMonadType(INamedTypeSymbol monadType)
    {
        if (monadType.GetAttributes().Any(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.ResultTypeAttribute"))
            return ResolveMonadDataFromResultType(monadType);
        if (monadType.IsUnboundGenericType)
            return ResolveMonadDataFromGenericMonadType(monadType);
        return ResolveMonadDataFromStaticMonadType(monadType);
    }

    private static MonadData ResolveMonadDataFromResultType(INamedTypeSymbol resultType)
    {
        var returnMethod = new MethodInfo(
            "Ok",
            (t, p) => $"{GenericTypeName(t[0])}.Ok({p[0]})");
        var bindMethod = new MethodInfo(
            "Bind",
            (_, p) => $"{p[0]}.Bind({p[1]})");
        return new MonadData(
            GenericTypeName,
            returnMethod,
            bindMethod);

        string GenericTypeName(string t) => $"global::{resultType.FullTypeNameWithNamespace()}<{t}>";
    }

    private static GenerationResult<MonadData> ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
    {
        var returnMethod = staticMonadType
            .GetMembers()
            .OfType<IMethodSymbol>()
            .FirstOrDefault(IsStaticReturnMethod);
        if (returnMethod is null)
            return new DiagnosticInfo(Diagnostics.MissingReturnMethod(
                $"{staticMonadType.FullTypeNameWithNamespace()} is missing a return method",
                staticMonadType.Locations.FirstOrDefault()));
        
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

        static bool IsStaticBindMethod(INamedTypeSymbol genericMonadType, IMethodSymbol method)
        {
            if (method.TypeParameters.Length != 2) return false;
            if (method.Parameters.Length != 2) return false;
            if (method.ReturnType is not INamedTypeSymbol {IsGenericType: true, TypeArguments.Length: 1} genericReturnType) return false;
            if (!SymbolEqualityComparer.IncludeNullability.Equals(genericReturnType.ConstructUnboundGenericType(), genericMonadType)) return false;
            if (genericReturnType.TypeArguments[0].Name != method.TypeParameters[1].Name) return false;
            return true;
        }
    }

    private static GenerationResult<MonadData> ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType)
    {
        var attributeData = transformerType.GetAttributes()
            .FirstOrDefault(x =>
                x.AttributeClass?.FullTypeNameWithNamespace() == MonadTransformerAttribute.ATTRIBUTE_NAME);
        if (attributeData is null)
            return new DiagnosticInfo(Diagnostics.MonadTransformerNoAttribute(
                $"{transformerType.FullTypeNameWithNamespace()} is missing the MonadTransformer attribute",
                transformerType.Locations.FirstOrDefault()));
        
        var monadTransformerAttribute = MonadTransformerAttribute.From(attributeData);
        var staticMonadType = monadTransformerAttribute.MonadType;
        var monadData = ResolveMonadDataFromMonadType(staticMonadType);
        return monadData;
    }
}

internal record MonadData(
    Func<string, string> GenericTypeName,
    MethodInfo ReturnMethod,
    MethodInfo BindMethod,
    bool ImplementsMonadInterface = false);

internal record MethodInfo(
    string Name,
    InvokeMethod Invoke);

internal record MonadImplementationGenerationInfo(
    Func<string, string> GenericTypeName,
    MonadData Monad);

internal delegate string InvokeMethod(IReadOnlyList<string> typeParameters, IReadOnlyList<string> parameters);

internal record TypeInfo(string Name, IReadOnlyList<TypeInfo> TypeParameters)
{
    public override string ToString() =>
        TypeParameters.Count > 0
            ? $"{Name}<{string.Join(", ", TypeParameters)}>"
            : Name;
}
