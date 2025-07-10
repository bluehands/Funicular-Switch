using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FunicularSwitch.Analyzers.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace FunicularSwitch.Analyzers.CodeFixProviders;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MatchNullCodeFixProvider)), Shared]
public class MatchNullCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [MatchNullAnalyzer.DiagnosticId];

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.Single();

        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

        var diagnosticNode = root?.FindNode(diagnosticSpan);
        
        if (diagnosticNode is not InvocationExpressionSyntax invocationExpressionSyntax)
        {
            return;
        }
        
        if (invocationExpressionSyntax.Expression is not MemberAccessExpressionSyntax m)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                title: $"Use .Map().GetValueOrDefault()",
                equivalenceKey: MatchNullAnalyzer.DiagnosticId,
                createChangedDocument: c => MigrateMatch(context.Document, diagnostic.Id, invocationExpressionSyntax, m, context.CancellationToken)),
            diagnostic);
    }

    private async Task<Document> MigrateMatch(Document document, string diagnosticId, InvocationExpressionSyntax invocationExpressionSyntax, MemberAccessExpressionSyntax memberAccessExpression, CancellationToken cancellationToken)
    {
        var updatedInvocationExpression = invocationExpressionSyntax
            .WithExpression(memberAccessExpression.WithName(IdentifierName("Map")))
            .WithArgumentList(
                ArgumentList(SingletonSeparatedList<ArgumentSyntax>(
                    invocationExpressionSyntax.ArgumentList.Arguments[0].WithNameColon(null))));
        var newInvocationExpression = InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                updatedInvocationExpression
                    .WithTrailingTrivia(memberAccessExpression.Expression.GetTrailingTrivia()),
                IdentifierName("GetValueOrDefault")
                    .WithLeadingTrivia(memberAccessExpression.Name.GetLeadingTrivia()))
                .WithOperatorToken(memberAccessExpression.OperatorToken));
        var editor = await DocumentEditor.CreateAsync(document, cancellationToken);
        editor.ReplaceNode(invocationExpressionSyntax, newInvocationExpression);
        return editor.GetChangedDocument();
    }

    public sealed override FixAllProvider? GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;
}