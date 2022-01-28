//HintName: OperationResult.g.cs
#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunicularSwitch;
using FunicularSwitch.Test.Errors;

namespace FunicularSwitch.Test

{
#pragma warning disable 1591
    public abstract partial class OperationResult
    {
        public static OperationResult<T> Error<T>(MyError details) => new OperationResult<T>.Error_(details);
        public static OperationResult<T> Ok<T>(T value) => new OperationResult<T>.Ok_(value);
        public bool IsError => GetType().GetGenericTypeDefinition() == typeof(OperationResult<>.Error_);
        public bool IsOk => !IsError;
        public abstract MyError? GetErrorOrDefault();

        public static OperationResult<T> Try<T>(Func<T> action, Func<Exception, MyError> formatError)
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

        public static async Task<OperationResult<T>> Try<T>(Func<Task<T>> action, Func<Exception, MyError> formatError)
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
    }

    public abstract partial class OperationResult<T> : OperationResult, IEnumerable<T>
    {
        public static OperationResult<T> Error(MyError message) => Error<T>(message);
        public static OperationResult<T> Ok(T value) => Ok<T>(value);

        public static implicit operator OperationResult<T>(T value) => OperationResult.Ok(value);

        public static bool operator true(OperationResult<T> result) => result.IsOk;
        public static bool operator false(OperationResult<T> result) => result.IsError;

        public static bool operator !(OperationResult<T> result) => result.IsError;

        //just here to suppress warning, never called because all subtypes (Ok_, Error_) implement Equals and GetHashCode
        bool Equals(OperationResult<T> other) => this switch
        {
            Ok_ ok => ok.Equals((object)other),
            Error_ error => error.Equals((object)other),
            _ => throw new InvalidOperationException($"Unexpected type derived from {nameof(OperationResult<T>)}")
        };

        public override int GetHashCode() => this switch
        {
            Ok_ ok => ok.GetHashCode(),
            Error_ error => error.GetHashCode(),
            _ => throw new InvalidOperationException($"Unexpected type derived from {nameof(OperationResult<T>)}")
        };

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OperationResult<T>)obj);
        }

        public static bool operator ==(OperationResult<T>? left, OperationResult<T>? right) => Equals(left, right);

        public static bool operator !=(OperationResult<T>? left, OperationResult<T>? right) => !Equals(left, right);

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
                Ok_ okOperationResult => ok(okOperationResult.Value),
                Error_ errorOperationResult => error(errorOperationResult.Details),
                _ => throw new InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        public async Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<MyError, Task<T1>> error)
        {
            return this switch
            {
                Ok_ okOperationResult => await ok(okOperationResult.Value).ConfigureAwait(false),
                Error_ errorOperationResult => await error(errorOperationResult.Details).ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        public Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<MyError, T1> error) =>
            Match(ok, e => Task.FromResult(error(e)));

        public async Task Match(Func<T, Task> ok)
        {
            if (this is Ok_ okOperationResult) await ok(okOperationResult.Value).ConfigureAwait(false);
        }

        public T Match(Func<MyError, T> error) => Match(v => v, error);

        public OperationResult<T1> Bind<T1>(Func<T, OperationResult<T1>> bind)
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

        public async Task<OperationResult<T1>> Bind<T1>(Func<T, Task<OperationResult<T1>>> bind)
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

        public OperationResult<T1> Map<T1>(Func<T, T1> map)
            => Bind(value => Ok(map(value)));

        public Task<OperationResult<T1>> Map<T1>(Func<T, Task<T1>> map)
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

        public sealed partial class Ok_ : OperationResult<T>
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

        public sealed partial class Error_ : OperationResult<T>
        {
            public MyError Details { get; }

            public Error_(MyError details) => Details = details;

            public OperationResult<T1>.Error_ Convert<T1>() => new OperationResult<T1>.Error_(Details);

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

    public static partial class OperationResultExtension
    {
        #region bind

        public static async Task<OperationResult<T1>> Bind<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, OperationResult<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        public static async Task<OperationResult<T1>> Bind<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, Task<OperationResult<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        public static async Task<OperationResult<T1>> Map<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        public static Task<OperationResult<T1>> Map<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, Task<T1>> bind)
            => Bind(result, async v => OperationResult.Ok(await bind(v).ConfigureAwait(false)));

        public static OperationResult<T> MapError<T>(this OperationResult<T> result, Func<MyError, MyError> mapError) =>
            result.Match(ok => ok, error => OperationResult.Error<T>(mapError(error)));

        #endregion

        #region match

        public static async Task<T1> Match<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, Task<T1>> ok,
            Func<MyError, Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, Task<T1>> ok,
            Func<MyError, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<OperationResult<T>> result,
            Func<T, T1> ok,
            Func<MyError, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);

        #endregion

        public static OperationResult<T> Flatten<T>(this OperationResult<OperationResult<T>> result) => result.Bind(r => r);

        public static OperationResult<T1> As<T, T1>(this OperationResult<T> result, Func<MyError> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return OperationResult.Error<T1>(errorTIsNotT1());
            });

        public static OperationResult<T1> As<T1>(this OperationResult<object> result, Func<MyError> errorIsNotT1) =>
            result.As<object, T1>(errorIsNotT1);
    }
}

namespace FunicularSwitch.Test
.Extensions
{
    public static partial class OperationResultExtension
    {
        public static IEnumerable<T1> Choose<T, T1>(
            this IEnumerable<T> items,
            Func<T, OperationResult<T1>> choose,
            Action<MyError> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static IEnumerable<T> Choose<T>(
            this IEnumerable<OperationResult<T>> results,
            Action<MyError> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        public static OperationResult<T> As<T>(this object item, Func<MyError> error) =>
            !(item is T t) ? OperationResult.Error<T>(error()) : t;

        public static OperationResult<T> NotNull<T>(this T item, Func<MyError> error) =>
            item ?? OperationResult.Error<T>(error());

        public static OperationResult<string> NotNullOrEmpty(this string s, Func<MyError> error)
            => string.IsNullOrEmpty(s) ? OperationResult.Error<string>(error()) : s;

        public static OperationResult<string> NotNullOrWhiteSpace(this string s, Func<MyError> error)
            => string.IsNullOrWhiteSpace(s) ? OperationResult.Error<string>(error()) : s;

        public static OperationResult<T> First<T>(this IEnumerable<T> candidates, Func<T, bool> predicate, Func<MyError> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
#pragma warning restore 1591
}
