#pragma warning disable 1591
namespace FunicularSwitch.Generators.Consumer
{
	public static partial class EnumSpecs_PlatformIdentifierMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, System.Func<T> developerMachine, System.Func<T> linuxDevice, System.Func<T> windowsDevice) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice => linuxDevice(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice => windowsDevice(),
			_ => throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static System.Threading.Tasks.Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, System.Func<System.Threading.Tasks.Task<T>> developerMachine, System.Func<System.Threading.Tasks.Task<T>> linuxDevice, System.Func<System.Threading.Tasks.Task<T>> windowsDevice) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice => linuxDevice(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice => windowsDevice(),
			_ => throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, System.Func<T> developerMachine, System.Func<T> linuxDevice, System.Func<T> windowsDevice) =>
		(await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxDevice, windowsDevice);
		
		public static async System.Threading.Tasks.Task<T> Match<T>(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, System.Func<System.Threading.Tasks.Task<T>> developerMachine, System.Func<System.Threading.Tasks.Task<T>> linuxDevice, System.Func<System.Threading.Tasks.Task<T>> windowsDevice) =>
		await (await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxDevice, windowsDevice).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, System.Action developerMachine, System.Action linuxDevice, System.Action windowsDevice)
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
					throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, System.Func<System.Threading.Tasks.Task> developerMachine, System.Func<System.Threading.Tasks.Task> linuxDevice, System.Func<System.Threading.Tasks.Task> windowsDevice)
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
					throw new System.ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, System.Action developerMachine, System.Action linuxDevice, System.Action windowsDevice) =>
		(await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxDevice, windowsDevice);
		
		public static async System.Threading.Tasks.Task Switch(this System.Threading.Tasks.Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, System.Func<System.Threading.Tasks.Task> developerMachine, System.Func<System.Threading.Tasks.Task> linuxDevice, System.Func<System.Threading.Tasks.Task> windowsDevice) =>
		await (await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxDevice, windowsDevice).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
