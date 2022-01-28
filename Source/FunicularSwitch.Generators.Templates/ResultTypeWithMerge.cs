#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//additional using directives

namespace FunicularSwitch.Generators.Templates
{
#pragma warning disable 1591
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
            var isError = false;
            MyError aggregated = default!;
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
                if (result is MyResult<T>.Error_ e)
                    errors.Add(e.Details);
                else
                    return result;
            }

            if (!errors.Any())
                errors.Add(onEmpty());

            return MyResult.Error<T>(MergeErrors(errors));
        }

        public static async Task<MyResult<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<MyResult<T>>> results,
            int maxDegreeOfParallelism)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .Aggregate();

        public static async Task<MyResult<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<MyResult<T>>>> results,
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

        public static MyResult<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<T> candidates, Func<T, IEnumerable<MyError>> validate) =>
            candidates
                .Select(c => c.Validate(validate))
                .Aggregate();

        public static MyResult<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<MyResult<T>> candidates,
            Func<T, IEnumerable<MyError>> validate) =>
            candidates
                .Bind(items => items.AllOk(validate));

        public static MyResult<T> Validate<T>(this MyResult<T> item, Func<T, IEnumerable<MyError>> validate) => item.Bind(i => i.Validate(validate));

        public static MyResult<T> Validate<T>(this T item, Func<T, IEnumerable<MyError>> validate)
        {
            var errors = validate(item).ToList();
            return errors.Count > 0 ? MyResult.Error<T>(MergeErrors(errors)) : item;
        }

        public static MyResult<T> FirstOk<T>(this IEnumerable<T> candidates, Func<T, IEnumerable<MyError>> validate, Func<MyError> onEmpty) =>
            candidates
                .Select(r => r.Validate(validate))
                .FirstOk(onEmpty);

        #region helpers

        static MyError MergeErrors(IEnumerable<MyError> errors)
        {
            var first = true;
            MyError aggregated = default!;
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

        static MyError MergeErrors(MyError aggregated, MyError error) => aggregated.Merge__MemberOrExtensionMethod(error);

        #endregion
    }
#pragma warning restore 1591
}