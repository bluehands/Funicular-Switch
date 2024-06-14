//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Zwei, T> zwei, global::System.Func<FunicularSwitch.Test.Eins, T> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei zwei1 => zwei(zwei1),
			FunicularSwitch.Test.Eins eins2 => eins(eins2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Zwei, global::System.Threading.Tasks.Task<T>> zwei, global::System.Func<FunicularSwitch.Test.Eins, global::System.Threading.Tasks.Task<T>> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei zwei1 => zwei(zwei1),
			FunicularSwitch.Test.Eins eins2 => eins(eins2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Zwei, T> zwei, global::System.Func<FunicularSwitch.Test.Eins, T> eins) =>
		(await @base.ConfigureAwait(false)).Match(zwei, eins);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Zwei, global::System.Threading.Tasks.Task<T>> zwei, global::System.Func<FunicularSwitch.Test.Eins, global::System.Threading.Tasks.Task<T>> eins) =>
		await (await @base.ConfigureAwait(false)).Match(zwei, eins).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.Zwei> zwei, global::System.Action<FunicularSwitch.Test.Eins> eins)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Zwei zwei1:
					zwei(zwei1);
					break;
				case FunicularSwitch.Test.Eins eins2:
					eins(eins2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Zwei, global::System.Threading.Tasks.Task> zwei, global::System.Func<FunicularSwitch.Test.Eins, global::System.Threading.Tasks.Task> eins)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Zwei zwei1:
					await zwei(zwei1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Eins eins2:
					await eins(eins2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.Zwei> zwei, global::System.Action<FunicularSwitch.Test.Eins> eins) =>
		(await @base.ConfigureAwait(false)).Switch(zwei, eins);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Zwei, global::System.Threading.Tasks.Task> zwei, global::System.Func<FunicularSwitch.Test.Eins, global::System.Threading.Tasks.Task> eins) =>
		await (await @base.ConfigureAwait(false)).Switch(zwei, eins).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
