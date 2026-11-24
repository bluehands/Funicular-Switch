using FunicularSwitch.Analyzers.CodeFixProviders;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Analyzers.Tests;

public class ResultTypeClosedAnalyzerTest : VerifyAnalyzer
{
    public ResultTypeClosedAnalyzerTest() : base() { }

    private const string Prelude =
        """
        using FunicularSwitch.Generators;
        namespace FunicularSwitch.Generators
        {
            [System.AttributeUsage(System.AttributeTargets.Class)]
            public sealed class ResultTypeAttribute : System.Attribute { public ResultTypeAttribute(System.Type errorType) { } }
        }
        public class MyError;

        """;

    private Task Check(string code, string[] ids, LanguageVersion lv = LanguageVersion.Preview, bool netStandard = false, [System.Runtime.CompilerServices.CallerMemberName] string caller = "") =>
        Verify(Prelude + code, new ResultTypeClosedAnalyzer(), new MakeClosedCodeFixProvider(),
            d => d.Select(x => x.Id).Should().BeEquivalentTo(ids), languageVersion: lv, netStandard: netStandard, callingMethod: caller);

    [Fact]
    public Task AbstractPartialResultType_SuggestsClosed_AllPartialsFixed() =>
        Check(
            """
            [ResultType(typeof(MyError))]
            public abstract partial class MyResult<T>;
            public abstract partial class MyResult<T> { }
            """,
            ["FS0007"]);

    [Fact]
    public Task AlreadyClosed_NoSuggestion() =>
        Check("[ResultType(typeof(MyError))] public closed partial class MyResult<T>;", []);

    [Fact]
    public Task LanguageVersion14_NoSuggestion() =>
        Check("[ResultType(typeof(MyError))] public abstract partial class MyResult<T>;", [], LanguageVersion.CSharp14);

    [Fact]
    public Task ClosedSupportTypesMissing_NoSuggestion() =>
        Check("[ResultType(typeof(MyError))] public abstract partial class MyResult<T>;", [], LanguageVersion.Preview, netStandard: true);

    [Fact]
    public Task AbstractClassWithoutResultType_NoSuggestion() =>
        Check("public abstract partial class Other<T>;", []);
}
