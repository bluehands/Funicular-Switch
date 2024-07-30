#pragma warning disable 1591
namespace FluentAssertions.Equivalency
{
	internal static partial class OrderStrictnessMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, global::System.Func<T> irrelevant, global::System.Func<T> notStrict, global::System.Func<T> strict) =>
		orderStrictness switch
		{
			FluentAssertions.Equivalency.OrderStrictness.Irrelevant => irrelevant(),
			FluentAssertions.Equivalency.OrderStrictness.NotStrict => notStrict(),
			FluentAssertions.Equivalency.OrderStrictness.Strict => strict(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, global::System.Func<global::System.Threading.Tasks.Task<T>> irrelevant, global::System.Func<global::System.Threading.Tasks.Task<T>> notStrict, global::System.Func<global::System.Threading.Tasks.Task<T>> strict) =>
		orderStrictness switch
		{
			FluentAssertions.Equivalency.OrderStrictness.Irrelevant => irrelevant(),
			FluentAssertions.Equivalency.OrderStrictness.NotStrict => notStrict(),
			FluentAssertions.Equivalency.OrderStrictness.Strict => strict(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, global::System.Func<T> irrelevant, global::System.Func<T> notStrict, global::System.Func<T> strict) =>
		(await orderStrictness.ConfigureAwait(false)).Match(irrelevant, notStrict, strict);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, global::System.Func<global::System.Threading.Tasks.Task<T>> irrelevant, global::System.Func<global::System.Threading.Tasks.Task<T>> notStrict, global::System.Func<global::System.Threading.Tasks.Task<T>> strict) =>
		await (await orderStrictness.ConfigureAwait(false)).Match(irrelevant, notStrict, strict).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, global::System.Action irrelevant, global::System.Action notStrict, global::System.Action strict)
		{
			switch (orderStrictness)
			{
				case FluentAssertions.Equivalency.OrderStrictness.Irrelevant:
					irrelevant();
					break;
				case FluentAssertions.Equivalency.OrderStrictness.NotStrict:
					notStrict();
					break;
				case FluentAssertions.Equivalency.OrderStrictness.Strict:
					strict();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, global::System.Func<global::System.Threading.Tasks.Task> irrelevant, global::System.Func<global::System.Threading.Tasks.Task> notStrict, global::System.Func<global::System.Threading.Tasks.Task> strict)
		{
			switch (orderStrictness)
			{
				case FluentAssertions.Equivalency.OrderStrictness.Irrelevant:
					await irrelevant().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.OrderStrictness.NotStrict:
					await notStrict().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.OrderStrictness.Strict:
					await strict().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, global::System.Action irrelevant, global::System.Action notStrict, global::System.Action strict) =>
		(await orderStrictness.ConfigureAwait(false)).Switch(irrelevant, notStrict, strict);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, global::System.Func<global::System.Threading.Tasks.Task> irrelevant, global::System.Func<global::System.Threading.Tasks.Task> notStrict, global::System.Func<global::System.Threading.Tasks.Task> strict) =>
		await (await orderStrictness.ConfigureAwait(false)).Switch(irrelevant, notStrict, strict).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
