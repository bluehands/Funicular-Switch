// ReSharper disable InconsistentNaming

using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace FunicularSwitch.Transformers;

public interface Monad<A>
{
    public Monad<B> Bind<B>(Func<A, Monad<B>> fn);

    public B Cast<B>();

    public Monad<B> Return<B>(B value);
}

public static class MonadExtension
{
    [Pure]
    [DebuggerStepThrough]
    public static Monad<B> Map<A, B>(this Monad<A> ma, Func<A, B> fn) => ma.Bind([DebuggerStepThrough](a) => ma.Return(fn(a)));
}
