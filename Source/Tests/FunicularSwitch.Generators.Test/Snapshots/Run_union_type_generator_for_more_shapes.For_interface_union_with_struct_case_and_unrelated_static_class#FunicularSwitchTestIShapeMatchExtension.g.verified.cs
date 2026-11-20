//HintName: FunicularSwitchTestIShapeMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class IShapeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.IShape iShape, global::System.Func<FunicularSwitch.Test.Circle, T> circle, global::System.Func<FunicularSwitch.Test.Derived, T> derived, global::System.Func<FunicularSwitch.Test.Point, T> point) =>
		iShape switch
		{
			null => throw new global::System.ArgumentNullException(nameof(iShape)),
			FunicularSwitch.Test.Circle circle1 => circle(circle1),
			FunicularSwitch.Test.Derived derived2 => derived(derived2),
			FunicularSwitch.Test.Point point3 => point(point3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IShape: {iShape.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.IShape iShape, global::System.Func<FunicularSwitch.Test.Circle, global::System.Threading.Tasks.Task<T>> circle, global::System.Func<FunicularSwitch.Test.Derived, global::System.Threading.Tasks.Task<T>> derived, global::System.Func<FunicularSwitch.Test.Point, global::System.Threading.Tasks.Task<T>> point) =>
		iShape switch
		{
			null => throw new global::System.ArgumentNullException(nameof(iShape)),
			FunicularSwitch.Test.Circle circle1 => await circle(circle1).ConfigureAwait(false),
			FunicularSwitch.Test.Derived derived2 => await derived(derived2).ConfigureAwait(false),
			FunicularSwitch.Test.Point point3 => await point(point3).ConfigureAwait(false),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IShape: {iShape.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.IShape> iShape, global::System.Func<FunicularSwitch.Test.Circle, T> circle, global::System.Func<FunicularSwitch.Test.Derived, T> derived, global::System.Func<FunicularSwitch.Test.Point, T> point) =>
		(await iShape.ConfigureAwait(false)).Match(circle, derived, point);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.IShape> iShape, global::System.Func<FunicularSwitch.Test.Circle, global::System.Threading.Tasks.Task<T>> circle, global::System.Func<FunicularSwitch.Test.Derived, global::System.Threading.Tasks.Task<T>> derived, global::System.Func<FunicularSwitch.Test.Point, global::System.Threading.Tasks.Task<T>> point) =>
		await (await iShape.ConfigureAwait(false)).Match(circle, derived, point).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.IShape iShape, global::System.Action<FunicularSwitch.Test.Circle> circle, global::System.Action<FunicularSwitch.Test.Derived> derived, global::System.Action<FunicularSwitch.Test.Point> point)
		{
			switch (iShape)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(iShape));
				case FunicularSwitch.Test.Circle circle1:
					circle(circle1);
					break;
				case FunicularSwitch.Test.Derived derived2:
					derived(derived2);
					break;
				case FunicularSwitch.Test.Point point3:
					point(point3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IShape: {iShape.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.IShape iShape, global::System.Func<FunicularSwitch.Test.Circle, global::System.Threading.Tasks.Task> circle, global::System.Func<FunicularSwitch.Test.Derived, global::System.Threading.Tasks.Task> derived, global::System.Func<FunicularSwitch.Test.Point, global::System.Threading.Tasks.Task> point)
		{
			switch (iShape)
			{
				case null:
					throw new global::System.ArgumentNullException(nameof(iShape));
				case FunicularSwitch.Test.Circle circle1:
					await circle(circle1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Derived derived2:
					await derived(derived2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Point point3:
					await point(point3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.IShape: {iShape.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.IShape> iShape, global::System.Action<FunicularSwitch.Test.Circle> circle, global::System.Action<FunicularSwitch.Test.Derived> derived, global::System.Action<FunicularSwitch.Test.Point> point) =>
		(await iShape.ConfigureAwait(false)).Switch(circle, derived, point);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.IShape> iShape, global::System.Func<FunicularSwitch.Test.Circle, global::System.Threading.Tasks.Task> circle, global::System.Func<FunicularSwitch.Test.Derived, global::System.Threading.Tasks.Task> derived, global::System.Func<FunicularSwitch.Test.Point, global::System.Threading.Tasks.Task> point) =>
		await (await iShape.ConfigureAwait(false)).Switch(circle, derived, point).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
