using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace FunicularSwitch.Analyzers.CodeFixProviders;

/// <summary>Adds <c>sealed</c> to a union case (FS0004).</summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddSealedCodeFixProvider)), Shared]
public class AddSealedCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [DiagnosticId.UnionCaseNotSealed];

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.Single();
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
        var declaration = root?.FindToken(diagnostic.Location.SourceSpan.Start).Parent?.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().FirstOrDefault();
        if (declaration == null) return;
        var model = await context.Document.GetSemanticModelAsync(context.CancellationToken);
        if (model?.GetDeclaredSymbol(declaration, context.CancellationToken) is not INamedTypeSymbol symbol) return;

        context.RegisterCodeFix(
            CodeAction.Create("Make union case sealed", c => AddSealed(context.Document.Project.Solution, symbol, c), equivalenceKey: diagnostic.Id),
            diagnostic);
    }

    private static async Task<Solution> AddSealed(Solution solution, INamedTypeSymbol symbol, CancellationToken ct)
    {
        // 'sealed' goes on one declaration only; partials must agree but one is enough for the compiler.
        var reference = symbol.DeclaringSyntaxReferences.First();
        var document = solution.GetDocument(reference.SyntaxTree);
        if (document == null) return solution;
        var editor = await DocumentEditor.CreateAsync(document, ct);
        if (await reference.GetSyntaxAsync(ct) is not TypeDeclarationSyntax declaration) return solution;
        editor.ReplaceNode(declaration, (n, _) =>
        {
            var type = (TypeDeclarationSyntax)n;
            if (type.Modifiers.Any(SyntaxKind.SealedKeyword)) return type;
            // insert before 'partial' (must stay last) otherwise at the end of the modifier list
            var partial = type.Modifiers.IndexOf(SyntaxKind.PartialKeyword);
            var sealedToken = SyntaxFactory.Token(SyntaxKind.SealedKeyword).WithTrailingTrivia(SyntaxFactory.Space);
            var modifiers = partial >= 0 ? type.Modifiers.Insert(partial, sealedToken) : type.Modifiers.Add(sealedToken);
            if (partial < 0 && type.Modifiers.Count == 0) return type.WithModifiers(modifiers).WithLeadingTrivia(type.GetLeadingTrivia());
            return type.WithModifiers(modifiers);
        });
        return solution.WithDocumentSyntaxRoot(document.Id, editor.GetChangedRoot());
    }

    public sealed override FixAllProvider? GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;
}
