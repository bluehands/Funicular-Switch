#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class GenericResultMatchExtension
	{
		public static TMatchResult Match<T, TFailure, TMatchResult>(this FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure> genericResult, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_, TMatchResult> ok, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_, TMatchResult> error) =>
		genericResult switch
		{
			FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_ ok1 => ok(ok1),
			FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_ error2 => error(error2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.GenericResult: {genericResult.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<TMatchResult> Match<T, TFailure, TMatchResult>(this FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure> genericResult, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_, global::System.Threading.Tasks.Task<TMatchResult>> ok, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_, global::System.Threading.Tasks.Task<TMatchResult>> error) =>
		genericResult switch
		{
			FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_ ok1 => ok(ok1),
			FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_ error2 => error(error2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.GenericResult: {genericResult.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TFailure, TMatchResult>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>> genericResult, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_, TMatchResult> ok, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_, TMatchResult> error) =>
		(await genericResult.ConfigureAwait(false)).Match(ok, error);
		
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TFailure, TMatchResult>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>> genericResult, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_, global::System.Threading.Tasks.Task<TMatchResult>> ok, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_, global::System.Threading.Tasks.Task<TMatchResult>> error) =>
		await (await genericResult.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);
		
		public static void Switch<T, TFailure>(this FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure> genericResult, global::System.Action<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_> ok, global::System.Action<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_> error)
		{
			switch (genericResult)
			{
				case FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_ ok1:
					ok(ok1);
					break;
				case FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_ error2:
					error(error2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.GenericResult: {genericResult.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T, TFailure>(this FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure> genericResult, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_, global::System.Threading.Tasks.Task> ok, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_, global::System.Threading.Tasks.Task> error)
		{
			switch (genericResult)
			{
				case FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_ ok1:
					await ok(ok1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_ error2:
					await error(error2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.GenericResult: {genericResult.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T, TFailure>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>> genericResult, global::System.Action<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_> ok, global::System.Action<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_> error) =>
		(await genericResult.ConfigureAwait(false)).Switch(ok, error);
		
		public static async global::System.Threading.Tasks.Task Switch<T, TFailure>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>> genericResult, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_, global::System.Threading.Tasks.Task> ok, global::System.Func<FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_, global::System.Threading.Tasks.Task> error) =>
		await (await genericResult.ConfigureAwait(false)).Switch(ok, error).ConfigureAwait(false);
	}
	
	public abstract partial record GenericResult<T, TFailure>
	{
		public static FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure> Ok(T Value) => new FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Ok_(Value);
		public static FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure> Error(TFailure Failure) => new FunicularSwitch.Generators.Consumer.GenericResult<T, TFailure>.Error_(Failure);
	}
}
#pragma warning restore 1591
