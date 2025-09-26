#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class BaseMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Base @base, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseA_, T> caseA, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseB_, T> caseB) =>
		@base switch
		{
			FunicularSwitch.Generators.Consumer.Base.CaseA_ caseA1 => caseA(caseA1),
			FunicularSwitch.Generators.Consumer.Base.CaseB_ caseB2 => caseB(caseB2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Base: {@base.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Base @base, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseA_, global::System.Threading.Tasks.Task<T>> caseA, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseB_, global::System.Threading.Tasks.Task<T>> caseB) =>
		@base switch
		{
			FunicularSwitch.Generators.Consumer.Base.CaseA_ caseA1 => caseA(caseA1),
			FunicularSwitch.Generators.Consumer.Base.CaseB_ caseB2 => caseB(caseB2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Base: {@base.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Base> @base, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseA_, T> caseA, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseB_, T> caseB) =>
		(await @base.ConfigureAwait(false)).Match(caseA, caseB);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Base> @base, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseA_, global::System.Threading.Tasks.Task<T>> caseA, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseB_, global::System.Threading.Tasks.Task<T>> caseB) =>
		await (await @base.ConfigureAwait(false)).Match(caseA, caseB).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Base @base, global::System.Action<FunicularSwitch.Generators.Consumer.Base.CaseA_> caseA, global::System.Action<FunicularSwitch.Generators.Consumer.Base.CaseB_> caseB)
		{
			switch (@base)
			{
				case FunicularSwitch.Generators.Consumer.Base.CaseA_ caseA1:
					caseA(caseA1);
					break;
				case FunicularSwitch.Generators.Consumer.Base.CaseB_ caseB2:
					caseB(caseB2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Base: {@base.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Base @base, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseA_, global::System.Threading.Tasks.Task> caseA, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseB_, global::System.Threading.Tasks.Task> caseB)
		{
			switch (@base)
			{
				case FunicularSwitch.Generators.Consumer.Base.CaseA_ caseA1:
					await caseA(caseA1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Base.CaseB_ caseB2:
					await caseB(caseB2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.Base: {@base.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Base> @base, global::System.Action<FunicularSwitch.Generators.Consumer.Base.CaseA_> caseA, global::System.Action<FunicularSwitch.Generators.Consumer.Base.CaseB_> caseB) =>
		(await @base.ConfigureAwait(false)).Switch(caseA, caseB);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Base> @base, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseA_, global::System.Threading.Tasks.Task> caseA, global::System.Func<FunicularSwitch.Generators.Consumer.Base.CaseB_, global::System.Threading.Tasks.Task> caseB) =>
		await (await @base.ConfigureAwait(false)).Switch(caseA, caseB).ConfigureAwait(false);
	}
	
	public partial record Base
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Generators.Consumer.Base CaseA(string Text) => new FunicularSwitch.Generators.Consumer.Base.CaseA_(Text);
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Generators.Consumer.Base CaseB(int Number) => new FunicularSwitch.Generators.Consumer.Base.CaseB_(Number);
	}
}
#pragma warning restore 1591
