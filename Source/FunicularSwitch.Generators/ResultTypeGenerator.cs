using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.ResultType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class ResultTypeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource(
                "Attributes.g.cs",
                Templates.ResultTypeTemplates.StaticCode);
        });

        var resultTypeClasses =
            context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    ResultTypeAttribute,
                    predicate: static (s, _) => true,
                    transform: static (ctx, cancellationToken) =>
                    {
                        //TODO: support record result types one day
                        if (ctx.TargetSymbol is not INamedTypeSymbol n || n.IsRecord)
                            return GenerationResult<ResultTypeSchema>.Empty;

                        var resultClass = (ClassDeclarationSyntax)ctx.TargetNode;
                        var errorTypeSymbol = (INamedTypeSymbol?)(!ctx.Attributes[0].NamedArguments.IsEmpty
                            ? ctx.Attributes[0].NamedArguments[0].Value.Value!
                            : !ctx.Attributes[0].ConstructorArguments.IsEmpty
                                ? ctx.Attributes[0].ConstructorArguments[0].Value
                                : null);

                        return new ResultTypeSchema(resultClass, errorTypeSymbol);
                    });

        var referencesJetBrainsAnnotationsAssembly = context.CompilationProvider
            .SelectMany((c, _) => c.SourceModule.ReferencedAssemblySymbols)
            .Where(a => a.Name == "JetBrains.Annotations")
            .Collect()
            .Select((a, _) => a.Length > 0);

        var compilationAndClasses = context.CompilationProvider
            .Select((compilation, _) =>
            {
                List<Diagnostic> diagnostics = new();
                var mergeMethods = Parser.FindMergeMethods(compilation, diagnostics.Add);
                var exceptionToErrorMethods = Parser.FindExceptionToErrorMethods(compilation, diagnostics.Add);
                var referencesFunicularSwitchGeneric = Parser.ReferencesFunicularSwitchGeneric(compilation);


                return GenerationResult.Create(
                    (
                        mergeMethods: mergeMethods.Values.ToImmutableArray().AsEquatableArray(),
                        exceptionToErrorMethods: exceptionToErrorMethods.Values.ToImmutableArray().AsEquatableArray(),
                        stringSymbol: SymbolWrapper.Create(compilation.GetTypeByMetadataName("System.String")!),
                        referencesFunicularSwitchGeneric
                    ),
                    diagnostics.Select(d => new DiagnosticInfo(d)).ToImmutableArray(), true
                );
            })
            .Combine(resultTypeClasses.Collect())
            .Combine(referencesJetBrainsAnnotationsAssembly);

        context.RegisterSourceOutput(
            compilationAndClasses,
            static (spc, source) => Execute(source.Left.Left, source.Left.Right, source.Right, spc));
    }

    static void Execute(
        GenerationResult<(
            EquatableArray<MergeMethod> mergeMethods,
            EquatableArray<ExceptionToErrorMethod> exceptionToErrorMethods,
            SymbolWrapper<INamedTypeSymbol> stringSymbol,
            bool referencesFunicularSwitchGeneric
            )> resultTypeMethods,
        ImmutableArray<GenerationResult<ResultTypeSchema>> resultTypeClassesResult,
        bool hasJetBrainsAnnotationsReference,
        SourceProductionContext context)
    {
        foreach (var diagnosticInfo in resultTypeClassesResult
                     .SelectMany(r => r.Diagnostics)
                     .Concat(resultTypeMethods.Diagnostics))
            context.ReportDiagnostic(diagnosticInfo);

        ImmutableArray<ResultTypeSchema> resultTypeSchemata = resultTypeClassesResult
            .Select(r => r.Value)
            .Where(r => r != null)
            .ToImmutableArray()!;

        if (resultTypeSchemata.IsDefaultOrEmpty) return;

        var generated = resultTypeSchemata
            .SelectMany(r =>
            {
                var defaultErrorType = resultTypeMethods.Value.stringSymbol;
                var errorTypeSymbol = r.ErrorType ?? defaultErrorType;

                return Generator.Emit(
                    resultTypeSchema: r,
                    defaultErrorType: defaultErrorType,
                    mergeErrorMethod: resultTypeMethods.Value.mergeMethods.FirstOrDefault(m =>
                        m.FullErrorTypeName == errorTypeSymbol.FullNameWithNamespace),
                    exceptionToErrorMethod: resultTypeMethods.Value.exceptionToErrorMethods.FirstOrDefault(e =>
                        e.ErrorTypeName == errorTypeSymbol.FullNameWithNamespace),
                    reportDiagnostic: context.ReportDiagnostic, 
                    referencesFunicularSwitchGeneric: resultTypeMethods.Value.referencesFunicularSwitchGeneric,
                    referencesJetBrainsAnnotations: hasJetBrainsAnnotationsReference,
                    cancellationToken: context.CancellationToken);
            }).ToImmutableArray();

        foreach (var (filename, source) in generated) context.AddSource(filename, source);
    }

    internal const string ResultTypeAttribute = "FunicularSwitch.Generators.ResultTypeAttribute";
}