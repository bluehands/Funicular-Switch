using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FunicularSwitch.Generators;

[Generator]
public class ResultTypeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "ResultTypeAttribute.g.cs",
            @"using System;
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; }
    }
}"));

        var resultTypeClasses =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)

                )
                .Where(static target => target != null)
                .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(resultTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses, 
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, SourceProductionContext context)
    {
        if (resultTypeClasses.IsDefaultOrEmpty) return;

        var resultTypeSchemata = Parser.GetResultTypes(compilation, resultTypeClasses, context.ReportDiagnostic, context.CancellationToken).ToImmutableArray();

        var generated = resultTypeSchemata.SelectMany(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken)).ToImmutableArray();

        foreach (var (filename, source) in generated) context.AddSource(filename, source);
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
        syntaxNode is ClassDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };

    const string ResultTypeAttribute = "FunicularSwitch.Generators.ResultTypeAttribute";

    static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;
                if (symbol is not IMethodSymbol attributeSymbol)
                    continue;

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                var attributeFullName = attributeContainingTypeSymbol.ToDisplayString();

                if (attributeFullName == ResultTypeAttribute)
                {
                    return classDeclarationSyntax;
                }
            }
        }

        return null;
    }
}

static class Generator
{
    const string TemplateNamespace = "FunicularSwitch.Generators.Templates";
    const string TemplateResultTypeName = "MyResult";
    const string TemplateErrorTypeName = "MyError";

    public static IEnumerable<(string filename, string source)> Emit(ResultTypeSchema resultTypeSchema, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var @namespace = resultTypeSchema.ResultType.GetContainingNamespace();
        var resultTypeName = resultTypeSchema.ResultType.Identifier.ToFullString();

        string Replace(string code) =>
            code
                .Replace($"namespace {TemplateNamespace}", $"namespace {@namespace}")
                .Replace(TemplateResultTypeName, resultTypeName)
                .Replace(TemplateErrorTypeName, resultTypeSchema.ErrorType.Name);

        yield return ($"{resultTypeSchema.ResultType.Identifier}.g.cs", Replace(Templates.Templates.ResultType));
        
            
        if (resultTypeSchema.MergeMethod != null)
            yield return ($"{resultTypeSchema.ResultType.Identifier}WithMerge.g.cs", Replace(Templates.Templates.ResultTypeWithMerge));
    }
}

static class Diagnostics
{
    const string Category = nameof(ResultTypeGenerator);

    public static Diagnostic InvalidAttributeUsage(string message, Location location) =>
        Diagnostic.Create(
            new (
                id: "FUN0001",
                title: "Invalid attribute usage",
                messageFormat: $"{message} -  Please use ResultType attribute with typeof expression like [ResultType(typeof(MyError))]",
                category: Category,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true
            ),
            location
        );
}

static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {

        if (resultTypeClasses.Length == 0)
            throw new Exception("No result types");

        foreach (var resultTypeClass in resultTypeClasses)
        {
            var semanticModel = compilation.GetSemanticModel(resultTypeClass.SyntaxTree);

            //var resultTypeInfo = semanticModel.GetTypeInfo(resultTypeClass);

            //var symbol = semanticModel.GetSymbolInfo(resultTypeClass, cancellationToken).Symbol;
            //if (symbol is not INamedTypeSymbol
            //    resultTypeSymbol)
            //{
            //    throw new Exception($"Unexpteced symbol: {symbol}");
            //    continue;
            //}
            
            var attribute = resultTypeClass.AttributeLists
                .Select(l => l.Attributes.First(a => a.Name.ToFullString() == "ResultType"))
                .First();

            var errorType = TryGetErrorType(attribute, reportDiagnostic);

            if (errorType == null)
            {
                throw new Exception("Error type not found");
                continue;
            }

            if (semanticModel.GetSymbolInfo(errorType, cancellationToken)
                    .Symbol is not INamedTypeSymbol errorTypeSymbol)
            {
                throw new Exception("Error type not unexptected");
                continue;
            }

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

class ResultTypeSchema
{
    public ClassDeclarationSyntax ResultType { get; }
    public INamedTypeSymbol ErrorType { get; }
    public IMethodSymbol? MergeMethod { get; }

    public ResultTypeSchema(ClassDeclarationSyntax resultType, INamedTypeSymbol errorType, IMethodSymbol? mergeMethod)
    {
        ResultType = resultType;
        ErrorType = errorType;
        MergeMethod = mergeMethod;
    }

    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}, {nameof(MergeMethod)}: {MergeMethod}";
}

static class Helpers
{
    public static string GetContainingNamespace(this SyntaxNode node)
    {
        var current = node;
        do
        {
            if (current is NamespaceDeclarationSyntax n)
                return n.Name.ToFullString();
            if (current is FileScopedNamespaceDeclarationSyntax f)
                return f.Name.ToFullString();
            current = current.Parent;
        } while (current != null);

        throw new InvalidOperationException($"No containing namespace found for node {node}");
    }
}