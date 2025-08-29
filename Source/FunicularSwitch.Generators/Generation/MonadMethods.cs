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

    private static IEnumerable<MethodGenerationInfo> Bind(string name, Func<string, string> genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(genericTypeName("B")),
            ..ForFnType(chainedMonad.GenericTypeName("B")),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(string fnReturnType) =>
        [
            new(
                genericTypeName("B"),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                ],
                name,
                $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")})ma)", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}"
            ),
            new(
                Types.Task(genericTypeName("B")),
                ["A", "B"],
                [
                    new ParameterGenerationInfo(Types.Task(genericTypeName("A")), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                ],
                name,
                $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")})(await ma))", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}",
                true
            ),
        ];
    }

    private static IEnumerable<MethodGenerationInfo> Bind2(string name, Func<string, string> genericTypeName, MonadInfo chainedMonad)
    {
        return
        [
            ..ForFnType(genericTypeName("B")),
            ..ForFnType(chainedMonad.GenericTypeName("B")),
        ];

        IEnumerable<MethodGenerationInfo> ForFnType(string fnReturnType) =>
        [
            new(
                genericTypeName("C"),
                ["A", "B", "C"],
                [
                    new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                    new ParameterGenerationInfo(Types.Func("A", "B", "C"), "selector"),
                ],
                name,
                $"ma.SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName("B")})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))"
            ),
            new(
                Types.Task(genericTypeName("C")),
                ["A", "B", "C"],
                [
                    new ParameterGenerationInfo(Types.Task(genericTypeName("A")), "ma", true),
                    new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
                    new ParameterGenerationInfo(Types.Func("A", "B", "C"), "selector"),
                ],
                name,
                $"(await ma).SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName("B")})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))",
                true
            ),
        ];
    }

    private static IEnumerable<MethodGenerationInfo> Lift(Func<string, string> genericTypeName, MonadInfo chainedMonad, MonadInfo outerMonad, MonadInfo innerMonad) =>
    [
        new(
            genericTypeName("A"),
            ["A"],
            [new ParameterGenerationInfo(outerMonad.GenericTypeName("A"), "ma")],
            "Lift",
            $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], ["ma", $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}"
        ),
        new(
            Types.Task(genericTypeName("A")),
            ["A"],
            [new ParameterGenerationInfo(Types.Task(outerMonad.GenericTypeName("A")), "ma")],
            "Lift",
            $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], ["(await ma)", $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}",
            true
        ),
    ];

    private static IEnumerable<MethodGenerationInfo> Map(string name, Func<string, string> genericTypeName, MonadInfo monad) =>
    [
        new(
            genericTypeName("B"),
            ["A", "B"],
            [
                new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
                new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
            ],
            name,
            $"ma.{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke(["B"], ["fn(a)"])})"
        ),
        new(
            Types.Task(genericTypeName("B")),
            ["A", "B"],
            [
                new ParameterGenerationInfo(Types.Task(genericTypeName("A")), "ma", true),
                new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
            ],
            name,
            $"(await ma).{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke(["B"], ["fn(a)"])})",
            true
        ),
    ];

    private static IEnumerable<MethodGenerationInfo> Return(Func<string, string> genericTypeName, MonadInfo chainedMonad) =>
    [
        new(
            genericTypeName("A"),
            ["A"],
            [new ParameterGenerationInfo("A", "a")],
            chainedMonad.ReturnMethod // implicit cast
        ),
        new(
            Types.Task(genericTypeName("A")),
            ["A"],
            [new ParameterGenerationInfo(Types.Task("A"), "a")],
            chainedMonad.ReturnMethod.Name,
            chainedMonad.ReturnMethod.Invoke(["A"], ["(await a)"]),
            true
        ),
    ];
}
