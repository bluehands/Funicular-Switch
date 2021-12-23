#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//additional using directives

namespace FunicularSwitch.Generators.Templates
{
    public abstract partial class MyResult
    {
        public static MyResult<T> Error<T>(MyError details) => new MyResult<T>.Error_(details);
        public static MyResult<T> Ok<T>(T value) => new MyResult<T>.Ok_(value);
        public bool IsError => GetType().GetGenericTypeDefinition() == typeof(MyResult<>.Error_);
        public bool IsOk => !IsError;
        public abstract MyError? GetErrorOrDefault();

        public static MyResult<T> Try<T>(Func<T> action, Func<Exception, MyError> formatError)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                return Error<T>(formatError(e));
            }
        }

        public static async Task<MyResult<T>> Try<T>(Func<Task<T>> action, Func<Exception, MyError> formatError)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                return Error<T>(formatError(e));
            }
        }

        public static MyResult<Unit> Try(Action action, Func<Exception, MyError> formatError)
        {
            try
            {
                action();
                return No.Thing;
            }
            catch (Exception e)
            {
                return Error<Unit>(formatError(e));
            }
        }

        public static async Task<MyResult<Unit>> Try(Func<Task> action, Func<Exception, MyError> formatError)
        {
            try
            {
                await action();
                return No.Thing;
            }
            catch (Exception e)
            {
                return Error<Unit>(formatError(e));
            }
        }
    }

    public abstract partial class MyResult<T> : MyResult, IEnumerable<T>
    {
        public static MyResult<T> Error(MyError message) => Error<T>(message);
        public static MyResult<T> Ok(T value) => Ok<T>(value);

        public static implicit operator MyResult<T>(T value) => MyResult.Ok(value);

        public static bool operator true(MyResult<T> result) => result.IsOk;
        public static bool operator false(MyResult<T> result) => result.IsError;

        public static bool operator !(MyResult<T> result) => result.IsError;

        public void Match(Action<T> ok, Action<MyError>? error = null) => Match(
            v =>
            {
                ok.Invoke(v);
                return 42;
            },
            err =>
            {
                error?.Invoke(err);
                return 42;
            });
        public T1 Match<T1>(Func<T, T1> ok, Func<MyError, T1> error)
        {
            return this switch
            {
                Ok_ okMyResult => ok(okMyResult.Value),
                Error_ errorMyResult => error(errorMyResult.Details),
                _ => throw new InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }
        public async Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<MyError, Task<T1>> error)
        {
            return this switch
            {
                Ok_ okMyResult => await ok(okMyResult.Value).ConfigureAwait(false),
                Error_ errorMyResult => await error(errorMyResult.Details).ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }
        public Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<MyError, T1> error) => Match(ok, e => Task.FromResult(error(e)));
        public async Task Match(Func<T, Task> ok)
        {
            if (this is Ok_ okMyResult) await ok(okMyResult.Value).ConfigureAwait(false);
        }
        public T Match(Func<MyError, T> error) => Match(v => v, error);

        public MyResult<T1> Bind<T1>(Func<T, MyResult<T1>> bind)
        {
            switch (this)
            {
                case Ok_ ok:
                    return bind(ok.Value);
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }
        public async Task<MyResult<T1>> Bind<T1>(Func<T, Task<MyResult<T1>>> bind)
        {
            switch (this)
            {
                case Ok_ ok:
                    return await bind(ok.Value).ConfigureAwait(false);
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        public MyResult<T1> Map<T1>(Func<T, T1> map)
            => Bind(value => Ok(map(value)));

        public Task<MyResult<T1>> Map<T1>(Func<T, Task<T1>> map)
            => Bind(async value => Ok(await map(value).ConfigureAwait(false)));

        public T? GetValueOrDefault(Func<T>? defaultValue = null)
            => Match(
                v => v,
                _ => defaultValue != null ? defaultValue() : default);

        public T GetValueOrThrow()
            => Match(
                v => v,
                details => throw new InvalidOperationException($"Cannot access error result value. Error: {details}"));

        public IEnumerator<T> GetEnumerator() => Match(ok => new[] { ok }, _ => Enumerable.Empty<T>()).GetEnumerator();

        public override string ToString() => Match(ok => $"Ok {ok?.ToString()}", error => $"Error {error}");
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public sealed partial class Ok_ : MyResult<T>
        {
            public T Value { get; }

            public Ok_(T value) => Value = value;

            public override MyError? GetErrorOrDefault() => null;

            public bool Equals(Ok_? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return EqualityComparer<T>.Default.Equals(Value, other.Value);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is Ok_ other && Equals(other);
            }

            public override int GetHashCode() => Value == null ? 0 : EqualityComparer<T>.Default.GetHashCode(Value);

            public static bool operator ==(Ok_ left, Ok_ right) => Equals(left, right);

            public static bool operator !=(Ok_ left, Ok_ right) => !Equals(left, right);
        }

        public sealed partial class Error_ : MyResult<T>
        {
            public MyError Details { get; }

            public Error_(MyError details) => Details = details;

            public MyResult<T1>.Error_ Convert<T1>() => new MyResult<T1>.Error_(Details);

            public override MyError? GetErrorOrDefault() => Details;

            public bool Equals(Error_? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(Details, other.Details);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is Error_ other && Equals(other);
            }

            public override int GetHashCode() => Details.GetHashCode();

            public static bool operator ==(Error_ left, Error_ right) => Equals(left, right);

            public static bool operator !=(Error_ left, Error_ right) => !Equals(left, right);
        }

    }

    public static partial class MyResultExtension
    {
        #region bind

        public static async Task<MyResult<T1>> Bind<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, MyResult<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        public static async Task<MyResult<T1>> Bind<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<MyResult<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        public static async Task<MyResult<T1>> Map<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        public static Task<MyResult<T1>> Map<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<T1>> bind)
            => Bind(result, async v => MyResult.Ok(await bind(v).ConfigureAwait(false)));

        public static MyResult<T> MapError<T>(this MyResult<T> result, Func<MyError, MyError> mapError) =>
            result.Match(ok => ok, error => MyResult.Error<T>(mapError(error)));

        #endregion

        #region match
        public static async Task<T1> Match<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<T1>> ok,
            Func<MyError, Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, Task<T1>> ok,
            Func<MyError, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<MyResult<T>> result,
            Func<T, T1> ok,
            Func<MyError, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);
        #endregion

        #region choose

        public static IEnumerable<T1> Choose<T, T1>(
            this IEnumerable<T> items,
            Func<T, MyResult<T1>> choose,
            Action<MyError> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static IEnumerable<T> Choose<T>(
            this IEnumerable<MyResult<T>> results,
            Action<MyError> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        #endregion

        public static MyResult<T> Flatten<T>(this MyResult<MyResult<T>> result) => result.Bind(r => r);

        public static MyResult<T1> As<T, T1>(this MyResult<T> result, Func<MyError> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return MyResult.Error<T1>(errorTIsNotT1());
            });

        public static MyResult<T1> As<T1>(this MyResult<object> result, Func<MyError> errorIsNotT1) => result.As<object, T1>(errorIsNotT1);

        public static MyResult<T> As<T>(this object item, Func<MyError> error) =>
            !(item is T t) ? MyResult.Error<T>(error()) : t;

        public static MyResult<T> NotNull<T>(this T item, Func<MyError> error) =>
            item ?? MyResult.Error<T>(error());

        public static MyResult<string> NotNullOrEmpty(this string s, Func<MyError> error)
            => string.IsNullOrEmpty(s) ? MyResult.Error<string>(error()) : s;

        public static MyResult<string> NotNullOrWhiteSpace(this string s, Func<MyError> error)
            => string.IsNullOrWhiteSpace(s) ? MyResult.Error<string>(error()) : s;

        public static MyResult<T> First<T>(this IEnumerable<T> candidates, Func<T, bool> predicate, Func<MyError> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
}
