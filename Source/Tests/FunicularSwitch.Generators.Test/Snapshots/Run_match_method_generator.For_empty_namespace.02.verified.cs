//HintName: OuterBaseMatchExtension.g.cs
#pragma warning disable 1591
using System;
using System.Threading.Tasks;

public static partial class BaseMatchExtension
{
	public static T Match<T>(this Outer.Base @base, Func<Outer.One, T> one, Func<Outer.Two, T> two) =>
	@base switch
	{
		Outer.One case1 => one(case1),
		Outer.Two case2 => two(case2),
		_ => throw new ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}")
	};
	
	public static Task<T> Match<T>(this Outer.Base @base, Func<Outer.One, Task<T>> one, Func<Outer.Two, Task<T>> two) =>
	@base switch
	{
		Outer.One case1 => one(case1),
		Outer.Two case2 => two(case2),
		_ => throw new ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}")
	};
	
	public static async Task<T> Match<T>(this Task<Outer.Base> @base, Func<Outer.One, T> one, Func<Outer.Two, T> two) =>
	(await @base.ConfigureAwait(false)).Match(one, two);
	
	public static async Task<T> Match<T>(this Task<Outer.Base> @base, Func<Outer.One, Task<T>> one, Func<Outer.Two, Task<T>> two) =>
	await (await @base.ConfigureAwait(false)).Match(one, two).ConfigureAwait(false);
	
	public static void Switch(this Outer.Base @base, Action<Outer.One> one, Action<Outer.Two> two)
	{
		switch (@base)
		{
			case Outer.One case1:
				one(case1);
				break;
			case Outer.Two case2:
				two(case2);
				break;
			default:
				throw new ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}");
		}
	}
	
	public static async Task Switch(this Outer.Base @base, Func<Outer.One, Task> one, Func<Outer.Two, Task> two)
	{
		switch (@base)
		{
			case Outer.One case1:
				await one(case1).ConfigureAwait(false);
				break;
			case Outer.Two case2:
				await two(case2).ConfigureAwait(false);
				break;
			default:
				throw new ArgumentException($"Unknown type derived from Outer.Base: {@base.GetType().Name}");
		}
	}
	
	public static async Task Switch(this Task<Outer.Base> @base, Action<Outer.One> one, Action<Outer.Two> two) =>
	(await @base.ConfigureAwait(false)).Switch(one, two);
	
	public static async Task Switch(this Task<Outer.Base> @base, Func<Outer.One, Task> one, Func<Outer.Two, Task> two) =>
	await (await @base.ConfigureAwait(false)).Switch(one, two).ConfigureAwait(false);
}
#pragma warning restore 1591
