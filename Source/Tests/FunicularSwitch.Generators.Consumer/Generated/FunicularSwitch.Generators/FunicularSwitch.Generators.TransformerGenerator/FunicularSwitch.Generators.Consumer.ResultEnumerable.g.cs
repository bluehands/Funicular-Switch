namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct ResultEnumerable<A>(global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>> M) : global::FunicularSwitch.Generators.Monad<A>
    {
        public static implicit operator ResultEnumerable<A>(global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>> ma) => new(ma);
        public static implicit operator global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>(ResultEnumerable<A> ma) => ma.M;
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Return<A_>(A_ a) => ResultEnumerable.OkYield(a);
        global::FunicularSwitch.Generators.Monad<A_> global::FunicularSwitch.Generators.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Generators.Monad<A_>> fn) => this.Bind(a => (ResultEnumerable<A_>)fn(a));
    }

    public static partial class ResultEnumerable
    {
        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> OkYield<A>(A a) => global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>.Ok(global::FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a));

        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Bind<A, B>(this global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.ResultEnumerable<B>> fn) => global::FunicularSwitch.Generators.Consumer.EnumerableT.Bind<A, B, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>, global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>>(ma, x => fn(x), global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<B>>.Ok, (ma, fn) => ma.Bind(fn));

        public static global::FunicularSwitch.Generators.Consumer.ResultEnumerable<A> Lift<A>(global::FunicularSwitch.Generators.Consumer.Result<A> ma) => ma.Bind(a => global::FunicularSwitch.Generators.Consumer.Result<global::System.Collections.Generic.IEnumerable<A>>.Ok(global::FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a)));
    }
}
