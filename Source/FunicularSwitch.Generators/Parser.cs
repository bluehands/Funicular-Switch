using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {

        if (resultTypeClasses.Length == 0)
            throw new Exception("No result types");

        foreach (var resultTypeClass in resultTypeClasses)
        {
            var semanticModel = compilation.GetSemanticModel(resultTypeClass.SyntaxTree);

            var attribute = resultTypeClass.AttributeLists
                .Select(l => l.Attributes.First(a => a.Name.ToFullString() == "ResultType"))
                .First();

            var errorType = TryGetErrorType(attribute, reportDiagnostic);
            if (errorType == null)
                continue;

            if (semanticModel.GetSymbolInfo(errorType, cancellationToken)
                    .Symbol is not INamedTypeSymbol errorTypeSymbol)
                continue;

            var mergeMethod = errorTypeSymbol
                .GetMembers()
                .OfType<IMethodSymbol>()
                .FirstOrDefault(m =>
                    m.Name == "Merge" &&
                    m.Parameters.Length == 1 &&
                    m.Parameters.All(p => p.Type.Name == errorTypeSymbol.Name) &&
                    m.ReturnType.Name == errorTypeSymbol.Name
                );

            yield return new(resultTypeClass, errorTypeSymbol, mergeMethod);
        }
    }

    static TypeSyntax? TryGetErrorType(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
    {
        var expressionSyntax = attribute.ArgumentList!.Arguments[0].Expression;
        if (expressionSyntax is not TypeOfExpressionSyntax es)
        {
            reportDiagnostics(Diagnostics.InvalidAttributeUsage("Expected typeof expression for error type parameter", attribute.GetLocation()));
            return null;
        }

        return es.Type;
    }
}