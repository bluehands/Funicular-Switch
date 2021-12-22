#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunicularSwitch.Test

{
    abstract partial class OperationResult
    {
        
        public static OperationResult<(T1, T2)> Aggregate<T1, T2>(OperationResult<T1> r1, OperationResult<T2> r2) => OperationResultExtension.Aggregate(r1, r2);

        public static OperationResult<TResult> Aggregate<T1, T2, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, Func<T1, T2, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, combine);

        public static Task<OperationResult<(T1, T2)>> Aggregate<T1, T2>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2) => OperationResultExtension.Aggregate(r1, r2);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Func<T1, T2, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, combine);

        public static OperationResult<(T1, T2, T3)> Aggregate<T1, T2, T3>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3) => OperationResultExtension.Aggregate(r1, r2, r3);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, Func<T1, T2, T3, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, combine);

        public static Task<OperationResult<(T1, T2, T3)>> Aggregate<T1, T2, T3>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3) => OperationResultExtension.Aggregate(r1, r2, r3);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Func<T1, T2, T3, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, combine);

        public static OperationResult<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4) => OperationResultExtension.Aggregate(r1, r2, r3, r4);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, Func<T1, T2, T3, T4, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, combine);

        public static Task<OperationResult<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4) => OperationResultExtension.Aggregate(r1, r2, r3, r4);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Func<T1, T2, T3, T4, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, combine);

        public static OperationResult<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, Func<T1, T2, T3, T4, T5, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, combine);

        public static Task<OperationResult<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Func<T1, T2, T3, T4, T5, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, combine);

        public static OperationResult<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, combine);

        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, combine);

        public static OperationResult<(T1, T2, T3, T4, T5, T6, T7)> Aggregate<T1, T2, T3, T4, T5, T6, T7>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, combine);

        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6, T7)>> Aggregate<T1, T2, T3, T4, T5, T6, T7>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, combine);

        public static OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, combine);

        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, combine);

        public static OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8, OperationResult<T9> r9) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9);

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8, OperationResult<T9> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, combine);

        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8, Task<OperationResult<T9>> r9) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9);

        public static Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8, Task<OperationResult<T9>> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine) => OperationResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, combine);
    }

    static partial class OperationResultExtension
    {
        public static OperationResult<IReadOnlyCollection<T1>> Map<T, T1>(this IEnumerable<OperationResult<T>> results,
            Func<T, T1> map) =>
            results.Select(r => r.Map(map)).Aggregate();

        public static OperationResult<IReadOnlyCollection<T1>> Bind<T, T1>(this IEnumerable<OperationResult<T>> results,
            Func<T, OperationResult<T1>> bind) =>
            results.Select(r => r.Bind(bind)).Aggregate();

        public static OperationResult<IReadOnlyCollection<T1>> Bind<T, T1>(this OperationResult<T> result,
            Func<T, IEnumerable<OperationResult<T1>>> bindMany) =>
            result.Map(ok => bindMany(ok).Aggregate()).Flatten();

        public static OperationResult<T1> Bind<T, T1>(this IEnumerable<OperationResult<T>> results,
            Func<IEnumerable<T>, OperationResult<T1>> bind) =>
            results.Aggregate().Bind(bind);
        
        public static OperationResult<IReadOnlyCollection<T>> Aggregate<T>(this IEnumerable<OperationResult<T>> results)
        {
            MyError? errors = default;
            var oks = new List<T>();
            foreach (var result in results)
            {
                result.Match(
                    ok => oks.Add(ok),
                    error => { errors = errors == null ? error : errors.Merge(error); }
                );
            }

            return errors != null
                ? OperationResult.Error<IReadOnlyCollection<T>>(errors)
                : OperationResult.Ok<IReadOnlyCollection<T>>(oks);
        }

        public static async Task<OperationResult<IReadOnlyCollection<T>>> Aggregate<T>(
            this Task<IEnumerable<OperationResult<T>>> results)
            => (await results.ConfigureAwait(false))
                .Aggregate();

        public static async Task<OperationResult<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<OperationResult<T>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .Aggregate();

        public static async Task<OperationResult<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<OperationResult<T>>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        
        public static OperationResult<(T1, T2)> Aggregate<T1, T2>(this OperationResult<T1> r1, OperationResult<T2> r2) => 
            Aggregate(r1, r2, (v1, v2) => (v1, v2));

        public static OperationResult<TResult> Aggregate<T1, T2, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, Func<T1, T2, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2)
                return combine(ok1.Value, ok2.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2)>> Aggregate<T1, T2>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2)
            => Aggregate(r1, r2, (v1, v2) => (v1, v2));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Func<T1, T2, TResult> combine)            
        {
            await Task.WhenAll(r1, r2);
            return Aggregate(r1.Result, r2.Result, combine);
        }

        public static OperationResult<(T1, T2, T3)> Aggregate<T1, T2, T3>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3) => 
            Aggregate(r1, r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, Func<T1, T2, T3, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3)
                return combine(ok1.Value, ok2.Value, ok3.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3)>> Aggregate<T1, T2, T3>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3)
            => Aggregate(r1, r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Func<T1, T2, T3, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3);
            return Aggregate(r1.Result, r2.Result, r3.Result, combine);
        }

        public static OperationResult<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4) => 
            Aggregate(r1, r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, Func<T1, T2, T3, T4, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3 && r4 is OperationResult<T4>.Ok_ ok4)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3, r4 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4)
            => Aggregate(r1, r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Func<T1, T2, T3, T4, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, combine);
        }

        public static OperationResult<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5) => 
            Aggregate(r1, r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, Func<T1, T2, T3, T4, T5, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3 && r4 is OperationResult<T4>.Ok_ ok4 && r5 is OperationResult<T5>.Ok_ ok5)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3, r4, r5 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5)
            => Aggregate(r1, r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Func<T1, T2, T3, T4, T5, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, combine);
        }

        public static OperationResult<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6) => 
            Aggregate(r1, r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3 && r4 is OperationResult<T4>.Ok_ ok4 && r5 is OperationResult<T5>.Ok_ ok5 && r6 is OperationResult<T6>.Ok_ ok6)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3, r4, r5, r6 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6)
            => Aggregate(r1, r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, combine);
        }

        public static OperationResult<(T1, T2, T3, T4, T5, T6, T7)> Aggregate<T1, T2, T3, T4, T5, T6, T7>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, (v1, v2, v3, v4, v5, v6, v7) => (v1, v2, v3, v4, v5, v6, v7));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3 && r4 is OperationResult<T4>.Ok_ ok4 && r5 is OperationResult<T5>.Ok_ ok5 && r6 is OperationResult<T6>.Ok_ ok6 && r7 is OperationResult<T7>.Ok_ ok7)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3, r4, r5, r6, r7 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6, T7)>> Aggregate<T1, T2, T3, T4, T5, T6, T7>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, (v1, v2, v3, v4, v5, v6, v7) => (v1, v2, v3, v4, v5, v6, v7));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, combine);
        }

        public static OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, (v1, v2, v3, v4, v5, v6, v7, v8) => (v1, v2, v3, v4, v5, v6, v7, v8));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3 && r4 is OperationResult<T4>.Ok_ ok4 && r5 is OperationResult<T5>.Ok_ ok5 && r6 is OperationResult<T6>.Ok_ ok6 && r7 is OperationResult<T7>.Ok_ ok7 && r8 is OperationResult<T8>.Ok_ ok8)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value, ok8.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3, r4, r5, r6, r7, r8 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, (v1, v2, v3, v4, v5, v6, v7, v8) => (v1, v2, v3, v4, v5, v6, v7, v8));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7, r8);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, r8.Result, combine);
        }

        public static OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8, OperationResult<T9> r9) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, (v1, v2, v3, v4, v5, v6, v7, v8, v9) => (v1, v2, v3, v4, v5, v6, v7, v8, v9));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6, OperationResult<T7> r7, OperationResult<T8> r8, OperationResult<T9> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine)            
        {
            if (r1 is OperationResult<T1>.Ok_ ok1 && r2 is OperationResult<T2>.Ok_ ok2 && r3 is OperationResult<T3>.Ok_ ok3 && r4 is OperationResult<T4>.Ok_ ok4 && r5 is OperationResult<T5>.Ok_ ok5 && r6 is OperationResult<T6>.Ok_ ok6 && r7 is OperationResult<T7>.Ok_ ok7 && r8 is OperationResult<T8>.Ok_ ok8 && r9 is OperationResult<T9>.Ok_ ok9)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value, ok8.Value, ok9.Value);

            return OperationResult.Error<TResult>(
                MergeErrors(new OperationResult[] { r1, r2, r3, r4, r5, r6, r7, r8, r9 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<OperationResult<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8, Task<OperationResult<T9>> r9)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, (v1, v2, v3, v4, v5, v6, v7, v8, v9) => (v1, v2, v3, v4, v5, v6, v7, v8, v9));

        public static async Task<OperationResult<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2, Task<OperationResult<T3>> r3, Task<OperationResult<T4>> r4, Task<OperationResult<T5>> r5, Task<OperationResult<T6>> r6, Task<OperationResult<T7>> r7, Task<OperationResult<T8>> r8, Task<OperationResult<T9>> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7, r8, r9);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, r8.Result, r9.Result, combine);
        }

        public static OperationResult<T> FirstOk<T>(this IEnumerable<OperationResult<T>> results, Func<MyError> onEmpty)
        {
            var errors = new List<MyError>();
            foreach (var result in results)
            {
                if (result.IsError)
                    errors.Add(result.GetErrorOrDefault()!);
                else
                    return result;
            }

            if (!errors.Any())
                errors.Add(onEmpty());

            return OperationResult.Error<T>(MergeErrors(errors)!);
        }

        //depends on FunicularSwitch and merge

        //public static async Task<OperationResult<IReadOnlyCollection<T>>> Aggregate<T>(
        //    this IEnumerable<Task<OperationResult<T>>> results,
        //    int maxDegreeOfParallelism)
        //    => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
        //        .Aggregate();

        //public static async Task<OperationResult<IReadOnlyCollection<T>>> AggregateMany<T>(
        //    this IEnumerable<Task<IEnumerable<OperationResult<T>>>> results,
        //    int maxDegreeOfParallelism)
        //    => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
        //        .SelectMany(e => e)
        //        .Aggregate();


        #region helpers

        static MyError? MergeErrors(IEnumerable<MyError> errors)
        {
            var first = true;
            MyError? aggregated = default;
            foreach (var myError in errors)
            {
                if (first)
                {
                    aggregated = myError;
                    first = false;
                }
                else
                {
                    aggregated = aggregated!.Merge(myError);
                }
            }

            return aggregated;
        }

        #endregion
    }
}