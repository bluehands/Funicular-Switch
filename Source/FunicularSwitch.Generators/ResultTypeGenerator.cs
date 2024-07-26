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
                .CreateSyntaxProvider(
                    predicate: static (s, _) => s.IsTypeDeclarationWithAttributes(),
                    transform: static (ctx, cancellationToken) =>
                    {
                        //TODO: support record result types one day
                        if (GeneratorHelper.GetSemanticTargetForGeneration(ctx, ResultTypeAttribute) is not ClassDeclarationSyntax resultTypeClass)
                            return GenerationResult<ResultTypeSchema>.Empty;

                        var schema = Parser.GetResultTypeSchema(resultTypeClass
                            , ctx.SemanticModel.Compilation, cancellationToken);
                        return schema;
                    });

        var compilationAndClasses = context.CompilationProvider
            .Select((compilation, _) => 
            {
                List<Diagnostic> diagnostics = new();
                var mergeMethods = Parser.FindMergeMethods(compilation, diagnostics.Add);
                var exceptionToErrorMethods = Parser.FindExceptionToErrorMethods(compilation, diagnostics.Add);

                return GenerationResult.Create(
                    (
                        mergeMetdhods: mergeMethods.Values.ToImmutableArray().AsEquatableArray(),
                        exceptionToErrorMethods: exceptionToErrorMethods.Values.ToImmutableArray().AsEquatableArray()
                    ),
                    diagnostics.Select(d => new DiagnosticInfo(d)).ToImmutableArray(), true
                );
            })
            .Combine(resultTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses,
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(
        GenerationResult<(EquatableArray<MergeMethod> mergeMethods, EquatableArray<ExceptionToErrorMethod> exceptionToErrorMethods)> resultTypeMethods, 
        ImmutableArray<GenerationResult<ResultTypeSchema>> resultTypeClassesResult, SourceProductionContext context)
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
            .SelectMany(r => Generator.Emit(r, 
                resultTypeMethods.Value.mergeMethods.FirstOrDefault(m => m.FullErrorTypeName == r.ErrorType.FullNameWithNamespace),
                resultTypeMethods.Value.exceptionToErrorMethods.FirstOrDefault(e => e.ErrorTypeName == r.ErrorType.FullNameWithNamespace),
                context.ReportDiagnostic, context.CancellationToken)).ToImmutableArray();

        foreach (var (filename, source) in generated) context.AddSource(filename, source);
    }

    internal const string ResultTypeAttribute = "FunicularSwitch.Generators.ResultTypeAttribute";
}
