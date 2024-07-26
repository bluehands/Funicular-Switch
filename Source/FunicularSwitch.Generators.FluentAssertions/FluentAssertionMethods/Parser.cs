using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

internal static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(
        IAssemblySymbol assembly,
        bool generateForInternalTypes,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var allTypesInAssembly = GetAllTypes(assembly);
        
        return GetTypesWithAttribute(allTypesInAssembly, ResultTypeAttribute)
            .Where(tuple => tuple.Type.DeclaredAccessibility == Accessibility.Public || (tuple.Type.DeclaredAccessibility == Accessibility.Internal && generateForInternalTypes))
            .Select(tuple =>
            {
                var errorTypeSymbol = TryGetErrorType(tuple.Attribute, reportDiagnostic);
                return new ResultTypeSchema(tuple.Type, errorTypeSymbol);
            });
    }

    public static IEnumerable<UnionTypeSchema> GetUnionTypes(
        IAssemblySymbol assembly,
        bool generateForInternalTypes,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var allTypesInAssembly = GetAllTypes(assembly);
        
        var unionTypes = GetTypesWithAttribute(allTypesInAssembly, UnionTypeAttribute)
            .Where(tuple => tuple.Type.DeclaredAccessibility == Accessibility.Public || (tuple.Type.DeclaredAccessibility == Accessibility.Internal && generateForInternalTypes));

        foreach (var (unionType, _) in unionTypes)
        {
            var derivedTypes = allTypesInAssembly.Where(t => t.InheritsFrom(unionType)).ToList();
            yield return new UnionTypeSchema(unionType, derivedTypes);
        }
    }

    static List<INamedTypeSymbol> GetAllTypes(IAssemblySymbol assembly)
    {
        var allTypesInAssembly = assembly.Modules
            .SelectMany(m => m.GlobalNamespace.GetAllTypes())
            .ToList();
        return allTypesInAssembly;
    }

    private static IEnumerable<(INamedTypeSymbol Type, AttributeData Attribute)> GetTypesWithAttribute(
        IReadOnlyCollection<INamedTypeSymbol> types, string name)
        => types
            .Select(t => (Type: t,
                Attribute: t.GetAttributes().FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == name)))
            .Where(tuple => tuple.Attribute is not null)
            .Select(tuple => (tuple.Type, tuple.Attribute!));

    

    private static INamedTypeSymbol? TryGetErrorType(AttributeData? attribute, Action<Diagnostic> reportDiagnostics)
    {
        var typedConstant = attribute?.NamedArguments
                                .Select(x => (KeyValuePair<string, TypedConstant>?)x)
                                .FirstOrDefault(kvp => kvp?.Key == "ErrorType")
                                ?.Value
                            ?? attribute?.ConstructorArguments.FirstOrDefault();
        if (typedConstant?.Value is not INamedTypeSymbol typeSymbol)
        {
            return null;
        }

        return typeSymbol;
    }

    internal const string ResultTypeAttribute = "FunicularSwitch.Generators.ResultTypeAttribute";
    internal const string UnionTypeAttribute = "FunicularSwitch.Generators.UnionTypeAttribute";
}