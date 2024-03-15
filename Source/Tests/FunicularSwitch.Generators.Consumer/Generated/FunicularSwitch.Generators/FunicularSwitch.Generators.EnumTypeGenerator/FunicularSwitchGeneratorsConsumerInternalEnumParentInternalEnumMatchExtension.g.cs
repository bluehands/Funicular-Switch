#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class InternalEnumParent_InternalEnumMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, global::System.Func<T> one, global::System.Func<T> two) =>
		internalEnum switch
		{
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.One => one(),
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.Two => two(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
		internalEnum switch
		{
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.One => one(),
			FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum.Two => two(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, global::System.Func<T> one, global::System.Func<T> two) =>
		(await internalEnum.ConfigureAwait(false)).Match(one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
		await (await internalEnum.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, global::System.Action one, global::System.Action two)
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
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum internalEnum, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two)
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
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum: {internalEnum.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, global::System.Action one, global::System.Action two) =>
		(await internalEnum.ConfigureAwait(false)).Switch(one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.InternalEnumParent.InternalEnum> internalEnum, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two) =>
		await (await internalEnum.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
