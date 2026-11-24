#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class CreatePetMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.CreatePet createPet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, T> cat2, global::System.Func<string?, T> @string) =>
		createPet switch
		{
			null => @string(null),
			global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21 => cat2(cat21),
			string @string2 => @string(@string2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.CreatePet createPet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task<T>> cat2, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string) =>
		createPet switch
		{
			null => await @string(null).ConfigureAwait(false),
			global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21 => await cat2(cat21).ConfigureAwait(false),
			string @string2 => await @string(@string2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.CreatePet> createPet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, T> cat2, global::System.Func<string?, T> @string) =>
		(await createPet.ConfigureAwait(false)).Match(cat2, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.CreatePet> createPet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task<T>> cat2, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string) =>
		await (await createPet.ConfigureAwait(false)).Match(cat2, @string).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.CreatePet createPet, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Cat2> cat2, global::System.Action<string?> @string)
		{
			switch (createPet)
			{
				case null:
					@string(null);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21:
					cat2(cat21);
					break;
				case string @string2:
					@string(@string2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.CreatePet createPet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task> cat2, global::System.Func<string?, global::System.Threading.Tasks.Task> @string)
		{
			switch (createPet)
			{
				case null:
					await @string(null).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21:
					await cat2(cat21).ConfigureAwait(false);
					break;
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.CreatePet> createPet, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Cat2> cat2, global::System.Action<string?> @string) =>
		(await createPet.ConfigureAwait(false)).Switch(cat2, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.CreatePet> createPet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task> cat2, global::System.Func<string?, global::System.Threading.Tasks.Task> @string) =>
		await (await createPet.ConfigureAwait(false)).Switch(cat2, @string).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
