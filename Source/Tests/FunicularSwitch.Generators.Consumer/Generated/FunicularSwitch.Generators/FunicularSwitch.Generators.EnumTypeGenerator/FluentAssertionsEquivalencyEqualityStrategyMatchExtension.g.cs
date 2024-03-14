#pragma warning disable 1591
namespace FluentAssertions.Equivalency
{
	public static partial class EqualityStrategyMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, System.Func<T> @equals, System.Func<T> forceEquals, System.Func<T> forceMembers, System.Func<T> members) =>
		equalityStrategy switch
		{
			FluentAssertions.Equivalency.EqualityStrategy.Equals => @equals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceEquals => forceEquals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceMembers => forceMembers(),
			FluentAssertions.Equivalency.EqualityStrategy.Members => members(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, System.Func<System.Threading.Tasks.Task<T>> @equals, System.Func<System.Threading.Tasks.Task<T>> forceEquals, System.Func<System.Threading.Tasks.Task<T>> forceMembers, System.Func<System.Threading.Tasks.Task<T>> members) =>
		equalityStrategy switch
		{
			FluentAssertions.Equivalency.EqualityStrategy.Equals => @equals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceEquals => forceEquals(),
			FluentAssertions.Equivalency.EqualityStrategy.ForceMembers => forceMembers(),
			FluentAssertions.Equivalency.EqualityStrategy.Members => members(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, System.Func<T> @equals, System.Func<T> forceEquals, System.Func<T> forceMembers, System.Func<T> members) =>
		(await equalityStrategy.ConfigureAwait(false)).Match(@equals, forceEquals, forceMembers, members);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, System.Func<System.Threading.Tasks.Task<T>> @equals, System.Func<System.Threading.Tasks.Task<T>> forceEquals, System.Func<System.Threading.Tasks.Task<T>> forceMembers, System.Func<System.Threading.Tasks.Task<T>> members) =>
		await (await equalityStrategy.ConfigureAwait(false)).Match(@equals, forceEquals, forceMembers, members).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, System.Action @equals, System.Action forceEquals, System.Action forceMembers, System.Action members)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FluentAssertions.Equivalency.EqualityStrategy equalityStrategy, System.Func<System.Threading.Tasks.Task> @equals, System.Func<System.Threading.Tasks.Task> forceEquals, System.Func<System.Threading.Tasks.Task> forceMembers, System.Func<System.Threading.Tasks.Task> members)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Equivalency.EqualityStrategy: {equalityStrategy.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, System.Action @equals, System.Action forceEquals, System.Action forceMembers, System.Action members) =>
		(await equalityStrategy.ConfigureAwait(false)).Switch(@equals, forceEquals, forceMembers, members);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Equivalency.EqualityStrategy> equalityStrategy, System.Func<System.Threading.Tasks.Task> @equals, System.Func<System.Threading.Tasks.Task> forceEquals, System.Func<System.Threading.Tasks.Task> forceMembers, System.Func<System.Threading.Tasks.Task> members) =>
		await (await equalityStrategy.ConfigureAwait(false)).Switch(@equals, forceEquals, forceMembers, members).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
