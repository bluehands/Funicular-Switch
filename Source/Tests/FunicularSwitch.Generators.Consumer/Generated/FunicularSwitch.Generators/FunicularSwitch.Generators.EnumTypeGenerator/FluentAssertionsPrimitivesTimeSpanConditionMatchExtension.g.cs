#pragma warning disable 1591
namespace FluentAssertions.Primitives
{
	public static partial class TimeSpanConditionMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, System.Func<T> atLeast, System.Func<T> exactly, System.Func<T> lessThan, System.Func<T> moreThan, System.Func<T> within) =>
		timeSpanCondition switch
		{
			FluentAssertions.Primitives.TimeSpanCondition.AtLeast => atLeast(),
			FluentAssertions.Primitives.TimeSpanCondition.Exactly => exactly(),
			FluentAssertions.Primitives.TimeSpanCondition.LessThan => lessThan(),
			FluentAssertions.Primitives.TimeSpanCondition.MoreThan => moreThan(),
			FluentAssertions.Primitives.TimeSpanCondition.Within => within(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, System.Func<System.Threading.Tasks.Task<T>> atLeast, System.Func<System.Threading.Tasks.Task<T>> exactly, System.Func<System.Threading.Tasks.Task<T>> lessThan, System.Func<System.Threading.Tasks.Task<T>> moreThan, System.Func<System.Threading.Tasks.Task<T>> within) =>
		timeSpanCondition switch
		{
			FluentAssertions.Primitives.TimeSpanCondition.AtLeast => atLeast(),
			FluentAssertions.Primitives.TimeSpanCondition.Exactly => exactly(),
			FluentAssertions.Primitives.TimeSpanCondition.LessThan => lessThan(),
			FluentAssertions.Primitives.TimeSpanCondition.MoreThan => moreThan(),
			FluentAssertions.Primitives.TimeSpanCondition.Within => within(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, System.Func<T> atLeast, System.Func<T> exactly, System.Func<T> lessThan, System.Func<T> moreThan, System.Func<T> within) =>
		(await timeSpanCondition.ConfigureAwait(false)).Match(atLeast, exactly, lessThan, moreThan, within);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, System.Func<System.Threading.Tasks.Task<T>> atLeast, System.Func<System.Threading.Tasks.Task<T>> exactly, System.Func<System.Threading.Tasks.Task<T>> lessThan, System.Func<System.Threading.Tasks.Task<T>> moreThan, System.Func<System.Threading.Tasks.Task<T>> within) =>
		await (await timeSpanCondition.ConfigureAwait(false)).Match(atLeast, exactly, lessThan, moreThan, within).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, System.Action atLeast, System.Action exactly, System.Action lessThan, System.Action moreThan, System.Action within)
		{
			switch (timeSpanCondition)
			{
				case FluentAssertions.Primitives.TimeSpanCondition.AtLeast:
					atLeast();
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.Exactly:
					exactly();
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.LessThan:
					lessThan();
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.MoreThan:
					moreThan();
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.Within:
					within();
					break;
				default:
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, System.Func<System.Threading.Tasks.Task> atLeast, System.Func<System.Threading.Tasks.Task> exactly, System.Func<System.Threading.Tasks.Task> lessThan, System.Func<System.Threading.Tasks.Task> moreThan, System.Func<System.Threading.Tasks.Task> within)
		{
			switch (timeSpanCondition)
			{
				case FluentAssertions.Primitives.TimeSpanCondition.AtLeast:
					await atLeast().ConfigureAwait(false);
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.Exactly:
					await exactly().ConfigureAwait(false);
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.LessThan:
					await lessThan().ConfigureAwait(false);
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.MoreThan:
					await moreThan().ConfigureAwait(false);
					break;
				case FluentAssertions.Primitives.TimeSpanCondition.Within:
					await within().ConfigureAwait(false);
					break;
				default:
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, System.Action atLeast, System.Action exactly, System.Action lessThan, System.Action moreThan, System.Action within) =>
		(await timeSpanCondition.ConfigureAwait(false)).Switch(atLeast, exactly, lessThan, moreThan, within);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, System.Func<System.Threading.Tasks.Task> atLeast, System.Func<System.Threading.Tasks.Task> exactly, System.Func<System.Threading.Tasks.Task> lessThan, System.Func<System.Threading.Tasks.Task> moreThan, System.Func<System.Threading.Tasks.Task> within) =>
		await (await timeSpanCondition.ConfigureAwait(false)).Switch(atLeast, exactly, lessThan, moreThan, within).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
