using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.ResultType;

static class Parser
{
    public static IEnumerable<ResultTypeSchema> GetResultTypes(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var mergeMethodByErrorTypeName = FindMergeMethods(compilation, reportDiagnostic);

        foreach (var resultTypeClass in resultTypeClasses)
        {
            var semanticModel = compilation.GetSemanticModel(resultTypeClass.SyntaxTree);

            var attribute = resultTypeClass.AttributeLists
                .Select(l => l.Attributes.First(a => a.GetAttributeFullName(semanticModel) == ResultTypeGenerator.ResultTypeAttribute))
                .First();

            var errorType = TryGetErrorType(attribute, reportDiagnostic);
            if (errorType == null)
                continue;

            if (semanticModel.GetSymbolInfo(errorType, cancellationToken).Symbol is not INamedTypeSymbol errorTypeSymbol)
                continue;

            var fullErrorTypeName = semanticModel.GetFullTypeName(errorType);

            mergeMethodByErrorTypeName.TryGetValue(fullErrorTypeName, out var mergeMethod);

            yield return new(resultTypeClass, errorTypeSymbol, mergeMethod);
        }
    }

    static Dictionary<string, MergeMethod> FindMergeMethods(Compilation compilation, Action<Diagnostic> reportDiagnostic)
    {
        var mergeMethodByErrorTypeName = compilation.SyntaxTrees
            .SelectMany(t => FindMergeMethodsWalker.Get(t.GetRoot(), tree => compilation.GetSemanticModel(tree)))
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

                reportDiagnostic(Diagnostics.InvalidMergeMethod(
                    $"Method {methodDeclaration.Identifier}", methodDeclaration.GetLocation()));
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

    static TypeSyntax? TryGetErrorType(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
    {
        var expressionSyntax = attribute.ArgumentList!.Arguments[0].Expression;
        if (expressionSyntax is not TypeOfExpressionSyntax es)
        {
            reportDiagnostics(Diagnostics.InvalidResultTypeAttributeUsage("Expected typeof expression for error type parameter", attribute.GetLocation()));
            return null;
        }

        return es.Type;
    }
}

class FindMergeMethodsWalker : CSharpSyntaxWalker
{
    readonly Func<SyntaxTree, SemanticModel> m_GetSemanticModel;
    readonly List<MethodDeclarationSyntax> m_MergeMethods = new();

    FindMergeMethodsWalker(Func<SyntaxTree, SemanticModel> getSemanticModel) => m_GetSemanticModel = getSemanticModel;

    public static IEnumerable<MethodDeclarationSyntax> Get(SyntaxNode node, Func<SyntaxTree, SemanticModel> getSemanticModel)
    {
        var me = new FindMergeMethodsWalker(getSemanticModel);
        me.Visit(node);
        return me.m_MergeMethods;
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        if (node.AttributeLists
            .SelectMany(a => a.Attributes)
            .Any(a =>
                a.Name.ToString().Contains("MergeError") &&
                a.GetAttributeFullName(m_GetSemanticModel(a.SyntaxTree)) == "FunicularSwitch.Generators.MergeErrorAttribute")
            )
        {
            m_MergeMethods.Add(node);
        }

        base.VisitMethodDeclaration(node);
    }
}

public abstract class MergeMethod
{
    public static MergeMethod ErrorTypeMember(string methodName, string errorTypeName) => new ErrorTypeMember_(methodName, errorTypeName);
    public static MergeMethod StaticMerge(string methodName, string? @namespace, string errorTypeName) => new StaticMerge_(methodName, @namespace, errorTypeName);

    public string FullErrorTypeName { get; }
    public string MethodName { get; }

    public class ErrorTypeMember_ : MergeMethod
    {
        public ErrorTypeMember_(string methodName, string fullErrorTypeName) : base(UnionCases.ErrorTypeMember, methodName, fullErrorTypeName)
        {
        }
    }

    public class StaticMerge_ : MergeMethod
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
    bool Equals(MergeMethod other) => UnionCase == other.UnionCase;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((MergeMethod)obj);
    }

    public override int GetHashCode() => (int)UnionCase;
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

    public static async Task<T> Match<T>(this Task<MergeMethod> mergeMethod, Func<MergeMethod.ErrorTypeMember_, T> errorTypeMember, Func<MergeMethod.StaticMerge_, T> staticMerge) => (await mergeMethod.ConfigureAwait(false)).Match(errorTypeMember, staticMerge);
    public static async Task<T> Match<T>(this Task<MergeMethod> mergeMethod, Func<MergeMethod.ErrorTypeMember_, Task<T>> errorTypeMember, Func<MergeMethod.StaticMerge_, Task<T>> staticMerge) => await (await mergeMethod.ConfigureAwait(false)).Match(errorTypeMember, staticMerge).ConfigureAwait(false);
}