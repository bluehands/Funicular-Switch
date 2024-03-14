//HintName: FunicularSwitchTestOuterBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Outer.Base @base, System.Func<FunicularSwitch.Test.Outer.Aaa, T> aaa, System.Func<FunicularSwitch.Test.Outer.One, T> one, System.Func<FunicularSwitch.Test.Outer.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.Outer.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.Outer.One case2 => one(case2),
			FunicularSwitch.Test.Outer.Two case3 => two(case3),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Outer.Base @base, System.Func<FunicularSwitch.Test.Outer.Aaa, System.Threading.Tasks.Task<T>> aaa, System.Func<FunicularSwitch.Test.Outer.One, System.Threading.Tasks.Task<T>> one, System.Func<FunicularSwitch.Test.Outer.Two, System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.Outer.Aaa case1 => aaa(case1),
			FunicularSwitch.Test.Outer.One case2 => one(case2),
			FunicularSwitch.Test.Outer.Two case3 => two(case3),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, System.Func<FunicularSwitch.Test.Outer.Aaa, T> aaa, System.Func<FunicularSwitch.Test.Outer.One, T> one, System.Func<FunicularSwitch.Test.Outer.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(aaa, one, two);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, System.Func<FunicularSwitch.Test.Outer.Aaa, System.Threading.Tasks.Task<T>> aaa, System.Func<FunicularSwitch.Test.Outer.One, System.Threading.Tasks.Task<T>> one, System.Func<FunicularSwitch.Test.Outer.Two, System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(aaa, one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Outer.Base @base, System.Action<FunicularSwitch.Test.Outer.Aaa> aaa, System.Action<FunicularSwitch.Test.Outer.One> one, System.Action<FunicularSwitch.Test.Outer.Two> two)
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
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Outer.Base @base, System.Func<FunicularSwitch.Test.Outer.Aaa, System.Threading.Tasks.Task> aaa, System.Func<FunicularSwitch.Test.Outer.One, System.Threading.Tasks.Task> one, System.Func<FunicularSwitch.Test.Outer.Two, System.Threading.Tasks.Task> two)
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
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, System.Action<FunicularSwitch.Test.Outer.Aaa> aaa, System.Action<FunicularSwitch.Test.Outer.One> one, System.Action<FunicularSwitch.Test.Outer.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(aaa, one, two);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.Base> @base, System.Func<FunicularSwitch.Test.Outer.Aaa, System.Threading.Tasks.Task> aaa, System.Func<FunicularSwitch.Test.Outer.One, System.Threading.Tasks.Task> one, System.Func<FunicularSwitch.Test.Outer.Two, System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(aaa, one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
