# Plan: Integrate FunicularSwitch union types with C# 15 / .NET 11

Status: draft · Date: 2026-11-17 · Scope: everything **except** making our base types `[Union]` types ("option B").

## 1. Background and decisions

C# 15 ships two related features:

| Feature | Shape | Fit for FunicularSwitch |
|---|---|---|
| `closed` classes (closed hierarchies) | abstract base + derived cases in the same assembly; native `switch` is exhaustive over **direct** subtypes | Same model as our `[UnionType]` on abstract classes/records |
| `union` / `[Union]` (type unions) | wrapper type with `Value`, one constructor or `Create` per case | Different model (case types don't share a base); new feature area |

Option B (adding `[Union]`/`IUnion` to our base types) was **rejected** after a spike (`%TEMP%\opencode\unionspike`):

- `[Union]` can't be applied to interfaces (CS0592).
- A public `Value => this` breaks record `ToString()` (stack overflow) and STJ serialization of cases (cycle). Explicit implementation fixes that.
- System.Text.Json:
  - It treats the base as `JsonTypeInfoKind.Union`, but finds **no cases** from `IUnionMembers.Create` methods.
  - It **breaks existing `[JsonPolymorphic]`/`[JsonDerivedType]` setups**, including the officially recommended `closed` + `InferClosedTypePolymorphism`.
- Microsoft docs explicitly recommend closed hierarchies with inferred polymorphism for our shape.

**Decision:** embrace `closed` for class/record unions (Workstream A), add `Match`/`Switch` for native type unions (Workstream C.1; own non-boxing unions are deferred, see section 11), and keep existing behavior for interface/struct unions and older language versions.

## 2. Guiding principles

1. **Non-breaking by default.** Every new behavior is gated on:
   - `LanguageVersion >= CSharp15` (or `Preview`), and
   - the required runtime types being present (`System.Runtime.CompilerServices.ClosedAttribute` or `UnionAttribute`), detected by metadata name.
2. **Generator stays `netstandard2.0`. Roslyn is raised to the .NET 11 SDK version** (decided; see 6.3). We use the new symbol APIs directly, such as closed and union detection. We still gate on the consumer's `LanguageVersion`, because a new compiler can still build C# ≤ 14 projects.
3. **Existing generated API stays.** `Match`/`Switch` (sync, async, `Task<>`) and the static factories remain the main ergonomic layer. Native switches are an additional option, not a replacement.
4. **Snapshot tests for every generator change.** We also add net11 consumer projects that prove native exhaustiveness compiles without warnings.

## 3. Workstream 0 – Infrastructure (prerequisite)

| # | Task | Details |
|---|---|---|
| 0.1 | .NET 11 SDK in build/CI | Pin the SDK via `global.json` (with `rollForward`). Update the CI pipeline. |
| 0.2 | Language version helper | Add `LanguageFeatures` in `FunicularSwitch.Generators.Common`:<br>- `SupportsClosed(Compilation)`<br>- `SupportsUnions(Compilation)`<br>Both check `((CSharpParseOptions)tree.Options).LanguageVersion` and `compilation.GetTypeByMetadataName(...)`. Feed this through the incremental pipeline (`CompilationProvider.Select`) so the value is cached. |
| 0.3 | Closed detection | `IsClosed(INamedTypeSymbol)` using the Roslyn symbol API for `closed` (with `ClosedAttribute` on metadata symbols as a fallback). Verify both source and metadata symbols in a test. |
| 0.4 | Test harness | `VerifySourceGenerator` gets an optional `LanguageVersion` and reference set (net11 reference assemblies via `Basic.Reference.Assemblies.Net110` or `Microsoft.NETCore.App.Ref` 11). Keep `Directory.Build.props` test TFM at net9, or add net11 multi-targeting only where needed. |
| 0.5 | Consumer project | New `Source/Tests/FunicularSwitch.Generators.Consumer.Net11` (net11.0, C# 15, `TreatWarningsAsErrors` incl. CS8509/CS8524). It holds closed-union samples and native switches. |

## 4. Workstream A – Closed hierarchies for `[UnionType]`

### A.1 Analyzer: suggest `closed` (new diagnostic)

- **ID:** `FS0002` in `FunicularSwitch.Analyzers` (add constants to `DiagnosticId.cs`). All new analyzers and code fixes in this plan live in `FunicularSwitch.Analyzers` / `.CodeFixProviders` with `FS00xx` IDs. Structural errors found while generating stay `FUN00xx`.
- **Trigger:** all of:
  - `[UnionType]` on an `abstract` class/record,
  - `SupportsClosed` is true,
  - the base is not already `closed`.
- **Severity:** Info / hidden suggestion, so it doesn't break `TreatWarningsAsErrors`.
- **Message:** "Union type 'X' can be declared `closed` to get compiler-checked exhaustive switches and prevent foreign derivations."
- **Not reported for:**
  - interfaces and structs (no `closed` support),
  - non-abstract bases (would change semantics),
  - unions with generic derived types that violate the "type parameters must appear in base" rule (see A.4).

### A.2 Code fix: "Make union type closed"

- **Location:** `FunicularSwitch.Analyzers.CodeFixProviders`.
- **Edit:** replace the `abstract` token with `closed` on **all** partial declarations of the type. Keep trivia. FixAll support (document/project/solution).
- **Option:** a second code action, "Make closed and add `[JsonPolymorphic(InferClosedTypePolymorphism = true)]`", offered only when STJ is referenced and no `JsonPolymorphic`/`JsonDerivedType` exists yet.
- **Tests:** in `FunicularSwitch.Analyzers.Tests`: single declaration, multiple partials, records, generic base, already closed (no diagnostic), LangVersion 14 (no diagnostic).

### A.3 Generator: closed-aware case discovery

Today the parser collects **all concrete types at any depth**. With `closed`, the compiler counts only **direct** subtypes, unless the intermediates are `closed` too. To keep `Match` semantics and native exhaustiveness aligned:

- Diagnostic **"Union case hierarchy not fully closed"** (warning):
  - When: the base is `closed` and an intermediate abstract (or non-sealed, case-bearing) type between the base and a case is not `closed`.
  - Code fix: make the intermediate type `closed`.
- Diagnostic **"Union case can be extended outside the assembly"** (info, optional):
  - When: the base is `closed` and a direct case is neither `sealed` nor `closed`.
  - Why it matters: `Match` stays correct because foreign subtypes still match the case, but this is worth knowing.
  - Code fix: add `sealed`.
- Generated emission is unchanged. Case ordering (`CaseOrder`) and naming are unaffected.

### A.4 Generator: generic constraint check

The rule: for a `closed` base, every type parameter of a derived case must appear in the base type specification.

- The compiler already reports this, but the A.1 analyzer must **not suggest** `closed` when the rule would be violated.
- Add a snapshot test for a generic base with the cases `Leaf<T>`/`Node<T>` (OK) and `Constant<U> : Tree<int>` (suggestion suppressed).

### A.5 Generator: emit native exhaustive switches for closed unions

When the union base is closed (and nullable is enabled):

- Generate `Match`/`Switch` bodies **without the throwing default arm**. The compiler then verifies our generated code is exhaustive, so a missing case becomes a compile error in generated code instead of a runtime `ArgumentException`.
- Keep `null` handling explicit:
  - Today a null receiver hits the default and throws `ArgumentException`.
  - New behavior: a `null => throw new ArgumentNullException(nameof(x))` arm.
  - **Decided:** use `ArgumentNullException`. It's the more accurate exception, and `ArgumentNullException` derives from `ArgumentException`, so existing `catch (ArgumentException)` code keeps working. Mention it in the release notes. For consistency, apply the same `null` arm to non-closed unions too (`MatchNullAnalyzer` FS0001 already discourages null receivers).
- Snapshot tests: closed record, closed class, closed generic, multi-level closed.

### A.6 JSON guidance (docs + optional analyzer)

- **Docs:** recommend `closed` + `[JsonPolymorphic(InferClosedTypePolymorphism = true)]`, and show the resulting `$type` JSON.
- **Optional analyzer** (info): `[JsonPolymorphic]` with manual `[JsonDerivedType]` lists on a closed union.
  - Suggest switching to `InferClosedTypePolymorphism`.
  - Or report derived types that are missing from the list. This is cheap, because we already know the cases.

### A.7 Result types

- Generated `Result` base classes (`ResultType.cs`, `ResultTypeWithMerge.cs` templates) have exactly two nested cases (`Ok_`, `Error_`).
- When `SupportsClosed`, emit the result base as `closed` instead of `abstract`:
  - users get native exhaustive `result switch { MyResult<T>.Ok_ ok => …, MyResult<T>.Error_ e => … }`;
  - a third-party subclass of `Result` becomes impossible.
- **Default:** on (decided), unless implementation finds a hard blocker. Possible blockers to check:
  - the generic rule fails for any Result template variant;
  - `closed` behaves unexpectedly with the nested case classes;
  - existing assertion libraries (`FluentAssertions`/`AwesomeAssertions` generators) or user code derive from the result base.

  If a blocker shows up, fall back to an opt-in `[ResultType(Closed = true)]`.
- **Breaking-ness:** low. External derivation from a generated result base isn't a supported scenario. The emitted language feature must be gated on the consumer's LangVersion.
- Check the generic rule. `Ok_`/`Error_` are nested in `MyResult<T>` and use `T`, so the rule should hold. Verify with a snapshot test.
- `FunicularSwitch` runtime library (`Option<T>`, generic `Result<T>`):
  - It targets netstandard2.0 and can't use `closed`.
  - Option: multi-target `net11.0` and declare the types `closed` there via `#if NET11_0_OR_GREATER`.
  - Evaluate this separately (6.2).

## 5. Workstream C – Native type unions

### C.1 `Match`/`Switch` for C# 15 `union` declarations

- Allow `[UnionType]` on `union` declarations, i.e. structs carrying `[Union]` after lowering. A source `union` declaration is a `UnionDeclarationSyntax`. The generator needs to recognize it:
  - via the symbol: a struct with `UnionAttribute` + `IUnion`. This is possible because of the Roslyn minimum version (6.3).
- **Case discovery:** the union creation members, i.e.
  - single-parameter public constructors, or
  - `IUnionMembers.Create(...)` methods.

  Not derived types.
- **Emission:** the same `Match`/`Switch` family (sync, async, `Task<>`). The body is a `switch` on the union value itself (the compiler unwraps it). Parameter names come from the case type names (`int` → `@int`? Define the naming rules for keywords, generics, arrays and nullable cases).
- **Null/default handling:**
  - A default struct union has `Value == null`.
  - Nullable case types (`string?`, `int?`) also match null.
  - Rule: if any case is nullable, null is routed to the first nullable case. Otherwise generate `null => throw new InvalidOperationException("Union has no value")`.
  - Document this.
- **Static factories:** not needed (implicit union conversions exist). Skip by default.

### C.2 Generated non-boxing custom unions → deferred (see section 11)

### C.3 Interop between our unions and native unions

- Nothing to generate. Document that a `closed` `[UnionType]` base can itself be a case of a native `union` (e.g. `union Response(MyResult<int>, Timeout)`).

## 6. Cross-cutting

### 6.1 Analyzer for existing `Match` calls

Optional, low priority:

- **Idea:** for closed unions, suggest converting simple `x.Match(a => ..., b => ...)` calls into native switch expressions.
- **Risk:** stylistic, so it should be opt-in or hidden.

### 6.2 Runtime library (`FunicularSwitch`, `FunicularSwitch.Generic`)

- Evaluate multi-targeting `net11.0`:
  - `closed` `Option<T>`/`Result<T>` bases;
  - `[JsonPolymorphic(InferClosedTypePolymorphism = true)]`, if JSON support is desired.
- Watch the binary compatibility of switching `abstract` → `closed` in a multi-targeted assembly. It adds `ClosedAttribute` metadata, so the IL stays abstract.

### 6.3 Roslyn minimum version (decided)

- Bump `Microsoft.CodeAnalysis.CSharp` (generators, analyzers, code fixes) to the version shipped with the .NET 11 SDK.
- The package then requires VS 2026 / .NET 11 SDK or newer. This is a **major version** (5.0), stated clearly in the README and release notes. Users on older toolchains stay on 4.x.
- No multi-targeted analyzer folders.
- Update test packages (`Verify.SourceGenerators`, the `Microsoft.CodeAnalysis.*` testing packages).
- `FunicularSwitch.Generators.Consumer.StandardMinLangVersion` keeps checking that projects with older *language versions* still build on the new compiler.

### 6.4 Documentation

- README / Tutorial / DocSamples:
  - a "C# 15" section covering closed unions, native switch vs `Match`, JSON with inferred polymorphism, and native type unions with `Match`;
  - a feature matrix (which union shape gets which feature on which language version).
- `RELEASE-NOTES.txt` entries per release.

## 7. Decisions

| # | Topic | Decision |
|---|---|---|
| 1 | Diagnostic location | New analyzers and code fixes go in `FunicularSwitch.Analyzers` / `.CodeFixProviders` with `FS00xx` IDs. Generator-time structural errors stay `FUN00xx`. |
| 2 | Null receiver in `Match`/`Switch` | Throw `ArgumentNullException` (a subclass of `ArgumentException`, so compatible), for all unions (A.5). |
| 3 | Generated non-boxing unions (C.2) | Deferred (section 11). |
| 4 | Roslyn | Hard minimum version = .NET 11 SDK Roslyn. Major version 5.0 (6.3). |
| 5 | Closed Result types | **Outcome:** on by default is blocked (user's `abstract partial` part + generated `closed` part = CS9384; netstandard lacks the closed attributes). Implemented as opt-in: the user declares `closed partial class Result<T>`; FS0007 suggests it with a code fix. The runtime library declares its `Result<T>` closed on net11.0. |

## 8. Release plan

The Roslyn minimum version is a breaking toolchain requirement. So the C# 15 work ships as **5.0**, and 4.x stays the line for older toolchains (bug fixes only).

| Release | Content | Breaking? |
|---|---|---|
| **5.0** | Workstream 0 incl. Roslyn bump; A.1–A.7 (closed suggestion + fix, hierarchy diagnostics, default-less `Match`, `ArgumentNullException`, closed Result opt-in + FS0007, JSON docs); C.1 (`Match` on native unions) | Yes: requires .NET 11 SDK / VS 2026; null receiver exception type (compatible subclass); net11 consumers can no longer derive from runtime `Result<T>` |
| **5.1** | 6.2 runtime library `net11.0` multi-target | No |
| later | 6.1 `Match` → native switch refactoring, C.3 docs polish | No |

## 9. Test matrix (minimum)

| Scenario | C# ≤ 14 | C# 15 |
|---|---|---|
| abstract record/class union | unchanged snapshots | + suggestion diagnostic |
| closed record/class union | n/a | default-less `Match`, no CS8509 in consumer |
| multi-level closed / not-fully-closed | n/a | diagnostic + fix |
| generic closed union | n/a | snapshot + exhaustiveness |
| interface / struct-case union | unchanged | unchanged, no diagnostic |
| Result type | unchanged | closed when user declares it, native switch compiles |
| null receiver on `Match`/`Switch` | `ArgumentNullException` | `ArgumentNullException` |
| native `union` + `[UnionType]` | n/a | `Match`/`Switch` snapshot |
| generated non-boxing union | n/a | deferred |
| STJ closed + `InferClosedTypePolymorphism` | n/a | consumer test round trip |

## 10. Spike reference

Spike code (not part of the repo) is in `%TEMP%\opencode\unionspike`:

- `Program.cs`: compiler behavior
- `Checks.cs`: exhaustiveness and cases missing from `Create`
- `Json.cs` and `Explicit.cs`: STJ findings
- `Probe.cs`: contract API inspection

It documents why option B was dropped.

## 11. Deferred / not planned

### 11.1 Generated non-boxing custom unions (former C.2)

**Status:** deferred. Revisit only on real demand (issues or benchmarks showing allocation pressure from boxed value-type cases).

**Why:** once C.1 provides `Match`/`Switch` on native `union` declarations, the remaining benefit of our own union declaration is small:

| Aspect | Native `union` + C.1 | Own generated union |
|---|---|---|
| Exhaustive switch, conversions, pattern matching, STJ | ✅ | ✅ |
| `Match`/`Switch` API | ✅ (C.1) | ✅ |
| Value-type cases | boxed (allocation) | inline, no allocation |
| Named cases (`count:` vs `int:`) | ❌ | ✅ |
| Generated equality / `ToString` | default struct behavior | ✅ |
| Benefits from future compiler and runtime improvements | ✅ for free | must keep up |
| Cost to us | small | full generator, diagnostics, tests, maintenance |

- Most unions hold reference types (records). For those, storage is identical, so there's no benefit.
- Boxing matters only in hot paths. There, the union pattern can be hand-written in about 20 lines (documented by Microsoft).
- FunicularSwitch-style named cases are already covered by closed hierarchies (workstream A).
- The built-in `union` may still evolve (storage, generic `IUnion<T>`), which could make our version redundant.

**If revisited**, the preferred design is *partial constructors declare the cases*:

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
- `[UnionType]` on structs has no conflicting meaning today.

The generator would emit tag-based storage, `Value`/`HasValue`/`TryGetValue`, equality, `ToString` and `Match`/`Switch`.

Rejected alternatives:
- Adding non-boxing members to a native `partial union` declaration: verified impossible with the .NET 11 compiler.
  - All parts must be `union` declarations (CS0261).
  - Instance fields and auto-properties aren't allowed (CS9373).
  - Explicit public single-parameter constructors aren't allowed (CS9374).
  - The compiler always stores contents in a single `object` backing field. Methods such as `HasValue`/`TryGetValue` compile, but can only unbox an already boxed value.

  Non-boxing therefore always needs a hand-written `[Union]` struct.
- `[UnionOf<T1..Tn>]`: arity limit, no nullable reference type arguments, derived names.
- `[UnionType(Cases = new[] { typeof(...) })]`: same issues, and it overloads the meaning of `UnionType`.
- Settings on a native `union` declaration: impossible, because the compiler owns its storage.
