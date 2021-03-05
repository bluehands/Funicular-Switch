using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunicularSwitch.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// WhereAsync with unlimited possible parallelism.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> WhereAsync<T>(this IEnumerable<T> items, Func<T, Task<bool>> predicate)
        {
            var context = await items.SelectAsync(async item => (item, await predicate(item).ConfigureAwait(false))).ConfigureAwait(false);
            return context.Where(item => item.Item2).Select(item => item.item);
        }

        /// <summary>
        /// WhereAsync with limited possible parallelism.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> WhereAsync<T>(this IEnumerable<T> items, Func<T, Task<bool>> predicate, int maxDegreeOfParallelism)
        {
            var context = await items.SelectAsync( async item => (item, await predicate(item).ConfigureAwait(false), maxDegreeOfParallelism)).ConfigureAwait(false);
            return context.Where(item => item.Item2).Select(item => item.item);
        }

        /// <summary>
        /// WhereAsync keeping execution order. Parallelism is not possible.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> WhereAsyncSequential<T>(this IEnumerable<T> items, Func<T, Task<bool>> predicate)
        {
            var context = await items.SelectAsyncSequential(async item => (item, await predicate(item).ConfigureAwait(false))).ConfigureAwait(false);
            return context.Where(item => item.Item2).Select(item => item.item);
        }

        /// <summary>
        /// SelectAsync with unlimited possible parallelism.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Task<TOut[]> SelectAsync<T, TOut>(this IEnumerable<T> items, Func<T, Task<TOut>> selector)
        {
            return Task.WhenAll(items.Select(item => selector(item)));
        }

        /// <summary>
        /// SelectAsync with limited possible parallelism.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <param name="maxDegreeOfParallelism"></param>
        /// <returns></returns>
        public static async Task<TOut[]> SelectAsync<T, TOut>(this IEnumerable<T> items, Func<T, Task<TOut>> selector, int maxDegreeOfParallelism)
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

        /// <summary>
        /// SelectAsync keeping execution order. Parallelism is not possible.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<IReadOnlyCollection<TOut>> SelectAsyncSequential<T, TOut>(this IEnumerable<T> items, Func<T, Task<TOut>> selector)
        {
            var output = new List<TOut>();
            foreach (var item in items)
            {
                output.Add(await selector(item).ConfigureAwait(false));
            }
            return output;
        }

        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<TBase> Yield<T, TBase>(this T item) where TBase : T
        {
            yield return (TBase)item!;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T item, params T[] further)
        {
            foreach (var i in items)
                yield return i;

            yield return item;

            foreach (var i in further)
                yield return i;
        }
    }
}