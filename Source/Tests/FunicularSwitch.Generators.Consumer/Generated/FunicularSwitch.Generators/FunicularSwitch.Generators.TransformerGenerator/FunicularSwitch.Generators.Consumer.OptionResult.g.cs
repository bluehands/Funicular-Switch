namespace FunicularSwitch.Generators.Consumer
{
    public partial record OptionResult<A>(global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator OptionResult<A>(global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>(OptionResult<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => OptionResult.Ok(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (OptionResult<A_>)fn(a));
    }

    public static partial class OptionResult
    {
        public static global::FunicularSwitch.Generators.Consumer.OptionResult<A> Ok<A>(A a) => global::FunicularSwitch.Generators.Consumer.OptionM.Return(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a));

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.Bind<A, B, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>(ma, x => fn(x), global::FunicularSwitch.Generators.Consumer.OptionM.Return, global::FunicularSwitch.Generators.Consumer.OptionM.Bind);

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<A> Lift<A>(global::FunicularSwitch.Option<A> ma) => global::FunicularSwitch.Generators.Consumer.OptionM.Bind(ma, a => global::FunicularSwitch.Generators.Consumer.OptionM.Return(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a)));
    }
}
