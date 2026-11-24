#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class VehicleMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Vehicle vehicle, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, T> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, T> dog2, global::System.Func<int, T> @int) =>
		vehicle switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Generators.Consumer.Net11.Vehicle has no value (default instance of the union struct)"),
			global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21 => cat2(cat21),
			global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22 => dog2(dog22),
			int @int3 => @int(@int3),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Vehicle vehicle, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task<T>> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task<T>> dog2, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int) =>
		vehicle switch
		{
			null => throw new global::System.InvalidOperationException("Union FunicularSwitch.Generators.Consumer.Net11.Vehicle has no value (default instance of the union struct)"),
			global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21 => await cat2(cat21).ConfigureAwait(false),
			global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22 => await dog2(dog22).ConfigureAwait(false),
			int @int3 => await @int(@int3).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Vehicle> vehicle, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, T> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, T> dog2, global::System.Func<int, T> @int) =>
		(await vehicle.ConfigureAwait(false)).Match(cat2, dog2, @int);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Vehicle> vehicle, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task<T>> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task<T>> dog2, global::System.Func<int, global::System.Threading.Tasks.Task<T>> @int) =>
		await (await vehicle.ConfigureAwait(false)).Match(cat2, dog2, @int).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Vehicle vehicle, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Cat2> cat2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Dog2> dog2, global::System.Action<int> @int)
		{
			switch (vehicle)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Generators.Consumer.Net11.Vehicle has no value (default instance of the union struct)");
				case global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21:
					cat2(cat21);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22:
					dog2(dog22);
					break;
				case int @int3:
					@int(@int3);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Vehicle vehicle, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task> dog2, global::System.Func<int, global::System.Threading.Tasks.Task> @int)
		{
			switch (vehicle)
			{
				case null:
					throw new global::System.InvalidOperationException("Union FunicularSwitch.Generators.Consumer.Net11.Vehicle has no value (default instance of the union struct)");
				case global::FunicularSwitch.Generators.Consumer.Net11.Cat2 cat21:
					await cat2(cat21).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Dog2 dog22:
					await dog2(dog22).ConfigureAwait(false);
					break;
				case int @int3:
					await @int(@int3).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Vehicle> vehicle, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Cat2> cat2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Dog2> dog2, global::System.Action<int> @int) =>
		(await vehicle.ConfigureAwait(false)).Switch(cat2, dog2, @int);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Vehicle> vehicle, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Cat2, global::System.Threading.Tasks.Task> cat2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Dog2, global::System.Threading.Tasks.Task> dog2, global::System.Func<int, global::System.Threading.Tasks.Task> @int) =>
		await (await vehicle.ConfigureAwait(false)).Switch(cat2, dog2, @int).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
