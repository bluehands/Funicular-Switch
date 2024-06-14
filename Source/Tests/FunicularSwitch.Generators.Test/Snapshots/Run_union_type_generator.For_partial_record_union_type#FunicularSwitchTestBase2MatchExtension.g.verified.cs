//HintName: FunicularSwitchTestBase2MatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	internal static partial class Base2MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Base2 base2, global::System.Func<FunicularSwitch.Test.Base2.Base2Prefix, T> prefix, global::System.Func<FunicularSwitch.Test.Base2.PostfixBase2, T> postfix, global::System.Func<FunicularSwitch.Test.Base2.UnderscorePostfix_, T> underscorePostfix, global::System.Func<FunicularSwitch.Test.Base2._UnderscorePrefix, T> underscorePrefix, global::System.Func<FunicularSwitch.Test.Base2.Base22Invalid, T> base22Invalid) =>
		base2 switch
		{
			FunicularSwitch.Test.Base2.Base2Prefix prefix1 => prefix(prefix1),
			FunicularSwitch.Test.Base2.PostfixBase2 postfix2 => postfix(postfix2),
			FunicularSwitch.Test.Base2.UnderscorePostfix_ underscorePostfix3 => underscorePostfix(underscorePostfix3),
			FunicularSwitch.Test.Base2._UnderscorePrefix underscorePrefix4 => underscorePrefix(underscorePrefix4),
			FunicularSwitch.Test.Base2.Base22Invalid base22Invalid5 => base22Invalid(base22Invalid5),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base2: {base2.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Base2 base2, global::System.Func<FunicularSwitch.Test.Base2.Base2Prefix, global::System.Threading.Tasks.Task<T>> prefix, global::System.Func<FunicularSwitch.Test.Base2.PostfixBase2, global::System.Threading.Tasks.Task<T>> postfix, global::System.Func<FunicularSwitch.Test.Base2.UnderscorePostfix_, global::System.Threading.Tasks.Task<T>> underscorePostfix, global::System.Func<FunicularSwitch.Test.Base2._UnderscorePrefix, global::System.Threading.Tasks.Task<T>> underscorePrefix, global::System.Func<FunicularSwitch.Test.Base2.Base22Invalid, global::System.Threading.Tasks.Task<T>> base22Invalid) =>
		base2 switch
		{
			FunicularSwitch.Test.Base2.Base2Prefix prefix1 => prefix(prefix1),
			FunicularSwitch.Test.Base2.PostfixBase2 postfix2 => postfix(postfix2),
			FunicularSwitch.Test.Base2.UnderscorePostfix_ underscorePostfix3 => underscorePostfix(underscorePostfix3),
			FunicularSwitch.Test.Base2._UnderscorePrefix underscorePrefix4 => underscorePrefix(underscorePrefix4),
			FunicularSwitch.Test.Base2.Base22Invalid base22Invalid5 => base22Invalid(base22Invalid5),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base2: {base2.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base2> base2, global::System.Func<FunicularSwitch.Test.Base2.Base2Prefix, T> prefix, global::System.Func<FunicularSwitch.Test.Base2.PostfixBase2, T> postfix, global::System.Func<FunicularSwitch.Test.Base2.UnderscorePostfix_, T> underscorePostfix, global::System.Func<FunicularSwitch.Test.Base2._UnderscorePrefix, T> underscorePrefix, global::System.Func<FunicularSwitch.Test.Base2.Base22Invalid, T> base22Invalid) =>
		(await base2.ConfigureAwait(false)).Match(prefix, postfix, underscorePostfix, underscorePrefix, base22Invalid);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base2> base2, global::System.Func<FunicularSwitch.Test.Base2.Base2Prefix, global::System.Threading.Tasks.Task<T>> prefix, global::System.Func<FunicularSwitch.Test.Base2.PostfixBase2, global::System.Threading.Tasks.Task<T>> postfix, global::System.Func<FunicularSwitch.Test.Base2.UnderscorePostfix_, global::System.Threading.Tasks.Task<T>> underscorePostfix, global::System.Func<FunicularSwitch.Test.Base2._UnderscorePrefix, global::System.Threading.Tasks.Task<T>> underscorePrefix, global::System.Func<FunicularSwitch.Test.Base2.Base22Invalid, global::System.Threading.Tasks.Task<T>> base22Invalid) =>
		await (await base2.ConfigureAwait(false)).Match(prefix, postfix, underscorePostfix, underscorePrefix, base22Invalid).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Base2 base2, global::System.Action<FunicularSwitch.Test.Base2.Base2Prefix> prefix, global::System.Action<FunicularSwitch.Test.Base2.PostfixBase2> postfix, global::System.Action<FunicularSwitch.Test.Base2.UnderscorePostfix_> underscorePostfix, global::System.Action<FunicularSwitch.Test.Base2._UnderscorePrefix> underscorePrefix, global::System.Action<FunicularSwitch.Test.Base2.Base22Invalid> base22Invalid)
		{
			switch (base2)
			{
				case FunicularSwitch.Test.Base2.Base2Prefix prefix1:
					prefix(prefix1);
					break;
				case FunicularSwitch.Test.Base2.PostfixBase2 postfix2:
					postfix(postfix2);
					break;
				case FunicularSwitch.Test.Base2.UnderscorePostfix_ underscorePostfix3:
					underscorePostfix(underscorePostfix3);
					break;
				case FunicularSwitch.Test.Base2._UnderscorePrefix underscorePrefix4:
					underscorePrefix(underscorePrefix4);
					break;
				case FunicularSwitch.Test.Base2.Base22Invalid base22Invalid5:
					base22Invalid(base22Invalid5);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base2: {base2.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Base2 base2, global::System.Func<FunicularSwitch.Test.Base2.Base2Prefix, global::System.Threading.Tasks.Task> prefix, global::System.Func<FunicularSwitch.Test.Base2.PostfixBase2, global::System.Threading.Tasks.Task> postfix, global::System.Func<FunicularSwitch.Test.Base2.UnderscorePostfix_, global::System.Threading.Tasks.Task> underscorePostfix, global::System.Func<FunicularSwitch.Test.Base2._UnderscorePrefix, global::System.Threading.Tasks.Task> underscorePrefix, global::System.Func<FunicularSwitch.Test.Base2.Base22Invalid, global::System.Threading.Tasks.Task> base22Invalid)
		{
			switch (base2)
			{
				case FunicularSwitch.Test.Base2.Base2Prefix prefix1:
					await prefix(prefix1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Base2.PostfixBase2 postfix2:
					await postfix(postfix2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Base2.UnderscorePostfix_ underscorePostfix3:
					await underscorePostfix(underscorePostfix3).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Base2._UnderscorePrefix underscorePrefix4:
					await underscorePrefix(underscorePrefix4).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Base2.Base22Invalid base22Invalid5:
					await base22Invalid(base22Invalid5).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Base2: {base2.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base2> base2, global::System.Action<FunicularSwitch.Test.Base2.Base2Prefix> prefix, global::System.Action<FunicularSwitch.Test.Base2.PostfixBase2> postfix, global::System.Action<FunicularSwitch.Test.Base2.UnderscorePostfix_> underscorePostfix, global::System.Action<FunicularSwitch.Test.Base2._UnderscorePrefix> underscorePrefix, global::System.Action<FunicularSwitch.Test.Base2.Base22Invalid> base22Invalid) =>
		(await base2.ConfigureAwait(false)).Switch(prefix, postfix, underscorePostfix, underscorePrefix, base22Invalid);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Base2> base2, global::System.Func<FunicularSwitch.Test.Base2.Base2Prefix, global::System.Threading.Tasks.Task> prefix, global::System.Func<FunicularSwitch.Test.Base2.PostfixBase2, global::System.Threading.Tasks.Task> postfix, global::System.Func<FunicularSwitch.Test.Base2.UnderscorePostfix_, global::System.Threading.Tasks.Task> underscorePostfix, global::System.Func<FunicularSwitch.Test.Base2._UnderscorePrefix, global::System.Threading.Tasks.Task> underscorePrefix, global::System.Func<FunicularSwitch.Test.Base2.Base22Invalid, global::System.Threading.Tasks.Task> base22Invalid) =>
		await (await base2.ConfigureAwait(false)).Switch(prefix, postfix, underscorePostfix, underscorePrefix, base22Invalid).ConfigureAwait(false);
	}
	
	abstract partial record Base2
	{
		public static FunicularSwitch.Test.Base2 Prefix() => new FunicularSwitch.Test.Base2.Base2Prefix();
		public static FunicularSwitch.Test.Base2 Postfix() => new FunicularSwitch.Test.Base2.PostfixBase2();
		public static FunicularSwitch.Test.Base2 UnderscorePostfix() => new FunicularSwitch.Test.Base2.UnderscorePostfix_();
		public static FunicularSwitch.Test.Base2 UnderscorePrefix() => new FunicularSwitch.Test.Base2._UnderscorePrefix();
	}
}
#pragma warning restore 1591
