#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FunicularSwitch;
using FunicularSwitch.Generators.Consumer;

namespace Test

{
#pragma warning disable 1591
    public abstract partial class Blubs
    {
        
        public static Blubs<(T1, T2)> Aggregate<T1, T2>(Blubs<T1> r1, Blubs<T2> r2) => BlubsExtension.Aggregate(r1, r2);

        public static Blubs<TResult> Aggregate<T1, T2, TResult>(Blubs<T1> r1, Blubs<T2> r2, Func<T1, T2, TResult> combine) => BlubsExtension.Aggregate(r1, r2, combine);

        public static Task<Blubs<(T1, T2)>> Aggregate<T1, T2>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2) => BlubsExtension.Aggregate(r1, r2);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Func<T1, T2, TResult> combine) => BlubsExtension.Aggregate(r1, r2, combine);

        public static Blubs<(T1, T2, T3)> Aggregate<T1, T2, T3>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3) => BlubsExtension.Aggregate(r1, r2, r3);

        public static Blubs<TResult> Aggregate<T1, T2, T3, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Func<T1, T2, T3, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, combine);

        public static Task<Blubs<(T1, T2, T3)>> Aggregate<T1, T2, T3>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3) => BlubsExtension.Aggregate(r1, r2, r3);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Func<T1, T2, T3, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, combine);

        public static Blubs<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4) => BlubsExtension.Aggregate(r1, r2, r3, r4);

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Func<T1, T2, T3, T4, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, combine);

        public static Task<Blubs<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4) => BlubsExtension.Aggregate(r1, r2, r3, r4);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Func<T1, T2, T3, T4, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, combine);

        public static Blubs<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5);

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Func<T1, T2, T3, T4, T5, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, combine);

        public static Task<Blubs<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Func<T1, T2, T3, T4, T5, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, combine);

        public static Blubs<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6);

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, combine);

        public static Task<Blubs<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, combine);

        public static Blubs<(T1, T2, T3, T4, T5, T6, T7)> Aggregate<T1, T2, T3, T4, T5, T6, T7>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7);

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, combine);

        public static Task<Blubs<(T1, T2, T3, T4, T5, T6, T7)>> Aggregate<T1, T2, T3, T4, T5, T6, T7>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, combine);

        public static Blubs<(T1, T2, T3, T4, T5, T6, T7, T8)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8);

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, combine);

        public static Task<Blubs<(T1, T2, T3, T4, T5, T6, T7, T8)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, combine);

        public static Blubs<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8, Blubs<T9> r9) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9);

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8, Blubs<T9> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, combine);

        public static Task<Blubs<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8, Task<Blubs<T9>> r9) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9);

        public static Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8, Task<Blubs<T9>> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine) => BlubsExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, combine);
    }

    public static partial class BlubsExtension
    {
        public static Blubs<IReadOnlyCollection<T1>> Map<T, T1>(this IEnumerable<Blubs<T>> results,
            Func<T, T1> map) =>
            results.Select(r => r.Map(map)).Aggregate();

        public static Blubs<IReadOnlyCollection<T1>> Bind<T, T1>(this IEnumerable<Blubs<T>> results,
            Func<T, Blubs<T1>> bind) =>
            results.Select(r => r.Bind(bind)).Aggregate();

        public static Blubs<IReadOnlyCollection<T1>> Bind<T, T1>(this Blubs<T> result,
            Func<T, IEnumerable<Blubs<T1>>> bindMany) =>
            result.Map(ok => bindMany(ok).Aggregate()).Flatten();

        public static Blubs<T1> Bind<T, T1>(this IEnumerable<Blubs<T>> results,
            Func<IEnumerable<T>, Blubs<T1>> bind) =>
            results.Aggregate().Bind(bind);
        
        public static Blubs<IReadOnlyCollection<T>> Aggregate<T>(this IEnumerable<Blubs<T>> results)
        {
            var isError = false;
            String aggregated = default!;
            var oks = new List<T>();
            foreach (var result in results)
            {
                result.Match(
                    ok => oks.Add(ok),
                    error =>
                    {
                        aggregated = !isError ? error : MergeErrors(aggregated, error);
                        isError = true;
                    }
                );
            }

            return isError
                ? Blubs.Error<IReadOnlyCollection<T>>(aggregated)
                : Blubs.Ok<IReadOnlyCollection<T>>(oks);
        }

        public static async Task<Blubs<IReadOnlyCollection<T>>> Aggregate<T>(
            this Task<IEnumerable<Blubs<T>>> results)
            => (await results.ConfigureAwait(false))
                .Aggregate();

        public static async Task<Blubs<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<Blubs<T>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .Aggregate();

        public static async Task<Blubs<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<Blubs<T>>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        
        public static Blubs<(T1, T2)> Aggregate<T1, T2>(this Blubs<T1> r1, Blubs<T2> r2) => 
            Aggregate(r1, r2, (v1, v2) => (v1, v2));

        public static Blubs<TResult> Aggregate<T1, T2, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Func<T1, T2, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2)
                return combine(ok1.Value, ok2.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2)>> Aggregate<T1, T2>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2)
            => Aggregate(r1, r2, (v1, v2) => (v1, v2));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Func<T1, T2, TResult> combine)            
        {
            await Task.WhenAll(r1, r2);
            return Aggregate(r1.Result, r2.Result, combine);
        }

        public static Blubs<(T1, T2, T3)> Aggregate<T1, T2, T3>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3) => 
            Aggregate(r1, r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static Blubs<TResult> Aggregate<T1, T2, T3, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Func<T1, T2, T3, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3)
                return combine(ok1.Value, ok2.Value, ok3.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3)>> Aggregate<T1, T2, T3>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3)
            => Aggregate(r1, r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Func<T1, T2, T3, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3);
            return Aggregate(r1.Result, r2.Result, r3.Result, combine);
        }

        public static Blubs<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4) => 
            Aggregate(r1, r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4));

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Func<T1, T2, T3, T4, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3 && r4 is Blubs<T4>.Ok_ ok4)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3, r4 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4)
            => Aggregate(r1, r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Func<T1, T2, T3, T4, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, combine);
        }

        public static Blubs<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5) => 
            Aggregate(r1, r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5));

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Func<T1, T2, T3, T4, T5, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3 && r4 is Blubs<T4>.Ok_ ok4 && r5 is Blubs<T5>.Ok_ ok5)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3, r4, r5 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5)
            => Aggregate(r1, r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Func<T1, T2, T3, T4, T5, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, combine);
        }

        public static Blubs<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6) => 
            Aggregate(r1, r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6));

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3 && r4 is Blubs<T4>.Ok_ ok4 && r5 is Blubs<T5>.Ok_ ok5 && r6 is Blubs<T6>.Ok_ ok6)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3, r4, r5, r6 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6)
            => Aggregate(r1, r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, combine);
        }

        public static Blubs<(T1, T2, T3, T4, T5, T6, T7)> Aggregate<T1, T2, T3, T4, T5, T6, T7>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, (v1, v2, v3, v4, v5, v6, v7) => (v1, v2, v3, v4, v5, v6, v7));

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3 && r4 is Blubs<T4>.Ok_ ok4 && r5 is Blubs<T5>.Ok_ ok5 && r6 is Blubs<T6>.Ok_ ok6 && r7 is Blubs<T7>.Ok_ ok7)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3, r4, r5, r6, r7 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3, T4, T5, T6, T7)>> Aggregate<T1, T2, T3, T4, T5, T6, T7>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, (v1, v2, v3, v4, v5, v6, v7) => (v1, v2, v3, v4, v5, v6, v7));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, combine);
        }

        public static Blubs<(T1, T2, T3, T4, T5, T6, T7, T8)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, (v1, v2, v3, v4, v5, v6, v7, v8) => (v1, v2, v3, v4, v5, v6, v7, v8));

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3 && r4 is Blubs<T4>.Ok_ ok4 && r5 is Blubs<T5>.Ok_ ok5 && r6 is Blubs<T6>.Ok_ ok6 && r7 is Blubs<T7>.Ok_ ok7 && r8 is Blubs<T8>.Ok_ ok8)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value, ok8.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3, r4, r5, r6, r7, r8 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3, T4, T5, T6, T7, T8)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, (v1, v2, v3, v4, v5, v6, v7, v8) => (v1, v2, v3, v4, v5, v6, v7, v8));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7, r8);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, r8.Result, combine);
        }

        public static Blubs<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8, Blubs<T9> r9) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, (v1, v2, v3, v4, v5, v6, v7, v8, v9) => (v1, v2, v3, v4, v5, v6, v7, v8, v9));

        public static Blubs<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Blubs<T1> r1, Blubs<T2> r2, Blubs<T3> r3, Blubs<T4> r4, Blubs<T5> r5, Blubs<T6> r6, Blubs<T7> r7, Blubs<T8> r8, Blubs<T9> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine)            
        {
            if (r1 is Blubs<T1>.Ok_ ok1 && r2 is Blubs<T2>.Ok_ ok2 && r3 is Blubs<T3>.Ok_ ok3 && r4 is Blubs<T4>.Ok_ ok4 && r5 is Blubs<T5>.Ok_ ok5 && r6 is Blubs<T6>.Ok_ ok6 && r7 is Blubs<T7>.Ok_ ok7 && r8 is Blubs<T8>.Ok_ ok8 && r9 is Blubs<T9>.Ok_ ok9)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value, ok8.Value, ok9.Value);

            return Blubs.Error<TResult>(
                MergeErrors(new Blubs[] { r1, r2, r3, r4, r5, r6, r7, r8, r9 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Blubs<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8, Task<Blubs<T9>> r9)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, (v1, v2, v3, v4, v5, v6, v7, v8, v9) => (v1, v2, v3, v4, v5, v6, v7, v8, v9));

        public static async Task<Blubs<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Task<Blubs<T1>> r1, Task<Blubs<T2>> r2, Task<Blubs<T3>> r3, Task<Blubs<T4>> r4, Task<Blubs<T5>> r5, Task<Blubs<T6>> r6, Task<Blubs<T7>> r7, Task<Blubs<T8>> r8, Task<Blubs<T9>> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7, r8, r9);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, r8.Result, r9.Result, combine);
        }

        public static Blubs<T> FirstOk<T>(this IEnumerable<Blubs<T>> results, Func<String> onEmpty)
        {
            var errors = new List<String>();
            foreach (var result in results)
            {
                if (result is Blubs<T>.Error_ e)
                    errors.Add(e.Details);
                else
                    return result;
            }

            if (!errors.Any())
                errors.Add(onEmpty());

            return Blubs.Error<T>(MergeErrors(errors));
        }

        public static async Task<Blubs<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<Blubs<T>>> results,
            int maxDegreeOfParallelism)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .Aggregate();

        public static async Task<Blubs<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<Blubs<T>>>> results,
            int maxDegreeOfParallelism)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        static async Task<TOut[]> SelectAsync<T, TOut>(this IEnumerable<T> items, Func<T, Task<TOut>> selector, int maxDegreeOfParallelism)
        {
            using (var throttler = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism))
            {
                return await Task.WhenAll(items.Select(async item =>
                {
                    // ReSharper disable once AccessToDisposedClosure
                    await throttler.WaitAsync().ConfigureAwait(false);
                    try
                    {
                        return await selector(item).ConfigureAwait(false);
                    }
                    finally
                    {
                        // ReSharper disable once AccessToDisposedClosure
                        throttler.Release();
                    }
                })).ConfigureAwait(false);
            }
        }

        public static Blubs<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<T> candidates, Func<T, IEnumerable<String>> validate) =>
            candidates
                .Select(c => c.Validate(validate))
                .Aggregate();

        public static Blubs<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<Blubs<T>> candidates,
            Func<T, IEnumerable<String>> validate) =>
            candidates
                .Bind(items => items.AllOk(validate));

        public static Blubs<T> Validate<T>(this Blubs<T> item, Func<T, IEnumerable<String>> validate) => item.Bind(i => i.Validate(validate));

        public static Blubs<T> Validate<T>(this T item, Func<T, IEnumerable<String>> validate)
        {
            var errors = validate(item).ToList();
            return errors.Count > 0 ? Blubs.Error<T>(MergeErrors(errors)) : item;
        }

        public static Blubs<T> FirstOk<T>(this IEnumerable<T> candidates, Func<T, IEnumerable<String>> validate, Func<String> onEmpty) =>
            candidates
                .Select(r => r.Validate(validate))
                .FirstOk(onEmpty);

        #region helpers

        static String MergeErrors(IEnumerable<String> errors)
        {
            var first = true;
            String aggregated = default!;
            foreach (var myError in errors)
            {
                if (first)
                {
                    aggregated = myError;
                    first = false;
                }
                else
                {
                    aggregated = MergeErrors(aggregated, myError);
                }
            }

            return aggregated;
        }

        static String MergeErrors(String aggregated, String error) => aggregated.MergeErrors(error);

        #endregion
    }
#pragma warning restore 1591
}