namespace FunicularSwitch.Generators.Test;

using FluentAssertions;

[TestClass]
public class Run_closed_result_type_generator : VerifySourceGenerator
{
    [TestMethod]
    public Task For_result_type_declared_closed_by_user_generates_closed_base()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [ResultType(errorType: typeof(string))]
            public closed partial class Result<T>
            {
            }

            [ResultType(ErrorType = typeof(MyError))]
            internal closed partial class OperationResult<T>
            {
            }

            public enum MyError
            {
                Generic,
                NotFound
            }

            public static class MyErrorExtension
            {
                [MergeError]
                public static MyError MergeErrors(this MyError error, MyError other) => other;
            }
            """;
        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_closed_result_type_no_unexpected_default_remains_in_generated_code()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [ResultType(errorType: typeof(string))]
            public closed partial class Result<T>
            {
            }
            """;

        return Verify(code, [], (compilation, _) =>
        {
            var generated = string.Join("\n", compilation.SyntaxTrees.Skip(1).Select(t => t.ToString()));
            generated.Should().NotBeNullOrEmpty();
            generated.Should().NotContain("_ => throw", "closed result types have no throwing default switch expression arms");
            generated.Should().NotContain("Unexpected type derived from");
            generated.Should().Contain("default:", "switch statements keep their guard, flow analysis requires it");
            compilation.GetDiagnostics().Where(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error || d.Id is "CS8509" or "CS8524")
                .Should().BeEmpty();
        }, Microsoft.CodeAnalysis.CSharp.LanguageVersion.CSharp15, useNet11References: true);
    }
}
