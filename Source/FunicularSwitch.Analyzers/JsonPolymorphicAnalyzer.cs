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

    private static readonly DiagnosticDescriptor MissingDerivedTypeRule = new(
        id: DiagnosticId.JsonDerivedTypeMissing,
        title: "Union case missing from JsonDerivedType list",
        messageFormat: "Union case '{1}' of closed union '{0}' is not listed in a [JsonDerivedType] attribute",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rule, MissingDerivedTypeRule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(start =>
        {
            if (!LanguageFeatures.SupportsClosed(start.Compilation)) return;
            var jsonPolymorphic = start.Compilation.GetTypeByMetadataName(JsonNamespace + "JsonPolymorphicAttribute");
            if (jsonPolymorphic?.GetMembers("InferClosedTypePolymorphism").IsEmpty != false) return;
            var derived = new System.Lazy<ILookup<ISymbol, INamedTypeSymbol>>(() => LanguageFeatures.CreateDerivedTypeLookup(start.Compilation));
            start.RegisterSymbolAction(c => Analyze(c, derived), SymbolKind.NamedType);
        });
    }

    private static void Analyze(SymbolAnalysisContext context, System.Lazy<ILookup<ISymbol, INamedTypeSymbol>> derivedLookup)
    {
        if (context.Symbol is not INamedTypeSymbol type) return;
        if (!LanguageFeatures.IsClosed(type) || !LanguageFeatures.HasUnionTypeAttribute(type)) return;

        var attributes = type.GetAttributes();
        var polymorphic = attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == JsonNamespace + "JsonPolymorphicAttribute");
        if (polymorphic == null) return;
        if (polymorphic.NamedArguments.Any(n => n is { Key: "InferClosedTypePolymorphism", Value.Value: true })) return;
        var listed = attributes
            .Where(a => a.AttributeClass?.ToDisplayString() == JsonNamespace + "JsonDerivedTypeAttribute")
            .Select(a => a.ConstructorArguments.FirstOrDefault().Value as INamedTypeSymbol)
            .Where(t => t != null)
            .Select(t => t!.OriginalDefinition)
            .ToList();
        if (attributes.All(a => a.AttributeClass?.ToDisplayString() != JsonNamespace + "JsonDerivedTypeAttribute")) return;

        var location = LanguageFeatures.FirstUserLocation(type);
        if (location == null) return;
        context.ReportDiagnostic(Diagnostic.Create(Rule, location, type.Name));

        // concrete descendants (any depth) not in the manual list
        var lookup = derivedLookup.Value;
        var pending = new System.Collections.Generic.Stack<INamedTypeSymbol>(lookup[type.OriginalDefinition]);
        while (pending.Count > 0)
        {
            var d = pending.Pop();
            foreach (var child in lookup[d.OriginalDefinition]) pending.Push(child);
            if (d.IsAbstract) continue;
            if (!listed.Any(l => SymbolEqualityComparer.Default.Equals(l, d.OriginalDefinition)))
                context.ReportDiagnostic(Diagnostic.Create(MissingDerivedTypeRule, location, type.Name, d.Name));
        }
    }
}

