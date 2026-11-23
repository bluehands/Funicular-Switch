//HintName: UnitTestTreeOfTMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace Unit.Test
{
	public static partial class TreeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static TMatchResult Match<T, TMatchResult>(this global::Unit.Test.Tree<T> tree, global::System.Func<global::Unit.Test.Leaf<T>, TMatchResult> leaf) where T : class =>
		tree switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree)),
			global::Unit.Test.Leaf<T> leaf1 => leaf(leaf1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Tree: {tree.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::Unit.Test.Tree<T> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf) where T : class =>
		tree switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree)),
			global::Unit.Test.Leaf<T> leaf1 => await leaf(leaf1).ConfigureAwait(false),
			_ => throw new global::System.ArgumentException($"Unknown type derived from Unit.Test.Tree: {tree.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Func<global::Unit.Test.Leaf<T>, TMatchResult> leaf) where T : class =>
		(await tree.ConfigureAwait(false)).Match(leaf);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf) where T : class =>
		await (await tree.ConfigureAwait(false)).Match(leaf).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<T>(this global::Unit.Test.Tree<T> tree, global::System.Action<global::Unit.Test.Leaf<T>> leaf) where T : class
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
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::Unit.Test.Tree<T> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task> leaf) where T : class
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
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Action<global::Unit.Test.Leaf<T>> leaf) where T : class =>
		(await tree.ConfigureAwait(false)).Switch(leaf);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::Unit.Test.Tree<T>> tree, global::System.Func<global::Unit.Test.Leaf<T>, global::System.Threading.Tasks.Task> leaf) where T : class =>
		await (await tree.ConfigureAwait(false)).Switch(leaf).ConfigureAwait(false);
	}
	
	public abstract partial class Tree<T>
		where T : class
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static Unit.Test.Tree<T> Leaf(T? value, global::System.Collections.Generic.List<(T Item, T[] Items)> items, T? @event, global::System.Collections.Generic.Dictionary<string, T?[,]> values) => new global::Unit.Test.Leaf<T>(value, items)		
		{
			@event = @event,			
			Values = values			
		};
		[global::System.Diagnostics.DebuggerStepThrough]
		public static Unit.Test.Tree<T> Leaf(global::Unit.Test.Container<T>.Nested<T[]> nested, T? @event, global::System.Collections.Generic.Dictionary<string, T?[,]> values) => new global::Unit.Test.Leaf<T>(nested)		
		{
			@event = @event,			
			Values = values			
		};
	}
}
#pragma warning restore 1591
