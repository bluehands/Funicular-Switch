using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_match_method_generator : VerifySourceGenerator
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

    [TestMethod]
    public Task For_record_union_type_with_multi_level_concrete_derived_types()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public abstract record Base;

public record BaseChild : Base;
public record Bbb : BaseChild;
public record Aaa : Base;";

        return Verify(code);
    }

    [TestMethod]
    public Task For_record_union_type_with_explicit_case_order()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType(CaseOrder = CaseOrder.Explicit)]
public abstract record Base;

[UnionCase(2)]
public record Eins : Base;
[UnionCase(1)]
public record Zwei : Base;
";

        return Verify(code);
    }

    [TestMethod]
    public Task For_record_union_type_with_as_declared_case_order()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract record Base;

public record One : Base;
public record Aaa : Base;
public record Two : Base;";

        return Verify(code);
    }

    [TestMethod]
    public Task For_record_union_type_with_predefined_case_names()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract record Field;

public record String : Field;
public record Enum : Field;
public record Event : Field;";

        return Verify(code);
    }

    [TestMethod]
    public void KeyWorkSpecs()
    {
        "string".IsAnyKeyWord().Should().BeTrue();
        "myparameter".IsAnyKeyWord().Should().BeFalse();
        "event".IsAnyKeyWord().Should().BeTrue();
    }

}