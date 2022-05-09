using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Failure failure, Func<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_, T> invalidInput, Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, T> notFound, Func<FunicularSwitch.Generators.Consumer.Failure.InternalError_, T> internalError) =>
		failure switch
		{
			FunicularSwitch.Generators.Consumer.Failure.InvalidInput_ case1 => invalidInput(case1),
			FunicularSwitch.Generators.Consumer.Failure.NotFound_ case2 => notFound(case2),
			FunicularSwitch.Generators.Consumer.Failure.InternalError_ case3 => internalError(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Failure failure, Func<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_, Task<T>> invalidInput, Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, Task<T>> notFound, Func<FunicularSwitch.Generators.Consumer.Failure.InternalError_, Task<T>> internalError) =>
		failure switch
		{
			FunicularSwitch.Generators.Consumer.Failure.InvalidInput_ case1 => invalidInput(case1),
			FunicularSwitch.Generators.Consumer.Failure.NotFound_ case2 => notFound(case2),
			FunicularSwitch.Generators.Consumer.Failure.InternalError_ case3 => internalError(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.Failure> failure, Func<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_, T> invalidInput, Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, T> notFound, Func<FunicularSwitch.Generators.Consumer.Failure.InternalError_, T> internalError) =>
		(await failure.ConfigureAwait(false)).Match(invalidInput, notFound, internalError);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.Failure> failure, Func<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_, Task<T>> invalidInput, Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, Task<T>> notFound, Func<FunicularSwitch.Generators.Consumer.Failure.InternalError_, Task<T>> internalError) =>
		await (await failure.ConfigureAwait(false)).Match(invalidInput, notFound, internalError).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.Failure failure, Action<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_> invalidInput, Action<FunicularSwitch.Generators.Consumer.Failure.NotFound_> notFound, Action<FunicularSwitch.Generators.Consumer.Failure.InternalError_> internalError)
		{
			switch (failure)
			{
				case FunicularSwitch.Generators.Consumer.Failure.InvalidInput_ case1:
					invalidInput(case1);
					break;
				case FunicularSwitch.Generators.Consumer.Failure.NotFound_ case2:
					notFound(case2);
					break;
				case FunicularSwitch.Generators.Consumer.Failure.InternalError_ case3:
					internalError(case3);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Generators.Consumer.Failure failure, Func<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_, Task> invalidInput, Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, Task> notFound, Func<FunicularSwitch.Generators.Consumer.Failure.InternalError_, Task> internalError)
		{
			switch (failure)
			{
				case FunicularSwitch.Generators.Consumer.Failure.InvalidInput_ case1:
					await invalidInput(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Failure.NotFound_ case2:
					await notFound(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Failure.InternalError_ case3:
					await internalError(case3).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Failure: {failure.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.Failure> failure, Action<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_> invalidInput, Action<FunicularSwitch.Generators.Consumer.Failure.NotFound_> notFound, Action<FunicularSwitch.Generators.Consumer.Failure.InternalError_> internalError) =>
		(await failure.ConfigureAwait(false)).Switch(invalidInput, notFound, internalError);
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.Failure> failure, Func<FunicularSwitch.Generators.Consumer.Failure.InvalidInput_, Task> invalidInput, Func<FunicularSwitch.Generators.Consumer.Failure.NotFound_, Task> notFound, Func<FunicularSwitch.Generators.Consumer.Failure.InternalError_, Task> internalError) =>
		await (await failure.ConfigureAwait(false)).Switch(invalidInput, notFound, internalError).ConfigureAwait(false);
	}
}
