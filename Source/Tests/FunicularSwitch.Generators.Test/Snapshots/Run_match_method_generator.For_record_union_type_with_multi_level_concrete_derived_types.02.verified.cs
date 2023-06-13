//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Bbb, T> bbb, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.BaseChild, T> baseChild) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb case1 => bbb(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.BaseChild case3 => baseChild(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Bbb, Task<T>> bbb, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.BaseChild, Task<T>> baseChild) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb case1 => bbb(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.BaseChild case3 => baseChild(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Bbb, T> bbb, Func<FunicularSwitch.Test.Aaa, T> aaa, Func<FunicularSwitch.Test.BaseChild, T> baseChild) =>
		(await @base.ConfigureAwait(false)).Match(bbb, aaa, baseChild);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Bbb, Task<T>> bbb, Func<FunicularSwitch.Test.Aaa, Task<T>> aaa, Func<FunicularSwitch.Test.BaseChild, Task<T>> baseChild) =>
		await (await @base.ConfigureAwait(false)).Match(bbb, aaa, baseChild).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, Action<FunicularSwitch.Test.Bbb> bbb, Action<FunicularSwitch.Test.Aaa> aaa, Action<FunicularSwitch.Test.BaseChild> baseChild)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Bbb case1:
					bbb(case1);
					break;
				case FunicularSwitch.Test.Aaa case2:
					aaa(case2);
					break;
				case FunicularSwitch.Test.BaseChild case3:
					baseChild(case3);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Bbb, Task> bbb, Func<FunicularSwitch.Test.Aaa, Task> aaa, Func<FunicularSwitch.Test.BaseChild, Task> baseChild)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Bbb case1:
					await bbb(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Aaa case2:
					await aaa(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.BaseChild case3:
					await baseChild(case3).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Action<FunicularSwitch.Test.Bbb> bbb, Action<FunicularSwitch.Test.Aaa> aaa, Action<FunicularSwitch.Test.BaseChild> baseChild) =>
		(await @base.ConfigureAwait(false)).Switch(bbb, aaa, baseChild);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Bbb, Task> bbb, Func<FunicularSwitch.Test.Aaa, Task> aaa, Func<FunicularSwitch.Test.BaseChild, Task> baseChild) =>
		await (await @base.ConfigureAwait(false)).Switch(bbb, aaa, baseChild).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
