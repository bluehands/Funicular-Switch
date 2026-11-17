//HintName: FunicularSwitchTestEitherOfTLeft_TRightMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class EitherMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<TLeft, TRight, T>(this global::FunicularSwitch.Test.Either<TLeft, TRight> either, global::System.Func<TLeft, T> tLeft, global::System.Func<TRight, T> tRight) where TLeft : class where TRight : class =>
		either switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			TLeft tLeft1 => tLeft(tLeft1),
			TRight tRight2 => tRight(tRight2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<TLeft, TRight, T>(this global::FunicularSwitch.Test.Either<TLeft, TRight> either, global::System.Func<TLeft, global::System.Threading.Tasks.Task<T>> tLeft, global::System.Func<TRight, global::System.Threading.Tasks.Task<T>> tRight) where TLeft : class where TRight : class =>
		either switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			TLeft tLeft1 => await tLeft(tLeft1).ConfigureAwait(false),
			TRight tRight2 => await tRight(tRight2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<TLeft, TRight, T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Either<TLeft, TRight>> either, global::System.Func<TLeft, T> tLeft, global::System.Func<TRight, T> tRight) where TLeft : class where TRight : class =>
		(await either.ConfigureAwait(false)).Match(tLeft, tRight);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<TLeft, TRight, T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Either<TLeft, TRight>> either, global::System.Func<TLeft, global::System.Threading.Tasks.Task<T>> tLeft, global::System.Func<TRight, global::System.Threading.Tasks.Task<T>> tRight) where TLeft : class where TRight : class =>
		await (await either.ConfigureAwait(false)).Match(tLeft, tRight).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<TLeft, TRight>(this global::FunicularSwitch.Test.Either<TLeft, TRight> either, global::System.Action<TLeft> tLeft, global::System.Action<TRight> tRight) where TLeft : class where TRight : class
		{
			switch (either)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case TLeft tLeft1:
					tLeft(tLeft1);
					break;
				case TRight tRight2:
					tRight(tRight2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<TLeft, TRight>(this global::FunicularSwitch.Test.Either<TLeft, TRight> either, global::System.Func<TLeft, global::System.Threading.Tasks.Task> tLeft, global::System.Func<TRight, global::System.Threading.Tasks.Task> tRight) where TLeft : class where TRight : class
		{
			switch (either)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case TLeft tLeft1:
					await tLeft(tLeft1).ConfigureAwait(false);
					break;
				case TRight tRight2:
					await tRight(tRight2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<TLeft, TRight>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Either<TLeft, TRight>> either, global::System.Action<TLeft> tLeft, global::System.Action<TRight> tRight) where TLeft : class where TRight : class =>
		(await either.ConfigureAwait(false)).Switch(tLeft, tRight);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<TLeft, TRight>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Either<TLeft, TRight>> either, global::System.Func<TLeft, global::System.Threading.Tasks.Task> tLeft, global::System.Func<TRight, global::System.Threading.Tasks.Task> tRight) where TLeft : class where TRight : class =>
		await (await either.ConfigureAwait(false)).Switch(tLeft, tRight).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
