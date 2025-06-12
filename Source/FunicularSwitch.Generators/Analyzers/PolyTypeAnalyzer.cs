using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Parser = FunicularSwitch.Generators.UnionType.Parser;

namespace FunicularSwitch.Generators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class PolyTypeAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "FS0010";

    public static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId,
        title: "Funicular Switch PolyType integration usage opportunity",
        messageFormat: "Use DerivedTypeShape Attribute for PolyType support",
        category: "Usage Opportunity",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rule];
    
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.Attribute);
    }

    private void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not AttributeSyntax attributeSyntax)
        {
            return;
        }

        var attributeList = (attributeSyntax.Parent as AttributeListSyntax)!;
        var classSyntax = (attributeList.Parent as BaseTypeDeclarationSyntax)!;
        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classSyntax);
        var attributeData = classSymbol?.GetAttributes()
            .FirstOrDefault(a => a.ApplicationSyntaxReference!.SyntaxTree == attributeSyntax.SyntaxTree);
        
        
        if (attributeData is null)
        {
            return;
        }

        if (attributeData.AttributeClass?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) !=
            $"global::{UnionTypeGenerator.UnionTypeAttribute}")
        {
            return;
        }

        var schema = Parser.GetUnionTypeSchema(
            context.Compilation,
            context.CancellationToken,
            classSyntax,
            classSymbol,
            attributeData);

        var (unionTypeSchema, errors, hasValue) = schema;
        
        if (!hasValue || unionTypeSchema!.Cases.IsEmpty)
        {
            return;
        }
        
        if (!context.Compilation.ReferencedAssemblyNames.Any(a => a.Name is "PolyType"))
        {
            return;
        }

        var derivedAttributes = classSymbol
            .GetAttributes()
            .Where(a => a.AttributeClass?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ==
                        "global::PolyType.DerivedTypeShapeAttribute")
            .Select(a => (a.ConstructorArguments[0].Value as INamedTypeSymbol)?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
            .ToList();
        
        // ReSharper disable once SimplifyLinqExpressionUseAll
        if (unionTypeSchema.Cases
            .Any(derivedType => !derivedAttributes
                .Any(a => a == $"global::{derivedType.FullTypeName}")))
        {
            var diagnostic = Diagnostic.Create(
                Rule,
                attributeSyntax.GetLocation());

            context.ReportDiagnostic(diagnostic);
        }
    }
}