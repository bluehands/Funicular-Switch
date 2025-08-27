using FunicularSwitch.Generators;

namespace FunicularSwitch.Transformers;

[MonadTransformer(typeof(Result<>))]
public static class ResultT
{
    public static Monad<Result<B>> BindT<A, B>(Monad<Result<A>> ma, Func<A, Monad<Result<B>>> fn) =>
        ma.Bind(aResult => aResult.Match(fn, e => ma.Return(Result.Error<B>(e))));
}