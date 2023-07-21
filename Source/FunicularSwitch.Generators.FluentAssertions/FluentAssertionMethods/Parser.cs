using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

internal static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(
        IAssemblySymbol assembly,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        return assembly.Modules
            .SelectMany(m => GetTypesWithAttribute(m.GlobalNamespace, ResultTypeAttribute))
            .Select(tuple =>
            {
                var errorTypeSymbol = TryGetErrorType(tuple.Attribute, reportDiagnostic);
                return new ResultTypeSchema(tuple.Type, errorTypeSymbol);
            });
    }

    public static IEnumerable<UnionTypeSchema> GetUnionTypes(
        IAssemblySymbol assembly,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var unionTypes = assembly.Modules
            .SelectMany(m => GetTypesWithAttribute(m.GlobalNamespace, UnionTypeAttribute));

        foreach (var (unionType, _) in unionTypes)
        {
            var main = assembly.Identity.Name.Split('.').Aggregate(assembly.GlobalNamespace,
                (symbol, part) => symbol.GetNamespaceMembers().Single(m => m.Name.Equals(part)));

            var derivedTypes = main.GetAllTypes().Where(t => t.InheritsFrom(unionType)).ToList();
            yield return new UnionTypeSchema(unionType, derivedTypes);
        }
    }

    private static IEnumerable<(INamedTypeSymbol Type, AttributeData Attribute)> GetTypesWithAttribute(
        INamespaceSymbol @namespace, string name)
        => @namespace.GetAllTypes()
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