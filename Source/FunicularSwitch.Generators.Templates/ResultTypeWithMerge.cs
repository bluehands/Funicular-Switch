#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//additional using directives

namespace FunicularSwitch.Generators.Templates
{
    public abstract partial class MyResult
    {
        //generated aggregate methods
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
        
        public static MyResult<IReadOnlyCollection<T>> Aggregate<T>(this IEnumerable<MyResult<T>> results)
        {
            MyError? aggregated = default;
            var oks = new List<T>();
            foreach (var result in results)
            {
                result.Match(
                    ok => oks.Add(ok),
                    error => { aggregated = aggregated == null ? error : MergeErrors(aggregated, error); }
                );
            }

            return aggregated != null
                ? MyResult.Error<IReadOnlyCollection<T>>(aggregated)
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

        //generated aggregate extension methods

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
                    aggregated = MergeErrors(aggregated!, myError);
                }
            }

            return aggregated;
        }

        static MyError MergeErrors(MyError aggregated, MyError error) => aggregated.Merge(error);

        #endregion
    }
}