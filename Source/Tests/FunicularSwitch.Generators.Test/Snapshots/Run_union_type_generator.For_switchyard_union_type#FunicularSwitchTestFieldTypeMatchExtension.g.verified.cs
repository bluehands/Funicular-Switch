//HintName: FunicularSwitchTestFieldTypeMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public static partial class FieldTypeMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.FieldType fieldType, global::System.Func<FunicularSwitch.Test.FieldType.Bool_, T> @bool, global::System.Func<FunicularSwitch.Test.FieldType.Enum_, T> @enum, global::System.Func<FunicularSwitch.Test.FieldType.String_, T> @string) =>
		fieldType switch
		{
			FunicularSwitch.Test.FieldType.Bool_ case1 => @bool(case1),
			FunicularSwitch.Test.FieldType.Enum_ case2 => @enum(case2),
			FunicularSwitch.Test.FieldType.String_ case3 => @string(case3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.FieldType fieldType, global::System.Func<FunicularSwitch.Test.FieldType.Bool_, global::System.Threading.Tasks.Task<T>> @bool, global::System.Func<FunicularSwitch.Test.FieldType.Enum_, global::System.Threading.Tasks.Task<T>> @enum, global::System.Func<FunicularSwitch.Test.FieldType.String_, global::System.Threading.Tasks.Task<T>> @string) =>
		fieldType switch
		{
			FunicularSwitch.Test.FieldType.Bool_ case1 => @bool(case1),
			FunicularSwitch.Test.FieldType.Enum_ case2 => @enum(case2),
			FunicularSwitch.Test.FieldType.String_ case3 => @string(case3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.FieldType> fieldType, global::System.Func<FunicularSwitch.Test.FieldType.Bool_, T> @bool, global::System.Func<FunicularSwitch.Test.FieldType.Enum_, T> @enum, global::System.Func<FunicularSwitch.Test.FieldType.String_, T> @string) =>
		(await fieldType.ConfigureAwait(false)).Match(@bool, @enum, @string);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.FieldType> fieldType, global::System.Func<FunicularSwitch.Test.FieldType.Bool_, global::System.Threading.Tasks.Task<T>> @bool, global::System.Func<FunicularSwitch.Test.FieldType.Enum_, global::System.Threading.Tasks.Task<T>> @enum, global::System.Func<FunicularSwitch.Test.FieldType.String_, global::System.Threading.Tasks.Task<T>> @string) =>
		await (await fieldType.ConfigureAwait(false)).Match(@bool, @enum, @string).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.FieldType fieldType, global::System.Action<FunicularSwitch.Test.FieldType.Bool_> @bool, global::System.Action<FunicularSwitch.Test.FieldType.Enum_> @enum, global::System.Action<FunicularSwitch.Test.FieldType.String_> @string)
		{
			switch (fieldType)
			{
				case FunicularSwitch.Test.FieldType.Bool_ case1:
					@bool(case1);
					break;
				case FunicularSwitch.Test.FieldType.Enum_ case2:
					@enum(case2);
					break;
				case FunicularSwitch.Test.FieldType.String_ case3:
					@string(case3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.FieldType fieldType, global::System.Func<FunicularSwitch.Test.FieldType.Bool_, global::System.Threading.Tasks.Task> @bool, global::System.Func<FunicularSwitch.Test.FieldType.Enum_, global::System.Threading.Tasks.Task> @enum, global::System.Func<FunicularSwitch.Test.FieldType.String_, global::System.Threading.Tasks.Task> @string)
		{
			switch (fieldType)
			{
				case FunicularSwitch.Test.FieldType.Bool_ case1:
					await @bool(case1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.FieldType.Enum_ case2:
					await @enum(case2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.FieldType.String_ case3:
					await @string(case3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.FieldType> fieldType, global::System.Action<FunicularSwitch.Test.FieldType.Bool_> @bool, global::System.Action<FunicularSwitch.Test.FieldType.Enum_> @enum, global::System.Action<FunicularSwitch.Test.FieldType.String_> @string) =>
		(await fieldType.ConfigureAwait(false)).Switch(@bool, @enum, @string);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.FieldType> fieldType, global::System.Func<FunicularSwitch.Test.FieldType.Bool_, global::System.Threading.Tasks.Task> @bool, global::System.Func<FunicularSwitch.Test.FieldType.Enum_, global::System.Threading.Tasks.Task> @enum, global::System.Func<FunicularSwitch.Test.FieldType.String_, global::System.Threading.Tasks.Task> @string) =>
		await (await fieldType.ConfigureAwait(false)).Switch(@bool, @enum, @string).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
