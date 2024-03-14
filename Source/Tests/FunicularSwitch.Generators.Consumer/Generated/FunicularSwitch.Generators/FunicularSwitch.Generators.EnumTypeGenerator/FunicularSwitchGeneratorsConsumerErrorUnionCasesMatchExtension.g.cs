#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class Error_UnionCasesMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, System.Func<T> generic, System.Func<T> notFound, System.Func<T> notAuthorized, System.Func<T> aggregated) =>
		unionCases switch
		{
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic => generic(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound => notFound(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized => notAuthorized(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated => aggregated(),
			_ => throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, System.Func<System.Threading.Tasks.Task<T>> generic, System.Func<System.Threading.Tasks.Task<T>> notFound, System.Func<System.Threading.Tasks.Task<T>> notAuthorized, System.Func<System.Threading.Tasks.Task<T>> aggregated) =>
		unionCases switch
		{
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic => generic(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound => notFound(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized => notAuthorized(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated => aggregated(),
			_ => throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, System.Func<T> generic, System.Func<T> notFound, System.Func<T> notAuthorized, System.Func<T> aggregated) =>
		(await unionCases.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, System.Func<System.Threading.Tasks.Task<T>> generic, System.Func<System.Threading.Tasks.Task<T>> notFound, System.Func<System.Threading.Tasks.Task<T>> notAuthorized, System.Func<System.Threading.Tasks.Task<T>> aggregated) =>
		await (await unionCases.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, System.Action generic, System.Action notFound, System.Action notAuthorized, System.Action aggregated)
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
					throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, System.Func<System.Threading.Tasks.Task> generic, System.Func<System.Threading.Tasks.Task> notFound, System.Func<System.Threading.Tasks.Task> notAuthorized, System.Func<System.Threading.Tasks.Task> aggregated)
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
					throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, System.Action generic, System.Action notFound, System.Action notAuthorized, System.Action aggregated) =>
		(await unionCases.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, System.Func<System.Threading.Tasks.Task> generic, System.Func<System.Threading.Tasks.Task> notFound, System.Func<System.Threading.Tasks.Task> notAuthorized, System.Func<System.Threading.Tasks.Task> aggregated) =>
		await (await unionCases.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
