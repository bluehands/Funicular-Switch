//HintName: FunicularSwitchTestOuterNestedMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class NestedMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Outer.Nested nested, global::System.Func<int, T> @int, global::System.Func<string, T> @string) =>
		nested switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Outer.Nested has no value (default instance of the union struct)"),
			int @int1 => @int(@int1),
			string @string2 => @string(@string2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Outer.Nested nested, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string) =>
		nested switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Outer.Nested has no value (default instance of the union struct)"),
			int @int1 => await @int(@int1).ConfigureAwait(false),
			string @string2 => await @string(@string2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outer.Nested> nested, global::System.Func<int, T> @int, global::System.Func<string, T> @string) =>
		(await nested.ConfigureAwait(false)).Match(@int, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outer.Nested> nested, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string) =>
		await (await nested.ConfigureAwait(false)).Match(@int, @string).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Outer.Nested nested, global::System.Action<int> @int, global::System.Action<string> @string)
		{
			switch (nested)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Outer.Nested has no value (default instance of the union struct)");
				case int @int1:
					@int(@int1);
					break;
				case string @string2:
					@string(@string2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Outer.Nested nested, global::System.Func<int, global::System.Threading.Tasks.Task> @int, global::System.Func<string, global::System.Threading.Tasks.Task> @string)
		{
			switch (nested)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Outer.Nested has no value (default instance of the union struct)");
				case int @int1:
					await @int(@int1).ConfigureAwait(false);
					break;
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outer.Nested> nested, global::System.Action<int> @int, global::System.Action<string> @string) =>
		(await nested.ConfigureAwait(false)).Switch(@int, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Outer.Nested> nested, global::System.Func<int, global::System.Threading.Tasks.Task> @int, global::System.Func<string, global::System.Threading.Tasks.Task> @string) =>
		await (await nested.ConfigureAwait(false)).Switch(@int, @string).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
