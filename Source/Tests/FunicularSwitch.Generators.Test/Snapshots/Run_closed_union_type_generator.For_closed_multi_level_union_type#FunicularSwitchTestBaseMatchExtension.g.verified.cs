//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.MidA, T> midA, global::System.Func<FunicularSwitch.Test.MidB, T> midB, global::System.Func<FunicularSwitch.Test.Other, T> other) =>
		@base switch
		{
			null => throw new global::System.ArgumentNullException(nameof(@base)),
			FunicularSwitch.Test.MidA midA1 => midA(midA1),
			FunicularSwitch.Test.MidB midB2 => midB(midB2),
			FunicularSwitch.Test.Other other3 => other(other3),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.MidA, global::System.Threading.Tasks.Task<T>> midA, global::System.Func<FunicularSwitch.Test.MidB, global::System.Threading.Tasks.Task<T>> midB, global::System.Func<FunicularSwitch.Test.Other, global::System.Threading.Tasks.Task<T>> other) =>
		@base switch
		{
			null => throw new global::System.ArgumentNullException(nameof(@base)),
			FunicularSwitch.Test.MidA midA1 => await midA(midA1).ConfigureAwait(false),
			FunicularSwitch.Test.MidB midB2 => await midB(midB2).ConfigureAwait(false),
			FunicularSwitch.Test.Other other3 => await other(other3).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.MidA, T> midA, global::System.Func<FunicularSwitch.Test.MidB, T> midB, global::System.Func<FunicularSwitch.Test.Other, T> other) =>
		(await @base.ConfigureAwait(false)).Match(midA, midB, other);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.MidA, global::System.Threading.Tasks.Task<T>> midA, global::System.Func<FunicularSwitch.Test.MidB, global::System.Threading.Tasks.Task<T>> midB, global::System.Func<FunicularSwitch.Test.Other, global::System.Threading.Tasks.Task<T>> other) =>
		await (await @base.ConfigureAwait(false)).Match(midA, midB, other).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.MidA> midA, global::System.Action<FunicularSwitch.Test.MidB> midB, global::System.Action<FunicularSwitch.Test.Other> other)
		{
			switch (@base)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(@base));
				case FunicularSwitch.Test.MidA midA1:
					midA(midA1);
					break;
				case FunicularSwitch.Test.MidB midB2:
					midB(midB2);
					break;
				case FunicularSwitch.Test.Other other3:
					other(other3);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.MidA, global::System.Threading.Tasks.Task> midA, global::System.Func<FunicularSwitch.Test.MidB, global::System.Threading.Tasks.Task> midB, global::System.Func<FunicularSwitch.Test.Other, global::System.Threading.Tasks.Task> other)
		{
			switch (@base)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(@base));
				case FunicularSwitch.Test.MidA midA1:
					await midA(midA1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.MidB midB2:
					await midB(midB2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Other other3:
					await other(other3).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.MidA> midA, global::System.Action<FunicularSwitch.Test.MidB> midB, global::System.Action<FunicularSwitch.Test.Other> other) =>
		(await @base.ConfigureAwait(false)).Switch(midA, midB, other);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.MidA, global::System.Threading.Tasks.Task> midA, global::System.Func<FunicularSwitch.Test.MidB, global::System.Threading.Tasks.Task> midB, global::System.Func<FunicularSwitch.Test.Other, global::System.Threading.Tasks.Task> other) =>
		await (await @base.ConfigureAwait(false)).Switch(midA, midB, other).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
