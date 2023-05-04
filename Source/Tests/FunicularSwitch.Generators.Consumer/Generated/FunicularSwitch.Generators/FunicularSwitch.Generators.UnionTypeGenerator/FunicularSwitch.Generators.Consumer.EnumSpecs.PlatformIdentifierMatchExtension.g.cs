#pragma warning disable 1591
using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Consumer
{
	public static partial class MatchExtension
	{
		public static T Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Func<T> developerMachine, Func<T> linuxBox, Func<T> linuxBox2, Func<T> linuxDevice1, Func<T> linuxLed, Func<T> linuxTft, Func<T> windowsDevice1, Func<T> windowsDevice2, Func<T> windowsXp) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox => linuxBox(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox2 => linuxBox2(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice1 => linuxDevice1(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxLed => linuxLed(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxTft => linuxTft(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice1 => windowsDevice1(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice2 => windowsDevice2(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsXp => windowsXp(),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static Task<T> Match<T>(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Func<Task<T>> developerMachine, Func<Task<T>> linuxBox, Func<Task<T>> linuxBox2, Func<Task<T>> linuxDevice1, Func<Task<T>> linuxLed, Func<Task<T>> linuxTft, Func<Task<T>> windowsDevice1, Func<Task<T>> windowsDevice2, Func<Task<T>> windowsXp) =>
		platformIdentifier switch
		{
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine => developerMachine(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox => linuxBox(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox2 => linuxBox2(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice1 => linuxDevice1(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxLed => linuxLed(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxTft => linuxTft(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice1 => windowsDevice1(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice2 => windowsDevice2(),
			FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsXp => windowsXp(),
			_ => throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}")
		};
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Func<T> developerMachine, Func<T> linuxBox, Func<T> linuxBox2, Func<T> linuxDevice1, Func<T> linuxLed, Func<T> linuxTft, Func<T> windowsDevice1, Func<T> windowsDevice2, Func<T> windowsXp) =>
		(await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxBox, linuxBox2, linuxDevice1, linuxLed, linuxTft, windowsDevice1, windowsDevice2, windowsXp);
		
		public static async Task<T> Match<T>(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Func<Task<T>> developerMachine, Func<Task<T>> linuxBox, Func<Task<T>> linuxBox2, Func<Task<T>> linuxDevice1, Func<Task<T>> linuxLed, Func<Task<T>> linuxTft, Func<Task<T>> windowsDevice1, Func<Task<T>> windowsDevice2, Func<Task<T>> windowsXp) =>
		await (await platformIdentifier.ConfigureAwait(false)).Match(developerMachine, linuxBox, linuxBox2, linuxDevice1, linuxLed, linuxTft, windowsDevice1, windowsDevice2, windowsXp).ConfigureAwait(false);
		
		public static void Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Action developerMachine, Action linuxBox, Action linuxBox2, Action linuxDevice1, Action linuxLed, Action linuxTft, Action windowsDevice1, Action windowsDevice2, Action windowsXp)
		{
			switch (platformIdentifier)
			{
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine:
					developerMachine();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox:
					linuxBox();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox2:
					linuxBox2();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice1:
					linuxDevice1();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxLed:
					linuxLed();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxTft:
					linuxTft();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice1:
					windowsDevice1();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice2:
					windowsDevice2();
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsXp:
					windowsXp();
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async Task Switch(this FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier platformIdentifier, Func<Task> developerMachine, Func<Task> linuxBox, Func<Task> linuxBox2, Func<Task> linuxDevice1, Func<Task> linuxLed, Func<Task> linuxTft, Func<Task> windowsDevice1, Func<Task> windowsDevice2, Func<Task> windowsXp)
		{
			switch (platformIdentifier)
			{
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.DeveloperMachine:
					await developerMachine().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox:
					await linuxBox().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxBox2:
					await linuxBox2().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxDevice1:
					await linuxDevice1().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxLed:
					await linuxLed().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.LinuxTft:
					await linuxTft().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice1:
					await windowsDevice1().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsDevice2:
					await windowsDevice2().ConfigureAwait(false);
					break;
				case FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier.WindowsXp:
					await windowsXp().ConfigureAwait(false);
					break;
				default:
					throw new ArgumentException($"Unknown type derived from FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier: {platformIdentifier.GetType().Name}");
			}
		}
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Action developerMachine, Action linuxBox, Action linuxBox2, Action linuxDevice1, Action linuxLed, Action linuxTft, Action windowsDevice1, Action windowsDevice2, Action windowsXp) =>
		(await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxBox, linuxBox2, linuxDevice1, linuxLed, linuxTft, windowsDevice1, windowsDevice2, windowsXp);
		
		public static async Task Switch(this Task<FunicularSwitch.Generators.Consumer.EnumSpecs.PlatformIdentifier> platformIdentifier, Func<Task> developerMachine, Func<Task> linuxBox, Func<Task> linuxBox2, Func<Task> linuxDevice1, Func<Task> linuxLed, Func<Task> linuxTft, Func<Task> windowsDevice1, Func<Task> windowsDevice2, Func<Task> windowsXp) =>
		await (await platformIdentifier.ConfigureAwait(false)).Switch(developerMachine, linuxBox, linuxBox2, linuxDevice1, linuxLed, linuxTft, windowsDevice1, windowsDevice2, windowsXp).ConfigureAwait(false);
	}
}
#pragma warning restore 1591
