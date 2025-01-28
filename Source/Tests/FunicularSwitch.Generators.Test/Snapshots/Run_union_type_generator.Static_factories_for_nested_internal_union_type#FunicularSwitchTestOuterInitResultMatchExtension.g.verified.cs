//HintName: FunicularSwitchTestOuterInitResultMatchExtension.g.cs
#pragma warning disable 1591
namespace FunicularSwitch.Test
{
	internal static partial class InitResultMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Test.Outer.InitResult initResult, global::System.Func<FunicularSwitch.Test.Outer.InitResult.Sync_, T> sync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_, T> oneTimeSync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.NoSync_, T> noSync) =>
		initResult switch
		{
			FunicularSwitch.Test.Outer.InitResult.Sync_ sync1 => sync(sync1),
			FunicularSwitch.Test.Outer.InitResult.OneTimeSync_ oneTimeSync2 => oneTimeSync(oneTimeSync2),
			FunicularSwitch.Test.Outer.InitResult.NoSync_ noSync3 => noSync(noSync3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.InitResult: {initResult.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Test.Outer.InitResult initResult, global::System.Func<FunicularSwitch.Test.Outer.InitResult.Sync_, global::System.Threading.Tasks.Task<T>> sync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_, global::System.Threading.Tasks.Task<T>> oneTimeSync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.NoSync_, global::System.Threading.Tasks.Task<T>> noSync) =>
		initResult switch
		{
			FunicularSwitch.Test.Outer.InitResult.Sync_ sync1 => sync(sync1),
			FunicularSwitch.Test.Outer.InitResult.OneTimeSync_ oneTimeSync2 => oneTimeSync(oneTimeSync2),
			FunicularSwitch.Test.Outer.InitResult.NoSync_ noSync3 => noSync(noSync3),
			_ => throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.InitResult: {initResult.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.InitResult> initResult, global::System.Func<FunicularSwitch.Test.Outer.InitResult.Sync_, T> sync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_, T> oneTimeSync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.NoSync_, T> noSync) =>
		(await initResult.ConfigureAwait(false)).Match(sync, oneTimeSync, noSync);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.InitResult> initResult, global::System.Func<FunicularSwitch.Test.Outer.InitResult.Sync_, global::System.Threading.Tasks.Task<T>> sync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_, global::System.Threading.Tasks.Task<T>> oneTimeSync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.NoSync_, global::System.Threading.Tasks.Task<T>> noSync) =>
		await (await initResult.ConfigureAwait(false)).Match(sync, oneTimeSync, noSync).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Test.Outer.InitResult initResult, global::System.Action<FunicularSwitch.Test.Outer.InitResult.Sync_> sync, global::System.Action<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_> oneTimeSync, global::System.Action<FunicularSwitch.Test.Outer.InitResult.NoSync_> noSync)
		{
			switch (initResult)
			{
				case FunicularSwitch.Test.Outer.InitResult.Sync_ sync1:
					sync(sync1);
					break;
				case FunicularSwitch.Test.Outer.InitResult.OneTimeSync_ oneTimeSync2:
					oneTimeSync(oneTimeSync2);
					break;
				case FunicularSwitch.Test.Outer.InitResult.NoSync_ noSync3:
					noSync(noSync3);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.InitResult: {initResult.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Test.Outer.InitResult initResult, global::System.Func<FunicularSwitch.Test.Outer.InitResult.Sync_, global::System.Threading.Tasks.Task> sync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_, global::System.Threading.Tasks.Task> oneTimeSync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.NoSync_, global::System.Threading.Tasks.Task> noSync)
		{
			switch (initResult)
			{
				case FunicularSwitch.Test.Outer.InitResult.Sync_ sync1:
					await sync(sync1).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.InitResult.OneTimeSync_ oneTimeSync2:
					await oneTimeSync(oneTimeSync2).ConfigureAwait(false);
					break;
				case FunicularSwitch.Test.Outer.InitResult.NoSync_ noSync3:
					await noSync(noSync3).ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown type derived from FunicularSwitch.Test.Outer.InitResult: {initResult.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.InitResult> initResult, global::System.Action<FunicularSwitch.Test.Outer.InitResult.Sync_> sync, global::System.Action<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_> oneTimeSync, global::System.Action<FunicularSwitch.Test.Outer.InitResult.NoSync_> noSync) =>
		(await initResult.ConfigureAwait(false)).Switch(sync, oneTimeSync, noSync);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Test.Outer.InitResult> initResult, global::System.Func<FunicularSwitch.Test.Outer.InitResult.Sync_, global::System.Threading.Tasks.Task> sync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.OneTimeSync_, global::System.Threading.Tasks.Task> oneTimeSync, global::System.Func<FunicularSwitch.Test.Outer.InitResult.NoSync_, global::System.Threading.Tasks.Task> noSync) =>
		await (await initResult.ConfigureAwait(false)).Switch(sync, oneTimeSync, noSync).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
