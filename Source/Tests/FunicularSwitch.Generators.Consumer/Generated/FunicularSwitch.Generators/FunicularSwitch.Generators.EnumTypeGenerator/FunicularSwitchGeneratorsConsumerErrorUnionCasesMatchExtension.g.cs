#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class Error_UnionCasesMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, global::System.Func<T> generic, global::System.Func<T> notFound, global::System.Func<T> notAuthorized, global::System.Func<T> aggregated) =>
		unionCases switch
		{
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic => generic(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound => notFound(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized => notAuthorized(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated => aggregated(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, global::System.Func<global::System.Threading.Tasks.Task<T>> generic, global::System.Func<global::System.Threading.Tasks.Task<T>> notFound, global::System.Func<global::System.Threading.Tasks.Task<T>> notAuthorized, global::System.Func<global::System.Threading.Tasks.Task<T>> aggregated) =>
		unionCases switch
		{
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic => generic(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound => notFound(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized => notAuthorized(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated => aggregated(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, global::System.Func<T> generic, global::System.Func<T> notFound, global::System.Func<T> notAuthorized, global::System.Func<T> aggregated) =>
		(await unionCases.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, global::System.Func<global::System.Threading.Tasks.Task<T>> generic, global::System.Func<global::System.Threading.Tasks.Task<T>> notFound, global::System.Func<global::System.Threading.Tasks.Task<T>> notAuthorized, global::System.Func<global::System.Threading.Tasks.Task<T>> aggregated) =>
		await (await unionCases.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, global::System.Action generic, global::System.Action notFound, global::System.Action notAuthorized, global::System.Action aggregated)
		{
			switch (unionCases)
			{
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic:
					generic();
					break;
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound:
					notFound();
					break;
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized:
					notAuthorized();
					break;
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated:
					aggregated();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, global::System.Func<global::System.Threading.Tasks.Task> generic, global::System.Func<global::System.Threading.Tasks.Task> notFound, global::System.Func<global::System.Threading.Tasks.Task> notAuthorized, global::System.Func<global::System.Threading.Tasks.Task> aggregated)
		{
			switch (unionCases)
			{
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic:
					await generic().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound:
					await notFound().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized:
					await notAuthorized().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated:
					await aggregated().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, global::System.Action generic, global::System.Action notFound, global::System.Action notAuthorized, global::System.Action aggregated) =>
		(await unionCases.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, global::System.Func<global::System.Threading.Tasks.Task> generic, global::System.Func<global::System.Threading.Tasks.Task> notFound, global::System.Func<global::System.Threading.Tasks.Task> notAuthorized, global::System.Func<global::System.Threading.Tasks.Task> aggregated) =>
		await (await unionCases.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
