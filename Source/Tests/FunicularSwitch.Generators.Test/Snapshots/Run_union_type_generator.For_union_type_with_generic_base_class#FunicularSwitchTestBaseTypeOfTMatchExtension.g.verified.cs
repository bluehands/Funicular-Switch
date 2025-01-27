//HintName: FunicularSwitchTestBaseTypeOfTMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	public abstract partial record BaseType<T>
	{
		public TMatchResult Match<TMatchResult>(global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving, TMatchResult> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2, TMatchResult> deriving2) =>
		this switch
		{
			FunicularSwitch.Test.BaseType<T>.Deriving deriving1 => deriving(deriving1),
			FunicularSwitch.Test.BaseType<T>.Deriving2 deriving22 => deriving2(deriving22),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {this.GetType().Name}")
		};
		
		public void Switch(global::System.Action<FunicularSwitch.Test.BaseType<T>.Deriving> deriving, global::System.Action<FunicularSwitch.Test.BaseType<T>.Deriving2> deriving2)
		{
			switch (this)
			{
				case FunicularSwitch.Test.BaseType<T>.Deriving deriving1:
					deriving(deriving1);
					break;
				case FunicularSwitch.Test.BaseType<T>.Deriving2 deriving22:
					deriving2(deriving22);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {this.GetType().Name}");
			}
		}
		
		public async global::System.Threading.Tasks.Task Switch(global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving, global::System.Threading.Tasks.Task> deriving, global::System.Func<FunicularSwitch.Test.BaseType<T>.Deriving2, global::System.Threading.Tasks.Task> deriving2)
		{
			switch (this)
			{
				case FunicularSwitch.Test.BaseType<T>.Deriving deriving1:
					await deriving(deriving1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.BaseType<T>.Deriving2 deriving22:
					await deriving2(deriving22).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.BaseType: {this.GetType().Name}");
			}
		}
	}
	
	public abstract partial record BaseType<T>
	{
	}
}
#pragma warning restore 1591
