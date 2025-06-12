using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.AwesomeAssertions.AssertionMethods;

internal static class Generator
{
    public const string TemplateNamespace = "FunicularSwitch.Generators.AwesomeAssertions.Templates";

    public static IEnumerable<(string filename, string source)> EmitForResultType(
        ResultTypeSchema resultTypeSchema,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var resultTypeNameFullName = resultTypeSchema.ResultType.FullTypeName().Replace('.', '_');
        var resultTypeFullNameWithNamespace = resultTypeSchema.ResultType.FullTypeNameWithNamespace();
        var format =
            SymbolWrapper.FullTypeWithNamespaceAndGenericsDisplayFormat.WithGenericsOptions(SymbolDisplayGenericsOptions
                .None);
        var resultTypeFullNameWithGlobalNamespace = resultTypeSchema.ResultType.ToDisplayString(format);
        var resultTypeNamespace = resultTypeSchema.ResultType.GetFullNamespace()!;

        var errorTypeNameFullName = resultTypeSchema.ErrorType?.FullTypeNameWithNamespace() ?? typeof(string).FullName;

        var generateFileHint = $"{resultTypeFullNameWithNamespace}";

        var assertionsTypeName = $"{resultTypeNameFullName}Assertions";
        var assertionsCode =
            $$"""
              #nullable enable
              using AwesomeAssertions.Execution;
              using AwesomeAssertions.Primitives;
              using AwesomeAssertions;
              using {{resultTypeNamespace}};
              
              namespace {{resultTypeNamespace}}
              {
                  internal partial class {{assertionsTypeName}}<T> : ObjectAssertions<{{resultTypeFullNameWithGlobalNamespace}}<T>, {{assertionsTypeName}}<T>>
                  {
                      public {{assertionsTypeName}}({{resultTypeFullNameWithGlobalNamespace}}<T> value) : base(value, AssertionChain.GetOrCreate())
                      {
                      }
                      
                      public AndWhichConstraint<{{assertionsTypeName}}<T>, T> BeOk(string because = "", params object[] becauseArgs)
                      {
                          CurrentAssertionChain
                              .ForCondition(this.Subject.IsOk)
                              .BecauseOf(because, becauseArgs)
                              .FailWith("Expected {context} to be Ok{reason}, but found {0}", this.Subject.ToString());
                              
                          return new(this, this.Subject.GetValueOrThrow());
                      }
                      
                      public AndWhichConstraint<{{assertionsTypeName}}<T>, {{errorTypeNameFullName}}> BeError(string because = "", params object[] becauseArgs)
                      {
                          CurrentAssertionChain
                              .ForCondition(this.Subject.IsError)
                              .BecauseOf(because, becauseArgs)
                              .FailWith("Expected {context} to be Error{reason}, but found {0}", this.Subject.ToString());
                              
                          return new(this, this.Subject.GetErrorOrDefault()!);
                      }
                  }
              }
              """;

        var extensionCode =
            $$"""
              #nullable enable
              using {{resultTypeNamespace}};

              namespace {{resultTypeNamespace}}
              {
                  internal static class {{resultTypeNameFullName}}AwesomeAssertionExtensions
                  {
                      public static {{assertionsTypeName}}<T> Should<T>(this {{resultTypeFullNameWithGlobalNamespace}}<T> result) => new(result);
                  }
              }
              """;

        yield return (
            $"{generateFileHint}Assertions.g.cs",
            assertionsCode);

        yield return (
            $"{generateFileHint}AwesomeAssertionExtensions.g.cs",
            extensionCode);
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
        EquatableArray<string> typeConstraints = unionTypeSchema.UnionTypeBaseType.TypeParameters
            .Select(t => t.FormatTypeConstraints()).ToImmutableArray();
        var unionTypeNamespace = unionTypeSchema.UnionTypeBaseType.GetFullNamespace();
        var typeParametersText = RoslynExtensions.FormatTypeParameters(typeParameters);

        var generateFileHint = $"{unionTypeFullNameWithNamespace}{RoslynExtensions.FormatTypeParameterForFileName(typeParameters)}";

        var typeConstraintsText = string.Join("", typeConstraints);
        var assertionsCode =
            $$"""
            #nullable enable
            using AwesomeAssertions.Primitives;
            using AwesomeAssertions.Execution;
            using {{unionTypeNamespace}};
            
            namespace {{unionTypeNamespace}}
            {
                internal partial class {{unionTypeFullName}}Assertions{{typeParametersText}} : ObjectAssertions<{{unionTypeFullNameWithNamespaceAndGenerics}}, {{unionTypeFullName}}Assertions{{typeParametersText}}>{{typeConstraintsText}}
                {
                    public {{unionTypeFullName}}Assertions({{unionTypeFullNameWithNamespaceAndGenerics}} value) : base(value, AssertionChain.GetOrCreate())
                    {
                    
                    }
                }
            }
            """;
        
        var extensionsCode =
            $$"""
            #nullable enable
            using {{unionTypeNamespace}};
            
            namespace {{unionTypeNamespace}}
            {
                internal static class {{unionTypeFullName}}AwesomeAssertionExtensions
                {
                    public static {{unionTypeFullName}}Assertions{{typeParametersText}} Should{{typeParametersText}}(this {{unionTypeFullNameWithNamespaceAndGenerics}} unionType){{typeConstraintsText}} => new(unionType);
                }
            }
            """;

        yield return (
            $"{generateFileHint}Assertions.g.cs",
            assertionsCode);

        yield return (
            $"{generateFileHint}AwesomeAssertionExtensions.g.cs",
            extensionsCode);

        foreach (var derivedType in unionTypeSchema.DerivedTypes)
        {
            var derivedTypeFullName = derivedType.FullTypeName();
            var derivedTypeFullNameWithGlobalNamespace = derivedType.FullTypeNameWithNamespaceAndGenerics();
            var friendlyDerivedUnionTypeName = derivedType.Name.Trim('_');
            var derivedAssertionCode =
                $$"""
                #nullable enable
                using AwesomeAssertions;
                using AwesomeAssertions.Execution;
                using {{unionTypeNamespace}};
                
                namespace {{unionTypeNamespace}}
                {
                    internal partial class {{unionTypeFullName}}Assertions{{typeParametersText}}
                    {
                        public AndWhichConstraint<{{unionTypeFullName}}Assertions{{typeParametersText}}, {{derivedTypeFullNameWithGlobalNamespace}}> Be{{friendlyDerivedUnionTypeName}}(
                            string because = "",
                            params object[] becauseArgs)
                        {
                            CurrentAssertionChain
                                .ForCondition(this.Subject is {{derivedTypeFullNameWithGlobalNamespace}})
                                .BecauseOf(because, becauseArgs)
                                .FailWith("Expected {context} to be {{derivedTypeFullName}}{reason}, but found {0}",
                                    this.Subject.ToString());
                
                            return new(this, (this.Subject as {{derivedTypeFullNameWithGlobalNamespace}})!);
                        }
                    }
                }
                """;
            yield return (
                $"{generateFileHint}_Derived_{derivedType.Name}Assertions.g.cs",
                derivedAssertionCode);
        }
    }
}