using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunicularSwitch.Extensions;

namespace FunicularSwitch
{
    public abstract class Option
    {
        public static Option<T> Some<T>(T value) => new Some<T>(value);
        public static Option<T> None<T>() => Option<T>.None;
    }

    public abstract class Option<T> : Option, IEnumerable<T>
    {
#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
        public new static readonly Option<T> None = new None<T>();
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required

        public bool IsSome() => GetType() == typeof(Some<T>);

        public bool IsNone() => !IsSome();

        public Option<T1> Map<T1>(Func<T, T1> map) => Match(t => Some(map(t)), None<T1>);

        public Task<Option<T1>> Map<T1>(Func<T, Task<T1>> map) => Match(async t => Some(await map(t).ConfigureAwait(false)), () => Task.FromResult(None<T1>()));

        public Option<T1> Bind<T1>(Func<T, Option<T1>> map) => Match(map, None<T1>);

        public Task<Option<T1>> Bind<T1>(Func<T, Task<Option<T1>>> bind) => Match(bind, () => None<T1>());

        public void Match(Action<T> some, Action? none = null)
        {
            Match(some.ToFunc(), none?.ToFunc<int>() ?? (() => 42));
        }

        public async Task Match(Func<T, Task> some, Func<Task>? none = null)
        {
            var iAmSome = this as Some<T>;
            if (iAmSome != null)
            {
                await some(iAmSome.Value).ConfigureAwait(false);
            }
            else if (none != null)
            {
                await none().ConfigureAwait(false);
            }
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            var iAmSome = this as Some<T>;
            return iAmSome != null ? some(iAmSome.Value) : none();
        }

        public TResult Match<TResult>(Func<T, TResult> some, TResult none)
        {
            var iAmSome = this as Some<T>;
            return iAmSome != null ? some(iAmSome.Value) : none;
        }

        public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, Func<Task<TResult>> none)
        {
            var iAmSome = this as Some<T>;
            if (iAmSome != null)
            {
                return await some(iAmSome.Value).ConfigureAwait(false);
            }
            return await none().ConfigureAwait(false);
        }

        public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, Func<TResult> none)
        {
            var iAmSome = this as Some<T>;
            if (iAmSome != null)
            {
                return await some(iAmSome.Value).ConfigureAwait(false);
            }
            return none();
        }

        public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, TResult none)
        {
            var iAmSome = this as Some<T>;
            if (iAmSome != null)
            {
                return await some(iAmSome.Value).ConfigureAwait(false);
            }
            return none;
        }

        public static implicit operator Option<T>(T value) => Some(value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => Match(v => new[] { v }, Enumerable.Empty<T>).GetEnumerator();

        public T? GetValueOrDefault() => Match(v => (T?)v, () => default);

        public T GetValueOrDefault(Func<T> defaultValue) => Match(v => v, () => defaultValue());

        public T GetValueOrDefault(T defaultValue) => Match(v => v, () => defaultValue);

        public T GetValueOrThrow(string? errorMessage = null) => Match(v => v, () => throw new InvalidOperationException(errorMessage ?? "Cannot access value of none option"));

        public Option<TOther> Convert<TOther>() => Match(s => Some((TOther) (object)s!), None<TOther>);

        public override string ToString() => Match(v => v?.ToString() ?? "", () => $"None {GetType().BeautifulName()}");
    }

    public sealed class Some<T> : Option<T>
    {
        public T Value { get; }

        public Some(T value) => Value = value;

        bool Equals(Some<T> other) => EqualityComparer<T>.Default.Equals(Value, other.Value);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Some<T>)obj);
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(Some<T>? left, Some<T>? right) => Equals(left, right);

        public static bool operator !=(Some<T>? left, Some<T>? right) => !Equals(left, right);
    }

    public sealed class None<T> : Option<T>
    {
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType();
        }

        public override int GetHashCode() => typeof(None<T>).GetHashCode();

        public static bool operator ==(None<T> left, None<T> right) => Equals(left, right);

        public static bool operator !=(None<T> left, None<T> right) => !Equals(left, right);
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
    }
}