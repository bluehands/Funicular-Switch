namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct WriterResult<A>(global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> M) : global::FunicularSwitch.Transformers.Monad<A>
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator WriterResult<A>(global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> ma) => new(ma);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>(WriterResult<A> ma) => ma.M;
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Return<A_>(A_ a) => WriterResult.InitOk(a);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<A_>> fn) => this.Bind(a => (WriterResult<A_>)fn(a));
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        A_ global::FunicularSwitch.Transformers.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class WriterResult
    {
        private readonly record struct Impl__FunicularSwitch_Generators_Consumer_Writer<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> M) : global::FunicularSwitch.Transformers.Monad<A>
        {
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator Impl__FunicularSwitch_Generators_Consumer_Writer<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> ma) => new(ma);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator global::FunicularSwitch.Generators.Consumer.Writer<A>(Impl__FunicularSwitch_Generators_Consumer_Writer<A> ma) => ma.M;
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Transformers.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Generators_Consumer_Writer<B>)global::FunicularSwitch.Generators.Consumer.Writer.Init(a);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Transformers.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<B>> fn) => (Impl__FunicularSwitch_Generators_Consumer_Writer<B>)global::FunicularSwitch.Generators.Consumer.Writer.Bind(M, a => (global::FunicularSwitch.Generators.Consumer.Writer<B>)(Impl__FunicularSwitch_Generators_Consumer_Writer<B>)fn(a));
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public B Cast<B>() => (B)(object)M;
        }

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<A> InitOk<A>(A a) => global::FunicularSwitch.Generators.Consumer.Writer.Init(global::FunicularSwitch.Result<A>.Ok(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<B>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<B>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.WriterResult<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.WriterResult<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> ma) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(ma, a => global::FunicularSwitch.Generators.Consumer.Writer.Init(global::FunicularSwitch.Result<A>.Ok(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => WriterResult.InitOk(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => WriterResult.InitOk(fn(a)));
    }
}
