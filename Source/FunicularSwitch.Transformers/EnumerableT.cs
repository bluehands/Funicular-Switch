namespace FunicularSwitch.Transformers;

[MonadTransformer(typeof(EnumerableT))]
public static class EnumerableT
{
    public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> ma, Func<A, IEnumerable<B>> fn) =>
        ma.SelectMany(fn);

    public static IEnumerable<A> Yield<A>(A a) => [a];
    
    public static Monad<IEnumerable<B>> BindT<A, B>(Monad<IEnumerable<A>> ma, Func<A, Monad<IEnumerable<B>>> fn) =>
        ma.Bind(xs => xs.Aggregate(
            ma.Return<IEnumerable<B>>([]),
            (cur, acc) => cur.Bind(xs_ =>
                fn(acc).Map(y => (IEnumerable<B>) [..xs_, ..y]))));
}
