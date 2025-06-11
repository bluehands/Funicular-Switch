using System.Collections.Immutable;
using System.Composition;
using FunicularSwitch.Generators.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace FunicularSwitch.Generators.CodeFixProviders;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MatchNullCodeFixProvider)), Shared]
public class MatchNullCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [MatchNullAnalyzer.DiagnosticId];

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        throw new NotImplementedException();
    }
}