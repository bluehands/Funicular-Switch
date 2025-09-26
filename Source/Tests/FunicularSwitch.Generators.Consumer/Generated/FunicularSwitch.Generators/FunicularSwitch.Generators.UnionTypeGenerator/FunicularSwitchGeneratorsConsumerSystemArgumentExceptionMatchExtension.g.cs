#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.System
{
	public static partial class ArgumentExceptionMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.System.ArgumentException argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_, T> action, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_, T> func) =>
		argumentException switch
		{
			FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_ action1 => action(action1),
			FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_ func2 => func(func2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.System.ArgumentException: {argumentException.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.System.ArgumentException argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_, global::System.Threading.Tasks.Task<T>> action, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_, global::System.Threading.Tasks.Task<T>> func) =>
		argumentException switch
		{
			FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_ action1 => action(action1),
			FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_ func2 => func(func2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.System.ArgumentException: {argumentException.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.System.ArgumentException> argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_, T> action, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_, T> func) =>
		(await argumentException.ConfigureAwait(false)).Match(action, func);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.System.ArgumentException> argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_, global::System.Threading.Tasks.Task<T>> action, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_, global::System.Threading.Tasks.Task<T>> func) =>
		await (await argumentException.ConfigureAwait(false)).Match(action, func).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.System.ArgumentException argumentException, global::System.Action<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_> action, global::System.Action<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_> func)
		{
			switch (argumentException)
			{
				case FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_ action1:
					action(action1);
					break;
				case FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_ func2:
					func(func2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.System.ArgumentException: {argumentException.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.System.ArgumentException argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_, global::System.Threading.Tasks.Task> action, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_, global::System.Threading.Tasks.Task> func)
		{
			switch (argumentException)
			{
				case FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_ action1:
					await action(action1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_ func2:
					await func(func2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.System.ArgumentException: {argumentException.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.System.ArgumentException> argumentException, global::System.Action<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_> action, global::System.Action<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_> func) =>
		(await argumentException.ConfigureAwait(false)).Switch(action, func);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.System.ArgumentException> argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_, global::System.Threading.Tasks.Task> action, global::System.Func<FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_, global::System.Threading.Tasks.Task> func) =>
		await (await argumentException.ConfigureAwait(false)).Switch(action, func).ConfigureAwait(false);
	}
	
	public abstract partial record ArgumentException
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Generators.Consumer.System.ArgumentException Action() => new FunicularSwitch.Generators.Consumer.System.ArgumentException.Action_();
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Generators.Consumer.System.ArgumentException Func() => new FunicularSwitch.Generators.Consumer.System.ArgumentException.Func_();
	}
}
#pragma warning restore 1591
