using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.ResultType;

static class Parser
{
    public static GenerationResult<ResultTypeSchema> GetResultTypeSchema(
        ClassDeclarationSyntax resultTypeClass, Compilation compilation, CancellationToken cancellationToken)
    {
        var semanticModel = compilation.GetSemanticModel(resultTypeClass.SyntaxTree);

        var attribute = resultTypeClass.AttributeLists
            .SelectMany(l => l.Attributes)
            .First(a => a.GetAttributeFullName(semanticModel) == ResultTypeGenerator.ResultTypeAttribute);

        return TryGetErrorType(attribute)
            .Bind(errorType =>
            {
                if (semanticModel.GetSymbolInfo(errorType, cancellationToken).Symbol is not INamedTypeSymbol errorTypeSymbol)
                    return GenerationResult<ResultTypeSchema>.Empty;

                var resultTypeSchema = new ResultTypeSchema(resultTypeClass, errorTypeSymbol);
                return resultTypeSchema;
            });
    }

    public static Dictionary<string, MergeMethod> FindMergeMethods(Compilation compilation, Action<Diagnostic> reportDiagnostic)
    {
        var mergeMethodByErrorTypeName = compilation.SyntaxTrees
            .SelectMany(t => FindMergeMethodsWalker.Get(t.GetRoot(), tree => compilation.GetSemanticModel(tree), "MergeError", "FunicularSwitch.Generators.MergeErrorAttribute"))
            .Select(methodDeclaration =>
            {
                var semanticModel = compilation.GetSemanticModel(methodDeclaration.SyntaxTree);
                var returnTypeName = semanticModel.GetFullTypeName(methodDeclaration.ReturnType);

                if (methodDeclaration.Modifiers.HasModifier(SyntaxKind.StaticKeyword))
                {
                    if (methodDeclaration.ParameterList.Parameters.Count == 2 &&
                        methodDeclaration.ParameterList.Parameters.All(p => semanticModel.GetFullTypeName(p.Type!) == returnTypeName) &&
                        methodDeclaration.ParameterList.Parameters[0].Modifiers.HasModifier(SyntaxKind.ThisKeyword))
                    {
                        return MergeMethod.StaticMerge(methodDeclaration.Identifier.ToString(),
                            methodDeclaration.GetContainingNamespace(), returnTypeName);
                    }
                }
                else if (methodDeclaration.ParameterList.Parameters.Count == 1 &&
                         methodDeclaration.Parent is ClassDeclarationSyntax c &&
                         //TODO: get correct name with visitor or from semantic model
                         $"{c.GetContainingNamespace()}.{c.Identifier}".TrimStart('.') == returnTypeName &&
                         methodDeclaration.ParameterList.Parameters.All(p => semanticModel.GetFullTypeName(p.Type!) == returnTypeName))
                {
                    return MergeMethod.ErrorTypeMember(methodDeclaration.Identifier.ToString(), returnTypeName);
                }

                reportDiagnostic(Diagnostics.InvalidMergeMethod($"Method {methodDeclaration.Identifier}", methodDeclaration.GetLocation()));
                return null;
            })
            .Where(m => m != null)
            .GroupBy(m => m!.FullErrorTypeName)
            .ToDictionary(g => g.Key, g =>
            {
                var methods = g.ToList();
                if (methods.Count > 1)
                    reportDiagnostic(Diagnostics.AmbiguousMergeMethods(methods.Select(m => m!.MethodName)));
                return methods[0]!;
            });
        return mergeMethodByErrorTypeName;
    }

    public static Dictionary<string, ExceptionToErrorMethod> FindExceptionToErrorMethods(Compilation compilation, Action<Diagnostic> reportDiagnostic)
    {
        var factoryMethodByErrorTypeName = compilation.SyntaxTrees
            .SelectMany(t => FindMergeMethodsWalker.Get(t.GetRoot(), tree => compilation.GetSemanticModel(tree), "ExceptionToError", "FunicularSwitch.Generators.ExceptionToError"))
            .Select(methodDeclaration =>
            {
                if (methodDeclaration.Modifiers.HasModifier(SyntaxKind.StaticKeyword))
                {
                    var semanticModel = compilation.GetSemanticModel(methodDeclaration.SyntaxTree);
                    var errorTypeName = semanticModel.GetFullTypeName(methodDeclaration.ReturnType);
                    var declaringTypeSyntax = methodDeclaration.Parent;
                    string declaringTypeFullName;
                    if (declaringTypeSyntax != null && semanticModel.GetDeclaredSymbol(declaringTypeSyntax) is INamedTypeSymbol declaredSymbol)
                    {
                        declaringTypeFullName = declaredSymbol.FullTypeNameWithNamespace();
                    }
                    else
                    {
                        reportDiagnostic(Diagnostics.InvalidExceptionToErrorMethod($"Could not get declaring type name of method {methodDeclaration.Identifier}", methodDeclaration.GetLocation()));
                        return null;
                    }

                    var parameters = methodDeclaration.ParameterList.Parameters;
                    if (parameters.Count == 1 &&
                        parameters.All(p => semanticModel.GetFullTypeName(p.Type!) == "System.Exception"))
                    {
                        return new ExceptionToErrorMethod(errorTypeName, declaringTypeFullName, methodDeclaration.Identifier.ToString());
                    }
                }

                reportDiagnostic(Diagnostics.InvalidExceptionToErrorMethod($"Method {methodDeclaration.Identifier}", methodDeclaration.GetLocation()));
                return null;
            })
            .Where(m => m != null)
            .GroupBy(m => m!.ErrorTypeName)
            .ToDictionary(g => g.Key, g =>
            {
                var methods = g.ToList();
                if (methods.Count > 1)
                    reportDiagnostic(Diagnostics.AmbiguousMergeMethods(methods.Select(m => m!.MethodName)));
                return methods[0]!;
            });
        return factoryMethodByErrorTypeName;
    }

