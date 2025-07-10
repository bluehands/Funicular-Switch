using FunicularSwitch.Analyzers.Analyzers;
using FunicularSwitch.Analyzers.CodeFixProviders;

namespace FunicularSwitch.Analyzers.Tests;

public class MatchNullAnalyzerTest : VerifyAnalyzer
{
    public MatchNullAnalyzerTest() : base()
    {
    }

    [Fact]
    public async Task MatchNullOption_IsRecognized_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch;
            
            public class Test()
            {
                public string? T()
                {
                    var option = Option.Some("Hi");
                    return option.Match(some => some.ToLower(), none: () => null);
                }
            }
            """;
        await Verify(
            code,
            new MatchNullAnalyzer(),
            new MatchNullCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0001"));
    }
    
    [Fact]
    public async Task MatchNullOption_WithNamedSome_IsRecognized_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch;
            
            public class Test()
            {
                public string? T()
                {
                    var option = Option.Some("Hi");
                    return option.Match(some: some => some.ToLower(), none: () => null);
                }
            }
            """;
        await Verify(
            code,
            new MatchNullAnalyzer(),
            new MatchNullCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0001"));
    }
    
    [Fact]
    public async Task MatchNullOption_WithDefault_IsRecognized_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch;
            
            public class Test()
            {
                public string? T()
                {
                    var option = Option.Some("Hi");
                    return option.Match(some => some.ToLower(), none: () => default);
                }
            }
            """;
        await Verify(
            code,
            new MatchNullAnalyzer(),
            new MatchNullCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0001"));
    }
    
    [Fact]
    public async Task MatchNullResult_IsRecognized_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch;

            public class Test()
            {
                public string? T()
                {
                    var result = Result.Ok("Hi");
                    return result.Match(some => some.ToLower(), error: e => null);
                }
            }
            """;
        await Verify(
            code,
            new MatchNullAnalyzer(),
            new MatchNullCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0001"));
    }
}