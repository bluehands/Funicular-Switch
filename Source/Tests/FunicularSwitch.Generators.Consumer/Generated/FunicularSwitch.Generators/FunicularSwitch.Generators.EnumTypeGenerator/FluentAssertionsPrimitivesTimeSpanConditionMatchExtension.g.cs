#pragma warning disable 1591
namespace FluentAssertions.Primitives
{
	internal static partial class TimeSpanConditionMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, global::System.Func<T> atLeast, global::System.Func<T> exactly, global::System.Func<T> lessThan, global::System.Func<T> moreThan, global::System.Func<T> within) =>
		timeSpanCondition switch
		{
			FluentAssertions.Primitives.TimeSpanCondition.AtLeast => atLeast(),
			FluentAssertions.Primitives.TimeSpanCondition.Exactly => exactly(),
			FluentAssertions.Primitives.TimeSpanCondition.LessThan => lessThan(),
			FluentAssertions.Primitives.TimeSpanCondition.MoreThan => moreThan(),
			FluentAssertions.Primitives.TimeSpanCondition.Within => within(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, global::System.Func<global::System.Threading.Tasks.Task<T>> atLeast, global::System.Func<global::System.Threading.Tasks.Task<T>> exactly, global::System.Func<global::System.Threading.Tasks.Task<T>> lessThan, global::System.Func<global::System.Threading.Tasks.Task<T>> moreThan, global::System.Func<global::System.Threading.Tasks.Task<T>> within) =>
		timeSpanCondition switch
		{
			FluentAssertions.Primitives.TimeSpanCondition.AtLeast => atLeast(),
			FluentAssertions.Primitives.TimeSpanCondition.Exactly => exactly(),
			FluentAssertions.Primitives.TimeSpanCondition.LessThan => lessThan(),
			FluentAssertions.Primitives.TimeSpanCondition.MoreThan => moreThan(),
			FluentAssertions.Primitives.TimeSpanCondition.Within => within(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, global::System.Func<T> atLeast, global::System.Func<T> exactly, global::System.Func<T> lessThan, global::System.Func<T> moreThan, global::System.Func<T> within) =>
		(await timeSpanCondition.ConfigureAwait(false)).Match(atLeast, exactly, lessThan, moreThan, within);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, global::System.Func<global::System.Threading.Tasks.Task<T>> atLeast, global::System.Func<global::System.Threading.Tasks.Task<T>> exactly, global::System.Func<global::System.Threading.Tasks.Task<T>> lessThan, global::System.Func<global::System.Threading.Tasks.Task<T>> moreThan, global::System.Func<global::System.Threading.Tasks.Task<T>> within) =>
		await (await timeSpanCondition.ConfigureAwait(false)).Match(atLeast, exactly, lessThan, moreThan, within).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, global::System.Action atLeast, global::System.Action exactly, global::System.Action lessThan, global::System.Action moreThan, global::System.Action within)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Primitives.TimeSpanCondition timeSpanCondition, global::System.Func<global::System.Threading.Tasks.Task> atLeast, global::System.Func<global::System.Threading.Tasks.Task> exactly, global::System.Func<global::System.Threading.Tasks.Task> lessThan, global::System.Func<global::System.Threading.Tasks.Task> moreThan, global::System.Func<global::System.Threading.Tasks.Task> within)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Primitives.TimeSpanCondition: {timeSpanCondition.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, global::System.Action atLeast, global::System.Action exactly, global::System.Action lessThan, global::System.Action moreThan, global::System.Action within) =>
		(await timeSpanCondition.ConfigureAwait(false)).Switch(atLeast, exactly, lessThan, moreThan, within);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Primitives.TimeSpanCondition> timeSpanCondition, global::System.Func<global::System.Threading.Tasks.Task> atLeast, global::System.Func<global::System.Threading.Tasks.Task> exactly, global::System.Func<global::System.Threading.Tasks.Task> lessThan, global::System.Func<global::System.Threading.Tasks.Task> moreThan, global::System.Func<global::System.Threading.Tasks.Task> within) =>
		await (await timeSpanCondition.ConfigureAwait(false)).Switch(atLeast, exactly, lessThan, moreThan, within).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
