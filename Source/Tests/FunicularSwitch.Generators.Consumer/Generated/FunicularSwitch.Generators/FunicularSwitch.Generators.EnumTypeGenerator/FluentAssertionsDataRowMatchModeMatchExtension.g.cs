#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Data
{
	public static partial class RowMatchModeMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Data.RowMatchMode rowMatchMode, Func<T> index, Func<T> primaryKey) =>
		rowMatchMode switch
		{
			FluentAssertions.Data.RowMatchMode.Index => index(),
			FluentAssertions.Data.RowMatchMode.PrimaryKey => primaryKey(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Data.RowMatchMode rowMatchMode, Func<Task<T>> index, Func<Task<T>> primaryKey) =>
		rowMatchMode switch
		{
			FluentAssertions.Data.RowMatchMode.Index => index(),
			FluentAssertions.Data.RowMatchMode.PrimaryKey => primaryKey(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, Func<T> index, Func<T> primaryKey) =>
		(await rowMatchMode.ConfigureAwait(false)).Match(index, primaryKey);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, Func<Task<T>> index, Func<Task<T>> primaryKey) =>
		await (await rowMatchMode.ConfigureAwait(false)).Match(index, primaryKey).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Data.RowMatchMode rowMatchMode, Action index, Action primaryKey)
		{
			switch (rowMatchMode)
			{
				case FluentAssertions.Data.RowMatchMode.Index:
					index();
					break;
				case FluentAssertions.Data.RowMatchMode.PrimaryKey:
					primaryKey();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Data.RowMatchMode rowMatchMode, Func<Task> index, Func<Task> primaryKey)
		{
			switch (rowMatchMode)
			{
				case FluentAssertions.Data.RowMatchMode.Index:
					await index().ConfigureAwait(false);
					break;
				case FluentAssertions.Data.RowMatchMode.PrimaryKey:
					await primaryKey().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, Action index, Action primaryKey) =>
		(await rowMatchMode.ConfigureAwait(false)).Switch(index, primaryKey);
		
		public static async Task Switch(this Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, Func<Task> index, Func<Task> primaryKey) =>
		await (await rowMatchMode.ConfigureAwait(false)).Switch(index, primaryKey).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
