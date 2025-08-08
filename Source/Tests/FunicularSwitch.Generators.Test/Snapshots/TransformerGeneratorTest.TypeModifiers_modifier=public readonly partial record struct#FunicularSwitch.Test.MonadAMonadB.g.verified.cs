//HintName: FunicularSwitch.Test.MonadAMonadB.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadAMonadB<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> M)
    {
        public static implicit operator MonadAMonadB<A>(FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>(MonadAMonadB<A> ma) => ma.M;
    }
    
    public static partial class MonadAMonadB
    {
        public static FunicularSwitch.Test.MonadAMonadB<A> Return<A>(A a) => FunicularSwitch.Test.MonadA.Return(FunicularSwitch.Test.MonadB.Return(a));
        
        public static FunicularSwitch.Test.MonadAMonadB<B> Bind<A, B>(this FunicularSwitch.Test.MonadAMonadB<A> ma, global::System.Func<A, FunicularSwitch.Test.MonadAMonadB<B>> fn) => FunicularSwitch.Test.MonadBT.Bind<A, B, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<A>>, FunicularSwitch.Test.MonadA<FunicularSwitch.Test.MonadB<B>>>(ma, x => fn(x), FunicularSwitch.Test.MonadA.Return, FunicularSwitch.Test.MonadA.Bind);
        
        public static FunicularSwitch.Test.MonadAMonadB<A> Lift<A>(FunicularSwitch.Test.MonadA<A> ma) => FunicularSwitch.Test.MonadA.Bind(ma, a => FunicularSwitch.Test.MonadA.Return(FunicularSwitch.Test.MonadB.Return(a)));
    }
}
