namespace FunicularSwitch.Generators.Consumer
{
    public static partial class Writer
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> Init<A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<A>> Init<A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(((global::FunicularSwitch.Generators.Consumer.Writer<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<B>> Bind<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(((global::FunicularSwitch.Generators.Consumer.Writer<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<B> SelectMany<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(((global::FunicularSwitch.Generators.Consumer.Writer<A>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(((global::FunicularSwitch.Generators.Consumer.Writer<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn) => global::FunicularSwitch.Generators.Consumer.Writer.Bind(((global::FunicularSwitch.Generators.Consumer.Writer<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<C> SelectMany<A, B, C>(this global::FunicularSwitch.Generators.Consumer.Writer<A> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Writer<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Writer<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, global::FunicularSwitch.Generators.Consumer.Writer<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Generators.Consumer.Writer<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<B> Map<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<B>> Map<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Generators.Consumer.Writer<B> Select<A, B>(this global::FunicularSwitch.Generators.Consumer.Writer<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<B>> Select<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Generators.Consumer.Writer<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Generators.Consumer.Writer.Init<B>(fn(a)));
    }
}
