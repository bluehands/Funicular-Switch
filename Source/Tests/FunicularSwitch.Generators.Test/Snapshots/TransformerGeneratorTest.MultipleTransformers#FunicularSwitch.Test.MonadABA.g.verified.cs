//HintName: FunicularSwitch.Test.MonadABA.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadABA<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator MonadABA<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>> ma) => new(ma);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>(MonadABA<A> ma) => ma.M;
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadABA.Return(a);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (MonadABA<A_>)fn(a));
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class MonadABA
    {
        private readonly record struct Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> ma) => new(ma);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator global::FunicularSwitch.Test.MonadA<A>(Impl__FunicularSwitch_Test_MonadA<A> ma) => ma.M;
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadA<B>)global::FunicularSwitch.Test.MonadA<B>.Return(a);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadA<B>)M.Bind(a => (global::FunicularSwitch.Test.MonadA<B>)(Impl__FunicularSwitch_Test_MonadA<B>)fn(a));
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public B Cast<B>() => (B)(object)M;
        }

        private readonly record struct Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<A> ma) => ma.M;
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<B>)global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>.Return(global::FunicularSwitch.Test.MonadB<B>.Return(a));
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<B>)global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)M, a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => (global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>)(Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<B>)fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public B Cast<B>() => (B)(object)M;
        }

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>.Return(global::FunicularSwitch.Test.MonadA<A>.Return(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABA<B>> fn) => global::FunicularSwitch.Test.MonadAT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>)ma), a => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>> fn) => global::FunicularSwitch.Test.MonadAT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>)ma), a => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABA<B>> fn) => global::FunicularSwitch.Test.MonadAT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>)ma), a => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>> fn) => global::FunicularSwitch.Test.MonadAT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>)ma), a => (Impl__FunicularSwitch_Test_MonadA_global__FunicularSwitch_Test_MonadB<global::FunicularSwitch.Test.MonadA<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadABA<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadABA<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<B>>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadABA<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>>.Return(global::FunicularSwitch.Test.MonadB<global::FunicularSwitch.Test.MonadA<A>>.Return(global::FunicularSwitch.Test.MonadA<A>.Return(a))));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<B> Map<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadABA.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadABA<B> Select<A, B>(this global::FunicularSwitch.Test.MonadABA<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadABA.Return(fn(a)));
    }
}
