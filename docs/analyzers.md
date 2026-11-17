# FunicularSwitch analyzers

| ID | Severity | Needs | Description | Code fix |
|---|---|---|---|---|
| FS0001 | Info | | `Match(..., () => null)` on Option/Result: use `Map(...).GetValueOrDefault()` | yes |
| FS0002 | Info | C# 15 | Abstract `[UnionType]` class/record can be `closed`. Not reported if a derived case has a type parameter missing from its base specification (compiler rule for closed bases). | `abstract` -> `closed` on all partial declarations; optional second action adds `[JsonPolymorphic(InferClosedTypePolymorphism = true)]` when System.Text.Json supports it and no JSON polymorphism attributes exist |
| FS0003 | Warning | C# 15 | Closed union has an abstract intermediate type that is not `closed`: native `switch` sees it as one case while `Match` sees its derived cases | make intermediate `closed` |
| FS0004 | Info | C# 15 | Direct case of a closed union is neither `sealed` nor `closed` and can be extended outside the assembly | add `sealed` |
| FS0005 | Info | C# 15, STJ | Closed `[UnionType]` with `[JsonPolymorphic]` + manual `[JsonDerivedType]` list: use `InferClosedTypePolymorphism = true` | none |

All C# 15 diagnostics are only reported when the compilation's language version is C# 15 or preview.

## How `closed` is detected

`LanguageFeatures.SupportsClosed(compilation)` checks the language version (`LanguageVersion` values are `major * 100`, so C# 15 is `1500`; `Preview`/`Latest` are mapped with `MapSpecifiedToEffectiveVersion`).
`LanguageFeatures.IsClosed(type)` uses the public Roslyn 5.12 API `ITypeSymbol.IsClosed` (works for source and metadata symbols), with a `ClosedAttribute` fallback.
The compiler synthesizes `ClosedAttribute`, so the attribute is not required to be in the reference assemblies.

## Testing C# 15 sources

The test host runs on net10, so `VerifyAnalyzer` loads the net11 reference assemblies from the SDK targeting pack
(`<dotnet root>/packs/Microsoft.NETCore.App.Ref/11.*/ref/net11.0`) and parses with `LanguageVersion.Preview` by default. The .NET 11 SDK must be installed.
