#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class InternalEnumParent_InternalEnumMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, Func<T> one, Func<T> two) =>
		internalEnum switch
		{
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.One => one(),
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.Two => two(),
			_ => throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, Func<Task<T>> one, Func<Task<T>> two) =>
		internalEnum switch
		{
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.One => one(),
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.Two => two(),
			_ => throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, Func<T> one, Func<T> two) =>
		(await internalEnum.ConfigureAwait(false)).Match(one, two);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, Func<Task<T>> one, Func<Task<T>> two) =>
		await (await internalEnum.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, Action one, Action two)
		{
			switch (internalEnum)
			{
				case FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.One:
					one();
					break;
				case FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.Two:
					two();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, Func<Task> one, Func<Task> two)
		{
			switch (internalEnum)
			{
				case FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.One:
					await one().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.Two:
					await two().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, Action one, Action two) =>
		(await internalEnum.ConfigureAwait(false)).Switch(one, two);
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, Func<Task> one, Func<Task> two) =>
		await (await internalEnum.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
