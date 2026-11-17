//HintName: FunicularSwitchTestVariantMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class VariantMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Variant variant, global::System.Func<global::FunicularSwitch.Test.Cat?, T> cat, global::System.Func<int?, T> @int, global::System.Func<string?, T> @string, global::System.Func<bool, T> @bool) =>
		variant switch
		{
			null => cat(null),
			global::FunicularSwitch.Test.Cat cat1 => cat(cat1),
			int @int2 => @int(@int2),
			string @string3 => @string(@string3),
			bool @bool4 => @bool(@bool4),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Variant variant, global::System.Func<global::FunicularSwitch.Test.Cat?, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<int?, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<bool, global::System.Threading.Tasks.Task<T>> @bool) =>
		variant switch
		{
			null => await cat(null).ConfigureAwait(false),
			global::FunicularSwitch.Test.Cat cat1 => await cat(cat1).ConfigureAwait(false),
			int @int2 => await @int(@int2).ConfigureAwait(false),
			string @string3 => await @string(@string3).ConfigureAwait(false),
			bool @bool4 => await @bool(@bool4).ConfigureAwait(false),
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Variant> variant, global::System.Func<global::FunicularSwitch.Test.Cat?, T> cat, global::System.Func<int?, T> @int, global::System.Func<string?, T> @string, global::System.Func<bool, T> @bool) =>
		(await variant.ConfigureAwait(false)).Match(cat, @int, @string, @bool);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Variant> variant, global::System.Func<global::FunicularSwitch.Test.Cat?, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<int?, global::System.Threading.Tasks.Task<T>> @int, global::System.Func<string?, global::System.Threading.Tasks.Task<T>> @string, global::System.Func<bool, global::System.Threading.Tasks.Task<T>> @bool) =>
		await (await variant.ConfigureAwait(false)).Match(cat, @int, @string, @bool).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Variant variant, global::System.Action<global::FunicularSwitch.Test.Cat?> cat, global::System.Action<int?> @int, global::System.Action<string?> @string, global::System.Action<bool> @bool)
		{
			switch (variant)
			{
				case null:
					cat(null);
					break;
				case global::FunicularSwitch.Test.Cat cat1:
					cat(cat1);
					break;
				case int @int2:
					@int(@int2);
					break;
				case string @string3:
					@string(@string3);
					break;
				case bool @bool4:
					@bool(@bool4);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Variant variant, global::System.Func<global::FunicularSwitch.Test.Cat?, global::System.Threading.Tasks.Task> cat, global::System.Func<int?, global::System.Threading.Tasks.Task> @int, global::System.Func<string?, global::System.Threading.Tasks.Task> @string, global::System.Func<bool, global::System.Threading.Tasks.Task> @bool)
		{
			switch (variant)
			{
				case null:
					await cat(null).ConfigureAwait(false);
					break;
				case global::FunicularSwitch.Test.Cat cat1:
					await cat(cat1).ConfigureAwait(false);
					break;
				case int @int2:
					await @int(@int2).ConfigureAwait(false);
					break;
				case string @string3:
					await @string(@string3).ConfigureAwait(false);
					break;
				case bool @bool4:
					await @bool(@bool4).ConfigureAwait(false);
					break;
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Variant> variant, global::System.Action<global::FunicularSwitch.Test.Cat?> cat, global::System.Action<int?> @int, global::System.Action<string?> @string, global::System.Action<bool> @bool) =>
		(await variant.ConfigureAwait(false)).Switch(cat, @int, @string, @bool);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Variant> variant, global::System.Func<global::FunicularSwitch.Test.Cat?, global::System.Threading.Tasks.Task> cat, global::System.Func<int?, global::System.Threading.Tasks.Task> @int, global::System.Func<string?, global::System.Threading.Tasks.Task> @string, global::System.Func<bool, global::System.Threading.Tasks.Task> @bool) =>
		await (await variant.ConfigureAwait(false)).Switch(cat, @int, @string, @bool).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
