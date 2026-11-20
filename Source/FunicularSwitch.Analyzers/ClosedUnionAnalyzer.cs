using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FunicularSwitch.Analyzers;

/// <summary>
/// FS0002: suggest <c>closed</c> for abstract [UnionType] classes/records.
/// FS0003: closed union has a non-closed abstract intermediate type.
/// FS0004: closed union has a direct case that is neither sealed nor closed.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ClosedUnionAnalyzer : DiagnosticAnalyzer
{
    private const string Category = "Usage Opportunity";

    private static readonly DiagnosticDescriptor SuggestClosedRule = new(
        id: DiagnosticId.SuggestClosed,
        title: "Union type can be declared closed",
        messageFormat: "Union type '{0}' can be declared 'closed' to get compiler-checked exhaustive switches and prevent foreign derivations",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor HierarchyNotClosedRule = new(
        id: DiagnosticId.UnionHierarchyNotClosed,
        title: "Union case hierarchy not fully closed",
        messageFormat: "Type '{0}' is an abstract intermediate type of closed union '{1}' but is not closed; native switches only see it as one case while Match sees its derived cases",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor CaseNotSealedRule = new(
        id: DiagnosticId.UnionCaseNotSealed,
        title: "Union case can be extended outside the assembly",
        messageFormat: "Union case '{0}' of closed union '{1}' is not sealed and can be extended outside the assembly",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        [SuggestClosedRule, HierarchyNotClosedRule, CaseNotSealedRule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            if (!LanguageFeatures.SupportsClosed(start.Compilation)) return;
            var derived = new Lazy<ILookup<ISymbol, INamedTypeSymbol>>(
                () => LanguageFeatures.CreateDerivedTypeLookup(start.Compilation));
            start.RegisterSymbolAction(c => Analyze(c, derived), SymbolKind.NamedType);
        });
    }

    private static void Analyze(SymbolAnalysisContext context, Lazy<ILookup<ISymbol, INamedTypeSymbol>> derivedLookup)
    {
        if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Class } type) return;
        var location = LanguageFeatures.FirstUserLocation(type);
        if (location == null) return;

        if (LanguageFeatures.HasUnionTypeAttribute(type) && type.IsAbstract && !LanguageFeatures.IsClosed(type))
        {
            var cases = derivedLookup.Value[type.OriginalDefinition];
            if (cases.All(LanguageFeatures.SatisfiesClosedGenericRule))
                context.ReportDiagnostic(Diagnostic.Create(SuggestClosedRule, location, type.Name));
            return;
        }

        // Types below a closed union base
        var parent = type.BaseType;
        if (parent == null || !LanguageFeatures.IsClosed(parent) || !LanguageFeatures.IsInClosedUnion(parent)) return;
        var union = UnionRoot(parent);

        var hasDerived = derivedLookup.Value[type.OriginalDefinition].Any();
        if (type.IsAbstract && !LanguageFeatures.IsClosed(type))
        {
            if (hasDerived)
                context.ReportDiagnostic(Diagnostic.Create(HierarchyNotClosedRule, location, type.Name, union.Name));
        }
        else if (!type.IsAbstract && !type.IsSealed && !hasDerived)
        {
            context.ReportDiagnostic(Diagnostic.Create(CaseNotSealedRule, location, type.Name, union.Name));
        }
    }

    private static INamedTypeSymbol UnionRoot(INamedTypeSymbol type)
    {
        for (var t = type; t != null; t = t.BaseType)
            if (LanguageFeatures.IsClosed(t) && LanguageFeatures.HasUnionTypeAttribute(t)) return t;
        return type;
    }
}

