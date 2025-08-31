//HintName: FunicularSwitch.Test.MonadA.g.cs
namespace FunicularSwitch.Test
{
    public static partial class MonadA
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> Return<T0, A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Test.MonadA.Return<T0, A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> Return<T0, A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Test.MonadA.Return<T0, A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, B>> Bind<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, B>> Bind<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, B> SelectMany<T0, A, B>(this global::FunicularSwitch.Test.MonadA<T0, A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, A>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, B>> SelectMany<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, B>> SelectMany<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, C> SelectMany<T0, A, B, C>(this global::FunicularSwitch.Test.MonadA<T0, A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, C>> SelectMany<T0, A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, C>> SelectMany<T0, A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, B> Map<T0, A, B>(this global::FunicularSwitch.Test.MonadA<T0, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, B>> Map<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, B>> Map<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, B> Select<T0, A, B>(this global::FunicularSwitch.Test.MonadA<T0, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, B>> Select<T0, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, B>> Select<T0, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, B>(fn(a)));
    }
}
