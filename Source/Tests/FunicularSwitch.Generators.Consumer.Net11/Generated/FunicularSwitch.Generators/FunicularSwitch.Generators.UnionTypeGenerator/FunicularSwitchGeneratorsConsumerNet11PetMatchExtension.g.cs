#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class PetMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Pet pet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, T> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, T> dog2) =>
		pet switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21 => cat2(cat21),
			global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22 => dog2(dog22),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Pet pet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task<T>> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task<T>> dog2) =>
		pet switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21 => await cat2(cat21).ConfigureAwait(false),
			global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22 => await dog2(dog22).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Pet> pet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, T> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, T> dog2) =>
		(await pet.ConfigureAwait(false)).Match(cat2, dog2);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Pet> pet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task<T>> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task<T>> dog2) =>
		await (await pet.ConfigureAwait(false)).Match(cat2, dog2).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Pet pet, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Cat2> cat2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Dog2> dog2)
		{
			switch (pet)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21:
					cat2(cat21);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22:
					dog2(dog22);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Pet pet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task> dog2)
		{
			switch (pet)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21:
					await cat2(cat21).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22:
					await dog2(dog22).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Pet> pet, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Cat2> cat2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Dog2> dog2) =>
		(await pet.ConfigureAwait(false)).Switch(cat2, dog2);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Pet> pet, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task> dog2) =>
		await (await pet.ConfigureAwait(false)).Switch(cat2, dog2).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
