#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Equivalency
{
	public static partial class OrderStrictnessMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, Func<T> irrelevant, Func<T> notStrict, Func<T> strict) =>
		orderStrictness switch
		{
			FluentAssertions.Equivalency.OrderStrictness.Irrelevant => irrelevant(),
			FluentAssertions.Equivalency.OrderStrictness.NotStrict => notStrict(),
			FluentAssertions.Equivalency.OrderStrictness.Strict => strict(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, Func<Task<T>> irrelevant, Func<Task<T>> notStrict, Func<Task<T>> strict) =>
		orderStrictness switch
		{
			FluentAssertions.Equivalency.OrderStrictness.Irrelevant => irrelevant(),
			FluentAssertions.Equivalency.OrderStrictness.NotStrict => notStrict(),
			FluentAssertions.Equivalency.OrderStrictness.Strict => strict(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, Func<T> irrelevant, Func<T> notStrict, Func<T> strict) =>
		(await orderStrictness.ConfigureAwait(false)).Match(irrelevant, notStrict, strict);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, Func<Task<T>> irrelevant, Func<Task<T>> notStrict, Func<Task<T>> strict) =>
		await (await orderStrictness.ConfigureAwait(false)).Match(irrelevant, notStrict, strict).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, Action irrelevant, Action notStrict, Action strict)
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
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Equivalency.OrderStrictness orderStrictness, Func<Task> irrelevant, Func<Task> notStrict, Func<Task> strict)
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
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.OrderStrictness: {orderStrictness.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, Action irrelevant, Action notStrict, Action strict) =>
		(await orderStrictness.ConfigureAwait(false)).Switch(irrelevant, notStrict, strict);
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.OrderStrictness> orderStrictness, Func<Task> irrelevant, Func<Task> notStrict, Func<Task> strict) =>
		await (await orderStrictness.ConfigureAwait(false)).Switch(irrelevant, notStrict, strict).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
