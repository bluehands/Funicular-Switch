using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this UnionTest unionTest, Func<FunicularSwitch.Generators.Consumer.UnionTest.Ons_, T> ons, Func<FunicularSwitch.Generators.Consumer.UnionTest.Zwo_, T> zwo, Func<FunicularSwitch.Generators.Consumer.UnionTest.Droi_, T> droi) =>
		unionTest switch
		{
			FunicularSwitch.Generators.Consumer.UnionTest.Ons_ case1 => ons(case1),
			FunicularSwitch.Generators.Consumer.UnionTest.Zwo_ case2 => zwo(case2),
			FunicularSwitch.Generators.Consumer.UnionTest.Droi_ case3 => droi(case3),
			_ => throw new ArgumentException($"Unknown type derived from UnionTest: {unionTest.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this UnionTest unionTest, Func<FunicularSwitch.Generators.Consumer.UnionTest.Ons_, Task<T>> ons, Func<FunicularSwitch.Generators.Consumer.UnionTest.Zwo_, Task<T>> zwo, Func<FunicularSwitch.Generators.Consumer.UnionTest.Droi_, Task<T>> droi) =>
		unionTest switch
		{
			FunicularSwitch.Generators.Consumer.UnionTest.Ons_ case1 => ons(case1),
			FunicularSwitch.Generators.Consumer.UnionTest.Zwo_ case2 => zwo(case2),
			FunicularSwitch.Generators.Consumer.UnionTest.Droi_ case3 => droi(case3),
			_ => throw new ArgumentException($"Unknown type derived from UnionTest: {unionTest.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<UnionTest> unionTest, Func<FunicularSwitch.Generators.Consumer.UnionTest.Ons_, T> ons, Func<FunicularSwitch.Generators.Consumer.UnionTest.Zwo_, T> zwo, Func<FunicularSwitch.Generators.Consumer.UnionTest.Droi_, T> droi) =>
		(await unionTest.ConfigureAwait(false)).Match(ons, zwo, droi);
		
		public static async Task<T> Match<T>(this Task<UnionTest> unionTest, Func<FunicularSwitch.Generators.Consumer.UnionTest.Ons_, Task<T>> ons, Func<FunicularSwitch.Generators.Consumer.UnionTest.Zwo_, Task<T>> zwo, Func<FunicularSwitch.Generators.Consumer.UnionTest.Droi_, Task<T>> droi) =>
		await (await unionTest.ConfigureAwait(false)).Match(ons, zwo, droi).ConfigureAwait(false);
	}
}
