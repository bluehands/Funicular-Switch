//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Bbb, T> bbb, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, T> baseChild) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb case1 => bbb(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.BaseChild case3 => baseChild(case3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task<T>> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task<T>> baseChild) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb case1 => bbb(case1),
			FunicularSwitch.Test.Aaa case2 => aaa(case2),
			FunicularSwitch.Test.BaseChild case3 => baseChild(case3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Bbb, T> bbb, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, T> baseChild) =>
		(await @base.ConfigureAwait(false)).Match(bbb, aaa, baseChild);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task<T>> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task<T>> baseChild) =>
		await (await @base.ConfigureAwait(false)).Match(bbb, aaa, baseChild).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.Bbb> bbb, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.BaseChild> baseChild)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Bbb case1:
					bbb(case1);
					break;
				case FunicularSwitch.Test.Aaa case2:
					aaa(case2);
					break;
				case FunicularSwitch.Test.BaseChild case3:
					baseChild(case3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task> baseChild)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Bbb case1:
					await bbb(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Aaa case2:
					await aaa(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.BaseChild case3:
					await baseChild(case3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.Bbb> bbb, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.BaseChild> baseChild) =>
		(await @base.ConfigureAwait(false)).Switch(bbb, aaa, baseChild);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task> baseChild) =>
		await (await @base.ConfigureAwait(false)).Switch(bbb, aaa, baseChild).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
