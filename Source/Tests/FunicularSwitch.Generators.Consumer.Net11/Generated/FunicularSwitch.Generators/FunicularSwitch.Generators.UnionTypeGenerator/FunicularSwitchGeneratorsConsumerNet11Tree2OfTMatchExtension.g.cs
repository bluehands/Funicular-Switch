#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class Tree2MatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static TMatchResult Match<T, TMatchResult>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T> tree2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>, TMatchResult> leaf2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>, TMatchResult> node2) =>
		tree2 switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree2)),
			global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T> leaf21 => leaf2(leaf21),
			global::FunicularSwitch.Generators.Consumer.Net11.Node2<T> node22 => node2(node22),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T> tree2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>, global::System.Threading.Tasks.Task<TMatchResult>> node2) =>
		tree2 switch
		{
			null => throw new global::System.ArgumentNullException(nameof(tree2)),
			global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T> leaf21 => await leaf2(leaf21).ConfigureAwait(false),
			global::FunicularSwitch.Generators.Consumer.Net11.Node2<T> node22 => await node2(node22).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T>> tree2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>, TMatchResult> leaf2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>, TMatchResult> node2) =>
		(await tree2.ConfigureAwait(false)).Match(leaf2, node2);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T>> tree2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>, global::System.Threading.Tasks.Task<TMatchResult>> leaf2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>, global::System.Threading.Tasks.Task<TMatchResult>> node2) =>
		await (await tree2.ConfigureAwait(false)).Match(leaf2, node2).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T> tree2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>> leaf2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>> node2)
		{
			switch (tree2)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(tree2));
				case global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T> leaf21:
					leaf2(leaf21);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Node2<T> node22:
					node2(node22);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T> tree2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>, global::System.Threading.Tasks.Task> leaf2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>, global::System.Threading.Tasks.Task> node2)
		{
			switch (tree2)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(tree2));
				case global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T> leaf21:
					await leaf2(leaf21).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Generators.Consumer.Net11.Node2<T> node22:
					await node2(node22).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T>> tree2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>> leaf2, global::System.Action<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>> node2) =>
		(await tree2.ConfigureAwait(false)).Switch(leaf2, node2);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Tree2<T>> tree2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Leaf2<T>, global::System.Threading.Tasks.Task> leaf2, global::System.Func<global::FunicularSwitch.Generators.Consumer.Net11.Node2<T>, global::System.Threading.Tasks.Task> node2) =>
		await (await tree2.ConfigureAwait(false)).Switch(leaf2, node2).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
