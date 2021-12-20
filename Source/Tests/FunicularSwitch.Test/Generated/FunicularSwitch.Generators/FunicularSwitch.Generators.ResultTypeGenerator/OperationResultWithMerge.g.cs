using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunicularSwitch.Test

{
    public abstract partial class OperationResult
    {
        public static OperationResult<(T1, T2)> Aggregate<T1, T2>(OperationResult<T1> r1, OperationResult<T2> r2) => r1.Aggregate(r2);

        public static OperationResult<(T1, T2, T3)> Aggregate<T1, T2, T3>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3) =>
            r1.Aggregate(r2, r3);

        //public static OperationResult<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4) => r1.Aggregate(r2, r3, r4);

        //public static OperationResult<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5) => r1.Aggregate(r2, r3, r4, r5);

        //public static OperationResult<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(OperationResult<T1> r1, OperationResult<T2> r2, OperationResult<T3> r3, OperationResult<T4> r4, OperationResult<T5> r5, OperationResult<T6> r6) => r1.Aggregate(r2, r3, r4, r5, r6);

        public static Task<OperationResult<(T1, T2)>> Aggregate<T1, T2>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2) =>
            r1.Aggregate(r2);

        public static Task<OperationResult<(T1, T2, T3)>> Aggregate<T1, T2, T3>(Task<OperationResult<T1>> r1, Task<OperationResult<T2>> r2,
            Task<OperationResult<T3>> r3) => r1.Aggregate(r2, r3);

        //public static global::System.Threading.Tasks.Task<OperationResult<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(global::System.Threading.Tasks.Task<OperationResult<T1>> r1, global::System.Threading.Tasks.Task<OperationResult<T2>> r2, global::System.Threading.Tasks.Task<OperationResult<T3>> r3, global::System.Threading.Tasks.Task<OperationResult<T4>> r4) => r1.Aggregate(r2, r3, r4);

        //public static global::System.Threading.Tasks.Task<OperationResult<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(global::System.Threading.Tasks.Task<OperationResult<T1>> r1, global::System.Threading.Tasks.Task<OperationResult<T2>> r2, global::System.Threading.Tasks.Task<OperationResult<T3>> r3, global::System.Threading.Tasks.Task<OperationResult<T4>> r4, global::System.Threading.Tasks.Task<OperationResult<T5>> r5) => r1.Aggregate(r2, r3, r4, r5);

        //public static global::System.Threading.Tasks.Task<OperationResult<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(global::System.Threading.Tasks.Task<OperationResult<T1>> r1, global::System.Threading.Tasks.Task<OperationResult<T2>> r2, global::System.Threading.Tasks.Task<OperationResult<T3>> r3, global::System.Threading.Tasks.Task<OperationResult<T4>> r4, global::System.Threading.Tasks.Task<OperationResult<T5>> r5, global::System.Threading.Tasks.Task<OperationResult<T6>> r6) => r1.Aggregate(r2, r3, r4, r5, r6);
    }

    public static partial class OperationResultExtension
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

        public static OperationResult<(T1, T2)> Aggregate<T1, T2>(
            this OperationResult<T1> r1,
            OperationResult<T2> r2)
            => r1.Aggregate(r2, (v1, v2) => (v1, v2));

        public static OperationResult<TResult> Aggregate<T1, T2, TResult>(
            this OperationResult<T1> r1,
            OperationResult<T2> r2,
            Func<T1, T2, TResult> combine) =>
            r1.Match(
                ok1 => r2.Match(
                    ok2 => combine(ok1, ok2),
                    error2 => OperationResult.Error<TResult>(error2)
                ),
                error1 => r2.Match(
                    _ => OperationResult.Error<TResult>(error1),
                    error2 => OperationResult.Error<TResult>(error1.Merge(error2))
                )
            );

        public static OperationResult<(T1, T2, T3)> Aggregate<T1, T2, T3>(
            this OperationResult<T1> r1,
            OperationResult<T2> r2,
            OperationResult<T3> r3)
            => r1.Aggregate(r2, r3, (v1, v2, v3) => (v1, v2, v3));

        public static OperationResult<TResult> Aggregate<T1, T2, T3, TResult>(
            this OperationResult<T1> r1,
            OperationResult<T2> r2,
            OperationResult<T3> r3,
            Func<T1, T2, T3, TResult> combine)
        {
            var combined = r1.Bind(ok1 => r2.Bind(ok2 => r3.Map(ok3 => combine(ok1, ok2, ok3))));
            return combined.Match(
                ok: _ => _,
                error: _ => OperationResult.Error<TResult>(
                    MergeErrors(new OperationResult[] { r1, r2, r3 }
                        .Where(r => r.IsError)
                        .Select(r => r.GetErrorOrDefault()!)
                    )!
                )
            );
        }

        public static Task<OperationResult<(T1, T2)>> Aggregate<T1, T2>(
            this Task<OperationResult<T1>> tr1,
            Task<OperationResult<T2>> tr2)
            => tr1.Aggregate(tr2, (v1, v2) => (v1, v2));

        public static async Task<OperationResult<TOperationResult>> Aggregate<T1, T2, TOperationResult>(
            this Task<OperationResult<T1>> tr1,
            Task<OperationResult<T2>> tr2,
            Func<T1, T2, TOperationResult> combine)
        {
            await Task.WhenAll(tr1, tr2);
            return tr1.Result.Aggregate(tr2.Result, combine);
        }

        public static Task<OperationResult<(T1, T2, T3)>> Aggregate<T1, T2, T3>(
            this Task<OperationResult<T1>> tr1,
            Task<OperationResult<T2>> tr2,
            Task<OperationResult<T3>> tr3)
            => tr1.Aggregate(tr2, tr3, (v1, v2, v3) => (v1, v2, v3));

        public static async Task<OperationResult<TOperationResult>> Aggregate<T1, T2, T3, TOperationResult>(
            this Task<OperationResult<T1>> tr1,
            Task<OperationResult<T2>> tr2,
            Task<OperationResult<T3>> tr3,
            Func<T1, T2, T3, TOperationResult> combine)
        {
            await Task.WhenAll(tr1, tr2, tr3);
            return tr1.Result.Aggregate(tr2.Result, tr3.Result, combine);
        }

        public static OperationResult<IReadOnlyCollection<T>> Aggregate<T>(
            this IEnumerable<OperationResult<T>> results)
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

        //depends on merge

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