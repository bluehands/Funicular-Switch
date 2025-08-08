namespace FunicularSwitch.Generators.Consumer
{
    public partial record ResultOption<A>(FunicularSwitch.Generators.Consumer.Result<FunicularSwitch.Option<A>> M)
    {
        public static implicit operator ResultOption<A>(FunicularSwitch.Generators.Consumer.Result<FunicularSwitch.Option<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Generators.Consumer.Result<FunicularSwitch.Option<A>>(ResultOption<A> ma) => ma.M;
    }
    
    public static partial class ResultOption
    {
        public static FunicularSwitch.Generators.Consumer.ResultOption<A> Return<A>(A a) => FunicularSwitch.Generators.Consumer.ResultM.Return(FunicularSwitch.Generators.Consumer.OptionM.Return(a));
        
        public static FunicularSwitch.Generators.Consumer.ResultOption<B> Bind<A, B>(this FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, FunicularSwitch.Generators.Consumer.ResultOption<B>> fn) => FunicularSwitch.Generators.Consumer.OptionT.Bind<A, B, FunicularSwitch.Generators.Consumer.Result<FunicularSwitch.Option<A>>, FunicularSwitch.Generators.Consumer.Result<FunicularSwitch.Option<B>>>(ma, x => fn(x), FunicularSwitch.Generators.Consumer.ResultM.Return, FunicularSwitch.Generators.Consumer.ResultM.Bind);
        
        public static FunicularSwitch.Generators.Consumer.ResultOption<A> Lift<A>(FunicularSwitch.Generators.Consumer.Result<A> ma) => FunicularSwitch.Generators.Consumer.ResultM.Bind(ma, a => FunicularSwitch.Generators.Consumer.ResultM.Return(FunicularSwitch.Generators.Consumer.OptionM.Return(a)));
    }
}