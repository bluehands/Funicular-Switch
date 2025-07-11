#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class HypermediaObjectKeyReference3MatchExtension
	{
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3 hypermediaObjectKeyReference3, global::System.Func<FunicularSwitch.Generators.Consumer.KeyReference_2, T> keyReference_2) =>
		hypermediaObjectKeyReference3 switch
		{
			FunicularSwitch.Generators.Consumer.KeyReference_2 keyReference_21 => keyReference_2(keyReference_21),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3: {hypermediaObjectKeyReference3.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3 hypermediaObjectKeyReference3, global::System.Func<FunicularSwitch.Generators.Consumer.KeyReference_2, global::System.Threading.Tasks.Task<T>> keyReference_2) =>
		hypermediaObjectKeyReference3 switch
		{
			FunicularSwitch.Generators.Consumer.KeyReference_2 keyReference_21 => keyReference_2(keyReference_21),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3: {hypermediaObjectKeyReference3.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3> hypermediaObjectKeyReference3, global::System.Func<FunicularSwitch.Generators.Consumer.KeyReference_2, T> keyReference_2) =>
		(await hypermediaObjectKeyReference3.ConfigureAwait(false)).Match(keyReference_2);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3> hypermediaObjectKeyReference3, global::System.Func<FunicularSwitch.Generators.Consumer.KeyReference_2, global::System.Threading.Tasks.Task<T>> keyReference_2) =>
		await (await hypermediaObjectKeyReference3.ConfigureAwait(false)).Match(keyReference_2).ConfigureAwait(false);
		
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3 hypermediaObjectKeyReference3, global::System.Action<FunicularSwitch.Generators.Consumer.KeyReference_2> keyReference_2)
		{
			switch (hypermediaObjectKeyReference3)
			{
				case FunicularSwitch.Generators.Consumer.KeyReference_2 keyReference_21:
					keyReference_2(keyReference_21);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3: {hypermediaObjectKeyReference3.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3 hypermediaObjectKeyReference3, global::System.Func<FunicularSwitch.Generators.Consumer.KeyReference_2, global::System.Threading.Tasks.Task> keyReference_2)
		{
			switch (hypermediaObjectKeyReference3)
			{
				case FunicularSwitch.Generators.Consumer.KeyReference_2 keyReference_21:
					await keyReference_2(keyReference_21).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3: {hypermediaObjectKeyReference3.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3> hypermediaObjectKeyReference3, global::System.Action<FunicularSwitch.Generators.Consumer.KeyReference_2> keyReference_2) =>
		(await hypermediaObjectKeyReference3.ConfigureAwait(false)).Switch(keyReference_2);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3> hypermediaObjectKeyReference3, global::System.Func<FunicularSwitch.Generators.Consumer.KeyReference_2, global::System.Threading.Tasks.Task> keyReference_2) =>
		await (await hypermediaObjectKeyReference3.ConfigureAwait(false)).Switch(keyReference_2).ConfigureAwait(false);
	}
	
	public abstract partial record HypermediaObjectKeyReference3
	{
		public static FunicularSwitch.Generators.Consumer.HypermediaObjectKeyReference3 KeyReference_2() => new FunicularSwitch.Generators.Consumer.KeyReference_2();
	}
}
#pragma warning restore 1591
