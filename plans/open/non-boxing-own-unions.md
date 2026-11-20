# Plan: Own non-boxing union types

Status: deferred. Date: 2026-11-17. Origin: former workstream C.2 of the C# 15 union integration (`plans/done/csharp15-union-integration.md`). Revisit only on real demand, for example issues or benchmarks showing allocation pressure from boxed value-type cases.

## 1. Why this is deferred

`Match`/`Switch` on native `union` declarations is implemented. The remaining benefit of our own union declaration is small:

| Aspect | Native `union` + `Match`/`Switch` | Own generated union |
|---|---|---|
| Exhaustive switch, conversions, pattern matching, STJ | yes | yes |
| `Match`/`Switch` API | yes | yes |
| Value-type cases | boxed (allocation) | inline, no allocation |
| Named cases (`count:` vs `int:`) | no | yes |
| Generated equality / `ToString` | default struct behavior | yes |
| Benefits from future compiler and runtime improvements | for free | must keep up |
| Cost to us | small | full generator, diagnostics, tests, maintenance |

- Most unions hold reference types (records). For those, storage is identical, so there's no benefit.
- Boxing matters only in hot paths. There, the union pattern can be hand-written in about 20 lines (documented by Microsoft). Such hand-written `[Union]` structs already get `Match`/`Switch` through `[UnionType]` or `[assembly: ExtendUnions]`.
- FunicularSwitch-style named cases are already covered by closed hierarchies.
- The built-in `union` may still evolve (storage, generic `IUnion<T>`), which could make our version redundant.

## 2. Preferred design if revisited

Partial constructors declare the cases:

```csharp
[UnionType]
public readonly partial struct Payload
{
    public partial Payload(int count);
    public partial Payload(string? text);
    public partial Payload(Message message);
}
```

- It mirrors the language's union pattern exactly.
- Nullable reference cases can be written directly.
- Parameter names give case names.
- It allows per-case docs.
- `[UnionType]` on structs has no conflicting meaning today (hand-written `[Union]` structs and native unions are the only accepted shapes, FUN0017 reports others).

The generator would emit tag-based storage, `Value`/`HasValue`/`TryGetValue`, equality, `ToString` and `Match`/`Switch`.

## 3. Rejected alternatives

- Adding non-boxing members to a native `partial union` declaration: verified impossible with the .NET 11 compiler.
  - All parts must be `union` declarations (CS0261).
  - Instance fields and auto-properties aren't allowed (CS9373).
  - Explicit public single-parameter constructors aren't allowed (CS9374).
  - The compiler always stores contents in a single `object` backing field. Methods such as `HasValue`/`TryGetValue` compile, but can only unbox an already boxed value.

  Non-boxing therefore always needs a hand-written `[Union]` struct.
- `[UnionOf<T1..Tn>]`: arity limit, no nullable reference type arguments, derived names.
- `[UnionType(Cases = new[] { typeof(...) })]`: same issues, and it overloads the meaning of `UnionType`.
- Settings on a native `union` declaration: impossible, because the compiler owns its storage.

## 4. If we start

1. Collect evidence first: an issue, or a benchmark of a realistic hot path with boxed value-type cases.
2. Check what the compiler and runtime offer by then (storage changes, `IUnion<T>`).
3. Prototype the partial-constructor design in a spike outside the repo, including STJ and equality behavior.
4. Then write a proper implementation plan (generator, FUN diagnostics, snapshot and consumer tests, docs).
