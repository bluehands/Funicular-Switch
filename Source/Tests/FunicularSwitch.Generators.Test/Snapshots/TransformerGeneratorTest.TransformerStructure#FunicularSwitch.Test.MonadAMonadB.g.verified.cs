//HintName: FunicularSwitch.Test.MonadAMonadB.g.cs
namespace FunicularSwitch.Test
{
    public partial record MonadAMonadB<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> M)
    {
        public static implicit operator MonadAMonadB<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>(MonadAMonadB<A> ma) => ma.M;
    }
    
    public static partial class MonadAMonadB
    {
        public static MonadAMonadB<A> Return<A>(A a) => FunicularSwitch.Test.MonadA.Return(FunicularSwitch.Test.MonadB.Return(a));
        
        public static MonadAMonadB<B> Bind<A, B>(this MonadAMonadB<A> ma, System.Func<A, MonadAMonadB<B>> fn) => FunicularSwitch.Test.MonadBT.Bind<A, B, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<B>>>(ma, x => fn(x), FunicularSwitch.Test.MonadA.Return, FunicularSwitch.Test.MonadA.Bind);
        
        public static MonadAMonadB<A> Lift<A>(FunicularSwitch.Test.MonadA<A> ma) => FunicularSwitch.Test.MonadA.Bind(ma, a => FunicularSwitch.Test.MonadA.Return(FunicularSwitch.Test.MonadB.Return(a)));
    }
}
