//HintName: FunicularSwitch.Test.MonadABC.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABC<A>(global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator MonadABC<A>(global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>(MonadABC<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadABC.Return(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (MonadABC<A_>)fn(a));
    }

    public static partial class MonadABC
    {
        public static global::FunicularSwitch.Test.MonadABC<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadAB.Return(global::FunicularSwitch.Test.MonadC<A>.Return(a));

        public static global::FunicularSwitch.Test.MonadABC<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABC<B>> fn) => global::FunicularSwitch.Test.MonadCT.Bind<A, B, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>(ma, x => fn(x), global::FunicularSwitch.Test.MonadAB.Return, (ma, fn) => ma.Bind(fn));

        public static global::FunicularSwitch.Test.MonadABC<A> Lift<A>(global::FunicularSwitch.Test.MonadAB<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadAB.Return(global::FunicularSwitch.Test.MonadC<A>.Return(a)));
    }
}
