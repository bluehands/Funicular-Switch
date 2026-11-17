//HintName: FunicularSwitchTestThingMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class ThingMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Thing thing, global::System.Func<global::FunicularSwitch.Test.Animal, T> animal, global::System.Func<global::FunicularSwitch.Test.Dog, T> dog, global::System.Func<string, T> @string) =>
		thing switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			global::FunicularSwitch.Test.Dog dog2 => dog(dog2),
			global::FunicularSwitch.Test.Animal animal1 => animal(animal1),
			string @string3 => @string(@string3),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Thing thing, global::System.Func<global::FunicularSwitch.Test.Animal, global::System.Threading.Tasks.Task<T>> animal, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task<T>> dog, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string) =>
		thing switch
		{
			null => throw new global::System.InvalidOperationException("Union has no value"),
			global::FunicularSwitch.Test.Dog dog2 => await dog(dog2).ConfigureAwait(false),
			global::FunicularSwitch.Test.Animal animal1 => await animal(animal1).ConfigureAwait(false),
			string @string3 => await @string(@string3).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Thing> thing, global::System.Func<global::FunicularSwitch.Test.Animal, T> animal, global::System.Func<global::FunicularSwitch.Test.Dog, T> dog, global::System.Func<string, T> @string) =>
		(await thing.ConfigureAwait(false)).Match(animal, dog, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Thing> thing, global::System.Func<global::FunicularSwitch.Test.Animal, global::System.Threading.Tasks.Task<T>> animal, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task<T>> dog, global::System.Func<string, global::System.Threading.Tasks.Task<T>> @string) =>
		await (await thing.ConfigureAwait(false)).Match(animal, dog, @string).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Thing thing, global::System.Action<global::FunicularSwitch.Test.Animal> animal, global::System.Action<global::FunicularSwitch.Test.Dog> dog, global::System.Action<string> @string)
		{
			switch (thing)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case global::FunicularSwitch.Test.Dog dog2:
					dog(dog2);
					break;
				case global::FunicularSwitch.Test.Animal animal1:
					animal(animal1);
					break;
				case string @string3:
					@string(@string3);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Thing thing, global::System.Func<global::FunicularSwitch.Test.Animal, global::System.Threading.Tasks.Task> animal, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task> dog, global::System.Func<string, global::System.Threading.Tasks.Task> @string)
		{
			switch (thing)
			{
				case null:
					throw new global::System.InvalidOperationException("Union has no value");
				case global::FunicularSwitch.Test.Dog dog2:
					await dog(dog2).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Test.Animal animal1:
					await animal(animal1).ConfigureAwait(false);
					break;
				case string @string3:
					await @string(@string3).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Thing> thing, global::System.Action<global::FunicularSwitch.Test.Animal> animal, global::System.Action<global::FunicularSwitch.Test.Dog> dog, global::System.Action<string> @string) =>
		(await thing.ConfigureAwait(false)).Switch(animal, dog, @string);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Thing> thing, global::System.Func<global::FunicularSwitch.Test.Animal, global::System.Threading.Tasks.Task> animal, global::System.Func<global::FunicularSwitch.Test.Dog, global::System.Threading.Tasks.Task> dog, global::System.Func<string, global::System.Threading.Tasks.Task> @string) =>
		await (await thing.ConfigureAwait(false)).Switch(animal, dog, @string).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
