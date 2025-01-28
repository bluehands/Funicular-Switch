#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	internal static partial class InnerMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.Outer.Inner inner, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.One, T> one, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.Two, T> two) =>
		inner switch
		{
			FunicularSwitch.Generators.Consumer.Outer.Inner.One one1 => one(one1),
			FunicularSwitch.Generators.Consumer.Outer.Inner.Two two2 => two(two2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Outer.Inner: {inner.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.Outer.Inner inner, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.Two, global::System.Threading.Tasks.Task<T>> two) =>
		inner switch
		{
			FunicularSwitch.Generators.Consumer.Outer.Inner.One one1 => one(one1),
			FunicularSwitch.Generators.Consumer.Outer.Inner.Two two2 => two(two2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Outer.Inner: {inner.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Outer.Inner> inner, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.One, T> one, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.Two, T> two) =>
		(await inner.ConfigureAwait(false)).Match(one, two);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Outer.Inner> inner, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await inner.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.Outer.Inner inner, global::System.Action<FunicularSwitch.Generators.Consumer.Outer.Inner.One> one, global::System.Action<FunicularSwitch.Generators.Consumer.Outer.Inner.Two> two)
		{
			switch (inner)
			{
				case FunicularSwitch.Generators.Consumer.Outer.Inner.One one1:
					one(one1);
					break;
				case FunicularSwitch.Generators.Consumer.Outer.Inner.Two two2:
					two(two2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Outer.Inner: {inner.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.Outer.Inner inner, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.Two, global::System.Threading.Tasks.Task> two)
		{
			switch (inner)
			{
				case FunicularSwitch.Generators.Consumer.Outer.Inner.One one1:
					await one(one1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Outer.Inner.Two two2:
					await two(two2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Outer.Inner: {inner.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Outer.Inner> inner, global::System.Action<FunicularSwitch.Generators.Consumer.Outer.Inner.One> one, global::System.Action<FunicularSwitch.Generators.Consumer.Outer.Inner.Two> two) =>
		(await inner.ConfigureAwait(false)).Switch(one, two);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.Outer.Inner> inner, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Generators.Consumer.Outer.Inner.Two, global::System.Threading.Tasks.Task> two) =>
		await (await inner.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
	
	internal abstract partial record Inner
	{
	}
}
#pragma warning restore 1591
