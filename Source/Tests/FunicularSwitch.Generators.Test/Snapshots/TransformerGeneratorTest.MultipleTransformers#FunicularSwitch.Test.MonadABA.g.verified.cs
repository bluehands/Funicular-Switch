//HintName: FunicularSwitch.Test.MonadABA.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABA<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>> M)
    {
        public static implicit operator MonadABA<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>>(MonadABA<A> ma) => ma.M;
    }

    public static partial class MonadABA
    {
        public static FunicularSwitch.Test.MonadABA<A> Return<A>(A a) => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>>.Return(FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>.Return(FunicularSwitch.Test.MonadA<A>.Return(a)));

        public static FunicularSwitch.Test.MonadABA<B> Bind<A, B>(this FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, FunicularSwitch.Test.MonadABA<B>> fn) => FunicularSwitch.Test.MonadAT.Bind<A, B, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<B>>>>(ma, x => fn(x), x => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<B>>>.Return(FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<B>>.Return(x)), (ma, fn) => FunicularSwitch.Test.MonadBT.Bind<FunicularSwitch.Test.MonadA<A>, FunicularSwitch.Test.MonadA<B>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<B>>>>(ma, x => fn(x), FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<B>>>.Return, (ma, fn) => ma.Bind(fn)));

        public static FunicularSwitch.Test.MonadABA<A> Lift<A>(FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>>.Return(FunicularSwitch.Test.MonadB<FunicularSwitch.Test.MonadA<A>>.Return(FunicularSwitch.Test.MonadA<A>.Return(a))));
    }
}
