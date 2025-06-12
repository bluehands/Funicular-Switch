using System.Collections.Immutable;
using System.Composition;
using FunicularSwitch.Generators.Analyzers;
using FunicularSwitch.Generators.UnionType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace FunicularSwitch.Generators.CodeFixProviders;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MatchNullCodeFixProvider)), Shared]
public class PolyTypeCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [PolyTypeAnalyzer.DiagnosticId];

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.Single();

        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

        var diagnosticNode = root?.FindNode(diagnosticSpan);
        
        if (diagnosticNode is not AttributeSyntax attributeSyntax)
        {
            return;
        }
        
        var semanticModel = await context.Document.GetSemanticModelAsync(context.CancellationToken);

        if (semanticModel is null)
        {
            return;
        }
        
        context.RegisterCodeFix(
            CodeAction.Create(
                title: $"Add DerivedTypeShape Attributes for union cases",
                createChangedDocument: c => AddMissingAttributes(context.Document, semanticModel, diagnostic.Id, attributeSyntax, context.CancellationToken)),
            diagnostic);
    }

    private async Task<Document> AddMissingAttributes(
        Document document, SemanticModel semanticModel, string diagnosticId,
        AttributeSyntax attributeSyntax,
        CancellationToken cancellationToken)
    {
        
        var attributeList = (attributeSyntax.Parent as AttributeListSyntax)!;
        var classSyntax = (attributeList.Parent as BaseTypeDeclarationSyntax)!;
        var classSymbol = semanticModel.GetDeclaredSymbol(classSyntax);
        var attributeData = classSymbol?.GetAttributes()
            .FirstOrDefault(a => a.ApplicationSyntaxReference!.SyntaxTree == attributeSyntax.SyntaxTree);

        var schema = Parser.GetUnionTypeSchema(
            semanticModel.Compilation,
            cancellationToken,
            classSyntax,
            classSymbol,
            attributeData);
        
        var (unionTypeSchema, errors, hasValue) = schema;

        var derivedAttributes = classSymbol
            .GetAttributes()
            .Where(a => a.AttributeClass?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ==
                        "global::PolyType.DerivedTypeShapeAttribute")
            .Select(a => (a.ConstructorArguments[0].Value as INamedTypeSymbol)?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
            .ToList();
        
        // ReSharper disable once SimplifyLinqExpressionUseAll
        var casesWithoutAttributes = unionTypeSchema.Cases
            .Where(derivedType => !derivedAttributes
                .Any(a => a == $"global::{derivedType.FullTypeName}"));
        
        var newClassSyntax = classSyntax.AddAttributeLists(
            casesWithoutAttributes.Select(c =>
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("PolyType.DerivedTypeShape"))
                        .WithArgumentList(
                            AttributeArgumentList(
                                SingletonSeparatedList(
                                    AttributeArgument(
                                        TypeOfExpression(
                                            IdentifierName(c.FullTypeName) // TODO Das hat auch generics usw. hier brauchen wir das INamedTypeSymbol oder nur den Typename ohne namespace
                                            )))))))).ToArray());
        
        
        var editor = await DocumentEditor.CreateAsync(document, cancellationToken);
        editor.ReplaceNode(classSyntax, newClassSyntax);
        return editor.GetChangedDocument();
    }
}