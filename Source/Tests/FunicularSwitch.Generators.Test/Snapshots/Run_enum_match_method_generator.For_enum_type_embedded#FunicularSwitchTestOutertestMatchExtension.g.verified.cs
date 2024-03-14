//HintName: FunicularSwitchTestOutertestMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class Outer_testMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Outer.test test, System.Func<T> one, System.Func<T> two) =>
		test switch
		{
			FunicularSwitch.Test.Outer.test.one => one(),
			FunicularSwitch.Test.Outer.test.two => two(),
			_ => throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.Outer.test: {test.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Outer.test test, System.Func<System.Threading.Tasks.Task<T>> one, System.Func<System.Threading.Tasks.Task<T>> two) =>
		test switch
		{
			FunicularSwitch.Test.Outer.test.one => one(),
			FunicularSwitch.Test.Outer.test.two => two(),
			_ => throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.Outer.test: {test.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.test> test, System.Func<T> one, System.Func<T> two) =>
		(await test.ConfigureAwait(false)).Match(one, two);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.test> test, System.Func<System.Threading.Tasks.Task<T>> one, System.Func<System.Threading.Tasks.Task<T>> two) =>
		await (await test.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Outer.test test, System.Action one, System.Action two)
		{
			switch (test)
			{
				case FunicularSwitch.Test.Outer.test.one:
					one();
					break;
				case FunicularSwitch.Test.Outer.test.two:
					two();
					break;
				default:
					throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.Outer.test: {test.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Outer.test test, System.Func<System.Threading.Tasks.Task> one, System.Func<System.Threading.Tasks.Task> two)
		{
			switch (test)
			{
				case FunicularSwitch.Test.Outer.test.one:
					await one().ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.test.two:
					await two().ConfigureAwait(false);
					break;
				default:
					throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.Outer.test: {test.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.test> test, System.Action one, System.Action two) =>
		(await test.ConfigureAwait(false)).Switch(one, two);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.test> test, System.Func<System.Threading.Tasks.Task> one, System.Func<System.Threading.Tasks.Task> two) =>
		await (await test.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
