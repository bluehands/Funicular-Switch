using System.Collections.Immutable;
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
		builder.WriteUsings("System", "System.Threading.Tasks");

		using (unionTypeSchema.Namespace != null ? builder.Namespace(unionTypeSchema.Namespace) : null)
		{
			WriteMatchExtension(unionTypeSchema, builder);
			if (unionTypeSchema is { IsPartial: true, StaticFactoryInfo: not null })
			{
				builder.WriteLine("");
				WritePartialWithStaticFactories(unionTypeSchema, builder);
			}
		}

		builder.WriteLine("#pragma warning restore 1591");
		return (unionTypeSchema.FullTypeName.ToMatchExtensionFilename(), builder.ToString());
	}

	static void WriteMatchExtension(UnionTypeSchema unionTypeSchema, CSharpBuilder builder)
	{
		using (builder.StaticPartialClass($"{unionTypeSchema.TypeName.Replace(".", "_")}MatchExtension",
				   unionTypeSchema.IsInternal ? "internal" : "public"))
		{
			var thisTaskParameter = ThisParameter(unionTypeSchema, $"Task<{unionTypeSchema.FullTypeName}>");
			var caseParameters = unionTypeSchema.Cases.Select(c => c.ParameterName).ToSeparatedString();

			void WriteBodyForTaskExtension(string matchMethodName) => builder.WriteLine(
				$"(await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters});");

			void WriteBodyForAsyncTaskExtension(string matchMethodName) => builder.WriteLine(
				$"await (await {thisTaskParameter.Name}.ConfigureAwait(false)).{matchMethodName}({caseParameters}).ConfigureAwait(false);");

			GenerateMatchMethod(builder, unionTypeSchema, "T");
			BlankLine();
			GenerateMatchMethod(builder, unionTypeSchema, "Task<T>");
			BlankLine();

			WriteMatchSignature(builder, unionTypeSchema, thisTaskParameter, "Task<T>", "T", "public static async");
			WriteBodyForTaskExtension(MatchMethodName);
			BlankLine();
			WriteMatchSignature(builder, unionTypeSchema, thisTaskParameter, "Task<T>", handlerReturnType: "Task<T>",
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

		void BlankLine()
		{
			builder.WriteLine("");
		}
	}

	static void WritePartialWithStaticFactories(UnionTypeSchema unionTypeSchema, CSharpBuilder builder)
	{
		var info = unionTypeSchema.StaticFactoryInfo!;

		builder.WriteLine($"{(info.Modifiers.Select(m => m.Text).ToSeparatedString(" "))} {(unionTypeSchema.IsRecord ? "record" : "class")} {unionTypeSchema.TypeName}");
		using (builder.Scope())
		{
			foreach (var derivedType in unionTypeSchema.Cases)
			{
				var nameParts = derivedType.FullTypeName.Split('.');
				var derivedTypeName = nameParts[nameParts.Length - 1];
				var methodName = derivedTypeName.Trim('_');

				if ($"{unionTypeSchema.FullTypeName}.{methodName}" == derivedType.FullTypeName) //union case is nested type without underscores, so factory method name would conflict with type name
					continue;

				var constructors = derivedType.Constructors;
				if (constructors.Count == 0)
					constructors = new[]
					{
						new MemberInfo($"{derivedTypeName}",
							SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
							ImmutableList<ParameterInfo>.Empty)
					};

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
					builder.WriteLine($"{(isInternal ? "internal" : "public")} static {unionTypeSchema.FullTypeName} {methodName}({arguments}) => {constructorInvocation};");
				}
			}
		}
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
			.Select(c => new Parameter(unionTypeSchema.IsEnum ? $"Func<{handlerReturnType}>" : $"Func<{c.FullTypeName}, {handlerReturnType}>", c.ParameterName));

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