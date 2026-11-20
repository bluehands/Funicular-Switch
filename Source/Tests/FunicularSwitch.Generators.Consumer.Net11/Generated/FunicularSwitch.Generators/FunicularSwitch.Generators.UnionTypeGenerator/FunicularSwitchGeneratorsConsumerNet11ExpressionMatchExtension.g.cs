#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class ExpressionMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Expression expression, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Add, T> @add, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Constant, T> constant, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Multiply, T> multiply) =>
		expression switch
		{
			null => throw new global::System.ArgumentNullException(nameof(expression)),
			FunicularSwitch.Generators.Consumer.Net11.Add @add1 => @add(@add1),
			FunicularSwitch.Generators.Consumer.Net11.Constant constant2 => constant(constant2),
			FunicularSwitch.Generators.Consumer.Net11.Multiply multiply3 => multiply(multiply3),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Expression expression, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Add, global::System.Threading.Tasks.Task<T>> @add, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Constant, global::System.Threading.Tasks.Task<T>> constant, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Multiply, global::System.Threading.Tasks.Task<T>> multiply) =>
		expression switch
		{
			null => throw new global::System.ArgumentNullException(nameof(expression)),
			FunicularSwitch.Generators.Consumer.Net11.Add @add1 => await @add(@add1).ConfigureAwait(false),
			FunicularSwitch.Generators.Consumer.Net11.Constant constant2 => await constant(constant2).ConfigureAwait(false),
			FunicularSwitch.Generators.Consumer.Net11.Multiply multiply3 => await multiply(multiply3).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Expression> expression, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Add, T> @add, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Constant, T> constant, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Multiply, T> multiply) =>
		(await expression.ConfigureAwait(false)).Match(@add, constant, multiply);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Expression> expression, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Add, global::System.Threading.Tasks.Task<T>> @add, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Constant, global::System.Threading.Tasks.Task<T>> constant, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Multiply, global::System.Threading.Tasks.Task<T>> multiply) =>
		await (await expression.ConfigureAwait(false)).Match(@add, constant, multiply).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Expression expression, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Add> @add, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Constant> constant, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Multiply> multiply)
		{
			switch (expression)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(expression));
				case FunicularSwitch.Generators.Consumer.Net11.Add @add1:
					@add(@add1);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Constant constant2:
					constant(constant2);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Multiply multiply3:
					multiply(multiply3);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Expression expression, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Add, global::System.Threading.Tasks.Task> @add, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Constant, global::System.Threading.Tasks.Task> constant, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Multiply, global::System.Threading.Tasks.Task> multiply)
		{
			switch (expression)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(expression));
				case FunicularSwitch.Generators.Consumer.Net11.Add @add1:
					await @add(@add1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Constant constant2:
					await constant(constant2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Multiply multiply3:
					await multiply(multiply3).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Expression> expression, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Add> @add, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Constant> constant, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Multiply> multiply) =>
		(await expression.ConfigureAwait(false)).Switch(@add, constant, multiply);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Expression> expression, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Add, global::System.Threading.Tasks.Task> @add, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Constant, global::System.Threading.Tasks.Task> constant, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Multiply, global::System.Threading.Tasks.Task> multiply) =>
		await (await expression.ConfigureAwait(false)).Switch(@add, constant, multiply).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
