//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.Zwei, T> zwei, System.Func<FunicularSwitch.Test.Eins, T> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei case1 => zwei(case1),
			FunicularSwitch.Test.Eins case2 => eins(case2),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.Zwei, System.Threading.Tasks.Task<T>> zwei, System.Func<FunicularSwitch.Test.Eins, System.Threading.Tasks.Task<T>> eins) =>
		@base switch
		{
			FunicularSwitch.Test.Zwei case1 => zwei(case1),
			FunicularSwitch.Test.Eins case2 => eins(case2),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.Zwei, T> zwei, System.Func<FunicularSwitch.Test.Eins, T> eins) =>
		(await @base.ConfigureAwait(false)).Match(zwei, eins);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.Zwei, System.Threading.Tasks.Task<T>> zwei, System.Func<FunicularSwitch.Test.Eins, System.Threading.Tasks.Task<T>> eins) =>
		await (await @base.ConfigureAwait(false)).Match(zwei, eins).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, System.Action<FunicularSwitch.Test.Zwei> zwei, System.Action<FunicularSwitch.Test.Eins> eins)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Zwei case1:
					zwei(case1);
					break;
				case FunicularSwitch.Test.Eins case2:
					eins(case2);
					break;
				default:
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, System.Func<FunicularSwitch.Test.Zwei, System.Threading.Tasks.Task> zwei, System.Func<FunicularSwitch.Test.Eins, System.Threading.Tasks.Task> eins)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Zwei case1:
					await zwei(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Eins case2:
					await eins(case2).ConfigureAwait(false);
					break;
				default:
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Action<FunicularSwitch.Test.Zwei> zwei, System.Action<FunicularSwitch.Test.Eins> eins) =>
		(await @base.ConfigureAwait(false)).Switch(zwei, eins);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, System.Func<FunicularSwitch.Test.Zwei, System.Threading.Tasks.Task> zwei, System.Func<FunicularSwitch.Test.Eins, System.Threading.Tasks.Task> eins) =>
		await (await @base.ConfigureAwait(false)).Switch(zwei, eins).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
