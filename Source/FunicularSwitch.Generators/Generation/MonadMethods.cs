using FunicularSwitch.Generators.Transformer;

namespace FunicularSwitch.Generators.Generation;

internal static class MonadMethods
{
    public static MethodGenerationInfo Create(
        int arity,
        string returnTypeParameter,
        ConstructType genericTypeName,
        IReadOnlyList<string> typeParameters,
        Func<IReadOnlyList<TypeInfo>, IReadOnlyList<ParameterGenerationInfo>> parameters,
        string name,
        Func<IReadOnlyList<TypeInfo>, string> invoke)
    {
        var extraTypeParameters = Enumerable.Range(0, arity)
            .Select(i => $"T{i}")
            .Select(TypeInfo.Parameter)
            .ToList();
        var allTypeParameters = extraTypeParameters
            .Select(x => x.ToString())
            .Concat(typeParameters).ToList();

        return new MethodGenerationInfo(
            genericTypeName([..extraTypeParameters, returnTypeParameter]),
            allTypeParameters,
            parameters(extraTypeParameters),
            name,
            invoke(extraTypeParameters));
    }

    public static IReadOnlyList<MethodGenerationInfo> CreateCoreMonadMethods(
        ConstructType genericTypeName,
        MonadInfo chainedMonad,
        MonadInfo outerMonad,
        MonadInfo innerMonad)
    {
        return
        [
            ..Lift(genericTypeName, chainedMonad, outerMonad, innerMonad),
        ];
    }

    public static IReadOnlyList<MethodGenerationInfo> CreateExtendMonadMethods(
        ConstructType genericTypeName,
        MonadInfo monad,
        IReadOnlyList<MethodGenerationInfo> existingMethods)
    {
        IReadOnlyList<MethodGenerationInfo> methodGenerationInfos =
        [
            ..Return(genericTypeName, monad),
            ..BindMethods(),
            ..MapMethods(),
            ..CombineMethods(),
            ..FlattenMethods(),
        ];
        return methodGenerationInfos
            .Distinct(MethodGenerationInfo.SignatureComparer.Instance)
            .Except(existingMethods, MethodGenerationInfo.SignatureComparer.Instance)
            .ToList();

        IEnumerable<MethodGenerationInfo> BindMethods() =>
        [
            ..Bind(monad.BindMethod.Name, genericTypeName, monad),
            ..Bind("SelectMany", genericTypeName, monad),
            ..Bind2("SelectMany", genericTypeName, monad),
        ];

        IEnumerable<MethodGenerationInfo> MapMethods() =>
        [
            ..Map("Map", genericTypeName, monad),
            ..Map("Select", genericTypeName, monad),
        ];

        IEnumerable<MethodGenerationInfo> CombineMethods() =>
            Combine(genericTypeName, monad, 12);

        IEnumerable<MethodGenerationInfo> FlattenMethods() =>
            Flatten("Flatten", genericTypeName, monad);
    }

    private static IEnumerable<MethodGenerationInfo> AsyncVariants(string parameterName, Func<string, MethodGenerationInfo> fn)
    {
        var sync = fn(parameterName);
        var parameter = sync.Parameters.First(x => x.Name == parameterName);
        var asyncBase = fn($"(await {parameterName})");

        return
        [
            sync,
            WithAsyncType(Types.Task),
            WithAsyncType(Types.ValueTask),
        ];

        MethodGenerationInfo WithAsyncType(Func<TypeInfo, TypeInfo> taskType) =>
            asyncBase with
            {
                ReturnType = taskType(sync.ReturnType),
                Parameters =
                [
                    parameter with {Type = taskType(parameter.Type)},
                    ..sync.Parameters.Skip(1),
                ],
                IsAsync = true,
            };
    }

