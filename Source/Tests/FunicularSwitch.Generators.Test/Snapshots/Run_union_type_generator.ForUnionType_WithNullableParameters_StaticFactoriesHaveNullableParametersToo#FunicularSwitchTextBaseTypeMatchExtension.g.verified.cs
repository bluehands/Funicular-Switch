//HintName: FunicularSwitchTextBaseTypeMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Text
{
	public static partial class BaseTypeMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Text.BaseType baseType, global::System.Func<FunicularSwitch.Text.BaseType.DerivedType_, T> derivedType) =>
		baseType switch
		{
			FunicularSwitch.Text.BaseType.DerivedType_ derivedType1 => derivedType(derivedType1),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Text.BaseType: {baseType.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Text.BaseType baseType, global::System.Func<FunicularSwitch.Text.BaseType.DerivedType_, global::System.Threading.Tasks.Task<T>> derivedType) =>
		baseType switch
		{
			FunicularSwitch.Text.BaseType.DerivedType_ derivedType1 => await derivedType(derivedType1).ConfigureAwait(false),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Text.BaseType: {baseType.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Text.BaseType> baseType, global::System.Func<FunicularSwitch.Text.BaseType.DerivedType_, T> derivedType) =>
		(await baseType.ConfigureAwait(false)).Match(derivedType);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Text.BaseType> baseType, global::System.Func<FunicularSwitch.Text.BaseType.DerivedType_, global::System.Threading.Tasks.Task<T>> derivedType) =>
		await (await baseType.ConfigureAwait(false)).Match(derivedType).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Text.BaseType baseType, global::System.Action<FunicularSwitch.Text.BaseType.DerivedType_> derivedType)
		{
			switch (baseType)
			{
				case FunicularSwitch.Text.BaseType.DerivedType_ derivedType1:
					derivedType(derivedType1);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Text.BaseType: {baseType.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Text.BaseType baseType, global::System.Func<FunicularSwitch.Text.BaseType.DerivedType_, global::System.Threading.Tasks.Task> derivedType)
		{
			switch (baseType)
			{
				case FunicularSwitch.Text.BaseType.DerivedType_ derivedType1:
					await derivedType(derivedType1).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Text.BaseType: {baseType.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Text.BaseType> baseType, global::System.Action<FunicularSwitch.Text.BaseType.DerivedType_> derivedType) =>
		(await baseType.ConfigureAwait(false)).Switch(derivedType);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Text.BaseType> baseType, global::System.Func<FunicularSwitch.Text.BaseType.DerivedType_, global::System.Threading.Tasks.Task> derivedType) =>
		await (await baseType.ConfigureAwait(false)).Switch(derivedType).ConfigureAwait(false);
	}
	
	public abstract partial record BaseType
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Text.BaseType DerivedType(string? NullableReferenceType, int? NullableStruct) => new FunicularSwitch.Text.BaseType.DerivedType_(NullableReferenceType, NullableStruct);
	}
}
#pragma warning restore 1591
