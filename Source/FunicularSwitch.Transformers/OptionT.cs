using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace FunicularSwitch.Transformers;

[MonadTransformer(typeof(Option<>))]
public static class OptionT
{
    [Pure]
    [DebuggerStepThrough]
    public static Monad<Option<B>> BindT<A, B>(Monad<Option<A>> ma, Func<A, Monad<Option<B>>> fn) =>
        ma.Bind([DebuggerStepThrough](aOption) => aOption.Match(fn, [DebuggerStepThrough]() => ma.Return(Option.None<B>())));
}
