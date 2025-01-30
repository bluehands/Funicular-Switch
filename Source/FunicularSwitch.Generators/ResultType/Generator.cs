using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.ResultType;

static class Generator
{
    const string TemplateNamespace = "FunicularSwitch.Generators.Templates";
    const string TemplateResultTypeName = "MyResult";
    const string TemplateErrorTypeName = "MyError";

    public static IEnumerable<(string filename, string source)> Emit(
        ResultTypeSchema resultTypeSchema, 
        SymbolWrapper<INamedTypeSymbol> defaultErrorType,
        MergeMethod? mergeErrorMethod,
        ExceptionToErrorMethod? exceptionToErrorMethod,
        Action<Diagnostic> reportDiagnostic, 
        CancellationToken cancellationToken)
    {
	    var resultTypeName = resultTypeSchema.ResultTypeName.Name;
        var resultTypeNamespace = resultTypeSchema.ResultTypeNamespace;
        if (resultTypeNamespace == null)
        {
	        reportDiagnostic(Diagnostics.ResultTypeInGlobalNamespace($"Result type {resultTypeName} is placed in global namespace, this is not supported. Please put {resultTypeName} into non empty namespace.", resultTypeSchema.ResultTypeLocation?.ToLocation() ?? Location.None));
	        yield break;
        }

        var errorTypeSymbol = resultTypeSchema.ErrorType ?? defaultErrorType;
        var isValueType = errorTypeSymbol.Symbol.IsValueType;
        var errorTypeNamespace = errorTypeSymbol.Symbol.GetFullNamespace();

        string Replace(string code, IReadOnlyCollection<string> additionalNamespaces, string genericTypeParameterNameForHandleExceptions)
        {
	        code = code
                .Replace($"namespace {TemplateNamespace}", $"namespace {resultTypeNamespace}")
                .Replace(TemplateResultTypeName, resultTypeName)
                .Replace(TemplateErrorTypeName, errorTypeSymbol.Symbol.Name);

            if (resultTypeSchema.IsInternal)
                code = code
                    .Replace("public abstract partial", "abstract partial")
                    .Replace("public static partial", "static partial");

            code = code
                .Replace("//additional using directives", additionalNamespaces.Distinct().Select(a => $"using {a};").ToSeparatedString("\n"));

            if (exceptionToErrorMethod != null)
            {
	            code = code.Replace("throw; //createGenericErrorResult",
		            $"return {resultTypeName}.Error<{genericTypeParameterNameForHandleExceptions}>({exceptionToErrorMethod.FullMethodName}(e));");
            }

            return code;
        }

        var additionalNamespaces = new List<string>();
        if (errorTypeNamespace != resultTypeNamespace && errorTypeNamespace != null)
            additionalNamespaces.Add(errorTypeNamespace);

        var generateFileHint = $"{resultTypeNamespace}.{resultTypeSchema.ResultTypeName}";

        var resultTypeImpl = Replace(Templates.ResultTypeTemplates.ResultType, additionalNamespaces, "T1");

        //resultTypeImpl = $"//Generator runs: {RunCount.Increase(generateFileHint)}\r\n" + resultTypeImpl;
        
        yield return ($"{generateFileHint}.g.cs", resultTypeImpl);

        if (mergeErrorMethod != null)
        {
            var mergeMethodNamespace = mergeErrorMethod.Match(staticMerge: m => m.Namespace, errorTypeMember: _ => "");
            if (!string.IsNullOrEmpty(mergeMethodNamespace) && mergeMethodNamespace != resultTypeNamespace)
                additionalNamespaces.Add(mergeMethodNamespace!);

            var mergeCode = Replace(
                Templates.ResultTypeTemplates.ResultTypeWithMerge
                    .Replace("//generated aggregate methods", GenerateAggregateMethods(10))
                    .Replace("//generated aggregate extension methods", GenerateAggregateExtensionMethods(10, isValueType))
                    .Replace("Merge__MemberOrExtensionMethod", mergeErrorMethod.MethodName),
                additionalNamespaces,
                "T"
            );

            yield return ($"{generateFileHint}WithMerge.g.cs", mergeCode);
        }
    }

