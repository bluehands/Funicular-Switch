//HintName: FunicularSwitch.Test.MonadAMonadB.g.cs
namespace FunicularSwitch.Test
{
    internal partial class MonadAMonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M) : global::FunicularSwitch.Transformers.Monad<A>
    {
        public global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M { get; } = M;
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator MonadAMonadB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(MonadAMonadB<A> ma) => ma.M;
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Return<A_>(A_ a) => MonadAMonadB.Return(a);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<A_>> fn) => this.Bind(a => (MonadAMonadB<A_>)fn(a));
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        A_ global::FunicularSwitch.Transformers.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    internal static partial class MonadAMonadB
    {
        private readonly record struct Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> M) : global::FunicularSwitch.Transformers.Monad<A>
        {
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator Impl__FunicularSwitch_Test_MonadA<A>(global::FunicularSwitch.Test.MonadA<A> ma) => new(ma);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator global::FunicularSwitch.Test.MonadA<A>(Impl__FunicularSwitch_Test_MonadA<A> ma) => ma.M;
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Transformers.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Test_MonadA<B>)global::FunicularSwitch.Test.MonadA.Return(a);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Transformers.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<B>> fn) => (Impl__FunicularSwitch_Test_MonadA<B>)global::FunicularSwitch.Test.MonadA.Bind(M, a => (global::FunicularSwitch.Test.MonadA<B>)(Impl__FunicularSwitch_Test_MonadA<B>)fn(a));
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public B Cast<B>() => (B)(object)M;
        }

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadA.Return(global::FunicularSwitch.Test.MonadB.Return(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> Return<A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Test.MonadA.Return(global::FunicularSwitch.Test.MonadB.Return((await a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)(await ma)), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)(await ma)), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)(await ma)), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)(await ma)), a => (Impl__FunicularSwitch_Test_MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadAMonadB<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadAMonadB<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAMonadB<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany(a => ((global::FunicularSwitch.Test.MonadAMonadB<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany(a => ((global::FunicularSwitch.Test.MonadAMonadB<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => global::FunicularSwitch.Test.MonadA.Bind(ma, a => global::FunicularSwitch.Test.MonadA.Return(global::FunicularSwitch.Test.MonadB.Return(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> Lift<A>(global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma) => global::FunicularSwitch.Test.MonadA.Bind((await ma), a => global::FunicularSwitch.Test.MonadA.Return(global::FunicularSwitch.Test.MonadB.Return(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<B> Map<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadAMonadB.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind(a => MonadAMonadB.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadAMonadB<B> Select<A, B>(this global::FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadAMonadB.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadAMonadB<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind(a => MonadAMonadB.Return(fn(a)));
    }
}
