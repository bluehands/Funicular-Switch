#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class WithPrimaryConstructorMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, global::System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, T> derived) =>
		withPrimaryConstructor switch
		{
			FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor derived1 => derived(derived1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, global::System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, global::System.Threading.Tasks.Task<T>> derived) =>
		withPrimaryConstructor switch
		{
			FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor derived1 => derived(derived1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, global::System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, T> derived) =>
		(await withPrimaryConstructor.ConfigureAwait(false)).Match(derived);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, global::System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, global::System.Threading.Tasks.Task<T>> derived) =>
		await (await withPrimaryConstructor.ConfigureAwait(false)).Match(derived).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, global::System.Action<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor> derived)
		{
			switch (withPrimaryConstructor)
			{
				case FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor derived1:
					derived(derived1);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, global::System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, global::System.Threading.Tasks.Task> derived)
		{
			switch (withPrimaryConstructor)
			{
				case FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor derived1:
					await derived(derived1).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, global::System.Action<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor> derived) =>
		(await withPrimaryConstructor.ConfigureAwait(false)).Switch(derived);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, global::System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, global::System.Threading.Tasks.Task> derived) =>
		await (await withPrimaryConstructor.ConfigureAwait(false)).Switch(derived).ConfigureAwait(false);
	}
	
	public abstract partial class WithPrimaryConstructor
	{
		public static FunicularSwitch.Generators.Consumer.WithPrimaryConstructor Derived(string message, int test) => new FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor(message, test);
	}
}
#pragma warning restore 1591