    private static IEnumerable<MethodGenerationInfo> Bind(string name, ConstructType genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(t => genericTypeName([..t, "B"])),
            ..ForFnType(t => chainedMonad.GenericTypeName([..t, "B"])),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(Func<IReadOnlyList<TypeInfo>, TypeInfo> fnReturnType) =>
            AsyncVariants("ma", p => Create(
                chainedMonad.ExtraArity,
                "B",
                genericTypeName,
                ["A", "B"],
                t =>
                [
                    new ParameterGenerationInfo(genericTypeName([..t, "A"]), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType(t)), "fn"),
                ],
                name,
                t => $"{chainedMonad.BindMethod.Invoke([..t, "A", "B"], [$"(({chainedMonad.GenericTypeName([..t, "A"])}){p})", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}"
            ));
    }

    private static IEnumerable<MethodGenerationInfo> Bind2(string name, ConstructType genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(t => genericTypeName([..t, "B"])),
            ..ForFnType(t => chainedMonad.GenericTypeName([..t, "B"])),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(Func<IReadOnlyList<TypeInfo>, TypeInfo> fnReturnType) =>
            AsyncVariants("ma", p => Create(
                chainedMonad.ExtraArity,
                "C",
                genericTypeName,
                ["A", "B", "C"],
                t =>
                [
                    new ParameterGenerationInfo(genericTypeName([..t, "A"]), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType(t)), "fn"),
                    new ParameterGenerationInfo(Types.Func("A", "B", "C"), "selector"),
                ],
                name,
                t => $"{p}.SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName([..t, "B"])})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))"
            ));
    }

    private static IEnumerable<MethodGenerationInfo> Combine(ConstructType genericTypeName, MonadInfo chainedMonad, int maxCount)
    {
        var mapMethod = new InvokeMethod((t, p) => $"{p[0]}.Map<{string.Join(", ", t)}>({p[1]})");

        return Enumerable.Range(2, maxCount - 1)
            .SelectMany(ForCount);

        string Tuple(int count) => $"({string.Join(", ", Enumerable.Range(0, count).Select(i => $"S{i}"))})";

        string CombineArgs(int count) => string.Join(", ", Enumerable.Range(0, count).Select(i => $"s{i}"));

        IEnumerable<MethodGenerationInfo> ForCount(int count)
        {
            var typeParameters = Enumerable.Range(0, count).Select(x => $"S{x}").ToList();
            yield return Create(
                chainedMonad.ExtraArity,
                $"({string.Join(", ", typeParameters)})",
                genericTypeName,
                typeParameters,
                t => Enumerable.Range(0, count)
                    .Select(i => new ParameterGenerationInfo(
                        genericTypeName([..t, $"S{i}"]), $"s{i}"))
                    .ToList(),
                "Combine",
                t => count > 2
                    ? CombineTail(t)
                    : CombineHead(t)
            );

            string CombineHead(IReadOnlyList<TypeInfo> t) =>
                chainedMonad.BindMethod.Invoke([..t, "S0", "(S0, S1)"], ["s0", $"v0 => {mapMethod([..t, "S1", "(S0, S1)"], ["s1", "v1 => (v0, v1)"])}"]);

            string CombineTail(IReadOnlyList<TypeInfo> t)
            {
                var fromTupleType = Tuple(count - 1);
                var toTupleType = Tuple(count);
                var lastType = $"S{count - 1}";
                var lastArg = $"s{count - 1}";
                var mapFn = $"last => ({string.Join(", ", Enumerable.Range(1, count - 1).Select(i => $"prev.Item{i}"))}, last)";
                return chainedMonad.BindMethod.Invoke([..t, fromTupleType, toTupleType], [$"Combine({CombineArgs(count - 1)})", $"prev => {mapMethod([..t, lastType, toTupleType], [lastArg, mapFn])}"]);
            }
        }
    }

    private static IEnumerable<MethodGenerationInfo> Flatten(string name, ConstructType genericTypeName, MonadInfo monad) =>
        AsyncVariants("ma", p => Create(
            monad.ExtraArity,
            "A",
            genericTypeName,
            ["A"],
            t =>
            [
                new ParameterGenerationInfo(genericTypeName([..t, genericTypeName([..t, "A"])]), "ma", true),
            ],
            name,
            t => $"{p}.{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => a)"
        ));

    private static IEnumerable<MethodGenerationInfo> Lift(ConstructType genericTypeName, MonadInfo chainedMonad, MonadInfo outerMonad, MonadInfo innerMonad) =>
        AsyncVariants("ma", p => Create(
            chainedMonad.ExtraArity,
            "A",
            genericTypeName,
            ["A"],
            t => [new ParameterGenerationInfo(outerMonad.GenericTypeName([..t.Take(outerMonad.ExtraArity), "A"]), "ma")],
            "Lift",
            t => $"{outerMonad.BindMethod.Invoke([..t.Take(outerMonad.ExtraArity), "A", $"{innerMonad.GenericTypeName([..t.Skip(outerMonad.ExtraArity), "A"])}"], [p, $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke([..t, "A"], ["a"])}"])}"
        ));

    private static IEnumerable<MethodGenerationInfo> Map(string name, ConstructType genericTypeName, MonadInfo monad) =>
        AsyncVariants("ma", p => Create(
            monad.ExtraArity,
            "B",
            genericTypeName,
            ["A", "B"],
            t =>
            [
                new ParameterGenerationInfo(genericTypeName([..t, "A"]), "ma", true),
                new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
            ],
            name,
            t => $"{p}.{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke([..t, "B"], ["fn(a)"])})"
        ));

    private static IEnumerable<MethodGenerationInfo> Return(ConstructType genericTypeName, MonadInfo chainedMonad) =>
        AsyncVariants("a", p => Create(
            chainedMonad.ExtraArity,
            "A",
            genericTypeName,
            ["A"],
            _ => [new ParameterGenerationInfo("A", "a")],
            chainedMonad.ReturnMethod.Name,
            t => chainedMonad.ReturnMethod.Invoke([..t, "A"], [p])
        ));
}
