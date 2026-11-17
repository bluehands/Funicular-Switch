#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class PayloadMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Payload payload, global::System.Func<int, T> @int, global::System.Func<string, T> @string, global::System.Func<int[], T> intArray) =>
		payload switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			int @int1 => @int(@int1),
			string @string2 => @string(@string2),
			int[] intArray3 => intArray(intArray3),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Payload payload, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<int[], global::System.Threading.Tasks.Task<T>> intArray) =>
		payload switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			int @int1 => await @int(@int1).ConfigureAwait(false),
			string @string2 => await @string(@string2).ConfigureAwait(false),
			int[] intArray3 => await intArray(intArray3).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Payload> payload, global::System.Func<int, T> @int, global::System.Func<string, T> @string, global::System.Func<int[], T> intArray) =>
		(await payload.ConfigureAwait(false)).Match(@int, @string, intArray);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Payload> payload, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<int[], global::System.Threading.Tasks.Task<T>> intArray) =>
		await (await payload.ConfigureAwait(false)).Match(@int, @string, intArray).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Payload payload, global::System.Action<int> @int, global::System.Action<string> @string, global::System.Action<int[]> intArray)
		{
			switch (payload)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case int @int1:
					@int(@int1);
					break;
				case string @string2:
					@string(@string2);
					break;
				case int[] intArray3:
					intArray(intArray3);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Payload payload, global::System.Func<int, global::System.Threading.Tasks.Task> @int, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<int[], global::System.Threading.Tasks.Task> intArray)
		{
			switch (payload)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case int @int1:
					await @int(@int1).ConfigureAwait(false);
					break;
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
				case int[] intArray3:
					await intArray(intArray3).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Payload> payload, global::System.Action<int> @int, global::System.Action<string> @string, global::System.Action<int[]> intArray) =>
		(await payload.ConfigureAwait(false)).Switch(@int, @string, intArray);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Payload> payload, global::System.Func<int, global::System.Threading.Tasks.Task> @int, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<int[], global::System.Threading.Tasks.Task> intArray) =>
		await (await payload.ConfigureAwait(false)).Switch(@int, @string, intArray).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
