using System.Collections.Immutable;
using System.Diagnostics;
using FunicularSwitch.Generators.EnumType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class EnumTypeGenerator : IIncrementalGenerator
{
	internal const string EnumTypeAttribute = "FunicularSwitch.Generators.EnumTypeAttribute";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
			"Attributes.g.cs",
			Templates.EnumTypeTemplates.StaticCode));

		var enumTypeClasses =
			context.SyntaxProvider
				.CreateSyntaxProvider(
					predicate: static (s, _) => s is EnumDeclarationSyntax && s.IsTypeDeclarationWithAttributes(),
					transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration3(ctx, EnumTypeAttribute)
				)
				.Where(static target => target != null)
				.Select(static (target, _) => target!);

		context.RegisterSourceOutput(
			enumTypeClasses,
			static (spc, source) => Execute(source, spc));
	}

	static void Execute(EnumTypeSchema enumTypeSchema, SourceProductionContext context)
	{
		//FileLog.LogAccess();

		var (filename, source) = Generator.Emit(enumTypeSchema, context.ReportDiagnostic, context.CancellationToken);

		context.AddSource(filename, source);
	}

	static void Execute(Compilation compilation, ImmutableArray<BaseTypeDeclarationSyntax> enumTypeClasses, SourceProductionContext context)
	{
		if (enumTypeClasses.IsDefaultOrEmpty) return;

		//FileLog.LogAccess();

		var resultTypeSchemata =
			Parser.GetEnumTypes(compilation, enumTypeClasses, context.ReportDiagnostic, context.CancellationToken)
				.ToImmutableArray();

		var generation =
			resultTypeSchemata.Select(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken));

		foreach (var (filename, source) in generation) context.AddSource(filename, source);
	}
}

internal class FileLog
{
	public static void LogAccess(TimeSpan elapsed, string message)
	{
		var filePath = @"c:\temp\callcount.txt";
		var line = $"{DateTime.Now:HH:mm:ss fff}: {message} ({elapsed})";
		if (!File.Exists(filePath))
		{
			File.WriteAllText(filePath, line);
		}
		else
		{
			File.AppendAllLines(filePath, new[] { line });
		}
	}
}

[Generator]
public class MatchForEnumTypesGenerator : IIncrementalGenerator
{
	internal const string EnumTypeAttribute = "FunicularSwitch.Generators.ExtendEnumTypesAttribute";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var enumTypeClasses =
			context.SyntaxProvider
				.CreateSyntaxProvider(
					predicate: static (s, _) => s is AttributeSyntax,
					transform: static (ctx, _) => GeneratorHelper.GetSemanticTargetForGeneration2(ctx, EnumTypeAttribute)
				)
				.Where(static target => target != null)
				.Select(static (target, _) => target!);

		var globalNamespace = context.CompilationProvider
			.Select((c, _) => GeneratorHelper.SymbolWrapper.Create(c.GlobalNamespace));

		var compilationAndClasses = enumTypeClasses.Combine(globalNamespace);

		var relevantNamespaces =
			compilationAndClasses
				.SelectMany((t, c) =>
				{
					static IEnumerable<INamespaceSymbol> GetNamespaceWithTypeMembers(INamespaceSymbol namespaceSymbol,
						ImmutableHashSet<ISymbol> relevantAssemblies)
					{
						if (!namespaceSymbol.IsGlobalNamespace &&
						    !relevantAssemblies.Contains(namespaceSymbol.ContainingAssembly))
							yield break;

						if (!namespaceSymbol.IsGlobalNamespace && namespaceSymbol.GetTypeMembers().Any())
							yield return namespaceSymbol;

						foreach (var subNamespace in namespaceSymbol.GetNamespaceMembers())
						{
							foreach (var namedTypeSymbol in GetNamespaceWithTypeMembers(subNamespace,
								         relevantAssemblies))
							{
								yield return namedTypeSymbol;
							}
						}
					}

					var assemblies = ImmutableHashSet
						.Create<ISymbol>(SymbolEqualityComparer.Default, t.Left.AssemblySymbol);

					var namespaceWithTypeMembers = GetNamespaceWithTypeMembers(t.Right.Symbol, assemblies!)
							.Select(s => (t.Left, GeneratorHelper.SymbolWrapper.Create(s)));
					return namespaceWithTypeMembers;
				});

		context.RegisterSourceOutput(
			relevantNamespaces,
			static (spc, source) => Execute(source.Item2.Symbol, ImmutableArray.Create(source.Left), spc)
			);
	}

	static void Execute(INamespaceSymbol compilation, ImmutableArray<GeneratorHelper.EnumTypesAttributeInfo> enumTypesAttributes, SourceProductionContext context)
	{
		//if (!Debugger.IsAttached)
		//	Debugger.Launch();

		if (enumTypesAttributes.IsDefaultOrEmpty) return;

		//if (!Debugger.IsAttached) Debugger.Launch();

		var sw = Stopwatch.StartNew();
		var resultTypeSchemata =
			Parser.GetEnumTypes(compilation, enumTypesAttributes, context.ReportDiagnostic, context.CancellationToken)
				.ToImmutableArray();

		var generation =
			resultTypeSchemata.Select(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken));

		foreach (var (filename, source) in generation) context.AddSource(filename, source);
		FileLog.LogAccess(sw.Elapsed, $"namespace: {FullNamespace(compilation)}");
	}

	static string FullNamespace(ISymbol compilation)
	{

		static IEnumerable<string> GetParentNamespaces(ISymbol ns)
		{
			if (ns.ContainingNamespace != null)
			{
				foreach (var parentNamespace in GetParentNamespaces(ns.ContainingNamespace))
					yield return parentNamespace;
			}
			yield return ns.Name;
		}

		return ResultType.StringExtension.ToSeparatedString(GetParentNamespaces(compilation)
			.Where(s => !string.IsNullOrEmpty(s)), ".");
	}
}