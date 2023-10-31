using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class OptionSpecs
{
	[TestMethod]
	public void NullCoalescingWithOptionBoolBehavesAsExpected()
	{
		bool? foo = null;

		var implicitTypedOption = foo ?? Option<bool>.None;
		implicitTypedOption.GetType().Should().Be(typeof(None<bool>));

		Option<bool> option = foo ?? Option<bool>.None;
		option.Equals(Option<bool>.None).Should().BeTrue();
	}

	[TestMethod]
	public void ToOptionForNullable()
	{
		var none = ((bool?)null).ToOption();
		none.Equals(Option<bool>.None).Should().BeTrue();

		var some = ((bool?)false).ToOption();
		some.Equals(Option.Some(false)).Should().BeTrue();

		var nullableNull = none.ToNullable();
		nullableNull.Should().BeNull();

		var nullableWithValue = some.ToNullable();
		nullableWithValue.Should().Be(false);
	}

	[TestMethod]
	public void NullCoalescingWithResultBoolBehavesAsExpected()
	{
		bool? foo = null;

		var result = foo ?? Result<bool>.Error("Value is missing");
		if (result)
		{

		}
		result.Equals(Result<bool>.Error("Value is missing")).Should().BeTrue();
	}
	
	    [TestMethod]
        public void QueryExpressionSelect()
        {
	        Option<int> subject = 42;
            var result =
                from r in subject
                select r;
            result.Should().BeEquivalentTo(Option.Some(42));
        }
    
        [TestMethod]
        public void QueryExpressionSelectMany()
        {
            Option<int> some = 42;
            var none = Option.None<int>();
    
            (
                from r in some
                from r1 in none
                select r1
            ).Should().BeEquivalentTo(none);
    
            (
                from r in none
                from r1 in some
                select r1
            ).Should().BeEquivalentTo(none);
    
            (
                from r in some
                let x = r * 2
                from r1 in some
                select x
            ).Should().BeEquivalentTo(some.Map(r => r * 2));
        }
    
        [TestMethod]
        public async Task QueryExpressionSelectManyAsync()
        {
            Task<Option<int>> someAsync = Task.FromResult(Option.Some(42));
            var noneAsync = Task.FromResult(Option.None<int>());
            var some = Option.Some(1);
    
            (await (
                from r in someAsync
                from r1 in noneAsync
                select r1
            )).Should().BeEquivalentTo(await noneAsync);
    
            (await (
                from r in noneAsync
                from r1 in someAsync
                select r1
            )).Should().BeEquivalentTo(await noneAsync);
    
            (await (
                from r in someAsync
                let x = r * 2
                from r1 in someAsync
                select x
            )).Should().BeEquivalentTo(await someAsync.Map(r => r * 2));        
            
            (await (
                from r in some
                let x = r * 2
                from r1 in someAsync
                select x
            )).Should().BeEquivalentTo( some.Map(r => r * 2));        
            
            (await (
                from r in someAsync
                let x = r * 2
                from r1 in some
                select x
            )).Should().BeEquivalentTo(await someAsync.Map(r => r * 2));
        }
}