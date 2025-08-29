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
            ..ReturnMethods(),
            ..BindMethods().Distinct(MethodGenerationInfo.Comparer.Instance),
            ..LiftMethods(),
        ];

        IEnumerable<MethodGenerationInfo> ReturnMethods() =>
        [
            Return(genericTypeName, chainedMonad),
            ReturnAsync(genericTypeName, chainedMonad),
        ];

        IEnumerable<MethodGenerationInfo> BindMethods() =>
        [
            Bind(chainedMonad.BindMethod.Name, genericTypeName("B"), genericTypeName, chainedMonad),
            Bind(chainedMonad.BindMethod.Name, chainedMonad.GenericTypeName("B"), genericTypeName, chainedMonad),
            BindAsync(chainedMonad.BindMethod.Name, genericTypeName("B"), genericTypeName, chainedMonad),
            BindAsync(chainedMonad.BindMethod.Name, chainedMonad.GenericTypeName("B"), genericTypeName, chainedMonad),
        ];

        IEnumerable<MethodGenerationInfo> LiftMethods() =>
        [
            Lift(genericTypeName, chainedMonad, outerMonad, innerMonad),
            LiftAsync(genericTypeName, chainedMonad, outerMonad, innerMonad),
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
            Bind("SelectMany", genericTypeName("B"), genericTypeName, monad),
            Bind("SelectMany", monad.GenericTypeName("B"), genericTypeName, monad),
            BindAsync("SelectMany", genericTypeName("B"), genericTypeName, monad),
            BindAsync("SelectMany", monad.GenericTypeName("B"), genericTypeName, monad),
            Bind2("SelectMany", genericTypeName("B"), genericTypeName, monad),
            Bind2("SelectMany", monad.GenericTypeName("B"), genericTypeName, monad),
            Bind2Async("SelectMany", genericTypeName("B"), genericTypeName, monad),
            Bind2Async("SelectMany", monad.GenericTypeName("B"), genericTypeName, monad),
        ];

        IEnumerable<MethodGenerationInfo> MapMethods() =>
        [
            Map("Map", genericTypeName, monad),
            MapAsync("Map", genericTypeName, monad),
            Map("Select", genericTypeName, monad),
            MapAsync("Select", genericTypeName, monad),
        ];
    }

    private static MethodGenerationInfo Bind(string name, string fnReturnType, Func<string, string> genericTypeName, MonadInfo chainedMonad) => new(
        genericTypeName("B"),
        ["A", "B"],
        [
            new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
            new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
        ],
        name,
        $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")})ma)", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}"
    );

    private static MethodGenerationInfo Bind2(string name, string fnReturnType, Func<string, string> genericTypeName, MonadInfo chainedMonad) => new(
        genericTypeName("C"),
        ["A", "B", "C"],
        [
            new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
            new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
            new ParameterGenerationInfo(Types.Func("A", "B", "C"), "selector"),
        ],
        name,
        $"ma.SelectMany([{Constants.DebuggerStepThroughAttribute}](a) => (({genericTypeName("B")})fn(a)).Map([{Constants.DebuggerStepThroughAttribute}](b) => selector(a, b)))"
    );

    private static MethodGenerationInfo Bind2Async(string name, string fnReturnType, Func<string, string> genericTypeName, MonadInfo chainedMonad) => new(
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
    );

    private static MethodGenerationInfo BindAsync(string name, string fnReturnType, Func<string, string> genericTypeName, MonadInfo chainedMonad) => new(
        Types.Task(genericTypeName("B")),
        ["A", "B"],
        [
            new ParameterGenerationInfo(Types.Task(genericTypeName("A")), "ma", true),
            new ParameterGenerationInfo(Types.Func("A", fnReturnType), "fn"),
        ],
        name,
        $"{chainedMonad.BindMethod.Invoke(["A", "B"], [$"(({chainedMonad.GenericTypeName("A")})(await ma))", $"[{Constants.DebuggerStepThroughAttribute}](a) => fn(a)"])}",
        true
    );

    private static MethodGenerationInfo Lift(Func<string, string> genericTypeName, MonadInfo chainedMonad, MonadInfo outerMonad, MonadInfo innerMonad) => new(
        genericTypeName("A"),
        ["A"],
        [new ParameterGenerationInfo(outerMonad.GenericTypeName("A"), "ma")],
        "Lift",
        $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], ["ma", $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}"
    );

    private static MethodGenerationInfo LiftAsync(Func<string, string> genericTypeName, MonadInfo chainedMonad, MonadInfo outerMonad, MonadInfo innerMonad) => new(
        Types.Task(genericTypeName("A")),
        ["A"],
        [new ParameterGenerationInfo(Types.Task(outerMonad.GenericTypeName("A")), "ma")],
        "Lift",
        $"{outerMonad.BindMethod.Invoke(["A", $"{innerMonad.GenericTypeName("A")}"], ["(await ma)", $"[{Constants.DebuggerStepThroughAttribute}](a) => {chainedMonad.ReturnMethod.Invoke(["A"], ["a"])}"])}",
        true
    );

    private static MethodGenerationInfo Map(string name, Func<string, string> genericTypeName, MonadInfo monad) => new(
        genericTypeName("B"),
        ["A", "B"],
        [
            new ParameterGenerationInfo(genericTypeName("A"), "ma", true),
            new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
        ],
        name,
        $"ma.{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke(["B"], ["fn(a)"])})"
    );

    private static MethodGenerationInfo MapAsync(string name, Func<string, string> genericTypeName, MonadInfo monad) => new(
        Types.Task(genericTypeName("B")),
        ["A", "B"],
        [
            new ParameterGenerationInfo(Types.Task(genericTypeName("A")), "ma", true),
            new ParameterGenerationInfo(Types.Func("A", "B"), "fn"),
        ],
        name,
        $"(await ma).{monad.BindMethod.Name}([{Constants.DebuggerStepThroughAttribute}](a) => {monad.ReturnMethod.Invoke(["B"], ["fn(a)"])})",
        true
    );

    private static MethodGenerationInfo Return(Func<string, string> genericTypeName, MonadInfo chainedMonad) => new(
        genericTypeName("A"),
        ["A"],
        [new ParameterGenerationInfo("A", "a")],
        chainedMonad.ReturnMethod // implicit cast
    );

    private static MethodGenerationInfo ReturnAsync(Func<string, string> genericTypeName, MonadInfo chainedMonad) => new(
        Types.Task(genericTypeName("A")),
        ["A"],
        [new ParameterGenerationInfo(Types.Task("A"), "a")],
        chainedMonad.ReturnMethod.Name,
        chainedMonad.ReturnMethod.Invoke(["A"], ["(await a)"]),
        true
    );
}
