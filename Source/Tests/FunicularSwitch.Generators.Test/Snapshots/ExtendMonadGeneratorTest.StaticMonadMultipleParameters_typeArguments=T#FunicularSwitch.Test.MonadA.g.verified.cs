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

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1)> Combine<T0, S0, S1>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1) => global::FunicularSwitch.Test.MonadA.Bind(s0, v0 => s1.Map<T0, S1, (S0, S1)>(v1 => (v0, v1)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2)> Combine<T0, S0, S1, S2>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1), prev => s2.Map<T0, S2, (S0, S1, S2)>(last => (prev.Item1, prev.Item2, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3)> Combine<T0, S0, S1, S2, S3>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2), prev => s3.Map<T0, S3, (S0, S1, S2, S3)>(last => (prev.Item1, prev.Item2, prev.Item3, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4)> Combine<T0, S0, S1, S2, S3, S4>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3), prev => s4.Map<T0, S4, (S0, S1, S2, S3, S4)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5)> Combine<T0, S0, S1, S2, S3, S4, S5>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4), prev => s5.Map<T0, S5, (S0, S1, S2, S3, S4, S5)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5, S6)> Combine<T0, S0, S1, S2, S3, S4, S5, S6>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5, global::FunicularSwitch.Test.MonadA<T0, S6> s6) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4, s5), prev => s6.Map<T0, S6, (S0, S1, S2, S3, S4, S5, S6)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5, S6, S7)> Combine<T0, S0, S1, S2, S3, S4, S5, S6, S7>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5, global::FunicularSwitch.Test.MonadA<T0, S6> s6, global::FunicularSwitch.Test.MonadA<T0, S7> s7) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4, s5, s6), prev => s7.Map<T0, S7, (S0, S1, S2, S3, S4, S5, S6, S7)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5, S6, S7, S8)> Combine<T0, S0, S1, S2, S3, S4, S5, S6, S7, S8>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5, global::FunicularSwitch.Test.MonadA<T0, S6> s6, global::FunicularSwitch.Test.MonadA<T0, S7> s7, global::FunicularSwitch.Test.MonadA<T0, S8> s8) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4, s5, s6, s7), prev => s8.Map<T0, S8, (S0, S1, S2, S3, S4, S5, S6, S7, S8)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9)> Combine<T0, S0, S1, S2, S3, S4, S5, S6, S7, S8, S9>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5, global::FunicularSwitch.Test.MonadA<T0, S6> s6, global::FunicularSwitch.Test.MonadA<T0, S7> s7, global::FunicularSwitch.Test.MonadA<T0, S8> s8, global::FunicularSwitch.Test.MonadA<T0, S9> s9) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4, s5, s6, s7, s8), prev => s9.Map<T0, S9, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, prev.Item9, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10)> Combine<T0, S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5, global::FunicularSwitch.Test.MonadA<T0, S6> s6, global::FunicularSwitch.Test.MonadA<T0, S7> s7, global::FunicularSwitch.Test.MonadA<T0, S8> s8, global::FunicularSwitch.Test.MonadA<T0, S9> s9, global::FunicularSwitch.Test.MonadA<T0, S10> s10) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4, s5, s6, s7, s8, s9), prev => s10.Map<T0, S10, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, prev.Item9, prev.Item10, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11)> Combine<T0, S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11>(global::FunicularSwitch.Test.MonadA<T0, S0> s0, global::FunicularSwitch.Test.MonadA<T0, S1> s1, global::FunicularSwitch.Test.MonadA<T0, S2> s2, global::FunicularSwitch.Test.MonadA<T0, S3> s3, global::FunicularSwitch.Test.MonadA<T0, S4> s4, global::FunicularSwitch.Test.MonadA<T0, S5> s5, global::FunicularSwitch.Test.MonadA<T0, S6> s6, global::FunicularSwitch.Test.MonadA<T0, S7> s7, global::FunicularSwitch.Test.MonadA<T0, S8> s8, global::FunicularSwitch.Test.MonadA<T0, S9> s9, global::FunicularSwitch.Test.MonadA<T0, S10> s10, global::FunicularSwitch.Test.MonadA<T0, S11> s11) => global::FunicularSwitch.Test.MonadA.Bind(Combine(s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10), prev => s11.Map<T0, S11, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, prev.Item9, prev.Item10, prev.Item11, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<T0, A> Flatten<T0, A>(this global::FunicularSwitch.Test.MonadA<T0, global::FunicularSwitch.Test.MonadA<T0, A>> ma) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => a);

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, A>> Flatten<T0, A>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<T0, global::FunicularSwitch.Test.MonadA<T0, A>>> ma) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => a);

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, A>> Flatten<T0, A>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<T0, global::FunicularSwitch.Test.MonadA<T0, A>>> ma) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => a);
    }
}
