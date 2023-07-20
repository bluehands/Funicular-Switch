#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Equivalency
{
	public static partial class EquivalencyResultMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, Func<T> assertionCompleted, Func<T> continueWithNext) =>
		equivalencyResult switch
		{
			FluentAssertions.Equivalency.EquivalencyResult.AssertionCompleted => assertionCompleted(),
			FluentAssertions.Equivalency.EquivalencyResult.ContinueWithNext => continueWithNext(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, Func<Task<T>> assertionCompleted, Func<Task<T>> continueWithNext) =>
		equivalencyResult switch
		{
			FluentAssertions.Equivalency.EquivalencyResult.AssertionCompleted => assertionCompleted(),
			FluentAssertions.Equivalency.EquivalencyResult.ContinueWithNext => continueWithNext(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, Func<T> assertionCompleted, Func<T> continueWithNext) =>
		(await equivalencyResult.ConfigureAwait(false)).Match(assertionCompleted, continueWithNext);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, Func<Task<T>> assertionCompleted, Func<Task<T>> continueWithNext) =>
		await (await equivalencyResult.ConfigureAwait(false)).Match(assertionCompleted, continueWithNext).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, Action assertionCompleted, Action continueWithNext)
		{
			switch (equivalencyResult)
			{
				case FluentAssertions.Equivalency.EquivalencyResult.AssertionCompleted:
					assertionCompleted();
					break;
				case FluentAssertions.Equivalency.EquivalencyResult.ContinueWithNext:
					continueWithNext();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, Func<Task> assertionCompleted, Func<Task> continueWithNext)
		{
			switch (equivalencyResult)
			{
				case FluentAssertions.Equivalency.EquivalencyResult.AssertionCompleted:
					await assertionCompleted().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.EquivalencyResult.ContinueWithNext:
					await continueWithNext().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, Action assertionCompleted, Action continueWithNext) =>
		(await equivalencyResult.ConfigureAwait(false)).Switch(assertionCompleted, continueWithNext);
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, Func<Task> assertionCompleted, Func<Task> continueWithNext) =>
		await (await equivalencyResult.ConfigureAwait(false)).Switch(assertionCompleted, continueWithNext).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
