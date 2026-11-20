//HintName: FunicularSwitchTestPetMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	internal static partial class PetMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Pet pet, global::System.Func<global::FunicularSwitch.Test.Cat, T> cat, global::System.Func<global::FunicularSwitch.Test.Dog, T> dog) =>
		pet switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Pet has no value (default instance of the union struct)"),
			global::FunicularSwitch.Test.Cat cat1 => cat(cat1),
			global::FunicularSwitch.Test.Dog dog2 => dog(dog2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Pet pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task<T>> dog) =>
		pet switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Pet has no value (default instance of the union struct)"),
			global::FunicularSwitch.Test.Cat cat1 => await cat(cat1).ConfigureAwait(false),
			global::FunicularSwitch.Test.Dog dog2 => await dog(dog2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Func<global::FunicularSwitch.Test.Cat, T> cat, global::System.Func<global::FunicularSwitch.Test.Dog, T> dog) =>
		(await pet.ConfigureAwait(false)).Match(cat, dog);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task<T>> dog) =>
		await (await pet.ConfigureAwait(false)).Match(cat, dog).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Pet pet, global::System.Action<global::FunicularSwitch.Test.Cat> cat, global::System.Action<global::FunicularSwitch.Test.Dog> dog)
		{
			switch (pet)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Pet has no value (default instance of the union struct)");
				case global::FunicularSwitch.Test.Cat cat1:
					cat(cat1);
					break;
				case global::FunicularSwitch.Test.Dog dog2:
					dog(dog2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Pet pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task> dog)
		{
			switch (pet)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Test.Pet has no value (default instance of the union struct)");
				case global::FunicularSwitch.Test.Cat cat1:
					await cat(cat1).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Test.Dog dog2:
					await dog(dog2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Action<global::FunicularSwitch.Test.Cat> cat, global::System.Action<global::FunicularSwitch.Test.Dog> dog) =>
		(await pet.ConfigureAwait(false)).Switch(cat, dog);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Pet> pet, global::System.Func<global::FunicularSwitch.Test.Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task> dog) =>
		await (await pet.ConfigureAwait(false)).Switch(cat, dog).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
