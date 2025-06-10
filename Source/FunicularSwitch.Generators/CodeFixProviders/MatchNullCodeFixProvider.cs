using System.Collections.Immutable;
using System.Composition;
using FunicularSwitch.Generators.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace FunicularSwitch.Generators.CodeFixProviders;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MatchNullCodeFixProvider)), Shared]
public class MatchNullCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [MatchNullAnalyzer.DiagnosticId];

    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        throw new NotImplementedException();
    }
}