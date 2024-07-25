using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Common
{
    public static class SourceProductionContextExtension
    {
        public static void ReportDiagnostic(this SourceProductionContext context, DiagnosticInfo diagnostic) => 
            context.ReportDiagnostic(Diagnostic.Create(diagnostic.Descriptor, diagnostic.Location?.ToLocation()));
    }
}