namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct WriterResult<A>(FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<A>> M)
    {
        public static implicit operator WriterResult<A>(FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<A>>(WriterResult<A> ma) => ma.M;
    }

    public static partial class WriterResult
    {
        public static FunicularSwitch.Generators.Consumer.WriterResult<A> InitOk<A>(A a) => FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<A>>.Init(FunicularSwitch.Generators.Consumer.Result<A>.Ok(a));

        public static FunicularSwitch.Generators.Consumer.WriterResult<B> Bind<A, B>(this FunicularSwitch.Generators.Consumer.WriterResult<A> ma, global::System.Func<A, FunicularSwitch.Generators.Consumer.WriterResult<B>> fn) => FunicularSwitch.Generators.Consumer.ResultT.Bind<A, B, FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<A>>, FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<B>>>(ma, x => fn(x), FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<B>>.Init, (ma, fn) => ma.Bind(fn));

        public static FunicularSwitch.Generators.Consumer.WriterResult<A> Lift<A>(FunicularSwitch.Generators.Consumer.Writer<A> ma) => ma.Bind(a => FunicularSwitch.Generators.Consumer.Writer<FunicularSwitch.Generators.Consumer.Result<A>>.Init(FunicularSwitch.Generators.Consumer.Result<A>.Ok(a)));
    }
}
