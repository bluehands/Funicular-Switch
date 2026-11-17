using FluentAssertions;

[TestClass]
public class RuntimeResultClosedSpecs
{
    [TestMethod]
    public void NativeExhaustiveSwitchOverRuntimeResult()
    {
        static string Describe(global::FunicularSwitch.Result<int> r) => r switch
        {
            global::FunicularSwitch.Result<int>.Ok_ ok => $"ok {ok.Value}",
            global::FunicularSwitch.Result<int>.Error_ e => $"error {e.Details}",
        };

        Describe(global::FunicularSwitch.Result.Ok(1)).Should().Be("ok 1");
        Describe(global::FunicularSwitch.Result.Error<int>("x")).Should().Be("error x");
    }
}
