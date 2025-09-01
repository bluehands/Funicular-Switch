using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace FunicularSwitch.Transformers;

[MonadTransformer(typeof(EnumerableT))]
public static class EnumerableT
{
    [Pure]
    [DebuggerStepThrough]
    public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> ma, Func<A, IEnumerable<B>> fn) =>
        ma.SelectMany(fn);

    [Pure]
    [DebuggerStepThrough]
    public static Monad<IEnumerable<B>> BindT<A, B>(Monad<IEnumerable<A>> ma, Func<A, Monad<IEnumerable<B>>> fn) =>
        ma.Bind([DebuggerStepThrough](xs) => xs.Aggregate(
                ma.Return<IEnumerable<B>>([]),
                [DebuggerStepThrough](acc, cur) =>
                    acc.Bind([DebuggerStepThrough](ys) =>
                        fn(cur).Map(ys.Concat)
                    )
            )
        );

    [Pure]
    [DebuggerStepThrough]
    public static IEnumerable<A> Yield<A>(A a) => [a];
}
