namespace FunicularSwitch.Generators.Test;

[TestClass]
public class GeneralGeneratorSpecs : VerifySourceGenerator
{
    [TestMethod]
    public Task VerifyPostInitOutput()
        => Verify(
            "",
            useNet11References: false,
            ignorePostInitializationOutput: false);

    [TestMethod]
    public Task VerifyPostInitOutputNet11()
        => Verify(
            "",
            useNet11References: true,
            ignorePostInitializationOutput: false);
}