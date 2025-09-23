using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Generator
{
    public static (string filename, string source) Emit(
        TransformMonadInfo info,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var filename = $"{info.FullTypeName}.g.cs";

        var cs = new CSharpBuilder(defaultIntent: "    ");
        using (cs.Namespace(info.Namespace))
        {
            if (info.GenericMonadGenerationInfo is not null)
            {
                WriteGenericMonad(info.GenericMonadGenerationInfo, cs);
                GeneralGenerator.BlankLine(cs);
            }

            GeneralGenerator.WriteStaticMonad(info.StaticMonadGenerationInfo, cs, cancellationToken);
        }

        return (filename, cs);
    }

    private static void WriteGenericMonad(GenericMonadGenerationInfo data, CSharpBuilder cs)
    {
        var nestedTypeName = data.Monad.GenericTypeName(data.TypeParameters.Select(TypeInfo.Parameter).ToList());
        var extraTypeParametersString = string.Concat(data.TypeParameters.Take(data.TypeParameters.Count - 1).Select(x => $"{x}, "));
        var valueTypeParameter = data.TypeParameters.Last();
        var monadInterface = $"global::FunicularSwitch.Transformers.Monad<{valueTypeParameter}>";
        var altValueTypeParameter = $"{data.TypeParameters.Last()}_";
        var monadInterfaceAlt = $"global::FunicularSwitch.Transformers.Monad<{altValueTypeParameter}>";
        using var _ = new Scope(cs, $"{Types.DetermineAccessModifier(data.Accessibility)} {data.Modifier} {data.TypeNameWithTypeParameters}({nestedTypeName} M) : {monadInterface}");

        if (!data.IsRecord)
        {
            cs.WriteGetOnlyProperty(nestedTypeName.ToString(), "M", "M");
        }

        GeneralGenerator.WriteCommonMethodAttributes(cs);
        cs.WriteLine($"public static implicit operator {data.TypeNameWithTypeParameters}({nestedTypeName} ma) => new(ma);");
        GeneralGenerator.WriteCommonMethodAttributes(cs);
        cs.WriteLine($"public static implicit operator {nestedTypeName}({data.TypeNameWithTypeParameters} ma) => ma.M;");
        GeneralGenerator.WriteCommonMethodAttributes(cs);
        cs.WriteLine($"{monadInterfaceAlt} {monadInterface}.Return<{altValueTypeParameter}>({altValueTypeParameter} a) => {data.TypeName}.{data.Monad.ReturnMethod.Name}<{extraTypeParametersString}{altValueTypeParameter}>(a);");
        GeneralGenerator.WriteCommonMethodAttributes(cs);
        cs.WriteLine($"{monadInterfaceAlt} {monadInterface}.Bind<{altValueTypeParameter}>(global::System.Func<{valueTypeParameter}, {monadInterfaceAlt}> fn) => this.{data.Monad.BindMethod.Name}(a => ({data.TypeName}<{extraTypeParametersString}{altValueTypeParameter}>)fn(a));");
        GeneralGenerator.WriteCommonMethodAttributes(cs);
        cs.WriteLine($"{altValueTypeParameter} {monadInterface}.Cast<{altValueTypeParameter}>() => ({altValueTypeParameter})(object)M;");
    }
}
