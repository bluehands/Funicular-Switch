namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct ResultEnumerable<A>(global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator ResultEnumerable<A>(global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>(ResultEnumerable<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => ResultEnumerable.OkYield(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (ResultEnumerable<A_>)fn(a));
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class ResultEnumerable
    {
        private readonly record struct Impl__FunicularSwitch_Generators_Consumer_Result<A>(global::FunicularSwitch.Generators.Consumer.Result<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            public static implicit operator Impl__FunicularSwitch_Generators_Consumer_Result<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => new(ma);
            public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<A>(Impl__FunicularSwitch_Generators_Consumer_Result<A> ma) => ma.M;
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Generators_Consumer_Result<B>)global::FunicularSwitch.Generators.Consumer.Result<B>.Ok(a);
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Generators_Consumer_Result<B>)M.Bind(a => (global::FunicularSwitch.Generators.Consumer.Result<B>)(Impl__FunicularSwitch_Generators_Consumer_Result<B>)fn(a));
            public B Cast<B>() => (B)(object)M;
        }

        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> OkYield<A>(A a) => global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>.Ok(global::FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a));

        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>> fn) => global::FunicularSwitch.Generators.Consumer.EnumerableT.Bind<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>();

        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => ma.Bind(a => global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>.Ok(global::FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a)));
    }
}
