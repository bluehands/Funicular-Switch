using FunicularSwitch.Generators.Common;
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

        var source =
            /*lang=csharp*/
            $$"""
              namespace {{data.Namespace}}
              {
                  public {{data.Modifier}} {{data.TypeNameWithTypeParameters}}({{NestedTypeName(data.TypeParameter)}} M)
                  {
                      public static implicit operator {{data.TypeNameWithTypeParameters}}({{NestedTypeName(data.TypeParameter)}} ma) => new(ma);
                      public static implicit operator {{NestedTypeName(data.TypeParameter)}}({{data.TypeNameWithTypeParameters}} ma) => ma.M;
                  }
                  
                  public static partial class {{data.TypeName}}
                  {
                      public static {{data.FullGenericType("A")}} Return<A>(A a) => {{data.OuterMonad.StaticTypeName}}.Return({{data.InnerMonad.StaticTypeName}}.Return(a));
                      
                      public static {{data.FullGenericType("B")}} Bind<A, B>(this {{data.FullGenericType("A")}} ma, global::System.Func<A, {{data.FullGenericType("B")}}> fn) => {{data.TransformerTypeName}}.Bind<A, B, {{NestedTypeName("A")}}, {{NestedTypeName("B")}}>(ma, x => fn(x), {{data.OuterMonad.StaticTypeName}}.Return, {{data.OuterMonad.StaticTypeName}}.Bind);
                      
                      public static {{data.FullGenericType("A")}} Lift<A>({{data.OuterMonad.GenericTypeName("A")}} ma) => {{data.OuterMonad.StaticTypeName}}.Bind(ma, a => {{data.OuterMonad.StaticTypeName}}.Return({{data.InnerMonad.StaticTypeName}}.Return(a)));
                  }
              }
              """;

        return (filename, source);

        string NestedTypeName(string genericArgument) =>
            data.OuterMonad.GenericTypeName(data.InnerMonad.GenericTypeName(genericArgument));
    }
}