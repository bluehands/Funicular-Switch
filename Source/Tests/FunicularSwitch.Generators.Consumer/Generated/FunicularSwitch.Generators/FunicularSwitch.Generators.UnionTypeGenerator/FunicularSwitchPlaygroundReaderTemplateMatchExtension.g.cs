#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Playground
{
	internal static partial class ReaderTemplateMatchExtension
	{
		public static T Match<T>(this global::FunicularSwitch.Playground.ReaderTemplate readerTemplate, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Compound_, T> compound, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Include_, T> include, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Quote_, T> quote, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Text_, T> text, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Variable_, T> variable) =>
		readerTemplate switch
		{
			FunicularSwitch.Playground.ReaderTemplate.Compound_ compound1 => compound(compound1),
			FunicularSwitch.Playground.ReaderTemplate.Include_ include2 => include(include2),
			FunicularSwitch.Playground.ReaderTemplate.Quote_ quote3 => quote(quote3),
			FunicularSwitch.Playground.ReaderTemplate.Text_ text4 => text(text4),
			FunicularSwitch.Playground.ReaderTemplate.Variable_ variable5 => variable(variable5),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Playground.ReaderTemplate: {readerTemplate.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Playground.ReaderTemplate readerTemplate, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Compound_, global::System.Threading.Tasks.Task<T>> compound, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Include_, global::System.Threading.Tasks.Task<T>> include, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Quote_, global::System.Threading.Tasks.Task<T>> quote, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Text_, global::System.Threading.Tasks.Task<T>> text, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Variable_, global::System.Threading.Tasks.Task<T>> variable) =>
		readerTemplate switch
		{
			FunicularSwitch.Playground.ReaderTemplate.Compound_ compound1 => compound(compound1),
			FunicularSwitch.Playground.ReaderTemplate.Include_ include2 => include(include2),
			FunicularSwitch.Playground.ReaderTemplate.Quote_ quote3 => quote(quote3),
			FunicularSwitch.Playground.ReaderTemplate.Text_ text4 => text(text4),
			FunicularSwitch.Playground.ReaderTemplate.Variable_ variable5 => variable(variable5),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Playground.ReaderTemplate: {readerTemplate.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.ReaderTemplate> readerTemplate, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Compound_, T> compound, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Include_, T> include, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Quote_, T> quote, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Text_, T> text, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Variable_, T> variable) =>
		(await readerTemplate.ConfigureAwait(false)).Match(compound, include, quote, text, variable);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.ReaderTemplate> readerTemplate, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Compound_, global::System.Threading.Tasks.Task<T>> compound, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Include_, global::System.Threading.Tasks.Task<T>> include, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Quote_, global::System.Threading.Tasks.Task<T>> quote, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Text_, global::System.Threading.Tasks.Task<T>> text, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Variable_, global::System.Threading.Tasks.Task<T>> variable) =>
		await (await readerTemplate.ConfigureAwait(false)).Match(compound, include, quote, text, variable).ConfigureAwait(false);
		
		public static void Switch(this global::FunicularSwitch.Playground.ReaderTemplate readerTemplate, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Compound_> compound, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Include_> include, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Quote_> quote, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Text_> text, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Variable_> variable)
		{
			switch (readerTemplate)
			{
				case FunicularSwitch.Playground.ReaderTemplate.Compound_ compound1:
					compound(compound1);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Include_ include2:
					include(include2);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Quote_ quote3:
					quote(quote3);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Text_ text4:
					text(text4);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Variable_ variable5:
					variable(variable5);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Playground.ReaderTemplate: {readerTemplate.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Playground.ReaderTemplate readerTemplate, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Compound_, global::System.Threading.Tasks.Task> compound, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Include_, global::System.Threading.Tasks.Task> include, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Quote_, global::System.Threading.Tasks.Task> quote, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Text_, global::System.Threading.Tasks.Task> text, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Variable_, global::System.Threading.Tasks.Task> variable)
		{
			switch (readerTemplate)
			{
				case FunicularSwitch.Playground.ReaderTemplate.Compound_ compound1:
					await compound(compound1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Include_ include2:
					await include(include2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Quote_ quote3:
					await quote(quote3).ConfigureAwait(false);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Text_ text4:
					await text(text4).ConfigureAwait(false);
					break;
				case FunicularSwitch.Playground.ReaderTemplate.Variable_ variable5:
					await variable(variable5).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Playground.ReaderTemplate: {readerTemplate.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.ReaderTemplate> readerTemplate, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Compound_> compound, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Include_> include, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Quote_> quote, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Text_> text, global::System.Action<FunicularSwitch.Playground.ReaderTemplate.Variable_> variable) =>
		(await readerTemplate.ConfigureAwait(false)).Switch(compound, include, quote, text, variable);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.ReaderTemplate> readerTemplate, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Compound_, global::System.Threading.Tasks.Task> compound, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Include_, global::System.Threading.Tasks.Task> include, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Quote_, global::System.Threading.Tasks.Task> quote, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Text_, global::System.Threading.Tasks.Task> text, global::System.Func<FunicularSwitch.Playground.ReaderTemplate.Variable_, global::System.Threading.Tasks.Task> variable) =>
		await (await readerTemplate.ConfigureAwait(false)).Switch(compound, include, quote, text, variable).ConfigureAwait(false);
	}
	
	internal abstract partial record ReaderTemplate
	{
		public static FunicularSwitch.Playground.ReaderTemplate Compound(global::System.Collections.Generic.IReadOnlyList<global::FunicularSwitch.Playground.ReaderTemplate> Templates) => new FunicularSwitch.Playground.ReaderTemplate.Compound_(Templates);
		public static FunicularSwitch.Playground.ReaderTemplate Include(global::FunicularSwitch.Playground.ReaderTemplate Template, global::System.Collections.Generic.IReadOnlyList<global::FunicularSwitch.Playground.ReaderDefinition> Definitions) => new FunicularSwitch.Playground.ReaderTemplate.Include_(Template, Definitions);
		public static FunicularSwitch.Playground.ReaderTemplate Quote(global::FunicularSwitch.Playground.ReaderTemplate Template) => new FunicularSwitch.Playground.ReaderTemplate.Quote_(Template);
		public static FunicularSwitch.Playground.ReaderTemplate Text(string Value) => new FunicularSwitch.Playground.ReaderTemplate.Text_(Value);
		public static FunicularSwitch.Playground.ReaderTemplate Variable(global::FunicularSwitch.Playground.ReaderTemplate Template) => new FunicularSwitch.Playground.ReaderTemplate.Variable_(Template);
	}
}
#pragma warning restore 1591
