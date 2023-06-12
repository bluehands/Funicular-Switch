//HintName: FunicularSwitchTestFieldTypeMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class FieldTypeMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.FieldType fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, T> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, T> @enum, Func<FunicularSwitch.Test.FieldType.String_, T> @string) =>
		fieldType switch
		{
			FunicularSwitch.Test.FieldType.Bool_ case1 => @bool(case1),
			FunicularSwitch.Test.FieldType.Enum_ case2 => @enum(case2),
			FunicularSwitch.Test.FieldType.String_ case3 => @string(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Test.FieldType fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, Task<T>> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, Task<T>> @enum, Func<FunicularSwitch.Test.FieldType.String_, Task<T>> @string) =>
		fieldType switch
		{
			FunicularSwitch.Test.FieldType.Bool_ case1 => @bool(case1),
			FunicularSwitch.Test.FieldType.Enum_ case2 => @enum(case2),
			FunicularSwitch.Test.FieldType.String_ case3 => @string(case3),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.FieldType> fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, T> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, T> @enum, Func<FunicularSwitch.Test.FieldType.String_, T> @string) =>
		(await fieldType.ConfigureAwait(false)).Match(@bool, @enum, @string);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Test.FieldType> fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, Task<T>> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, Task<T>> @enum, Func<FunicularSwitch.Test.FieldType.String_, Task<T>> @string) =>
		await (await fieldType.ConfigureAwait(false)).Match(@bool, @enum, @string).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.FieldType fieldType, Action<FunicularSwitch.Test.FieldType.Bool_> @bool, Action<FunicularSwitch.Test.FieldType.Enum_> @enum, Action<FunicularSwitch.Test.FieldType.String_> @string)
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
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Test.FieldType fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, Task> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, Task> @enum, Func<FunicularSwitch.Test.FieldType.String_, Task> @string)
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
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Test.FieldType: {fieldType.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Test.FieldType> fieldType, Action<FunicularSwitch.Test.FieldType.Bool_> @bool, Action<FunicularSwitch.Test.FieldType.Enum_> @enum, Action<FunicularSwitch.Test.FieldType.String_> @string) =>
		(await fieldType.ConfigureAwait(false)).Switch(@bool, @enum, @string);
		
		public static async Task Switch(this Task<FunicularSwitch.Test.FieldType> fieldType, Func<FunicularSwitch.Test.FieldType.Bool_, Task> @bool, Func<FunicularSwitch.Test.FieldType.Enum_, Task> @enum, Func<FunicularSwitch.Test.FieldType.String_, Task> @string) =>
		await (await fieldType.ConfigureAwait(false)).Switch(@bool, @enum, @string).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
