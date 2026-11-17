//HintName: PetMatchExtension.g.cs
#pragma warning disable 1591
#nullable enable
public static partial class PetMatchExtension
{
	[global::System.Diagnostics.DebuggerStepThrough]
	public static T Match<T>(this global::Pet pet, global::System.Func<global::Cat, T> cat, global::System.Func<global::Dog, T> dog) =>
	pet switch
	{
		null => throw new global::System.InvalidOperationException("Union Pet has no value (default instance of the union struct)"),
		global::Cat cat1 => cat(cat1),
		global::Dog dog2 => dog(dog2),
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::Pet pet, global::System.Func<global::Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<global::Dog, global::System.Threading.Tasks.Task<T>> dog) =>
	pet switch
	{
		null => throw new global::System.InvalidOperationException("Union Pet has no value (default instance of the union struct)"),
		global::Cat cat1 => await cat(cat1).ConfigureAwait(false),
		global::Dog dog2 => await dog(dog2).ConfigureAwait(false),
	};
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::Pet> pet, global::System.Func<global::Cat, T> cat, global::System.Func<global::Dog, T> dog) =>
	(await pet.ConfigureAwait(false)).Match(cat, dog);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<global::Pet> pet, global::System.Func<global::Cat, global::System.Threading.Tasks.Task<T>> cat, global::System.Func<global::Dog, global::System.Threading.Tasks.Task<T>> dog) =>
	await (await pet.ConfigureAwait(false)).Match(cat, dog).ConfigureAwait(false);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void Switch(this global::Pet pet, global::System.Action<global::Cat> cat, global::System.Action<global::Dog> dog)
	{
		switch (pet)
		{
			case null:
				throw new global::System.InvalidOperationException("Union Pet has no value (default instance of the union struct)");
			case global::Cat cat1:
				cat(cat1);
				break;
			case global::Dog dog2:
				dog(dog2);
				break;
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::Pet pet, global::System.Func<global::Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<global::Dog, global::System.Threading.Tasks.Task> dog)
	{
		switch (pet)
		{
			case null:
				throw new global::System.InvalidOperationException("Union Pet has no value (default instance of the union struct)");
			case global::Cat cat1:
				await cat(cat1).ConfigureAwait(false);
				break;
			case global::Dog dog2:
				await dog(dog2).ConfigureAwait(false);
				break;
		}
	}
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::Pet> pet, global::System.Action<global::Cat> cat, global::System.Action<global::Dog> dog) =>
	(await pet.ConfigureAwait(false)).Switch(cat, dog);
	
	[global::System.Diagnostics.DebuggerStepThrough]
	public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<global::Pet> pet, global::System.Func<global::Cat, global::System.Threading.Tasks.Task> cat, global::System.Func<global::Dog, global::System.Threading.Tasks.Task> dog) =>
	await (await pet.ConfigureAwait(false)).Switch(cat, dog).ConfigureAwait(false);
}
#pragma warning restore 1591
