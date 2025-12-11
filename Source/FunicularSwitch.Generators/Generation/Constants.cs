namespace FunicularSwitch.Generators.Generation;

internal static class Constants
{
	public const string ExperimentalGeneratorDocs = """
                                                    <summary>
                                                    This generator is still considered experimental and might break.
                                                    Please open any issues you may find in <see href=""https://github.com/bluehands/Funicular-Switch/issues"">the GitHub repo</see>.
                                                    </summary>
                                                    """;

	public static class Attributes
	{
		public const string InstantHandle = "[global::JetBrains.Annotations.InstantHandle]";
		public const string InstantHandleRequireAwait = "[global::JetBrains.Annotations.InstantHandle(RequireAwait = true)]";
		public const string MustUseReturnValue = "[global::JetBrains.Annotations.MustUseReturnValue]";
		public const string DebuggerStepThrough = "[global::System.Diagnostics.DebuggerStepThrough]";
		public const string Pure = "[global::System.Diagnostics.Contracts.PureAttribute]";
	}
}
