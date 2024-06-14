#pragma warning disable 1591
namespace StandardMinLangVersion
{
	public static partial class MyUnionMatchExtension
	{
		public static T Match<T>(this StandardMinLangVersion.MyUnion myUnion, global::System.Func<StandardMinLangVersion.MyUnion.Case1_, T> case1, global::System.Func<StandardMinLangVersion.MyUnion.MyUnionCase2, T> case2) =>
		myUnion switch
		{
			StandardMinLangVersion.MyUnion.Case1_ case11 => case1(case11),
			StandardMinLangVersion.MyUnion.MyUnionCase2 case22 => case2(case22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from StandardMinLangVersion.MyUnion: {myUnion.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this StandardMinLangVersion.MyUnion myUnion, global::System.Func<StandardMinLangVersion.MyUnion.Case1_, global::System.Threading.Tasks.Task<T>> case1, global::System.Func<StandardMinLangVersion.MyUnion.MyUnionCase2, global::System.Threading.Tasks.Task<T>> case2) =>
		myUnion switch
		{
			StandardMinLangVersion.MyUnion.Case1_ case11 => case1(case11),
			StandardMinLangVersion.MyUnion.MyUnionCase2 case22 => case2(case22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from StandardMinLangVersion.MyUnion: {myUnion.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<StandardMinLangVersion.MyUnion> myUnion, global::System.Func<StandardMinLangVersion.MyUnion.Case1_, T> case1, global::System.Func<StandardMinLangVersion.MyUnion.MyUnionCase2, T> case2) =>
		(await myUnion.ConfigureAwait(false)).Match(case1, case2);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<StandardMinLangVersion.MyUnion> myUnion, global::System.Func<StandardMinLangVersion.MyUnion.Case1_, global::System.Threading.Tasks.Task<T>> case1, global::System.Func<StandardMinLangVersion.MyUnion.MyUnionCase2, global::System.Threading.Tasks.Task<T>> case2) =>
		await (await myUnion.ConfigureAwait(false)).Match(case1, case2).ConfigureAwait(false);
		
		public static void Switch(this StandardMinLangVersion.MyUnion myUnion, global::System.Action<StandardMinLangVersion.MyUnion.Case1_> case1, global::System.Action<StandardMinLangVersion.MyUnion.MyUnionCase2> case2)
		{
			switch (myUnion)
			{
				case StandardMinLangVersion.MyUnion.Case1_ case11:
					case1(case11);
					break;
				case StandardMinLangVersion.MyUnion.MyUnionCase2 case22:
					case2(case22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from StandardMinLangVersion.MyUnion: {myUnion.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this StandardMinLangVersion.MyUnion myUnion, global::System.Func<StandardMinLangVersion.MyUnion.Case1_, global::System.Threading.Tasks.Task> case1, global::System.Func<StandardMinLangVersion.MyUnion.MyUnionCase2, global::System.Threading.Tasks.Task> case2)
		{
			switch (myUnion)
			{
				case StandardMinLangVersion.MyUnion.Case1_ case11:
					await case1(case11).ConfigureAwait(false);
					break;
				case StandardMinLangVersion.MyUnion.MyUnionCase2 case22:
					await case2(case22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from StandardMinLangVersion.MyUnion: {myUnion.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<StandardMinLangVersion.MyUnion> myUnion, global::System.Action<StandardMinLangVersion.MyUnion.Case1_> case1, global::System.Action<StandardMinLangVersion.MyUnion.MyUnionCase2> case2) =>
		(await myUnion.ConfigureAwait(false)).Switch(case1, case2);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<StandardMinLangVersion.MyUnion> myUnion, global::System.Func<StandardMinLangVersion.MyUnion.Case1_, global::System.Threading.Tasks.Task> case1, global::System.Func<StandardMinLangVersion.MyUnion.MyUnionCase2, global::System.Threading.Tasks.Task> case2) =>
		await (await myUnion.ConfigureAwait(false)).Switch(case1, case2).ConfigureAwait(false);
	}
	
	public abstract partial class MyUnion
	{
		public static StandardMinLangVersion.MyUnion Case1() => new StandardMinLangVersion.MyUnion.Case1_();
		public static StandardMinLangVersion.MyUnion Case2() => new StandardMinLangVersion.MyUnion.MyUnionCase2();
	}
}
#pragma warning restore 1591
