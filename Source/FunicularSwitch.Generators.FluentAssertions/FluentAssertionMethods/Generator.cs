using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

internal static class Generator
{
    private const string TemplateNamespace = "FunicularSwitch.Generators.FluentAssertions.Templates";
    private const string TemplateResultTypeName = "MyResult";
    private const string TemplateErrorTypeName = "MyErrorType";
    private const string TemplateDerivedErrorTypeName = "MyDerivedErrorType";
    private const string TemplateFriendlyDerivedErrorTypeName = "FriendlyDerivedErrorTypeName";
    private const string TemplateAdditionalUsingDirectives = "//additional using directives";

    public static IEnumerable<(string filename, string source)> Emit(
        ResultTypeSchema resultTypeSchema,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var resultTypeName = resultTypeSchema.ResultType.Name;
        var resultTypeNamespace = resultTypeSchema.ResultType.GetFullNamespace();

        var errorTypeName = resultTypeSchema.ErrorType?.Name ?? nameof(System.String);
        var errorTypeNamespace = resultTypeSchema.ErrorType?.GetFullNamespace() ?? nameof(System);

        var generateFileHint = $"{resultTypeNamespace}.{resultTypeName}";

        string Replace(string code, IReadOnlyCollection<string>? additionalNamespaces = null)
        {
            code = code
                .Replace($"namespace {TemplateNamespace}", $"namespace {resultTypeNamespace}")
                .Replace(TemplateResultTypeName, resultTypeName)
                .Replace(TemplateErrorTypeName, errorTypeName);

            additionalNamespaces ??= Array.Empty<string>();
            var includingErrorNamespace = errorTypeNamespace != resultTypeNamespace
                ? additionalNamespaces.Append(errorTypeNamespace)
                : additionalNamespaces;
            if (additionalNamespaces.Count > 0)
                code = code
                    .Replace(TemplateAdditionalUsingDirectives, includingErrorNamespace.Distinct().Select(a => $"using {a};")
                            .ToSeparatedString(Environment.NewLine));

            return code;
        }

        yield return (
            $"{generateFileHint}Assertions.g.cs",
            Replace(Templates.GenerateFluentAssertionsForTemplates.MyResultAssertions));

        yield return (
            $"{generateFileHint}FluentAssertionExtensions.g.cs",
            Replace(Templates.GenerateFluentAssertionsForTemplates.MyResultFluentAssertionExtensions));

        foreach (var derivedErrorType in resultTypeSchema.ErrorTypeDerivedTypes)
        {
            yield return (
                $"{generateFileHint}.{errorTypeName}.{derivedErrorType.Name}Assertions.g.cs",
                Replace(Templates.GenerateFluentAssertionsForTemplates.MyResultAssertions_DerivedErrorType)
                    .Replace(TemplateDerivedErrorTypeName, derivedErrorType.Name)
                    .Replace(TemplateFriendlyDerivedErrorTypeName, derivedErrorType.Name.Trim('_')));
        }
    }
}