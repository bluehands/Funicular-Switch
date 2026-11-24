//HintName: testMatchExtension.g.cs
#pragma warning disable 1591
public static partial class testMatchExtension
{
	[global::System.Diagnostics.DebuggerStepThrough]
	public static T Match<T>(this test test, global::System.Func<T> one, global::System.Func<T> two) =>
	test switch
	{
		test.one => one(),
		test.two => two(),
		_ => throw new global::System.ArgumentException($"Unknown enum value from test: {test.GetType().Name}")
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this test test, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
	test switch
	{
		test.one => await one().ConfigureAwait(false),
		test.two => await two().ConfigureAwait(false),
		_ => throw new global::System.ArgumentException($"Unknown enum value from test: {test.GetType().Name}")
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<test> test, global::System.Func<T> one, global::System.Func<T> two) =>
	(await test.ConfigureAwait(false)).Match(one, two);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<test> test, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
	await (await test.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void Switch(this test test, global::System.Action one, global::System.Action two)
	{
		switch (test)
		{
			case test.one:
				one();
				break;
			case test.two:
				two();
				break;
			default:
				throw new global::System.ArgumentException($"Unknown enum value from test: {test.GetType().Name}");
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this test test, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two)
	{
		switch (test)
		{
			case test.one:
				await one().ConfigureAwait(false);
				break;
			case test.two:
				await two().ConfigureAwait(false);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown enum value from test: {test.GetType().Name}");
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<test> test, global::System.Action one, global::System.Action two) =>
	(await test.ConfigureAwait(false)).Switch(one, two);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<test> test, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two) =>
	await (await test.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
}
#pragma warning restore 1591
