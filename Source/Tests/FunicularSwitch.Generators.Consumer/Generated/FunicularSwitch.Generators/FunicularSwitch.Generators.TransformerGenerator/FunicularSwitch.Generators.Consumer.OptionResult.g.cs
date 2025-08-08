namespace FunicularSwitch.Generators.Consumer
{
    public partial record OptionResult<A>(FunicularSwitch.Option<FunicularSwitch.Generators.Consumer.Result<A>> M)
    {
        public static implicit operator OptionResult<A>(FunicularSwitch.Option<FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Option<FunicularSwitch.Generators.Consumer.Result<A>>(OptionResult<A> ma) => ma.M;
    }
    
    public static partial class OptionResult
    {
        public static OptionResult<A> Return<A>(A a) => FunicularSwitch.Generators.Consumer.OptionM.Return(FunicularSwitch.Generators.Consumer.ResultM.Return(a));
        
        public static OptionResult<B> Bind<A, B>(this OptionResult<A> ma, global::System.Func<A, OptionResult<B>> fn) => FunicularSwitch.Generators.Consumer.ResultT.Bind<A, B, FunicularSwitch.Option<FunicularSwitch.Generators.Consumer.Result<A>>, FunicularSwitch.Option<FunicularSwitch.Generators.Consumer.Result<B>>>(ma, x => fn(x), FunicularSwitch.Generators.Consumer.OptionM.Return, FunicularSwitch.Generators.Consumer.OptionM.Bind);
        
        public static OptionResult<A> Lift<A>(FunicularSwitch.Option<A> ma) => FunicularSwitch.Generators.Consumer.OptionM.Bind(ma, a => FunicularSwitch.Generators.Consumer.OptionM.Return(FunicularSwitch.Generators.Consumer.ResultM.Return(a)));
    }
}