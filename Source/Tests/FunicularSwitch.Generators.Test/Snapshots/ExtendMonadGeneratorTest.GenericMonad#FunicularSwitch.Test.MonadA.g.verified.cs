//HintName: FunicularSwitch.Test.MonadA.g.cs
namespace FunicularSwitch.Test
{
    public static partial class MonadA
    {
        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> Return<A>(global::System.Threading.Tasks.Task<A> a) => global::FunicularSwitch.Test.MonadA<A>.Return((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> Return<A>(global::System.Threading.Tasks.ValueTask<A> a) => global::FunicularSwitch.Test.MonadA<A>.Return((await a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> Bind<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => ((global::FunicularSwitch.Test.MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> Bind<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => ((global::FunicularSwitch.Test.MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<B> SelectMany<A, B>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => ((global::FunicularSwitch.Test.MonadA<A>)ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> SelectMany<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => ((global::FunicularSwitch.Test.MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> SelectMany<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn) => ((global::FunicularSwitch.Test.MonadA<A>)(await ma)).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => fn(a));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<C> SelectMany<A, B, C>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn, global::System.Func<A, B, C> selector) => ma.SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<C>> SelectMany<A, B, C>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, global::FunicularSwitch.Test.MonadA<B>> fn, global::System.Func<A, B, C> selector) => (await ma).SelectMany([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => ((global::FunicularSwitch.Test.MonadA<B>)fn(a)).Map([global::System.Diagnostics.DebuggerStepThroughAttribute](b) => selector(a, b)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<B> Map<A, B>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> Map<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> Map<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<B> Select<A, B>(this global::FunicularSwitch.Test.MonadA<A> ma, global::System.Func<A, B> fn) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<B>> Select<A, B>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<B>> Select<A, B>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> ma, global::System.Func<A, B> fn) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => global::FunicularSwitch.Test.MonadA<B>.Return(fn(a)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1)> Combine<S0, S1>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1) => s0.Bind(v0 => s1.Map<S1, (S0, S1)>(v1 => (v0, v1)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2)> Combine<S0, S1, S2>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2) => Combine(s0, s1).Bind(prev => s2.Map<S2, (S0, S1, S2)>(last => (prev.Item1, prev.Item2, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3)> Combine<S0, S1, S2, S3>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3) => Combine(s0, s1, s2).Bind(prev => s3.Map<S3, (S0, S1, S2, S3)>(last => (prev.Item1, prev.Item2, prev.Item3, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4)> Combine<S0, S1, S2, S3, S4>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4) => Combine(s0, s1, s2, s3).Bind(prev => s4.Map<S4, (S0, S1, S2, S3, S4)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5)> Combine<S0, S1, S2, S3, S4, S5>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5) => Combine(s0, s1, s2, s3, s4).Bind(prev => s5.Map<S5, (S0, S1, S2, S3, S4, S5)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5, S6)> Combine<S0, S1, S2, S3, S4, S5, S6>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5, global::FunicularSwitch.Test.MonadA<S6> s6) => Combine(s0, s1, s2, s3, s4, s5).Bind(prev => s6.Map<S6, (S0, S1, S2, S3, S4, S5, S6)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5, S6, S7)> Combine<S0, S1, S2, S3, S4, S5, S6, S7>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5, global::FunicularSwitch.Test.MonadA<S6> s6, global::FunicularSwitch.Test.MonadA<S7> s7) => Combine(s0, s1, s2, s3, s4, s5, s6).Bind(prev => s7.Map<S7, (S0, S1, S2, S3, S4, S5, S6, S7)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5, S6, S7, S8)> Combine<S0, S1, S2, S3, S4, S5, S6, S7, S8>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5, global::FunicularSwitch.Test.MonadA<S6> s6, global::FunicularSwitch.Test.MonadA<S7> s7, global::FunicularSwitch.Test.MonadA<S8> s8) => Combine(s0, s1, s2, s3, s4, s5, s6, s7).Bind(prev => s8.Map<S8, (S0, S1, S2, S3, S4, S5, S6, S7, S8)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5, S6, S7, S8, S9)> Combine<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5, global::FunicularSwitch.Test.MonadA<S6> s6, global::FunicularSwitch.Test.MonadA<S7> s7, global::FunicularSwitch.Test.MonadA<S8> s8, global::FunicularSwitch.Test.MonadA<S9> s9) => Combine(s0, s1, s2, s3, s4, s5, s6, s7, s8).Bind(prev => s9.Map<S9, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, prev.Item9, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10)> Combine<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5, global::FunicularSwitch.Test.MonadA<S6> s6, global::FunicularSwitch.Test.MonadA<S7> s7, global::FunicularSwitch.Test.MonadA<S8> s8, global::FunicularSwitch.Test.MonadA<S9> s9, global::FunicularSwitch.Test.MonadA<S10> s10) => Combine(s0, s1, s2, s3, s4, s5, s6, s7, s8, s9).Bind(prev => s10.Map<S10, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, prev.Item9, prev.Item10, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<(S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11)> Combine<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11>(global::FunicularSwitch.Test.MonadA<S0> s0, global::FunicularSwitch.Test.MonadA<S1> s1, global::FunicularSwitch.Test.MonadA<S2> s2, global::FunicularSwitch.Test.MonadA<S3> s3, global::FunicularSwitch.Test.MonadA<S4> s4, global::FunicularSwitch.Test.MonadA<S5> s5, global::FunicularSwitch.Test.MonadA<S6> s6, global::FunicularSwitch.Test.MonadA<S7> s7, global::FunicularSwitch.Test.MonadA<S8> s8, global::FunicularSwitch.Test.MonadA<S9> s9, global::FunicularSwitch.Test.MonadA<S10> s10, global::FunicularSwitch.Test.MonadA<S11> s11) => Combine(s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10).Bind(prev => s11.Map<S11, (S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11)>(last => (prev.Item1, prev.Item2, prev.Item3, prev.Item4, prev.Item5, prev.Item6, prev.Item7, prev.Item8, prev.Item9, prev.Item10, prev.Item11, last)));

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static global::FunicularSwitch.Test.MonadA<A> Flatten<A>(this global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadA<A>> ma) => ma.Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => a);

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<A>> Flatten<A>(this global::System.Threading.Tasks.Task<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadA<A>>> ma) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => a);

        [global::System.Diagnostics.Contracts.PureAttribute, global::System.Diagnostics.DebuggerStepThroughAttribute]
        public static async global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<A>> Flatten<A>(this global::System.Threading.Tasks.ValueTask<global::FunicularSwitch.Test.MonadA<global::FunicularSwitch.Test.MonadA<A>>> ma) => (await ma).Bind([global::System.Diagnostics.DebuggerStepThroughAttribute](a) => a);
    }
}
