//HintName: FunicularSwitch.Test.MonadABC.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABC<A>(FunicularSwitch.Test.MonadAB<FunicularSwitch.Test.MonadC<A>> M)
    {
        public static implicit operator MonadABC<A>(FunicularSwitch.Test.MonadAB<FunicularSwitch.Test.MonadC<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Test.MonadAB<FunicularSwitch.Test.MonadC<A>>(MonadABC<A> ma) => ma.M;
    }

    public static partial class MonadABC
    {
        public static FunicularSwitch.Test.MonadABC<A> Return<A>(A a) => FunicularSwitch.Test.MonadAB.Return(FunicularSwitch.Test.MonadC<A>.Return(a));

        public static FunicularSwitch.Test.MonadABC<B> Bind<A, B>(this FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, FunicularSwitch.Test.MonadABC<B>> fn) => FunicularSwitch.Test.MonadCT.Bind<A, B, FunicularSwitch.Test.MonadAB<FunicularSwitch.Test.MonadC<A>>, FunicularSwitch.Test.MonadAB<FunicularSwitch.Test.MonadC<B>>>(ma, x => fn(x), FunicularSwitch.Test.MonadAB.Return, (ma, fn) => ma.Bind(fn));

        public static FunicularSwitch.Test.MonadABC<A> Lift<A>(FunicularSwitch.Test.MonadAB<A> ma) => ma.Bind(a => FunicularSwitch.Test.MonadAB.Return(FunicularSwitch.Test.MonadC<A>.Return(a)));
    }
}
