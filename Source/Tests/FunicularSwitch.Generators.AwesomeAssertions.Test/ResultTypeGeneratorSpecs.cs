using FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

namespace FunicularSwitch.Generators.AwesomeAssertions.Test;

public class ResultTypeGeneratorSpecs : VerifySourceGenerator
{
    public ResultTypeGeneratorSpecs() : base()
    {
    }

    [Fact]
    public async Task Assertions()
    {
        await Verify([typeof(ExampleResult).Assembly], subfolder: "GeneratedTypes");
        await Verify([typeof(Result).Assembly], subfolder: "FunicularSwitchTypes");
    }
}