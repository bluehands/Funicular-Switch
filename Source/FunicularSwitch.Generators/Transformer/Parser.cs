using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Parser
{
    public static StaticMonadGenerationInfo BuildStaticMonad(
        string typeName,
        Func<string, string> genericTypeName,
        string accessModifier,
        IReadOnlyList<MonadImplementationGenerationInfo> monadImplementations,
        MonadInfo chainedMonad,
        MonadInfo outerMonad,
        MonadInfo innerMonad,
        bool generateCoreMethods = true)
    {
        return new StaticMonadGenerationInfo(
            typeName,
            accessModifier,
            monadImplementations,
            [
                ..generateCoreMethods ? ReturnMethods() : [],
                ..BindMethods().Distinct(MethodGenerationInfo.Comparer.Instance),
                ..LiftMethods(),
                ..MapMethods(),
            ]
        );

        IEnumerable<MethodGenerationInfo> ReturnMethods()
        {
            return
            [
                Build(),
                BuildAsync(),
            ];

            MethodGenerationInfo Build() => new(
                genericTypeName("A"),
                ["A"],
                [new ParameterGenerationInfo("A", "a")],
                chainedMonad.ReturnMethod // implicit cast
            );

            MethodGenerationInfo BuildAsync() => new(
                TaskType(genericTypeName("A")),
                ["A"],
                [new ParameterGenerationInfo(TaskType("A"), "a")],
                chainedMonad.ReturnMethod.Name,
                chainedMonad.ReturnMethod.Invoke(["A"], ["(await a)"]),
                true
            );
        }

        IEnumerable<MethodGenerationInfo> BindMethods()
        {
            var methods = new List<MethodGenerationInfo>();
            if (generateCoreMethods)
                methods.AddRange([
                    Build(chainedMonad.BindMethod.Name, genericTypeName("B")),
                    Build(chainedMonad.BindMethod.Name, chainedMonad.GenericTypeName("B")),
                    BuildAsync(chainedMonad.BindMethod.Name, genericTypeName("B")),
                    BuildAsync(chainedMonad.BindMethod.Name, chainedMonad.GenericTypeName("B")),
                ]);

            methods.AddRange([
                Build("SelectMany", genericTypeName("B")),
                Build("SelectMany", chainedMonad.GenericTypeName("B")),
                BuildAsync("SelectMany", genericTypeName("B")),
                BuildAsync("SelectMany", chainedMonad.GenericTypeName("B")),
                Build2("SelectMany", genericTypeName("B")),
                Build2("SelectMany", chainedMonad.GenericTypeName("B")),
                Build2Async("SelectMany", genericTypeName("B")),
                Build2Async("SelectMany", chainedMonad.GenericTypeName("B")),
            ]);

            return methods;

            MethodGenerationInfo Build(string name, string fnReturnType) => new(
                genericTypeName("B"),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(FuncType("A", fnReturnType), "fn"),
                ],
                name,
                $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")})ma)", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}"
            );

            MethodGenerationInfo BuildAsync(string name, string fnReturnType) => new(
                TaskType(genericTypeName("B")),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(TaskType(genericTypeName("A")), "ma", true),
                    new ParameterGenerationInfo(FuncType("A", fnReturnType), "fn"),
                ],
                name,
                $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")})(await ma))", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}",
                true
            );

            MethodGenerationInfo Build2(string name, string fnReturnType) => new(
                genericTypeName("C"),
                ["A", "B", "C"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(FuncType("A", fnReturnType), "fn"),
                    new ParameterGenerationInfo(FuncType("A", "B", "C"), "selector"),
                ],
                name,
                $"ma.SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName("B")})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))"
            );

            MethodGenerationInfo Build2Async(string name, string fnReturnType) => new(
                TaskType(genericTypeName("C")),
                ["A", "B", "C"],
                [
                    new ParameterGenerationInfo(TaskType(genericTypeName("A")), "ma", true),
                    new ParameterGenerationInfo(FuncType("A", fnReturnType), "fn"),
                    new ParameterGenerationInfo(FuncType("A", "B", "C"), "selector"),
                ],
                name,
                $"(await ma).SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName("B")})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))",
                true
            );
        }

        IEnumerable<MethodGenerationInfo> LiftMethods()
        {
            return
            [
                Build(),
                BuildAsync(),
            ];

            MethodGenerationInfo Build() => new(
                genericTypeName("A"),
                ["A"],
                [new ParameterGenerationInfo(outerMonad.GenericTypeName("A"), "ma")],
                "Lift",
                $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], ["ma", $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}"
            );

            MethodGenerationInfo BuildAsync() => new(
                TaskType(genericTypeName("A")),
                ["A"],
                [new ParameterGenerationInfo(TaskType(outerMonad.GenericTypeName("A")), "ma")],
                "Lift",
                $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], ["(await ma)", $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}",
                true
            );
        }

        IEnumerable<MethodGenerationInfo> MapMethods()
        {
            return
            [
                Build("Map"),
                BuildAsync("Map"),
                Build("Select"),
                BuildAsync("Select"),
            ];

            MethodGenerationInfo Build(string name) => new(
                genericTypeName("B"),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(FuncType("A", "B"), "fn"),
                ],
                name,
                $"ma.{chainedMonad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {typeName}.{chainedMonad.ReturnMethod.Name}(fn(a)))"
            );

            MethodGenerationInfo BuildAsync(string name) => new(
                TaskType(genericTypeName("B")),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(TaskType(genericTypeName("A")), "ma", true),
                    new ParameterGenerationInfo(FuncType("A", "B"), "fn"),
                ],
                name,
                $"(await ma).{chainedMonad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {typeName}.{chainedMonad.ReturnMethod.Name}(fn(a)))",
                true
            );
        }

        static string FuncType(params string[] typeParameters) =>
            GenericType("global::System.Func", typeParameters);

        static string TaskType(string typeParameter) =>
            GenericType("global::System.Threading.Tasks.Task", typeParameter);

        static string GenericType(string name, params string[] typeParameters) =>
            $"{name}<{string.Join(", ", typeParameters)}>";
    }

    public static string DetermineAccessModifier(INamedTypeSymbol type) =>
        type.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            _ => throw new ArgumentOutOfRangeException(),
        };

    public static GenerationResult<TransformMonadInfo> GetTransformedMonadSchema(
        INamedTypeSymbol transformedMonadSymbol,
        TransformMonadAttribute transformMonadAttribute,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var accessModifier = DetermineAccessModifier(transformedMonadSymbol);
        var outerMonadType = transformMonadAttribute.MonadType;

        return
            from outerMonadData in ResolveMonadDataFromMonadType(outerMonadType, cancellationToken)
            let transformerTypes = new[] {transformMonadAttribute.TransformerType}
                .Concat(transformMonadAttribute.ExtraTransformerTypes)
                .ToList()
            from chainedMonads in transformerTypes
                .Aggregate(
                    (GenerationResult<IReadOnlyList<MonadInfo>>) new[] {outerMonadData},
                    (acc, cur) =>
                        acc.Bind(acc_ =>
                            TransformMonad(acc_.Last(), cur, cancellationToken).Map<IReadOnlyList<MonadInfo>>(transformMonad =>
                                [..acc_, transformMonad])))
            let chainedMonad = chainedMonads.Last()
            let implementations = chainedMonads
                .Take(chainedMonads.Count - 1)
                .Where(x => !x.ImplementsMonadInterface)
                .Select(GenerateImplementationForMonad)
                .ToList()
            let transformMonadData = new TransformMonadInfo(
                transformedMonadSymbol.GetFullNamespace()!,
                transformedMonadSymbol.FullTypeNameWithNamespace(),
                transformedMonadSymbol.IsStatic ? chainedMonad.GenericTypeName : FullGenericType,
                chainedMonad,
                transformedMonadSymbol.IsStatic
                    ? null
                    : BuildGenericMonad(
                        transformedMonadSymbol,
                        accessModifier,
                        chainedMonad
                    ),
                BuildStaticMonad(
                    transformedMonadSymbol.Name,
                    transformedMonadSymbol.IsStatic ? chainedMonad.GenericTypeName : FullGenericType,
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

        static MethodInfo CombineReturn(MethodInfo outer, MonadInfo inner) =>
            new MethodInfo(
                DetermineMethodName(outer.Name, inner.ReturnMethod.Name, "Return"),
                (t, p) => $"{outer.Invoke([inner.GenericTypeName(t[0])], [inner.ReturnMethod.Invoke(t, p)])}");

        static MethodInfo TransformBind(MonadInfo outer, MonadInfo inner, string transformerTypeName, Func<string, string> outerInterfaceImplName)
        {
            var chainedGenericType = ChainGenericTypeName(outer.GenericTypeName, inner.GenericTypeName);

            return new MethodInfo(
                DetermineMethodName(outer.BindMethod.Name, inner.BindMethod.Name, "Bind"),
                (t, p) =>
                {
                    var ma = $"({outerInterfaceImplName(inner.GenericTypeName(t[0]))}){p[0]}";
                    var fn = $"[{Constants.DebuggerStepThroughAttribute}](a) => ({outerInterfaceImplName(inner.GenericTypeName(t[1]))})(new global::System.Func<{t[0]}, {chainedGenericType(t[1])}>({p[1]}).Invoke(a))"; // A -> Monad<X<B>>

                    var call = $"{transformerTypeName}.BindT<{t[0]}, {t[1]}>({ma}, {fn}).Cast<{chainedGenericType(t[1])}>()";
                    return call;
                });
        }

        static GenerationResult<MonadInfo> TransformMonad(MonadInfo outer, INamedTypeSymbol transformerType,
            CancellationToken cancellationToken1 = default) =>
            ResolveMonadDataFromTransformerType(transformerType, cancellationToken1)
                .Map(innerMonad =>
                {
                    var transformerTypeName = $"global::{transformerType.FullTypeNameWithNamespace()}";
                    var outerInterfaceImplementation =
                        !outer.ImplementsMonadInterface ? GenerateImplementationForMonad(outer) : null;
                    var outerInterfaceName =
                        outerInterfaceImplementation?.GenericTypeName ?? outer.GenericTypeName;

                    var transformedMonad = new MonadInfo(
                        ChainGenericTypeName(outer.GenericTypeName, innerMonad.GenericTypeName),
                        CombineReturn(outer.ReturnMethod, innerMonad),
                        TransformBind(outer, innerMonad, transformerTypeName, outerInterfaceName));
                    return transformedMonad;
                });
    }

    public static GenerationResult<MonadInfo> ResolveMonadDataFromMonadType(INamedTypeSymbol monadType,
        CancellationToken cancellationToken)
    {
        if (monadType.GetAttributes().Any(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.ResultTypeAttribute"))
            return ResolveMonadDataFromResultType(monadType);
        if (monadType.IsUnboundGenericType)
            return ResolveMonadDataFromGenericMonadType(monadType, cancellationToken);
        return ResolveMonadDataFromStaticMonadType(monadType);
    }

    private static GenericMonadGenerationInfo BuildGenericMonad(
        INamedTypeSymbol transformedMonadSymbol,
        string accessModifier,
        MonadInfo chainedMonad)
    {
        var typeModifier = DetermineTypeModifier(transformedMonadSymbol);
        var isRecord = transformedMonadSymbol.IsRecord;
        var typeParameter = transformedMonadSymbol.TypeArguments[0].Name;

        return new(
            accessModifier,
            typeModifier,
            transformedMonadSymbol.Name,
            typeParameter,
            $"{transformedMonadSymbol.Name}<{typeParameter}>",
            isRecord,
            chainedMonad
        );
    }

    private static MonadInfo CreateMonadData(INamedTypeSymbol? staticType, INamedTypeSymbol genericType, IMethodSymbol returnMethod, IMethodSymbol? bindMethod = default)
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

        return new MonadInfo(
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

    private static MonadImplementationGenerationInfo GenerateImplementationForMonad(MonadInfo info)
    {
        var baseName = info.GenericTypeName("_")
            .Replace('.', '_')
            .Replace('<', '_')
            .Replace('>', '_')
            .Replace(':', '_')
            .TrimEnd('_')
            [8..];
        return new MonadImplementationGenerationInfo(
            t => $"Impl__{baseName}<{t}>",
            info);
    }

    private static bool ImplementsMonadInterface(INamedTypeSymbol genericType) =>
        genericType.GetAttributes().Any(x =>
            x.AttributeClass?.FullTypeNameWithNamespace() == TransformMonadAttribute.ATTRIBUTE_NAME) ||
        genericType.OriginalDefinition.AllInterfaces.Any(x => x.FullTypeNameWithNamespace() == "FunicularSwitch.Transformers.Monad");

    private static GenerationResult<MonadInfo> ResolveMonadDataFromGenericMonadType(INamedTypeSymbol genericMonadType,
        CancellationToken cancellationToken)
    {
        var transformMonadAttribute = genericMonadType
            .GetAttributes()
            .FirstOrDefault(x => x.AttributeClass?.FullTypeNameWithNamespace() == "FunicularSwitch.Generators.TransformMonadAttribute");
        if (transformMonadAttribute is not null)
        {
            var transformedMonadData = GetTransformedMonadSchema(genericMonadType, TransformMonadAttribute.From(transformMonadAttribute), cancellationToken).Value!;
            var returnMethodInfo = transformedMonadData.Monad.ReturnMethod with
            {
                Invoke = (_, p) => $"global::{transformedMonadData.FullTypeName}.{transformedMonadData.Monad.ReturnMethod.Name}({p[0]})",
            };
            var bindMethodInfo = transformedMonadData.Monad.BindMethod with
            {
                Invoke = (_, p) => $"{p[0]}.{transformedMonadData.Monad.BindMethod.Name}({p[1]})",
            };
            return new MonadInfo(
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
            return new DiagnosticInfo(Diagnostics.MissingReturnMethod(genericMonadType));

        var bindMethod = genericMonadType.OriginalDefinition
            .GetMembers()
            .OfType<IMethodSymbol>()
            .FirstOrDefault(IsBindMethod);

        if (bindMethod is null)
            return new DiagnosticInfo(Diagnostics.MissingBindMethod(genericMonadType));

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

        bool IsBindMethod(IMethodSymbol method)
        {
            if (method.TypeParameters.Length != 1) return false;
            if (method.Parameters.Length != 1) return false;
            if (method.ReturnType is not INamedTypeSymbol {IsGenericType: true, TypeArguments.Length: 1} genericReturnType) return false;
            if (!SymbolEqualityComparer.IncludeNullability.Equals(genericReturnType.ConstructUnboundGenericType(), genericMonadType)) return false;
            if (genericReturnType.TypeArguments[0].Name == genericMonadType.OriginalDefinition.TypeParameters[0].Name) return false;
            return true;
        }
    }

    private static MonadInfo ResolveMonadDataFromResultType(INamedTypeSymbol resultType)
    {
        var returnMethod = new MethodInfo(
            "Ok",
            (t, p) => $"{GenericTypeName(t[0])}.Ok({p[0]})");
        var bindMethod = new MethodInfo(
            "Bind",
            (_, p) => $"{p[0]}.Bind({p[1]})");
        return new MonadInfo(
            GenericTypeName,
            returnMethod,
            bindMethod);

        string GenericTypeName(string t) => $"global::{resultType.FullTypeNameWithNamespace()}<{t}>";
    }

    private static GenerationResult<MonadInfo> ResolveMonadDataFromStaticMonadType(INamedTypeSymbol staticMonadType)
    {
        var returnMethod = staticMonadType
            .GetMembers()
            .OfType<IMethodSymbol>()
            .FirstOrDefault(IsStaticReturnMethod);

        if (returnMethod is null)
            return new DiagnosticInfo(Diagnostics.MissingReturnMethod(staticMonadType));

        var genericMonadType = ((INamedTypeSymbol) returnMethod.ReturnType).ConstructUnboundGenericType();

        var bindMethod = staticMonadType
            .GetMembers()
            .OfType<IMethodSymbol>()
            .FirstOrDefault(x => IsStaticBindMethod(genericMonadType, x));

        if (bindMethod is null)
            return new DiagnosticInfo(Diagnostics.MissingBindMethod(staticMonadType));

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

    private static GenerationResult<MonadInfo> ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType,
        CancellationToken cancellationToken)
    {
        var attributeData = transformerType.GetAttributes()
            .FirstOrDefault(x =>
                x.AttributeClass?.FullTypeNameWithNamespace() == MonadTransformerAttribute.ATTRIBUTE_NAME);

        if (attributeData is null)
            return new DiagnosticInfo(Diagnostics.MonadTransformerNoAttribute(transformerType));

        var hasBindTMethod = transformerType
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(IsBindTMethod);

        if (!hasBindTMethod)
            return new DiagnosticInfo(Diagnostics.MissingBindTMethod(transformerType));

        var monadTransformerAttribute = MonadTransformerAttribute.From(attributeData);
        var staticMonadType = monadTransformerAttribute.MonadType;
        var monadData = ResolveMonadDataFromMonadType(staticMonadType, cancellationToken);
        return monadData;

        static bool IsBindTMethod(IMethodSymbol method)
        {
            if (method.Name != "BindT") return false;
            return true;
        }
    }
}
