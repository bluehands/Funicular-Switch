#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	public static partial class PlatformIdentifierMatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Func<T> developerMachine, Func<T> linuxDevice, Func<T> windowsDevice) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice => linuxDevice(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice => windowsDevice(),
			_ => throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Func<Task<T>> developerMachine, Func<Task<T>> linuxDevice, Func<Task<T>> windowsDevice) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice => linuxDevice(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice => windowsDevice(),
			_ => throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Func<T> developerMachine, Func<T> linuxDevice, Func<T> windowsDevice) =>
		(await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxDevice, windowsDevice);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Func<Task<T>> developerMachine, Func<Task<T>> linuxDevice, Func<Task<T>> windowsDevice) =>
		await (await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxDevice, windowsDevice).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Action developerMachine, Action linuxDevice, Action windowsDevice)
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
					throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Func<Task> developerMachine, Func<Task> linuxDevice, Func<Task> windowsDevice)
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
					throw new ArgumentException($"Unknown enum value from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Action developerMachine, Action linuxDevice, Action windowsDevice) =>
		(await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxDevice, windowsDevice);
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Func<Task> developerMachine, Func<Task> linuxDevice, Func<Task> windowsDevice) =>
		await (await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxDevice, windowsDevice).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
