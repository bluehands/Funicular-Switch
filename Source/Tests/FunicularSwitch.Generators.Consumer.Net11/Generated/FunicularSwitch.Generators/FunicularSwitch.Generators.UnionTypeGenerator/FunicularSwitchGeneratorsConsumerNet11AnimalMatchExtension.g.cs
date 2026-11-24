#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class AnimalMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Animal animal, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Cat, T> cat, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Dog, T> dog) =>
		animal switch
		{
			null => throw new global::System.ArgumentNullException(nameof(animal)),
			FunicularSwitch.Generators.Consumer.Net11.Cat cat1 => cat(cat1),
			FunicularSwitch.Generators.Consumer.Net11.Dog dog2 => dog(dog2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Animal animal, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Dog, global::System.Threading.Tasks.Task<T>> dog) =>
		animal switch
		{
			null => throw new global::System.ArgumentNullException(nameof(animal)),
			FunicularSwitch.Generators.Consumer.Net11.Cat cat1 => await cat(cat1).ConfigureAwait(false),
			FunicularSwitch.Generators.Consumer.Net11.Dog dog2 => await dog(dog2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Animal> animal, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Cat, T> cat, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Dog, T> dog) =>
		(await animal.ConfigureAwait(false)).Match(cat, dog);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Animal> animal, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Dog, global::System.Threading.Tasks.Task<T>> dog) =>
		await (await animal.ConfigureAwait(false)).Match(cat, dog).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Animal animal, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Cat> cat, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Dog> dog)
		{
			switch (animal)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(animal));
				case FunicularSwitch.Generators.Consumer.Net11.Cat cat1:
					cat(cat1);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Dog dog2:
					dog(dog2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Animal animal, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Dog, global::System.Threading.Tasks.Task> dog)
		{
			switch (animal)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(animal));
				case FunicularSwitch.Generators.Consumer.Net11.Cat cat1:
					await cat(cat1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Dog dog2:
					await dog(dog2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Animal> animal, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Cat> cat, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Dog> dog) =>
		(await animal.ConfigureAwait(false)).Switch(cat, dog);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Animal> animal, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Dog, global::System.Threading.Tasks.Task> dog) =>
		await (await animal.ConfigureAwait(false)).Switch(cat, dog).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
