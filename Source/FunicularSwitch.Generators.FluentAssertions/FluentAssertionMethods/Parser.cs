using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

internal static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(
        Compilation compilation,
        ImmutableArray<AttributeSyntax> attributes,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        foreach (var attribute in attributes)
        {
            var semanticModel = compilation.GetSemanticModel(attribute.SyntaxTree);

            var resultTypeSyntax = TryGetResultType(attribute, reportDiagnostic);

            if (resultTypeSyntax is null)
            {
                continue;
            }

            var symbolInfo = semanticModel.GetSymbolInfo(resultTypeSyntax, cancellationToken);
            if (symbolInfo.Symbol is not INamedTypeSymbol resultTypeSymbol)
            {
                if (symbolInfo.CandidateSymbols.Length == 1
                    && symbolInfo.CandidateReason == CandidateReason.WrongArity
                    && symbolInfo.CandidateSymbols.First() is INamedTypeSymbol res)
                {
                    resultTypeSymbol = res;
                }
                else
                {
                    continue;
                }
            }


            var resultTypeInfo = semanticModel.GetTypeInfo(resultTypeSyntax);
            var resultTypeAttribute = resultTypeInfo
                .Type?
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == ResultTypeAttribute);
            var errorTypeSymbol = TryGetErrorType(resultTypeAttribute, reportDiagnostic);

            var derivedErrorTypes = new List<INamedTypeSymbol>();
            if (errorTypeSymbol is not null)
            {
                var types = compilation.SourceModule.ReferencedAssemblySymbols
                    .Append(compilation.Assembly)
                    .Where(a => a.Identity.Name == errorTypeSymbol.ContainingAssembly.Name)
                    .SelectMany(assemblySymbol =>
                    {
                        try
                        {
                            var main = assemblySymbol.Identity.Name
                                .Split('.')
                                .Aggregate(assemblySymbol.GlobalNamespace,
                                    (symbol, part) => symbol.GetNamespaceMembers().Single(m => m.Name.Equals(part)));
                            return GetAllTypes(main);
                        }
                        catch
                        {
                            return Enumerable.Empty<INamedTypeSymbol>();
                        }
                    });

                bool IsDerived(INamedTypeSymbol t)
                {
                    if (t.BaseType is null)
                    {
                        return false;
                    }

                    if (t.BaseType.Equals(errorTypeSymbol, SymbolEqualityComparer.Default))
                    {
                        return true;
                    }

                    return IsDerived(t.BaseType);
                }

                derivedErrorTypes.AddRange(types.Where(IsDerived));
            }

            yield return new(resultTypeSymbol, errorTypeSymbol, derivedErrorTypes);
        }
    }

    private static IEnumerable<INamedTypeSymbol> GetAllTypes(INamespaceOrTypeSymbol root)
    {
        foreach (var symbol in root.GetMembers())
        {
            switch (symbol)
            {
                case INamespaceSymbol namespaceSymbol:
                    foreach (var nested in GetAllTypes(namespaceSymbol))
                    {
                        yield return nested;
                    }
                    break;
                case INamedTypeSymbol typeSymbol:
                    yield return typeSymbol;
                    foreach (var nested in GetAllTypes(typeSymbol))
                    {
                        yield return nested;
                    }
                    break;
            }
        }
    }

    private static TypeSyntax? TryGetResultType(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
    {
        var expressionSyntax = attribute.ArgumentList!.Arguments[0].Expression;
        if (expressionSyntax is not TypeOfExpressionSyntax es)
        {
            reportDiagnostics(Diagnostics.InavlidGenerateMethodsForUsage(
                "Expected typeof expression for result type parameter", attribute.GetLocation()));
            return null;
        }

        return es.Type;
    }

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
}

class FindConcreteDerivedTypesWalker : CSharpSyntaxWalker
{
    readonly List<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node)> m_DerivedClasses = new();
    readonly SemanticModel m_SemanticModel;
    readonly ITypeSymbol m_BaseClass;

    FindConcreteDerivedTypesWalker(SemanticModel semanticModel, ITypeSymbol baseClass)
    {
        m_SemanticModel = semanticModel;
        m_BaseClass = baseClass;
    }

    public static IEnumerable<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node, int numberOfConctreteBaseTypes)>
        Get(SyntaxNode node, ITypeSymbol baseClass, SemanticModel semanticModel)
    {
        var me = new FindConcreteDerivedTypesWalker(semanticModel, baseClass);
        me.Visit(node);
        return me.m_DerivedClasses.Select(d => (d.symbol, d.node,
            numberOfConctreteBaseTypes: me.m_DerivedClasses.Count(t => d.symbol.InheritsFrom(t.symbol))));
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        CheckIsConcreteDerived(node);
        base.VisitClassDeclaration(node);
    }

    public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        CheckIsConcreteDerived(node);
        base.VisitRecordDeclaration(node);
    }

    void CheckIsConcreteDerived(BaseTypeDeclarationSyntax node)
    {
        var isAbstract = node.Modifiers.Any(m => m.Text.ToString() == "abstract");
        if (!isAbstract)
        {
            var symbol = m_SemanticModel.GetDeclaredSymbol(node);
            if (symbol != null && (symbol.InheritsFrom(m_BaseClass) || symbol.Implements(m_BaseClass)))
            {
                m_DerivedClasses.Add((symbol, node));
            }
        }
    }
}