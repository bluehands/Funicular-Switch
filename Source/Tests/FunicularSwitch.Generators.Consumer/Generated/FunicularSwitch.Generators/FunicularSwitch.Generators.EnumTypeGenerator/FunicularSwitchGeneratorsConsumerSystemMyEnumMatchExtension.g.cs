#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer.System
{
	public static partial class MyEnumMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.System.MyEnum myEnum, global::System.Func<T> one, global::System.Func<T> two) =>
		myEnum switch
		{
			FunicularSwitch.Generators.Consumer.System.MyEnum.One => one(),
			FunicularSwitch.Generators.Consumer.System.MyEnum.Two => two(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.System.MyEnum: {myEnum.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.System.MyEnum myEnum, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
		myEnum switch
		{
			FunicularSwitch.Generators.Consumer.System.MyEnum.One => one(),
			FunicularSwitch.Generators.Consumer.System.MyEnum.Two => two(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.System.MyEnum: {myEnum.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.System.MyEnum> myEnum, global::System.Func<T> one, global::System.Func<T> two) =>
		(await myEnum.ConfigureAwait(false)).Match(one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.System.MyEnum> myEnum, global::System.Func<global::System.Threading.Tasks.Task<T>> one, global::System.Func<global::System.Threading.Tasks.Task<T>> two) =>
		await (await myEnum.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.System.MyEnum myEnum, global::System.Action one, global::System.Action two)
		{
			switch (myEnum)
			{
				case FunicularSwitch.Generators.Consumer.System.MyEnum.One:
					one();
					break;
				case FunicularSwitch.Generators.Consumer.System.MyEnum.Two:
					two();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.System.MyEnum: {myEnum.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.System.MyEnum myEnum, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two)
		{
			switch (myEnum)
			{
				case FunicularSwitch.Generators.Consumer.System.MyEnum.One:
					await one().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.System.MyEnum.Two:
					await two().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.System.MyEnum: {myEnum.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.System.MyEnum> myEnum, global::System.Action one, global::System.Action two) =>
		(await myEnum.ConfigureAwait(false)).Switch(one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.System.MyEnum> myEnum, global::System.Func<global::System.Threading.Tasks.Task> one, global::System.Func<global::System.Threading.Tasks.Task> two) =>
		await (await myEnum.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
