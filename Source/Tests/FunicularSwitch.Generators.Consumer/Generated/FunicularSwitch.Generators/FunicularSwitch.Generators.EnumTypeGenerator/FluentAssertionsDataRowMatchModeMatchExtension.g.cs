#pragma warning disable 1591
namespace FluentAssertions.Data
{
	internal static partial class RowMatchModeMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Data.RowMatchMode rowMatchMode, global::System.Func<T> index, global::System.Func<T> primaryKey) =>
		rowMatchMode switch
		{
			FluentAssertions.Data.RowMatchMode.Index => index(),
			FluentAssertions.Data.RowMatchMode.PrimaryKey => primaryKey(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Data.RowMatchMode rowMatchMode, global::System.Func<global::System.Threading.Tasks.Task<T>> index, global::System.Func<global::System.Threading.Tasks.Task<T>> primaryKey) =>
		rowMatchMode switch
		{
			FluentAssertions.Data.RowMatchMode.Index => index(),
			FluentAssertions.Data.RowMatchMode.PrimaryKey => primaryKey(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, global::System.Func<T> index, global::System.Func<T> primaryKey) =>
		(await rowMatchMode.ConfigureAwait(false)).Match(index, primaryKey);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, global::System.Func<global::System.Threading.Tasks.Task<T>> index, global::System.Func<global::System.Threading.Tasks.Task<T>> primaryKey) =>
		await (await rowMatchMode.ConfigureAwait(false)).Match(index, primaryKey).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Data.RowMatchMode rowMatchMode, global::System.Action index, global::System.Action primaryKey)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Data.RowMatchMode rowMatchMode, global::System.Func<global::System.Threading.Tasks.Task> index, global::System.Func<global::System.Threading.Tasks.Task> primaryKey)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, global::System.Action index, global::System.Action primaryKey) =>
		(await rowMatchMode.ConfigureAwait(false)).Switch(index, primaryKey);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, global::System.Func<global::System.Threading.Tasks.Task> index, global::System.Func<global::System.Threading.Tasks.Task> primaryKey) =>
		await (await rowMatchMode.ConfigureAwait(false)).Switch(index, primaryKey).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
