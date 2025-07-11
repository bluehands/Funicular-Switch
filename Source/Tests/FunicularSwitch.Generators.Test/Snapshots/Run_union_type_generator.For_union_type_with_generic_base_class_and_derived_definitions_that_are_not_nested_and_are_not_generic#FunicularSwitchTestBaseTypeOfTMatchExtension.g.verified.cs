//HintName: FunicularSwitchTestBaseTypeOfTMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseTypeMatchExtension
	{
		public static TMatchResult Match<T, TMatchResult>(this global::FunicularSwitch.Test.BaseType<T> baseType, global::System.Func<FunicularSwitch.Test.Deriving, TMatchResult> deriving) =>
		baseType switch
		{
			FunicularSwitch.Test.Deriving deriving1 => deriving(deriving1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::FunicularSwitch.Test.BaseType<T> baseType, global::System.Func<FunicularSwitch.Test.Deriving, global::System.Threading.Tasks.Task<TMatchResult>> deriving) =>
		baseType switch
		{
			FunicularSwitch.Test.Deriving deriving1 => deriving(deriving1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T>> baseType, global::System.Func<FunicularSwitch.Test.Deriving, TMatchResult> deriving) =>
		(await baseType.ConfigureAwait(false)).Match(deriving);
		
		public static async global::System.Threading.Tasks.Task<TMatchResult> Match<T, TMatchResult>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T>> baseType, global::System.Func<FunicularSwitch.Test.Deriving, global::System.Threading.Tasks.Task<TMatchResult>> deriving) =>
		await (await baseType.ConfigureAwait(false)).Match(deriving).ConfigureAwait(false);
		
		public static void Switch<T>(this global::FunicularSwitch.Test.BaseType<T> baseType, global::System.Action<FunicularSwitch.Test.Deriving> deriving)
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.Deriving deriving1:
					deriving(deriving1);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::FunicularSwitch.Test.BaseType<T> baseType, global::System.Func<FunicularSwitch.Test.Deriving, global::System.Threading.Tasks.Task> deriving)
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.Deriving deriving1:
					await deriving(deriving1).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T>> baseType, global::System.Action<FunicularSwitch.Test.Deriving> deriving) =>
		(await baseType.ConfigureAwait(false)).Switch(deriving);
		
		public static async global::System.Threading.Tasks.Task Switch<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T>> baseType, global::System.Func<FunicularSwitch.Test.Deriving, global::System.Threading.Tasks.Task> deriving) =>
		await (await baseType.ConfigureAwait(false)).Switch(deriving).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
