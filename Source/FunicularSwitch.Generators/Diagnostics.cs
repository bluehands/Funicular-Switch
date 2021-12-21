using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

static class Diagnostics
{
    const string Category = nameof(ResultTypeGenerator);

    public static Diagnostic InvalidAttributeUsage(string message, Location location) =>
        Diagnostic.Create(
            new (
                id: "FUN0001",
                title: "Invalid attribute usage",
                messageFormat: $"{message} -  Please use ResultType attribute with typeof expression like [ResultType(typeof(MyError))]",
                category: Category,
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true
            ),
            location
        );
}