//HintName: FunicularSwitch.Test.MonadABA.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABA<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>> M)
    {
        public static implicit operator MonadABA<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>(MonadABA<A> ma) => ma.M;
    }

    public static partial class MonadABA
    {
        public static global::FunicularSwitch.Test.MonadABA<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>.Return(global::FunicularSwitch.Test.MonadA<A>.Return(a)));

        public static global::FunicularSwitch.Test.MonadABA<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABA<B>> fn) => global::FunicularSwitch.Test.MonadAT.Bind<A, B, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(ma, x => fn(x), x => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>.Return(x)), (ma, fn) => global::FunicularSwitch.Test.MonadBT.Bind<global::FunicularSwitch.Test.MonadA<A>, global::FunicularSwitch.Test.MonadA<B>, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(ma, x => fn(x), global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>.Return, (ma, fn) => ma.Bind(fn)));

        public static global::FunicularSwitch.Test.MonadABA<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>.Return(global::FunicularSwitch.Test.MonadA<A>.Return(a))));
    }
}
