#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace System
{
	public static partial class DateTimeKindMatchExtension
	{
		public static T Match<T>(this System.DateTimeKind dateTimeKind, Func<T> local, Func<T> unspecified, Func<T> utc) =>
		dateTimeKind switch
		{
			System.DateTimeKind.Local => local(),
			System.DateTimeKind.Unspecified => unspecified(),
			System.DateTimeKind.Utc => utc(),
			_ => throw new ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this System.DateTimeKind dateTimeKind, Func<Task<T>> local, Func<Task<T>> unspecified, Func<Task<T>> utc) =>
		dateTimeKind switch
		{
			System.DateTimeKind.Local => local(),
			System.DateTimeKind.Unspecified => unspecified(),
			System.DateTimeKind.Utc => utc(),
			_ => throw new ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<System.DateTimeKind> dateTimeKind, Func<T> local, Func<T> unspecified, Func<T> utc) =>
		(await dateTimeKind.ConfigureAwait(false)).Match(local, unspecified, utc);
		
		public static async Task<T> Match<T>(this Task<System.DateTimeKind> dateTimeKind, Func<Task<T>> local, Func<Task<T>> unspecified, Func<Task<T>> utc) =>
		await (await dateTimeKind.ConfigureAwait(false)).Match(local, unspecified, utc).ConfigureAwait(false);
		
		public static void Switch(this System.DateTimeKind dateTimeKind, Action local, Action unspecified, Action utc)
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
					throw new ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}");
			}
		}
		
		public static async Task Switch(this System.DateTimeKind dateTimeKind, Func<Task> local, Func<Task> unspecified, Func<Task> utc)
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
					throw new ArgumentException($"Unknown enum value from System.DateTimeKind: {dateTimeKind.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<System.DateTimeKind> dateTimeKind, Action local, Action unspecified, Action utc) =>
		(await dateTimeKind.ConfigureAwait(false)).Switch(local, unspecified, utc);
		
		public static async Task Switch(this Task<System.DateTimeKind> dateTimeKind, Func<Task> local, Func<Task> unspecified, Func<Task> utc) =>
		await (await dateTimeKind.ConfigureAwait(false)).Switch(local, unspecified, utc).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
