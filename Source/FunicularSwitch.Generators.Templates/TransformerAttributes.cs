#nullable enable
using System;

namespace FunicularSwitch.Generators
{
    // TODO: move to own library
    public interface Monad<A>
    {
        public Monad<B> Bind<B>(Func<A, Monad<B>> fn);

        public Monad<B> Return<B>(B value);

        public B Cast<B>();
    }

    // TODO: move to own library
    public static class MonadExtension
    {
        public static Monad<B> Map<A, B>(this Monad<A> ma, Func<A, B> fn) => ma.Bind(a => ma.Return(fn(a)));
    }
}
