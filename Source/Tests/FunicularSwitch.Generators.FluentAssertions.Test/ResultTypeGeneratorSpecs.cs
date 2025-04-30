using FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.FluentAssertions.Test;

public class ResultTypeGeneratorSpecs : VerifySourceGenerator
{
    public ResultTypeGeneratorSpecs() : base()
    {
    }

    [Fact]
    public async Task Assertions()
    {
        await Verify([typeof(ExampleResult).Assembly, typeof(Result).Assembly]);
    }
}