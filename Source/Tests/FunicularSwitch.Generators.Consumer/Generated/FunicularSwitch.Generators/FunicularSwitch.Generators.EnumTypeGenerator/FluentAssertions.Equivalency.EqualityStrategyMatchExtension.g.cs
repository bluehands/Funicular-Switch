#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Equivalency
{
	public static partial class EqualityStrategyMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, Func<T> @equals, Func<T> forceEquals, Func<T> forceMembers, Func<T> members) =>
		equalityStrategy switch
		{
			FluentAssertions.Equivalency.EqualityStrategy.Equals => @equals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceEquals => forceEquals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceMembers => forceMembers(),
			FluentAssertions.Equivalency.EqualityStrategy.Members => members(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, Func<Task<T>> @equals, Func<Task<T>> forceEquals, Func<Task<T>> forceMembers, Func<Task<T>> members) =>
		equalityStrategy switch
		{
			FluentAssertions.Equivalency.EqualityStrategy.Equals => @equals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceEquals => forceEquals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceMembers => forceMembers(),
			FluentAssertions.Equivalency.EqualityStrategy.Members => members(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, Func<T> @equals, Func<T> forceEquals, Func<T> forceMembers, Func<T> members) =>
		(await equalityStrategy.ConfigureAwait(false)).Match(@equals, forceEquals, forceMembers, members);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, Func<Task<T>> @equals, Func<Task<T>> forceEquals, Func<Task<T>> forceMembers, Func<Task<T>> members) =>
		await (await equalityStrategy.ConfigureAwait(false)).Match(@equals, forceEquals, forceMembers, members).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, Action @equals, Action forceEquals, Action forceMembers, Action members)
		{
			switch (equalityStrategy)
			{
				case FluentAssertions.Equivalency.EqualityStrategy.Equals:
					@equals();
					break;
				case FluentAssertions.Equivalency.EqualityStrategy.ForceEquals:
					forceEquals();
					break;
				case FluentAssertions.Equivalency.EqualityStrategy.ForceMembers:
					forceMembers();
					break;
				case FluentAssertions.Equivalency.EqualityStrategy.Members:
					members();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, Func<Task> @equals, Func<Task> forceEquals, Func<Task> forceMembers, Func<Task> members)
		{
			switch (equalityStrategy)
			{
				case FluentAssertions.Equivalency.EqualityStrategy.Equals:
					await @equals().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.EqualityStrategy.ForceEquals:
					await forceEquals().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.EqualityStrategy.ForceMembers:
					await forceMembers().ConfigureAwait(false);
					break;
				case FluentAssertions.Equivalency.EqualityStrategy.Members:
					await members().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, Action @equals, Action forceEquals, Action forceMembers, Action members) =>
		(await equalityStrategy.ConfigureAwait(false)).Switch(@equals, forceEquals, forceMembers, members);
		
		public static async Task Switch(this Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, Func<Task> @equals, Func<Task> forceEquals, Func<Task> forceMembers, Func<Task> members) =>
		await (await equalityStrategy.ConfigureAwait(false)).Switch(@equals, forceEquals, forceMembers, members).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
