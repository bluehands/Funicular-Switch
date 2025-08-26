namespace FunicularSwitch.Generators.Consumer
{
    public partial record OptionResult<A>(global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator OptionResult<A>(global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>(OptionResult<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => OptionResult.Ok(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (OptionResult<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class OptionResult
    {
        private readonly record struct Impl__FunicularSwitch_Option<A>(global::FunicularSwitch.Option<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Option<A>(global::FunicularSwitch.Option<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Option<A>(Impl__FunicularSwitch_Option<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Option<B>)global::FunicularSwitch.Generators.Consumer.OptionM.Return(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Option<B>)global::FunicularSwitch.Generators.Consumer.OptionM.Bind(M, a => (global::FunicularSwitch.Option<B>)(Impl__FunicularSwitch_Option<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<A> Ok<A>(A a) => global::FunicularSwitch.Generators.Consumer.OptionM.Return(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a));

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn) => global::FunicularSwitch.Generators.Consumer.ResultT.Bind<A, B>((Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)((global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<A>>)ma), a => (Impl__FunicularSwitch_Option<global::FunicularSwitch.Generators.Consumer.Result<B>>)(new global::System.Func<A, global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Option<global::FunicularSwitch.Generators.Consumer.Result<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<A> Lift<A>(global::FunicularSwitch.Option<A> ma) => global::FunicularSwitch.Generators.Consumer.OptionM.Bind(ma, a => global::FunicularSwitch.Generators.Consumer.OptionM.Return(global::FunicularSwitch.Generators.Consumer.Result<A>.Ok(a)));

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => OptionResult.Ok(fn(a)));

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => OptionResult.Ok(fn(a)));

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn) => ma.Bind(fn);

        public static global::FunicularSwitch.Generators.Consumer.OptionResult<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.OptionResult<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.OptionResult<B>> fn, global::System.Func<A, B, C> selector) => ma.Bind(a => fn(a).Map(b => selector(a, b)));
    }
}
