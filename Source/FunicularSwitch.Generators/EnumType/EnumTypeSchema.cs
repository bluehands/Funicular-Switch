using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using FunicularSwitch.Generators.Generation;

namespace FunicularSwitch.Generators.EnumType;

sealed record EnumTypeSchema(string? Namespace, string TypeName, string FullTypeName, EquatableArray<EnumCase> Cases, bool IsInternal, AttributePrecedence Precedence);

public sealed record EnumCase
{
    public string FullCaseName { get; }
    public string ParameterName { get; }

    public EnumCase(string fullCaseName, string caseName)
    {
        FullCaseName = fullCaseName;
        ParameterName = (caseName.Any(c => c != '_') ? caseName.TrimEnd('_') : caseName).ToParameterName();
    }
}