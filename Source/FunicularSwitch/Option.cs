using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunicularSwitch.Extensions;

namespace FunicularSwitch
{
    public static class Option
    {
        public static Option<T> Some<T>(T value) => Option<T>.Some(value);
        public static Option<T> None<T>() => Option<T>.None;
        public static async Task<Option<T>> Some<T>(Task<T> value) => Some(await value);
        public static Task<Option<T>> NoneAsync<T>() => Task.FromResult(Option<T>.None);
    }

    public interface IOption
    {
        bool IsSome();
        bool IsNone();
    }

    public readonly struct Option<T> : IEnumerable<T>, IEquatable<Option<T>>, IOption
    {
        public static readonly Option<T> None = default;

        public static Option<T> Some(T value) => new(value);

        readonly bool _isSome;

        readonly T _value;

        Option(T value)
        {
            _isSome = true;
            _value = value;
        }

        public bool IsSome() => _isSome;

        public bool IsNone() => !_isSome;

        public Option<T1> Map<T1>(Func<T, T1> map) => Match(t => Option<T1>.Some(map(t)), Option<T1>.None);

        public Task<Option<T1>> Map<T1>(Func<T, Task<T1>> map) => Match(async t => Option<T1>.Some(await map(t).ConfigureAwait(false)), () => Task.FromResult(Option<T1>.None));

        public Option<T1> Bind<T1>(Func<T, Option<T1>> map) => Match(map, Option<T1>.None);

        public Task<Option<T1>> Bind<T1>(Func<T, Task<Option<T1>>> bind) => Match(bind, () => Option<T1>.None);

        public void Match(Action<T> some, Action? none = null)
        {
            Match(some.ToFunc(), none?.ToFunc<int>() ?? (() => 42));
        }

        public async Task Match(Func<T, Task> some, Func<Task>? none = null)
        {
            if (_isSome)
            {
                await some(_value).ConfigureAwait(false);
            }
            else if (none != null)
            {
                await none().ConfigureAwait(false);
            }
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => _isSome ? some(_value) : none();

        public TResult Match<TResult>(Func<T, TResult> some, TResult none) => _isSome ? some(_value) : none;

        public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, Func<Task<TResult>> none)
        {
            if (_isSome)
            {
                return await some(_value).ConfigureAwait(false);
            }

            return await none().ConfigureAwait(false);
        }

        public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, Func<TResult> none)
        {
            if (_isSome)
            {
                return await some(_value).ConfigureAwait(false);
            }

            return none();
        }

        public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, TResult none)
        {
            if (_isSome)
            {
                return await some(_value).ConfigureAwait(false);
            }

            return none;
        }

        public static implicit operator Option<T>(T value) => Some(value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => Match(v => new[] { v }, Enumerable.Empty<T>).GetEnumerator();

        public T? GetValueOrDefault() => Match(v => (T?)v, () => default);

        public T GetValueOrDefault(Func<T> defaultValue) => Match(v => v, defaultValue);

        public T GetValueOrDefault(T defaultValue) => Match(v => v, () => defaultValue);

        public T GetValueOrThrow(string? errorMessage = null) => Match(v => v, () => throw new InvalidOperationException(errorMessage ?? "Cannot access value of none option"));

        public Option<TOther> Convert<TOther>() => Match(s => Option<TOther>.Some((TOther)(object)s!), Option<TOther>.None);

        public override string ToString() => Match(v => v?.ToString() ?? "", () => $"None {typeof(T).BeautifulName()}");

        public bool Equals(Option<T> other) => _isSome == other._isSome && EqualityComparer<T>.Default.Equals(_value, other._value);

        public override bool Equals(object? obj) => obj is Option<T> other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _isSome.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(_value);
                hashCode = (hashCode * 397) ^ typeof(T).GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

        public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);
    }

    public static class OptionExtension
    {
        public static Option<T> Flatten<T>(this Option<Option<T>> option)
        {
            return option.Match(s => s, () => Option<T>.None);
        }

        public static Option<T> ToOption<T>(this T? item) where T : class => item ?? Option<T>.None;

        public static Option<T> ToOption<T>(this T? item) where T : struct => item.HasValue ? Option.Some(item.Value) : Option<T>.None;
        public static T? ToNullable<T>(this Option<T> option) where T : struct => option.Match(some => some, () => (T?)null);

        public static Option<TTarget> As<TTarget>(this object item) where TTarget : class => (item as TTarget).ToOption();

        public static async Task<TOut> Match<T, TOut>(this Task<Option<T>> option, Func<T, TOut> some, Func<TOut> none)
        {
            var result = await option.ConfigureAwait(false);
            return result.Match(some, none);
        }

        public static async Task<Option<TOut>> Map<T, TOut>(this Task<Option<T>> map, Func<T, Task<TOut>> convert)
        {
            var result = await map.ConfigureAwait(false);
            return await result.Map(convert).ConfigureAwait(false);
        }

        public static async Task<Option<TOut>> Map<T, TOut>(this Task<Option<T>> map, Func<T, TOut> convert)
        {
            var result = await map.ConfigureAwait(false);
            return result.Map(convert);
        }

        public static async Task<Option<TOut>> Bind<T, TOut>(this Task<Option<T>> bind, Func<T, Option<TOut>> convert)
        {
            var result = await bind.ConfigureAwait(false);
            return result.Bind(convert);
        }

        public static async Task<Option<TOut>> Bind<T, TOut>(this Task<Option<T>> bind, Func<T, Task<Option<TOut>>> convert)
        {
            var result = await bind.ConfigureAwait(false);
            return await result.Bind(convert).ConfigureAwait(false);
        }

        public static IEnumerable<TOut> Choose<T, TOut>(this IEnumerable<T> items, Func<T, Option<TOut>> choose) => items.SelectMany(i => choose(i));

        public static Option<T> ToOption<T>(this Result<T> result) => ToOption(result, null);

        public static Option<T> ToOption<T>(this Result<T> result, Action<string>? logError) =>
            result.Match(
                ok => Option.Some(ok),
                error =>
                {
                    logError?.Invoke(error);
                    return Option<T>.None;
                });

        public static Result<T> ToResult<T>(this Option<T> option, Func<string> errorIfNone) =>
            option.Match(s => Result.Ok(s), () => Result.Error<T>(errorIfNone()));

        #region query-expression pattern

        public static Option<T1> Select<T, T1>(this Option<T> result, Func<T, T1> selector) => result.Map(selector);
        public static Task<Option<T1>> Select<T, T1>(this Task<Option<T>> result, Func<T, T1> selector) => result.Map(selector);

        public static Option<T2> SelectMany<T, T1, T2>(this Option<T> result, Func<T, Option<T1>> selector, Func<T, T1, T2> resultSelector) =>
            result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        public static Task<Option<T2>> SelectMany<T, T1, T2>(this Task<Option<T>> result, Func<T, Task<Option<T1>>> selector, Func<T, T1, T2> resultSelector) =>
            result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        public static Task<Option<T2>> SelectMany<T, T1, T2>(this Task<Option<T>> result, Func<T, Option<T1>> selector, Func<T, T1, T2> resultSelector) =>
            result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        public static Task<Option<T2>> SelectMany<T, T1, T2>(this Option<T> result, Func<T, Task<Option<T1>>> selector, Func<T, T1, T2> resultSelector) =>
            result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        #endregion
    }
}