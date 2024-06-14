//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	internal static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two, global::System.Func<FunicularSwitch.Test.Three, T> three, global::System.Func<FunicularSwitch.Test.Cases.Nested, T> nested, global::System.Func<FunicularSwitch.Test.Cases.Five, T> five, global::System.Func<FunicularSwitch.Test.WithDefault, T> withDefault) =>
		@base switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Two two2 => two(two2),
			FunicularSwitch.Test.Three three3 => three(three3),
			FunicularSwitch.Test.Cases.Nested nested4 => nested(nested4),
			FunicularSwitch.Test.Cases.Five five5 => five(five5),
			FunicularSwitch.Test.WithDefault withDefault6 => withDefault(withDefault6),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task<T>> three, global::System.Func<FunicularSwitch.Test.Cases.Nested, global::System.Threading.Tasks.Task<T>> nested, global::System.Func<FunicularSwitch.Test.Cases.Five, global::System.Threading.Tasks.Task<T>> five, global::System.Func<FunicularSwitch.Test.WithDefault, global::System.Threading.Tasks.Task<T>> withDefault) =>
		@base switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Two two2 => two(two2),
			FunicularSwitch.Test.Three three3 => three(three3),
			FunicularSwitch.Test.Cases.Nested nested4 => nested(nested4),
			FunicularSwitch.Test.Cases.Five five5 => five(five5),
			FunicularSwitch.Test.WithDefault withDefault6 => withDefault(withDefault6),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two, global::System.Func<FunicularSwitch.Test.Three, T> three, global::System.Func<FunicularSwitch.Test.Cases.Nested, T> nested, global::System.Func<FunicularSwitch.Test.Cases.Five, T> five, global::System.Func<FunicularSwitch.Test.WithDefault, T> withDefault) =>
		(await @base.ConfigureAwait(false)).Match(one, two, three, nested, five, withDefault);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task<T>> three, global::System.Func<FunicularSwitch.Test.Cases.Nested, global::System.Threading.Tasks.Task<T>> nested, global::System.Func<FunicularSwitch.Test.Cases.Five, global::System.Threading.Tasks.Task<T>> five, global::System.Func<FunicularSwitch.Test.WithDefault, global::System.Threading.Tasks.Task<T>> withDefault) =>
		await (await @base.ConfigureAwait(false)).Match(one, two, three, nested, five, withDefault).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two, global::System.Action<FunicularSwitch.Test.Three> three, global::System.Action<FunicularSwitch.Test.Cases.Nested> nested, global::System.Action<FunicularSwitch.Test.Cases.Five> five, global::System.Action<FunicularSwitch.Test.WithDefault> withDefault)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One one1:
					one(one1);
					break;
				case FunicularSwitch.Test.Two two2:
					two(two2);
					break;
				case FunicularSwitch.Test.Three three3:
					three(three3);
					break;
				case FunicularSwitch.Test.Cases.Nested nested4:
					nested(nested4);
					break;
				case FunicularSwitch.Test.Cases.Five five5:
					five(five5);
					break;
				case FunicularSwitch.Test.WithDefault withDefault6:
					withDefault(withDefault6);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task> three, global::System.Func<FunicularSwitch.Test.Cases.Nested, global::System.Threading.Tasks.Task> nested, global::System.Func<FunicularSwitch.Test.Cases.Five, global::System.Threading.Tasks.Task> five, global::System.Func<FunicularSwitch.Test.WithDefault, global::System.Threading.Tasks.Task> withDefault)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One one1:
					await one(one1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two two2:
					await two(two2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Three three3:
					await three(three3).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Cases.Nested nested4:
					await nested(nested4).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Cases.Five five5:
					await five(five5).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.WithDefault withDefault6:
					await withDefault(withDefault6).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two, global::System.Action<FunicularSwitch.Test.Three> three, global::System.Action<FunicularSwitch.Test.Cases.Nested> nested, global::System.Action<FunicularSwitch.Test.Cases.Five> five, global::System.Action<FunicularSwitch.Test.WithDefault> withDefault) =>
		(await @base.ConfigureAwait(false)).Switch(one, two, three, nested, five, withDefault);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task> three, global::System.Func<FunicularSwitch.Test.Cases.Nested, global::System.Threading.Tasks.Task> nested, global::System.Func<FunicularSwitch.Test.Cases.Five, global::System.Threading.Tasks.Task> five, global::System.Func<FunicularSwitch.Test.WithDefault, global::System.Threading.Tasks.Task> withDefault) =>
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
