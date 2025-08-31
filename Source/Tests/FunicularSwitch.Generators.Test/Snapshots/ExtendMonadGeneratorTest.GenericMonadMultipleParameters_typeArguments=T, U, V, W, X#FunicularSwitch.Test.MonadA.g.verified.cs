//HintName: FunicularSwitch.Test.MonadA.g.cs
namespace FunicularSwitch.Test
{
    public static partial class MonadA
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> Return<T0, T1, T2, T3, T4, A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>.Return((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> Return<T0, T1, T2, T3, T4, A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>.Return((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> Bind<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> Bind<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B> SelectMany<T0, T1, T2, T3, T4, A, B>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>)ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> SelectMany<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> SelectMany<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, C> SelectMany<T0, T1, T2, T3, T4, A, B, C>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, C>> SelectMany<T0, T1, T2, T3, T4, A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, C>> SelectMany<T0, T1, T2, T3, T4, A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B> Map<T0, T1, T2, T3, T4, A, B>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> Map<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> Map<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B> Select<T0, T1, T2, T3, T4, A, B>(this global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> Select<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>> Select<T0, T1, T2, T3, T4, A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<T0, T1, T2, T3, T4, B>.Return(fn(a)));
    }
}
