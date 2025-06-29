﻿//HintName: OuterBaseMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
public static partial class BaseMatchExtension
{
	public static T Match<T>(this global::Outer.Base @base, global::System.Func<Outer.One, T> one, global::System.Func<Outer.Two, T> two) =>
	@base switch
	{
		Outer.One one1 => one(one1),
		Outer.Two two2 => two(two2),
		_ => throw new global::System.ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}")
	};
	
	public static global::System.Threading.Tasks.Task<T> Match<T>(this global::Outer.Base @base, global::System.Func<Outer.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<Outer.Two, global::System.Threading.Tasks.Task<T>> two) =>
	@base switch
	{
		Outer.One one1 => one(one1),
		Outer.Two two2 => two(two2),
		_ => throw new global::System.ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}")
	};
	
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::Outer.Base> @base, global::System.Func<Outer.One, T> one, global::System.Func<Outer.Two, T> two) =>
	(await @base.ConfigureAwait(false)).Match(one, two);
	
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::Outer.Base> @base, global::System.Func<Outer.One, global::System.Threading.Tasks.Task<T>> one, global::System.Func<Outer.Two, global::System.Threading.Tasks.Task<T>> two) =>
	await (await @base.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
	
	public static void Switch(this global::Outer.Base @base, global::System.Action<Outer.One> one, global::System.Action<Outer.Two> two)
	{
		switch (@base)
		{
			case Outer.One one1:
				one(one1);
				break;
			case Outer.Two two2:
				two(two2);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}");
		}
	}
	
	public static async global::System.Threading.Tasks.Task Switch(this global::Outer.Base @base, global::System.Func<Outer.One, global::System.Threading.Tasks.Task> one, global::System.Func<Outer.Two, global::System.Threading.Tasks.Task> two)
	{
		switch (@base)
		{
			case Outer.One one1:
				await one(one1).ConfigureAwait(false);
				break;
			case Outer.Two two2:
				await two(two2).ConfigureAwait(false);
				break;
			default:
				throw new global::System.ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}");
		}
	}
	
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::Outer.Base> @base, global::System.Action<Outer.One> one, global::System.Action<Outer.Two> two) =>
	(await @base.ConfigureAwait(false)).Switch(one, two);
	
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::Outer.Base> @base, global::System.Func<Outer.One, global::System.Threading.Tasks.Task> one, global::System.Func<Outer.Two, global::System.Threading.Tasks.Task> two) =>
	await (await @base.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
}
#pragma warning restore 1591
