using FunicularSwitch.Generators.Transformer;

namespace FunicularSwitch.Generators.Generation;

internal static class GeneralGenerator
{
    public static void BlankLine(CSharpBuilder cs) => cs.Content.AppendLine();

    public static void WriteCommonMethodAttributes(CSharpBuilder cs) =>
        cs.WriteLine($"[global::System.Diagnostics.Contracts.PureAttribute, {Constants.DebuggerStepThroughAttribute}]");

    public static void WriteStaticMonad(StaticMonadGenerationInfo data, CSharpBuilder cs, CancellationToken cancellationToken)
    {
        using var _ = cs.StaticPartialClass(data.TypeName, Types.DetermineAccessModifier(data.Accessibility));

        foreach (var generationInfo in data.MonadsWithoutImplementation)
        {
            cancellationToken.ThrowIfCancellationRequested();
            WriteMonadInterfaceImplementation(generationInfo, cs);
            BlankLine(cs);
        }

        WriteMethod(data.Methods.First(), cs);
        foreach (var method in data.Methods.Skip(1))
        {
            cancellationToken.ThrowIfCancellationRequested();
            BlankLine(cs);
            WriteMethod(method, cs);
        }
    }

    private static void WriteMethod(MethodGenerationInfo info, CSharpBuilder cs)
    {
        var modifierList = new List<string>
        {
            "public",
            "static",
        };

        if (info.IsAsync)
            modifierList.Add("async");

        var modifiers = string.Join(" ", modifierList);
        var typeArgs = info.TypeParameters.Count > 0 ? $"<{string.Join(", ", info.TypeParameters)}>" : string.Empty;
        var args = string.Join(", ", info.Parameters.Select(x => $"{(x.IsExtension ? "this " : string.Empty)}{x.Type} {x.Name}"));
        WriteCommonMethodAttributes(cs);
        cs.WriteLine($"{modifiers} {info.ReturnType} {info.Name}{typeArgs}({args}) => {info.Body};");
    }

    private static void WriteMonadInterfaceImplementation(MonadImplementationGenerationInfo data, CSharpBuilder cs)
    {
        var interfaceA = InterfaceFn("A");
        var interfaceB = InterfaceFn("B");
        var typeNameA = data.GenericTypeName(["A"]);
        var typeNameB = data.GenericTypeName(["B"]);
        var monadTypeNameA = data.Monad.GenericTypeName(["A"]);
        var monadTypeNameB = data.Monad.GenericTypeName(["B"]);

        cs.WriteLine($"private readonly record struct {typeNameA}({monadTypeNameA} M) : {interfaceA}");
        using var _ = cs.Scope();
        WriteCommonMethodAttributes(cs);
        cs.WriteLine($"public static implicit operator {typeNameA}({monadTypeNameA} ma) => new(ma);");
        WriteCommonMethodAttributes(cs);
        cs.WriteLine($"public static implicit operator {monadTypeNameA}({typeNameA} ma) => ma.M;");
        WriteCommonMethodAttributes(cs);
        cs.WriteLine($"public {interfaceB} Return<B>(B a) => ({typeNameB}){data.Monad.ReturnMethod.Invoke(["B"], ["a"])};");
        WriteCommonMethodAttributes(cs);
        cs.WriteLine($"public {interfaceB} Bind<B>(global::System.Func<A, {interfaceB}> fn) => ({typeNameB}){data.Monad.BindMethod.Invoke(["A", "B"], ["M", $"a => ({monadTypeNameB})({typeNameB})fn(a)"])};");
        WriteCommonMethodAttributes(cs);
        cs.WriteLine("public B Cast<B>() => (B)(object)M;");

        static string InterfaceFn(string t) => $"global::FunicularSwitch.Transformers.Monad<{t}>";
    }
}
