namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct WriterResult<A>(global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator WriterResult<A>(global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>(WriterResult<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => WriterResult.InitOk(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (WriterResult<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class WriterResult
    {
        private readonly record struct Impl__FunicularSwitch_Generators_Consumer_Writer<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Generators_Consumer_Writer<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Generators.Consumer.Writer<A>(Impl__FunicularSwitch_Generators_Consumer_Writer<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Generators_Consumer_Writer<B>)global::FunicularSwitch.Generators.Consumer.Writer<B>.Init(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Generators_Consumer_Writer<B>)M.Bind(a => (global::FunicularSwitch.Generators.Consumer.Writer<B>)(Impl__FunicularSwitch_Generators_Consumer_Writer<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Generators.Consumer.WriterResult<A> InitOk<A>(A a) => global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>.Init(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a));

        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<B>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.Bind<A, B>((Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>)((global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Writer<global::FunicularSwitch.Generators.Consumer.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.WriterResult<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> ma) => ma.Bind(a => global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>.Init(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a)));
    }
}
