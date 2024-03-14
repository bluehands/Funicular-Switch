#pragma warning disable 1591
namespace FluentAssertions.Equivalency
{
	public static partial class CyclicReferenceHandlingMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, System.Func<T> ignore, System.Func<T> throwException) =>
		cyclicReferenceHandling switch
		{
			FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore => ignore(),
			FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException => throwException(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, System.Func<System.Threading.Tasks.Task<T>> ignore, System.Func<System.Threading.Tasks.Task<T>> throwException) =>
		cyclicReferenceHandling switch
		{
			FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore => ignore(),
			FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException => throwException(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, System.Func<T> ignore, System.Func<T> throwException) =>
		(await cyclicReferenceHandling.ConfigureAwait(false)).Match(ignore, throwException);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, System.Func<System.Threading.Tasks.Task<T>> ignore, System.Func<System.Threading.Tasks.Task<T>> throwException) =>
		await (await cyclicReferenceHandling.ConfigureAwait(false)).Match(ignore, throwException).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, System.Action ignore, System.Action throwException)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, System.Func<System.Threading.Tasks.Task> ignore, System.Func<System.Threading.Tasks.Task> throwException)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, System.Action ignore, System.Action throwException) =>
		(await cyclicReferenceHandling.ConfigureAwait(false)).Switch(ignore, throwException);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, System.Func<System.Threading.Tasks.Task> ignore, System.Func<System.Threading.Tasks.Task> throwException) =>
		await (await cyclicReferenceHandling.ConfigureAwait(false)).Switch(ignore, throwException).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
