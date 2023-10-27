using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.ResultType;

class ResultTypeSchema
{
    public ClassDeclarationSyntax ResultType { get; }
    public INamedTypeSymbol ErrorType { get; }
    public MergeMethod? MergeMethod { get; }
    public ExceptionToErrorMethod? ExceptionToErrorMethod { get; }

    public ResultTypeSchema(ClassDeclarationSyntax resultType, INamedTypeSymbol errorType, MergeMethod? mergeMethod,
	    ExceptionToErrorMethod? exceptionToErrorMethod)
    {
        ResultType = resultType;
        ErrorType = errorType;
        MergeMethod = mergeMethod;
        ExceptionToErrorMethod = exceptionToErrorMethod;
    }

    public override string ToString() => $"{nameof(ResultType)}: {ResultType}, {nameof(ErrorType)}: {ErrorType}, {nameof(MergeMethod)}: {MergeMethod}";
}