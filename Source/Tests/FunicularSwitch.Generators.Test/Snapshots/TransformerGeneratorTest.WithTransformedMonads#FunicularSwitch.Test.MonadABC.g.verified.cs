//HintName: FunicularSwitch.Test.MonadABC.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABC<A>(global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator MonadABC<A>(global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>(MonadABC<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadABC.Return(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (MonadABC<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class MonadABC
    {
        private readonly record struct Impl__FunicularSwitch_Test_MonadAB<A>(global::FunicularSwitch.Test.MonadAB<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Test_MonadAB<A>(global::FunicularSwitch.Test.MonadAB<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Test.MonadAB<A>(Impl__FunicularSwitch_Test_MonadAB<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadAB<B>)global::FunicularSwitch.Test.MonadAB.Return(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadAB<B>)M.Bind(a => (global::FunicularSwitch.Test.MonadAB<B>)(Impl__FunicularSwitch_Test_MonadAB<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Test.MonadABC<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadAB.Return(global::FunicularSwitch.Test.MonadC<A>.Return(a));

        public static global::FunicularSwitch.Test.MonadABC<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABC<B>> fn) => global::FunicularSwitch.Test.MonadCT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<A>>)((global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>();

        public static global::FunicularSwitch.Test.MonadABC<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>> fn) => global::FunicularSwitch.Test.MonadCT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<A>>)((global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>();

        public static global::FunicularSwitch.Test.MonadABC<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABC<B>> fn) => global::FunicularSwitch.Test.MonadCT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<A>>)((global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>();

        public static global::FunicularSwitch.Test.MonadABC<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>> fn) => global::FunicularSwitch.Test.MonadCT.Bind<A, B>((Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<A>>)((global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadAB<global::FunicularSwitch.Test.MonadC<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>>();

        public static global::FunicularSwitch.Test.MonadABC<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABC<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadABC<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Test.MonadABC<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<global::FunicularSwitch.Test.MonadC<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadABC<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Test.MonadABC<A> Lift<A>(global::FunicularSwitch.Test.MonadAB<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadAB.Return(global::FunicularSwitch.Test.MonadC<A>.Return(a)));

        public static global::FunicularSwitch.Test.MonadABC<B> Map<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadABC.Return(fn(a)));

        public static global::FunicularSwitch.Test.MonadABC<B> Select<A, B>(this global::FunicularSwitch.Test.MonadABC<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadABC.Return(fn(a)));
    }
}
