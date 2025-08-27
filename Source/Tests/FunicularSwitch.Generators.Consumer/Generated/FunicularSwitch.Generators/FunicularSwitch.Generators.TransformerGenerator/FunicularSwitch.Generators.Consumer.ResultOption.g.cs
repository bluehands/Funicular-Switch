namespace FunicularSwitch.Generators.Consumer
{
    public partial record ResultOption<A>(global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator ResultOption<A>(global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>(ResultOption<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => ResultOption.Return(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (ResultOption<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class ResultOption
    {
        private readonly record struct Impl__FunicularSwitch_Generators_Consumer_Result<A>(global::FunicularSwitch.Generators.Consumer.Result<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Generators_Consumer_Result<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<A>(Impl__FunicularSwitch_Generators_Consumer_Result<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Generators_Consumer_Result<B>)global::FunicularSwitch.Generators.Consumer.ResultM.Return(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Generators_Consumer_Result<B>)global::FunicularSwitch.Generators.Consumer.ResultM.Bind(M, a => (global::FunicularSwitch.Generators.Consumer.Result<B>)(Impl__FunicularSwitch_Generators_Consumer_Result<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<A> Return<A>(A a) => global::FunicularSwitch.Generators.Consumer.ResultM.Return(global::FunicularSwitch.Generators.Consumer.OptionM.Return(a));

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultOption<B>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultOption<B>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultOption<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.ResultOption<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::FunicularSwitch.Option<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.ResultOption<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => global::FunicularSwitch.Generators.Consumer.ResultM.Bind(ma, a => global::FunicularSwitch.Generators.Consumer.ResultM.Return(global::FunicularSwitch.Generators.Consumer.OptionM.Return(a)));

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => ResultOption.Return(fn(a)));

        public static global::FunicularSwitch.Generators.Consumer.ResultOption<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultOption<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => ResultOption.Return(fn(a)));
    }
}
