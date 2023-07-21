using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

internal static class Generator
{
    private const string TemplateNamespace = "FunicularSwitch.Generators.FluentAssertions.Templates";
    private const string TemplateResultTypeName = "MyResult";
    private const string TemplateErrorTypeName = "MyErrorType";
    private const string TemplateUnionTypeName = "MyUnionType";
    private const string TemplateDerivedUnionTypeName = "MyDerivedUnionType";
    private const string TemplateFriendlyDerivedUnionTypeName = "FriendlyDerivedUnionTypeName";
    private const string TemplateAdditionalUsingDirectives = "//additional using directives";

    public static IEnumerable<(string filename, string source)> EmitForResultType(
        ResultTypeSchema resultTypeSchema,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var resultTypeName = resultTypeSchema.ResultType.Name;
        var resultTypeNamespace = resultTypeSchema.ResultType.GetFullNamespace();

        var errorTypeNameFullName = resultTypeSchema.ErrorType?.FullTypeNameWithNamespace() ?? typeof(string).FullName;

        var generateFileHint = $"{resultTypeNamespace}.{resultTypeName}";

        string Replace(string code, params string[] additionalNamespaces)
        {
            code = code
                .Replace($"namespace {TemplateNamespace}", $"namespace {resultTypeNamespace}")
                .Replace(TemplateResultTypeName, resultTypeName)
                .Replace(TemplateErrorTypeName, errorTypeNameFullName)
                .Replace(
                    TemplateAdditionalUsingDirectives,
                        additionalNamespaces
                            .Append(resultTypeNamespace)
                            .Distinct()
                            .Select(a => $"using {a};")
                            .ToSeparatedString(Environment.NewLine));
            return code;
        }

        yield return (
            $"{generateFileHint}Assertions.g.cs",
            Replace(Templates.GenerateFluentAssertionsForTemplates.MyResultAssertions, resultTypeNamespace));

        yield return (
            $"{generateFileHint}FluentAssertionExtensions.g.cs",
            Replace(Templates.GenerateFluentAssertionsForTemplates.MyResultFluentAssertionExtensions));
    }

    public static IEnumerable<(string filename, string source)> EmitForUnionType(
        UnionTypeSchema unionTypeSchema,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var unionTypeName = unionTypeSchema.UnionTypeBaseType.Name;
        var unionTypeNamespace = unionTypeSchema.UnionTypeBaseType.GetFullNamespace();

        var generateFileHint = $"{unionTypeNamespace}.{unionTypeName}";

        string Replace(string code, params string[] additionalNamespaces)
        {
            code = code
                .Replace($"namespace {TemplateNamespace}", $"namespace {unionTypeNamespace}")
                .Replace(TemplateUnionTypeName, unionTypeName)
                .Replace(
                    TemplateAdditionalUsingDirectives,
                    additionalNamespaces
                        .Append(unionTypeNamespace)
                        .Distinct()
                        .Select(a => $"using {a};")
                        .ToSeparatedString(Environment.NewLine));
            return code;
        }

        yield return (
            $"{generateFileHint}Assertions.g.cs",
            Replace(Templates.GenerateFluentAssertionsForTemplates.MyUnionTypeAssertions));

        yield return (
            $"{generateFileHint}FluentAssertionExtensions.g.cs",
            Replace(Templates.GenerateFluentAssertionsForTemplates.MyUnionTypeFluentAssertionExtensions));

        foreach (var derivedType in unionTypeSchema.DerivedTypes)
        {
            yield return (
                $"{generateFileHint}.{derivedType.Name}Assertions.g.cs",
                Replace(Templates.GenerateFluentAssertionsForTemplates.MyDerivedUnionTypeAssertions)
                    .Replace(TemplateDerivedUnionTypeName, derivedType.FullTypeNameWithNamespace())
                    .Replace(TemplateFriendlyDerivedUnionTypeName, derivedType.Name.Trim('_')));
        }
    }
}