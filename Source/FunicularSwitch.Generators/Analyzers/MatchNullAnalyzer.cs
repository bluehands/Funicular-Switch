using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FunicularSwitch.Generators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MatchNullAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "FS0001";

    private static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId,
        title: "",
        messageFormat: "",
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
        
    }
}