//HintName: UnitTestTreeOfTMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace Unit.Test
{
	public static partial class TreeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static TMatchResult Match<T, TMatchResult>(this global::Unit.Test.Tree<T> tree, global::System.Func<global::Unit.Test.Leaf<T>, TMatchResult> leaf) =>
		tree switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree)),
			global::Unit.Test.Leaf<T> leaf1 => leaf(leaf1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Tree: {tree.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::Unit.Test.Tree<T> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf) =>
		tree switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree)),
			global::Unit.Test.Leaf<T> leaf1 => await leaf(leaf1).ConfigureAwait(false),
			_ => throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Tree: {tree.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Func<global::Unit.Test.Leaf<T>, TMatchResult> leaf) =>
		(await tree.ConfigureAwait(false)).Match(leaf);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf) =>
		await (await tree.ConfigureAwait(false)).Match(leaf).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<T>(this global::Unit.Test.Tree<T> tree, global::System.Action<global::Unit.Test.Leaf<T>> leaf)
		{
			switch (tree)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(tree));
				case global::Unit.Test.Leaf<T> leaf1:
					leaf(leaf1);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Tree: {tree.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::Unit.Test.Tree<T> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task> leaf)
		{
			switch (tree)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(tree));
				case global::Unit.Test.Leaf<T> leaf1:
					await leaf(leaf1).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Tree: {tree.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Action<global::Unit.Test.Leaf<T>> leaf) =>
		(await tree.ConfigureAwait(false)).Switch(leaf);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task> leaf) =>
		await (await tree.ConfigureAwait(false)).Switch(leaf).ConfigureAwait(false);
	}
	
	public abstract partial record Tree<T>
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static Unit.Test.Tree<T> Leaf(T Value) => new global::Unit.Test.Leaf<T>(Value);
	}
}
#pragma warning restore 1591
