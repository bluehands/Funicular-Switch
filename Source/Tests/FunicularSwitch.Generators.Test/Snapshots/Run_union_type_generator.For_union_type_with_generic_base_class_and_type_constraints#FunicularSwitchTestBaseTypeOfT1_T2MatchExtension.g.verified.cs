//HintName: FunicularSwitchTestBaseTypeOfT1_T2MatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseTypeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T1, T2, T>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_, T> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_, T> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		baseType switch
		{
			FunicularSwitch.Test.BaseType<T1, T2>.Deriving_ deriving1 => deriving(deriving1),
			FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_ deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static global::System.Threading.Tasks.Task<T> Match<T1, T2, T>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_, global::System.Threading.Tasks.Task<T>> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_, global::System.Threading.Tasks.Task<T>> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		baseType switch
		{
			FunicularSwitch.Test.BaseType<T1, T2>.Deriving_ deriving1 => deriving(deriving1),
			FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_ deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T1, T2, T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_, T> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_, T> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		(await baseType.ConfigureAwait(false)).Match(deriving, deriving2);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T1, T2, T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_, global::System.Threading.Tasks.Task<T>> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_, global::System.Threading.Tasks.Task<T>> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		await (await baseType.ConfigureAwait(false)).Match(deriving, deriving2).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch<T1, T2>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Action<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_> deriving, global::System.Action<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int>
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.BaseType<T1, T2>.Deriving_ deriving1:
					deriving(deriving1);
					break;
				case FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_ deriving22:
					deriving2(deriving22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T1, T2>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_, global::System.Threading.Tasks.Task> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int>
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.BaseType<T1, T2>.Deriving_ deriving1:
					await deriving(deriving1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_ deriving22:
					await deriving2(deriving22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T1, T2>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Action<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_> deriving, global::System.Action<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		(await baseType.ConfigureAwait(false)).Switch(deriving, deriving2);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch<T1, T2>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving_, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_, global::System.Threading.Tasks.Task> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		await (await baseType.ConfigureAwait(false)).Switch(deriving, deriving2).ConfigureAwait(false);
	}
	
	public abstract partial record BaseType<T1, T2>
		where T1 : notnull, new()

		where T2 : class, global::System.Collections.Generic.IEnumerable<int>
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Test.BaseType<T1, T2> Deriving(string Value, T1 Other, T2 List) => new FunicularSwitch.Test.BaseType<T1, T2>.Deriving_(Value, Other, List);
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Test.BaseType<T1, T2> Deriving2(string Value) => new FunicularSwitch.Test.BaseType<T1, T2>.Deriving2_(Value);
	}
}
#pragma warning restore 1591