    static string GenerateAggregateExtensionMethods(int maxParameterCount, bool isValueType) => Generate(maxParameterCount, i => MakeAggregateExtensionMethod(i, isValueType));
    static string GenerateAggregateMethods(int maxParameterCount) => Generate(maxParameterCount, GenerateAggregateMethod);
        

    static string Generate(int maxParameterCount, Func<int, string> generateMethods) =>
        Enumerable
            .Range(2, maxParameterCount - 2)
            .Select(generateMethods)
            .ToSeparatedString("\n");

    static string MakeAggregateExtensionMethod(int typeParameterCount, bool isValueType)
    {
        var range = Enumerable.Range(1, typeParameterCount).ToImmutableArray();
        string Expand(Func<int, string> strAtIndex, string separator = ", ") => range.Select(strAtIndex).ToSeparatedString(separator); 

        var typeArguments = Expand(i => $"T{i}");
        var typeArgumentsWithResult = $"{typeArguments}, TResult";

        var parameterDeclarations = Expand(i => $"MyResult<T{i}> r{i}");
        var taskParameterDeclarations = Expand(i => $"global::System.Threading.Tasks.Task<MyResult<T{i}>> r{i}");
        var parametersWithCombine = $"{parameterDeclarations}, global::System.Func<{typeArgumentsWithResult}> combine";

        var okCheck = Expand(i => $"r{i} is MyResult<T{i}>.Ok_ ok{i}", " && ");
        var combineArguments = Expand(i => $"ok{i}.Value");

        var resultArrayElements = Expand(i => $"r{i}");
        var taskResultArrayElements = Expand(i => $"r{i}.Result");
        var tupleArguments = Expand(i => $"v{i}");

        return $@"
        public static MyResult<({typeArguments})> Aggregate<{typeArguments}>(this {parameterDeclarations}) => 
            Aggregate({resultArrayElements}, ({tupleArguments}) => ({tupleArguments}));

        public static MyResult<TResult> Aggregate<{typeArgumentsWithResult}>(this {parametersWithCombine})            
        {{
            if ({okCheck})
                return combine({combineArguments});

            return MyResult.Error<TResult>(
                MergeErrors(new MyResult[] {{ {resultArrayElements} }}
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!{(isValueType ? ".Value" : "")})
                )!);
        }}
        
        public static global::System.Threading.Tasks.Task<MyResult<({typeArguments})>> Aggregate<{typeArguments}>(this {taskParameterDeclarations})
            => Aggregate({resultArrayElements}, ({tupleArguments}) => ({tupleArguments}));

        public static async global::System.Threading.Tasks.Task<MyResult<TResult>> Aggregate<{typeArgumentsWithResult}>(this {taskParameterDeclarations}, global::System.Func<{typeArgumentsWithResult}> combine)            
        {{
            await global::System.Threading.Tasks.Task.WhenAll({resultArrayElements});
            return Aggregate({taskResultArrayElements}, combine);
        }}";
    }

    public static string GenerateAggregateMethod(int typeParameterCount)
    {
        var range = Enumerable.Range(1, typeParameterCount).ToImmutableArray();
        string Expand(Func<int, string> strAtIndex, string separator = ", ") => range.Select(strAtIndex).ToSeparatedString(separator); 

        var typeParameters = Expand(i => $"T{i}");
        var parameterDeclarations = Expand(i => $"MyResult<T{i}> r{i}");
        var taskParameterDeclarations = Expand(i => $"global::System.Threading.Tasks.Task<MyResult<T{i}>> r{i}");
        var parameters = Expand(i => $"r{i}");

        return $@"
        public static MyResult<({typeParameters})> Aggregate<{typeParameters}>({parameterDeclarations}) => MyResultExtension.Aggregate({parameters});

        public static MyResult<TResult> Aggregate<{typeParameters}, TResult>({parameterDeclarations}, global::System.Func<{typeParameters}, TResult> combine) => MyResultExtension.Aggregate({parameters}, combine);

        public static global::System.Threading.Tasks.Task<MyResult<({typeParameters})>> Aggregate<{typeParameters}>({taskParameterDeclarations}) => MyResultExtension.Aggregate({parameters});

        public static global::System.Threading.Tasks.Task<MyResult<TResult>> Aggregate<{typeParameters}, TResult>({taskParameterDeclarations}, global::System.Func<{typeParameters}, TResult> combine) => MyResultExtension.Aggregate({parameters}, combine);";
    }
}