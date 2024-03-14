#pragma warning disable 1591
namespace System
{
	public static partial class DateTimeKindMatchExtension
	{
		public static T Match<T>(this System.DateTimeKind dateTimeKind, System.Func<T> local, System.Func<T> unspecified, System.Func<T> utc) =>
		dateTimeKind switch
		{
			System.DateTimeKind.Local => local(),
			System.DateTimeKind.Unspecified => unspecified(),
			System.DateTimeKind.Utc => utc(),
			_ => throw new System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this System.DateTimeKind dateTimeKind, System.Func<System.Threading.Tasks.Task<T>> local, System.Func<System.Threading.Tasks.Task<T>> unspecified, System.Func<System.Threading.Tasks.Task<T>> utc) =>
		dateTimeKind switch
		{
			System.DateTimeKind.Local => local(),
			System.DateTimeKind.Unspecified => unspecified(),
			System.DateTimeKind.Utc => utc(),
			_ => throw new System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, System.Func<T> local, System.Func<T> unspecified, System.Func<T> utc) =>
		(await dateTimeKind.ConfigureAwait(false)).Match(local, unspecified, utc);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, System.Func<System.Threading.Tasks.Task<T>> local, System.Func<System.Threading.Tasks.Task<T>> unspecified, System.Func<System.Threading.Tasks.Task<T>> utc) =>
		await (await dateTimeKind.ConfigureAwait(false)).Match(local, unspecified, utc).ConfigureAwait(false);
		
		public static void Switch(this System.DateTimeKind dateTimeKind, System.Action local, System.Action unspecified, System.Action utc)
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
					throw new System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.DateTimeKind dateTimeKind, System.Func<System.Threading.Tasks.Task> local, System.Func<System.Threading.Tasks.Task> unspecified, System.Func<System.Threading.Tasks.Task> utc)
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
					throw new System.ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, System.Action local, System.Action unspecified, System.Action utc) =>
		(await dateTimeKind.ConfigureAwait(false)).Switch(local, unspecified, utc);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<System.DateTimeKind> dateTimeKind, System.Func<System.Threading.Tasks.Task> local, System.Func<System.Threading.Tasks.Task> unspecified, System.Func<System.Threading.Tasks.Task> utc) =>
		await (await dateTimeKind.ConfigureAwait(false)).Switch(local, unspecified, utc).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
