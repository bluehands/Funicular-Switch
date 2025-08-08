namespace FunicularSwitch.Generators.Test;

[TestClass]
public class TransformerGeneratorTest : VerifySourceGenerator<TransformerGenerator>
{
    [TestMethod]
    [DataRow("public readonly partial record struct")]
    [DataRow("public partial record")]
    public Task TypeModifiers(string modifier)
    {
        var code =
            /*lang=csharp*/
            $$"""
            using System;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record MonadA<A>(A Value);
            [Monad(typeof(MonadA<>))]
            public class MonadA
            {
                public static MonadA<A> Return<A>(A a) => throw new NotImplementedException();
                
                public static MonadA<B> Bind<A, B>(MonadA<A> ma, Func<A, MonadA<B>> fn) => throw new NotImplementedException();
            }
            public record MonadB<B>(B Value);
            [Monad(typeof(MonadB<>))]
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
}