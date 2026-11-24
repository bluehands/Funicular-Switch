#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer.Net11
{
	public static partial class ShapeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Shape shape, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Circle, T> circle, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Rectangle, T> rectangle) =>
		shape switch
		{
			null => throw new global::System.ArgumentNullException(nameof(shape)),
			FunicularSwitch.Generators.Consumer.Net11.Circle circle1 => circle(circle1),
			FunicularSwitch.Generators.Consumer.Net11.Rectangle rectangle2 => rectangle(rectangle2),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.Net11.Shape shape, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Circle, global::System.Threading.Tasks.Task<T>> circle, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Rectangle, global::System.Threading.Tasks.Task<T>> rectangle) =>
		shape switch
		{
			null => throw new global::System.ArgumentNullException(nameof(shape)),
			FunicularSwitch.Generators.Consumer.Net11.Circle circle1 => await circle(circle1).ConfigureAwait(false),
			FunicularSwitch.Generators.Consumer.Net11.Rectangle rectangle2 => await rectangle(rectangle2).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Shape> shape, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Circle, T> circle, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Rectangle, T> rectangle) =>
		(await shape.ConfigureAwait(false)).Match(circle, rectangle);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Shape> shape, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Circle, global::System.Threading.Tasks.Task<T>> circle, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Rectangle, global::System.Threading.Tasks.Task<T>> rectangle) =>
		await (await shape.ConfigureAwait(false)).Match(circle, rectangle).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Shape shape, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Circle> circle, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Rectangle> rectangle)
		{
			switch (shape)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(shape));
				case FunicularSwitch.Generators.Consumer.Net11.Circle circle1:
					circle(circle1);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Rectangle rectangle2:
					rectangle(rectangle2);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.Net11.Shape shape, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Circle, global::System.Threading.Tasks.Task> circle, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Rectangle, global::System.Threading.Tasks.Task> rectangle)
		{
			switch (shape)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(shape));
				case FunicularSwitch.Generators.Consumer.Net11.Circle circle1:
					await circle(circle1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.Net11.Rectangle rectangle2:
					await rectangle(rectangle2).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Shape> shape, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Circle> circle, global::System.Action<FunicularSwitch.Generators.Consumer.Net11.Rectangle> rectangle) =>
		(await shape.ConfigureAwait(false)).Switch(circle, rectangle);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Net11.Shape> shape, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Circle, global::System.Threading.Tasks.Task> circle, global::System.Func<FunicularSwitch.Generators.Consumer.Net11.Rectangle, global::System.Threading.Tasks.Task> rectangle) =>
		await (await shape.ConfigureAwait(false)).Switch(circle, rectangle).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