    static GenerationResult<TypeSyntax> TryGetErrorType(AttributeSyntax attribute)
    {
        var expressionSyntax = attribute.ArgumentList!.Arguments[0].Expression;
        if (expressionSyntax is not TypeOfExpressionSyntax es)
        {
            return new DiagnosticInfo(Diagnostics.InvalidResultTypeAttributeUsage("Expected typeof expression for error type parameter", attribute.GetLocation()));
        }

        return es.Type;
    }
}

class FindMergeMethodsWalker : CSharpSyntaxWalker
{
    readonly Func<SyntaxTree, SemanticModel> m_GetSemanticModel;
    readonly List<MethodDeclarationSyntax> m_MergeMethods = new();
    readonly string _attributeName;
    readonly string _fullAttributeName;

    FindMergeMethodsWalker(Func<SyntaxTree, SemanticModel> getSemanticModel, string attributeName, string fullAttributeName)
    {
        _attributeName = attributeName;
        m_GetSemanticModel = getSemanticModel;
        _fullAttributeName = fullAttributeName;
    }

    public static IEnumerable<MethodDeclarationSyntax> Get(SyntaxNode node, Func<SyntaxTree, SemanticModel> getSemanticModel, string attributeName, string fullAttributeName)
    {
        var me = new FindMergeMethodsWalker(getSemanticModel, attributeName, fullAttributeName);
        me.Visit(node);
        return me.m_MergeMethods;
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        if (node.AttributeLists
            .SelectMany(a => a.Attributes)
            .Any(a =>
                a.Name.ToString().Contains(_attributeName) &&
                a.GetAttributeFullName(m_GetSemanticModel(a.SyntaxTree)) == _fullAttributeName)
           )
        {
            m_MergeMethods.Add(node);
        }

        base.VisitMethodDeclaration(node);
    }
}

public sealed record ExceptionToErrorMethod(string ErrorTypeName, string FullTypeName, string MethodName)
{
    public string FullMethodName => $"{FullTypeName}.{MethodName}";
}

public abstract record MergeMethod
{
    public static MergeMethod ErrorTypeMember(string methodName, string errorTypeName) => new ErrorTypeMember_(methodName, errorTypeName);
    public static MergeMethod StaticMerge(string methodName, string? @namespace, string errorTypeName) => new StaticMerge_(methodName, @namespace, errorTypeName);

    public string FullErrorTypeName { get; }
    public string MethodName { get; }

    public record ErrorTypeMember_ : MergeMethod
    {
        public ErrorTypeMember_(string methodName, string fullErrorTypeName) : base(UnionCases.ErrorTypeMember, methodName, fullErrorTypeName)
        {
        }
    }

    public record StaticMerge_ : MergeMethod
    {
        public string? Namespace { get; }

        public StaticMerge_(string methodName, string? @namespace, string fullErrorTypeName) : base(UnionCases.StaticMerge, methodName, fullErrorTypeName) => Namespace = @namespace;
    }

    internal enum UnionCases
    {
        ErrorTypeMember,
        StaticMerge
    }

    internal UnionCases UnionCase { get; }

    MergeMethod(UnionCases unionCase, string methodName, string fullErrorTypeName)
    {
        UnionCase = unionCase;
        MethodName = methodName;
        FullErrorTypeName = fullErrorTypeName;
    }

    public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
}

public static class MergeMethodExtension
{
    public static T Match<T>(this MergeMethod mergeMethod, Func<MergeMethod.ErrorTypeMember_, T> errorTypeMember, Func<MergeMethod.StaticMerge_, T> staticMerge)
    {
        switch (mergeMethod.UnionCase)
        {
            case MergeMethod.UnionCases.ErrorTypeMember:
                return errorTypeMember((MergeMethod.ErrorTypeMember_)mergeMethod);
            case MergeMethod.UnionCases.StaticMerge:
                return staticMerge((MergeMethod.StaticMerge_)mergeMethod);
            default:
                throw new ArgumentException($"Unknown type derived from MergeMethod: {mergeMethod.GetType().Name}");
        }
    }

    public static async Task<T> Match<T>(this MergeMethod mergeMethod, Func<MergeMethod.ErrorTypeMember_, Task<T>> errorTypeMember, Func<MergeMethod.StaticMerge_, Task<T>> staticMerge)
    {
        switch (mergeMethod.UnionCase)
        {
            case MergeMethod.UnionCases.ErrorTypeMember:
                return await errorTypeMember((MergeMethod.ErrorTypeMember_)mergeMethod).ConfigureAwait(false);
            case MergeMethod.UnionCases.StaticMerge:
                return await staticMerge((MergeMethod.StaticMerge_)mergeMethod).ConfigureAwait(false);
            default:
                throw new ArgumentException($"Unknown type derived from MergeMethod: {mergeMethod.GetType().Name}");
        }
    }

    public static async Task<T> Match<T>(this Task<MergeMethod> mergeMethod, Func<MergeMethod.ErrorTypeMember_, T> errorTypeMember, Func<MergeMethod.StaticMerge_, T> staticMerge) =>
        (await mergeMethod.ConfigureAwait(false)).Match(errorTypeMember, staticMerge);

    public static async Task<T> Match<T>(this Task<MergeMethod> mergeMethod, Func<MergeMethod.ErrorTypeMember_, Task<T>> errorTypeMember, Func<MergeMethod.StaticMerge_, Task<T>> staticMerge) =>
        await (await mergeMethod.ConfigureAwait(false)).Match(errorTypeMember, staticMerge).ConfigureAwait(false);
}