using FluentAssertions;
using FunicularSwitch.Generators.Analyzers;
using FunicularSwitch.Generators.CodeFixProviders;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class MatchNullAnalyzerTest : VerifyAnalyzer
{
    [TestMethod]
    public async Task MatchNullOption_IsRecognized_FixIsApplied()
    {
        var code =
            """

            """;
        await Verify(
            code,
            new MatchNullAnalyzer(),
            new MatchNullCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0001"));
    }
}