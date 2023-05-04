//HintName: FunicularSwitch.Test.Outer.testMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Outer.test test, Func<T> eins, Func<T> zwei) =>
		test switch
		{
			FunicularSwitch.Test.Outer.test.eins => eins(),
			FunicularSwitch.Test.Outer.test.zwei => zwei(),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.test: {test.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.Outer.test test, Func<Task<T>> eins, Func<Task<T>> zwei) =>
		test switch
		{
			FunicularSwitch.Test.Outer.test.eins => eins(),
			FunicularSwitch.Test.Outer.test.zwei => zwei(),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.test: {test.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Outer.test> test, Func<T> eins, Func<T> zwei) =>
		(await test.ConfigureAwait(false)).Match(eins, zwei);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.Outer.test> test, Func<Task<T>> eins, Func<Task<T>> zwei) =>
		await (await test.ConfigureAwait(false)).Match(eins, zwei).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Outer.test test, Action eins, Action zwei)
		{
			switch (test)
			{
				case FunicularSwitch.Test.Outer.test.eins:
					eins();
					break;
				case FunicularSwitch.Test.Outer.test.zwei:
					zwei();
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.test: {test.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.Outer.test test, Func<Task> eins, Func<Task> zwei)
		{
			switch (test)
			{
				case FunicularSwitch.Test.Outer.test.eins:
					await eins().ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.test.zwei:
					await zwei().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.test: {test.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Outer.test> test, Action eins, Action zwei) =>
		(await test.ConfigureAwait(false)).Switch(eins, zwei);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.Outer.test> test, Func<Task> eins, Func<Task> zwei) =>
		await (await test.ConfigureAwait(false)).Switch(eins, zwei).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
