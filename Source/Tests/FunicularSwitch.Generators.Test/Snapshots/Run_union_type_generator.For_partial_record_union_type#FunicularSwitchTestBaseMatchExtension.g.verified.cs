//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	internal static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Two, T> two, Func<FunicularSwitch.Test.Three, T> three, Func<FunicularSwitch.Test.Cases.Nested, T> nested, Func<FunicularSwitch.Test.Cases.Five, T> five, Func<FunicularSwitch.Test.WithDefault, T> withDefault) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			FunicularSwitch.Test.Three case3 => three(case3),
			FunicularSwitch.Test.Cases.Nested case4 => nested(case4),
			FunicularSwitch.Test.Cases.Five case5 => five(case5),
			FunicularSwitch.Test.WithDefault case6 => withDefault(case6),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Two, Task<T>> two, Func<FunicularSwitch.Test.Three, Task<T>> three, Func<FunicularSwitch.Test.Cases.Nested, Task<T>> nested, Func<FunicularSwitch.Test.Cases.Five, Task<T>> five, Func<FunicularSwitch.Test.WithDefault, Task<T>> withDefault) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			FunicularSwitch.Test.Three case3 => three(case3),
			FunicularSwitch.Test.Cases.Nested case4 => nested(case4),
			FunicularSwitch.Test.Cases.Five case5 => five(case5),
			FunicularSwitch.Test.WithDefault case6 => withDefault(case6),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Two, T> two, Func<FunicularSwitch.Test.Three, T> three, Func<FunicularSwitch.Test.Cases.Nested, T> nested, Func<FunicularSwitch.Test.Cases.Five, T> five, Func<FunicularSwitch.Test.WithDefault, T> withDefault) =>
		(await @base.ConfigureAwait(false)).Match(one, two, three, nested, five, withDefault);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Two, Task<T>> two, Func<FunicularSwitch.Test.Three, Task<T>> three, Func<FunicularSwitch.Test.Cases.Nested, Task<T>> nested, Func<FunicularSwitch.Test.Cases.Five, Task<T>> five, Func<FunicularSwitch.Test.WithDefault, Task<T>> withDefault) =>
		await (await @base.ConfigureAwait(false)).Match(one, two, three, nested, five, withDefault).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, Action<FunicularSwitch.Test.One> one, Action<FunicularSwitch.Test.Two> two, Action<FunicularSwitch.Test.Three> three, Action<FunicularSwitch.Test.Cases.Nested> nested, Action<FunicularSwitch.Test.Cases.Five> five, Action<FunicularSwitch.Test.WithDefault> withDefault)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One case1:
					one(case1);
					break;
				case FunicularSwitch.Test.Two case2:
					two(case2);
					break;
				case FunicularSwitch.Test.Three case3:
					three(case3);
					break;
				case FunicularSwitch.Test.Cases.Nested case4:
					nested(case4);
					break;
				case FunicularSwitch.Test.Cases.Five case5:
					five(case5);
					break;
				case FunicularSwitch.Test.WithDefault case6:
					withDefault(case6);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.One, Task> one, Func<FunicularSwitch.Test.Two, Task> two, Func<FunicularSwitch.Test.Three, Task> three, Func<FunicularSwitch.Test.Cases.Nested, Task> nested, Func<FunicularSwitch.Test.Cases.Five, Task> five, Func<FunicularSwitch.Test.WithDefault, Task> withDefault)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One case1:
					await one(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two case2:
					await two(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Three case3:
					await three(case3).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Cases.Nested case4:
					await nested(case4).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Cases.Five case5:
					await five(case5).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.WithDefault case6:
					await withDefault(case6).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Action<FunicularSwitch.Test.One> one, Action<FunicularSwitch.Test.Two> two, Action<FunicularSwitch.Test.Three> three, Action<FunicularSwitch.Test.Cases.Nested> nested, Action<FunicularSwitch.Test.Cases.Five> five, Action<FunicularSwitch.Test.WithDefault> withDefault) =>
		(await @base.ConfigureAwait(false)).Switch(one, two, three, nested, five, withDefault);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.One, Task> one, Func<FunicularSwitch.Test.Two, Task> two, Func<FunicularSwitch.Test.Three, Task> three, Func<FunicularSwitch.Test.Cases.Nested, Task> nested, Func<FunicularSwitch.Test.Cases.Five, Task> five, Func<FunicularSwitch.Test.WithDefault, Task> withDefault) =>
		await (await @base.ConfigureAwait(false)).Switch(one, two, three, nested, five, withDefault).ConfigureAwait(false);
	}
	
	internal abstract partial record Base
	{
		internal static FunicularSwitch.Test.Base One(int Number) => new FunicularSwitch.Test.One(Number);
		public static FunicularSwitch.Test.Base Two() => new FunicularSwitch.Test.Two();
		public static FunicularSwitch.Test.Base Nested() => new FunicularSwitch.Test.Cases.Nested();
		internal static FunicularSwitch.Test.Base WithDefault(int Number = 42) => new FunicularSwitch.Test.WithDefault(Number);
	}
}
#pragma warning restore 1591
