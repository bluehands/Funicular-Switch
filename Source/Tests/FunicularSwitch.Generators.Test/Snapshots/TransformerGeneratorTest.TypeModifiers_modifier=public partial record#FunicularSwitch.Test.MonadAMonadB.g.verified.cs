//HintName: FunicularSwitch.Test.MonadAMonadB.g.cs
namespace FunicularSwitch.Test
{
    public partial record MonadAMonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M)
    {
        public static implicit operator MonadAMonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(MonadAMonadB<A> ma) => ma.M;
    }

    public static partial class MonadAMonadB
    {
        public static global::FunicularSwitch.Test.MonadAMonadB<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadA.Return(global::FunicularSwitch.Test.MonadB.Return(a));

        public static global::FunicularSwitch.Test.MonadAMonadB<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn) => global::FunicularSwitch.Test.MonadBT.Bind<A, B, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(ma, x => fn(x), global::FunicularSwitch.Test.MonadA.Return, global::FunicularSwitch.Test.MonadA.Bind);

        public static global::FunicularSwitch.Test.MonadAMonadB<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => global::FunicularSwitch.Test.MonadA.Bind(ma, a => global::FunicularSwitch.Test.MonadA.Return(global::FunicularSwitch.Test.MonadB.Return(a)));
    }
}
