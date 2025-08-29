//HintName: FunicularSwitchTestBaseTypeOfT1_T2MatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseTypeMatchExtension
	{
		public static T Match<T1, T2, T>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Func<FunicularSwitch.Test.Deriving<T1, T2>, T> deriving, global::System.Func<FunicularSwitch.Test.Deriving2<T1, T2>, T> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		baseType switch
		{
			FunicularSwitch.Test.Deriving<T1, T2> deriving1 => deriving(deriving1),
			FunicularSwitch.Test.Deriving2<T1, T2> deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T1, T2, T>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Func<FunicularSwitch.Test.Deriving<T1, T2>, global::System.Threading.Tasks.Task<T>> deriving, global::System.Func<FunicularSwitch.Test.Deriving2<T1, T2>, global::System.Threading.Tasks.Task<T>> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		baseType switch
		{
			FunicularSwitch.Test.Deriving<T1, T2> deriving1 => deriving(deriving1),
			FunicularSwitch.Test.Deriving2<T1, T2> deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T1, T2, T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Func<FunicularSwitch.Test.Deriving<T1, T2>, T> deriving, global::System.Func<FunicularSwitch.Test.Deriving2<T1, T2>, T> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		(await baseType.ConfigureAwait(false)).Match(deriving, deriving2);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T1, T2, T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Func<FunicularSwitch.Test.Deriving<T1, T2>, global::System.Threading.Tasks.Task<T>> deriving, global::System.Func<FunicularSwitch.Test.Deriving2<T1, T2>, global::System.Threading.Tasks.Task<T>> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		await (await baseType.ConfigureAwait(false)).Match(deriving, deriving2).ConfigureAwait(false);
		
		public static void Switch<T1, T2>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Action<FunicularSwitch.Test.Deriving<T1, T2>> deriving, global::System.Action<FunicularSwitch.Test.Deriving2<T1, T2>> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int>
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.Deriving<T1, T2> deriving1:
					deriving(deriving1);
					break;
				case FunicularSwitch.Test.Deriving2<T1, T2> deriving22:
					deriving2(deriving22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T1, T2>(this global::FunicularSwitch.Test.BaseType<T1, T2> baseType, global::System.Func<FunicularSwitch.Test.Deriving<T1, T2>, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.Deriving2<T1, T2>, global::System.Threading.Tasks.Task> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int>
		{
			switch (baseType)
			{
				case FunicularSwitch.Test.Deriving<T1, T2> deriving1:
					await deriving(deriving1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Deriving2<T1, T2> deriving22:
					await deriving2(deriving22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch<T1, T2>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Action<FunicularSwitch.Test.Deriving<T1, T2>> deriving, global::System.Action<FunicularSwitch.Test.Deriving2<T1, T2>> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		(await baseType.ConfigureAwait(false)).Switch(deriving, deriving2);
		
		public static async global::System.Threading.Tasks.Task Switch<T1, T2>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.BaseType<T1, T2>> baseType, global::System.Func<FunicularSwitch.Test.Deriving<T1, T2>, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.Deriving2<T1, T2>, global::System.Threading.Tasks.Task> deriving2) where T1 : notnull, new()
 where T2 : class, global::System.Collections.Generic.IEnumerable<int> =>
		await (await baseType.ConfigureAwait(false)).Switch(deriving, deriving2).ConfigureAwait(false);
	}
	
	public abstract partial record BaseType<T1, T2>
		where T1 : notnull, new()

		where T2 : class, global::System.Collections.Generic.IEnumerable<int>
	{
		public static FunicularSwitch.Test.BaseType<T1, T2> Deriving(string Value, T1 Other, T2 List) => new FunicularSwitch.Test.Deriving<T1, T2>(Value, Other, List);
		public static FunicularSwitch.Test.BaseType<T1, T2> Deriving2(string Value) => new FunicularSwitch.Test.Deriving2<T1, T2>(Value);
	}
}
#pragma warning restore 1591
