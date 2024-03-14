#pragma warning disable 1591
namespace FluentAssertions.Data
{
	public static partial class RowMatchModeMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Data.RowMatchMode rowMatchMode, System.Func<T> index, System.Func<T> primaryKey) =>
		rowMatchMode switch
		{
			FluentAssertions.Data.RowMatchMode.Index => index(),
			FluentAssertions.Data.RowMatchMode.PrimaryKey => primaryKey(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Data.RowMatchMode rowMatchMode, System.Func<System.Threading.Tasks.Task<T>> index, System.Func<System.Threading.Tasks.Task<T>> primaryKey) =>
		rowMatchMode switch
		{
			FluentAssertions.Data.RowMatchMode.Index => index(),
			FluentAssertions.Data.RowMatchMode.PrimaryKey => primaryKey(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, System.Func<T> index, System.Func<T> primaryKey) =>
		(await rowMatchMode.ConfigureAwait(false)).Match(index, primaryKey);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, System.Func<System.Threading.Tasks.Task<T>> index, System.Func<System.Threading.Tasks.Task<T>> primaryKey) =>
		await (await rowMatchMode.ConfigureAwait(false)).Match(index, primaryKey).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Data.RowMatchMode rowMatchMode, System.Action index, System.Action primaryKey)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FluentAssertions.Data.RowMatchMode rowMatchMode, System.Func<System.Threading.Tasks.Task> index, System.Func<System.Threading.Tasks.Task> primaryKey)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Data.RowMatchMode: {rowMatchMode.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, System.Action index, System.Action primaryKey) =>
		(await rowMatchMode.ConfigureAwait(false)).Switch(index, primaryKey);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Data.RowMatchMode> rowMatchMode, System.Func<System.Threading.Tasks.Task> index, System.Func<System.Threading.Tasks.Task> primaryKey) =>
		await (await rowMatchMode.ConfigureAwait(false)).Switch(index, primaryKey).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
