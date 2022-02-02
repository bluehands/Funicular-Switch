//HintName: FieldTypeMatchExtension.g.cs
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FieldType fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, T> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, T> @enum, Func<FunicularSwitch.Test.FieldType.String_, T> @string) =>
		fieldType switch
		{
			FunicularSwitch.Test.FieldType.Bool_ case1 => @bool(case1),
			FunicularSwitch.Test.FieldType.Enum_ case2 => @enum(case2),
			FunicularSwitch.Test.FieldType.String_ case3 => @string(case3),
			_ => throw new ArgumentException($"Unknown type derived from FieldType: {fieldType.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FieldType fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, Task<T>> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, Task<T>> @enum, Func<FunicularSwitch.Test.FieldType.String_, Task<T>> @string) =>
		fieldType switch
		{
			FunicularSwitch.Test.FieldType.Bool_ case1 => @bool(case1),
			FunicularSwitch.Test.FieldType.Enum_ case2 => @enum(case2),
			FunicularSwitch.Test.FieldType.String_ case3 => @string(case3),
			_ => throw new ArgumentException($"Unknown type derived from FieldType: {fieldType.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FieldType> fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, T> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, T> @enum, Func<FunicularSwitch.Test.FieldType.String_, T> @string) =>
		(await fieldType.ConfigureAwait(false)).Match(@bool, @enum, @string);
		
		public static async Task<T> Match<T>(this Task<FieldType> fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, Task<T>> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, Task<T>> @enum, Func<FunicularSwitch.Test.FieldType.String_, Task<T>> @string) =>
		await (await fieldType.ConfigureAwait(false)).Match(@bool, @enum, @string).ConfigureAwait(false);
	}
}
