#pragma warning disable 1591
namespace FluentAssertions.Equivalency
{
	public static partial class EquivalencyResultMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, System.Func<T> assertionCompleted, System.Func<T> continueWithNext) =>
		equivalencyResult switch
		{
			FluentAssertions.Equivalency.EquivalencyResult.AssertionCompleted => assertionCompleted(),
			FluentAssertions.Equivalency.EquivalencyResult.ContinueWithNext => continueWithNext(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, System.Func<System.Threading.Tasks.Task<T>> assertionCompleted, System.Func<System.Threading.Tasks.Task<T>> continueWithNext) =>
		equivalencyResult switch
		{
			FluentAssertions.Equivalency.EquivalencyResult.AssertionCompleted => assertionCompleted(),
			FluentAssertions.Equivalency.EquivalencyResult.ContinueWithNext => continueWithNext(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, System.Func<T> assertionCompleted, System.Func<T> continueWithNext) =>
		(await equivalencyResult.ConfigureAwait(false)).Match(assertionCompleted, continueWithNext);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, System.Func<System.Threading.Tasks.Task<T>> assertionCompleted, System.Func<System.Threading.Tasks.Task<T>> continueWithNext) =>
		await (await equivalencyResult.ConfigureAwait(false)).Match(assertionCompleted, continueWithNext).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, System.Action assertionCompleted, System.Action continueWithNext)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FluentAssertions.Equivalency.EquivalencyResult equivalencyResult, System.Func<System.Threading.Tasks.Task> assertionCompleted, System.Func<System.Threading.Tasks.Task> continueWithNext)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EquivalencyResult: {equivalencyResult.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, System.Action assertionCompleted, System.Action continueWithNext) =>
		(await equivalencyResult.ConfigureAwait(false)).Switch(assertionCompleted, continueWithNext);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EquivalencyResult> equivalencyResult, System.Func<System.Threading.Tasks.Task> assertionCompleted, System.Func<System.Threading.Tasks.Task> continueWithNext) =>
		await (await equivalencyResult.ConfigureAwait(false)).Switch(assertionCompleted, continueWithNext).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
