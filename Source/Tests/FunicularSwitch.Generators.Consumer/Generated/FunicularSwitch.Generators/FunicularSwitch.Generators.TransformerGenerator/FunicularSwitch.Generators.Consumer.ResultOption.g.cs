namespace FunicularSwitch.Generators.Consumer
{
    public partial record ResultOption<A>(global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator ResultOption<A>(global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>(ResultOption<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => ResultOption.Return(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (ResultOption<A_>)fn(a));
    }

    public static partial class ResultOption
    {
        public static global::FunicularSwitch.Generators.Consumer.ResultOption<A> Return<A>(A a) => global::FunicularSwitch.Generators.Consumer.ResultM.Return(global::FunicularSwitch.Generators.Consumer.OptionM.Return(a));

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultOption<B>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.Bind<A, B, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>(ma, x => fn(x), global::FunicularSwitch.Generators.Consumer.ResultM.Return, global::FunicularSwitch.Generators.Consumer.ResultM.Bind);

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => global::FunicularSwitch.Generators.Consumer.ResultM.Bind(ma, a => global::FunicularSwitch.Generators.Consumer.ResultM.Return(global::FunicularSwitch.Generators.Consumer.OptionM.Return(a)));
    }
}
