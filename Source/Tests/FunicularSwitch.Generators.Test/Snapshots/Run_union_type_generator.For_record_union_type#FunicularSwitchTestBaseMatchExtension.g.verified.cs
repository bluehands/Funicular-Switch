﻿//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.One case2 => one(case2),
			FunicularSwitch.Test.Two case3 => two(case3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.One case2 => one(case2),
			FunicularSwitch.Test.Two case3 => two(case3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(aaa, one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(aaa, one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two)
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
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two)
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
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(aaa, one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(aaa, one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
