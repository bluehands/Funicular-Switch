//HintName: FunicularSwitchTestBaseMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
namespace FunicularSwitch.Test
{
	public static partial class BaseMatchExtension
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static T Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		@base switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Two two2 => two(two2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static global::System.Threading.Tasks.Task<T> Match<T>(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		@base switch
		{
			FunicularSwitch.Test.One one1 => one(one1),
			FunicularSwitch.Test.Two two2 => two(two2),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}")
		};
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, T> one, global::System.Func<FunicularSwitch.Test.Two, T> two) =>
		(await @base.ConfigureAwait(false)).Match(one, two);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task<T>> two) =>
		await (await @base.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static void Switch(this global::FunicularSwitch.Test.Base @base, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One one1:
					one(one1);
					break;
				case FunicularSwitch.Test.Two two2:
					two(two2);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::FunicularSwitch.Test.Base @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two)
		{
			switch (@base)
			{
				case FunicularSwitch.Test.One one1:
					await one(one1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Two two2:
					await two(two2).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base: {@base.GetType().Name}");
			}
		}
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Action<FunicularSwitch.Test.One> one, global::System.Action<FunicularSwitch.Test.Two> two) =>
		(await @base.ConfigureAwait(false)).Switch(one, two);
		
		[global::System.Diagnostics.DebuggerStepThrough]
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.Base> @base, global::System.Func<FunicularSwitch.Test.One, global::System.Threading.Tasks.Task> one, global::System.Func<FunicularSwitch.Test.Two, global::System.Threading.Tasks.Task> two) =>
		await (await @base.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
	}
	
	public partial class Base
	{
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Test.Base One(int requiredField, string requiredProperty) => new FunicularSwitch.Test.One()		
		{
			RequiredField = requiredField,			
			RequiredProperty = requiredProperty			
		};
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Test.Base Two(int bla, int strangeNameField, int _strangeNameField, bool @bool) => new FunicularSwitch.Test.Two(bla)		
		{
			strangeNameField = strangeNameField,			
			_strangeNameField = _strangeNameField,			
			Bool = @bool			
		};
		[global::System.Diagnostics.DebuggerStepThrough]
		public static FunicularSwitch.Test.Base Two(int bla, int strangeNameField, int strangeNameField2, int _strangeNameField, int __strangeNameField, bool @bool) => new FunicularSwitch.Test.Two(bla, strangeNameField, strangeNameField2)		
		{
			strangeNameField = _strangeNameField,			
			_strangeNameField = __strangeNameField,			
			Bool = @bool			
		};
	}
}
#pragma warning restore 1591
