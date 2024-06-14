#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class IUnionMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.IUnion iUnion, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case1_, T> case1, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case2_, T> case2) =>
		iUnion switch
		{
			FunicularSwitch.Generators.Consumer.IUnion.Case1_ case11 => case1(case11),
			FunicularSwitch.Generators.Consumer.IUnion.Case2_ case22 => case2(case22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.IUnion: {iUnion.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.IUnion iUnion, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case1_, global::System.Threading.Tasks.Task<T>> case1, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case2_, global::System.Threading.Tasks.Task<T>> case2) =>
		iUnion switch
		{
			FunicularSwitch.Generators.Consumer.IUnion.Case1_ case11 => case1(case11),
			FunicularSwitch.Generators.Consumer.IUnion.Case2_ case22 => case2(case22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.IUnion: {iUnion.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.IUnion> iUnion, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case1_, T> case1, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case2_, T> case2) =>
		(await iUnion.ConfigureAwait(false)).Match(case1, case2);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.IUnion> iUnion, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case1_, global::System.Threading.Tasks.Task<T>> case1, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case2_, global::System.Threading.Tasks.Task<T>> case2) =>
		await (await iUnion.ConfigureAwait(false)).Match(case1, case2).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.IUnion iUnion, global::System.Action<FunicularSwitch.Generators.Consumer.IUnion.Case1_> case1, global::System.Action<FunicularSwitch.Generators.Consumer.IUnion.Case2_> case2)
		{
			switch (iUnion)
			{
				case FunicularSwitch.Generators.Consumer.IUnion.Case1_ case11:
					case1(case11);
					break;
				case FunicularSwitch.Generators.Consumer.IUnion.Case2_ case22:
					case2(case22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.IUnion: {iUnion.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.IUnion iUnion, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case1_, global::System.Threading.Tasks.Task> case1, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case2_, global::System.Threading.Tasks.Task> case2)
		{
			switch (iUnion)
			{
				case FunicularSwitch.Generators.Consumer.IUnion.Case1_ case11:
					await case1(case11).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.IUnion.Case2_ case22:
					await case2(case22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.IUnion: {iUnion.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.IUnion> iUnion, global::System.Action<FunicularSwitch.Generators.Consumer.IUnion.Case1_> case1, global::System.Action<FunicularSwitch.Generators.Consumer.IUnion.Case2_> case2) =>
		(await iUnion.ConfigureAwait(false)).Switch(case1, case2);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.IUnion> iUnion, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case1_, global::System.Threading.Tasks.Task> case1, global::System.Func<FunicularSwitch.Generators.Consumer.IUnion.Case2_, global::System.Threading.Tasks.Task> case2) =>
		await (await iUnion.ConfigureAwait(false)).Switch(case1, case2).ConfigureAwait(false);
	}
	
	public partial interface IUnion
	{
		public static FunicularSwitch.Generators.Consumer.IUnion Case1() => new FunicularSwitch.Generators.Consumer.IUnion.Case1_();
		public static FunicularSwitch.Generators.Consumer.IUnion Case2() => new FunicularSwitch.Generators.Consumer.IUnion.Case2_();
	}
}
#pragma warning restore 1591
