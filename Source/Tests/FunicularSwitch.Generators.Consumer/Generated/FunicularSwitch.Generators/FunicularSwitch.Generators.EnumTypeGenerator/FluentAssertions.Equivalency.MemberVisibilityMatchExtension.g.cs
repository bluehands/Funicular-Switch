#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Equivalency
{
	public static partial class MemberVisibilityMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.MemberVisibility memberVisibility, Func<T> @internal, Func<T> none, Func<T> @public) =>
		memberVisibility switch
		{
			FluentAssertions.Equivalency.MemberVisibility.Internal => @internal(),
			FluentAssertions.Equivalency.MemberVisibility.None => none(),
			FluentAssertions.Equivalency.MemberVisibility.Public => @public(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.MemberVisibility: {memberVisibility.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Equivalency.MemberVisibility memberVisibility, Func<Task<T>> @internal, Func<Task<T>> none, Func<Task<T>> @public) =>
		memberVisibility switch
		{
			FluentAssertions.Equivalency.MemberVisibility.Internal => @internal(),
			FluentAssertions.Equivalency.MemberVisibility.None => none(),
			FluentAssertions.Equivalency.MemberVisibility.Public => @public(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.MemberVisibility: {memberVisibility.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.MemberVisibility> memberVisibility, Func<T> @internal, Func<T> none, Func<T> @public) =>
		(await memberVisibility.ConfigureAwait(false)).Match(@internal, none, @public);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.MemberVisibility> memberVisibility, Func<Task<T>> @internal, Func<Task<T>> none, Func<Task<T>> @public) =>
		await (await memberVisibility.ConfigureAwait(false)).Match(@internal, none, @public).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.MemberVisibility memberVisibility, Action @internal, Action none, Action @public)
		{
			switch (memberVisibility)
			{
				case FluentAssertions.Equivalency.MemberVisibility.Internal:
					@internal();
					break;
				case FluentAssertions.Equivalency.MemberVisibility.None:
					none();
					break;
				case FluentAssertions.Equivalency.MemberVisibility.Public:
					@public();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.MemberVisibility: {memberVisibility.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Equivalency.MemberVisibility memberVisibility, Func<Task> @internal, Func<Task> none, Func<Task> @public)
		{
			switch (memberVisibility)
			{
				case FluentAssertions.Equivalency.MemberVisibility.Internal:
					await @internal().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.MemberVisibility.None:
					await none().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.MemberVisibility.Public:
					await @public().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.MemberVisibility: {memberVisibility.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.MemberVisibility> memberVisibility, Action @internal, Action none, Action @public) =>
		(await memberVisibility.ConfigureAwait(false)).Switch(@internal, none, @public);
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.MemberVisibility> memberVisibility, Func<Task> @internal, Func<Task> none, Func<Task> @public) =>
		await (await memberVisibility.ConfigureAwait(false)).Switch(@internal, none, @public).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
