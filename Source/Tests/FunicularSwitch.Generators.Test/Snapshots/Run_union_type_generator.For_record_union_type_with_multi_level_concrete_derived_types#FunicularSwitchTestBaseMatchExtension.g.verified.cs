//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Bbb, T> bbb, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, T> child) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb bbb1 => bbb(bbb1),
			FunicularSwitch.Test.Aaa aaa2 => aaa(aaa2),
			FunicularSwitch.Test.BaseChild child3 => child(child3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task<T>> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task<T>> child) =>
		@base switch
		{
			FunicularSwitch.Test.Bbb bbb1 => bbb(bbb1),
			FunicularSwitch.Test.Aaa aaa2 => aaa(aaa2),
			FunicularSwitch.Test.BaseChild child3 => child(child3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Bbb, T> bbb, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, T> child) =>
		(await @base.ConfigureAwait(false)).Match(bbb, aaa, child);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task<T>> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task<T>> child) =>
		await (await @base.ConfigureAwait(false)).Match(bbb, aaa, child).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.Bbb> bbb, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.BaseChild> child)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Bbb bbb1:
					bbb(bbb1);
					break;
				case FunicularSwitch.Test.Aaa aaa2:
					aaa(aaa2);
					break;
				case FunicularSwitch.Test.BaseChild child3:
					child(child3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task> child)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.Bbb bbb1:
					await bbb(bbb1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Aaa aaa2:
					await aaa(aaa2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.BaseChild child3:
					await child(child3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.Bbb> bbb, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.BaseChild> child) =>
		(await @base.ConfigureAwait(false)).Switch(bbb, aaa, child);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.Bbb, global::System.Threading.Tasks.Task> bbb, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.BaseChild, global::System.Threading.Tasks.Task> child) =>
		await (await @base.ConfigureAwait(false)).Switch(bbb, aaa, child).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
