using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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

		//builder.WriteLine($"//Generator runs: {RunCount.Increase(unionTypeSchema.FullTypeName)}");

		using (unionTypeSchema.Namespace != null ? builder.Namespace(unionTypeSchema.Namespace) : null)
		{
            if (unionTypeSchema.TypeParameters.Length > 0)
            {
                if (unionTypeSchema.IsPartial)
                {
                    WritePartialWithMatchMethods(unionTypeSchema, builder);
                }
            }
            else
            {
                WriteMatchExtension(unionTypeSchema, builder);
            }

            if (unionTypeSchema is { IsPartial: true, StaticFactoryInfo: not null })
            {
                builder.WriteLine("");
                WritePartialWithStaticFactories(unionTypeSchema, builder);
            }
        }

		builder.WriteLine("#pragma warning restore 1591");
		return (unionTypeSchema.FullTypeName.ToMatchExtensionFilename(unionTypeSchema.TypeParameters), builder.ToString());
	}

    private static void WriteMatchExtension(UnionTypeSchema unionTypeSchema, CSharpBuilder builder)
	{
		using (builder.StaticPartialClass($"{unionTypeSchema.TypeName.Replace(".", "_")}MatchExtension",
				   unionTypeSchema.IsInternal ? "internal" : "public"))
		{
			var thisTaskParameter = ThisParameter(unionTypeSchema, $"global::System.Threading.Tasks.Task<{unionTypeSchema.FullTypeName}>");
			var caseParameters = unionTypeSchema.Cases.Select(c => c.ParameterName).ToSeparatedString();

			void WriteBodyForTaskExtension(string matchMethodName) => builder.WriteLine(
				$"(await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters});");

			void WriteBodyForAsyncTaskExtension(string matchMethodName) => builder.WriteLine(
				$"await (await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters}).ConfigureAwait(false);");

			GenerateMatchMethod(builder, unionTypeSchema, "T");
			BlankLine();
			GenerateMatchMethod(builder, unionTypeSchema, "global::System.Threading.Tasks.Task<T>");
			BlankLine();

			WriteMatchSignature(builder, unionTypeSchema, thisTaskParameter, "global::System.Threading.Tasks.Task<T>", "T", "public static async");
			WriteBodyForTaskExtension(MatchMethodName);
			BlankLine();
			WriteMatchSignature(builder, unionTypeSchema, thisTaskParameter, "global::System.Threading.Tasks.Task<T>", handlerReturnType: "global::System.Threading.Tasks.Task<T>",
				"public static async");
			WriteBodyForAsyncTaskExtension(MatchMethodName);
			BlankLine();

			GenerateSwitchMethod(builder, unionTypeSchema, false);
			BlankLine();
			GenerateSwitchMethod(builder, unionTypeSchema, true);
			BlankLine();
			WriteSwitchSignature(builder: builder, unionTypeSchema: unionTypeSchema, thisParameter: thisTaskParameter,
				isAsync: false, asyncReturn: true, lambda: true);
			WriteBodyForTaskExtension(VoidMatchMethodName);
			BlankLine();
			WriteSwitchSignature(builder: builder, unionTypeSchema: unionTypeSchema, thisParameter: thisTaskParameter,
				isAsync: true, lambda: true);
			WriteBodyForAsyncTaskExtension(VoidMatchMethodName);
		}

        return;

		void BlankLine()
		{
			builder.WriteLine("");
		}
	}

    private static void WritePartialWithMatchMethods(UnionTypeSchema unionTypeSchema, CSharpBuilder builder)
    {
        var typeParameters = RoslynExtensions.FormatTypeParameters(unionTypeSchema.TypeParameters);
        var unusedTypeParameter = GetUnusedTypeParameter(unionTypeSchema.TypeParameters);
        var typeKind = GetTypeKind(unionTypeSchema);
		builder.WriteLine($"{(unionTypeSchema.Modifiers.ToSeparatedString(" "))} {typeKind} {unionTypeSchema.TypeName}{typeParameters}");
        using (builder.Scope())
        {
			GenerateMatchMethod(builder, unionTypeSchema, returnType: unusedTypeParameter, t: unusedTypeParameter, asExtension: false);
            BlankLine();

			GenerateSwitchMethod(builder, unionTypeSchema, isAsync: false, asExtension: false);
            BlankLine();
			GenerateSwitchMethod(builder, unionTypeSchema, isAsync: true, asExtension: false);
        }

        return;

        void BlankLine()
        {
			builder.WriteLine("");
        }
    }

    private static void WritePartialWithStaticFactories(UnionTypeSchema unionTypeSchema, CSharpBuilder builder)
	{
		var info = unionTypeSchema.StaticFactoryInfo!;
        var typeParameters = RoslynExtensions.FormatTypeParameters(unionTypeSchema.TypeParameters);

        var typeKind = GetTypeKind(unionTypeSchema);
        builder.WriteLine($"{(unionTypeSchema.Modifiers.ToSeparatedString(" "))} {typeKind} {unionTypeSchema.TypeName}{typeParameters}");
		using (builder.Scope())
		{
			foreach (var derivedType in unionTypeSchema.Cases)
			{
				var nameParts = derivedType.FullTypeName.Split('.');
				var derivedTypeName = nameParts[nameParts.Length - 1];
				var methodName = derivedType.StaticFactoryMethodName;

				if ($"{unionTypeSchema.FullTypeNameWithTypeParameters}.{methodName}" == derivedType.FullTypeName) //union case is nested type without underscores, so factory method name would conflict with type name
					continue;

				var constructors = derivedType.Constructors;
				if (constructors.Length == 0)
					constructors = new[]
					{
						new MemberInfo($"{derivedTypeName}",
							ImmutableArray<string>.Empty.Add("public"),
							ImmutableArray<ParameterInfo>.Empty)
					}.ToImmutableArray();

				foreach (var constructor in constructors)
				{
					var isPublic = constructor.Modifiers.HasModifier(SyntaxKind.PublicKeyword);
					var isInternal = !isPublic && constructor.Modifiers.HasModifier(SyntaxKind.InternalKeyword);

					if (!isInternal && !isPublic)
						continue; //constructor inaccessible

					if (info.ExistingStaticFields.Contains(methodName))
						continue; //name conflict with existing field

					if (info.ExistingStaticMethods.Any(s =>
							s.Name == methodName &&
							s.Parameters.Select(p => p.Type)
								.SequenceEqual(constructor.Parameters.Select(p => p.Type), SymbolEqualityComparer.Default)))
						continue; //static method already exists

					var arguments = constructor.Parameters.ToSeparatedString();
					var constructorInvocation = $"new {derivedType.FullTypeName}({(constructor.Parameters.Select(p => p.Name).ToSeparatedString())})";
					builder.WriteLine($"{(isInternal ? "internal" : "public")} static {unionTypeSchema.FullTypeName}{typeParameters} {methodName}({arguments}) => {constructorInvocation};");
				}
			}
		}
	}

    private static string GetTypeKind(UnionTypeSchema unionTypeSchema)
    {
        var typeKind = unionTypeSchema.TypeKind switch { UnionTypeTypeKind.Class => "class", UnionTypeTypeKind.Interface => "interface", UnionTypeTypeKind.Record => "record", _ => throw new ArgumentException($"Unknown type kind: {unionTypeSchema.TypeKind}") };
        return typeKind;
    }

    static void GenerateMatchMethod(CSharpBuilder builder, UnionTypeSchema unionTypeSchema, string returnType, string t = "T", bool asExtension = true)
	{
		var thisParameterType = unionTypeSchema.FullTypeName;
		var thisParameter = ThisParameter(unionTypeSchema, thisParameterType);
		var thisParameterName = thisParameter.Name;
        var thisStatement = asExtension ? thisParameterName : "this";
		WriteMatchSignature(
            builder: builder,
            unionTypeSchema: unionTypeSchema,
            thisParameter: asExtension ? thisParameter : null,
            returnType: returnType,
			t: t,
            modifiers: asExtension ? "public static" : "public");
		builder.WriteLine($"{thisStatement} switch");
		using (builder.ScopeWithSemicolon())
		{
			var caseIndex = 0;
			foreach (var c in unionTypeSchema.Cases)
			{
				caseIndex++;
                var caseVariableName = $"{c.ParameterName}{caseIndex}";
				builder.WriteLine($"{c.FullTypeName} {caseVariableName} => {c.ParameterName}({caseVariableName}),");
			}

			builder.WriteLine(
				$"_ => throw new global::System.ArgumentException($\"Unknown type derived from {unionTypeSchema.FullTypeName}: {{{thisStatement}.GetType().Name}}\")");
		}
	}

	static void GenerateSwitchMethod(CSharpBuilder builder, UnionTypeSchema unionTypeSchema, bool isAsync, bool asExtension = true)
	{
		var thisParameterType = unionTypeSchema.FullTypeName;
		var thisParameter = ThisParameter(unionTypeSchema, thisParameterType);
		var thisParameterName = thisParameter.Name;
        var thisStatement = asExtension ? thisParameterName : "this";
        WriteSwitchSignature(builder, unionTypeSchema, asExtension ? thisParameter : null, isAsync, modifiers: asExtension ? "public static" : "public");
		using (builder.Scope())
		{
			builder.WriteLine($"switch ({thisStatement})");
			using (builder.Scope())
			{
				var caseIndex = 0;
				foreach (var c in unionTypeSchema.Cases)
				{
					caseIndex++;
                    var caseVariableName = $"{c.ParameterName}{caseIndex}";
					builder.WriteLine($"case {c.FullTypeName} {caseVariableName}:");
					using (builder.Indent())
					{
						var call = $"{c.ParameterName}({caseVariableName})";
						if (isAsync)
							call = $"await {call}.ConfigureAwait(false)";
						builder.WriteLine($"{call};");
						builder.WriteLine("break;");
					}
				}

				builder.WriteLine("default:");
				using (builder.Indent())
				{
					builder.WriteLine($"throw new global::System.ArgumentException($\"Unknown type derived from {unionTypeSchema.FullTypeName}: {{{thisStatement}.GetType().Name}}\");");
				}
			}
		}
    }

    private static string GetUnusedTypeParameter(EquatableArray<string> typeParameters)
    {
        return Enumerable.Range(0, 20)
            .Select(i => Check(new string('_', i) + "TMatchResult"))
            .FirstOrDefault(s => s is not null) ?? "T" + Guid.NewGuid().ToString("N");

        string? Check(string typeName)
        {
            if (!typeParameters.Contains(typeName))
            {
                return typeName;
            }

            return null;
        }
    }

    static Parameter ThisParameter(UnionTypeSchema unionTypeSchema, string thisParameterType) => new($"this {thisParameterType}", unionTypeSchema.TypeName.ToParameterName());

	static void WriteMatchSignature(CSharpBuilder builder, UnionTypeSchema unionTypeSchema,
		Parameter? thisParameter, string returnType, string? handlerReturnType = null, string modifiers = "public static", string t = "T")
	{
		handlerReturnType ??= returnType;
		var handlerParameters = unionTypeSchema.Cases
			.Select(c => new Parameter($"global::System.Func<{c.FullTypeName}, {handlerReturnType}>", c.ParameterName));

        if (thisParameter is not null)
        {
            handlerParameters = handlerParameters.Prepend(thisParameter);
        }
        builder.WriteMethodSignature(
			modifiers: modifiers,
			returnType: returnType,
			methodName: "Match<" + t + ">", parameters: handlerParameters,
			lambda: true);
	}

	static void WriteSwitchSignature(CSharpBuilder builder, UnionTypeSchema unionTypeSchema,
		Parameter? thisParameter, bool isAsync, string modifiers = "public static", bool? asyncReturn = null, bool lambda = false)
	{
		var returnType = asyncReturn ?? isAsync ? "async global::System.Threading.Tasks.Task" : "void";
		var handlerParameters = unionTypeSchema.Cases
			.Select(c =>
			{
				var parameterType = isAsync
						? $"global::System.Func<{c.FullTypeName}, global::System.Threading.Tasks.Task>"
						: $"global::System.Action<{c.FullTypeName}>";
				return new Parameter(
						parameterType,
						c.ParameterName);
			});
        if (thisParameter is not null)
        {
            handlerParameters = handlerParameters.Prepend(thisParameter);
        }

        builder.WriteMethodSignature(
			modifiers: modifiers,
			returnType: returnType,
			methodName: VoidMatchMethodName, parameters: handlerParameters,
			lambda: lambda);
	}
}