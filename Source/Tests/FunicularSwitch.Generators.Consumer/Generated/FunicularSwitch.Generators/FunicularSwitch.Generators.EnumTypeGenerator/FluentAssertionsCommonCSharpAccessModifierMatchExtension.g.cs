#pragma warning disable 1591
namespace FluentAssertions.Common
{
	internal static partial class CSharpAccessModifierMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, global::System.Func<T> @internal, global::System.Func<T> invalidForCSharp, global::System.Func<T> @private, global::System.Func<T> privateProtected, global::System.Func<T> @protected, global::System.Func<T> protectedInternal, global::System.Func<T> @public) =>
		cSharpAccessModifier switch
		{
			FluentAssertions.Common.CSharpAccessModifier.Internal => @internal(),
			FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp => invalidForCSharp(),
			FluentAssertions.Common.CSharpAccessModifier.Private => @private(),
			FluentAssertions.Common.CSharpAccessModifier.PrivateProtected => privateProtected(),
			FluentAssertions.Common.CSharpAccessModifier.Protected => @protected(),
			FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal => protectedInternal(),
			FluentAssertions.Common.CSharpAccessModifier.Public => @public(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, global::System.Func<global::System.Threading.Tasks.Task<T>> @internal, global::System.Func<global::System.Threading.Tasks.Task<T>> invalidForCSharp, global::System.Func<global::System.Threading.Tasks.Task<T>> @private, global::System.Func<global::System.Threading.Tasks.Task<T>> privateProtected, global::System.Func<global::System.Threading.Tasks.Task<T>> @protected, global::System.Func<global::System.Threading.Tasks.Task<T>> protectedInternal, global::System.Func<global::System.Threading.Tasks.Task<T>> @public) =>
		cSharpAccessModifier switch
		{
			FluentAssertions.Common.CSharpAccessModifier.Internal => @internal(),
			FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp => invalidForCSharp(),
			FluentAssertions.Common.CSharpAccessModifier.Private => @private(),
			FluentAssertions.Common.CSharpAccessModifier.PrivateProtected => privateProtected(),
			FluentAssertions.Common.CSharpAccessModifier.Protected => @protected(),
			FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal => protectedInternal(),
			FluentAssertions.Common.CSharpAccessModifier.Public => @public(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, global::System.Func<T> @internal, global::System.Func<T> invalidForCSharp, global::System.Func<T> @private, global::System.Func<T> privateProtected, global::System.Func<T> @protected, global::System.Func<T> protectedInternal, global::System.Func<T> @public) =>
		(await cSharpAccessModifier.ConfigureAwait(false)).Match(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, global::System.Func<global::System.Threading.Tasks.Task<T>> @internal, global::System.Func<global::System.Threading.Tasks.Task<T>> invalidForCSharp, global::System.Func<global::System.Threading.Tasks.Task<T>> @private, global::System.Func<global::System.Threading.Tasks.Task<T>> privateProtected, global::System.Func<global::System.Threading.Tasks.Task<T>> @protected, global::System.Func<global::System.Threading.Tasks.Task<T>> protectedInternal, global::System.Func<global::System.Threading.Tasks.Task<T>> @public) =>
		await (await cSharpAccessModifier.ConfigureAwait(false)).Match(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, global::System.Action @internal, global::System.Action invalidForCSharp, global::System.Action @private, global::System.Action privateProtected, global::System.Action @protected, global::System.Action protectedInternal, global::System.Action @public)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, global::System.Func<global::System.Threading.Tasks.Task> @internal, global::System.Func<global::System.Threading.Tasks.Task> invalidForCSharp, global::System.Func<global::System.Threading.Tasks.Task> @private, global::System.Func<global::System.Threading.Tasks.Task> privateProtected, global::System.Func<global::System.Threading.Tasks.Task> @protected, global::System.Func<global::System.Threading.Tasks.Task> protectedInternal, global::System.Func<global::System.Threading.Tasks.Task> @public)
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
					throw new global::System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, global::System.Action @internal, global::System.Action invalidForCSharp, global::System.Action @private, global::System.Action privateProtected, global::System.Action @protected, global::System.Action protectedInternal, global::System.Action @public) =>
		(await cSharpAccessModifier.ConfigureAwait(false)).Switch(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, global::System.Func<global::System.Threading.Tasks.Task> @internal, global::System.Func<global::System.Threading.Tasks.Task> invalidForCSharp, global::System.Func<global::System.Threading.Tasks.Task> @private, global::System.Func<global::System.Threading.Tasks.Task> privateProtected, global::System.Func<global::System.Threading.Tasks.Task> @protected, global::System.Func<global::System.Threading.Tasks.Task> protectedInternal, global::System.Func<global::System.Threading.Tasks.Task> @public) =>
		await (await cSharpAccessModifier.ConfigureAwait(false)).Switch(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
