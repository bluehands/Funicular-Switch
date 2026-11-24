//HintName: MonadA.g.cs
public static partial class MonadA
{
    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadA<A>> Return<A>(global::System.Threading.Tasks.Task<A> a) => global::MonadA<A>.Return((await a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadA<A>> Return<A>(global::System.Threading.Tasks.ValueTask<A> a) => global::MonadA<A>.Return((await a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadA<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::MonadA<A>> ma, global::System.Func<A, global::MonadA<B>> fn) => ((global::MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadA<B>> Bind<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadA<A>> ma, global::System.Func<A, global::MonadA<B>> fn) => ((global::MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadA<B> SelectMany<A, B>(this global::MonadA<A> ma, global::System.Func<A, global::MonadA<B>> fn) => ((global::MonadA<A>)ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadA<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::MonadA<A>> ma, global::System.Func<A, global::MonadA<B>> fn) => ((global::MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadA<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadA<A>> ma, global::System.Func<A, global::MonadA<B>> fn) => ((global::MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadA<C> SelectMany<A, B, C>(this global::MonadA<A> ma, global::System.Func<A, global::MonadA<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadA<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::MonadA<A>> ma, global::System.Func<A, global::MonadA<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadA<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::MonadA<A>> ma, global::System.Func<A, global::MonadA<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadA<B> Map<A, B>(this global::MonadA<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA<B>.Return(fn(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadA<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA<B>.Return(fn(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadA<B>> Map<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA<B>.Return(fn(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadA<B> Select<A, B>(this global::MonadA<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA<B>.Return(fn(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadA<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA<B>.Return(fn(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadA<B>> Select<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA<B>.Return(fn(a)));
}
