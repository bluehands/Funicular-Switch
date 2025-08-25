namespace FunicularSwitch.Generators.Test;

public class VerifyTransformerInteropSourceGenerator() : BaseVerifySourceGenerators(new TransformerGenerator(), new ResultTypeGenerator());

[TestClass]
public class TransformerGeneratorTest : VerifySourceGenerator<TransformerGenerator>
{
    [TestMethod]
    [DataRow("public partial class")]
    [DataRow("public partial struct")]
    [DataRow("public partial record")]
    [DataRow("public partial record struct")]
    [DataRow("public readonly partial struct")]
    [DataRow("public readonly partial record struct")]
    [DataRow("internal partial class")]
    public Task TypeModifiers(string modifier)
    {
        var code =
            /*lang=csharp*/
            $$"""
            using System;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record MonadA<A>(A Value);
            public class MonadA
            {
                public static MonadA<A> Return<A>(A a) => throw new NotImplementedException();
                
                public static MonadA<B> Bind<A, B>(MonadA<A> ma, Func<A, MonadA<B>> fn) => throw new NotImplementedException();
            }
            public record MonadB<B>(B Value);
            public class MonadB
            {
                public static MonadB<A> Return<A>(A a) => throw new NotImplementedException();
                
                public static MonadB<B> Bind<A, B>(MonadB<A> ma, Func<A, MonadB<B>> fn) => throw new NotImplementedException();
            }

            [MonadTransformer(typeof(MonadB))]
            public class MonadBT
            {
                public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
            }

            [TransformMonad(typeof(MonadA), typeof(MonadBT))]
            {{modifier}} MonadAMonadB<A>;
            """;

        return Verify(code);
    }

    [TestMethod]
    public Task ImplicitMonadTypes()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value);
              public class MonadA
              {
                  public static MonadA<A> Return<A>(A a) => throw new NotImplementedException();
                  
                  public static MonadA<B> Bind<A, B>(MonadA<A> ma, Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value);
              public class MonadB
              {
                  public static MonadB<A> Return<A>(A a) => throw new NotImplementedException();
                  
                  public static MonadB<B> Bind<A, B>(MonadB<A> ma, Func<A, MonadB<B>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB))]
              public class MonadBT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task ImplicitMonadMethods()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value);
              public class MonadA
              {
                  public static MonadA<A> ReturnA<A>(A a) => throw new NotImplementedException();
                  
                  public static MonadA<B> BindA<A, B>(MonadA<A> ma, Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value);
              public class MonadB
              {
                  public static MonadB<A> ReturnB<A>(A a) => throw new NotImplementedException();
                  
                  public static MonadB<B> BindB<A, B>(MonadB<A> ma, Func<A, MonadB<B>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB))]
              public class MonadBT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task ImplicitMonadMethodsFromGeneric()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value)
              {
                  public static MonadA<A> Return(A a) => throw new NotImplementedException();
                  
                  public MonadA<B> Bind<B>(Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value)
              {
                  public static MonadB<B> Return(B a) => throw new NotImplementedException();
                  
                  public MonadB<A> Bind<A>(Func<B, MonadB<A>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task MultipleTransformers()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              
              namespace FunicularSwitch.Test;
              
              public record MonadA<A>(A Value)
              {
                  public static MonadA<A> Return(A a) => throw new NotImplementedException();
                  
                  public MonadA<B> Bind<B>(Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value)
              {
                  public static MonadB<B> Return(B a) => throw new NotImplementedException();
                  
                  public MonadB<A> Bind<A>(Func<B, MonadB<A>> fn) => throw new NotImplementedException();
              }
              
              [MonadTransformer(typeof(MonadA<>))]
              public class MonadAT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadA<B>, TMB> returnM, Func<TMA, Func<MonadA<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }
              
              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }
              
              [TransformMonad(typeof(MonadA<>), typeof(MonadBT), typeof(MonadAT))]
              public readonly partial record struct MonadABA<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task WithTransformedMonads()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              
              namespace FunicularSwitch.Test;
              
              public record MonadA<A>(A Value)
              {
                  public static MonadA<A> Return(A a) => throw new NotImplementedException();
                  
                  public MonadA<B> Bind<B>(Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value)
              {
                  public static MonadB<B> Return(B a) => throw new NotImplementedException();
                  
                  public MonadB<A> Bind<A>(Func<B, MonadB<A>> fn) => throw new NotImplementedException();
              }
              public record MonadC<C>(C Value)
              {
                  public static MonadC<C> Return(C a) => throw new NotImplementedException();
                  
                  public MonadC<A> Bind<A>(Func<C, MonadC<A>> fn) => throw new NotImplementedException();
              }
              
              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }
              
              [MonadTransformer(typeof(MonadC<>))]
              public class MonadCT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadC<B>, TMB> returnM, Func<TMA, Func<MonadC<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }
              
              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              
              [TransformMonad(typeof(MonadAB<>), typeof(MonadCT))]
              public readonly partial record struct MonadABC<A>;
              """;

        return Verify(code);
    }
}

[TestClass]
public class TransformerGeneratorInteropTest : VerifyTransformerInteropSourceGenerator
{
    [TestMethod]
    public Task TransformResultType()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;

              namespace FunicularSwitch.Test;

              [ResultType(typeof(string))]
              public abstract partial class MonadA<T>;
              
              [ResultType(typeof(int))]
              public abstract partial class MonadB<T>;

              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<MonadB<B>, TMB> returnM, Func<TMA, Func<MonadB<A>, TMB>, TMB> bindM) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }
}