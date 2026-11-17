using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FunicularSwitch.Analyzers;

/// <summary>FS0005: closed [UnionType] with a manual [JsonPolymorphic]/[JsonDerivedType] list.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class JsonPolymorphicAnalyzer : DiagnosticAnalyzer
{
    private const string JsonNamespace = "System.Text.Json.Serialization.";

    private static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId.JsonPolymorphicInferClosed,
        title: "Use InferClosedTypePolymorphism for closed union",
        messageFormat: "Closed union '{0}' lists its derived types manually; consider [JsonPolymorphic(InferClosedTypePolymorphism = true)] and remove the [JsonDerivedType] attributes",
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
            var jsonPolymorphic = start.Compilation.GetTypeByMetadataName(JsonNamespace + "JsonPolymorphicAttribute");
            if (jsonPolymorphic?.GetMembers("InferClosedTypePolymorphism").IsEmpty != false) return;
            start.RegisterSymbolAction(c => Analyze(c), SymbolKind.NamedType);
        });
    }

    private static void Analyze(SymbolAnalysisContext context)
    {
        if (context.Symbol is not INamedTypeSymbol type) return;
        if (!LanguageFeatures.IsClosed(type) || !LanguageFeatures.HasUnionTypeAttribute(type)) return;
        var location = type.Locations.FirstOrDefault(l => l.IsInSource);
        if (location == null) return;

        var attributes = type.GetAttributes();
        var polymorphic = attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == JsonNamespace + "JsonPolymorphicAttribute");
        if (polymorphic == null) return;
        if (polymorphic.NamedArguments.Any(n => n is { Key: "InferClosedTypePolymorphism", Value.Value: true })) return;
        if (!attributes.Any(a => a.AttributeClass?.ToDisplayString() == JsonNamespace + "JsonDerivedTypeAttribute")) return;

        context.ReportDiagnostic(Diagnostic.Create(Rule, location, type.Name));
    }
}
