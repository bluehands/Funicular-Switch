//HintName: FunicularSwitch.Test.MonadAB.g.cs
namespace FunicularSwitch.Test
{
    public readonly partial record struct MonadAB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator MonadAB<A>(global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>(MonadAB<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => MonadAB.Return(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (MonadAB<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class MonadAB
    {
        public static global::FunicularSwitch.Test.MonadAB<A> Return<A>(A a) => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>.Return(global::FunicularSwitch.Test.MonadB<A>.Return(a));

        public static global::FunicularSwitch.Test.MonadAB<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<B>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        public static global::FunicularSwitch.Test.MonadAB<B> Bind<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        public static global::FunicularSwitch.Test.MonadAB<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<B>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        public static global::FunicularSwitch.Test.MonadAB<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn) => global::FunicularSwitch.Test.MonadBT.BindT<A, B>((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)((global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>)ma), a => (global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>)(new global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>>();

        public static global::FunicularSwitch.Test.MonadAB<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadAB<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadAB<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Test.MonadAB<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Test.MonadAB<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Test.MonadAB<A> Lift<A>(global::FunicularSwitch.Test.MonadA<A> ma) => ma.Bind(a => global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadB<A>>.Return(global::FunicularSwitch.Test.MonadB<A>.Return(a)));

        public static global::FunicularSwitch.Test.MonadAB<B> Map<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadAB.Return(fn(a)));

        public static global::FunicularSwitch.Test.MonadAB<B> Select<A, B>(this global::FunicularSwitch.Test.MonadAB<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => MonadAB.Return(fn(a)));
    }
}
