namespace FunicularSwitch.Generators.Consumer
{
    public partial record OptionResult<A>(global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>> M) : global::FunicularSwitch.Transformers.Monad<A>
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator OptionResult<A>(global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>(OptionResult<A> ma) => ma.M;
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Return<A_>(A_ a) => OptionResult.SomeOk(a);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<A_>> fn) => this.Bind(a => (OptionResult<A_>)fn(a));
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        A_ global::FunicularSwitch.Transformers.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class OptionResult
    {
        private readonly record struct Impl__FunicularSwitch_Option<A>(global::FunicularSwitch.Option<A> M) : global::FunicularSwitch.Transformers.Monad<A>
        {
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator Impl__FunicularSwitch_Option<A>(global::FunicularSwitch.Option<A> ma) => new(ma);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator global::FunicularSwitch.Option<A>(Impl__FunicularSwitch_Option<A> ma) => ma.M;
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Transformers.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Option<B>)global::FunicularSwitch.Option<B>.Some(a);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Transformers.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<B>> fn) => (Impl__FunicularSwitch_Option<B>)M.Bind(a => (global::FunicularSwitch.Option<B>)(Impl__FunicularSwitch_Option<B>)fn(a));
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public B Cast<B>() => (B)(object)M;
        }

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<A> SomeOk<A>(A a) => global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>.Some(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.BindT<A, B>((Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)((global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)ma), a => (Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.BindT<A, B>((Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)((global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)ma), a => (Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.BindT<A, B>((Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)((global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)ma), a => (Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.BindT<A, B>((Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)((global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)ma), a => (Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.OptionResult<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.OptionResult<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<A> Lift<A>(global::FunicularSwitch.Option<A> ma) => ma.Bind(a => global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>.Some(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => OptionResult.SomeOk(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => OptionResult.SomeOk(fn(a)));
    }
}
