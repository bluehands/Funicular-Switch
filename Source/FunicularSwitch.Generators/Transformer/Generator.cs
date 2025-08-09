using FunicularSwitch.Generators.Common;
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
            WriteStaticMonad(data, builder);
        }
        
        return (filename, builder);
    }
    
    private static string NestedTypeName(TransformMonadData data, string genericArgument) =>
        data.OuterMonad.GenericTypeName(data.InnerMonad.GenericTypeName(genericArgument));

    private static void WriteGenericMonad(TransformMonadData data, CSharpBuilder builder)
    {
        var nestedTypeName = NestedTypeName(data, data.TypeParameter);
        using var _ = new Scope(builder, $"{data.AccessModifier} {data.Modifier} {data.TypeNameWithTypeParameters}({nestedTypeName} M)");

        if (!data.IsRecord)
        {
             builder.WriteGetOnlyProperty(nestedTypeName, "M", "M");
        }
        
        builder.WriteLine($"public static implicit operator {data.TypeNameWithTypeParameters}({nestedTypeName} ma) => new(ma);");
        builder.WriteLine($"public static implicit operator {nestedTypeName}({data.TypeNameWithTypeParameters} ma) => ma.M;");
    }

    private static void WriteStaticMonad(TransformMonadData data, CSharpBuilder builder)
    {
        using var _ = builder.StaticPartialClass(data.TypeName, data.AccessModifier);
        builder.WriteLine($"public static {data.FullGenericType("A")} {data.ReturnName}<A>(A a) => {InvokeTransformReturn(data, "a")};");
        BlankLine(builder);
        builder.WriteLine($"public static {data.FullGenericType("B")} {data.BindName}<A, B>(this {data.FullGenericType("A")} ma, global::System.Func<A, {data.FullGenericType("B")}> fn) => {data.TransformerTypeName}.Bind<A, B, {NestedTypeName(data, "A")}, {NestedTypeName(data, "B")}>(ma, x => fn(x), {data.OuterMonad.ReturnMethodFullName}, {data.OuterMonad.BindMethodFullName});");
        BlankLine(builder);
        builder.WriteLine($"public static {data.FullGenericType("A")} Lift<A>({data.OuterMonad.GenericTypeName("A")} ma) => {InvokeMonadBind(data.OuterMonad, $"ma, a => {InvokeTransformReturn(data, "a")}")};");
    }

    private static void BlankLine(CSharpBuilder builder) => builder.Content.AppendLine();

    private static string InvokeMonadReturn(MonadData data, string argument) => $"{data.ReturnMethodFullName}({argument})";

    private static string InvokeTransformReturn(TransformMonadData data, string argument) =>
        InvokeMonadReturn(data.OuterMonad, InvokeMonadReturn(data.InnerMonad, argument));

    private static string InvokeMonadBind(MonadData data, string arguments) =>
        $"{data.BindMethodFullName}({arguments})";

}
