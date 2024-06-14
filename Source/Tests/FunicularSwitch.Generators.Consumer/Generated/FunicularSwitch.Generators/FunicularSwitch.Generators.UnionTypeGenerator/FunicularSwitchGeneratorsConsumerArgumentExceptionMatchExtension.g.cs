#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class ArgumentExceptionMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.ArgumentException argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Action_, T> action, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Func_, T> func) =>
		argumentException switch
		{
			FunicularSwitch.Generators.Consumer.ArgumentException.Action_ action1 => action(action1),
			FunicularSwitch.Generators.Consumer.ArgumentException.Func_ func2 => func(func2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.ArgumentException: {argumentException.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.ArgumentException argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Action_, global::System.Threading.Tasks.Task<T>> action, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Func_, global::System.Threading.Tasks.Task<T>> func) =>
		argumentException switch
		{
			FunicularSwitch.Generators.Consumer.ArgumentException.Action_ action1 => action(action1),
			FunicularSwitch.Generators.Consumer.ArgumentException.Func_ func2 => func(func2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.ArgumentException: {argumentException.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.ArgumentException> argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Action_, T> action, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Func_, T> func) =>
		(await argumentException.ConfigureAwait(false)).Match(action, func);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.ArgumentException> argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Action_, global::System.Threading.Tasks.Task<T>> action, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Func_, global::System.Threading.Tasks.Task<T>> func) =>
		await (await argumentException.ConfigureAwait(false)).Match(action, func).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.ArgumentException argumentException, global::System.Action<FunicularSwitch.Generators.Consumer.ArgumentException.Action_> action, global::System.Action<FunicularSwitch.Generators.Consumer.ArgumentException.Func_> func)
		{
			switch (argumentException)
			{
				case FunicularSwitch.Generators.Consumer.ArgumentException.Action_ action1:
					action(action1);
					break;
				case FunicularSwitch.Generators.Consumer.ArgumentException.Func_ func2:
					func(func2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.ArgumentException: {argumentException.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.ArgumentException argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Action_, global::System.Threading.Tasks.Task> action, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Func_, global::System.Threading.Tasks.Task> func)
		{
			switch (argumentException)
			{
				case FunicularSwitch.Generators.Consumer.ArgumentException.Action_ action1:
					await action(action1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.ArgumentException.Func_ func2:
					await func(func2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.ArgumentException: {argumentException.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.ArgumentException> argumentException, global::System.Action<FunicularSwitch.Generators.Consumer.ArgumentException.Action_> action, global::System.Action<FunicularSwitch.Generators.Consumer.ArgumentException.Func_> func) =>
		(await argumentException.ConfigureAwait(false)).Switch(action, func);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.ArgumentException> argumentException, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Action_, global::System.Threading.Tasks.Task> action, global::System.Func<FunicularSwitch.Generators.Consumer.ArgumentException.Func_, global::System.Threading.Tasks.Task> func) =>
		await (await argumentException.ConfigureAwait(false)).Switch(action, func).ConfigureAwait(false);
	}
	
	public abstract partial record ArgumentException
	{
		public static FunicularSwitch.Generators.Consumer.ArgumentException Action() => new FunicularSwitch.Generators.Consumer.ArgumentException.Action_();
		public static FunicularSwitch.Generators.Consumer.ArgumentException Func() => new FunicularSwitch.Generators.Consumer.ArgumentException.Func_();
	}
}
#pragma warning restore 1591
