﻿//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.Aaa aaa1 => aaa(aaa1),
			FunicularSwitch.Test.One one2 => one(one2),
			FunicularSwitch.Test.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.Aaa aaa1 => aaa(aaa1),
			FunicularSwitch.Test.One one2 => one(one2),
			FunicularSwitch.Test.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(aaa, one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(aaa, one, two).ConfigureAwait(false);
		
		public static void Switch(this global::FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Aaa aaa1:
					aaa(aaa1);
					break;
				case FunicularSwitch.Test.One one2:
					one(one2);
					break;
				case FunicularSwitch.Test.Two two3:
					two(two3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Aaa aaa1:
					await aaa(aaa1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.One one2:
					await one(one2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two two3:
					await two(two3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(aaa, one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(aaa, one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
