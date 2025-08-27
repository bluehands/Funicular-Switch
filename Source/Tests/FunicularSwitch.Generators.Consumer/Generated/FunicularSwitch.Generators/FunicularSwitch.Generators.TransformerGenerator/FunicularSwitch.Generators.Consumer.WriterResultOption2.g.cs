namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct WriterResultOption2<A>(global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator WriterResultOption2<A>(global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>>(WriterResultOption2<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => WriterResultOption2.InitOk(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (WriterResultOption2<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class WriterResultOption2
    {
        private readonly record struct Impl__FunicularSwitch_Generators_Consumer_WriterResult<A>(global::FunicularSwitch.Generators.Consumer.WriterResult<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Generators_Consumer_WriterResult<A>(global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Generators.Consumer.WriterResult<A>(Impl__FunicularSwitch_Generators_Consumer_WriterResult<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Generators_Consumer_WriterResult<B>)global::FunicularSwitch.Generators.Consumer.WriterResult.InitOk(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Generators_Consumer_WriterResult<B>)M.Bind(a => (global::FunicularSwitch.Generators.Consumer.WriterResult<B>)(Impl__FunicularSwitch_Generators_Consumer_WriterResult<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> InitOk<A>(A a) => global::FunicularSwitch.Generators.Consumer.WriterResult.InitOk(global::FunicularSwitch.Generators.Consumer.OptionM.Return(a));

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.Bind<A, B>((Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.Bind<A, B>((Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.Bind<A, B>((Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>> fn) => global::FunicularSwitch.Generators.Consumer.OptionT.Bind<A, B>((Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<A>>)((global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_WriterResult<global::FunicularSwitch.Option<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.WriterResult<global::FunicularSwitch.Option<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B>)fn(a)).Map(b => selector(a, b)));

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.WriterResult<A> ma) => ma.Bind(a => global::FunicularSwitch.Generators.Consumer.WriterResult.InitOk(global::FunicularSwitch.Generators.Consumer.OptionM.Return(a)));

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => WriterResultOption2.InitOk(fn(a)));

        public static global::FunicularSwitch.Generators.Consumer.WriterResultOption2<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.WriterResultOption2<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => WriterResultOption2.InitOk(fn(a)));
    }
}
