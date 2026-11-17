## C# 15

With C# 15 and .NET 11, declare union bases as `closed` (instead of `abstract`) to get compiler-checked exhaustive `switch` expressions next to the generated `Match`/`Switch`. `[UnionType]` also works on native `union` declarations. Analyzers FS0002-FS0007 suggest the changes. FunicularSwitch.Generators 5.0 (and FunicularSwitch 8.0, which includes it) requires the .NET 11 SDK (stay on 4.x for older toolchains), and a null receiver in `Match`/`Switch` now throws `ArgumentNullException`. See [docs/csharp15.md](docs/csharp15.md).
