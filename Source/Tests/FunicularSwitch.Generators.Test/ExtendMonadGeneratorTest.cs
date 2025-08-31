namespace FunicularSwitch.Generators.Test;

[TestClass]
public class ExtendMonadGeneratorTest : VerifySourceGenerator<ExtendMonadGenerator>
{
    [TestMethod]
    public Task GenericMonad()
    {
        var source =
            /*lang=csharp*/
            """
            using System;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [ExtendMonad]
            public readonly record struct MonadA<A>
            {
                public static MonadA<A> Return(A a) => throw new NotImplementedException();
                
                public MonadA<B> Bind<B>(Func<A, MonadA<B>> fn) => throw new NotImplementedException();
            }
            """;

        return Verify(source);
    }

    [TestMethod]
    public Task StaticMonad()
    {
        var source =
            /*lang=csharp*/
            """
            using System;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public readonly record struct MonadA<A>;

            [ExtendMonad]
            public static partial class MonadA
            {
                public static MonadA<A> Return<A>(A a) => throw new NotImplementedException();
                
                public static MonadA<B> Bind<A, B>(this MonadA<A> ma, Func<A, MonadA<B>> fn) => throw new NotImplementedException();
            }
            """;

        return Verify(source);
    }

    [TestMethod]
    [DataRow("T")]
    [DataRow("T, U")]
    [DataRow("T, U, V")]
    [DataRow("T, U, V, W")]
    [DataRow("T, U, V, W, X")]
    public Task StaticMonadMultipleParameters(string typeArguments)
    {
        var source =
            /*lang=csharp*/
            $$"""
              using System;
              using FunicularSwitch.Generators;

              namespace FunicularSwitch.Test;

              public readonly record struct MonadA<{{typeArguments}}, A>;

              [ExtendMonad]
              public static partial class MonadA
              {
                  public static MonadA<{{typeArguments}}, A> Return<{{typeArguments}}, A>(A a) => throw new NotImplementedException();
                  
                  public static MonadA<{{typeArguments}}, B> Bind<{{typeArguments}}, A, B>(this MonadA<{{typeArguments}}, A> ma, Func<A, MonadA<{{typeArguments}}, B>> fn) => throw new NotImplementedException();
              }
              """;

        return Verify(source);
    }
}
