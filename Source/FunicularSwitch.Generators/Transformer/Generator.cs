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
        var filename = $"{data.TargetTypeSymbol.FullTypeNameWithNamespace()}.g.cs";
        
        var outerMonadGenericType =
            (INamedTypeSymbol)data.MonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
        var innerMonadType = (INamedTypeSymbol)data.TransformerType.GetAttributes()[0].ConstructorArguments[0]
            .Value!;
        var innerMonadGenericType =
            (INamedTypeSymbol)innerMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
        Func<string, string> nestedTypeName = genericArgument =>
            $"{outerMonadGenericType.FullTypeNameWithNamespace()}<{innerMonadGenericType.FullTypeNameWithNamespace()}<{genericArgument}>>";
        var source =
            /*lang=csharp*/
            $$"""
              namespace {{data.TargetTypeSymbol.GetFullNamespace()}}
              {
                  public partial record {{data.TargetTypeSymbol.Name}}<{{data.TargetTypeSymbol.TypeArguments[0].Name}}>({{nestedTypeName(data.TargetTypeSymbol.TypeArguments[0].Name)}} M)
                  {
                      public static implicit operator {{data.TargetTypeSymbol.Name}}<{{data.TargetTypeSymbol.TypeArguments[0].Name}}>({{nestedTypeName(data.TargetTypeSymbol.TypeArguments[0].Name)}} ma) => new(ma);
                      public static implicit operator {{nestedTypeName(data.TargetTypeSymbol.TypeArguments[0].Name)}}({{data.TargetTypeSymbol.Name}}<{{data.TargetTypeSymbol.TypeArguments[0].Name}}> ma) => ma.M;
                  }
                  
                  public static partial class {{data.TargetTypeSymbol.Name}}
                  {
                      public static {{data.TargetTypeSymbol.Name}}<A> Return<A>(A a) => {{data.MonadType.FullTypeNameWithNamespace()}}.Return({{innerMonadType.FullTypeNameWithNamespace()}}.Return(a));
                      
                      public static {{data.TargetTypeSymbol.Name}}<B> Bind<A, B>(this {{data.TargetTypeSymbol.Name}}<A> ma, global::System.Func<A, {{data.TargetTypeSymbol.Name}}<B>> fn) => {{data.TransformerType.FullTypeNameWithNamespace()}}.Bind<A, B, {{nestedTypeName("A")}}, {{nestedTypeName("B")}}>(ma, x => fn(x), {{data.MonadType.FullTypeNameWithNamespace()}}.Return, {{data.MonadType.FullTypeNameWithNamespace()}}.Bind);
                      
                      public static {{data.TargetTypeSymbol.Name}}<A> Lift<A>({{outerMonadGenericType.FullTypeNameWithNamespace()}}<A> ma) => {{data.MonadType.FullTypeNameWithNamespace()}}.Bind(ma, a => {{data.MonadType.FullTypeNameWithNamespace()}}.Return({{innerMonadType.FullTypeNameWithNamespace()}}.Return(a)));
                  }
              }
              """;

        return (filename, source);
    }
}