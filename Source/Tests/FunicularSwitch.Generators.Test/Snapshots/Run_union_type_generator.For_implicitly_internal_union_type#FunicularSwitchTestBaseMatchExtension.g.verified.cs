//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	internal static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.One, T> one, System.Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task<T>> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.One, T> one, System.Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(one, two);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task<T>> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, System.Action<FunicularSwitch.Test.One> one, System.Action<FunicularSwitch.Test.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One case1:
					one(case1);
					break;
				case FunicularSwitch.Test.Two case2:
					two(case2);
					break;
				default:
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One case1:
					await one(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two case2:
					await two(case2).ConfigureAwait(false);
					break;
				default:
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Action<FunicularSwitch.Test.One> one, System.Action<FunicularSwitch.Test.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(one, two);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.One, System.Threading.Tasks.Task> one, System.Func<FunicularSwitch.Test.Two, System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
