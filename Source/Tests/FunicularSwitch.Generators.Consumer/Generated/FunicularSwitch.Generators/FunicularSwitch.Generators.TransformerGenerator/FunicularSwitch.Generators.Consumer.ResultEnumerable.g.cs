namespace FunicularSwitch.Generators.Consumer
{
    public readonly partial record struct ResultEnumerable<A>(FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<A>> M)
    {
        public static implicit operator ResultEnumerable<A>(FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<A>> ma) => new(ma);
        public static implicit operator FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<A>>(ResultEnumerable<A> ma) => ma.M;
    }

    public static partial class ResultEnumerable
    {
        public static FunicularSwitch.Generators.Consumer.ResultEnumerable<A> OkYield<A>(A a) => FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<A>>.Ok(FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a));

        public static FunicularSwitch.Generators.Consumer.ResultEnumerable<B> Bind<A, B>(this FunicularSwitch.Generators.Consumer.ResultEnumerable<A> ma, global::System.Func<A, FunicularSwitch.Generators.Consumer.ResultEnumerable<B>> fn) => FunicularSwitch.Generators.Consumer.EnumerableT.Bind<A, B, FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<A>>, FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<B>>>(ma, x => fn(x), FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<B>>.Ok, (ma, fn) => ma.Bind(fn));

        public static FunicularSwitch.Generators.Consumer.ResultEnumerable<A> Lift<A>(FunicularSwitch.Generators.Consumer.Result<A> ma) => ma.Bind(a => FunicularSwitch.Generators.Consumer.Result<System.Collections.Generic.IEnumerable<A>>.Ok(FunicularSwitch.Generators.Consumer.EnumerableM.Yield(a)));
    }
}
