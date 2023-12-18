#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	public static partial class WithPrimaryConstructorMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, T> derivedWithPrimaryConstructor) =>
		withPrimaryConstructor switch
		{
			FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1 => derivedWithPrimaryConstructor(case1),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, Task<T>> derivedWithPrimaryConstructor) =>
		withPrimaryConstructor switch
		{
			FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1 => derivedWithPrimaryConstructor(case1),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, T> derivedWithPrimaryConstructor) =>
		(await withPrimaryConstructor.ConfigureAwait(false)).Match(derivedWithPrimaryConstructor);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, Task<T>> derivedWithPrimaryConstructor) =>
		await (await withPrimaryConstructor.ConfigureAwait(false)).Match(derivedWithPrimaryConstructor).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, Action<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor> derivedWithPrimaryConstructor)
		{
			switch (withPrimaryConstructor)
			{
				case FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1:
					derivedWithPrimaryConstructor(case1);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Generators.Consumer.WithPrimaryConstructor withPrimaryConstructor, Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, Task> derivedWithPrimaryConstructor)
		{
			switch (withPrimaryConstructor)
			{
				case FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor case1:
					await derivedWithPrimaryConstructor(case1).ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.WithPrimaryConstructor: {withPrimaryConstructor.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, Action<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor> derivedWithPrimaryConstructor) =>
		(await withPrimaryConstructor.ConfigureAwait(false)).Switch(derivedWithPrimaryConstructor);
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.WithPrimaryConstructor> withPrimaryConstructor, Func<FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor, Task> derivedWithPrimaryConstructor) =>
		await (await withPrimaryConstructor.ConfigureAwait(false)).Switch(derivedWithPrimaryConstructor).ConfigureAwait(false);
	}
	
	public abstract partial class WithPrimaryConstructor
	{
		public static FunicularSwitch.Generators.Consumer.WithPrimaryConstructor DerivedWithPrimaryConstructor(string Blubs, int test) => new FunicularSwitch.Generators.Consumer.DerivedWithPrimaryConstructor(Blubs, test);
	}
}
#pragma warning restore 1591
