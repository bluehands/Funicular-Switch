//HintName: FunicularSwitchTesttestMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class testMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.test test, global::System.Func<T> one, global::System.Func<T> two) =>
		test switch
		{
			FunicularSwitch.Test.test.one => one(),
			FunicularSwitch.Test.test.two => two(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.test: {test.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.test test, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
		test switch
		{
			FunicularSwitch.Test.test.one => one(),
			FunicularSwitch.Test.test.two => two(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.test: {test.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.test> test, global::System.Func<T> one, global::System.Func<T> two) =>
		(await test.ConfigureAwait(false)).Match(one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.test> test, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
		await (await test.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.test test, global::System.Action one, global::System.Action two)
		{
			switch (test)
			{
				case FunicularSwitch.Test.test.one:
					one();
					break;
				case FunicularSwitch.Test.test.two:
					two();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.test: {test.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.test test, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two)
		{
			switch (test)
			{
				case FunicularSwitch.Test.test.one:
					await one().ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.test.two:
					await two().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Test.test: {test.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.test> test, global::System.Action one, global::System.Action two) =>
		(await test.ConfigureAwait(false)).Switch(one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.test> test, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two) =>
		await (await test.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
