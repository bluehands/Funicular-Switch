namespace FunicularSwitch.Generators.Consumer
{
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
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> InitOk<A>(A a) => global::FunicularSwitch.Generators.Consumer.Writer.Init(global::FunicularSwitch.Result<A>.Ok(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> InitOk<A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Generators.Consumer.Writer.Init(global::FunicularSwitch.Result<A>.Ok((await a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn) => global::FunicularSwitch.Transformers.ResultT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<C>> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<C>>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> Lift<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> ma) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(ma, [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init(global::FunicularSwitch.Result<A>.Ok(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> Lift<A>(global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma) => global::FunicularSwitch.Generators.Consumer.Writer.Bind((await ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init(global::FunicularSwitch.Result<A>.Ok(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => WriterResult.InitOk(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> Map<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => WriterResult.InitOk(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => WriterResult.InitOk(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<B>>> Select<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Result<A>>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => WriterResult.InitOk(fn(a)));
    }
}
