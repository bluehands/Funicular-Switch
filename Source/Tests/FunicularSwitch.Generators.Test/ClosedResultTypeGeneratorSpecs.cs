namespace FunicularSwitch.Generators.Test;

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
}
