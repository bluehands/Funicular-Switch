using FunicularSwitch.Generators.Transformer;

namespace FunicularSwitch.Generators.Generation;

internal static class MonadMethods
{
    public static IReadOnlyList<MethodGenerationInfo> CreateCoreMonadMethods(
        Func<string, string> genericTypeName,
        MonadInfo chainedMonad,
        MonadInfo outerMonad,
        MonadInfo innerMonad)
    {
        return
        [
            ..Return(genericTypeName, chainedMonad),
            ..Bind(chainedMonad.BindMethod.Name, genericTypeName, chainedMonad)
                .Distinct(MethodGenerationInfo.Comparer.Instance),
            ..Lift(genericTypeName, chainedMonad, outerMonad, innerMonad),
        ];
    }

    public static IReadOnlyList<MethodGenerationInfo> CreateExtendMonadMethods(
        Func<string, string> genericTypeName,
        MonadInfo monad)
    {
        return
        [
            ..BindMethods().Distinct(MethodGenerationInfo.Comparer.Instance),
            ..MapMethods(),
        ];

        IEnumerable<MethodGenerationInfo> BindMethods() =>
        [
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
        var async = fn($"(await {parameterName})") with
        {
            ReturnType = Types.Task(sync.ReturnType),
            Parameters =
            [
                parameter with {Type = Types.Task(parameter.Type)},
                ..sync.Parameters.Skip(1),
            ],
            IsAsync = true,
        };

        return
        [
            sync,
            async,
        ];
    }

    private static IEnumerable<MethodGenerationInfo> Bind(string name, Func<string, string> genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(genericTypeName("B")),
            ..ForFnType(chainedMonad.GenericTypeName("B")),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(string fnReturnType) =>
            AsyncVariants("ma", p => new(
                genericTypeName("B"),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                ],
                name,
                $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")}){p})", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}"
            ));
    }

    private static IEnumerable<MethodGenerationInfo> Bind2(string name, Func<string, string> genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(genericTypeName("B")),
            ..ForFnType(chainedMonad.GenericTypeName("B")),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(string fnReturnType) =>
            AsyncVariants("ma", p => new(
                genericTypeName("C"),
                ["A", "B", "C"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                    new ParameterGenerationInfo(Types.Func("A", "B", "C"), "selector"),
                ],
                name,
                $"{p}.SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName("B")})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))"
            ));
    }

    private static IEnumerable<MethodGenerationInfo> Lift(Func<string, string> genericTypeName, MonadInfo chainedMonad, MonadInfo outerMonad, MonadInfo innerMonad) =>
        AsyncVariants("ma", p => new(
            genericTypeName("A"),
            ["A"],
            [new ParameterGenerationInfo(outerMonad.GenericTypeName("A"), "ma")],
            "Lift",
            $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], [p, $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}"
        ));

    private static IEnumerable<MethodGenerationInfo> Map(string name, Func<string, string> genericTypeName, MonadInfo monad) =>
        AsyncVariants("ma", p => new(
            genericTypeName("B"),
            ["A", "B"],
            [
                new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
            ],
            name,
            $"{p}.{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke(["B"], ["fn(a)"])})"
        ));

    private static IEnumerable<MethodGenerationInfo> Return(Func<string, string> genericTypeName, MonadInfo chainedMonad) =>
        AsyncVariants("a", p => new(
            genericTypeName("A"),
            ["A"],
            [new ParameterGenerationInfo("A", "a")],
            chainedMonad.ReturnMethod.Name,
            chainedMonad.ReturnMethod.Invoke(["A"], [p])
        ));
}
