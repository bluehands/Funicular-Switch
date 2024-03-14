#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class WithPrimaryConstructorMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, T> derivedWithPrimaryConstructor) =>
		withPrimaryConstructor switch
		{
			FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1 => derivedWithPrimaryConstructor(case1),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, System.Threading.Tasks.Task<T>> derivedWithPrimaryConstructor) =>
		withPrimaryConstructor switch
		{
			FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1 => derivedWithPrimaryConstructor(case1),
			_ => throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, T> derivedWithPrimaryConstructor) =>
		(await withPrimaryConstructor.ConfigureAwait(false)).Match(derivedWithPrimaryConstructor);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, System.Threading.Tasks.Task<T>> derivedWithPrimaryConstructor) =>
		await (await withPrimaryConstructor.ConfigureAwait(false)).Match(derivedWithPrimaryConstructor).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, System.Action<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor> derivedWithPrimaryConstructor)
		{
			switch (withPrimaryConstructor)
			{
				case FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1:
					derivedWithPrimaryConstructor(case1);
					break;
				default:
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, System.Threading.Tasks.Task> derivedWithPrimaryConstructor)
		{
			switch (withPrimaryConstructor)
			{
				case FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1:
					await derivedWithPrimaryConstructor(case1).ConfigureAwait(false);
					break;
				default:
					throw new System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, System.Action<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor> derivedWithPrimaryConstructor) =>
		(await withPrimaryConstructor.ConfigureAwait(false)).Switch(derivedWithPrimaryConstructor);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, System.Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, System.Threading.Tasks.Task> derivedWithPrimaryConstructor) =>
		await (await withPrimaryConstructor.ConfigureAwait(false)).Switch(derivedWithPrimaryConstructor).ConfigureAwait(false);
	}
	
	public abstract partial class WithPrimaryConstructor
	{
		public static FunicularSwitch.Generators.Consumer.WithPrimaryConstructor DerivedWithPrimaryConstructor(string message, int test) => new FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor(message, test);
	}
}
#pragma warning restore 1591
