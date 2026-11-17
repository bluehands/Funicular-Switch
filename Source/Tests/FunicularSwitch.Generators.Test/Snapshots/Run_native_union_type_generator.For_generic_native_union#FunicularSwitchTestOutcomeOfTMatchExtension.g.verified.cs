//HintName: FunicularSwitchTestOutcomeOfTMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class OutcomeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static TMatchResult Match<T, TMatchResult>(this global::FunicularSwitch.Test.Outcome<T> outcome, global::System.Func<T, TMatchResult> t, global::System.Func<global::FunicularSwitch.Test.Failure, TMatchResult> failure) where T : class =>
		outcome switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			T t1 => t(t1),
			global::FunicularSwitch.Test.Failure failure2 => failure(failure2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::FunicularSwitch.Test.Outcome<T> outcome, global::System.Func<T, global::System.Threading.Tasks.Task<TMatchResult>> t, global::System.Func<global::FunicularSwitch.Test.Failure, global::System.Threading.Tasks.Task<TMatchResult>> failure) where T : class =>
		outcome switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			T t1 => await t(t1).ConfigureAwait(false),
			global::FunicularSwitch.Test.Failure failure2 => await failure(failure2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outcome<T>> outcome, global::System.Func<T, TMatchResult> t, global::System.Func<global::FunicularSwitch.Test.Failure, TMatchResult> failure) where T : class =>
		(await outcome.ConfigureAwait(false)).Match(t, failure);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outcome<T>> outcome, global::System.Func<T, global::System.Threading.Tasks.Task<TMatchResult>> t, global::System.Func<global::FunicularSwitch.Test.Failure, global::System.Threading.Tasks.Task<TMatchResult>> failure) where T : class =>
		await (await outcome.ConfigureAwait(false)).Match(t, failure).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<T>(this global::FunicularSwitch.Test.Outcome<T> outcome, global::System.Action<T> t, global::System.Action<global::FunicularSwitch.Test.Failure> failure) where T : class
		{
			switch (outcome)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case T t1:
					t(t1);
					break;
				case global::FunicularSwitch.Test.Failure failure2:
					failure(failure2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::FunicularSwitch.Test.Outcome<T> outcome, global::System.Func<T, global::System.Threading.Tasks.Task> t, global::System.Func<global::FunicularSwitch.Test.Failure, global::System.Threading.Tasks.Task> failure) where T : class
		{
			switch (outcome)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case T t1:
					await t(t1).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Test.Failure failure2:
					await failure(failure2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outcome<T>> outcome, global::System.Action<T> t, global::System.Action<global::FunicularSwitch.Test.Failure> failure) where T : class =>
		(await outcome.ConfigureAwait(false)).Switch(t, failure);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outcome<T>> outcome, global::System.Func<T, global::System.Threading.Tasks.Task> t, global::System.Func<global::FunicularSwitch.Test.Failure, global::System.Threading.Tasks.Task> failure) where T : class =>
		await (await outcome.ConfigureAwait(false)).Switch(t, failure).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
