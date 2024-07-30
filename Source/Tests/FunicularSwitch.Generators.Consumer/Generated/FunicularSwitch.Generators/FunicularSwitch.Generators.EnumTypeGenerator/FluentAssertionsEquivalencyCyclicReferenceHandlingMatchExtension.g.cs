#pragma warning disable 1591
namespace FluentAssertions.Equivalency
{
	internal static partial class CyclicReferenceHandlingMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, global::System.Func<T> ignore, global::System.Func<T> throwException) =>
		cyclicReferenceHandling switch
		{
			FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore => ignore(),
			FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException => throwException(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, global::System.Func<global::System.Threading.Tasks.Task<T>> ignore, global::System.Func<global::System.Threading.Tasks.Task<T>> throwException) =>
		cyclicReferenceHandling switch
		{
			FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore => ignore(),
			FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException => throwException(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, global::System.Func<T> ignore, global::System.Func<T> throwException) =>
		(await cyclicReferenceHandling.ConfigureAwait(false)).Match(ignore, throwException);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, global::System.Func<global::System.Threading.Tasks.Task<T>> ignore, global::System.Func<global::System.Threading.Tasks.Task<T>> throwException) =>
		await (await cyclicReferenceHandling.ConfigureAwait(false)).Match(ignore, throwException).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, global::System.Action ignore, global::System.Action throwException)
		{
			switch (cyclicReferenceHandling)
			{
				case FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore:
					ignore();
					break;
				case FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException:
					throwException();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, global::System.Func<global::System.Threading.Tasks.Task> ignore, global::System.Func<global::System.Threading.Tasks.Task> throwException)
		{
			switch (cyclicReferenceHandling)
			{
				case FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore:
					await ignore().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException:
					await throwException().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, global::System.Action ignore, global::System.Action throwException) =>
		(await cyclicReferenceHandling.ConfigureAwait(false)).Switch(ignore, throwException);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, global::System.Func<global::System.Threading.Tasks.Task> ignore, global::System.Func<global::System.Threading.Tasks.Task> throwException) =>
		await (await cyclicReferenceHandling.ConfigureAwait(false)).Switch(ignore, throwException).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
