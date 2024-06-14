//HintName: FunicularSwitchTestOuterBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Outer.Base @base, global::System.Func<FunicularSwitch.Test.Outer.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.Outer.One, T> one, global::System.Func<FunicularSwitch.Test.Outer.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.Outer.Aaa aaa1 => aaa(aaa1),
			FunicularSwitch.Test.Outer.One one2 => one(one2),
			FunicularSwitch.Test.Outer.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Outer.Base @base, global::System.Func<FunicularSwitch.Test.Outer.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.Outer.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Outer.Two, global::System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.Outer.Aaa aaa1 => aaa(aaa1),
			FunicularSwitch.Test.Outer.One one2 => one(one2),
			FunicularSwitch.Test.Outer.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, global::System.Func<FunicularSwitch.Test.Outer.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.Outer.One, T> one, global::System.Func<FunicularSwitch.Test.Outer.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(aaa, one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, global::System.Func<FunicularSwitch.Test.Outer.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.Outer.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Outer.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(aaa, one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Outer.Base @base, global::System.Action<FunicularSwitch.Test.Outer.Aaa> aaa, global::System.Action<FunicularSwitch.Test.Outer.One> one, global::System.Action<FunicularSwitch.Test.Outer.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Outer.Aaa aaa1:
					aaa(aaa1);
					break;
				case FunicularSwitch.Test.Outer.One one2:
					one(one2);
					break;
				case FunicularSwitch.Test.Outer.Two two3:
					two(two3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Outer.Base @base, global::System.Func<FunicularSwitch.Test.Outer.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.Outer.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Outer.Two, global::System.Threading.Tasks.Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Outer.Aaa aaa1:
					await aaa(aaa1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.One one2:
					await one(one2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.Two two3:
					await two(two3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, global::System.Action<FunicularSwitch.Test.Outer.Aaa> aaa, global::System.Action<FunicularSwitch.Test.Outer.One> one, global::System.Action<FunicularSwitch.Test.Outer.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(aaa, one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, global::System.Func<FunicularSwitch.Test.Outer.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.Outer.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Outer.Two, global::System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(aaa, one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
