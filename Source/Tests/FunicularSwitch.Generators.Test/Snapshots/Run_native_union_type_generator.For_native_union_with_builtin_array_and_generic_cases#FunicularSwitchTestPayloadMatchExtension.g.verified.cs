//HintName: FunicularSwitchTestPayloadMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	internal static partial class PayloadMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Payload payload, global::System.Func<int, T> @int, global::System.Func<string, T> @string, global::System.Func<int[], T> intArray, global::System.Func<global::System.Collections.Generic.List<string>, T> list) =>
		payload switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Payload has no value (default instance of the union struct)"),
			int @int1 => @int(@int1),
			string @string2 => @string(@string2),
			int[] intArray3 => intArray(intArray3),
			global::System.Collections.Generic.List<string> list4 => list(list4),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Payload payload, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<int[], global::System.Threading.Tasks.Task<T>> intArray, global::System.Func<global::System.Collections.Generic.List<string>, global::System.Threading.Tasks.Task<T>> list) =>
		payload switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Payload has no value (default instance of the union struct)"),
			int @int1 => await @int(@int1).ConfigureAwait(false),
			string @string2 => await @string(@string2).ConfigureAwait(false),
			int[] intArray3 => await intArray(intArray3).ConfigureAwait(false),
			global::System.Collections.Generic.List<string> list4 => await list(list4).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Payload> payload, global::System.Func<int, T> @int, global::System.Func<string, T> @string, global::System.Func<int[], T> intArray, global::System.Func<global::System.Collections.Generic.List<string>, T> list) =>
		(await payload.ConfigureAwait(false)).Match(@int, @string, intArray, list);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Payload> payload, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<int[], global::System.Threading.Tasks.Task<T>> intArray, global::System.Func<global::System.Collections.Generic.List<string>, global::System.Threading.Tasks.Task<T>> list) =>
		await (await payload.ConfigureAwait(false)).Match(@int, @string, intArray, list).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Payload payload, global::System.Action<int> @int, global::System.Action<string> @string, global::System.Action<int[]> intArray, global::System.Action<global::System.Collections.Generic.List<string>> list)
		{
			switch (payload)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Payload has no value (default instance of the union struct)");
				case int @int1:
					@int(@int1);
					break;
				case string @string2:
					@string(@string2);
					break;
				case int[] intArray3:
					intArray(intArray3);
					break;
				case global::System.Collections.Generic.List<string> list4:
					list(list4);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Payload payload, global::System.Func<int, global::System.Threading.Tasks.Task> @int, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<int[], global::System.Threading.Tasks.Task> intArray, global::System.Func<global::System.Collections.Generic.List<string>, global::System.Threading.Tasks.Task> list)
		{
			switch (payload)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Payload has no value (default instance of the union struct)");
				case int @int1:
					await @int(@int1).ConfigureAwait(false);
					break;
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
				case int[] intArray3:
					await intArray(intArray3).ConfigureAwait(false);
					break;
				case global::System.Collections.Generic.List<string> list4:
					await list(list4).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Payload> payload, global::System.Action<int> @int, global::System.Action<string> @string, global::System.Action<int[]> intArray, global::System.Action<global::System.Collections.Generic.List<string>> list) =>
		(await payload.ConfigureAwait(false)).Switch(@int, @string, intArray, list);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Payload> payload, global::System.Func<int, global::System.Threading.Tasks.Task> @int, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<int[], global::System.Threading.Tasks.Task> intArray, global::System.Func<global::System.Collections.Generic.List<string>, global::System.Threading.Tasks.Task> list) =>
		await (await payload.ConfigureAwait(false)).Switch(@int, @string, intArray, list).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
