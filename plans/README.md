# Plans

Plans describe work before it is done. Once implemented, the code, tests and docs are the source of truth.

- [open/](open/): planned or deferred work. Read a plan when your task relates to it.
- [done/](done/): implemented plans, kept as history. Read one only when you change code in its scope (see `Scope:` at the top of each plan).

When a plan is implemented, move it to `done/` and add a header with status, scope and the decisions worth knowing.

## Open

| Plan | Summary |
|---|---|
| [non-boxing-own-unions.md](open/non-boxing-own-unions.md) | Own generated non-boxing union types. Deferred until there is real demand. |
| [analyzer-match-to-native-switch.md](open/analyzer-match-to-native-switch.md) | Hidden suggestion to rewrite `Match` on closed unions into a native switch. Low priority idea. |
| [generator-unionstype-derived-type-scan.md](open/generator-unionstype-derived-type-scan.md) | Every `[UnionType]` transform scans all syntax trees. Existing on main. |
| [generator-resulttype-symbol-in-model.md](open/generator-resulttype-symbol-in-model.md) | Plain values instead of a symbol in the `[ResultType]` model. Existing on main. |

## Done

| Plan | Summary |
|---|---|
| [csharp15-union-integration.md](done/csharp15-union-integration.md) | C# 15 `closed` hierarchies and native `union` support, analyzers FS0002 to FS0007, Roslyn 5.12 minimum (release 5.0). |
