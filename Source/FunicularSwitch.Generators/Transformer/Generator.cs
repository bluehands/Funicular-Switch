using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Generator
{
    public static (string filename, string source) Emit(
        TransformMonadData data,
        Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken)
    {
        var filename = $"{data.FullTypeName}.g.cs";

        var cs = new CSharpBuilder(defaultIntent: "    ");
        using (cs.Namespace(data.Namespace))
        {
            WriteGenericMonad(data, cs);
            BlankLine(cs);
            WriteStaticMonad(data.StaticMonadGenerationInfo, cs, cancellationToken);
        }

        return (filename, cs);
    }

    private static void BlankLine(CSharpBuilder cs) => cs.Content.AppendLine();

    private static void WriteGenericMonad(TransformMonadData data, CSharpBuilder cs)
    {
        var nestedTypeName = data.Monad.GenericTypeName(data.TypeParameter);
        var monadInterface = $"global::FunicularSwitch.Generators.Monad<{data.TypeParameter}>";
        var altTypeParameter = $"{data.TypeParameter}_";
        var monadInterfaceAlt = $"global::FunicularSwitch.Generators.Monad<{altTypeParameter}>";
        using var _ = new Scope(cs, $"{data.AccessModifier} {data.Modifier} {data.TypeNameWithTypeParameters}({nestedTypeName} M) : {monadInterface}");

        if (!data.IsRecord)
        {
            cs.WriteGetOnlyProperty(nestedTypeName, "M", "M");
        }

        // TODO: add missing global::
        cs.WriteLine($"public static implicit operator {data.TypeNameWithTypeParameters}({nestedTypeName} ma) => new(ma);");
        cs.WriteLine($"public static implicit operator {nestedTypeName}({data.TypeNameWithTypeParameters} ma) => ma.M;");
        cs.WriteLine($"{monadInterfaceAlt} {monadInterface}.Return<{altTypeParameter}>({altTypeParameter} a) => {data.TypeName}.{data.Monad.ReturnMethod.Name}(a);");
        cs.WriteLine($"{monadInterfaceAlt} {monadInterface}.Bind<{altTypeParameter}>(global::System.Func<{data.TypeParameter}, {monadInterfaceAlt}> fn) => this.{data.Monad.BindMethod.Name}(a => ({data.TypeName}<{altTypeParameter}>)fn(a));");
        cs.WriteLine($"{altTypeParameter} {monadInterface}.Cast<{altTypeParameter}>() => ({altTypeParameter})(object)M;");
    }

    private static void WriteStaticMonad(StaticMonadGenerationInfo data, CSharpBuilder cs, CancellationToken cancellationToken)
    {
        using var _ = cs.StaticPartialClass(data.TypeName, data.AccessModifier);
        
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
        var typeArgs = info.TypeParameters.Count > 0 ? $"<{string.Join(", ", info.TypeParameters)}>" : string.Empty;
        var args = string.Join(", ", info.Parameters.Select(x => ($"{(x.IsExtension ? "this " : string.Empty)}{x.Type} {x.Name}")));
        cs.WriteLine($"public static {info.ReturnType} {info.Name}{typeArgs}({args}) => {info.Body};");
    }

    private static void WriteMonadInterfaceImplementation(MonadImplementationGenerationInfo data, CSharpBuilder cs)
    {
        var interfaceA = InterfaceFn("A");
        var interfaceB = InterfaceFn("B");
        var typeNameA = data.GenericTypeName("A");
        var typeNameB = data.GenericTypeName("B");
        var monadTypeNameA = data.Monad.GenericTypeName("A");
        var monadTypeNameB = data.Monad.GenericTypeName("B");
        
        cs.WriteLine($"private readonly record struct {typeNameA}({monadTypeNameA} M) : {interfaceA}");
        using var _ = cs.Scope();
        cs.WriteLine($"public static implicit operator {typeNameA}({monadTypeNameA} ma) => new(ma);");
        cs.WriteLine($"public static implicit operator {monadTypeNameA}({typeNameA} ma) => ma.M;");
        cs.WriteLine($"public {interfaceB} Return<B>(B a) => ({typeNameB}){data.Monad.ReturnMethod.Invoke(["B"], ["a"])};");
        cs.WriteLine($"public {interfaceB} Bind<B>(global::System.Func<A, {interfaceB}> fn) => ({typeNameB}){data.Monad.BindMethod.Invoke(["A", "B"], ["M", $"a => ({monadTypeNameB})({typeNameB})fn(a)"])};");
        cs.WriteLine($"public B Cast<B>() => (B)(object)M;");
        static string InterfaceFn(string t) => $"global::FunicularSwitch.Generators.Monad<{t}>";
    }
}