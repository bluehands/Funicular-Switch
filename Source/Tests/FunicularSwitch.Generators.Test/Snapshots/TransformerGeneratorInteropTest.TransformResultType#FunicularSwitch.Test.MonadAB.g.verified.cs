//HintName: FunicularSwitch.Test.MonadAB.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadAB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator MonadAB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(MonadAB<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadAB.Ok(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (MonadAB<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class MonadAB
    {
        private readonly record struct Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Test.MonadA<A>(Impl__FunicularSwitch_Test_MonadA<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadA<B>)global::FunicularSwitch.Test.MonadA<B>.Ok(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadA<B>)M.Bind(a => (global::FunicularSwitch.Test.MonadA<B>)(Impl__FunicularSwitch_Test_MonadA<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Test.MonadAB<A> Ok<A>(A a) => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>.Ok(global::FunicularSwitch.Test.MonadB<A>.Ok(a));

        public static global::FunicularSwitch.Test.MonadAB<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<B>> fn) => global::FunicularSwitch.Test.MonadBT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        public static global::FunicularSwitch.Test.MonadAB<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>.Ok(global::FunicularSwitch.Test.MonadB<A>.Ok(a)));

        public static global::FunicularSwitch.Test.MonadAB<B> Map<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadAB.Ok(fn(a)));
    }
}
