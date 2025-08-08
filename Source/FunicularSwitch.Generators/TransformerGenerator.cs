using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators;

[Generator]
public class TransformerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "Attributes.g.cs",
            /*lang=csharp*/
            """
            #nullable enable
            using System;
            namespace FunicularSwitch.Generators
            {
                [AttributeUsage(AttributeTargets.Class, Inherited = false)]
                internal sealed class MonadAttribute : Attribute
                {
                    public MonadAttribute(Type monadType)
                    {
                        MonadType = monadType;
                    }
                    
                    public Type MonadType { get; }
                }
                
                [AttributeUsage(AttributeTargets.Class, Inherited = false)]
                internal sealed class MonadTransformerAttribute : Attribute
                {
                    public MonadTransformerAttribute(Type monadType)
                    {
                        MonadType = monadType;
                    }
                    
                    public Type MonadType { get; }
                }
                
                [AttributeUsage(AttributeTargets.Class, Inherited = false)]
                internal sealed class TransformMonadAttribute : Attribute
                {
                    public TransformMonadAttribute(Type monadType, Type transformerType)
                    {
                        MonadType = monadType;
                        TransformerType = transformerType;
                    }
                    
                    public Type MonadType { get; }
                    
                    public Type TransformerType { get; }
                }
            }
            """));

        context.RegisterSourceOutput(
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "FunicularSwitch.Generators.TransformMonadAttribute",
                (node, token) => true,
                (syntaxContext, token) =>
                {
                    return new TransformMonadData(
                        (INamedTypeSymbol)syntaxContext.TargetSymbol,
                        (INamedTypeSymbol)syntaxContext.Attributes[0].ConstructorArguments[0].Value!,
                        (INamedTypeSymbol)syntaxContext.Attributes[0].ConstructorArguments[1].Value!);
                }),
            (productionContext, data) =>
            {
                var outerMonadGenericType =
                    (INamedTypeSymbol)data.MonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
                var innerMonadType = (INamedTypeSymbol)data.TransformerType.GetAttributes()[0].ConstructorArguments[0]
                    .Value!;
                var innerMonadGenericType =
                    (INamedTypeSymbol)innerMonadType.GetAttributes()[0].ConstructorArguments[0].Value!;
                Func<string, string> nestedTypeName = genericArgument => $"{outerMonadGenericType.FullTypeNameWithNamespace()}<{innerMonadGenericType.FullTypeNameWithNamespace()}<{genericArgument}>>";
                productionContext.AddSource(
                    $"{data.TargetTypeSymbol.FullTypeNameWithNamespace()}.g.cs",
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
                     """);
            });
    }

    internal record TransformMonadData(INamedTypeSymbol TargetTypeSymbol, INamedTypeSymbol MonadType, INamedTypeSymbol TransformerType);
}