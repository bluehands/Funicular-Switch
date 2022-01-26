using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_match_method_generator: VerifySourceGenerator
{
    [TestMethod]
    public Task For_record_union_type()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public abstract record Base;

public record One : Base;
public record Aaa : Base;
public record Two : Base;";

        return Verify(code);
    }
}