//HintName: FunicularSwitch.Test.MonadABA.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABA<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> M)
    {
        public static implicit operator MonadABA<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>(MonadABA<A> ma) => ma.M;
    }

    public static partial class MonadABA
    {
        public static FunicularSwitch.Test.MonadABA<A> Return<A>(A a) => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>.Return(FunicularSwitch.Test.MonadB<A>.Return(a));

        public static FunicularSwitch.Test.MonadABA<B> Bind<A, B>(this FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, FunicularSwitch.Test.MonadABA<B>> fn) => FunicularSwitch.Test.MonadBT.Bind<A, B, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<B>>>(ma, x => fn(x), FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<B>>.Return, (ma, fn) => ma.Bind(fn));

        public static FunicularSwitch.Test.MonadABA<A> Lift<A>(FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>.Return(FunicularSwitch.Test.MonadB<A>.Return(a)));
    }
}
