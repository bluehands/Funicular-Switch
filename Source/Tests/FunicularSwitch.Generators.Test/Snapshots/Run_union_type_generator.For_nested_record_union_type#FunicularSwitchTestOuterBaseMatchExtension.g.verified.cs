//HintName: FunicularSwitchTestOuterBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
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
		
		public static void Switch(this FunicularSwitch.Test.Outer.Base @base, Action<FunicularSwitch.Test.Outer.Aaa> aaa, Action<FunicularSwitch.Test.Outer.One> one, Action<FunicularSwitch.Test.Outer.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Outer.Aaa case1:
					aaa(case1);
					break;
				case FunicularSwitch.Test.Outer.One case2:
					one(case2);
					break;
				case FunicularSwitch.Test.Outer.Two case3:
					two(case3);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.Outer.Base @base, Func<FunicularSwitch.Test.Outer.Aaa, Task> aaa, Func<FunicularSwitch.Test.Outer.One, Task> one, Func<FunicularSwitch.Test.Outer.Two, Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Outer.Aaa case1:
					await aaa(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.One case2:
					await one(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.Two case3:
					await two(case3).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Outer.Base> @base, Action<FunicularSwitch.Test.Outer.Aaa> aaa, Action<FunicularSwitch.Test.Outer.One> one, Action<FunicularSwitch.Test.Outer.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(aaa, one, two);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Outer.Base> @base, Func<FunicularSwitch.Test.Outer.Aaa, Task> aaa, Func<FunicularSwitch.Test.Outer.One, Task> one, Func<FunicularSwitch.Test.Outer.Two, Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(aaa, one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
