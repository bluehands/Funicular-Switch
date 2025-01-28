using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.EnumType;

static class Generator
{
	const string VoidMatchMethodName = "Switch";
	const string MatchMethodName = "Match";

	public static (string filename, string source) Emit(EnumTypeSchema enumTypeSchema,
        Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var builder = new CSharpBuilder();
        builder.WriteLine("#pragma warning disable 1591");

        //builder.WriteLine($"//Generator runs: {RunCount.Increase(enumTypeSchema.FullTypeName)}");
        void BlankLine()
        {
	        builder.WriteLine("");
        }

        using (enumTypeSchema.Namespace != null ? builder.Namespace(enumTypeSchema.Namespace) : null)
        {
            using (builder.StaticPartialClass($"{enumTypeSchema.TypeName.Replace(".", "_")}MatchExtension", enumTypeSchema.IsInternal ? "internal" : "public"))
            {
	            var thisTaskParameter = ThisParameter(enumTypeSchema, $"global::System.Threading.Tasks.Task<{enumTypeSchema.FullTypeName}>");
	            var caseParameters = enumTypeSchema.Cases.Select(c => c.ParameterName).ToSeparatedString();
	            void WriteBodyForTaskExtension(string matchMethodName) => builder.WriteLine($"(await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters});");
	            void WriteBodyForAsyncTaskExtension(string matchMethodName) => builder.WriteLine($"await (await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters}).ConfigureAwait(false);");

                GenerateMatchMethod(builder, enumTypeSchema, "T");
                BlankLine();
                GenerateMatchMethod(builder, enumTypeSchema, "global::System.Threading.Tasks.Task<T>");
                BlankLine();
                
                WriteMatchSignature(builder, enumTypeSchema, thisTaskParameter, "global::System.Threading.Tasks.Task<T>", "T", "public static async");
                WriteBodyForTaskExtension(MatchMethodName);
                BlankLine();
                WriteMatchSignature(builder, enumTypeSchema, thisTaskParameter, "global::System.Threading.Tasks.Task<T>", handlerReturnType: "global::System.Threading.Tasks.Task<T>", "public static async");
                WriteBodyForAsyncTaskExtension(MatchMethodName);
                BlankLine();

                GenerateSwitchMethod(builder, enumTypeSchema, false);
                BlankLine();
                GenerateSwitchMethod(builder, enumTypeSchema, true);
                BlankLine();
                WriteSwitchSignature(builder: builder, enumTypeSchema: enumTypeSchema, thisParameter: thisTaskParameter, isAsync: false, asyncReturn: true, lambda: true);
                WriteBodyForTaskExtension(VoidMatchMethodName);
                BlankLine();
                WriteSwitchSignature(builder: builder, enumTypeSchema: enumTypeSchema, thisParameter: thisTaskParameter, isAsync: true, lambda: true);
                WriteBodyForAsyncTaskExtension(VoidMatchMethodName);
            }
        }

        builder.WriteLine("#pragma warning restore 1591");
        return (enumTypeSchema.FullTypeName.ToMatchExtensionFilename(ImmutableArray<string>.Empty), builder.ToString());
    }

    static void GenerateMatchMethod(CSharpBuilder builder, EnumTypeSchema enumTypeSchema, string t)
    {
        var thisParameterType = enumTypeSchema.FullTypeName;
        var thisParameter = ThisParameter(enumTypeSchema, thisParameterType);
        var thisParameterName = thisParameter.Name;
        WriteMatchSignature(builder, enumTypeSchema, thisParameter, t);
        builder.WriteLine($"{thisParameterName} switch");
        using (builder.ScopeWithSemicolon())
        {
	        foreach (var c in enumTypeSchema.Cases)
            {
	            builder.WriteLine($"{c.FullCaseName} => {c.ParameterName}(),");
            }

            builder.WriteLine(
                $"_ => throw new global::System.ArgumentException($\"Unknown enum value from {enumTypeSchema.FullTypeName}: {{{thisParameterName}.GetType().Name}}\")");
        }
    }

    static void GenerateSwitchMethod(CSharpBuilder builder, EnumTypeSchema enumTypeSchema, bool isAsync)
    {
	    var thisParameterType = enumTypeSchema.FullTypeName;
	    var thisParameter = ThisParameter(enumTypeSchema, thisParameterType);
	    var thisParameterName = thisParameter.Name;
	    WriteSwitchSignature(builder, enumTypeSchema, thisParameter, isAsync);
	    using (builder.Scope())
	    {
		    builder.WriteLine($"switch ({thisParameterName})");
		    using (builder.Scope())
		    {
			    foreach (var c in enumTypeSchema.Cases)
			    {
				    builder.WriteLine($"case {c.FullCaseName}:");
				    using (builder.Indent())
				    {
					    var call = $"{c.ParameterName}()";
					    if (isAsync)
						    call = $"await {call}.ConfigureAwait(false)";
					    builder.WriteLine($"{call};");
					    builder.WriteLine("break;");
				    }
			    }

			    builder.WriteLine("default:");
			    using (builder.Indent())
			    {
				    builder.WriteLine($"throw new global::System.ArgumentException($\"Unknown enum value from {enumTypeSchema.FullTypeName}: {{{thisParameterName}.GetType().Name}}\");");
			    }
		    }
	    }
    }

    static Parameter ThisParameter(EnumTypeSchema enumTypeSchema, string thisParameterType) => new($"this {thisParameterType}", enumTypeSchema.TypeName.ToParameterName());

    static void WriteMatchSignature(CSharpBuilder builder, EnumTypeSchema enumTypeSchema,
        Parameter thisParameter, string returnType, string? handlerReturnType = null, string modifiers = "public static")
    {
        handlerReturnType ??= returnType;
        var handlerParameters = enumTypeSchema.Cases
            .Select(c => new Parameter($"global::System.Func<{handlerReturnType}>", c.ParameterName));

        builder.WriteMethodSignature(
            modifiers: modifiers,
            returnType: returnType,
            methodName: "Match<T>", parameters: new[] { thisParameter }.Concat(handlerParameters),
            lambda: true);
    }

    static void WriteSwitchSignature(CSharpBuilder builder, EnumTypeSchema enumTypeSchema,
	    Parameter thisParameter, bool isAsync, bool? asyncReturn = null, bool lambda = false)
    {
	    var returnType = asyncReturn ?? isAsync ? "async global::System.Threading.Tasks.Task" : "void";
        var handlerParameters = enumTypeSchema.Cases
		    .Select(c => new Parameter(isAsync ? "global::System.Func<global::System.Threading.Tasks.Task>" : "global::System.Action", c.ParameterName));

        string modifiers = "public static";

        builder.WriteMethodSignature(
		    modifiers: modifiers,
		    returnType: returnType,
		    methodName: VoidMatchMethodName, parameters: new[] { thisParameter }.Concat(handlerParameters),
		    lambda: lambda);
    }
}