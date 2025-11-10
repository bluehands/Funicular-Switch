//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Aaa aaa2 => aaa(aaa2),
			FunicularSwitch.Test.Two two3 => two(two3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.One one1 => await one(one1).ConfigureAwait(false),
			FunicularSwitch.Test.Aaa aaa2 => await aaa(aaa2).ConfigureAwait(false),
			FunicularSwitch.Test.Two two3 => await two(two3).ConfigureAwait(false),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Aaa, T> aaa, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(one, aaa, two);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task<T>> aaa, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(one, aaa, two).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One one1:
					one(one1);
					break;
				case FunicularSwitch.Test.Aaa aaa2:
					aaa(aaa2);
					break;
				case FunicularSwitch.Test.Two two3:
					two(two3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One one1:
					await one(one1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Aaa aaa2:
					await aaa(aaa2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two two3:
					await two(two3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Aaa> aaa, global::System.Action<FunicularSwitch.Test.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(one, aaa, two);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Aaa, global::System.Threading.Tasks.Task> aaa, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(one, aaa, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
