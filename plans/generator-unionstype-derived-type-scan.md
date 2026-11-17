# Plan: Syntax-first derived type scan for `[UnionType]`

Status: draft. Date: 2026-11-17. Scope: generator performance only, no change to generated output. Out of scope for the C# 15 branch; the issue exists on `main`.

## 1. Problem

For every `[UnionType]` class or record, the transform in `UnionTypeGenerator` calls `Parser.GetUnionTypeSchema` (`Source/FunicularSwitch.Generators/UnionType/Parser.cs`, around line 39). That method does this:

- It loops over **every syntax tree** of the compilation (`compilation.SyntaxTrees`).
- It requests a semantic model for each tree.
- It walks the whole tree with `FindConcreteDerivedTypesWalker` (same file, around line 282) to find types derived from the union base.

`ForAttributeWithMetadataName` re-runs the transform for each union whenever the compilation changes. So **every keystroke in any file** costs about *unions x files* tree walks and semantic model lookups. The output stays cached, but the work to find that out is not free. It grows with solution size, which is exactly where consumers feel IDE lag.

## 2. Proposal

Split the work into a cheap syntax step and a small semantic step.

1. **Candidate provider.** Add `context.SyntaxProvider.CreateSyntaxProvider` with a predicate that accepts only `ClassDeclarationSyntax` / `RecordDeclarationSyntax` that have a `BaseList`. Most types fail this check without any semantic work. The transform returns the candidate's symbol (or a small value with its base type name and location).
2. **Per-union combine.** For each union, take the collected candidates and keep those whose base type resolves to the union symbol. The candidates are collected once and shared by all unions. Only the semantic check "does the base type equal this union" runs per union, and only over the candidates, not over all trees.
3. **Keep the schema as is.** `GetUnionTypeSchema` receives the derived types from step 2 instead of walking the trees. Case ordering, nested/abstract handling and the closed check (`AllCovered`) stay unchanged.

Indirect derivation (`A : B`, `B : Union`) works because every class with a base list is a candidate and `FindConcreteDerivedTypesWalker` semantics are kept for the base-chain check.

## 3. Tradeoffs

- **Plus:** work per edit drops from *unions x files* to roughly *candidate count*. Edits in files without base lists cost almost nothing.
- **Plus:** the candidate list is computed once, not once per union.
- **Minus:** derived types in other files still require the semantic check, so a change to any class with a base list re-runs the candidate transform. This is much cheaper than today but not zero.
- **Minus:** the candidates must be plain values (names, locations, case info), not symbols. Holding symbols keeps old compilations alive and would break caching (see `generator-resulttype-symbol-in-model.md`). This makes the models larger.
- **Minus:** more pipeline code and a second path to keep in sync with the walker. Remove the walker only once the new path is proven.

## 4. Steps

1. Add the tracking tests below first and record today's behavior (they pass, but the semantic work count is high).
2. Add the candidate provider and the per-union combine behind the existing output. Snapshots must stay identical.
3. Switch `GetUnionTypeSchema` to the new input and delete the tree loop and `FindConcreteDerivedTypesWalker` if unused.
4. Add an `Execute` count test (see below) to prevent regressions.

## 5. Test strategy

- All existing snapshot tests stay unchanged (they prove identical output).
- Reuse `Source/Tests/FunicularSwitch.Generators.Test/IncrementalCachingSpecs.cs` (`trackIncrementalGeneratorSteps: true`):
  - an unrelated edit and a comment/whitespace edit leave all outputs `Cached`/`Unchanged`;
  - adding a derived case in another file modifies only that union's output;
  - adding a derived case to union A does not modify union B.
- Add a test that counts transform runs, for example with a counter in a test-only wrapper or by checking `TrackedSteps` of the new candidate step, with many files and several unions. An edit in a file with no base list should not re-run the per-union combine for unchanged candidates.
- Add cases for derivation across files, nested derived types, generic unions, indirect derivation and partial declarations spread over several files.