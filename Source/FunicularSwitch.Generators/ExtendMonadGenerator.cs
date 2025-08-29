using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Monad;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

[Generator]
public class ExtendMonadGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ExtendMonadAttribute.AddTo);

        var transformedMonadTypes = ExtendMonadAttribute.Find(
            context.SyntaxProvider,
            static (_, _) => true,
            Parse);

        context.RegisterSourceOutput(
            transformedMonadTypes,
            Execute);
    }

    private static void Execute(SourceProductionContext context, GenerationResult<ExtendMonadInfo> target)
    {
        var (data, errors, hasValue) = target;
        foreach (var error in errors) context.ReportDiagnostic(error);

        if (!hasValue) return;

        var (filename, source) = Generator.Emit(data!, context.ReportDiagnostic, context.CancellationToken);
        context.AddSource(filename, source);
    }

    private static GenerationResult<ExtendMonadInfo> Parse(GeneratorAttributeSyntaxContext syntaxContext, IReadOnlyList<ExtendMonadAttribute> attributes, CancellationToken token) =>
        Parser.GetExtendedMonadSchema(
            (INamedTypeSymbol) syntaxContext.TargetSymbol,
            attributes.First(),
            token);
}
