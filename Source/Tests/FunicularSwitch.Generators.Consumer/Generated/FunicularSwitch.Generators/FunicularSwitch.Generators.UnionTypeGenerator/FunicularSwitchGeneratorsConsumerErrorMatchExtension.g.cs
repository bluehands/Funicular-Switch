#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class ErrorMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Error error, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Generic_, T> generic, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, T> notFound, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, T> notAuthorized, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, T> aggregated) =>
		error switch
		{
			FunicularSwitch.Generators.Consumer.Error.Generic_ generic1 => generic(generic1),
			FunicularSwitch.Generators.Consumer.Error.NotFound_ notFound2 => notFound(notFound2),
			FunicularSwitch.Generators.Consumer.Error.NotAuthorized_ notAuthorized3 => notAuthorized(notAuthorized3),
			FunicularSwitch.Generators.Consumer.Error.Aggregated_ aggregated4 => aggregated(aggregated4),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Error: {error.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Error error, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Generic_, global::System.Threading.Tasks.Task<T>> generic, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, global::System.Threading.Tasks.Task<T>> notFound, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, global::System.Threading.Tasks.Task<T>> notAuthorized, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, global::System.Threading.Tasks.Task<T>> aggregated) =>
		error switch
		{
			FunicularSwitch.Generators.Consumer.Error.Generic_ generic1 => generic(generic1),
			FunicularSwitch.Generators.Consumer.Error.NotFound_ notFound2 => notFound(notFound2),
			FunicularSwitch.Generators.Consumer.Error.NotAuthorized_ notAuthorized3 => notAuthorized(notAuthorized3),
			FunicularSwitch.Generators.Consumer.Error.Aggregated_ aggregated4 => aggregated(aggregated4),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Error: {error.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error> error, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Generic_, T> generic, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, T> notFound, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, T> notAuthorized, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, T> aggregated) =>
		(await error.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error> error, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Generic_, global::System.Threading.Tasks.Task<T>> generic, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, global::System.Threading.Tasks.Task<T>> notFound, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, global::System.Threading.Tasks.Task<T>> notAuthorized, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, global::System.Threading.Tasks.Task<T>> aggregated) =>
		await (await error.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.Error error, global::System.Action<FunicularSwitch.Generators.Consumer.Error.Generic_> generic, global::System.Action<FunicularSwitch.Generators.Consumer.Error.NotFound_> notFound, global::System.Action<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_> notAuthorized, global::System.Action<FunicularSwitch.Generators.Consumer.Error.Aggregated_> aggregated)
		{
			switch (error)
			{
				case FunicularSwitch.Generators.Consumer.Error.Generic_ generic1:
					generic(generic1);
					break;
				case FunicularSwitch.Generators.Consumer.Error.NotFound_ notFound2:
					notFound(notFound2);
					break;
				case FunicularSwitch.Generators.Consumer.Error.NotAuthorized_ notAuthorized3:
					notAuthorized(notAuthorized3);
					break;
				case FunicularSwitch.Generators.Consumer.Error.Aggregated_ aggregated4:
					aggregated(aggregated4);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Error: {error.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.Error error, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Generic_, global::System.Threading.Tasks.Task> generic, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, global::System.Threading.Tasks.Task> notFound, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, global::System.Threading.Tasks.Task> notAuthorized, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, global::System.Threading.Tasks.Task> aggregated)
		{
			switch (error)
			{
				case FunicularSwitch.Generators.Consumer.Error.Generic_ generic1:
					await generic(generic1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Error.NotFound_ notFound2:
					await notFound(notFound2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Error.NotAuthorized_ notAuthorized3:
					await notAuthorized(notAuthorized3).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Error.Aggregated_ aggregated4:
					await aggregated(aggregated4).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Error: {error.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error> error, global::System.Action<FunicularSwitch.Generators.Consumer.Error.Generic_> generic, global::System.Action<FunicularSwitch.Generators.Consumer.Error.NotFound_> notFound, global::System.Action<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_> notAuthorized, global::System.Action<FunicularSwitch.Generators.Consumer.Error.Aggregated_> aggregated) =>
		(await error.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error> error, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Generic_, global::System.Threading.Tasks.Task> generic, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotFound_, global::System.Threading.Tasks.Task> notFound, global::System.Func<FunicularSwitch.Generators.Consumer.Error.NotAuthorized_, global::System.Threading.Tasks.Task> notAuthorized, global::System.Func<FunicularSwitch.Generators.Consumer.Error.Aggregated_, global::System.Threading.Tasks.Task> aggregated) =>
		await (await error.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
	}
	
	public abstract partial class Error
	{
		public static FunicularSwitch.Generators.Consumer.Error Generic(string message) => new FunicularSwitch.Generators.Consumer.Error.Generic_(message);
		public static FunicularSwitch.Generators.Consumer.Error NotFound() => new FunicularSwitch.Generators.Consumer.Error.NotFound_();
		public static FunicularSwitch.Generators.Consumer.Error NotAuthorized() => new FunicularSwitch.Generators.Consumer.Error.NotAuthorized_();
		public static FunicularSwitch.Generators.Consumer.Error Aggregated(global::System.Collections.Immutable.ImmutableList<global::FunicularSwitch.Generators.Consumer.Error> errors) => new FunicularSwitch.Generators.Consumer.Error.Aggregated_(errors);
	}
}
#pragma warning restore 1591
