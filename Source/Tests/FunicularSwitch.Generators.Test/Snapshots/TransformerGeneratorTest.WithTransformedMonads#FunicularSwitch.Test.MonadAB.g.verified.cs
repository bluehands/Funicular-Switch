//HintName: FunicularSwitch.Test.MonadAB.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadAB<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> M)
    {
        public static implicit operator MonadAB<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>(MonadAB<A> ma) => ma.M;
    }

    public static partial class MonadAB
    {
        public static FunicularSwitch.Test.MonadAB<A> Return<A>(A a) => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>.Return(FunicularSwitch.Test.MonadB<A>.Return(a));

        public static FunicularSwitch.Test.MonadAB<B> Bind<A, B>(this FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, FunicularSwitch.Test.MonadAB<B>> fn) => FunicularSwitch.Test.MonadBT.Bind<A, B, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<B>>>(ma, x => fn(x), FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<B>>.Return, (ma, fn) => ma.Bind(fn));

        public static FunicularSwitch.Test.MonadAB<A> Lift<A>(FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>.Return(FunicularSwitch.Test.MonadB<A>.Return(a)));
    }
}
