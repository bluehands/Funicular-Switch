using System.Threading.Tasks;
using FluentAssertions;
using FunicularSwitch.Generators.Common;
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
	public Task For_switchyard_union_type()
	{
		var code = @"
namespace FunicularSwitch.Test;

[FunicularSwitch.Generators.UnionType]
public abstract class FieldType
{
    public static readonly FieldType String = new String_();
    public static readonly FieldType Bool = new Bool_();
    public static readonly FieldType Enum = new Enum_();

    public class String_ : FieldType
    {
        public String_() : base(UnionCases.String)
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
}
