#pragma warning disable 1591
namespace FluentAssertions.Common
{
	public static partial class CSharpAccessModifierMatchExtension
	{
		public static T Match<T>(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, System.Func<T> @internal, System.Func<T> invalidForCSharp, System.Func<T> @private, System.Func<T> privateProtected, System.Func<T> @protected, System.Func<T> protectedInternal, System.Func<T> @public) =>
		cSharpAccessModifier switch
		{
			FluentAssertions.Common.CSharpAccessModifier.Internal => @internal(),
			FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp => invalidForCSharp(),
			FluentAssertions.Common.CSharpAccessModifier.Private => @private(),
			FluentAssertions.Common.CSharpAccessModifier.PrivateProtected => privateProtected(),
			FluentAssertions.Common.CSharpAccessModifier.Protected => @protected(),
			FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal => protectedInternal(),
			FluentAssertions.Common.CSharpAccessModifier.Public => @public(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, System.Func<System.Threading.Tasks.Task<T>> @internal, System.Func<System.Threading.Tasks.Task<T>> invalidForCSharp, System.Func<System.Threading.Tasks.Task<T>> @private, System.Func<System.Threading.Tasks.Task<T>> privateProtected, System.Func<System.Threading.Tasks.Task<T>> @protected, System.Func<System.Threading.Tasks.Task<T>> protectedInternal, System.Func<System.Threading.Tasks.Task<T>> @public) =>
		cSharpAccessModifier switch
		{
			FluentAssertions.Common.CSharpAccessModifier.Internal => @internal(),
			FluentAssertions.Common.CSharpAccessModifier.InvalidForCSharp => invalidForCSharp(),
			FluentAssertions.Common.CSharpAccessModifier.Private => @private(),
			FluentAssertions.Common.CSharpAccessModifier.PrivateProtected => privateProtected(),
			FluentAssertions.Common.CSharpAccessModifier.Protected => @protected(),
			FluentAssertions.Common.CSharpAccessModifier.ProtectedInternal => protectedInternal(),
			FluentAssertions.Common.CSharpAccessModifier.Public => @public(),
			_ => throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, System.Func<T> @internal, System.Func<T> invalidForCSharp, System.Func<T> @private, System.Func<T> privateProtected, System.Func<T> @protected, System.Func<T> protectedInternal, System.Func<T> @public) =>
		(await cSharpAccessModifier.ConfigureAwait(false)).Match(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, System.Func<System.Threading.Tasks.Task<T>> @internal, System.Func<System.Threading.Tasks.Task<T>> invalidForCSharp, System.Func<System.Threading.Tasks.Task<T>> @private, System.Func<System.Threading.Tasks.Task<T>> privateProtected, System.Func<System.Threading.Tasks.Task<T>> @protected, System.Func<System.Threading.Tasks.Task<T>> protectedInternal, System.Func<System.Threading.Tasks.Task<T>> @public) =>
		await (await cSharpAccessModifier.ConfigureAwait(false)).Match(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public).ConfigureAwait(false);
		
		public static void Switch(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, System.Action @internal, System.Action invalidForCSharp, System.Action @private, System.Action privateProtected, System.Action @protected, System.Action protectedInternal, System.Action @public)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FluentAssertions.Common.CSharpAccessModifier cSharpAccessModifier, System.Func<System.Threading.Tasks.Task> @internal, System.Func<System.Threading.Tasks.Task> invalidForCSharp, System.Func<System.Threading.Tasks.Task> @private, System.Func<System.Threading.Tasks.Task> privateProtected, System.Func<System.Threading.Tasks.Task> @protected, System.Func<System.Threading.Tasks.Task> protectedInternal, System.Func<System.Threading.Tasks.Task> @public)
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
					throw new System.ArgumentException($"Unknown enum value from FluentAssertions.Common.CSharpAccessModifier: {cSharpAccessModifier.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, System.Action @internal, System.Action invalidForCSharp, System.Action @private, System.Action privateProtected, System.Action @protected, System.Action protectedInternal, System.Action @public) =>
		(await cSharpAccessModifier.ConfigureAwait(false)).Switch(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FluentAssertions.Common.CSharpAccessModifier> cSharpAccessModifier, System.Func<System.Threading.Tasks.Task> @internal, System.Func<System.Threading.Tasks.Task> invalidForCSharp, System.Func<System.Threading.Tasks.Task> @private, System.Func<System.Threading.Tasks.Task> privateProtected, System.Func<System.Threading.Tasks.Task> @protected, System.Func<System.Threading.Tasks.Task> protectedInternal, System.Func<System.Threading.Tasks.Task> @public) =>
		await (await cSharpAccessModifier.ConfigureAwait(false)).Switch(@internal, invalidForCSharp, @private, privateProtected, @protected, protectedInternal, @public).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
