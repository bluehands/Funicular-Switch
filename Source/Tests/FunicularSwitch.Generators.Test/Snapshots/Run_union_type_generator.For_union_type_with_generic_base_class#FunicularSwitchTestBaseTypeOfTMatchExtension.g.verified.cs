//HintName: FunicularSwitchTestBaseTypeOfTMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class BaseTypeMatchExtension
	{
		public static TMatchResult Match<T, TMatchResult>(this FunicularSwitch.Test.BaseType<T> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving_, TMatchResult> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2_, TMatchResult> deriving2) =>
		baseType switch
		{
			FunicularSwitch.Test.BaseType<T>.Deriving_ deriving1 => deriving(deriving1),
			FunicularSwitch.Test.BaseType<T>.Deriving2_ deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this FunicularSwitch.Test.BaseType<T> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving_, global::System.Threading.Tasks.Task<TMatchResult>> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2_, global::System.Threading.Tasks.Task<TMatchResult>> deriving2) =>
		baseType switch
		{
			FunicularSwitch.Test.BaseType<T>.Deriving_ deriving1 => deriving(deriving1),
			FunicularSwitch.Test.BaseType<T>.Deriving2_ deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.BaseType<T>> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving_, TMatchResult> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2_, TMatchResult> deriving2) =>
		(await baseType.ConfigureAwait(false)).Match(deriving, deriving2);
		
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.BaseType<T>> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving_, global::System.Threading.Tasks.Task<TMatchResult>> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2_, global::System.Threading.Tasks.Task<TMatchResult>> deriving2) =>
		await (await baseType.ConfigureAwait(false)).Match(deriving, deriving2).ConfigureAwait(false);
		
		public static void Switch<T>(this FunicularSwitch.Test.BaseType<T> baseType, global::System.Action<FunicularSwitch.Test.BaseType<T>.Deriving_> deriving, global::System.Action<FunicularSwitch.Test.BaseType<T>.Deriving2_> deriving2)
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.BaseType<T>.Deriving_ deriving1:
					deriving(deriving1);
					break;
				case FunicularSwitch.Test.BaseType<T>.Deriving2_ deriving22:
					deriving2(deriving22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T>(this FunicularSwitch.Test.BaseType<T> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving_, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2_, global::System.Threading.Tasks.Task> deriving2)
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.BaseType<T>.Deriving_ deriving1:
					await deriving(deriving1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.BaseType<T>.Deriving2_ deriving22:
					await deriving2(deriving22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.BaseType<T>> baseType, global::System.Action<FunicularSwitch.Test.BaseType<T>.Deriving_> deriving, global::System.Action<FunicularSwitch.Test.BaseType<T>.Deriving2_> deriving2) =>
		(await baseType.ConfigureAwait(false)).Switch(deriving, deriving2);
		
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.BaseType<T>> baseType, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving_, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2_, global::System.Threading.Tasks.Task> deriving2) =>
		await (await baseType.ConfigureAwait(false)).Switch(deriving, deriving2).ConfigureAwait(false);
	}
	
	public abstract partial record BaseType<T>
	{
		public static FunicularSwitch.Test.BaseType<T> Deriving(string Value, T Other) => new FunicularSwitch.Test.BaseType<T>.Deriving_(Value, Other);
		public static FunicularSwitch.Test.BaseType<T> Deriving2(string Value) => new FunicularSwitch.Test.BaseType<T>.Deriving2_(Value);
	}
}
#pragma warning restore 1591
