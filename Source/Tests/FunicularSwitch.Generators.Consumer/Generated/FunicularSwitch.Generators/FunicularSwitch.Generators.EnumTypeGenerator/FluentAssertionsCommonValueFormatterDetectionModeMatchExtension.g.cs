#pragma warning disable 1591
namespace FluentAssertions.Common
{
	internal static partial class ValueFormatterDetectionModeMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, global::System.Func<T> disabled, global::System.Func<T> scan, global::System.Func<T> specific) =>
		valueFormatterDetectionMode switch
		{
			FluentAssertions.Common.ValueFormatterDetectionMode.Disabled => disabled(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Scan => scan(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Specific => specific(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, global::System.Func<global::System.Threading.Tasks.Task<T>> disabled, global::System.Func<global::System.Threading.Tasks.Task<T>> scan, global::System.Func<global::System.Threading.Tasks.Task<T>> specific) =>
		valueFormatterDetectionMode switch
		{
			FluentAssertions.Common.ValueFormatterDetectionMode.Disabled => disabled(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Scan => scan(),
			FluentAssertions.Common.ValueFormatterDetectionMode.Specific => specific(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, global::System.Func<T> disabled, global::System.Func<T> scan, global::System.Func<T> specific) =>
		(await valueFormatterDetectionMode.ConfigureAwait(false)).Match(disabled, scan, specific);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, global::System.Func<global::System.Threading.Tasks.Task<T>> disabled, global::System.Func<global::System.Threading.Tasks.Task<T>> scan, global::System.Func<global::System.Threading.Tasks.Task<T>> specific) =>
		await (await valueFormatterDetectionMode.ConfigureAwait(false)).Match(disabled, scan, specific).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, global::System.Action disabled, global::System.Action scan, global::System.Action specific)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Common.ValueFormatterDetectionMode valueFormatterDetectionMode, global::System.Func<global::System.Threading.Tasks.Task> disabled, global::System.Func<global::System.Threading.Tasks.Task> scan, global::System.Func<global::System.Threading.Tasks.Task> specific)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.ValueFormatterDetectionMode: {valueFormatterDetectionMode.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, global::System.Action disabled, global::System.Action scan, global::System.Action specific) =>
		(await valueFormatterDetectionMode.ConfigureAwait(false)).Switch(disabled, scan, specific);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Common.ValueFormatterDetectionMode> valueFormatterDetectionMode, global::System.Func<global::System.Threading.Tasks.Task> disabled, global::System.Func<global::System.Threading.Tasks.Task> scan, global::System.Func<global::System.Threading.Tasks.Task> specific) =>
		await (await valueFormatterDetectionMode.ConfigureAwait(false)).Switch(disabled, scan, specific).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
