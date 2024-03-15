#pragma warning disable 1591
namespace System
{
	public static partial class DateTimeKindMatchExtension
	{
		public static T Match<T>(this System.DateTimeKind dateTimeKind, global::System.Func<T> local, global::System.Func<T> unspecified, global::System.Func<T> utc) =>
		dateTimeKind switch
		{
			System.DateTimeKind.Local => local(),
			System.DateTimeKind.Unspecified => unspecified(),
			System.DateTimeKind.Utc => utc(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this System.DateTimeKind dateTimeKind, global::System.Func<global::System.Threading.Tasks.Task<T>> local, global::System.Func<global::System.Threading.Tasks.Task<T>> unspecified, global::System.Func<global::System.Threading.Tasks.Task<T>> utc) =>
		dateTimeKind switch
		{
			System.DateTimeKind.Local => local(),
			System.DateTimeKind.Unspecified => unspecified(),
			System.DateTimeKind.Utc => utc(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, global::System.Func<T> local, global::System.Func<T> unspecified, global::System.Func<T> utc) =>
		(await dateTimeKind.ConfigureAwait(false)).Match(local, unspecified, utc);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, global::System.Func<global::System.Threading.Tasks.Task<T>> local, global::System.Func<global::System.Threading.Tasks.Task<T>> unspecified, global::System.Func<global::System.Threading.Tasks.Task<T>> utc) =>
		await (await dateTimeKind.ConfigureAwait(false)).Match(local, unspecified, utc).ConfigureAwait(false);
		
		public static void Switch(this System.DateTimeKind dateTimeKind, global::System.Action local, global::System.Action unspecified, global::System.Action utc)
		{
			switch (dateTimeKind)
			{
				case System.DateTimeKind.Local:
					local();
					break;
				case System.DateTimeKind.Unspecified:
					unspecified();
					break;
				case System.DateTimeKind.Utc:
					utc();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this System.DateTimeKind dateTimeKind, global::System.Func<global::System.Threading.Tasks.Task> local, global::System.Func<global::System.Threading.Tasks.Task> unspecified, global::System.Func<global::System.Threading.Tasks.Task> utc)
		{
			switch (dateTimeKind)
			{
				case System.DateTimeKind.Local:
					await local().ConfigureAwait(false);
					break;
				case System.DateTimeKind.Unspecified:
					await unspecified().ConfigureAwait(false);
					break;
				case System.DateTimeKind.Utc:
					await utc().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, global::System.Action local, global::System.Action unspecified, global::System.Action utc) =>
		(await dateTimeKind.ConfigureAwait(false)).Switch(local, unspecified, utc);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, global::System.Func<global::System.Threading.Tasks.Task> local, global::System.Func<global::System.Threading.Tasks.Task> unspecified, global::System.Func<global::System.Threading.Tasks.Task> utc) =>
		await (await dateTimeKind.ConfigureAwait(false)).Switch(local, unspecified, utc).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
