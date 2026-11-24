using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FunicularSwitch.Analyzers;

/// <summary>FS0007: suggest <c>closed</c> for an abstract [ResultType] class.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ResultTypeClosedAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId.SuggestClosedResultType,
        title: "Result type can be declared closed",
        messageFormat: "Result type '{0}' can be declared 'closed' so that switches over its generated Ok_/Error_ cases are exhaustive",
        category: "Usage Opportunity",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            if (!LanguageFeatures.SupportsClosed(start.Compilation)) return;
            start.RegisterSymbolAction(Analyze, SymbolKind.NamedType);
        });
    }

    private static void Analyze(SymbolAnalysisContext context)
    {
        if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Class, IsAbstract: true } type) return;
        if (LanguageFeatures.IsClosed(type)) return;
        if (!type.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == LanguageFeatures.ResultTypeAttributeName)) return;
        var location = LanguageFeatures.FirstUserLocation(type);
        if (location == null) return;
        context.ReportDiagnostic(Diagnostic.Create(Rule, location, type.Name));
    }
}
