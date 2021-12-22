using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

static class Diagnostics
{
    const string Category = nameof(ResultTypeGenerator);

    public static Diagnostic InvalidAttributeUsage(string message, Location location) =>
        Warning(location,
            id: "FUN0001",
            title: "Invalid attribute usage",
            messageFormat: $"{message} -  Please use ResultType attribute with typeof expression like [ResultType(typeof(MyError))]"
        );

    public static Diagnostic InvalidMergeMethod(string message, Location location) =>
        Warning(location,
            "FUN0002",
            "Invalid merge method",
            $"{message} -  Please implement merge as member of error type or static extension method with signature TError -> TError -> TError");

    public static Diagnostic AmbiguousMergeMethods(IEnumerable<string> methodNames) =>
        Warning(
            Location.None,
            "FUN003",
            "Ambiguous error merge methods",
            $"Ambiguous error merge methods: {string.Join(", ", methodNames)}");

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