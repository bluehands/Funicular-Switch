﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Templates
{
    public abstract partial class MyResult
    {
        //public static MyResult<(T1, T2)> Aggregate<T1, T2>(MyResult<T1> r1, MyResult<T2> r2) => r1.Aggregate(r2);

        //public static MyResult<(T1, T2, T3)> Aggregate<T1, T2, T3>(MyResult<T1> r1, MyResult<T2> r2, MyResult<T3> r3) => r1.Aggregate(r2, r3);

        //public static MyResult<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(MyResult<T1> r1, MyResult<T2> r2, MyResult<T3> r3, MyResult<T4> r4) => r1.Aggregate(r2, r3, r4);

        //public static MyResult<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(MyResult<T1> r1, MyResult<T2> r2, MyResult<T3> r3, MyResult<T4> r4, MyResult<T5> r5) => r1.Aggregate(r2, r3, r4, r5);

        //public static MyResult<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(MyResult<T1> r1, MyResult<T2> r2, MyResult<T3> r3, MyResult<T4> r4, MyResult<T5> r5, MyResult<T6> r6) => r1.Aggregate(r2, r3, r4, r5, r6);

        // public static Task<MyResult<(T1, T2)>> Aggregate<T1, T2>(Task<MyResult<T1>> r1, Task<MyResult<T2>> r2) =>r1.Aggregate(r2);

        //public static Task<MyResult<(T1, T2, T3)>> Aggregate<T1, T2, T3>(Task<MyResult<T1>> r1, Task<MyResult<T2>> r2,Task<MyResult<T3>> r3) => r1.Aggregate(r2, r3);

        //public static global::System.Threading.Tasks.Task<MyResult<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(global::System.Threading.Tasks.Task<MyResult<T1>> r1, global::System.Threading.Tasks.Task<MyResult<T2>> r2, global::System.Threading.Tasks.Task<MyResult<T3>> r3, global::System.Threading.Tasks.Task<MyResult<T4>> r4) => r1.Aggregate(r2, r3, r4);

        //public static global::System.Threading.Tasks.Task<MyResult<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(global::System.Threading.Tasks.Task<MyResult<T1>> r1, global::System.Threading.Tasks.Task<MyResult<T2>> r2, global::System.Threading.Tasks.Task<MyResult<T3>> r3, global::System.Threading.Tasks.Task<MyResult<T4>> r4, global::System.Threading.Tasks.Task<MyResult<T5>> r5) => r1.Aggregate(r2, r3, r4, r5);

        //public static global::System.Threading.Tasks.Task<MyResult<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(global::System.Threading.Tasks.Task<MyResult<T1>> r1, global::System.Threading.Tasks.Task<MyResult<T2>> r2, global::System.Threading.Tasks.Task<MyResult<T3>> r3, global::System.Threading.Tasks.Task<MyResult<T4>> r4, global::System.Threading.Tasks.Task<MyResult<T5>> r5, global::System.Threading.Tasks.Task<MyResult<T6>> r6) => r1.Aggregate(r2, r3, r4, r5, r6);
    }

    public static partial class MyResultExtension
    {
        public static MyResult<IReadOnlyCollection<T1>> Map<T, T1>(this IEnumerable<MyResult<T>> results,
            Func<T, T1> map) =>
            results.Select(r => r.Map(map)).Aggregate();

        public static MyResult<IReadOnlyCollection<T1>> Bind<T, T1>(this IEnumerable<MyResult<T>> results,
            Func<T, MyResult<T1>> bind) =>
            results.Select(r => r.Bind(bind)).Aggregate();

        public static MyResult<IReadOnlyCollection<T1>> Bind<T, T1>(this MyResult<T> result,
            Func<T, IEnumerable<MyResult<T1>>> bindMany) =>
            result.Map(ok => bindMany(ok).Aggregate()).Flatten();

        public static MyResult<T1> Bind<T, T1>(this IEnumerable<MyResult<T>> results,
            Func<IEnumerable<T>, MyResult<T1>> bind) =>
            results.Aggregate().Bind(bind);
        
        //generated aggregate methods

        public static MyResult<IReadOnlyCollection<T>> Aggregate<T>(this IEnumerable<MyResult<T>> results)
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
                ? MyResult.Error<IReadOnlyCollection<T>>(errors)
                : MyResult.Ok<IReadOnlyCollection<T>>(oks);
        }

        public static async Task<MyResult<IReadOnlyCollection<T>>> Aggregate<T>(
            this Task<IEnumerable<MyResult<T>>> results)
            => (await results.ConfigureAwait(false))
                .Aggregate();

        public static async Task<MyResult<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<MyResult<T>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .Aggregate();

        public static async Task<MyResult<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<MyResult<T>>>> results)
            => (await Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        public static MyResult<T> FirstOk<T>(this IEnumerable<MyResult<T>> results, Func<MyError> onEmpty)
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

            return MyResult.Error<T>(MergeErrors(errors)!);
        }

        //depends on FunicularSwitch and merge

        //public static async Task<MyResult<IReadOnlyCollection<T>>> Aggregate<T>(
        //    this IEnumerable<Task<MyResult<T>>> results,
        //    int maxDegreeOfParallelism)
        //    => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
        //        .Aggregate();

        //public static async Task<MyResult<IReadOnlyCollection<T>>> AggregateMany<T>(
        //    this IEnumerable<Task<IEnumerable<MyResult<T>>>> results,
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