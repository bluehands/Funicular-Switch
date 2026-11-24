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

/// <summary>Replaces <c>abstract</c> with <c>closed</c> (FS0002, FS0003, FS0007) on all partial declarations.</summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MakeClosedCodeFixProvider)), Shared]
public class MakeClosedCodeFixProvider : CodeFixProvider
{
    private const string JsonAttribute = "System.Text.Json.Serialization.JsonPolymorphicAttribute";

    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [DiagnosticId.SuggestClosed, DiagnosticId.UnionHierarchyNotClosed, DiagnosticId.SuggestClosedResultType];

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.Single();
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
        var declaration = root?.FindToken(diagnostic.Location.SourceSpan.Start).Parent?.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().FirstOrDefault();
        if (declaration == null) return;

        var model = await context.Document.GetSemanticModelAsync(context.CancellationToken);
        if (model?.GetDeclaredSymbol(declaration, context.CancellationToken) is not INamedTypeSymbol symbol) return;

        context.RegisterCodeFix(
            CodeAction.Create(
                "Make type closed",
                c => MakeClosed(context.Document.Project.Solution, symbol, addJson: false, c),
                equivalenceKey: diagnostic.Id),
            diagnostic);

        if (diagnostic.Id != DiagnosticId.SuggestClosed) return;
        var compilation = model.Compilation;
        var json = compilation.GetTypeByMetadataName(JsonAttribute);
        var hasJsonAttribute = symbol.GetAttributes().Any(a =>
            a.AttributeClass?.ToDisplayString() is JsonAttribute or "System.Text.Json.Serialization.JsonDerivedTypeAttribute");
        if (json != null && !json.GetMembers("InferClosedTypePolymorphism").IsEmpty && !hasJsonAttribute)
        {
            context.RegisterCodeFix(
                CodeAction.Create(
                    "Make closed and add [JsonPolymorphic(InferClosedTypePolymorphism = true)]",
                    c => MakeClosed(context.Document.Project.Solution, symbol, addJson: true, c),
                    equivalenceKey: diagnostic.Id + "+Json"),
                diagnostic);
        }
    }

    private static async Task<Solution> MakeClosed(Solution solution, INamedTypeSymbol symbol, bool addJson, CancellationToken ct)
    {
        var references = symbol.DeclaringSyntaxReferences;
        var first = true;
        foreach (var group in references.GroupBy(r => r.SyntaxTree))
        {
            var document = solution.GetDocument(group.Key);
            if (document == null) continue;
            var editor = await DocumentEditor.CreateAsync(document, ct);
            foreach (var reference in group)
            {
                if (await reference.GetSyntaxAsync(ct) is not TypeDeclarationSyntax declaration) continue;
                var attach = addJson && first;
                first = false;
                editor.ReplaceNode(declaration, (n, g) =>
                {
                    var type = (TypeDeclarationSyntax)n;
                    var index = type.Modifiers.IndexOf(SyntaxKind.AbstractKeyword);
                    if (index >= 0)
                    {
                        var abstractToken = type.Modifiers[index];
                        var closed = SyntaxFactory.Token(abstractToken.LeadingTrivia, SyntaxKind.ClosedKeyword, abstractToken.TrailingTrivia);
                        type = type.WithModifiers(type.Modifiers.Replace(abstractToken, closed));
                    }
                    if (attach)
                    {
                        var attribute = g.Attribute(
                            "System.Text.Json.Serialization.JsonPolymorphic",
                            [g.AttributeArgument("InferClosedTypePolymorphism", g.TrueLiteralExpression())]);
                        type = (TypeDeclarationSyntax)g.AddAttributes(type, attribute);
                    }
                    return type;
                });
            }
            solution = solution.WithDocumentSyntaxRoot(document.Id, editor.GetChangedRoot());
        }
        return solution;
    }

    public sealed override FixAllProvider? GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;
}

