namespace FunicularSwitch.Generators.Consumer
{
    public static partial class Either
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> Right<T0, A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> Right<T0, A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> Bind<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn) => global::FunicularSwitch.Generators.Consumer.Either.Bind(((global::FunicularSwitch.Generators.Consumer.Either<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> Bind<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn) => global::FunicularSwitch.Generators.Consumer.Either.Bind(((global::FunicularSwitch.Generators.Consumer.Either<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Either<T0, B> SelectMany<T0, A, B>(this global::FunicularSwitch.Generators.Consumer.Either<T0, A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn) => global::FunicularSwitch.Generators.Consumer.Either.Bind(((global::FunicularSwitch.Generators.Consumer.Either<T0, A>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> SelectMany<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn) => global::FunicularSwitch.Generators.Consumer.Either.Bind(((global::FunicularSwitch.Generators.Consumer.Either<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> SelectMany<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn) => global::FunicularSwitch.Generators.Consumer.Either.Bind(((global::FunicularSwitch.Generators.Consumer.Either<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Either<T0, C> SelectMany<T0, A, B, C>(this global::FunicularSwitch.Generators.Consumer.Either<T0, A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Either<T0, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, C>> SelectMany<T0, A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Either<T0, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, C>> SelectMany<T0, A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Either<T0, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Either<T0, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Either<T0, B> Map<T0, A, B>(this global::FunicularSwitch.Generators.Consumer.Either<T0, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> Map<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> Map<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Either<T0, B> Select<T0, A, B>(this global::FunicularSwitch.Generators.Consumer.Either<T0, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> Select<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, B>> Select<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Either<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Either.Right<T0, B>(fn(a)));
    }
}
