using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.FluentAssertions.FluentAssertionMethods;

internal static class Generator
{
    public const string TemplateNamespace = "FunicularSwitch.Generators.FluentAssertions.Templates";
    private const string TemplateResultTypeName = "MyResult";
    private const string TemplateErrorTypeName = "MyErrorType";
    private const string TemplateUnionTypeName = "MyUnionType";
    private const string TemplateDerivedUnionTypeName = "MyDerivedUnionType";
    private const string TemplateResultAssertionsTypeName = "MyAssertions_Result";
    private const string TemplateResultAssertionExtensions = "MyAssertionExtensions_Result";
    private const string TemplateUnionTypeAssertionsTypeName = "MyAssertions_UnionType";
    private const string TemplateUnionTypeAssertionExtensions = "MyAssertionExtensions_UnionType";
    private const string TemplateUnionTypeTypeParameterList = "<TTypeParameters>";
    private const string TemplateFriendlyDerivedUnionTypeName = "FriendlyDerivedUnionTypeName";
    private const string TemplateAdditionalUsingDirectives = "//additional using directives";

    public static IEnumerable<(string filename, string source)> EmitForResultType(
        ResultTypeSchema resultTypeSchema,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var resultTypeNameFullName = resultTypeSchema.ResultType.FullTypeName().Replace('.', '_');
        var resultTypeFullNameWithNamespace = resultTypeSchema.ResultType.FullTypeNameWithNamespace();
        var resultTypeNamespace = resultTypeSchema.ResultType.GetFullNamespace()!;

        var errorTypeNameFullName = resultTypeSchema.ErrorType?.FullTypeNameWithNamespace() ?? typeof(string).FullName;

        var generateFileHint = $"{resultTypeFullNameWithNamespace}";

        string Replace(string code, params string[] additionalNamespaces)
        {
            code = code
                .Replace($"namespace {TemplateNamespace}", $"namespace {resultTypeNamespace}")
                .Replace(TemplateResultTypeName, resultTypeFullNameWithNamespace)
                .Replace(TemplateErrorTypeName, errorTypeNameFullName)
                .Replace(TemplateResultAssertionsTypeName, $"{resultTypeNameFullName}Assertions")
                .Replace(TemplateResultAssertionExtensions, $"{resultTypeNameFullName}FluentAssertionExtensions")
                .Replace(
                    TemplateAdditionalUsingDirectives,
                        additionalNamespaces
                            .Append(resultTypeNamespace)
                            .Distinct()
                            .Select(a => $"using {a};")
                            .ToSeparatedString("\n"));
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
        var unionTypeFullName = unionTypeSchema.UnionTypeBaseType.FullTypeName().Replace('.', '_');
        var unionTypeFullNameWithNamespace = unionTypeSchema.UnionTypeBaseType.FullTypeNameWithNamespace();
        var unionTypeFullNameWithNamespaceAndGenerics = unionTypeSchema.UnionTypeBaseType.FullTypeNameWithNamespaceAndGenerics();
        EquatableArray<string> typeParameters = unionTypeSchema.UnionTypeBaseType.TypeParameters
            .Select(t => t.Name).ToImmutableArray();
        var unionTypeNamespace = unionTypeSchema.UnionTypeBaseType.GetFullNamespace();
        var typeParametersText = RoslynExtensions.FormatTypeParameters(typeParameters);

        var generateFileHint = $"{unionTypeFullNameWithNamespace}{RoslynExtensions.FormatTypeParameterForFileName(typeParameters)}";

        //var generatorRuns = RunCount.Increase(unionTypeSchema.UnionTypeBaseType.FullTypeNameWithNamespace());
        
        string Replace(string code, params string[] additionalNamespaces)
        {
            code = code
                .Replace($"namespace {TemplateNamespace}", $"namespace {unionTypeNamespace}")
                .Replace(TemplateUnionTypeName, unionTypeFullNameWithNamespaceAndGenerics)
                .Replace($"public {TemplateUnionTypeAssertionsTypeName}(", $"public {unionTypeFullName}Assertions(")
                .Replace(TemplateUnionTypeAssertionsTypeName, $"{unionTypeFullName}Assertions{typeParametersText}")
                .Replace(TemplateUnionTypeAssertionExtensions, $"{unionTypeFullName}FluentAssertionExtensions")
                .Replace(TemplateUnionTypeTypeParameterList, typeParametersText)
                .Replace(
                    TemplateAdditionalUsingDirectives,
                    additionalNamespaces
                        .Append(unionTypeNamespace)
                        .Distinct()
                        .Select(a => $"using {a};")
                        .ToSeparatedString("\n"));
            //code = $"//Generator runs: {generatorRuns}\r\n" + code;
            
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
            var derivedTypeFullNameWithNamespace = derivedType.FullTypeNameWithNamespaceAndGenerics();
            yield return (
                $"{generateFileHint}_Derived_{derivedType.Name}Assertions.g.cs",
                Replace(Templates.GenerateFluentAssertionsForTemplates.MyDerivedUnionTypeAssertions)
                    .Replace(TemplateDerivedUnionTypeName, derivedTypeFullNameWithNamespace)
                    .Replace(TemplateFriendlyDerivedUnionTypeName, derivedType.Name.Trim('_')));
        }
    }
}