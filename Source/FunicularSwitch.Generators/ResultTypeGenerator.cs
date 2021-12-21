﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators;

[Generator]
public class ResultTypeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "ResultTypeAttribute.g.cs",
            @"using System;
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; }
    }
}"));

        var resultTypeClasses =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)

                )
                .Where(static target => target != null)
                .Select(static (target, _) => target!);

        var compilationAndClasses = context.CompilationProvider.Combine(resultTypeClasses.Collect());

        context.RegisterSourceOutput(
            compilationAndClasses, 
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> resultTypeClasses, SourceProductionContext context)
    {
        if (resultTypeClasses.IsDefaultOrEmpty) return;

        var resultTypeSchemata = Parser.GetResultTypes(compilation, resultTypeClasses, context.ReportDiagnostic, context.CancellationToken).ToImmutableArray();

        var generated = resultTypeSchemata.SelectMany(r => Generator.Emit(r, context.ReportDiagnostic, context.CancellationToken)).ToImmutableArray();

        foreach (var (filename, source) in generated) context.AddSource(filename, source);
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
        syntaxNode is ClassDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };

    const string ResultTypeAttribute = "FunicularSwitch.Generators.ResultTypeAttribute";

    static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;
                if (symbol is not IMethodSymbol attributeSymbol)
                    continue;

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                var attributeFullName = attributeContainingTypeSymbol.ToDisplayString();

                if (attributeFullName == ResultTypeAttribute)
                {
                    return classDeclarationSyntax;
                }
            }
        }

        return null;
    }
}