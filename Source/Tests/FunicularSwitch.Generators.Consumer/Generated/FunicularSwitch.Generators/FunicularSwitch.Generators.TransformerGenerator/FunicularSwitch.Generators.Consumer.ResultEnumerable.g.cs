namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct ResultEnumerable<A>(global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator ResultEnumerable<A>(global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>> ma) => new(ma);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>(ResultEnumerable<A> ma) => ma.M;
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => ResultEnumerable.OkYield(a);
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (ResultEnumerable<A_>)fn(a));
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        A_ global::FunicularSwitch.Generators.Monad<A>.Cast<A_>() => (A_)(object)M;
    }

    public static partial class ResultEnumerable
    {
        private readonly record struct Impl__FunicularSwitch_Generators_Consumer_Result<A>(global::FunicularSwitch.Generators.Consumer.Result<A> M) : global::FunicularSwitch.Generators.Monad<A>
        {
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator Impl__FunicularSwitch_Generators_Consumer_Result<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => new(ma);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<A>(Impl__FunicularSwitch_Generators_Consumer_Result<A> ma) => ma.M;
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Generators.Monad<B> Return<B>(B a) => (Impl__FunicularSwitch_Generators_Consumer_Result<B>)global::FunicularSwitch.Generators.Consumer.Result<B>.Ok(a);
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public global::FunicularSwitch.Generators.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<B>> fn) => (Impl__FunicularSwitch_Generators_Consumer_Result<B>)M.Bind(a => (global::FunicularSwitch.Generators.Consumer.Result<B>)(Impl__FunicularSwitch_Generators_Consumer_Result<B>)fn(a));
            [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
            public B Cast<B>() => (B)(object)M;
        }

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> OkYield<A>(A a) => global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>.Ok(global::FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>> fn) => global::FunicularSwitch.Generators.Consumer.EnumerableT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>> fn) => global::FunicularSwitch.Generators.Consumer.EnumerableT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>> fn) => global::FunicularSwitch.Generators.Consumer.EnumerableT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>> fn) => global::FunicularSwitch.Generators.Consumer.EnumerableT.BindT<A, B>((Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<A>>)((global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>)ma), a => (Impl__FunicularSwitch_Generators_Consumer_Result<global::System.Collections.Generic.IEnumerable<B>>)(new global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>(a => fn(a)).Invoke(a))).Cast<global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>();

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany(a => ((global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>)fn(a)).Map(b => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => ma.Bind(a => global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>.Ok(global::FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => ResultEnumerable.OkYield(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, B> fn) => ma.Bind(a => ResultEnumerable.OkYield(fn(a)));
    }
}
