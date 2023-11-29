//HintName: FunicularSwitchTestIBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class IBaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.IBase iBase, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Two, T> two) =>
		iBase switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBase: {iBase.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.IBase iBase, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Two, Task<T>> two) =>
		iBase switch
		{
			FunicularSwitch.Test.One case1 => one(case1),
			FunicularSwitch.Test.Two case2 => two(case2),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBase: {iBase.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.IBase> iBase, Func<FunicularSwitch.Test.One, T> one, Func<FunicularSwitch.Test.Two, T> two) =>
		(await iBase.ConfigureAwait(false)).Match(one, two);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.IBase> iBase, Func<FunicularSwitch.Test.One, Task<T>> one, Func<FunicularSwitch.Test.Two, Task<T>> two) =>
		await (await iBase.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.IBase iBase, Action<FunicularSwitch.Test.One> one, Action<FunicularSwitch.Test.Two> two)
		{
			switch (iBase)
			{
				case FunicularSwitch.Test.One case1:
					one(case1);
					break;
				case FunicularSwitch.Test.Two case2:
					two(case2);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBase: {iBase.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.IBase iBase, Func<FunicularSwitch.Test.One, Task> one, Func<FunicularSwitch.Test.Two, Task> two)
		{
			switch (iBase)
			{
				case FunicularSwitch.Test.One case1:
					await one(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two case2:
					await two(case2).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBase: {iBase.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.IBase> iBase, Action<FunicularSwitch.Test.One> one, Action<FunicularSwitch.Test.Two> two) =>
		(await iBase.ConfigureAwait(false)).Switch(one, two);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.IBase> iBase, Func<FunicularSwitch.Test.One, Task> one, Func<FunicularSwitch.Test.Two, Task> two) =>
		await (await iBase.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
