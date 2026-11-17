# FunicularSwitch and C# 15

FunicularSwitch 5.0 works with the C# 15 features `closed` classes and `union` declarations. All C# 15 behavior is opt-in and only active when the compilation uses C# 15 (or preview) and references .NET 11 (the support types must exist). Older language versions behave as before.

`closed` needs both C# 15 and the compiler support types (`IsClosedTypeAttribute` etc., part of .NET 11). The compiler does not synthesize them: `closed` on netstandard2.0 or net10 and older fails with CS0656 even with `LangVersion` 15, so generators and analyzers treat such compilations as not supporting `closed`.

## Requirements (5.0 breaking changes)

- **.NET 11 SDK / VS 2026 is required** (Roslyn 5.12 or newer). This applies even if you build for older target frameworks or language versions. Stay on 4.x for older toolchains.
  This is a hard minimum, there are no analyzer folders for older compilers. The `FunicularSwitch.Generators` and `FunicularSwitch.Analyzers` packages ship a `buildTransitive` target that fails the build with error `FUNICULAR001` and an explanatory message if the compiler is older (it checks `$(CompilerApiVersion)`, e.g. `roslyn5.12`). Set `<FunicularSwitchSkipCompilerCheck>true</FunicularSwitchSkipCompilerCheck>` to suppress the check (the analyzers then fail to load with CS8032).
- The analyzer suggestions (FS0001-FS0007) come from the separate `FunicularSwitch.Analyzers` package. Generators alone don't report them.
- `Match`/`Switch` of generated union types now throw `ArgumentNullException` instead of `ArgumentException` for a null receiver. `ArgumentNullException` derives from `ArgumentException`, so existing `catch` blocks keep working.

## Closed unions vs. `Match`

A `[UnionType]` on a `closed` base gives you two equivalent ways to consume the union:

```csharp
[UnionType]
public abstract partial record Shape;           // before
[UnionType]
public closed partial record Shape;             // C# 15
public sealed record Circle(double R) : Shape;
public sealed record Square(double S) : Shape;

// FunicularSwitch Match: lambdas, async and Task<> overloads
var area = shape.Match(c => Math.PI * c.R * c.R, s => s.S * s.S);

// Native switch: exhaustiveness checked by the compiler, no lambdas
var area2 = shape switch
{
    Circle c => Math.PI * c.R * c.R,
    Square s => s.S * s.S,
};
```

Use `Match` for expression-style code, chaining and async lambdas (`Task<Shape>.Match(...)`). Use the native switch for pattern matching and `ref`/`await` freedom inside arms.

For a closed union the generated `Match`/`Switch` have **no throwing default arm**, so the compiler verifies the generated code. A null receiver throws `ArgumentNullException`. If the hierarchy is not fully closed (for example a non-closed abstract intermediate type), the default arm is kept.

## How to opt in

Analyzer suggestions (see [analyzers.md](analyzers.md)) appear only under C# 15 with net11 references:

| ID | Suggests |
|---|---|
| FS0002 | Make an abstract `[UnionType]` class/record `closed` (code fix; optional second fix adds `[JsonPolymorphic(InferClosedTypePolymorphism = true)]`) |
| FS0003 | Make a non-closed abstract intermediate type `closed` (warning) |
| FS0004 | Make a direct case `sealed` (or `closed`) |
| FS0005 / FS0006 | Use `InferClosedTypePolymorphism` instead of a manual `[JsonDerivedType]` list / a case is missing from that list |
| FS0007 | Make an abstract `[ResultType]` class `closed` |

### Closed `[UnionType]` bases

Replace `abstract` with `closed` (the FS0002 code fix does this on all partial declarations). Generic bases are supported, but every type parameter of a derived case must appear in the base type specification (compiler rule; FS0002 is not reported otherwise).

### Closed result types

Result types are not closed automatically, because `abstract` and `closed` can't be combined. Declare your partial class as `closed` yourself (FS0007 offers this):

