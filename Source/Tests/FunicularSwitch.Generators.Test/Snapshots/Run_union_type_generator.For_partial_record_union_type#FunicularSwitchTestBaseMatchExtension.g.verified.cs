//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	internal static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.One, T> one, System.Func<FunicularSwitch.Test.Two, T> two, System.Func<FunicularSwitch.Test.Three, T> three, System.Func<FunicularSwitch.Test.Cases.Nested, T> nested, System.Func<FunicularSwitch.Test.Cases.Five, T> five, System.Func<FunicularSwitch.Test.WithDefault, T> withDefault) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			FunicularSwitch.Test.Three case3 => three(case3),
			FunicularSwitch.Test.Cases.Nested case4 => nested(case4),
			FunicularSwitch.Test.Cases.Five case5 => five(case5),
			FunicularSwitch.Test.WithDefault case6 => withDefault(case6),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task<T>> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task<T>> two, System.Func<FunicularSwitch.Test.Three, System.Threading.Tasks.Task<T>> three, System.Func<FunicularSwitch.Test.Cases.Nested, System.Threading.Tasks.Task<T>> nested, System.Func<FunicularSwitch.Test.Cases.Five, System.Threading.Tasks.Task<T>> five, System.Func<FunicularSwitch.Test.WithDefault, System.Threading.Tasks.Task<T>> withDefault) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			FunicularSwitch.Test.Three case3 => three(case3),
			FunicularSwitch.Test.Cases.Nested case4 => nested(case4),
			FunicularSwitch.Test.Cases.Five case5 => five(case5),
			FunicularSwitch.Test.WithDefault case6 => withDefault(case6),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.One, T> one, System.Func<FunicularSwitch.Test.Two, T> two, System.Func<FunicularSwitch.Test.Three, T> three, System.Func<FunicularSwitch.Test.Cases.Nested, T> nested, System.Func<FunicularSwitch.Test.Cases.Five, T> five, System.Func<FunicularSwitch.Test.WithDefault, T> withDefault) =>
		(await @base.ConfigureAwait(false)).Match(one, two, three, nested, five, withDefault);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task<T>> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task<T>> two, System.Func<FunicularSwitch.Test.Three, System.Threading.Tasks.Task<T>> three, System.Func<FunicularSwitch.Test.Cases.Nested, System.Threading.Tasks.Task<T>> nested, System.Func<FunicularSwitch.Test.Cases.Five, System.Threading.Tasks.Task<T>> five, System.Func<FunicularSwitch.Test.WithDefault, System.Threading.Tasks.Task<T>> withDefault) =>
		await (await @base.ConfigureAwait(false)).Match(one, two, three, nested, five, withDefault).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, System.Action<FunicularSwitch.Test.One> one, System.Action<FunicularSwitch.Test.Two> two, System.Action<FunicularSwitch.Test.Three> three, System.Action<FunicularSwitch.Test.Cases.Nested> nested, System.Action<FunicularSwitch.Test.Cases.Five> five, System.Action<FunicularSwitch.Test.WithDefault> withDefault)
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
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task> two, System.Func<FunicularSwitch.Test.Three, System.Threading.Tasks.Task> three, System.Func<FunicularSwitch.Test.Cases.Nested, System.Threading.Tasks.Task> nested, System.Func<FunicularSwitch.Test.Cases.Five, System.Threading.Tasks.Task> five, System.Func<FunicularSwitch.Test.WithDefault, System.Threading.Tasks.Task> withDefault)
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
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Action<FunicularSwitch.Test.One> one, System.Action<FunicularSwitch.Test.Two> two, System.Action<FunicularSwitch.Test.Three> three, System.Action<FunicularSwitch.Test.Cases.Nested> nested, System.Action<FunicularSwitch.Test.Cases.Five> five, System.Action<FunicularSwitch.Test.WithDefault> withDefault) =>
		(await @base.ConfigureAwait(false)).Switch(one, two, three, nested, five, withDefault);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task> two, System.Func<FunicularSwitch.Test.Three, System.Threading.Tasks.Task> three, System.Func<FunicularSwitch.Test.Cases.Nested, System.Threading.Tasks.Task> nested, System.Func<FunicularSwitch.Test.Cases.Five, System.Threading.Tasks.Task> five, System.Func<FunicularSwitch.Test.WithDefault, System.Threading.Tasks.Task> withDefault) =>
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
