//HintName: MonadAMonadB.g.cs
public partial class MonadAMonadB<A>(global::MonadA<global::MonadB<A>> M) : global::FunicularSwitch.Transformers.Monad<A>
{
    public global::MonadA<global::MonadB<A>> M { get; } = M;
    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static implicit operator MonadAMonadB<A>(global::MonadA<global::MonadB<A>> ma) => new(ma);
    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static implicit operator global::MonadA<global::MonadB<A>>(MonadAMonadB<A> ma) => ma.M;
    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Return<A_>(A_ a) => MonadAMonadB.Return(a);
    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    global::FunicularSwitch.Transformers.Monad<A_> global::FunicularSwitch.Transformers.Monad<A>.Bind<A_>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<A_>> fn) => this.Bind(a => (MonadAMonadB<A_>)fn(a));
    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    A_ global::FunicularSwitch.Transformers.Monad<A>.Cast<A_>() => (A_)(object)M;
}

public static partial class MonadAMonadB
{
    private readonly record struct Impl__MonadA<A>(global::MonadA<A> M) : global::FunicularSwitch.Transformers.Monad<A>
    {
        [global::System.Diagnostics.Contracts.PureAttribute]
        [global::System.Diagnostics.DebuggerStepThrough]
        public static implicit operator Impl__MonadA<A>(global::MonadA<A> ma) => new(ma);
        [global::System.Diagnostics.Contracts.PureAttribute]
        [global::System.Diagnostics.DebuggerStepThrough]
        public static implicit operator global::MonadA<A>(Impl__MonadA<A> ma) => ma.M;
        [global::System.Diagnostics.Contracts.PureAttribute]
        [global::System.Diagnostics.DebuggerStepThrough]
        public global::FunicularSwitch.Transformers.Monad<B> Return<B>(B a) => (Impl__MonadA<B>)global::MonadA.Return<B>(a);
        [global::System.Diagnostics.Contracts.PureAttribute]
        [global::System.Diagnostics.DebuggerStepThrough]
        public global::FunicularSwitch.Transformers.Monad<B> Bind<B>(global::System.Func<A, global::FunicularSwitch.Transformers.Monad<B>> fn) => (Impl__MonadA<B>)global::MonadA.Bind(M, a => (global::MonadA<B>)(Impl__MonadA<B>)fn(a));
        [global::System.Diagnostics.Contracts.PureAttribute]
        [global::System.Diagnostics.DebuggerStepThrough]
        public B Cast<B>() => (B)(object)M;
    }

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<A> Lift<A>(global::MonadA<A> ma) => global::MonadA.Bind(ma, [global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<A>>(global::MonadB.Return<A>(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> Lift<A>(global::System.Threading.Tasks.Task<global::MonadA<A>> ma) => global::MonadA.Bind((await ma), [global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<A>>(global::MonadB.Return<A>(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> Lift<A>(global::System.Threading.Tasks.ValueTask<global::MonadA<A>> ma) => global::MonadA.Bind((await ma), [global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<A>>(global::MonadB.Return<A>(a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<A> Return<A>(A a) => global::MonadA.Return<global::MonadB<A>>(global::MonadB.Return<A>(a));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> Return<A>(global::System.Threading.Tasks.Task<A> a) => global::MonadA.Return<global::MonadB<A>>(global::MonadB.Return<A>((await a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> Return<A>(global::System.Threading.Tasks.ValueTask<A> a) => global::MonadA.Return<global::MonadB<A>>(global::MonadB.Return<A>((await a)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<B> Bind<A, B>(this global::MonadAMonadB<A> ma, global::System.Func<A, global::MonadAMonadB<B>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)ma), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadAMonadB<B>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<B>> Bind<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadAMonadB<B>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<B> Bind<A, B>(this global::MonadAMonadB<A> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)ma), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<B>> Bind<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<B> SelectMany<A, B>(this global::MonadAMonadB<A> ma, global::System.Func<A, global::MonadAMonadB<B>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)ma), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadAMonadB<B>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadAMonadB<B>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<B> SelectMany<A, B>(this global::MonadAMonadB<A> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)ma), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn) => global::MonadBT.BindT<A, B>((Impl__MonadA<global::MonadB<A>>)((global::MonadA<global::MonadB<A>>)(await ma)), [global::System.Diagnostics.DebuggerStepThrough](a) => (Impl__MonadA<global::MonadB<B>>)(new global::System.Func<A, global::MonadA<global::MonadB<B>>>([global::System.Diagnostics.DebuggerStepThrough](a) => fn(a)).Invoke(a))).Cast<global::MonadA<global::MonadB<B>>>();

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<C> SelectMany<A, B, C>(this global::MonadAMonadB<A> ma, global::System.Func<A, global::MonadAMonadB<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadAMonadB<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadAMonadB<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadAMonadB<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadAMonadB<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadAMonadB<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<C> SelectMany<A, B, C>(this global::MonadAMonadB<A> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadAMonadB<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadAMonadB<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, global::MonadA<global::MonadB<B>>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThrough](a) => ((global::MonadAMonadB<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThrough](b) => selector(a, b)));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<B> Map<A, B>(this global::MonadAMonadB<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<B>>(global::MonadB.Return<B>(fn(a))));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<B>>(global::MonadB.Return<B>(fn(a))));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<B>> Map<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<B>>(global::MonadB.Return<B>(fn(a))));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static global::MonadAMonadB<B> Select<A, B>(this global::MonadAMonadB<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<B>>(global::MonadB.Return<B>(fn(a))));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.Task<global::MonadAMonadB<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::MonadAMonadB<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<B>>(global::MonadB.Return<B>(fn(a))));

    [global::System.Diagnostics.Contracts.PureAttribute]
    [global::System.Diagnostics.DebuggerStepThrough]
    public static async global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<B>> Select<A, B>(this global::System.Threading.Tasks.ValueTask<global::MonadAMonadB<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThrough](a) => global::MonadA.Return<global::MonadB<B>>(global::MonadB.Return<B>(fn(a))));
}
