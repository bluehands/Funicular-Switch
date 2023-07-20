#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Primitives
{
	public static partial class TimeSpanConditionMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, Func<T> atLeast, Func<T> exactly, Func<T> lessThan, Func<T> moreThan, Func<T> within) =>
		timeSpanCondition switch
		{
			FluentAssertions.Primitives.TimeSpanCondition.AtLeast => atLeast(),
			FluentAssertions.Primitives.TimeSpanCondition.Exactly => exactly(),
			FluentAssertions.Primitives.TimeSpanCondition.LessThan => lessThan(),
			FluentAssertions.Primitives.TimeSpanCondition.MoreThan => moreThan(),
			FluentAssertions.Primitives.TimeSpanCondition.Within => within(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, Func<Task<T>> atLeast, Func<Task<T>> exactly, Func<Task<T>> lessThan, Func<Task<T>> moreThan, Func<Task<T>> within) =>
		timeSpanCondition switch
		{
			FluentAssertions.Primitives.TimeSpanCondition.AtLeast => atLeast(),
			FluentAssertions.Primitives.TimeSpanCondition.Exactly => exactly(),
			FluentAssertions.Primitives.TimeSpanCondition.LessThan => lessThan(),
			FluentAssertions.Primitives.TimeSpanCondition.MoreThan => moreThan(),
			FluentAssertions.Primitives.TimeSpanCondition.Within => within(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, Func<T> atLeast, Func<T> exactly, Func<T> lessThan, Func<T> moreThan, Func<T> within) =>
		(await timeSpanCondition.ConfigureAwait(false)).Match(atLeast, exactly, lessThan, moreThan, within);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, Func<Task<T>> atLeast, Func<Task<T>> exactly, Func<Task<T>> lessThan, Func<Task<T>> moreThan, Func<Task<T>> within) =>
		await (await timeSpanCondition.ConfigureAwait(false)).Match(atLeast, exactly, lessThan, moreThan, within).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, Action atLeast, Action exactly, Action lessThan, Action moreThan, Action within)
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
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, Func<Task> atLeast, Func<Task> exactly, Func<Task> lessThan, Func<Task> moreThan, Func<Task> within)
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
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, Action atLeast, Action exactly, Action lessThan, Action moreThan, Action within) =>
		(await timeSpanCondition.ConfigureAwait(false)).Switch(atLeast, exactly, lessThan, moreThan, within);
		
		public static async Task Switch(this Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, Func<Task> atLeast, Func<Task> exactly, Func<Task> lessThan, Func<Task> moreThan, Func<Task> within) =>
		await (await timeSpanCondition.ConfigureAwait(false)).Switch(atLeast, exactly, lessThan, moreThan, within).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
