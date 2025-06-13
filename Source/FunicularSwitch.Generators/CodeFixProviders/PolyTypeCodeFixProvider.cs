using System.Collections.Immutable;
using System.Composition;
using FunicularSwitch.Generators.Analyzers;
using FunicularSwitch.Generators.UnionType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace FunicularSwitch.Generators.CodeFixProviders;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MatchNullCodeFixProvider)), Shared]
public class PolyTypeCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [PolyTypeAnalyzer.DiagnosticId];

    public override FixAllProvider? GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

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

        var (classSyntax, classSymbol, attributeData) = PolyTypeAnalyzer.BaseTypeDeclarationSyntax(semanticModel, attributeSyntax);

        if (classSymbol is null || attributeData is null)
        {
            return;
        }

        var schema = Parser.GetUnionTypeSchema(
            semanticModel.Compilation,
            context.CancellationToken,
            classSyntax,
            classSymbol,
            attributeData);

        var (unionTypeSchema, _, hasValue) = schema;

        if (!hasValue || unionTypeSchema is null)
        {
            return;
        }

        bool hasPolyTypeUsing =
            (root as CompilationUnitSyntax)?.Usings.Any(u => u.Name?.ToFullString() == "PolyType") ?? false;

        context.RegisterCodeFix(
            CodeAction.Create(
                title: $"Add DerivedTypeShape Attributes for union cases",
                equivalenceKey: diagnostic.Id,
                createChangedDocument: c => AddMissingAttributes(
                    context.Document,
                    classSyntax,
                    classSymbol,
                    unionTypeSchema,
                    context.CancellationToken,
                    hasPolyTypeUsing)),
            diagnostic);
    }

    private async Task<Document> AddMissingAttributes(
        Document document,
        BaseTypeDeclarationSyntax classSyntax,
        INamedTypeSymbol classSymbol,
        UnionTypeSchema unionTypeSchema,
        CancellationToken cancellationToken,
        bool hasPolyTypeUsing)
    {
        var derivedAttributes = PolyTypeAnalyzer.GetAttributesOfDerivedTypes(classSymbol);

        // ReSharper disable once SimplifyLinqExpressionUseAll
        var casesWithoutAttributes =
            PolyTypeAnalyzer.GetUnionTypeCasesWithoutAttribute(unionTypeSchema, derivedAttributes);

        var @namespace = !hasPolyTypeUsing ? "PolyType." : string.Empty;
        
        var newClassSyntax = classSyntax.AddAttributeLists(
            casesWithoutAttributes.Select(c =>
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName($"{@namespace}DerivedTypeShape"))
                        .WithArgumentList(
                            AttributeArgumentList(
                                SingletonSeparatedList(
                                    AttributeArgument(
                                        TypeOfExpression(
                                            IdentifierName(c.PolyTypeTypeofExpressionName)
                                        )))))))).ToArray());

        var editor = await DocumentEditor.CreateAsync(document, cancellationToken);
        editor.ReplaceNode(classSyntax, newClassSyntax);
        return editor.GetChangedDocument();
    }
}