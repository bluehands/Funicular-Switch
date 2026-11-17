# FunicularSwitch analyzers

| ID | Severity | Needs | Description | Code fix |
|---|---|---|---|---|
| FS0001 | Info | | `Match(..., () => null)` on Option/Result: use `Map(...).GetValueOrDefault()` | yes |
| FS0002 | Info | C# 15 | Abstract `[UnionType]` class/record can be `closed`. Not reported if a derived case has a type parameter missing from its base specification (compiler rule for closed bases). | `abstract` -> `closed` on all partial declarations; optional second action adds `[JsonPolymorphic(InferClosedTypePolymorphism = true)]` when System.Text.Json supports it and no JSON polymorphism attributes exist |
| FS0003 | Warning | C# 15 | Closed union has an abstract intermediate type that is not `closed`: native `switch` sees it as one case while `Match` sees its derived cases | make intermediate `closed` |
| FS0004 | Info | C# 15 | Direct case of a closed union is neither `sealed` nor `closed` and can be extended outside the assembly | add `sealed` |
| FS0005 | Info | C# 15, STJ | Closed `[UnionType]` with `[JsonPolymorphic]` + manual `[JsonDerivedType]` list: use `InferClosedTypePolymorphism = true` | none |
| FS0006 | Info | C# 15, STJ | Concrete case of a closed union that is missing from a manual `[JsonDerivedType]` list (reported together with FS0005) | none |
| FS0007 | Info | C# 15 | Abstract `[ResultType]` class can be `closed` so native switches over the generated `Ok_`/`Error_` cases are exhaustive | `abstract` -> `closed` on all partials |

All `closed` suggestions (FS0002-FS0007) are only reported when the compilation can actually use `closed`: language version C# 15 or preview **and** the support types `System.Runtime.CompilerServices.IsClosedTypeAttribute` and `CompilerFeatureRequiredAttribute` are referenced (net11 has them; netstandard2.x and older TFMs don't).

## How `closed` is detected

`LanguageFeatures.`LanguageFeatures.SupportsClosed(compilation)` checks the language version (`LanguageVersion` values are `major * 100`, so C# 15 is `1500`; `Preview`/`Latest` are mapped with `MapSpecifiedToEffectiveVersion`) and that the closed support types above exist via `GetTypeByMetadataName`.
`LanguageFeatures.IsClosed(type)` uses the public Roslyn 5.12 API `ITypeSymbol.IsClosed` (works for source and metadata symbols), with a `ClosedAttribute` fallback.
The compiler does NOT synthesize the support attributes, so consumers on netstandard2.0 or pre-net11 targets get no suggestion.

## Testing C# 15 sources

The test host runs on net10, so `VerifyAnalyzer` loads the net11 reference assemblies from the SDK targeting pack
(`<dotnet root>/packs/Microsoft.NETCore.App.Ref/11.*/ref/net11.0`) and parses with `LanguageVersion.Preview` by default. The .NET 11 SDK must be installed.

