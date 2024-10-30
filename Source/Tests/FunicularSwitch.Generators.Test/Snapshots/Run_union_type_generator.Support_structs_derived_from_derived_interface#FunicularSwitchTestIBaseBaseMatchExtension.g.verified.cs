//HintName: FunicularSwitchTestIBaseBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class IBaseBaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.IBaseBase iBaseBase, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Three, T> three, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		iBaseBase switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Three three2 => three(three2),
			FunicularSwitch.Test.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBaseBase: {iBaseBase.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.IBaseBase iBaseBase, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task<T>> three, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		iBaseBase switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Three three2 => three(three2),
			FunicularSwitch.Test.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBaseBase: {iBaseBase.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.IBaseBase> iBaseBase, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Three, T> three, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		(await iBaseBase.ConfigureAwait(false)).Match(one, three, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.IBaseBase> iBaseBase, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task<T>> three, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await iBaseBase.ConfigureAwait(false)).Match(one, three, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.IBaseBase iBaseBase, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Three> three, global::System.Action<FunicularSwitch.Test.Two> two)
		{
			switch (iBaseBase)
			{
				case FunicularSwitch.Test.One one1:
					one(one1);
					break;
				case FunicularSwitch.Test.Three three2:
					three(three2);
					break;
				case FunicularSwitch.Test.Two two3:
					two(two3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBaseBase: {iBaseBase.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.IBaseBase iBaseBase, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task> three, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two)
		{
			switch (iBaseBase)
			{
				case FunicularSwitch.Test.One one1:
					await one(one1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Three three2:
					await three(three2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two two3:
					await two(two3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IBaseBase: {iBaseBase.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.IBaseBase> iBaseBase, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Three> three, global::System.Action<FunicularSwitch.Test.Two> two) =>
		(await iBaseBase.ConfigureAwait(false)).Switch(one, three, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.IBaseBase> iBaseBase, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Three, global::System.Threading.Tasks.Task> three, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two) =>
		await (await iBaseBase.ConfigureAwait(false)).Switch(one, three, two).ConfigureAwait(false);
	}
	
	public partial interface IBaseBase
	{
		public static FunicularSwitch.Test.IBaseBase One() => new FunicularSwitch.Test.One();
		public static FunicularSwitch.Test.IBaseBase Three() => new FunicularSwitch.Test.Three();
		public static FunicularSwitch.Test.IBaseBase Two() => new FunicularSwitch.Test.Two();
	}
}
#pragma warning restore 1591
