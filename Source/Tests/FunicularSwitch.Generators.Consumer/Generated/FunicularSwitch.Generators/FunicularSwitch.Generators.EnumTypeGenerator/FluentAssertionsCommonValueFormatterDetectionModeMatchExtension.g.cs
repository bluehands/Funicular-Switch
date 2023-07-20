#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Common
{
	public static partial class ValueFormatterDetectionModeMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, Func<T> disabled, Func<T> scan, Func<T> specific) =>
		valueFormatterDetectionMode switch
		{
			FluentAssertions.Common.ValueFormatterDetectionMode.Disabled => disabled(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Scan => scan(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Specific => specific(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, Func<Task<T>> disabled, Func<Task<T>> scan, Func<Task<T>> specific) =>
		valueFormatterDetectionMode switch
		{
			FluentAssertions.Common.ValueFormatterDetectionMode.Disabled => disabled(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Scan => scan(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Specific => specific(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, Func<T> disabled, Func<T> scan, Func<T> specific) =>
		(await valueFormatterDetectionMode.ConfigureAwait(false)).Match(disabled, scan, specific);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, Func<Task<T>> disabled, Func<Task<T>> scan, Func<Task<T>> specific) =>
		await (await valueFormatterDetectionMode.ConfigureAwait(false)).Match(disabled, scan, specific).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, Action disabled, Action scan, Action specific)
		{
			switch (valueFormatterDetectionMode)
			{
				case FluentAssertions.Common.ValueFormatterDetectionMode.Disabled:
					disabled();
					break;
				case FluentAssertions.Common.ValueFormatterDetectionMode.Scan:
					scan();
					break;
				case FluentAssertions.Common.ValueFormatterDetectionMode.Specific:
					specific();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, Func<Task> disabled, Func<Task> scan, Func<Task> specific)
		{
			switch (valueFormatterDetectionMode)
			{
				case FluentAssertions.Common.ValueFormatterDetectionMode.Disabled:
					await disabled().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.ValueFormatterDetectionMode.Scan:
					await scan().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.ValueFormatterDetectionMode.Specific:
					await specific().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, Action disabled, Action scan, Action specific) =>
		(await valueFormatterDetectionMode.ConfigureAwait(false)).Switch(disabled, scan, specific);
		
		public static async Task Switch(this Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, Func<Task> disabled, Func<Task> scan, Func<Task> specific) =>
		await (await valueFormatterDetectionMode.ConfigureAwait(false)).Switch(disabled, scan, specific).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
