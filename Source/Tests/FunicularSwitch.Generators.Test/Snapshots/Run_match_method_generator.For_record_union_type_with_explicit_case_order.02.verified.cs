//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Zwei, T> zwei, Func<FunicularSwitch.Test.Eins, T> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei case1 => zwei(case1),
			FunicularSwitch.Test.Eins case2 => eins(case2),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Zwei, Task<T>> zwei, Func<FunicularSwitch.Test.Eins, Task<T>> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei case1 => zwei(case1),
			FunicularSwitch.Test.Eins case2 => eins(case2),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Zwei, T> zwei, Func<FunicularSwitch.Test.Eins, T> eins) =>
		(await @base.ConfigureAwait(false)).Match(zwei, eins);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Zwei, Task<T>> zwei, Func<FunicularSwitch.Test.Eins, Task<T>> eins) =>
		await (await @base.ConfigureAwait(false)).Match(zwei, eins).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, Action<FunicularSwitch.Test.Zwei> zwei, Action<FunicularSwitch.Test.Eins> eins)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Zwei case1:
					zwei(case1);
					break;
				case FunicularSwitch.Test.Eins case2:
					eins(case2);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.Base @base, Func<FunicularSwitch.Test.Zwei, Task> zwei, Func<FunicularSwitch.Test.Eins, Task> eins)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Zwei case1:
					await zwei(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Eins case2:
					await eins(case2).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Action<FunicularSwitch.Test.Zwei> zwei, Action<FunicularSwitch.Test.Eins> eins) =>
		(await @base.ConfigureAwait(false)).Switch(zwei, eins);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Base> @base, Func<FunicularSwitch.Test.Zwei, Task> zwei, Func<FunicularSwitch.Test.Eins, Task> eins) =>
		await (await @base.ConfigureAwait(false)).Switch(zwei, eins).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
