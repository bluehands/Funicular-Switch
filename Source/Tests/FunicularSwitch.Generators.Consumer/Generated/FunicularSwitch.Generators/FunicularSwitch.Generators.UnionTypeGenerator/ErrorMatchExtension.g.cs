using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Error error, Func<FunicularSwitch.Generators.Consumer.Error.Generic_, T> generic, Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, T> notFound, Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, T> notAuthorized, Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, T> aggregated) =>
		error switch
		{
			FunicularSwitch.Generators.Consumer.Error.Generic_ case1 => generic(case1),
			FunicularSwitch.Generators.Consumer.Error.NotFound_ case2 => notFound(case2),
			FunicularSwitch.Generators.Consumer.Error.NotAuthorized_ case3 => notAuthorized(case3),
			FunicularSwitch.Generators.Consumer.Error.Aggregated_ case4 => aggregated(case4),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Error: {error.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Error error, Func<FunicularSwitch.Generators.Consumer.Error.Generic_, Task<T>> generic, Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, Task<T>> notFound, Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, Task<T>> notAuthorized, Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, Task<T>> aggregated) =>
		error switch
		{
			FunicularSwitch.Generators.Consumer.Error.Generic_ case1 => generic(case1),
			FunicularSwitch.Generators.Consumer.Error.NotFound_ case2 => notFound(case2),
			FunicularSwitch.Generators.Consumer.Error.NotAuthorized_ case3 => notAuthorized(case3),
			FunicularSwitch.Generators.Consumer.Error.Aggregated_ case4 => aggregated(case4),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Error: {error.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.Error> error, Func<FunicularSwitch.Generators.Consumer.Error.Generic_, T> generic, Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, T> notFound, Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, T> notAuthorized, Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, T> aggregated) =>
		(await error.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.Error> error, Func<FunicularSwitch.Generators.Consumer.Error.Generic_, Task<T>> generic, Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, Task<T>> notFound, Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, Task<T>> notAuthorized, Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, Task<T>> aggregated) =>
		await (await error.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
	}
}
