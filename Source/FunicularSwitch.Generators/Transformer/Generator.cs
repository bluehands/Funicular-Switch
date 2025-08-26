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

        var builder = new CSharpBuilder(defaultIntent: "    ");
        using (builder.Namespace(data.Namespace))
        {
            WriteGenericMonad(data, builder);
            BlankLine(builder);
            if(data.StaticMonadGenerationInfo is null)
                WriteStaticMonad(data, builder);
            else
                WriteStaticMonad(data.StaticMonadGenerationInfo, builder);
        }

        return (filename, builder);
    }

    private static void BlankLine(CSharpBuilder builder) => builder.Content.AppendLine();

    private static void WriteGenericMonad(TransformMonadData data, CSharpBuilder builder)
    {
        var nestedTypeName = data.Monad.GenericTypeName(data.TypeParameter);
        var monadInterface = $"global::FunicularSwitch.Generators.Monad<{data.TypeParameter}>";
        var altTypeParameter = $"{data.TypeParameter}_";
        var monadInterfaceAlt = $"global::FunicularSwitch.Generators.Monad<{altTypeParameter}>";
        using var _ = new Scope(builder, $"{data.AccessModifier} {data.Modifier} {data.TypeNameWithTypeParameters}({nestedTypeName} M) : {monadInterface}");

        if (!data.IsRecord)
        {
            builder.WriteGetOnlyProperty(nestedTypeName, "M", "M");
        }

        // TODO: add missing global::
        builder.WriteLine($"public static implicit operator {data.TypeNameWithTypeParameters}({nestedTypeName} ma) => new(ma);");
        builder.WriteLine($"public static implicit operator {nestedTypeName}({data.TypeNameWithTypeParameters} ma) => ma.M;");
        builder.WriteLine($"{monadInterfaceAlt} {monadInterface}.Return<{altTypeParameter}>({altTypeParameter} a) => {data.TypeName}.{data.Monad.ReturnMethod.Name}(a);");
        builder.WriteLine($"{monadInterfaceAlt} {monadInterface}.Bind<{altTypeParameter}>(global::System.Func<{data.TypeParameter}, {monadInterfaceAlt}> fn) => this.{data.Monad.BindMethod.Name}(a => ({data.TypeName}<{altTypeParameter}>)fn(a));");
        builder.WriteLine($"{altTypeParameter} {monadInterface}.Cast<{altTypeParameter}>() => ({altTypeParameter})(object)M;");
    }

    private static void WriteStaticMonad(TransformMonadData data, CSharpBuilder builder)
    {
        using var _ = builder.StaticPartialClass(data.TypeName, data.AccessModifier);
        
        foreach (var generationInfo in data.MonadsWithoutImplementation)
        {
            WriteMonadInterfaceImplementation(generationInfo, builder);
            BlankLine(builder);
        }
        
        builder.WriteLine($"public static {data.FullGenericType("A")} {data.Monad.ReturnMethod.Name}<A>(A a) => {data.Monad.ReturnMethod.Invoke(["A"], ["a"])};");
        BlankLine(builder);
        builder.WriteLine($"public static {data.FullGenericType("B")} {data.Monad.BindMethod.Name}<A, B>(this {data.FullGenericType("A")} ma, global::System.Func<A, {data.FullGenericType("B")}> fn) => {data.Monad.BindMethod.Invoke(["A", "B"], [$"({data.Monad.GenericTypeName("A")})ma", "fn"])};");
        BlankLine(builder);
        builder.WriteLine($"public static {data.FullGenericType("A")} Lift<A>({data.OuterMonad.GenericTypeName("A")} ma) => {data.OuterMonad.BindMethod.Invoke(["A", "B"], ["ma", $"a => {data.Monad.ReturnMethod.Invoke(["A"], ["a"])}"])};");
    }

    private static void WriteStaticMonad(StaticMonadGenerationInfo data, CSharpBuilder builder)
    {
        using var _ = builder.StaticPartialClass(data.TypeName, data.AccessModifier);
        
        foreach (var generationInfo in data.MonadsWithoutImplementation)
        {
            WriteMonadInterfaceImplementation(generationInfo, builder);
            BlankLine(builder);
        }
        
        WriteMethod(data.ReturnMethod, builder);
        BlankLine(builder);
        WriteMethod(data.BindMethod, builder);
        BlankLine(builder);
        WriteMethod(data.LiftMethod, builder);
    }

    private static void WriteMethod(MethodGenerationInfo info, CSharpBuilder cs)
    {
        var typeArgs = info.TypeParameters.Count > 0 ? $"<{string.Join(", ", info.TypeParameters)}>" : string.Empty;
        var args = string.Join(", ", info.Parameters.Select(x => ($"{(x.IsExtension ? "this " : string.Empty)}{x.Type} {x.Name}")));
        cs.WriteLine(
            $"public static {info.ReturnType} {info.Info.Name}{typeArgs}({args}) => {info.Info.Invoke(info.TypeParameters, info.Parameters.Select(x => x.Name).ToList())};");
    }

    private static void WriteMonadInterfaceImplementation(MonadImplementationGenerationInfo data, CSharpBuilder cs)
    {
        var interfaceFn = (string t) => $"global::FunicularSwitch.Generators.Monad<{t}>";
        var interfaceA = interfaceFn("A");
        var interfaceB = interfaceFn("B");
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
    }

    public static string GetMonadInterfaceImplementationName(MonadImplementationInfoEx info, IReadOnlyList<string> typeParameters)
    {
        // return info.GenericTypeName(typeParameters[0]);
        return $"{info.Info.FullName}<{string.Join(", ", typeParameters)}>";
        
        // var dataGenericTypeName = data.GenericTypeName(typeParameters[0]);
        // var typeArgumentsIndex = dataGenericTypeName.IndexOf('<');
        // var implName = dataGenericTypeName[8..typeArgumentsIndex].Replace('.', '_');
        // var arguments = dataGenericTypeName[typeArgumentsIndex..];
        // return $"Impl__{implName}{arguments}";
    }
}

internal record StaticMonadGenerationInfo(
    string TypeName,
    string AccessModifier,
    IReadOnlyList<MonadImplementationGenerationInfo> MonadsWithoutImplementation,
    MethodGenerationInfo ReturnMethod,
    MethodGenerationInfo BindMethod,
    MethodGenerationInfo LiftMethod);

internal record MethodGenerationInfo(
    string ReturnType,
    IReadOnlyList<string> TypeParameters,
    IReadOnlyList<ParameterGenerationInfo> Parameters,
    MethodInfo Info);

internal record ParameterGenerationInfo(
    string Type,
    string Name,
    bool IsExtension = false);
