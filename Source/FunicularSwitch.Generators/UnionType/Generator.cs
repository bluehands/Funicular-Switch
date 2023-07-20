using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.UnionType;

public static class Generator
{
	const string VoidMatchMethodName = "Switch";
	const string MatchMethodName = "Match";

	public static (string filename, string source) Emit(UnionTypeSchema unionTypeSchema,
        Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var builder = new CSharpBuilder();
        builder.WriteLine("#pragma warning disable 1591");
        builder.WriteUsings("System", "System.Threading.Tasks");

        void BlankLine()
        {
	        builder.WriteLine("");
        }

        using (unionTypeSchema.Namespace != null ? builder.Namespace(unionTypeSchema.Namespace) : null)
        {
            using (builder.StaticPartialClass($"{unionTypeSchema.TypeName.Replace(".", "_")}MatchExtension", unionTypeSchema.IsInternal ? "internal" : "public"))
            {
	            var thisTaskParameter = ThisParameter(unionTypeSchema, $"Task<{unionTypeSchema.FullTypeName}>");
	            var caseParameters = unionTypeSchema.Cases.Select(c => c.ParameterName).ToSeparatedString();
	            void WriteBodyForTaskExtension(string matchMethodName) => builder.WriteLine($"(await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters});");
	            void WriteBodyForAsyncTaskExtension(string matchMethodName) => builder.WriteLine($"await (await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters}).ConfigureAwait(false);");

                GenerateMatchMethod(builder, unionTypeSchema, "T");
                BlankLine();
                GenerateMatchMethod(builder, unionTypeSchema, "Task<T>");
                BlankLine();
                
                WriteMatchSignature(builder, unionTypeSchema, thisTaskParameter, "Task<T>", "T", "public static async");
                WriteBodyForTaskExtension(MatchMethodName);
                BlankLine();
                WriteMatchSignature(builder, unionTypeSchema, thisTaskParameter, "Task<T>", handlerReturnType: "Task<T>", "public static async");
                WriteBodyForAsyncTaskExtension(MatchMethodName);
                BlankLine();

                GenerateSwitchMethod(builder, unionTypeSchema, false);
                BlankLine();
                GenerateSwitchMethod(builder, unionTypeSchema, true);
                BlankLine();
                WriteSwitchSignature(builder: builder, unionTypeSchema: unionTypeSchema, thisParameter: thisTaskParameter, isAsync: false, asyncReturn: true, lambda: true);
                WriteBodyForTaskExtension(VoidMatchMethodName);
                BlankLine();
                WriteSwitchSignature(builder: builder, unionTypeSchema: unionTypeSchema, thisParameter: thisTaskParameter, isAsync: true, lambda: true);
                WriteBodyForAsyncTaskExtension(VoidMatchMethodName);
            }
        }

        builder.WriteLine("#pragma warning restore 1591");
        return (unionTypeSchema.FullTypeName.ToMatchExtensionFilename(), builder.ToString());
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
                builder.WriteLine(
	                unionTypeSchema.IsEnum
		                ? $"{c.FullTypeName} => {c.ParameterName}(),"
		                : $"{c.FullTypeName} case{caseIndex} => {c.ParameterName}(case{caseIndex}),");
            }

            builder.WriteLine(
                $"_ => throw new ArgumentException($\"Unknown type derived from {unionTypeSchema.FullTypeName}: {{{thisParameterName}.GetType().Name}}\")");
        }
    }

    static void GenerateSwitchMethod(CSharpBuilder builder, UnionTypeSchema unionTypeSchema, bool isAsync)
    {
	    var thisParameterType = unionTypeSchema.FullTypeName;
	    var thisParameter = ThisParameter(unionTypeSchema, thisParameterType);
	    var thisParameterName = thisParameter.Name;
	    WriteSwitchSignature(builder, unionTypeSchema, thisParameter, isAsync);
	    using (builder.Scope())
	    {
		    builder.WriteLine($"switch ({thisParameterName})");
		    using (builder.Scope())
		    {
			    var caseIndex = 0;
			    foreach (var c in unionTypeSchema.Cases)
			    {
				    caseIndex++;
				    builder.WriteLine(
					    unionTypeSchema.IsEnum
						    ? $"case {c.FullTypeName}:"
						    : $"case {c.FullTypeName} case{caseIndex}:"
				    );
				    using (builder.Indent())
				    {
					    var call = unionTypeSchema.IsEnum
						    ? $"{c.ParameterName}()"
						    : $"{c.ParameterName}(case{caseIndex})";
					    if (isAsync)
						    call = $"await {call}.ConfigureAwait(false)";
					    builder.WriteLine($"{call};");
					    builder.WriteLine("break;");
				    }
			    }

			    builder.WriteLine("default:");
			    using (builder.Indent())
			    {
				    builder.WriteLine($"throw new ArgumentException($\"Unknown type derived from {unionTypeSchema.FullTypeName}: {{{thisParameterName}.GetType().Name}}\");");
			    }
		    }
	    }
    }

    static Parameter ThisParameter(UnionTypeSchema unionTypeSchema, string thisParameterType) => new($"this {thisParameterType}", unionTypeSchema.TypeName.ToParameterName());

    static void WriteMatchSignature(CSharpBuilder builder, UnionTypeSchema unionTypeSchema,
        Parameter thisParameter, string returnType, string? handlerReturnType = null, string modifiers = "public static")
    {
        handlerReturnType ??= returnType;
        var handlerParameters = unionTypeSchema.Cases
            .Select(c => new Parameter( unionTypeSchema.IsEnum ? $"Func<{handlerReturnType}>" : $"Func<{c.FullTypeName}, {handlerReturnType}>", c.ParameterName));

        builder.WriteMethodSignature(
            modifiers: modifiers,
            returnType: returnType,
            methodName: "Match<T>", parameters: new[] { thisParameter }.Concat(handlerParameters),
            lambda: true);
    }

    static void WriteSwitchSignature(CSharpBuilder builder, UnionTypeSchema unionTypeSchema,
	    Parameter thisParameter, bool isAsync, bool? asyncReturn = null, bool lambda = false)
    {
	    var returnType = asyncReturn ?? isAsync ? "async Task" : "void";
        var handlerParameters = unionTypeSchema.Cases
		    .Select(c =>
		    {
			    var parameterType = unionTypeSchema.IsEnum
				    ? isAsync
					    ? "Func<Task>"
					    : "Action"
				    : isAsync
					    ? $"Func<{c.FullTypeName}, Task>"
					    : $"Action<{c.FullTypeName}>";
			    return new Parameter(
					    parameterType,
					    c.ParameterName);
		    });

        string modifiers = "public static";

        builder.WriteMethodSignature(
		    modifiers: modifiers,
		    returnType: returnType,
		    methodName: VoidMatchMethodName, parameters: new[] { thisParameter }.Concat(handlerParameters),
		    lambda: lambda);
    }
}