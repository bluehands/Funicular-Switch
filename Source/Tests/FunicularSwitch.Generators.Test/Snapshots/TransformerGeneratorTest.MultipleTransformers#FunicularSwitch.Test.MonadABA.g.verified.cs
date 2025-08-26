//HintName: FunicularSwitch.Test.MonadABA.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABA<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator MonadABA<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>(MonadABA<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadABA.Return(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (MonadABA<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class MonadABA
    {
        private readonly record struct Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Test.MonadA<A>(Impl__FunicularSwitch_Test_MonadA<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadA<B>)global::FunicularSwitch.Test.MonadA<B>.Return(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadA<B>)M.Bind(a => (global::FunicularSwitch.Test.MonadA<B>)(Impl__FunicularSwitch_Test_MonadA<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        private readonly record struct Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<B>)global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>.Return(global::FunicularSwitch.Test.MonadB<B>.Return(a));
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<B>)global::FunicularSwitch.Test.MonadBT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)M, a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => (global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>)(Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<B>)fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Test.MonadABA<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>.Return(global::FunicularSwitch.Test.MonadA<A>.Return(a)));

        public static global::FunicularSwitch.Test.MonadABA<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABA<B>> fn) => global::FunicularSwitch.Test.MonadAT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>)ma), a => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>();

        public static global::FunicularSwitch.Test.MonadABA<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>.Return(global::FunicularSwitch.Test.MonadA<A>.Return(a))));

        public static global::FunicularSwitch.Test.MonadABA<B> Map<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadABA.Return(fn(a)));
    }
}
