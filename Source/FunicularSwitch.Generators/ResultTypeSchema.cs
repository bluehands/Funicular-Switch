using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

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