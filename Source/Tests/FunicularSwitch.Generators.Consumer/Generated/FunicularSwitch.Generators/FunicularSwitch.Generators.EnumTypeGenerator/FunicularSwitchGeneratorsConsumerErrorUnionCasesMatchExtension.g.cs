#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class Error_UnionCasesMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, Func<T> generic, Func<T> notFound, Func<T> notAuthorized, Func<T> aggregated) =>
		unionCases switch
		{
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic => generic(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound => notFound(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized => notAuthorized(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated => aggregated(),
			_ => throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, Func<Task<T>> generic, Func<Task<T>> notFound, Func<Task<T>> notAuthorized, Func<Task<T>> aggregated) =>
		unionCases switch
		{
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Generic => generic(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotFound => notFound(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.NotAuthorized => notAuthorized(),
			FunicularSwitch.Generators.Consumer.Error.UnionCases.Aggregated => aggregated(),
			_ => throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, Func<T> generic, Func<T> notFound, Func<T> notAuthorized, Func<T> aggregated) =>
		(await unionCases.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, Func<Task<T>> generic, Func<Task<T>> notFound, Func<Task<T>> notAuthorized, Func<Task<T>> aggregated) =>
		await (await unionCases.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, Action generic, Action notFound, Action notAuthorized, Action aggregated)
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
					throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Generators.Consumer.Error.UnionCases unionCases, Func<Task> generic, Func<Task> notFound, Func<Task> notAuthorized, Func<Task> aggregated)
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
					throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.Error.UnionCases: {unionCases.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, Action generic, Action notFound, Action notAuthorized, Action aggregated) =>
		(await unionCases.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated);
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.Error.UnionCases> unionCases, Func<Task> generic, Func<Task> notFound, Func<Task> notAuthorized, Func<Task> aggregated) =>
		await (await unionCases.ConfigureAwait(false)).Switch(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
