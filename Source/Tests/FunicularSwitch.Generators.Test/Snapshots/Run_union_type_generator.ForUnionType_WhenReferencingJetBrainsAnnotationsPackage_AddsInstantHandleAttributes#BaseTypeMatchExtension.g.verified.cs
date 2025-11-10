//HintName: BaseTypeMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
public static partial class BaseTypeMatchExtension
{
	[global::System.Diagnostics.DebuggerStepThrough]
	public static T Match<T>(this global::BaseType baseType, [global::JetBrains.Annotations.InstantHandle]global::System.Func<BaseType.Derived_, T> derived) =>
	baseType switch
	{
		BaseType.Derived_ derived1 => derived(derived1),
		_ => throw new global::System.ArgumentException($"Unknown type derived from BaseType: {baseType.GetType().Name}")
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::BaseType baseType, [global::JetBrains.Annotations.InstantHandle(RequireAwait = true)]global::System.Func<BaseType.Derived_, global::System.Threading.Tasks.Task<T>> derived) =>
	baseType switch
	{
		BaseType.Derived_ derived1 => await derived(derived1).ConfigureAwait(false),
		_ => throw new global::System.ArgumentException($"Unknown type derived from BaseType: {baseType.GetType().Name}")
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::BaseType> baseType, [global::JetBrains.Annotations.InstantHandle(RequireAwait = true)]global::System.Func<BaseType.Derived_, T> derived) =>
	(await baseType.ConfigureAwait(false)).Match(derived);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::BaseType> baseType, [global::JetBrains.Annotations.InstantHandle(RequireAwait = true)]global::System.Func<BaseType.Derived_, global::System.Threading.Tasks.Task<T>> derived) =>
	await (await baseType.ConfigureAwait(false)).Match(derived).ConfigureAwait(false);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void Switch(this global::BaseType baseType, [global::JetBrains.Annotations.InstantHandle]global::System.Action<BaseType.Derived_> derived)
	{
		switch (baseType)
		{
			case BaseType.Derived_ derived1:
				derived(derived1);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown type derived from BaseType: {baseType.GetType().Name}");
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::BaseType baseType, [global::JetBrains.Annotations.InstantHandle(RequireAwait = true)]global::System.Func<BaseType.Derived_, global::System.Threading.Tasks.Task> derived)
	{
		switch (baseType)
		{
			case BaseType.Derived_ derived1:
				await derived(derived1).ConfigureAwait(false);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown type derived from BaseType: {baseType.GetType().Name}");
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::BaseType> baseType, [global::JetBrains.Annotations.InstantHandle]global::System.Action<BaseType.Derived_> derived) =>
	(await baseType.ConfigureAwait(false)).Switch(derived);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::BaseType> baseType, [global::JetBrains.Annotations.InstantHandle(RequireAwait = true)]global::System.Func<BaseType.Derived_, global::System.Threading.Tasks.Task> derived) =>
	await (await baseType.ConfigureAwait(false)).Switch(derived).ConfigureAwait(false);
}

public abstract partial record BaseType
{
	[global::System.Diagnostics.DebuggerStepThrough]
	public static BaseType Derived(int Number) => new BaseType.Derived_(Number);
}
#pragma warning restore 1591
