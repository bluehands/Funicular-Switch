//HintName: BaseMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
public static partial class BaseMatchExtension
{
	[global::System.Diagnostics.DebuggerStepThrough]
	public static T Match<T>(this global::Base @base, global::System.Func<One, T> one, global::System.Func<Two, T> two) =>
	@base switch
	{
		null => throw new global::System.ArgumentNullException(nameof(@base)),
		One one1 => one(one1),
		Two two2 => two(two2),
		_ => throw new global::System.ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}")
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::Base @base, global::System.Func<One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<Two, global::System.Threading.Tasks.Task<T>> two) =>
	@base switch
	{
		null => throw new global::System.ArgumentNullException(nameof(@base)),
		One one1 => await one(one1).ConfigureAwait(false),
		Two two2 => await two(two2).ConfigureAwait(false),
		_ => throw new global::System.ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}")
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::Base> @base, global::System.Func<One, T> one, global::System.Func<Two, T> two) =>
	(await @base.ConfigureAwait(false)).Match(one, two);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::Base> @base, global::System.Func<One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<Two, global::System.Threading.Tasks.Task<T>> two) =>
	await (await @base.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void Switch(this global::Base @base, global::System.Action<One> one, global::System.Action<Two> two)
	{
		switch (@base)
		{
			case null:
				throw new global::System.ArgumentNullException(nameof(@base));
			case One one1:
				one(one1);
				break;
			case Two two2:
				two(two2);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}");
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::Base @base, global::System.Func<One, global::System.Threading.Tasks.Task> one, global::System.Func<Two, global::System.Threading.Tasks.Task> two)
	{
		switch (@base)
		{
			case null:
				throw new global::System.ArgumentNullException(nameof(@base));
			case One one1:
				await one(one1).ConfigureAwait(false);
				break;
			case Two two2:
				await two(two2).ConfigureAwait(false);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown type derived from Base: {@base.GetType().Name}");
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::Base> @base, global::System.Action<One> one, global::System.Action<Two> two) =>
	(await @base.ConfigureAwait(false)).Switch(one, two);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::Base> @base, global::System.Func<One, global::System.Threading.Tasks.Task> one, global::System.Func<Two, global::System.Threading.Tasks.Task> two) =>
	await (await @base.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
}

public abstract partial record Base
{
	[global::System.Diagnostics.DebuggerStepThrough]
	public static Base One() => new One();
	[global::System.Diagnostics.DebuggerStepThrough]
	public static Base Two(int Value) => new Two(Value);
}
#pragma warning restore 1591
