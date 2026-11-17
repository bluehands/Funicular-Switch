//HintName: FunicularSwitchTestUMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class UMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.U u, global::System.Func<object, T> @object, global::System.Func<string, T> @string, global::System.Func<global::FunicularSwitch.Test.IFoo, T> iFoo) =>
		u switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			string @string2 => @string(@string2),
			global::FunicularSwitch.Test.IFoo iFoo3 => iFoo(iFoo3),
			object @object1 => @object(@object1),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.U u, global::System.Func<object, global::System.Threading.Tasks.Task<T>> @object, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<global::FunicularSwitch.Test.IFoo, global::System.Threading.Tasks.Task<T>> iFoo) =>
		u switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			string @string2 => await @string(@string2).ConfigureAwait(false),
			global::FunicularSwitch.Test.IFoo iFoo3 => await iFoo(iFoo3).ConfigureAwait(false),
			object @object1 => await @object(@object1).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.U> u, global::System.Func<object, T> @object, global::System.Func<string, T> @string, global::System.Func<global::FunicularSwitch.Test.IFoo, T> iFoo) =>
		(await u.ConfigureAwait(false)).Match(@object, @string, iFoo);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.U> u, global::System.Func<object, global::System.Threading.Tasks.Task<T>> @object, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<global::FunicularSwitch.Test.IFoo, global::System.Threading.Tasks.Task<T>> iFoo) =>
		await (await u.ConfigureAwait(false)).Match(@object, @string, iFoo).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.U u, global::System.Action<object> @object, global::System.Action<string> @string, global::System.Action<global::FunicularSwitch.Test.IFoo> iFoo)
		{
			switch (u)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case string @string2:
					@string(@string2);
					break;
				case global::FunicularSwitch.Test.IFoo iFoo3:
					iFoo(iFoo3);
					break;
				case object @object1:
					@object(@object1);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.U u, global::System.Func<object, global::System.Threading.Tasks.Task> @object, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<global::FunicularSwitch.Test.IFoo, global::System.Threading.Tasks.Task> iFoo)
		{
			switch (u)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Test.IFoo iFoo3:
					await iFoo(iFoo3).ConfigureAwait(false);
					break;
				case object @object1:
					await @object(@object1).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.U> u, global::System.Action<object> @object, global::System.Action<string> @string, global::System.Action<global::FunicularSwitch.Test.IFoo> iFoo) =>
		(await u.ConfigureAwait(false)).Switch(@object, @string, iFoo);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.U> u, global::System.Func<object, global::System.Threading.Tasks.Task> @object, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<global::FunicularSwitch.Test.IFoo, global::System.Threading.Tasks.Task> iFoo) =>
		await (await u.ConfigureAwait(false)).Switch(@object, @string, iFoo).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
