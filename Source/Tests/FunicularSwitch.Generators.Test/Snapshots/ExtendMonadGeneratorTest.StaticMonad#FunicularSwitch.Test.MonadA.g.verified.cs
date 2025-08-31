//HintName: FunicularSwitch.Test.MonadA.g.cs
namespace FunicularSwitch.Test
{
    public static partial class MonadA
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> Return<A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Test.MonadA.Return<A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> Return<A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Test.MonadA.Return<A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> Bind<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<A>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<B> Map<A, B>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> Map<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<B> Select<A, B>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> Select<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<B>(fn(a)));
    }
}
