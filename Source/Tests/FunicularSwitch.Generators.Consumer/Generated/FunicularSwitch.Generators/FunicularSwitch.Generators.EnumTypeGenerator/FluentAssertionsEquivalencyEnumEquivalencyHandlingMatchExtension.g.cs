#pragma warning disable 1591
namespace FluentAssertions.Equivalency
{
	internal static partial class EnumEquivalencyHandlingMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, global::System.Func<T> byName, global::System.Func<T> byValue) =>
		enumEquivalencyHandling switch
		{
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByName => byName(),
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByValue => byValue(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, global::System.Func<global::System.Threading.Tasks.Task<T>> byName, global::System.Func<global::System.Threading.Tasks.Task<T>> byValue) =>
		enumEquivalencyHandling switch
		{
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByName => byName(),
			FluentAssertions.Equivalency.EnumEquivalencyHandling.ByValue => byValue(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, global::System.Func<T> byName, global::System.Func<T> byValue) =>
		(await enumEquivalencyHandling.ConfigureAwait(false)).Match(byName, byValue);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, global::System.Func<global::System.Threading.Tasks.Task<T>> byName, global::System.Func<global::System.Threading.Tasks.Task<T>> byValue) =>
		await (await enumEquivalencyHandling.ConfigureAwait(false)).Match(byName, byValue).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, global::System.Action byName, global::System.Action byValue)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Equivalency.EnumEquivalencyHandling enumEquivalencyHandling, global::System.Func<global::System.Threading.Tasks.Task> byName, global::System.Func<global::System.Threading.Tasks.Task> byValue)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EnumEquivalencyHandling: {enumEquivalencyHandling.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, global::System.Action byName, global::System.Action byValue) =>
		(await enumEquivalencyHandling.ConfigureAwait(false)).Switch(byName, byValue);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.EnumEquivalencyHandling> enumEquivalencyHandling, global::System.Func<global::System.Threading.Tasks.Task> byName, global::System.Func<global::System.Threading.Tasks.Task> byValue) =>
		await (await enumEquivalencyHandling.ConfigureAwait(false)).Switch(byName, byValue).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
