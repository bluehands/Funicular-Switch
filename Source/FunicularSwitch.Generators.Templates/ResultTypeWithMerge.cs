#nullable enable
using global::System.Linq;
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
        public static MyResult<global::System.Collections.Generic.IReadOnlyCollection<T1>> Map<T, T1>(this global::System.Collections.Generic.IEnumerable<MyResult<T>> results,
            global::System.Func<T, T1> map) =>
            results.Select(r => r.Map(map)).Aggregate();

        public static MyResult<global::System.Collections.Generic.IReadOnlyCollection<T1>> Bind<T, T1>(this global::System.Collections.Generic.IEnumerable<MyResult<T>> results,
            global::System.Func<T, MyResult<T1>> bind) =>
            results.Select(r => r.Bind(bind)).Aggregate();

        public static MyResult<global::System.Collections.Generic.IReadOnlyCollection<T1>> Bind<T, T1>(this MyResult<T> result,
            global::System.Func<T, global::System.Collections.Generic.IEnumerable<MyResult<T1>>> bindMany) =>
            result.Map(ok => bindMany(ok).Aggregate()).Flatten();

        public static MyResult<T1> Bind<T, T1>(this global::System.Collections.Generic.IEnumerable<MyResult<T>> results,
            global::System.Func<global::System.Collections.Generic.IEnumerable<T>, MyResult<T1>> bind) =>
            results.Aggregate().Bind(bind);
        
        public static MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>> Aggregate<T>(this global::System.Collections.Generic.IEnumerable<MyResult<T>> results)
        {
            var isError = false;
            MyError aggregated = default!;
            var oks = new global::System.Collections.Generic.List<T>();
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
                ? MyResult.Error<global::System.Collections.Generic.IReadOnlyCollection<T>>(aggregated)
                : MyResult.Ok<global::System.Collections.Generic.IReadOnlyCollection<T>>(oks);
        }

        public static async global::System.Threading.Tasks.Task<MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>>> Aggregate<T>(
            this global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<MyResult<T>>> results)
            => (await results.ConfigureAwait(false))
                .Aggregate();

        public static async global::System.Threading.Tasks.Task<MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>>> Aggregate<T>(
            this global::System.Collections.Generic.IEnumerable<global::System.Threading.Tasks.Task<MyResult<T>>> results)
            => (await global::System.Threading.Tasks.Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .Aggregate();

        public static async global::System.Threading.Tasks.Task<MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>>> AggregateMany<T>(
            this global::System.Collections.Generic.IEnumerable<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<MyResult<T>>>> results)
            => (await global::System.Threading.Tasks.Task.WhenAll(results.Select(e => e)).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        //generated aggregate extension methods

        public static MyResult<T> FirstOk<T>(this global::System.Collections.Generic.IEnumerable<MyResult<T>> results, global::System.Func<MyError> onEmpty)
        {
            var errors = new global::System.Collections.Generic.List<MyError>();
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

        public static async global::System.Threading.Tasks.Task<MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>>> Aggregate<T>(
            this global::System.Collections.Generic.IEnumerable<global::System.Threading.Tasks.Task<MyResult<T>>> results,
            int maxDegreeOfParallelism)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .Aggregate();

        public static async global::System.Threading.Tasks.Task<MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>>> AggregateMany<T>(
            this global::System.Collections.Generic.IEnumerable<global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<MyResult<T>>>> results,
            int maxDegreeOfParallelism)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate();

        static async global::System.Threading.Tasks.Task<TOut[]> SelectAsync<T, TOut>(this global::System.Collections.Generic.IEnumerable<T> items, global::System.Func<T, global::System.Threading.Tasks.Task<TOut>> selector, int maxDegreeOfParallelism)
        {
            using (var throttler = new global::System.Threading.SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism))
            {
                return await global::System.Threading.Tasks.Task.WhenAll(items.Select(async item =>
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

        public static MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>> AllOk<T>(this global::System.Collections.Generic.IEnumerable<T> candidates, global::System.Func<T, global::System.Collections.Generic.IEnumerable<MyError>> validate) =>
            candidates
                .Select(c => c.Validate(validate))
                .Aggregate();

        public static MyResult<global::System.Collections.Generic.IReadOnlyCollection<T>> AllOk<T>(this global::System.Collections.Generic.IEnumerable<MyResult<T>> candidates,
            global::System.Func<T, global::System.Collections.Generic.IEnumerable<MyError>> validate) =>
            candidates
                .Bind(items => items.AllOk(validate));

        public static MyResult<T> Validate<T>(this MyResult<T> item, global::System.Func<T, global::System.Collections.Generic.IEnumerable<MyError>> validate) => item.Bind(i => i.Validate(validate));

        public static MyResult<T> Validate<T>(this T item, global::System.Func<T, global::System.Collections.Generic.IEnumerable<MyError>> validate)
        {
	        try
	        {
		        var errors = validate(item).ToList();
		        return errors.Count > 0 ? MyResult.Error<T>(MergeErrors(errors)) : item;
	        }
	        // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168 // Variable is declared but never used
	        catch (global::System.Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
	        {
		        throw; //createGenericErrorResult
	        }
        }

        public static MyResult<T> FirstOk<T>(this global::System.Collections.Generic.IEnumerable<T> candidates, global::System.Func<T, global::System.Collections.Generic.IEnumerable<MyError>> validate, global::System.Func<MyError> onEmpty) =>
            candidates
                .Select(r => r.Validate(validate))
                .FirstOk(onEmpty);

        #region helpers

        static MyError MergeErrors(global::System.Collections.Generic.IEnumerable<MyError> errors)
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