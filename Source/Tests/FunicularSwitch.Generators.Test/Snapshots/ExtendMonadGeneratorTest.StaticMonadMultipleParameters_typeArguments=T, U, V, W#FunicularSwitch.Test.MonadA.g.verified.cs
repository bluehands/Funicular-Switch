//HintName: FunicularSwitch.Test.MonadA.g.cs
namespace FunicularSwitch.Test
{
    public static partial class MonadA
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> Return<T0, T1, T2, T3, A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> Return<T0, T1, T2, T3, A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, A>((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> Bind<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> Bind<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B> SelectMany<T0, T1, T2, T3, A, B>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>)ma), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> SelectMany<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> SelectMany<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn) => global::FunicularSwitch.Test.MonadA.Bind(((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>)(await ma)), [global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, C> SelectMany<T0, T1, T2, T3, A, B, C>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, C>> SelectMany<T0, T1, T2, T3, A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, C>> SelectMany<T0, T1, T2, T3, A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B> Map<T0, T1, T2, T3, A, B>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> Map<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> Map<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B> Select<T0, T1, T2, T3, A, B>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> Select<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, B>(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, B>> Select<T0, T1, T2, T3, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA.Return<T0, T1, T2, T3, B>(fn(a)));
    }
}
