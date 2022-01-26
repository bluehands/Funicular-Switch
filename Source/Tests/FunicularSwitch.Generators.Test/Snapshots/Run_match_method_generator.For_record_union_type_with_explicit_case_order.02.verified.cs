//HintName: BaseMatchExtension.g.cs
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this Base @base, Func<FunicularSwitch.Test.Zwei, T> zwei, Func<FunicularSwitch.Test.Eins, T> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei case1 => zwei(case1),
			FunicularSwitch.Test.Eins case2 => eins(case2),
			_ => throw new ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this Base @base, Func<FunicularSwitch.Test.Zwei, Task<T>> zwei, Func<FunicularSwitch.Test.Eins, Task<T>> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei case1 => zwei(case1),
			FunicularSwitch.Test.Eins case2 => eins(case2),
			_ => throw new ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<Base> @base, Func<FunicularSwitch.Test.Zwei, T> zwei, Func<FunicularSwitch.Test.Eins, T> eins) =>
		(await @base.ConfigureAwait(false)).Match(zwei, eins);
		
		public static async Task<T> Match<T>(this Task<Base> @base, Func<FunicularSwitch.Test.Zwei, Task<T>> zwei, Func<FunicularSwitch.Test.Eins, Task<T>> eins) =>
		await (await @base.ConfigureAwait(false)).Match(zwei, eins).ConfigureAwait(false);
	}
}
