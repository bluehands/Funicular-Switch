using System.Threading.Tasks;
using FluentAssertions;
using FunicularSwitch.Generators.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_union_type_generator : VerifySourceGenerator
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
	public Task For_switchyard_union_type()
	{
		var code = @"
namespace FunicularSwitch.Test;

[FunicularSwitch.Generators.UnionType(StaticFactoryMethods=false)]
public abstract partial class FieldType
{
    public static FieldType String(int maxLength) => new String_(maxLength);
    public static readonly FieldType Bool = new Bool_();
    public static readonly FieldType Enum = new Enum_();

	public class String_ : FieldType
    {
        public String_(int maxLength) : base(UnionCases.String)
        {
        }
    }

    public class Bool_ : FieldType
    {
        public Bool_() : base(UnionCases.Bool)
        {
        }
    }

    public class Enum_ : FieldType
    {
        public Enum_() : base(UnionCases.Enum)
        {
        }
    }

    internal enum UnionCases
    {
        String,
        Bool,
        Enum
    }

    internal UnionCases UnionCase { get; }
    FieldType(UnionCases unionCase) => UnionCase = unionCase;

    public override string ToString() => System.Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
    bool Equals(FieldType other) => UnionCase == other.UnionCase;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FieldType)obj);
    }

    public override int GetHashCode() => (int)UnionCase;
}";

		return Verify(code);
	}

	[TestMethod]
	public void KeyWorkSpecs()
	{
		"string".IsAnyKeyWord().Should().BeTrue();
		"myparameter".IsAnyKeyWord().Should().BeFalse();
		"event".IsAnyKeyWord().Should().BeTrue();
	}

	[TestMethod]
	public Task For_nested_record_union_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

public class Outer {

	[UnionType]
	public abstract record Base;

	public record One : Base;
	public record Aaa : Base;
	public record Two : Base;
}
";

		return Verify(code);
	}

	[TestMethod]
	public Task For_inaccessible_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

public class Outer {

	[UnionType]
	abstract record Base;

	record One : Base;
	record Aaa : Base;
	record Two : Base;
}
";

		return Verify(code);
	}

	[TestMethod]
	public Task For_empty_namespace()
	{
		var code = @"
using FunicularSwitch.Generators;

public class Outer {

	[UnionType]
	public abstract record Base;

	public record One : Base;	
	public record Two : Base;
}
";

		return Verify(code);
	}

	[TestMethod]
	public Task For_interface_union_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public interface IBase {}

public class One : IBase {}
public record Two : IBase {}";

		return Verify(code);
	}

	[TestMethod]
	public Task For_implicitly_internal_union_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
abstract record Base;

record One : Base;
record Two : Base;";

		return Verify(code);
	}

	[TestMethod]
	public Task For_explicitly_internal_union_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
internal abstract record Base;

internal record One : Base;
record Two : Base;";

		return Verify(code);
	}

	[TestMethod]
	public Task For_partial_record_union_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType(CaseOrder = CaseOrder.AsDeclared, StaticFactoryMethods = true)]
internal abstract partial record Base
{
	public static int Five => 5;
}

internal record One(int Number) : Base;
record Two : Base;

//private constructors are handled correctly
record Three : Base {
	private Three() : base() {}
}

//nested case
public static class Cases {
	internal record Nested : Base;

	//static factory would conflict with static property
	internal record Five : Base;
}

internal record WithDefault(int Number = 42) : Base;

class Consumer {
	static Base CreateOne() => Base.One(42);
	static Base CreateNested() => Base.Nested();
	static Base CreateWithDefault() => Base.WithDefault();
}
";

		return Verify(code);
	}

	[TestMethod]
	public Task No_static_factories_for_interface_union_type()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public partial interface IBase {}

public class One : IBase {}
public record Two : IBase {}";

		return Verify(code);
	}

	[TestMethod]
	public Task For_union_type_without_derived_types()
	{
		var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType(CaseOrder = CaseOrder.Explicit)]
public abstract partial record NodeMessage(string NodeInstanceId)
{
	public string Node { get; } = NodeInstanceId.Substring(0, NodeInstanceId.IndexOf(':'));
}";

		return Verify(code);
	}
}