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

public class OtherAttribute : System.Attribute
{
}

[Other]
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

[UnionType(CaseOrder = CaseOrder.AsDeclared, StaticFactoryMethods = true)]
abstract partial record Base2
{
    //nested case with base type as prefix, that is removed in static factory method
    internal record Base2Prefix : Base2;	

    //nested case with base type as postfix, that is removed in static factory method
    internal record PostfixBase2 : Base2;   

    //nested case with underscore postfix, that is removed in static factory method
    internal record UnderscorePostfix_ : Base2;   

    //nested case with underscore prefix, that is removed in static factory method
    internal record _UnderscorePrefix : Base2;   

    //would result in invalid static factory method name
    internal record Base22Invalid : Base2;    
}


class Consumer {
	static Base CreateOne() => Base.One(42);
	static Base CreateNested() => Base.Nested();
	static Base CreateWithDefault() => Base.WithDefault();

    static Base2 CreatePrefix() => Base2.Prefix();
    static Base2 CreatePostfix() => Base2.Postfix();
    static Base2 CreateUnderscorePostfix() => Base2.UnderscorePostfix();
    static Base2 CreateUnderscorePrefix() => Base2.UnderscorePrefix();
}
";

		return Verify(code);
	}

    [TestMethod]
    public Task Static_factories_for_nested_internal_union_type()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

static class Outer
{
 [UnionType(CaseOrder = CaseOrder.AsDeclared)]
 public partial record InitResult
 {
   public record Sync_ : InitResult;
   public record OneTimeSync_(string TempRepoFolder) : InitResult;
   public record NoSync_() : InitResult;
 }
}";

        return Verify(code);
    }


	[TestMethod]
	public Task Static_factories_for_interface_union_type()
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
    public Task Static_factories_for_type_with_required_properties()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public partial class Base {}

public class One : Base
{
    public required int RequiredField;
    public required string RequiredProperty { get; init; }
} 
public class Two : Base
{
    public Two(int bla)
    {
        Bla = bla;
    }

    public Two(int bla, int strangeNameField, int strangeNameField2) : this(bla)
    {
        this.strangeNameField = strangeNameField;
        this._strangeNameField = strangeNameField2;
    }

    public int Bla { get; }
    public required int strangeNameField;
    public required int _strangeNameField;
    public required bool Bool;
}";

        return Verify(code);
    }

    [TestMethod]
    public Task Support_structs_derived_from_interface()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public partial interface IBase {}

public class One : IBase {}
public struct Two : IBase {}
public readonly partial record struct Three : IBase {}

public class Consumer
{
    public int Handle(IBase @base) => @base.Match(one: _ => 1, two: _ => 2, three: _ => 3);
}
";

        return Verify(code);
    }

    [TestMethod]
    public Task Support_structs_derived_from_derived_interface()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[UnionType]
public partial interface IBaseBase {}

public partial interface IBase : IBaseBase {}

public class One : IBase {}
public record Two : IBase {}
public struct Three : IBase {}

public class Consumer
{
    public int Handle(IBaseBase @base) => @base.Match(one: _ => 1, two: _ => 2, three: _ => 3);
}
";

        return Verify(code);
    }

    public readonly partial record struct X;

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

	[TestMethod]
	public Task For_union_type_with_generic_base_class()
    {
        var code = """
                   using FunicularSwitch.Generators;

                   namespace FunicularSwitch.Test;

                   [UnionType(CaseOrder = CaseOrder.AsDeclared)]
                   public abstract partial record BaseType<T>(string Value)
                   {
                       public sealed record Deriving_(string Value, T Other) : BaseType<T>(Value);
                       
                       public sealed record Deriving2_(string Value) : BaseType<T>(Value);
                   }
                   """;

        return Verify(code);
    }
}