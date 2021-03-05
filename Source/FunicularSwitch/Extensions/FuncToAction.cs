using System;
using System.Threading.Tasks;

namespace FunicularSwitch.Extensions
{
    public static class FuncToAction
    {
        public static Action<T1> IgnoreReturn<T1, T2>(this Func<T1, T2> func)
        {
            return t => { func(t); };
        }

        public static Action IgnoreReturn<T>(this Func<T> func)
        {
            return () => { func(); };
        }

        public static Func<T?> ToFunc<T>(this Action action) =>
            () =>
            {
                action();
                return default;
            };

        public static Func<T, int> ToFunc<T>(this Action<T> action)
        {
            return t =>
            {
                action(t);
                return 42;
            };
        }

        public static Func<Task<T?>> ToFunc<T>(this Func<Task> action)
        {
            return async () =>
            {
                await action().ConfigureAwait(false);
                return default;
            };
        }

        public static Func<T, Task<int>> ToFunc<T>(this Func<T, Task> action)
        {
            return async t =>
            {
                await action(t).ConfigureAwait(false);
                return 42;
            };
        }
    }
}