```csharp
[ResultType]
public closed partial class MyResult<T>;
```

The generated base is then closed too, and you can switch natively over the nested `Ok_` and `Error_` cases. The generated `switch` expressions lose their throwing default arm. Generated `switch` statements (`Bind`/`Map` overloads) keep an unreachable `default: throw`, because the compiler's flow analysis still requires it.

## .NET 11 consumer with an older language version

The net11.0 asset of `FunicularSwitch` contains the closed `Result<T>`. Consumers on net11.0 with `LangVersion` 14 or older still build and run fine: the compiler ignores the closed marker, so `Match`, `Ok_`/`Error_` pattern matching and everything else work as before. Only native `switch` expressions over `Result<T>` get the usual non exhaustive warning (CS8509) without a default arm. Generators and analyzers don't opt into any C# 15 behavior in that case.

## Native `union` declarations

`[UnionType]` may be applied to C# 15 `union` declarations (and to structs). It generates `Match`/`Switch` (sync, async, `Task<>`) extension methods:

```csharp
[UnionType]
public union Pet(Cat, Dog);

pet.Match(cat => "meow", dog => "woof");
```

- Cases are the union's case types, in declared order (not derived types).
- **Null/default routing:** a default union value has no content (`Value == null`).
  - If any case type is nullable (`int?`, `string?`), the null value is routed to the **first** nullable case, with `null` as argument.
  - Otherwise `Match`/`Switch` throw `InvalidOperationException("Union has no value")`.
- No static factories are generated; implicit union conversions cover that.
- `[UnionType]` on a struct that is neither a `union` declaration nor a `[Union]` struct is reported as FUN0017 and ignored.
- A closed `[UnionType]` base can itself be a case of a native union, e.g. `union Response(MyResult<int>, Timeout)`.

### Hand-written `[Union]` structs

Native `union` declarations always box value-type cases. If you need non-boxing storage, hand-write a struct with the `[Union]` attribute (Microsoft documents the pattern) and add `[UnionType]` to get `Match`/`Switch`. FunicularSwitch does not generate non-boxing unions.

## JSON

Use a closed base with inferred polymorphism:

```csharp
[UnionType]
[JsonPolymorphic(InferClosedTypePolymorphism = true)]
public closed partial record Shape;
```

This serializes with a `$type` discriminator and needs no `[JsonDerivedType]` list (FS0005/FS0006 flag manual lists).

**Why our base types are not `[Union]`:** a spike showed `[Union]` can't be applied to interfaces, and System.Text.Json then treats the base as a union with no discoverable cases, which breaks existing `[JsonPolymorphic]`/`[JsonDerivedType]` setups, including `closed` + `InferClosedTypePolymorphism`. Microsoft recommends closed hierarchies for this shape.

## Feature matrix

| Union shape | C# ≤ 14 | C# 15 (.NET 11 refs) |
|---|---|---|
| Abstract class/record `[UnionType]` | `Match`/`Switch`, factories | + FS0002 suggestion |
| Closed class/record `[UnionType]` | n/a | default-less `Match`/`Switch`, native exhaustive switch, STJ inference, FS0003-FS0006 |
| Interface / struct-case `[UnionType]` | `Match`/`Switch` | unchanged (no `closed` support) |
| `[ResultType]` abstract | as before | + FS0007 |
| `[ResultType]` `closed partial` | n/a | closed generated base, native switch over `Ok_`/`Error_` |
| Native `union` + `[UnionType]` | n/a | `Match`/`Switch` |
| Hand-written `[Union]` struct + `[UnionType]` | n/a | `Match`/`Switch` |
| `Option<T>` (runtime library) | unchanged | unchanged |
| `Result<T>` (runtime library) | unchanged | closed in the net11.0 asset of `FunicularSwitch` (native exhaustive switch over `Result<T>.Ok_`/`Error_`); the netstandard2.0 asset is unchanged |
