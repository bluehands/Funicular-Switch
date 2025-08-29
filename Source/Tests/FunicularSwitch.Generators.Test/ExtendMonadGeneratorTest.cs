namespace FunicularSwitch.Generators.Test;

[TestClass]
public class ExtendMonadGeneratorTest : VerifySourceGenerator<ExtendMonadGenerator>
{
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
}
