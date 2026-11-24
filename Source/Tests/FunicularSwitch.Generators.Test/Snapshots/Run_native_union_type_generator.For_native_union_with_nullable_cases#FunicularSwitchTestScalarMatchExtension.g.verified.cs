//HintName: FunicularSwitchTestScalarMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class ScalarMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Scalar scalar, global::System.Func<int?, T> @int, global::System.Func<string?, T> @string, global::System.Func<bool, T> @bool) =>
		scalar switch
		{
			null => @int(null),
			int @int1 => @int(@int1),
			string @string2 => @string(@string2),
			bool @bool3 => @bool(@bool3),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Scalar scalar, global::System.Func<int?, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<bool, global::System.Threading.Tasks.Task<T>> @bool) =>
		scalar switch
		{
			null => await @int(null).ConfigureAwait(false),
			int @int1 => await @int(@int1).ConfigureAwait(false),
			string @string2 => await @string(@string2).ConfigureAwait(false),
			bool @bool3 => await @bool(@bool3).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Scalar> scalar, global::System.Func<int?, T> @int, global::System.Func<string?, T> @string, global::System.Func<bool, T> @bool) =>
		(await scalar.ConfigureAwait(false)).Match(@int, @string, @bool);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Scalar> scalar, global::System.Func<int?, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<bool, global::System.Threading.Tasks.Task<T>> @bool) =>
		await (await scalar.ConfigureAwait(false)).Match(@int, @string, @bool).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Scalar scalar, global::System.Action<int?> @int, global::System.Action<string?> @string, global::System.Action<bool> @bool)
		{
			switch (scalar)
			{
				case null:
					@int(null);
					break;
				case int @int1:
					@int(@int1);
					break;
				case string @string2:
					@string(@string2);
					break;
				case bool @bool3:
					@bool(@bool3);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Scalar scalar, global::System.Func<int?, global::System.Threading.Tasks.Task> @int, global::System.Func<string?, global::System.Threading.Tasks.Task> @string, global::System.Func<bool, global::System.Threading.Tasks.Task> @bool)
		{
			switch (scalar)
			{
				case null:
					await @int(null).ConfigureAwait(false);
					break;
				case int @int1:
					await @int(@int1).ConfigureAwait(false);
					break;
				case string @string2:
					await @string(@string2).ConfigureAwait(false);
					break;
				case bool @bool3:
					await @bool(@bool3).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Scalar> scalar, global::System.Action<int?> @int, global::System.Action<string?> @string, global::System.Action<bool> @bool) =>
		(await scalar.ConfigureAwait(false)).Switch(@int, @string, @bool);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Scalar> scalar, global::System.Func<int?, global::System.Threading.Tasks.Task> @int, global::System.Func<string?, global::System.Threading.Tasks.Task> @string, global::System.Func<bool, global::System.Threading.Tasks.Task> @bool) =>
		await (await scalar.ConfigureAwait(false)).Switch(@int, @string, @bool).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
