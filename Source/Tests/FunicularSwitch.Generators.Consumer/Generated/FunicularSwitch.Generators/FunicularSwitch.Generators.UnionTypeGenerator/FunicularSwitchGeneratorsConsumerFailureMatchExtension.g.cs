#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class FailureMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Failure failure, global::System.Func<FunicularSwitch.Generators.Consumer.InvalidInputFailure, T> invalidInput, global::System.Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, T> notFound) =>
		failure switch
		{
			FunicularSwitch.Generators.Consumer.InvalidInputFailure invalidInput1 => invalidInput(invalidInput1),
			FunicularSwitch.Generators.Consumer.Failure.NotFound_ notFound2 => notFound(notFound2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Failure failure, global::System.Func<FunicularSwitch.Generators.Consumer.InvalidInputFailure, global::System.Threading.Tasks.Task<T>> invalidInput, global::System.Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, global::System.Threading.Tasks.Task<T>> notFound) =>
		failure switch
		{
			FunicularSwitch.Generators.Consumer.InvalidInputFailure invalidInput1 => invalidInput(invalidInput1),
			FunicularSwitch.Generators.Consumer.Failure.NotFound_ notFound2 => notFound(notFound2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Failure> failure, global::System.Func<FunicularSwitch.Generators.Consumer.InvalidInputFailure, T> invalidInput, global::System.Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, T> notFound) =>
		(await failure.ConfigureAwait(false)).Match(invalidInput, notFound);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Failure> failure, global::System.Func<FunicularSwitch.Generators.Consumer.InvalidInputFailure, global::System.Threading.Tasks.Task<T>> invalidInput, global::System.Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, global::System.Threading.Tasks.Task<T>> notFound) =>
		await (await failure.ConfigureAwait(false)).Match(invalidInput, notFound).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Failure failure, global::System.Action<FunicularSwitch.Generators.Consumer.InvalidInputFailure> invalidInput, global::System.Action<FunicularSwitch.Generators.Consumer.Failure.NotFound_> notFound)
		{
			switch (failure)
			{
				case FunicularSwitch.Generators.Consumer.InvalidInputFailure invalidInput1:
					invalidInput(invalidInput1);
					break;
				case FunicularSwitch.Generators.Consumer.Failure.NotFound_ notFound2:
					notFound(notFound2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Failure failure, global::System.Func<FunicularSwitch.Generators.Consumer.InvalidInputFailure, global::System.Threading.Tasks.Task> invalidInput, global::System.Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, global::System.Threading.Tasks.Task> notFound)
		{
			switch (failure)
			{
				case FunicularSwitch.Generators.Consumer.InvalidInputFailure invalidInput1:
					await invalidInput(invalidInput1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Failure.NotFound_ notFound2:
					await notFound(notFound2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Failure> failure, global::System.Action<FunicularSwitch.Generators.Consumer.InvalidInputFailure> invalidInput, global::System.Action<FunicularSwitch.Generators.Consumer.Failure.NotFound_> notFound) =>
		(await failure.ConfigureAwait(false)).Switch(invalidInput, notFound);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Failure> failure, global::System.Func<FunicularSwitch.Generators.Consumer.InvalidInputFailure, global::System.Threading.Tasks.Task> invalidInput, global::System.Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, global::System.Threading.Tasks.Task> notFound) =>
		await (await failure.ConfigureAwait(false)).Switch(invalidInput, notFound).ConfigureAwait(false);
	}
	
	public abstract partial record Failure
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Generators.Consumer.Failure InvalidInput(string Message) => new FunicularSwitch.Generators.Consumer.InvalidInputFailure(Message);
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Generators.Consumer.Failure NotFound(int Id) => new FunicularSwitch.Generators.Consumer.Failure.NotFound_(Id);
	}
}
#pragma warning restore 1591
