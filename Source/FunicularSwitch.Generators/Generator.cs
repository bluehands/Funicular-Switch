using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

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
            yield return ($"{resultTypeSchema.ResultType.Identifier}WithMerge.g.cs", Replace(
                        Templates.Templates.ResultTypeWithMerge
                            .Replace("//generated aggregate methods", MakeAggregateExtensions(10))
                    )
                );
    }

    static string MakeAggregateExtensions(int maxParameterCount) => 
        Enumerable
            .Range(2, maxParameterCount - 2)
            .Select(MakeAggregateExtension)
            .ToSeparatedString(Environment.NewLine);

    static string MakeAggregateExtension(int typeParameterCount)
    {
        var range = Enumerable.Range(1, typeParameterCount).ToImmutableArray();

        string Expand(Func<int, string> strAtIndex, string separator = ", ") => range.Select(strAtIndex).ToSeparatedString(separator); 

        var typeArguments = Expand(i => $"T{i}");
        var typeArgumentsWithResult = $"{typeArguments}, TResult";

        var parameters = Expand(i => $"MyResult<T{i}> r{i}");
        var taskParameters = Expand(i => $"Task<MyResult<T{i}>> r{i}");
        var parametersWithCombine = $"{parameters}, Func<{typeArgumentsWithResult}> combine";

        var okCheck = Expand(i => $"r{i} is MyResult<T{i}>.Ok_ ok{i}", " && ");
        var combineArguments = Expand(i => $"ok{i}.Value");

        var resultArrayElements = Expand(i => $"r{i}");
        var taskResultArrayElements = Expand(i => $"r{i}.Result");
        var tupleArguments = Expand(i => $"v{i}");

        return $@"
        public static MyResult<({typeArguments})> Aggregate<{typeArguments}>(this {parameters}) => 
            Aggregate({resultArrayElements}, ({tupleArguments}) => ({tupleArguments}));

        public static MyResult<TResult> Aggregate<{typeArgumentsWithResult}>(this {parametersWithCombine})            
        {{
            if ({okCheck})
                return combine({combineArguments});

            return MyResult.Error<TResult>(
                MergeErrors(new MyResult[] {{ {resultArrayElements} }}
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }}
        
        public static Task<MyResult<({typeArguments})>> Aggregate<{typeArguments}>(this {taskParameters})
            => Aggregate({resultArrayElements}, ({tupleArguments}) => ({tupleArguments}));

        public static async Task<MyResult<TResult>> Aggregate<{typeArgumentsWithResult}>(this {taskParameters}, Func<{typeArgumentsWithResult}> combine)            
        {{
            await Task.WhenAll({resultArrayElements});
            return Aggregate({taskResultArrayElements}, combine);
        }}";
    }

    static string ToSeparatedString<T>(this IEnumerable<T> items, string separator = ", ") => string.Join(separator, items);
}