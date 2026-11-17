#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class TreeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static TMatchResult Match<T, TMatchResult>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree<T> tree, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>, TMatchResult> leaf, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Node<T>, TMatchResult> node) =>
		tree switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree)),
			FunicularSwitch.Generators.Consumer.Net11.Leaf<T> leaf1 => leaf(leaf1),
			FunicularSwitch.Generators.Consumer.Net11.Node<T> node2 => node(node2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree<T> tree, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Node<T>, global::System.Threading.Tasks.Task<TMatchResult>> node) =>
		tree switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree)),
			FunicularSwitch.Generators.Consumer.Net11.Leaf<T> leaf1 => await leaf(leaf1).ConfigureAwait(false),
			FunicularSwitch.Generators.Consumer.Net11.Node<T> node2 => await node(node2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree<T>> tree, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>, TMatchResult> leaf, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Node<T>, TMatchResult> node) =>
		(await tree.ConfigureAwait(false)).Match(leaf, node);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree<T>> tree, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Node<T>, global::System.Threading.Tasks.Task<TMatchResult>> node) =>
		await (await tree.ConfigureAwait(false)).Match(leaf, node).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree<T> tree, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>> leaf, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Node<T>> node)
		{
			switch (tree)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(tree));
				case FunicularSwitch.Generators.Consumer.Net11.Leaf<T> leaf1:
					leaf(leaf1);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Node<T> node2:
					node(node2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree<T> tree, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>, global::System.Threading.Tasks.Task> leaf, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Node<T>, global::System.Threading.Tasks.Task> node)
		{
			switch (tree)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(tree));
				case FunicularSwitch.Generators.Consumer.Net11.Leaf<T> leaf1:
					await leaf(leaf1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Node<T> node2:
					await node(node2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree<T>> tree, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>> leaf, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Node<T>> node) =>
		(await tree.ConfigureAwait(false)).Switch(leaf, node);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree<T>> tree, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Leaf<T>, global::System.Threading.Tasks.Task> leaf, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Node<T>, global::System.Threading.Tasks.Task> node) =>
		await (await tree.ConfigureAwait(false)).Switch(leaf, node).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
