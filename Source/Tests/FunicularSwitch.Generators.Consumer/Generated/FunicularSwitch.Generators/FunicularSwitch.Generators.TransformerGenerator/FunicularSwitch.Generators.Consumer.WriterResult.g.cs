namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct WriterResult<A>(global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator WriterResult<A>(global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>(WriterResult<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => WriterResult.InitOk(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (WriterResult<A_>)fn(a));
    }

    public static partial class WriterResult
    {
        public static global::FunicularSwitch.Generators.Consumer.WriterResult<A> InitOk<A>(A a) => global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>.Init(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a));

        public static global::FunicularSwitch.Generators.Consumer.WriterResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<B>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.Bind<A, B, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>, global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<B>>>(ma, x => fn(x), global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<B>>.Init, (ma, fn) => ma.Bind(fn));

        public static global::FunicularSwitch.Generators.Consumer.WriterResult<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Writer<A> ma) => ma.Bind(a => global::FunicularSwitch.Generators.Consumer.Writer<global::FunicularSwitch.Generators.Consumer.Result<A>>.Init(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a)));
    }
}
