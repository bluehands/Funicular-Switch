using FunicularSwitch.Generators.Generation;
using FunicularSwitch.Generators.ResultType;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.UnionType;

public static class Generator
{
    public static (string filename, string source) Emit(UnionTypeSchema unionTypeSchema,
        Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var builder = new CSharpBuilder();
        builder.WriteUsings("System", "System.Threading.Tasks");

        using (builder.Namespace(unionTypeSchema.Namespace))
        {
            using (builder.PublicStaticPartialClass("MatchExtension"))
            {
                GenerateMatchMethod(builder, unionTypeSchema, "T");
                builder.WriteLine("");
                GenerateMatchMethod(builder, unionTypeSchema, "Task<T>");
                builder.WriteLine("");
                var thisParameter = ThisParameter(unionTypeSchema, $"Task<{unionTypeSchema.FullTypeName}>");
                WriteMatchSignature(builder, unionTypeSchema, thisParameter, "Task<T>", "T", "public static async");
                var caseParameters = unionTypeSchema.Cases.Select(c => c.ParameterName).ToSeparatedString();
                builder.WriteLine($"(await {thisParameter.Name}.ConfigureAwait(false)).Match({caseParameters});");
                builder.WriteLine("");
                var thisParameter1 = ThisParameter(unionTypeSchema, $"Task<{unionTypeSchema.FullTypeName}>");
                WriteMatchSignature(builder, unionTypeSchema, thisParameter1, "Task<T>", handlerReturnType: "Task<T>", "public static async");
                builder.WriteLine($"await (await {thisParameter1.Name}.ConfigureAwait(false)).Match({caseParameters}).ConfigureAwait(false);");
            }
        }

        return ($"{unionTypeSchema.TypeName}MatchExtension.g.cs", builder.ToString());
    }

    static void GenerateMatchMethod(CSharpBuilder builder, UnionTypeSchema unionTypeSchema, string t)
    {
        var thisParameterType = unionTypeSchema.FullTypeName;
        var thisParameter = ThisParameter(unionTypeSchema, thisParameterType);
        var thisParameterName = thisParameter.Name;
        WriteMatchSignature(builder, unionTypeSchema, thisParameter, t);
        builder.WriteLine($"{thisParameterName} switch");
        using (builder.ScopeWithSemicolon())
        {
            var caseIndex = 0;
            foreach (var c in unionTypeSchema.Cases)
            {
                caseIndex++;
                builder.WriteLine($"{c.FullTypeName} case{caseIndex} => {c.ParameterName}(case{caseIndex}),");
            }

            builder.WriteLine(
                $"_ => throw new ArgumentException($\"Unknown type derived from {unionTypeSchema.FullTypeName}: {{{thisParameterName}.GetType().Name}}\")");
        }
    }

    static Parameter ThisParameter(UnionTypeSchema unionTypeSchema, string thisParameterType) => new($"this {thisParameterType}", unionTypeSchema.TypeName.ToParameterName());

    static void WriteMatchSignature(CSharpBuilder builder, UnionTypeSchema unionTypeSchema,
        Parameter thisParameter, string returnType, string? handlerReturnType = null, string modifiers = "public static")
    {
        handlerReturnType ??= returnType;
        var handlerParameters = unionTypeSchema.Cases
            .Select(c => new Parameter($"Func<{c.FullTypeName}, {handlerReturnType}>", c.ParameterName));

        builder.WriteMethodSignature(
            modifiers: modifiers,
            returnType: returnType,
            methodName: "Match<T>", parameters: new[] { thisParameter }.Concat(handlerParameters),
            lambda: true);
    }
}