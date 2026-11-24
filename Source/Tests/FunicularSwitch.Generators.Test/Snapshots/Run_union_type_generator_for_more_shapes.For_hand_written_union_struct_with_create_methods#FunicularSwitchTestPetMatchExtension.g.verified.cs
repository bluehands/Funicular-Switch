//HintName: FunicularSwitchTestPetMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class PetMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Pet pet, global::System.Func<global::FunicularSwitch.Test.Cat, T> cat, global::System.Func<string?, T> @string) =>
		pet switch
		{
			null => @string(null),
			global::FunicularSwitch.Test.Cat cat1 => cat(cat1),
			string @string2 => @string(@string2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Pet pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string) =>
		pet switch
		{
			null => await @string(null).ConfigureAwait(false),
			global::FunicularSwitch.Test.Cat cat1 => await cat(cat1).ConfigureAwait(false),
			string @string2 => await @string(@string2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Func<global::FunicularSwitch.Test.Cat, T> cat, global::System.Func<string?, T> @string) =>
		(await pet.ConfigureAwait(false)).Match(cat, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string) =>
		await (await pet.ConfigureAwait(false)).Match(cat, @string).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Pet pet, global::System.Action<global::FunicularSwitch.Test.Cat> cat, global::System.Action<string?> @string)
		{
			switch (pet)
			{
				case null:
					@string(null);
					break;
				case global::FunicularSwitch.Test.Cat cat1:
					cat(cat1);
					break;
				case string @string2:
					@string(@string2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Pet pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<string?, global::System.Threading.Tasks.Task> @string)
		{
			switch (pet)
			{
				case null:
					await @string(null).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Test.Cat cat1:
					await cat(cat1).ConfigureAwait(false);
					break;
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Action<global::FunicularSwitch.Test.Cat> cat, global::System.Action<string?> @string) =>
		(await pet.ConfigureAwait(false)).Switch(cat, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<string?, global::System.Threading.Tasks.Task> @string) =>
		await (await pet.ConfigureAwait(false)).Switch(cat, @string).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
