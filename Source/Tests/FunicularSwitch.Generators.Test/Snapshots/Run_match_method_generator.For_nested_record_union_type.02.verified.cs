//HintName: BaseMatchExtension.g.cs
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Outer.Base @base, Func<FunicularSwitch.Test.Outer.Aaa, T> aaa, Func<FunicularSwitch.Test.Outer.One, T> one, Func<FunicularSwitch.Test.Outer.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.Outer.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.Outer.One case2 => one(case2),
			FunicularSwitch.Test.Outer.Two case3 => two(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Outer.Base @base, Func<FunicularSwitch.Test.Outer.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.Outer.One, Task<T>> one, Func<FunicularSwitch.Test.Outer.Two, Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.Outer.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.Outer.One case2 => one(case2),
			FunicularSwitch.Test.Outer.Two case3 => two(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Outer.Base> @base, Func<FunicularSwitch.Test.Outer.Aaa, T> aaa, Func<FunicularSwitch.Test.Outer.One, T> one, Func<FunicularSwitch.Test.Outer.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(aaa, one, two);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Outer.Base> @base, Func<FunicularSwitch.Test.Outer.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.Outer.One, Task<T>> one, Func<FunicularSwitch.Test.Outer.Two, Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(aaa, one, two).ConfigureAwait(false);
	}
}
