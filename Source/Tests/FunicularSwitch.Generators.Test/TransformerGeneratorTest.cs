namespace FunicularSwitch.Generators.Test;

public class VerifyTransformerInteropSourceGenerator() : BaseVerifySourceGenerators(new TransformerGenerator(), new ResultTypeGenerator());

[TestClass]
public class TransformerGeneratorTest : VerifySourceGenerator<TransformerGenerator>
{
    [TestMethod]
    public Task GenericMonadsMissingBindMethod()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value)
              {
                  public static MonadA<A> Return(A a) => throw new NotImplementedException();
              }

              public record MonadB<B>(B Value)
              {
                  public static MonadB<B> Return(B a) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    [DataRow("T, ", "")]
    [DataRow("", "T, ")]
    [DataRow("T, ", "U, ")]
    [DataRow("T, U, ", "V, ")]
    [DataRow("T, U, V, ", "W, X, ")]
    public Task GenericTransformedMonadWithMultipleParameters(string outerTypeParameters, string innerTypeParameters)
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<{{outerTypeParameters}}A>(A Value);
              public class MonadA
              {
                  public static MonadA<{{outerTypeParameters}}A> Return<{{outerTypeParameters}}A>(A a) => throw new NotImplementedException();
                  
                  public static MonadA<{{outerTypeParameters}}B> Bind<{{outerTypeParameters}}A, B>(MonadA<{{outerTypeParameters}}A> ma, Func<A, MonadA<{{outerTypeParameters}}B>> fn) => throw new NotImplementedException();
              }

              public record MonadB<{{innerTypeParameters}}B>(B Value);
              public class MonadB
              {
                  public static MonadB<{{innerTypeParameters}}A> Return<{{innerTypeParameters}}A>(A a) => throw new NotImplementedException();
                  
                  public static MonadB<{{innerTypeParameters}}B> Bind<{{innerTypeParameters}}A, B>(MonadB<{{innerTypeParameters}}A> ma, Func<A, MonadB<{{innerTypeParameters}}B>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB))]
              public class MonadBT
              {
                  public static Monad<MonadB<{{innerTypeParameters}}B>> BindT<{{innerTypeParameters}}A, B>(Monad<MonadB<{{innerTypeParameters}}A>> ma, Func<A, Monad<MonadB<{{innerTypeParameters}}B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public readonly partial record struct MonadAB<{{outerTypeParameters}}{{innerTypeParameters}}A>;
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
              using FunicularSwitch.Transformers;

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
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
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
              using FunicularSwitch.Transformers;

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
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
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
              using FunicularSwitch.Transformers;

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
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task InnerMonadWithoutReturnMethod()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value)
              {
                  public static MonadA<A> Return(A a) => throw new NotImplementedException();
                  
                  public MonadA<B> Bind<B>(Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }

              public record MonadB<B>(B Value)
              {
                  public MonadB<A> Bind<A>(Func<B, MonadB<A>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task MonadTransformerMissingBindTMethod()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

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
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task MonadTransformerWithoutAttribute()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

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

              public class MonadBT
              {
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
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
            """
            using System;
            using FunicularSwitch.Generators;
            using FunicularSwitch.Transformers;

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
                public static Monad<MonadA<B>> BindT<A, B>(Monad<MonadA<A>> ma, Func<A, Monad<MonadA<B>>> fn) => throw new NotImplementedException();
            }

            [MonadTransformer(typeof(MonadB<>))]
            public class MonadBT
            {
                public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
            }

            [TransformMonad(typeof(MonadA<>), typeof(MonadBT), typeof(MonadAT))]
            public readonly partial record struct MonadABA<A>;
            """;

        return Verify(code);
    }

    [TestMethod]
    public Task OuterMonadWithoutReturnMethod()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value)
              {
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
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task StaticMonadMissingReturnMethod()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value);
              public class MonadA
              {
                  public static MonadA<B> Bind<A, B>(MonadA<A> ma, Func<A, MonadA<B>> fn) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value);
              public class MonadB
              {
                  public static MonadB<B> Bind<A, B>(MonadB<A> ma, Func<A, MonadB<B>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB))]
              public class MonadBT
              {
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task StaticMonadsMissingBindMethod()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<A>(A Value);
              public class MonadA
              {
                  public static MonadA<A> Return<A>(A a) => throw new NotImplementedException();
              }
              public record MonadB<B>(B Value);
              public class MonadB
              {
                  public static MonadB<A> Return<A>(A a) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB))]
              public class MonadBT
              {
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task StaticTransformedMonad()
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

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
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public static partial class MonadAB;
              """;

        return Verify(code);
    }

    [TestMethod]
    [DataRow("T, ", "")]
    [DataRow("", "T, ")]
    [DataRow("T, ", "U, ")]
    [DataRow("T, U, ", "V, ")]
    [DataRow("T, U, V, ", "W, X, ")]
    [DataRow("T, U, V, ", "T, U, V, ")]
    public Task StaticTransformedMonadWithMultipleParameters(string outerTypeParameters, string innerTypeParameters)
    {
        var code =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              public record MonadA<{{outerTypeParameters}}A>(A Value);
              public class MonadA
              {
                  public static MonadA<{{outerTypeParameters}}A> Return<{{outerTypeParameters}}A>(A a) => throw new NotImplementedException();
                  
                  public static MonadA<{{outerTypeParameters}}B> Bind<{{outerTypeParameters}}A, B>(MonadA<{{outerTypeParameters}}A> ma, Func<A, MonadA<{{outerTypeParameters}}B>> fn) => throw new NotImplementedException();
              }

              public record MonadB<{{innerTypeParameters}}B>(B Value);
              public class MonadB
              {
                  public static MonadB<{{innerTypeParameters}}A> Return<{{innerTypeParameters}}A>(A a) => throw new NotImplementedException();
                  
                  public static MonadB<{{innerTypeParameters}}B> Bind<{{innerTypeParameters}}A, B>(MonadB<{{innerTypeParameters}}A> ma, Func<A, MonadB<{{innerTypeParameters}}B>> fn) => throw new NotImplementedException();
              }

              [MonadTransformer(typeof(MonadB))]
              public class MonadBT
              {
                  public static Monad<MonadB<{{innerTypeParameters}}B>> BindT<{{innerTypeParameters}}A, B>(Monad<MonadB<{{innerTypeParameters}}A>> ma, Func<A, Monad<MonadB<{{innerTypeParameters}}B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              public static partial class MonadAB;
              """;

        return Verify(code);
    }

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
              using FunicularSwitch.Transformers;

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
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA), typeof(MonadBT))]
              {{modifier}} MonadAMonadB<A>;
              """;

        return Verify(code);
    }

    [TestMethod]
    public Task WithMonadImplementingInterface()
    {
        var code =
            /*lang=csharp*/
            """
            using System;
            using FunicularSwitch.Generators;
            using FunicularSwitch.Transformers;

            namespace FunicularSwitch.Test;

            public record MonadA<A>(A Value) : Monad<A>
            {
                public static MonadA<A> Return(A a) => throw new NotImplementedException();
                
                public MonadA<B> Bind<B>(Func<A, MonadA<B>> fn) => throw new NotImplementedException();
                
                Monad<B> Monad<A>.Return<B>(B value) => throw new NotImplementedException();
                
                Monad<B> Monad<A>.Bind<B>(Func<A, Monad<B>> fn) => throw new NotImplementedException();
                
                B Monad<A>.Cast<B>() => throw new NotImplementedException();
            }

            public record MonadB<B>(B Value)
            {
                public static MonadB<B> Return(B a) => throw new NotImplementedException();
                
                public MonadB<A> Bind<A>(Func<B, MonadB<A>> fn) => throw new NotImplementedException();
            }

            [MonadTransformer(typeof(MonadB<>))]
            public class MonadBT
            {
                public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
            }

            [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
            public readonly partial record struct MonadAB<A>;
            """;

        return Verify(code);
    }

    [TestMethod]
    public Task WithTransformedMonads()
    {
        var code =
            /*lang=csharp*/
            """
            using System;
            using FunicularSwitch.Generators;
            using FunicularSwitch.Transformers;

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
                public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
            }

            [MonadTransformer(typeof(MonadC<>))]
            public class MonadCT
            {
                public static Monad<MonadC<B>> BindT<A, B>(Monad<MonadC<A>> ma, Func<A, Monad<MonadC<B>>> fn) => throw new NotImplementedException();
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
              using FunicularSwitch.Transformers;

              namespace FunicularSwitch.Test;

              [ResultType(typeof(string))]
              public abstract partial class MonadA<T>;

              [ResultType(typeof(int))]
              public abstract partial class MonadB<T>;

              [MonadTransformer(typeof(MonadB<>))]
              public class MonadBT
              {
                  public static Monad<MonadB<B>> BindT<A, B>(Monad<MonadB<A>> ma, Func<A, Monad<MonadB<B>>> fn) => throw new NotImplementedException();
              }

              [TransformMonad(typeof(MonadA<>), typeof(MonadBT))]
              public readonly partial record struct MonadAB<A>;
              """;

        return Verify(code);
    }
}
