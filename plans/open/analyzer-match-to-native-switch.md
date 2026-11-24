# Plan: Analyzer to suggest native switch for `Match` on closed unions

Status: idea, low priority. Date: 2026-11-17. Origin: former item 6.1 of the C# 15 union integration (`plans/done/csharp15-union-integration.md`).

## 1. Idea

For unions with a `closed` base (and cases that are `sealed` or `closed`), the compiler checks native switches for exhaustiveness. A call such as

```csharp
shape.Match(circle => circle.Radius, square => square.Side)
```

could be offered as a refactoring to

```csharp
shape switch { Circle circle => circle.Radius, Square square => square.Side }
```

as a hidden (IDE-only) suggestion with a code fix, in `FunicularSwitch.Analyzers` / `.CodeFixProviders`. It would get the next free `FS00xx` ID (FS0001 to FS0007 are taken, see `docs/analyzers.md`).

## 2. Risks and open questions

- It is stylistic. `Match` stays the supported main API, so the suggestion must be hidden or opt-in (severity `Hidden`, no build warnings).
- Named arguments, a different `CaseOrder`, async/`Task<>` variants and lambdas with statement bodies make the rewrite non trivial. Limit the first version to sync `Match` with simple lambdas.
- It changes the null behavior: `Match` throws `ArgumentNullException` for a null receiver, while a native switch expression without a null arm throws `SwitchExpressionException`. The fix must say so or add a null arm.
- Capturing lambdas become inline code. Variable name clashes need handling.
- Needs C# 15 and the closed support types, same gate as FS0002 (`ClosedSupport`).

## 3. Decision

Do nothing unless users ask for it. If started, write the implementation plan first and include a test matrix for the cases above.
