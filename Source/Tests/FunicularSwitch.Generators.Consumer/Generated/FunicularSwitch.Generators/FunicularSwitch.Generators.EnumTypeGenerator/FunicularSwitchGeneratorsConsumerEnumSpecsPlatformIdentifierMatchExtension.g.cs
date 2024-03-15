#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class EnumSpecs_PlatformIdentifierMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, global::System.Func<T> developerMachine, global::System.Func<T> linuxDevice, global::System.Func<T> windowsDevice) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice => linuxDevice(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice => windowsDevice(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static global::System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, global::System.Func<global::System.Threading.Tasks.Task<T>> developerMachine, global::System.Func<global::System.Threading.Tasks.Task<T>> linuxDevice, global::System.Func<global::System.Threading.Tasks.Task<T>> windowsDevice) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice => linuxDevice(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice => windowsDevice(),
			_ => throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, global::System.Func<T> developerMachine, global::System.Func<T> linuxDevice, global::System.Func<T> windowsDevice) =>
		(await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxDevice, windowsDevice);
		
		public static async global::System.Threading.Tasks.Task<T> Match<T>(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, global::System.Func<global::System.Threading.Tasks.Task<T>> developerMachine, global::System.Func<global::System.Threading.Tasks.Task<T>> linuxDevice, global::System.Func<global::System.Threading.Tasks.Task<T>> windowsDevice) =>
		await (await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxDevice, windowsDevice).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, global::System.Action developerMachine, global::System.Action linuxDevice, global::System.Action windowsDevice)
		{
			switch (platformIdentifier)
			{
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine:
					developerMachine();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice:
					linuxDevice();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice:
					windowsDevice();
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, global::System.Func<global::System.Threading.Tasks.Task> developerMachine, global::System.Func<global::System.Threading.Tasks.Task> linuxDevice, global::System.Func<global::System.Threading.Tasks.Task> windowsDevice)
		{
			switch (platformIdentifier)
			{
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine:
					await developerMachine().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice:
					await linuxDevice().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice:
					await windowsDevice().ConfigureAwait(false);
					break;
				default:
					throw new global::System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, global::System.Action developerMachine, global::System.Action linuxDevice, global::System.Action windowsDevice) =>
		(await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxDevice, windowsDevice);
		
		public static async global::System.Threading.Tasks.Task Switch(this global::System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, global::System.Func<global::System.Threading.Tasks.Task> developerMachine, global::System.Func<global::System.Threading.Tasks.Task> linuxDevice, global::System.Func<global::System.Threading.Tasks.Task> windowsDevice) =>
		await (await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxDevice, windowsDevice).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
