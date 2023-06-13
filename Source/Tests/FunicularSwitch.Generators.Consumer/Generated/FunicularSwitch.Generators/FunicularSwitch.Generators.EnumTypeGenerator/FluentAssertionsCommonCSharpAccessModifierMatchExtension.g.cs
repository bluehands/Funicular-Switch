#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FluentAssertions.Common
{
	public static partial class CSharpAccessModifierMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, Func<T> @internal, Func<T> invalidForCSharp, Func<T> @private, Func<T> privateProtected, Func<T> @protected, Func<T> protectedInternal, Func<T> @public) =>
		cSharpAccessModifier switch
		{
			FluentAssertions.Common.CSharpAccessModifier.Internal => @internal(),
			FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp => invalidForCSharp(),
			FluentAssertions.Common.CSharpAccessModifier.Private => @private(),
			FluentAssertions.Common.CSharpAccessModifier.PrivateProtected => privateProtected(),
			FluentAssertions.Common.CSharpAccessModifier.Protected => @protected(),
			FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal => protectedInternal(),
			FluentAssertions.Common.CSharpAccessModifier.Public => @public(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, Func<Task<T>> @internal, Func<Task<T>> invalidForCSharp, Func<Task<T>> @private, Func<Task<T>> privateProtected, Func<Task<T>> @protected, Func<Task<T>> protectedInternal, Func<Task<T>> @public) =>
		cSharpAccessModifier switch
		{
			FluentAssertions.Common.CSharpAccessModifier.Internal => @internal(),
			FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp => invalidForCSharp(),
			FluentAssertions.Common.CSharpAccessModifier.Private => @private(),
			FluentAssertions.Common.CSharpAccessModifier.PrivateProtected => privateProtected(),
			FluentAssertions.Common.CSharpAccessModifier.Protected => @protected(),
			FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal => protectedInternal(),
			FluentAssertions.Common.CSharpAccessModifier.Public => @public(),
			_ => throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, Func<T> @internal, Func<T> invalidForCSharp, Func<T> @private, Func<T> privateProtected, Func<T> @protected, Func<T> protectedInternal, Func<T> @public) =>
		(await cSharpAccessModifier.ConfigureAwait(false)).Match(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public);
		
		public static async Task<T> Match<T>(this Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, Func<Task<T>> @internal, Func<Task<T>> invalidForCSharp, Func<Task<T>> @private, Func<Task<T>> privateProtected, Func<Task<T>> @protected, Func<Task<T>> protectedInternal, Func<Task<T>> @public) =>
		await (await cSharpAccessModifier.ConfigureAwait(false)).Match(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, Action @internal, Action invalidForCSharp, Action @private, Action privateProtected, Action @protected, Action protectedInternal, Action @public)
		{
			switch (cSharpAccessModifier)
			{
				case FluentAssertions.Common.CSharpAccessModifier.Internal:
					@internal();
					break;
				case FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp:
					invalidForCSharp();
					break;
				case FluentAssertions.Common.CSharpAccessModifier.Private:
					@private();
					break;
				case FluentAssertions.Common.CSharpAccessModifier.PrivateProtected:
					privateProtected();
					break;
				case FluentAssertions.Common.CSharpAccessModifier.Protected:
					@protected();
					break;
				case FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal:
					protectedInternal();
					break;
				case FluentAssertions.Common.CSharpAccessModifier.Public:
					@public();
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, Func<Task> @internal, Func<Task> invalidForCSharp, Func<Task> @private, Func<Task> privateProtected, Func<Task> @protected, Func<Task> protectedInternal, Func<Task> @public)
		{
			switch (cSharpAccessModifier)
			{
				case FluentAssertions.Common.CSharpAccessModifier.Internal:
					await @internal().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp:
					await invalidForCSharp().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.CSharpAccessModifier.Private:
					await @private().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.CSharpAccessModifier.PrivateProtected:
					await privateProtected().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.CSharpAccessModifier.Protected:
					await @protected().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal:
					await protectedInternal().ConfigureAwait(false);
					break;
				case FluentAssertions.Common.CSharpAccessModifier.Public:
					await @public().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, Action @internal, Action invalidForCSharp, Action @private, Action privateProtected, Action @protected, Action protectedInternal, Action @public) =>
		(await cSharpAccessModifier.ConfigureAwait(false)).Switch(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public);
		
		public static async Task Switch(this Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, Func<Task> @internal, Func<Task> invalidForCSharp, Func<Task> @private, Func<Task> privateProtected, Func<Task> @protected, Func<Task> protectedInternal, Func<Task> @public) =>
		await (await cSharpAccessModifier.ConfigureAwait(false)).Switch(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
