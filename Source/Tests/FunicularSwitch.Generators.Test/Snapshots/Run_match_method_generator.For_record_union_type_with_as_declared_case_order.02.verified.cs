//HintName: BaseMatchExtension.g.cs
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.Two case3 => two(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.Two, Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.Two case3 => two(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(one, aaa, two);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.Two, Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(one, aaa, two).ConfigureAwait(false);
	}
}
