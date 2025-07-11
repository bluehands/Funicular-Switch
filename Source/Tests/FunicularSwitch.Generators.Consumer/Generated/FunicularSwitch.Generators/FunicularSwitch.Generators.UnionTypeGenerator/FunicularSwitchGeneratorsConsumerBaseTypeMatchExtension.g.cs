#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class BaseTypeMatchExtension
	{
		public static T Match<T>(this global::FunicularSwitch.Generators.Consumer.BaseType baseType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_, T> derivedType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_, T> derivedType2) =>
		baseType switch
		{
			FunicularSwitch.Generators.Consumer.BaseType.DerivedType_ derivedType1 => derivedType(derivedType1),
			FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_ derivedType22 => derivedType2(derivedType22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.BaseType: {baseType.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Generators.Consumer.BaseType baseType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_, global::System.Threading.Tasks.Task<T>> derivedType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_, global::System.Threading.Tasks.Task<T>> derivedType2) =>
		baseType switch
		{
			FunicularSwitch.Generators.Consumer.BaseType.DerivedType_ derivedType1 => derivedType(derivedType1),
			FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_ derivedType22 => derivedType2(derivedType22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.BaseType: {baseType.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.BaseType> baseType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_, T> derivedType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_, T> derivedType2) =>
		(await baseType.ConfigureAwait(false)).Match(derivedType, derivedType2);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.BaseType> baseType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_, global::System.Threading.Tasks.Task<T>> derivedType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_, global::System.Threading.Tasks.Task<T>> derivedType2) =>
		await (await baseType.ConfigureAwait(false)).Match(derivedType, derivedType2).ConfigureAwait(false);
		
		public static void Switch(this global::FunicularSwitch.Generators.Consumer.BaseType baseType, global::System.Action<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_> derivedType, global::System.Action<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_> derivedType2)
		{
			switch (baseType)
			{
				case FunicularSwitch.Generators.Consumer.BaseType.DerivedType_ derivedType1:
					derivedType(derivedType1);
					break;
				case FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_ derivedType22:
					derivedType2(derivedType22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Generators.Consumer.BaseType baseType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_, global::System.Threading.Tasks.Task> derivedType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_, global::System.Threading.Tasks.Task> derivedType2)
		{
			switch (baseType)
			{
				case FunicularSwitch.Generators.Consumer.BaseType.DerivedType_ derivedType1:
					await derivedType(derivedType1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_ derivedType22:
					await derivedType2(derivedType22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.BaseType: {baseType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.BaseType> baseType, global::System.Action<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_> derivedType, global::System.Action<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_> derivedType2) =>
		(await baseType.ConfigureAwait(false)).Switch(derivedType, derivedType2);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.BaseType> baseType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType_, global::System.Threading.Tasks.Task> derivedType, global::System.Func<FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_, global::System.Threading.Tasks.Task> derivedType2) =>
		await (await baseType.ConfigureAwait(false)).Switch(derivedType, derivedType2).ConfigureAwait(false);
	}
	
	public abstract partial record BaseType
	{
		public static FunicularSwitch.Generators.Consumer.BaseType DerivedType() => new FunicularSwitch.Generators.Consumer.BaseType.DerivedType_();
		public static FunicularSwitch.Generators.Consumer.BaseType DerivedType2() => new FunicularSwitch.Generators.Consumer.BaseType.DerivedType2_();
	}
}
#pragma warning restore 1591
