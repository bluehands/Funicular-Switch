#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	internal static partial class SecretMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Secret secret, global::System.Func<string, T> @string, global::System.Func<int, T> @int) =>
		secret switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			string @string1 => @string(@string1),
			int @int2 => @int(@int2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Secret secret, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int) =>
		secret switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			string @string1 => await @string(@string1).ConfigureAwait(false),
			int @int2 => await @int(@int2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Secret> secret, global::System.Func<string, T> @string, global::System.Func<int, T> @int) =>
		(await secret.ConfigureAwait(false)).Match(@string, @int);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Secret> secret, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int) =>
		await (await secret.ConfigureAwait(false)).Match(@string, @int).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Secret secret, global::System.Action<string> @string, global::System.Action<int> @int)
		{
			switch (secret)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case string @string1:
					@string(@string1);
					break;
				case int @int2:
					@int(@int2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Secret secret, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<int, global::System.Threading.Tasks.Task> @int)
		{
			switch (secret)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case string @string1:
					await @string(@string1).ConfigureAwait(false);
					break;
				case int @int2:
					await @int(@int2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Secret> secret, global::System.Action<string> @string, global::System.Action<int> @int) =>
		(await secret.ConfigureAwait(false)).Switch(@string, @int);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Secret> secret, global::System.Func<string, global::System.Threading.Tasks.Task> @string, global::System.Func<int, global::System.Threading.Tasks.Task> @int) =>
		await (await secret.ConfigureAwait(false)).Switch(@string, @int).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
