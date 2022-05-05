using System;
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
}

public static partial class MatchExtension
{
	public abstract class Test
	{
		public static readonly Test Eins = new Eins_();
		public static readonly Test Zwei = new Zwei_();

		public class Eins_ : Test
		{
			public Eins_() : base(UnionCases.Eins)
			{
			}
		}

		public class Zwei_ : Test
		{
			public Zwei_() : base(UnionCases.Zwei)
			{
			}
		}

		internal enum UnionCases
		{
			Eins,
			Zwei
		}

		internal UnionCases UnionCase { get; }
		Test(UnionCases unionCase) => UnionCase = unionCase;

		public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
		bool Equals(Test other) => UnionCase == other.UnionCase;

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Test)obj);
		}

		public override int GetHashCode() => (int)UnionCase;
	}
}
public static class MatchExtensionTestExtension
{
	public static void Switch(this MatchExtension.Test test, Action<MatchExtension.Test.Eins_> eins, Action<MatchExtension.Test.Zwei_> zwei)
	{
		switch (test)
		{
			case MatchExtension.Test.Eins_ case1:
				eins(case1);
				break;
			case MatchExtension.Test.Zwei_ case2:
				zwei(case2);
				break;
			default:
				throw new ArgumentException($"Unknown type derived from MatchExtension.Test: {test.GetType().Name}");
		}
	}

	public static async Task Switch(this MatchExtension.Test test, Func<MatchExtension.Test.Eins_, Task> eins, Func<MatchExtension.Test.Zwei_, Task> zwei)
	{
		switch (test)
		{
			case MatchExtension.Test.Eins_ case1:
				await eins(case1).ConfigureAwait(false);
				break;
			case MatchExtension.Test.Zwei_ case2:
				await zwei(case2).ConfigureAwait(false);
				break;
			default:
				throw new ArgumentException($"Unknown type derived from MatchExtension.Test: {test.GetType().Name}");
		}
	}

	public static async Task Switch(Task<MatchExtension.Test> test, Action<MatchExtension.Test.Eins_> eins, Action<MatchExtension.Test.Zwei_> zwei) => (await test.ConfigureAwait(false)).Switch(eins, zwei);
	public static async Task Switch(Task<MatchExtension.Test> test, Func<MatchExtension.Test.Eins_, Task> eins, Func<MatchExtension.Test.Zwei_, Task> zwei) => await (await test.ConfigureAwait(false)).Switch(eins, zwei).ConfigureAwait(false);

	public static T Match<T>(this MatchExtension.Test test, Func<MatchExtension.Test.Eins_, T> eins, Func<MatchExtension.Test.Zwei_, T> zwei)
	{
		switch (test.UnionCase)
		{
			case MatchExtension.Test.UnionCases.Eins:
				return eins((MatchExtension.Test.Eins_)test);
			case MatchExtension.Test.UnionCases.Zwei:
				return zwei((MatchExtension.Test.Zwei_)test);
			default:
				throw new ArgumentException($"Unknown type derived from MatchExtension.Test: {test.GetType().Name}");
		}
	}

	public static async Task<T> Match<T>(this MatchExtension.Test test, Func<MatchExtension.Test.Eins_, Task<T>> eins, Func<MatchExtension.Test.Zwei_, Task<T>> zwei)
	{
		return test.UnionCase switch
		{
			MatchExtension.Test.UnionCases.Eins => await eins((MatchExtension.Test.Eins_)test).ConfigureAwait(false),
			MatchExtension.Test.UnionCases.Zwei => await zwei((MatchExtension.Test.Zwei_)test).ConfigureAwait(false),
			_ => throw new ArgumentException($"Unknown type derived from MatchExtension.Test: {test.GetType().Name}")
		};
	}

	public static async Task<T> Match<T>(this Task<MatchExtension.Test> test, Func<MatchExtension.Test.Eins_, T> eins, Func<MatchExtension.Test.Zwei_, T> zwei) => (await test.ConfigureAwait(false)).Match(eins, zwei);
	public static async Task<T> Match<T>(this Task<MatchExtension.Test> test, Func<MatchExtension.Test.Eins_, Task<T>> eins, Func<MatchExtension.Test.Zwei_, Task<T>> zwei) => await(await test.ConfigureAwait(false)).Match(eins, zwei).ConfigureAwait(false);
}