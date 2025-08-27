using FunicularSwitch.Generators;

namespace FunicularSwitch.Transformers;

[MonadTransformer(typeof(Option<>))]
public static class OptionT
{
    public static Monad<Option<B>> BindT<A, B>(Monad<Option<A>> ma, Func<A, Monad<Option<B>>> fn) =>
        ma.Bind(aOption => aOption.Match(fn, () => ma.Return(Option.None<B>())));
}