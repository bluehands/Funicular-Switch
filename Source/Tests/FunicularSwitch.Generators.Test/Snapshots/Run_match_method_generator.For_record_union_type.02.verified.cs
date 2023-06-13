//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.One case2 => one(case2),
			FunicularSwitch.Test.Two case3 => two(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Two, Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.One case2 => one(case2),
			FunicularSwitch.Test.Two case3 => two(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(aaa, one, two);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Two, Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(aaa, one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, Action<FunicularSwitch.Test.Aaa> aaa, Action<FunicularSwitch.Test.One> one, Action<FunicularSwitch.Test.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Aaa case1:
					aaa(case1);
					break;
				case FunicularSwitch.Test.One case2:
					one(case2);
					break;
				case FunicularSwitch.Test.Two case3:
					two(case3);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Aaa, Task> aaa, Func<FunicularSwitch.Test.One, Task> one, Func<FunicularSwitch.Test.Two, Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Aaa case1:
					await aaa(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.One case2:
					await one(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two case3:
					await two(case3).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Action<FunicularSwitch.Test.Aaa> aaa, Action<FunicularSwitch.Test.One> one, Action<FunicularSwitch.Test.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(aaa, one, two);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Aaa, Task> aaa, Func<FunicularSwitch.Test.One, Task> one, Func<FunicularSwitch.Test.Two, Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(aaa, one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
