namespace FunicularSwitch.Playground
{
    internal static partial class Scoped
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Playground.Scoped<B> SelectMany<A, B>(this global::FunicularSwitch.Playground.Scoped<A> ma, global::System.Func<A, global::FunicularSwitch.Playground.Scoped<B>> fn) => global::FunicularSwitch.Playground.Scoped.Bind(((global::FunicularSwitch.Playground.Scoped<A>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, global::FunicularSwitch.Playground.Scoped<B>> fn) => global::FunicularSwitch.Playground.Scoped.Bind(((global::FunicularSwitch.Playground.Scoped<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, global::FunicularSwitch.Playground.Scoped<B>> fn) => global::FunicularSwitch.Playground.Scoped.Bind(((global::FunicularSwitch.Playground.Scoped<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Playground.Scoped<C> SelectMany<A, B, C>(this global::FunicularSwitch.Playground.Scoped<A> ma, global::System.Func<A, global::FunicularSwitch.Playground.Scoped<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Playground.Scoped<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, global::FunicularSwitch.Playground.Scoped<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Playground.Scoped<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, global::FunicularSwitch.Playground.Scoped<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Playground.Scoped<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Playground.Scoped<B> Map<A, B>(this global::FunicularSwitch.Playground.Scoped<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Playground.Scoped.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Playground.Scoped.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<B>> Map<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Playground.Scoped.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Playground.Scoped<B> Select<A, B>(this global::FunicularSwitch.Playground.Scoped<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Playground.Scoped.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Playground.Scoped.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<B>> Select<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Playground.Scoped<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Playground.Scoped.Return(fn(a)));
    }
}
