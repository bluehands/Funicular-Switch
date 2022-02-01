//HintName: FieldMatchExtension.g.cs
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Test
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this Field @field, Func<FunicularSwitch.Test.String, T> @string, Func<FunicularSwitch.Test.Enum, T> @enum, Func<FunicularSwitch.Test.Event, T> @event) =>
		@field switch
		{
			FunicularSwitch.Test.String case1 => @string(case1),
			FunicularSwitch.Test.Enum case2 => @enum(case2),
			FunicularSwitch.Test.Event case3 => @event(case3),
			_ => throw new ArgumentException($"Unknown type derived from Field: {@field.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this Field @field, Func<FunicularSwitch.Test.String, Task<T>> @string, Func<FunicularSwitch.Test.Enum, Task<T>> @enum, Func<FunicularSwitch.Test.Event, Task<T>> @event) =>
		@field switch
		{
			FunicularSwitch.Test.String case1 => @string(case1),
			FunicularSwitch.Test.Enum case2 => @enum(case2),
			FunicularSwitch.Test.Event case3 => @event(case3),
			_ => throw new ArgumentException($"Unknown type derived from Field: {@field.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<Field> @field, Func<FunicularSwitch.Test.String, T> @string, Func<FunicularSwitch.Test.Enum, T> @enum, Func<FunicularSwitch.Test.Event, T> @event) =>
		(await @field.ConfigureAwait(false)).Match(@string, @enum, @event);
		
		public static async Task<T> Match<T>(this Task<Field> @field, Func<FunicularSwitch.Test.String, Task<T>> @string, Func<FunicularSwitch.Test.Enum, Task<T>> @enum, Func<FunicularSwitch.Test.Event, Task<T>> @event) =>
		await (await @field.ConfigureAwait(false)).Match(@string, @enum, @event).ConfigureAwait(false);
	}
}
