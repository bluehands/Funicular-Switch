//HintName: BaseMatchExtension.g.cs
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this Base @base, Func<FunicularSwitch.Test.Bbb, T> bbb, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.BaseChild, T> baseChild) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb case1 => bbb(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.BaseChild case3 => baseChild(case3),
			_ => throw new ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this Base @base, Func<FunicularSwitch.Test.Bbb, Task<T>> bbb, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.BaseChild, Task<T>> baseChild) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb case1 => bbb(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.BaseChild case3 => baseChild(case3),
			_ => throw new ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<Base> @base, Func<FunicularSwitch.Test.Bbb, T> bbb, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.BaseChild, T> baseChild) =>
		(await @base.ConfigureAwait(false)).Match(bbb, aaa, baseChild);
		
		public static async Task<T> Match<T>(this Task<Base> @base, Func<FunicularSwitch.Test.Bbb, Task<T>> bbb, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.BaseChild, Task<T>> baseChild) =>
		await (await @base.ConfigureAwait(false)).Match(bbb, aaa, baseChild).ConfigureAwait(false);
	}
}
