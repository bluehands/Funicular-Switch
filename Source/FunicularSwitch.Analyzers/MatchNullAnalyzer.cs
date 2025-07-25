using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FunicularSwitch.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MatchNullAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId.MatchNull,
        title: "Funicular Switch Library usage opportunity",
        messageFormat: "Use .Map(...).GetValueOrDefault() over .Match(..., () => null)",
        category: "Usage Opportunity",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        
        context.EnableConcurrentExecution();
        
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
    }

    private void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not InvocationExpressionSyntax invocationExpressionSyntax)
        {
            return;
        }
        
        if (context.SemanticModel.GetSymbolInfo(invocationExpressionSyntax, context.CancellationToken)
                .Symbol is not IMethodSymbol methodSymbol)
        {
            return;
        }

        if (methodSymbol.Name != "Match")
        {
            return;
        }

        if (methodSymbol.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGenericsOptions(SymbolDisplayGenericsOptions.None))
            is not ("global::FunicularSwitch.Option" or "global::FunicularSwitch.Result"))
        {
            return;
        }

        var secondParameter = invocationExpressionSyntax.ArgumentList.Arguments[1];
        var isNullArgument = secondParameter.Expression switch
        {
            LambdaExpressionSyntax
            {
                Body: LiteralExpressionSyntax literal
            } when literal.Token.IsKind(SyntaxKind.NullKeyword) || literal.Token.IsKind(SyntaxKind.DefaultKeyword) => true,
            LiteralExpressionSyntax literal when literal.Token.IsKind(SyntaxKind.NullKeyword) || literal.Token.IsKind(SyntaxKind.DefaultKeyword) => true,
            _ => false,
        };
        if (!isNullArgument)
        {
            return;
        }

        var diagnostic = Diagnostic.Create(
            Rule,
            invocationExpressionSyntax.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}