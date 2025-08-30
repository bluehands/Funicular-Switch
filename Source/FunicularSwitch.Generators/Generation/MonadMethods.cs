using FunicularSwitch.Generators.Transformer;

namespace FunicularSwitch.Generators.Generation;

internal static class MonadMethods
{
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

        MethodGenerationInfo WithAsyncType(Func<string, string> taskType) =>
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
            ..ForFnType(genericTypeName(["B"])),
            ..ForFnType(chainedMonad.GenericTypeName(["B"])),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(string fnReturnType) =>
            AsyncVariants("ma", p => new(
                genericTypeName(["B"]),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(genericTypeName(["A"]), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                ],
                name,
                $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName(["A"])}){p})", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}"
            ));
    }

    private static IEnumerable<MethodGenerationInfo> Bind2(string name, ConstructType genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(genericTypeName(["B"])),
            ..ForFnType(chainedMonad.GenericTypeName(["B"])),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(string fnReturnType) =>
            AsyncVariants("ma", p => new(
                genericTypeName(["C"]),
                ["A", "B", "C"],
                [
                    new ParameterGenerationInfo(genericTypeName(["A"]), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                    new ParameterGenerationInfo(Types.Func("A", "B", "C"), "selector"),
                ],
                name,
                $"{p}.SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName(["B"])})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))"
            ));
    }

    private static IEnumerable<MethodGenerationInfo> Lift(ConstructType genericTypeName, MonadInfo chainedMonad, MonadInfo outerMonad, MonadInfo innerMonad) =>
        AsyncVariants("ma", p => new(
            genericTypeName(["A"]),
            ["A"],
            [new ParameterGenerationInfo(outerMonad.GenericTypeName(["A"]), "ma")],
            "Lift",
            $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName(["A"])}"], [p, $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}"
        ));

    private static IEnumerable<MethodGenerationInfo> Map(string name, ConstructType genericTypeName, MonadInfo monad) =>
        AsyncVariants("ma", p => new(
            genericTypeName(["B"]),
            ["A", "B"],
            [
                new ParameterGenerationInfo(genericTypeName(["A"]), "ma", true),
                new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
            ],
            name,
            $"{p}.{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke(["B"], ["fn(a)"])})"
        ));

    private static IEnumerable<MethodGenerationInfo> Return(ConstructType genericTypeName, MonadInfo chainedMonad) =>
        AsyncVariants("a", p => new(
            genericTypeName(["A"]),
            ["A"],
            [new ParameterGenerationInfo("A", "a")],
            chainedMonad.ReturnMethod.Name,
            chainedMonad.ReturnMethod.Invoke(["A"], [p])
        ));
}
