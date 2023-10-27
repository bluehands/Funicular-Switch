#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FunicularSwitch;

namespace FunicularSwitch.Generators.Consumer
{
#pragma warning disable 1591
    public abstract partial class Result
    {
        
        public static Result<(T1, T2)> Aggregate<T1, T2>(Result<T1> r1, Result<T2> r2) => ResultExtension.Aggregate(r1, r2);

        public static Result<TResult> Aggregate<T1, T2, TResult>(Result<T1> r1, Result<T2> r2, Func<T1, T2, TResult> combine) => ResultExtension.Aggregate(r1, r2, combine);

        public static Task<Result<(T1, T2)>> Aggregate<T1, T2>(Task<Result<T1>> r1, Task<Result<T2>> r2) => ResultExtension.Aggregate(r1, r2);

        public static Task<Result<TResult>> Aggregate<T1, T2, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Func<T1, T2, TResult> combine) => ResultExtension.Aggregate(r1, r2, combine);

        public static Result<(T1, T2, T3)> Aggregate<T1, T2, T3>(Result<T1> r1, Result<T2> r2, Result<T3> r3) => ResultExtension.Aggregate(r1, r2, r3);

        public static Result<TResult> Aggregate<T1, T2, T3, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Func<T1, T2, T3, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, combine);

        public static Task<Result<(T1, T2, T3)>> Aggregate<T1, T2, T3>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3) => ResultExtension.Aggregate(r1, r2, r3);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Func<T1, T2, T3, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, combine);

        public static Result<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4) => ResultExtension.Aggregate(r1, r2, r3, r4);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Func<T1, T2, T3, T4, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, combine);

        public static Task<Result<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4) => ResultExtension.Aggregate(r1, r2, r3, r4);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, T4, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Func<T1, T2, T3, T4, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, combine);

        public static Result<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5) => ResultExtension.Aggregate(r1, r2, r3, r4, r5);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Func<T1, T2, T3, T4, T5, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, combine);

        public static Task<Result<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5) => ResultExtension.Aggregate(r1, r2, r3, r4, r5);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Func<T1, T2, T3, T4, T5, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, combine);

        public static Result<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, combine);

        public static Task<Result<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, combine);

        public static Result<(T1, T2, T3, T4, T5, T6, T7)> Aggregate<T1, T2, T3, T4, T5, T6, T7>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, combine);

        public static Task<Result<(T1, T2, T3, T4, T5, T6, T7)>> Aggregate<T1, T2, T3, T4, T5, T6, T7>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, combine);

        public static Result<(T1, T2, T3, T4, T5, T6, T7, T8)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, combine);

        public static Task<Result<(T1, T2, T3, T4, T5, T6, T7, T8)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, combine);

        public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8, Result<T9> r9) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8, Result<T9> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, combine);

        public static Task<Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8, Task<Result<T9>> r9) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9);

        public static Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8, Task<Result<T9>> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine) => ResultExtension.Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, combine);
    }

    public static partial class ResultExtension
    {
        public static Result<IReadOnlyCollection<T1>> Map<T, T1>(this IEnumerable<Result<T>> results,
            Func<T, T1> map) =>
            results.Select(r => r.Map(map)).Aggregate();

        public static Result<IReadOnlyCollection<T1>> Bind<T, T1>(this IEnumerable<Result<T>> results,
            Func<T, Result<T1>> bind) =>
            results.Select(r => r.Bind(bind)).Aggregate();

        public static Result<IReadOnlyCollection<T1>> Bind<T, T1>(this Result<T> result,
            Func<T, IEnumerable<Result<T1>>> bindMany) =>
            result.Map(ok => bindMany(ok).Aggregate()).Flatten();

        public static Result<T1> Bind<T, T1>(this IEnumerable<Result<T>> results,
            Func<IEnumerable<T>, Result<T1>> bind) =>
            results.Aggregate().Bind(bind);
        
        public static Result<IReadOnlyCollection<T>> Aggregate<T>(this IEnumerable<Result<T>> results)
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
                ? Result.Error<IReadOnlyCollection<T>>(aggregated)
                : Result.Ok<IReadOnlyCollection<T>>(oks);
        }

        public static async Task<Result<IReadOnlyCollection<T>>> Aggregate<T>(
            this Task<IEnumerable<Result<T>>> results)
            => (await results.ConfigureAwait(false))
                .Aggregate();

        public static async Task<Result<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<Result<T>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .Aggregate();

        public static async Task<Result<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<Result<T>>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        
        public static Result<(T1, T2)> Aggregate<T1, T2>(this Result<T1> r1, Result<T2> r2) => 
            Aggregate(r1, r2, (v1, v2) => (v1, v2));

        public static Result<TResult> Aggregate<T1, T2, TResult>(this Result<T1> r1, Result<T2> r2, Func<T1, T2, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2)
                return combine(ok1.Value, ok2.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2)>> Aggregate<T1, T2>(this Task<Result<T1>> r1, Task<Result<T2>> r2)
            => Aggregate(r1, r2, (v1, v2) => (v1, v2));

        public static async Task<Result<TResult>> Aggregate<T1, T2, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Func<T1, T2, TResult> combine)            
        {
            await Task.WhenAll(r1, r2);
            return Aggregate(r1.Result, r2.Result, combine);
        }

        public static Result<(T1, T2, T3)> Aggregate<T1, T2, T3>(this Result<T1> r1, Result<T2> r2, Result<T3> r3) => 
            Aggregate(r1, r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static Result<TResult> Aggregate<T1, T2, T3, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Func<T1, T2, T3, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3)
                return combine(ok1.Value, ok2.Value, ok3.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3)>> Aggregate<T1, T2, T3>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3)
            => Aggregate(r1, r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Func<T1, T2, T3, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3);
            return Aggregate(r1.Result, r2.Result, r3.Result, combine);
        }

        public static Result<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4) => 
            Aggregate(r1, r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4));

        public static Result<TResult> Aggregate<T1, T2, T3, T4, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Func<T1, T2, T3, T4, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3 && r4 is Result<T4>.Ok_ ok4)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3, r4 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4)
            => Aggregate(r1, r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Func<T1, T2, T3, T4, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, combine);
        }

        public static Result<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5) => 
            Aggregate(r1, r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5));

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Func<T1, T2, T3, T4, T5, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3 && r4 is Result<T4>.Ok_ ok4 && r5 is Result<T5>.Ok_ ok5)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3, r4, r5 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5)
            => Aggregate(r1, r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Func<T1, T2, T3, T4, T5, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, combine);
        }

        public static Result<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6) => 
            Aggregate(r1, r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6));

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3 && r4 is Result<T4>.Ok_ ok4 && r5 is Result<T5>.Ok_ ok5 && r6 is Result<T6>.Ok_ ok6)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3, r4, r5, r6 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6)
            => Aggregate(r1, r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Func<T1, T2, T3, T4, T5, T6, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, combine);
        }

        public static Result<(T1, T2, T3, T4, T5, T6, T7)> Aggregate<T1, T2, T3, T4, T5, T6, T7>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, (v1, v2, v3, v4, v5, v6, v7) => (v1, v2, v3, v4, v5, v6, v7));

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3 && r4 is Result<T4>.Ok_ ok4 && r5 is Result<T5>.Ok_ ok5 && r6 is Result<T6>.Ok_ ok6 && r7 is Result<T7>.Ok_ ok7)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3, r4, r5, r6, r7 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3, T4, T5, T6, T7)>> Aggregate<T1, T2, T3, T4, T5, T6, T7>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, (v1, v2, v3, v4, v5, v6, v7) => (v1, v2, v3, v4, v5, v6, v7));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Func<T1, T2, T3, T4, T5, T6, T7, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, combine);
        }

        public static Result<(T1, T2, T3, T4, T5, T6, T7, T8)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, (v1, v2, v3, v4, v5, v6, v7, v8) => (v1, v2, v3, v4, v5, v6, v7, v8));

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3 && r4 is Result<T4>.Ok_ ok4 && r5 is Result<T5>.Ok_ ok5 && r6 is Result<T6>.Ok_ ok6 && r7 is Result<T7>.Ok_ ok7 && r8 is Result<T8>.Ok_ ok8)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value, ok8.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3, r4, r5, r6, r7, r8 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3, T4, T5, T6, T7, T8)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, (v1, v2, v3, v4, v5, v6, v7, v8) => (v1, v2, v3, v4, v5, v6, v7, v8));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7, r8);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, r8.Result, combine);
        }

        public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8, Result<T9> r9) => 
            Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, (v1, v2, v3, v4, v5, v6, v7, v8, v9) => (v1, v2, v3, v4, v5, v6, v7, v8, v9));

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6, Result<T7> r7, Result<T8> r8, Result<T9> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine)            
        {
            if (r1 is Result<T1>.Ok_ ok1 && r2 is Result<T2>.Ok_ ok2 && r3 is Result<T3>.Ok_ ok3 && r4 is Result<T4>.Ok_ ok4 && r5 is Result<T5>.Ok_ ok5 && r6 is Result<T6>.Ok_ ok6 && r7 is Result<T7>.Ok_ ok7 && r8 is Result<T8>.Ok_ ok8 && r9 is Result<T9>.Ok_ ok9)
                return combine(ok1.Value, ok2.Value, ok3.Value, ok4.Value, ok5.Value, ok6.Value, ok7.Value, ok8.Value, ok9.Value);

            return Result.Error<TResult>(
                MergeErrors(new Result[] { r1, r2, r3, r4, r5, r6, r7, r8, r9 }
                    .Where(r => r.IsError)
                    .Select(r => r.GetErrorOrDefault()!)
                )!);
        }
        
        public static Task<Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8, Task<Result<T9>> r9)
            => Aggregate(r1, r2, r3, r4, r5, r6, r7, r8, r9, (v1, v2, v3, v4, v5, v6, v7, v8, v9) => (v1, v2, v3, v4, v5, v6, v7, v8, v9));

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6, Task<Result<T7>> r7, Task<Result<T8>> r8, Task<Result<T9>> r9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> combine)            
        {
            await Task.WhenAll(r1, r2, r3, r4, r5, r6, r7, r8, r9);
            return Aggregate(r1.Result, r2.Result, r3.Result, r4.Result, r5.Result, r6.Result, r7.Result, r8.Result, r9.Result, combine);
        }

        public static Result<T> FirstOk<T>(this IEnumerable<Result<T>> results, Func<String> onEmpty)
        {
            var errors = new List<String>();
            foreach (var result in results)
            {
                if (result is Result<T>.Error_ e)
                    errors.Add(e.Details);
                else
                    return result;
            }

            if (!errors.Any())
                errors.Add(onEmpty());

            return Result.Error<T>(MergeErrors(errors));
        }

        public static async Task<Result<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<Result<T>>> results,
            int maxDegreeOfParallelism)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .Aggregate();

        public static async Task<Result<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<Result<T>>>> results,
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

        public static Result<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<T> candidates, Func<T, IEnumerable<String>> validate) =>
            candidates
                .Select(c => c.Validate(validate))
                .Aggregate();

        public static Result<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<Result<T>> candidates,
            Func<T, IEnumerable<String>> validate) =>
            candidates
                .Bind(items => items.AllOk(validate));

        public static Result<T> Validate<T>(this Result<T> item, Func<T, IEnumerable<String>> validate) => item.Bind(i => i.Validate(validate));

        public static Result<T> Validate<T>(this T item, Func<T, IEnumerable<String>> validate)
        {
	        try
	        {
		        var errors = validate(item).ToList();
		        return errors.Count > 0 ? Result.Error<T>(MergeErrors(errors)) : item;
	        }
	        // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168 // Variable is declared but never used
	        catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
	        {
		        return Result.Error<T>(FunicularSwitch.Generators.Consumer.ErrorExtension.UnexpectedToStringError(e));
	        }
        }

        public static Result<T> FirstOk<T>(this IEnumerable<T> candidates, Func<T, IEnumerable<String>> validate, Func<String> onEmpty) =>
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