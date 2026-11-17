# Plan: Plain values instead of a symbol in the `[ResultType]` model

Status: draft. Date: 2026-11-17. Scope: generator caching and memory only, no change to generated output. Out of scope for the C# 15 branch; the issue exists on `main`.

## 1. Problem

`ResultTypeSchema` (`Source/FunicularSwitch.Generators/ResultType/ResultTypeSchema.cs`, line 15) stores the error type as `SymbolWrapper<INamedTypeSymbol>`. The default error type in `ResultTypeGenerator` is a `SymbolWrapper` too (`stringSymbol`, `Source/FunicularSwitch.Generators/ResultTypeGenerator.cs`, around line 62).

- Equality of the wrapper uses the full type name only.
- The wrapper holds the symbol, so the pipeline cache keeps the **old compilation alive**. Roslyn guidance is to never put symbols in incremental models, because memory use grows in IDEs.

## 2. Is cached output ever stale? (investigated)

Short answer: **no, not today.** The generator reads three facts from the symbol in `ResultType/Generator.cs` (lines 34 to 50, 78, 125):

| Fact | Used for | Part of the name? |
|---|---|---|
| `Name` | template replacement | yes |
| namespace | `using` for the error type | yes (full name) |
| `IsValueType` | `.Value` in the aggregate extension methods | **no** |

`IsValueType` can change without the name changing (class to struct). It is only passed to `GenerateAggregateExtensionMethods`, and the template placeholder `//generated aggregate extension methods` no longer exists in `Source/FunicularSwitch.Generators/Templates`, so the value never reaches the output. Merge and exception-to-error methods come from the separate compilation provider as plain values, so they are not affected.

Test `Changing_error_type_from_class_to_struct_does_not_leave_stale_output` in `IncrementalCachingSpecs.cs` shows that incremental and from-scratch output are equal in this case, and `Adding_merge_error_method_updates_result_output` shows merge changes are picked up.

This is a latent bug: as soon as anyone makes the generator emit another symbol-derived fact (for example `IsValueType`, nullability, accessibility), the cache would silently go stale. The memory problem exists today.

## 3. Proposal

Replace the wrapper with plain values in the schema:

```csharp
sealed record ErrorTypeInfo(string Name, string FullNameWithNamespace, string? Namespace, bool IsValueType);
```

- `ResultTypeSchema.ErrorType` becomes `ErrorTypeInfo?` and takes part in equality (records give this for free).
- Build the info in the transform, where the symbol is available.
- Replace `defaultErrorType` (the `string` symbol) by a static `ErrorTypeInfo` for `System.String`, so the `compilation.Select` no longer needs `SymbolWrapper`.
- `ResultType/Generator.Emit` and `ResultTypeGenerator.Execute` use the plain fields (`FullNameWithNamespace` for the merge lookup).

## 4. Steps

1. Keep the existing tests; they act as the safety net.
2. Add `ErrorTypeInfo` and fill it in the transform; change `ResultTypeSchema` equality accordingly.
3. Change `Generator.Emit` / `Execute` to use the info instead of the symbol; remove `SymbolWrapper` usage in the result type generator.
4. Check that `SymbolWrapper` is still needed elsewhere (`EnumTypeGenerator` uses it too, see `Source/FunicularSwitch.Generators/EnumTypeGenerator.cs`); open a follow-up if so.

## 5. Test

- Snapshot tests stay identical.
- Keep the tracking tests in `Source/Tests/FunicularSwitch.Generators.Test/IncrementalCachingSpecs.cs`.
- Add a test that changes only `IsValueType` and makes the output depend on it (or asserts that the schema is *Modified*, for example via a `TrackedSteps` check on the transform step), so a new symbol-derived fact cannot go stale unnoticed.
- Optionally add a memory check: after a second run, a weak reference to the first compilation can be collected.