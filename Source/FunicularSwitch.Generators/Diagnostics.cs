using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

static class Diagnostics
{
    const string Category = nameof(ResultTypeGenerator);

    public static Diagnostic InvalidResultTypeAttributeUsage(string message, Location location) =>
        Warning(location,
            id: "FUN0001",
            title: "Invalid attribute usage",
            messageFormat: $"{message} -  Please use ResultType attribute with typeof expression like [ResultType(typeof(MyError))]."
        );

    public static Diagnostic InvalidMergeMethod(string message, Location location) =>
        Warning(location,
            "FUN0002",
            "Invalid merge method",
            $"{message} -  Please implement merge as member of error type or static extension method with signature TError -> TError -> TError.");

    public static Diagnostic AmbiguousMergeMethods(IEnumerable<string> methodNames) =>
        Warning(
            Location.None,
            "FUN003",
            "Ambiguous error merge methods",
            $"Ambiguous error merge methods: {string.Join(", ", methodNames)}.");

    public static Diagnostic MisleadingCaseOrdering(string message, Location location) =>
        Warning(location,
            id: "FUN0004",
            title: "Misleading case ordering",
            messageFormat: message
        );

    public static Diagnostic AmbiguousCaseIndex(string message, Location location) =>
        Warning(location,
            id: "FUN0005",
            title: "Ambiguous case index",
            messageFormat: message
        );

    public static Diagnostic CaseIndexNotSet(string message, Location location) =>
        Warning(location,
            id: "FUN0006",
            title: "Case index not set",
            messageFormat: message
        );

    public static Diagnostic UnionTypeIsNotAccessible(string message, Location location) =>
	    Warning(location,
		    id: "FUN0007",
		    title: "Union type is not accessible",
		    messageFormat: message
	    );

    static Diagnostic Warning(Location location, string id, string title, string messageFormat) =>
        Diagnostic.Create(
            new(
                id: id,
                title: title,
                messageFormat:
                messageFormat,
                category: Category,
                defaultSeverity: DiagnosticSeverity.Warning,
                isEnabledByDefault: true
            ),
            location
        );

    
}