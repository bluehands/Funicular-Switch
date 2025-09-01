using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace FunicularSwitch.Transformers;

[MonadTransformer(typeof(Result<>))]
public static class ResultT
{
    [Pure]
    [DebuggerStepThrough]
    public static Monad<Result<B>> BindT<A, B>(Monad<Result<A>> ma, Func<A, Monad<Result<B>>> fn) =>
        ma.Bind([DebuggerStepThrough](aResult) => aResult.Match(fn, [DebuggerStepThrough](e) => ma.Return(Result.Error<B>(e))));
}
