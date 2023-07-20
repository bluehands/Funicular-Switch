#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Equivalency
{
	public static partial class CyclicReferenceHandlingMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, Func<T> ignore, Func<T> throwException) =>
		cyclicReferenceHandling switch
		{
			FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore => ignore(),
			FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException => throwException(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, Func<Task<T>> ignore, Func<Task<T>> throwException) =>
		cyclicReferenceHandling switch
		{
			FluentAssertions.Equivalency.CyclicReferenceHandling.Ignore => ignore(),
			FluentAssertions.Equivalency.CyclicReferenceHandling.ThrowException => throwException(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, Func<T> ignore, Func<T> throwException) =>
		(await cyclicReferenceHandling.ConfigureAwait(false)).Match(ignore, throwException);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, Func<Task<T>> ignore, Func<Task<T>> throwException) =>
		await (await cyclicReferenceHandling.ConfigureAwait(false)).Match(ignore, throwException).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, Action ignore, Action throwException)
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
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Equivalency.CyclicReferenceHandling cyclicReferenceHandling, Func<Task> ignore, Func<Task> throwException)
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
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.CyclicReferenceHandling: {cyclicReferenceHandling.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, Action ignore, Action throwException) =>
		(await cyclicReferenceHandling.ConfigureAwait(false)).Switch(ignore, throwException);
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.CyclicReferenceHandling> cyclicReferenceHandling, Func<Task> ignore, Func<Task> throwException) =>
		await (await cyclicReferenceHandling.ConfigureAwait(false)).Switch(ignore, throwException).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
