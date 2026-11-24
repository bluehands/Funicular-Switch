using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.ExtendMonad;

internal class Generator
{
    public static (string filename, string source) Emit(ExtendMonadInfo info, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        var filename = $"{info.FullTypeName}.g.cs";

        var cs = new CSharpBuilder("    ");
        using (info.Namespace != null ? cs.Namespace(info.Namespace) : null)
        {
            GeneralGenerator.WriteStaticMonad(info.StaticMonadGenerationInfo, cs, cancellationToken);
        }

        return (filename, cs);
    }
}
