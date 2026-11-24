//HintName: UnitTestPairOfTFirst_TSecondMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace Unit.Test
{
	public static partial class PairMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<TFirst, TSecond, T>(this global::Unit.Test.Pair<TFirst, TSecond> pair, global::System.Func<global::Unit.Test.Value<TSecond, TFirst>, T> value) where TFirst : class
 where TSecond : struct =>
		pair switch
		{
			null => throw new global::System.ArgumentNullException(nameof(pair)),
			global::Unit.Test.Value<TSecond, TFirst> value1 => value(value1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Pair: {pair.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<TFirst, TSecond, T>(this global::Unit.Test.Pair<TFirst, TSecond> pair, global::System.Func<global::Unit.Test.Value<TSecond, TFirst>, global::System.Threading.Tasks.Task<T>> value) where TFirst : class
 where TSecond : struct =>
		pair switch
		{
			null => throw new global::System.ArgumentNullException(nameof(pair)),
			global::Unit.Test.Value<TSecond, TFirst> value1 => await value(value1).ConfigureAwait(false),
			_ => throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Pair: {pair.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<TFirst, TSecond, T>(this global::System.Threading.Tasks.Task<global::Unit.Test.Pair<TFirst, TSecond>> pair, global::System.Func<global::Unit.Test.Value<TSecond, TFirst>, T> value) where TFirst : class
 where TSecond : struct =>
		(await pair.ConfigureAwait(false)).Match(value);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<TFirst, TSecond, T>(this global::System.Threading.Tasks.Task<global::Unit.Test.Pair<TFirst, TSecond>> pair, global::System.Func<global::Unit.Test.Value<TSecond, TFirst>, global::System.Threading.Tasks.Task<T>> value) where TFirst : class
 where TSecond : struct =>
		await (await pair.ConfigureAwait(false)).Match(value).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<TFirst, TSecond>(this global::Unit.Test.Pair<TFirst, TSecond> pair, global::System.Action<global::Unit.Test.Value<TSecond, TFirst>> value) where TFirst : class
 where TSecond : struct
		{
			switch (pair)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(pair));
				case global::Unit.Test.Value<TSecond, TFirst> value1:
					value(value1);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Pair: {pair.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<TFirst, TSecond>(this global::Unit.Test.Pair<TFirst, TSecond> pair, global::System.Func<global::Unit.Test.Value<TSecond, TFirst>, global::System.Threading.Tasks.Task> value) where TFirst : class
 where TSecond : struct
		{
			switch (pair)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(pair));
				case global::Unit.Test.Value<TSecond, TFirst> value1:
					await value(value1).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Pair: {pair.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<TFirst, TSecond>(this global::System.Threading.Tasks.Task<global::Unit.Test.Pair<TFirst, TSecond>> pair, global::System.Action<global::Unit.Test.Value<TSecond, TFirst>> value) where TFirst : class
 where TSecond : struct =>
		(await pair.ConfigureAwait(false)).Switch(value);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<TFirst, TSecond>(this global::System.Threading.Tasks.Task<global::Unit.Test.Pair<TFirst, TSecond>> pair, global::System.Func<global::Unit.Test.Value<TSecond, TFirst>, global::System.Threading.Tasks.Task> value) where TFirst : class
 where TSecond : struct =>
		await (await pair.ConfigureAwait(false)).Switch(value).ConfigureAwait(false);
	}
	
	public abstract partial record Pair<TFirst, TSecond>
		where TFirst : class

		where TSecond : struct
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static Unit.Test.Pair<TFirst, TSecond> Value(TFirst Key, TSecond? Item, (TFirst Key, TSecond[] Items) details) => new global::Unit.Test.Value<TSecond, TFirst>(Key, Item)		
		{
			Details = details			
		};
	}
}
#pragma warning restore 1591
