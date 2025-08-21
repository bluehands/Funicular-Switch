//HintName: FunicularSwitch.Test.MonadAB.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadAB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator MonadAB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(MonadAB<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadAB.ReturnAReturnB(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.BindABindB(a => (MonadAB<A_>)fn(a));
    }

    public static partial class MonadAB
    {
        public static global::FunicularSwitch.Test.MonadAB<A> ReturnAReturnB<A>(A a) => global::FunicularSwitch.Test.MonadA.ReturnA(global::FunicularSwitch.Test.MonadB.ReturnB(a));

        public static global::FunicularSwitch.Test.MonadAB<B> BindABindB<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<B>> fn) => global::FunicularSwitch.Test.MonadBT.Bind<A, B, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(ma, x => fn(x), global::FunicularSwitch.Test.MonadA.ReturnA, global::FunicularSwitch.Test.MonadA.BindA);

        public static global::FunicularSwitch.Test.MonadAB<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => global::FunicularSwitch.Test.MonadA.BindA(ma, a => global::FunicularSwitch.Test.MonadA.ReturnA(global::FunicularSwitch.Test.MonadB.ReturnB(a)));
    }
}
