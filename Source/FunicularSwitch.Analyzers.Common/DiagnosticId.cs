namespace FunicularSwitch.Analyzers;

public static class DiagnosticId
{
    public const string MatchNull = "FS0001";
    public const string SuggestClosed = "FS0002";
    public const string UnionHierarchyNotClosed = "FS0003";
    public const string UnionCaseNotSealed = "FS0004";
    public const string JsonPolymorphicInferClosed = "FS0005";
    public const string JsonDerivedTypeMissing = "FS0006";
    public const string SuggestClosedResultType = "FS0007";
}
