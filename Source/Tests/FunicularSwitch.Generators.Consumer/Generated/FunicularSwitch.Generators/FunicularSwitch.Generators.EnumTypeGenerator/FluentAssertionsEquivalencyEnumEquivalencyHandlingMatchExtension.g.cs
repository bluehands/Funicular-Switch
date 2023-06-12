#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Equivalency
{
	public static partial class EnumEquivalencyHandlingMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, Func<T> byName, Func<T> byValue) =>
		enumEquivalencyHandling switch
		{
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByName => byName(),
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByValue => byValue(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, Func<Task<T>> byName, Func<Task<T>> byValue) =>
		enumEquivalencyHandling switch
		{
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByName => byName(),
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByValue => byValue(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, Func<T> byName, Func<T> byValue) =>
		(await enumEquivalencyHandling.ConfigureAwait(false)).Match(byName, byValue);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, Func<Task<T>> byName, Func<Task<T>> byValue) =>
		await (await enumEquivalencyHandling.ConfigureAwait(false)).Match(byName, byValue).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, Action byName, Action byValue)
		{
			switch (enumEquivalencyHandling)
			{
				case FluentAssertions.Equivalency.EnumEquivalencyHandling.ByName:
					byName();
					break;
				case FluentAssertions.Equivalency.EnumEquivalencyHandling.ByValue:
					byValue();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, Func<Task> byName, Func<Task> byValue)
		{
			switch (enumEquivalencyHandling)
			{
				case FluentAssertions.Equivalency.EnumEquivalencyHandling.ByName:
					await byName().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.EnumEquivalencyHandling.ByValue:
					await byValue().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, Action byName, Action byValue) =>
		(await enumEquivalencyHandling.ConfigureAwait(false)).Switch(byName, byValue);
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, Func<Task> byName, Func<Task> byValue) =>
		await (await enumEquivalencyHandling.ConfigureAwait(false)).Switch(byName, byValue).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
