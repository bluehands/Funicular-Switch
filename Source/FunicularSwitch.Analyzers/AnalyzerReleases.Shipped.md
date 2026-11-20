; Shipped analyzer releases
; https://github.com/dotnet/roslyn/blob/main/src/RoslynAnalyzers/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 1.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
FS0001 | Usage Opportunity | Info | MatchNullAnalyzer, use .Map(...).GetValueOrDefault() over .Match(..., () => null)
