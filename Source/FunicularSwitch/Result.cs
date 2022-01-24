using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunicularSwitch.Extensions;

namespace FunicularSwitch
{
    public abstract class Result
    {
        public static Result<T> Error<T>(string message) => new Error<T>(message);
        public static Result<T> Ok<T>(T value) => new Ok<T>(value);
        public bool IsError => GetType().GetGenericTypeDefinition() == typeof(Error<>);
        public bool IsOk => !IsError;
        public abstract string? GetErrorOrDefault();

        public static Result<T> Try<T>(Func<T> action, Func<Exception, string> formatError)
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

        public static Result<Unit> Try(Action action, Func<Exception, string> formatError)
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

        public static async Task<Result<Unit>> Try(Func<Task> action, Func<Exception, string> formatError)
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

        public static async Task<Result<T>> Try<T>(Func<Task<T>> action, Func<Exception, string> formatError)
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

        public static Task<Result<TResult>> Aggregate<T1, T2, TResult>(Task<Result<T1>> r1, Task<Result<T2>> r2, Func<T1, T2, TResult> combine) => ResultExtension.Aggregate(r1, r2, combine);

        public static Result<(T1, T2)> Aggregate<T1, T2>(Result<T1> r1, Result<T2> r2) => r1.Aggregate(r2);

        public static Result<(T1, T2, T3)> Aggregate<T1, T2, T3>(Result<T1> r1, Result<T2> r2, Result<T3> r3) => r1.Aggregate(r2, r3);

        public static Result<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4) => r1.Aggregate(r2, r3, r4);

        public static Result<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5) => r1.Aggregate(r2, r3, r4, r5);

        public static Result<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4, Result<T5> r5, Result<T6> r6) => r1.Aggregate(r2, r3, r4, r5, r6);

        public static Task<Result<(T1, T2)>> Aggregate<T1, T2>(Task<Result<T1>> r1, Task<Result<T2>> r2) => r1.Aggregate(r2);

        public static Task<Result<(T1, T2, T3)>> Aggregate<T1, T2, T3>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3) => r1.Aggregate(r2, r3);

        public static Task<Result<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4) => r1.Aggregate(r2, r3, r4);

        public static Task<Result<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5) => r1.Aggregate(r2, r3, r4, r5);

        public static Task<Result<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(Task<Result<T1>> r1, Task<Result<T2>> r2, Task<Result<T3>> r3, Task<Result<T4>> r4, Task<Result<T5>> r5, Task<Result<T6>> r6) => r1.Aggregate(r2, r3, r4, r5, r6);
    }

    public abstract class Result<T> : Result, IEnumerable<T>
    {
        public static Result<T> Error(string message) => Error<T>(message);
        public static Result<T> Ok(T value) => Ok<T>(value);

        public static implicit operator Result<T>(T value) => Result.Ok(value);

        public static bool operator true(Result<T> result) => result.IsOk;
        public static bool operator false(Result<T> result) => result.IsError;

        public static bool operator ==(Result<T>? left, Result<T>? right) => Equals(left, right);

        public static bool operator !=(Result<T>? left, Result<T>? right) => !Equals(left, right);

        public static bool operator !(Result<T> result) => result.IsError;

        public void Match(Action<T> ok, Action<string>? error = null) => Match(
            v => ok.ToFunc().Invoke(v),
            err =>
            {
                error?.Invoke(err);
                return 42;
            });
        public T1 Match<T1>(Func<T, T1> ok, Func<string, T1> error)
        {
            switch (this)
            {
                case Ok<T> okResult:
                    return ok(okResult.Value);
                case Error<T> errorResult:
                    return error(errorResult.Message);
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }
        public async Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<string, Task<T1>> error)
        {
            switch (this)
            {
                case Ok<T> okResult:
                    return await ok(okResult.Value).ConfigureAwait(false);
                case Error<T> errorResult:
                    return await error(errorResult.Message).ConfigureAwait(false);
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }
        public Task<T1> Match<T1>(Func<T, Task<T1>> ok, Func<string, T1> error) => Match(ok, e => Task.FromResult(error(e)));
        public async Task Match(Func<T, Task> ok)
        {
            switch (this)
            {
                case Ok<T> okResult:
                    await ok(okResult.Value).ConfigureAwait(false);
                    break;
                case Error<T>:
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }
        public T Match(Func<string, T> error) => Match(v => v, error);

        public Result<T1> Bind<T1>(Func<T, Result<T1>> bind)
        {
            switch (this)
            {
                case Ok<T> ok:
                    return bind(ok.Value);
                case Error<T> error:
                    return error.Convert<T1>();
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }
        public async Task<Result<T1>> Bind<T1>(Func<T, Task<Result<T1>>> bind)
        {
            switch (this)
            {
                case Ok<T> ok:
                    return await bind(ok.Value).ConfigureAwait(false);
                case Error<T> error:
                    return error.Convert<T1>();
                default:
                    throw new InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        public Result<T1> Map<T1>(Func<T, T1> map) 
            => Bind(value => Ok(map(value)));

        public Task<Result<T1>> Map<T1>(Func<T, Task<T1>> map) 
            => Bind(async value => Ok(await map(value).ConfigureAwait(false)));

        public T? GetValueOrDefault(Func<T>? defaultValue = null)
            => Match(
                v => v,
                _ => defaultValue != null ? defaultValue() : default);

        public T GetValueOrThrow()
            => Match(
                v => v,
                message => throw new InvalidOperationException($"Cannot access error result value. Error: {message}"));

        public IEnumerator<T> GetEnumerator() => Match(ok => new[]{ok}, _ => Enumerable.Empty<T>()).GetEnumerator();

        public override string ToString() => Match(ok => $"Ok {ok?.ToString()}", error => $"Error {error}");
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public sealed class Ok<T> : Result<T>
    {
        public T Value { get; }

        public Ok(T value) => Value = value;

        public override string? GetErrorOrDefault() => null;

        public bool Equals(Ok<T> other)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object? obj)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Ok<T> other && Equals(other);
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(Ok<T> left, Ok<T> right) => Equals(left, right);

        public static bool operator !=(Ok<T> left, Ok<T> right) => !Equals(left, right);
    }

    public sealed class Error<T> : Result<T>
    {
        public string Message { get; }

        public Error(string message) => Message = message ?? throw new ArgumentNullException(nameof(message), "Cannot create error result without error information");

        public Error<T1> Convert<T1>() => new(Message);

        public override string GetErrorOrDefault() => Message;

        public bool Equals(Error<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Message, other.Message);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Error<T> other && Equals(other);
        }

        public override int GetHashCode() => Message.GetHashCode();

        public static bool operator ==(Error<T> left, Error<T> right) => Equals(left, right);

        public static bool operator !=(Error<T> left, Error<T> right) => !Equals(left, right);
    }

    public static class ResultExtension
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public static string DefaultErrorSeparator = Environment.NewLine;

        #region bind

        public static async Task<Result<T1>> Bind<T, T1>(
            this Task<Result<T>> result,
            Func<T, Result<T1>> bind) 
            => (await result.ConfigureAwait(false)).Bind(bind);

        public static async Task<Result<T1>> Bind<T, T1>(
            this Task<Result<T>> result,
            Func<T, Task<Result<T1>>> bind) 
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        public static Result<IReadOnlyCollection<T1>> Bind<T, T1>(this IEnumerable<Result<T>> results, Func<T, Result<T1>> bind) =>
            results.Select(r => r.Bind(bind)).Aggregate();

        public static Result<IReadOnlyCollection<T1>> Bind<T, T1>(this Result<T> result, Func<T, IEnumerable<Result<T1>>> bindMany) => 
            result.Map(ok => bindMany(ok).Aggregate()).Flatten();

        public static Result<T1> Bind<T, T1>(this IEnumerable<Result<T>> results, Func<IEnumerable<T>, Result<T1>> bind) =>
            results.Aggregate().Bind(bind);

        #endregion

        #region map

        public static async Task<Result<T1>> Map<T, T1>(
            this Task<Result<T>> result,
            Func<T, T1> map) 
            => (await result.ConfigureAwait(false)).Map(map);

        public static Task<Result<T1>> Map<T, T1>(
            this Task<Result<T>> result,
            Func<T, Task<T1>> bind) 
            => Bind(result, async v => Result.Ok(await bind(v).ConfigureAwait(false)));

        public static Result<IReadOnlyCollection<T1>> Map<T, T1>(this IEnumerable<Result<T>> results, Func<T, T1> map) =>
            results.Select(r => r.Map(map)).Aggregate();

        public static Result<T> MapError<T>(this Result<T> result, Func<string, string> mapError) =>
            result.Match(ok => ok, error => Result.Error<T>(mapError(error)));

        #endregion

        #region match
        public static async Task<T1> Match<T, T1>(
            this Task<Result<T>> result,
            Func<T, Task<T1>> ok,
            Func<string, Task<T1>> error) 
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<Result<T>> result,
            Func<T, Task<T1>> ok,
            Func<string, T1> error) 
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async Task<T1> Match<T, T1>(
            this Task<Result<T>> result,
            Func<T, T1> ok,
            Func<string, T1> error) 
            => (await result.ConfigureAwait(false)).Match(ok, error);
        #endregion

        #region aggregate

        #region sync aggregate 

        public static Result<(T1, T2)> Aggregate<T1, T2>(
            this Result<T1> r1,
            Result<T2> r2,
            string? errorSeparator = null) 
            => r1.Aggregate(r2, (v1, v2) => (v1, v2), errorSeparator);

        public static Result<TResult> Aggregate<T1, T2, TResult>(
            this Result<T1> r1,
            Result<T2> r2,
            Func<T1, T2, TResult> combine,
            string? errorSeparator = null)
        {
            var errors = JoinErrorMessages(r1.Yield().Concat<Result>(r2), errorSeparator);
            return !string.IsNullOrEmpty(errors) 
                ? Result.Error<TResult>(errors) 
                : combine(r1.GetValueOrThrow(), r2.GetValueOrThrow());
        }

        public static Result<(T1, T2, T3)> Aggregate<T1, T2, T3>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            string? errorSeparator = null)
            => r1.Aggregate(r2, r3, (v1, v2, v3) => (v1, v2, v3), errorSeparator);

        public static Result<TResult> Aggregate<T1, T2, T3, TResult>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Func<T1, T2, T3, TResult> combine,
            string? errorSeparator = null)
        {
            var errors = JoinErrorMessages(r1.Yield().Concat<Result>(r2, r3), errorSeparator);
            return !string.IsNullOrEmpty(errors) 
                ? Result.Error<TResult>(errors) 
                : combine(r1.GetValueOrThrow(), r2.GetValueOrThrow(), r3.GetValueOrThrow());
        }

        public static Result<(T1, T2, T3, T4)> Aggregate<T1, T2, T3, T4>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Result<T4> r4,
            string? errorSeparator = null)
            => r1.Aggregate(r2, r3, r4, (v1, v2, v3, v4) => (v1, v2, v3, v4), errorSeparator);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, TResult>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Result<T4> r4,
            Func<T1, T2, T3, T4, TResult> combine,
            string? errorSeparator = null)
        {
            var errors = JoinErrorMessages(r1.Yield().Concat<Result>(r2, r3, r4), errorSeparator);
            return !string.IsNullOrEmpty(errors) 
                ? Result.Error<TResult>(errors) 
                : combine(r1.GetValueOrThrow(), r2.GetValueOrThrow(), r3.GetValueOrThrow(), r4.GetValueOrThrow());
        }

        public static Result<(T1, T2, T3, T4, T5)> Aggregate<T1, T2, T3, T4, T5>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Result<T4> r4,
            Result<T5> r5,
            string? errorSeparator = null)
            => r1.Aggregate(r2, r3, r4, r5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5), errorSeparator);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, TResult>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Result<T4> r4,
            Result<T5> r5,
            Func<T1, T2, T3, T4, T5, TResult> combine,
            string? errorSeparator = null)
        {
            var errors = JoinErrorMessages(r1.Yield().Concat<Result>(r2, r3, r4, r5), errorSeparator);
            return !string.IsNullOrEmpty(errors)
                ? Result.Error<TResult>(errors)
                : combine(
                    r1.GetValueOrThrow(), r2.GetValueOrThrow(), r3.GetValueOrThrow(),
                    r4.GetValueOrThrow(), r5.GetValueOrThrow());
        }

        public static Result<(T1, T2, T3, T4, T5, T6)> Aggregate<T1, T2, T3, T4, T5, T6>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Result<T4> r4,
            Result<T5> r5,
            Result<T6> r6,
            string? errorSeparator = null)
            => r1.Aggregate(r2, r3, r4, r5, r6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6), errorSeparator);

        public static Result<TResult> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(
            this Result<T1> r1,
            Result<T2> r2,
            Result<T3> r3,
            Result<T4> r4,
            Result<T5> r5,
            Result<T6> r6,
            Func<T1, T2, T3, T4, T5, T6, TResult> combine,
            string? errorSeparator = null)
        {
            var errors = JoinErrorMessages(r1.Yield().Concat<Result>(r2, r3, r4, r5, r6), errorSeparator);
            return !string.IsNullOrEmpty(errors)
                ? Result.Error<TResult>(errors)
                : combine(
                    r1.GetValueOrThrow(), r2.GetValueOrThrow(), r3.GetValueOrThrow(),
                    r4.GetValueOrThrow(), r5.GetValueOrThrow(), r6.GetValueOrThrow());
        }

#endregion

        #region async aggregate 
        public static Task<Result<(T1, T2)>> Aggregate<T1, T2>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            string? errorSeparator = null)
            => tr1.Aggregate(tr2, (v1, v2) => (v1, v2), errorSeparator);

        public static async Task<Result<TResult>> Aggregate<T1, T2, TResult>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Func<T1, T2, TResult> combine,
            string? errorSeparator = null)
        {
            await Task.WhenAll(tr1, tr2);
            return tr1.Result.Aggregate(tr2.Result, combine, errorSeparator);
        }

        public static Task<Result<(T1, T2, T3)>> Aggregate<T1, T2, T3>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            string? errorSeparator = null) 
            => tr1.Aggregate(tr2, tr3, (v1, v2, v3) => (v1, v2, v3), errorSeparator);

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, TResult>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Func<T1, T2, T3, TResult> combine,
            string? errorSeparator = null)
        {
            await Task.WhenAll(tr1, tr2, tr3);
            return tr1.Result.Aggregate(tr2.Result, tr3.Result, combine, errorSeparator);
        }

        public static Task<Result<(T1, T2, T3, T4)>> Aggregate<T1, T2, T3, T4>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Task<Result<T4>> tr4,
            string? errorSeparator = null)
            => tr1.Aggregate(tr2, tr3, tr4, (v1, v2, v3, v4) => (v1, v2, v3, v4), errorSeparator);

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, TResult>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Task<Result<T4>> tr4,
            Func<T1, T2, T3, T4, TResult> combine,
            string? errorSeparator = null)
        {
            await Task.WhenAll(tr1, tr2, tr3, tr4);
            return tr1.Result.Aggregate(tr2.Result, tr3.Result, tr4.Result, combine, errorSeparator);
        }

        public static Task<Result<(T1, T2, T3, T4, T5)>> Aggregate<T1, T2, T3, T4, T5>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Task<Result<T4>> tr4,
            Task<Result<T5>> tr5,
            string? errorSeparator = null)
            => tr1.Aggregate(tr2, tr3, tr4, tr5, (v1, v2, v3, v4, v5) => (v1, v2, v3, v4, v5), errorSeparator);

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, TResult>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Task<Result<T4>> tr4,
            Task<Result<T5>> tr5,
            Func<T1, T2, T3, T4, T5, TResult> combine,
            string? errorSeparator = null)
        {
            await Task.WhenAll(tr1, tr2, tr3, tr4, tr5);
            return tr1.Result.Aggregate(tr2.Result, tr3.Result, tr4.Result, tr5.Result, combine, errorSeparator);
        }

        public static Task<Result<(T1, T2, T3, T4, T5, T6)>> Aggregate<T1, T2, T3, T4, T5, T6>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Task<Result<T4>> tr4,
            Task<Result<T5>> tr5,
            Task<Result<T6>> tr6,
            string? errorSeparator = null)
            => tr1.Aggregate(tr2, tr3, tr4, tr5, tr6, (v1, v2, v3, v4, v5, v6) => (v1, v2, v3, v4, v5, v6), errorSeparator);

        public static async Task<Result<TResult>> Aggregate<T1, T2, T3, T4, T5, T6, TResult>(
            this Task<Result<T1>> tr1,
            Task<Result<T2>> tr2,
            Task<Result<T3>> tr3,
            Task<Result<T4>> tr4,
            Task<Result<T5>> tr5,
            Task<Result<T6>> tr6,
            Func<T1, T2, T3, T4, T5, T6, TResult> combine,
            string? errorSeparator = null)
        {
            await Task.WhenAll(tr1, tr2, tr3, tr4, tr5, tr6);
            return tr1.Result.Aggregate(tr2.Result, tr3.Result, tr4.Result, tr5.Result, tr6.Result, combine, errorSeparator);
        }

        #endregion

        public static Result<IReadOnlyCollection<T>> Aggregate<T>(
            this IEnumerable<Result<T>> results,
            string? errorSeparator = null)
        {
            var sb = new StringBuilder();
            var oks = new List<T>();
            foreach (var result in results)
            {
                result.Match(
                    ok => oks.Add(ok),
                    error => sb.Append(error).Append(errorSeparator ?? DefaultErrorSeparator));
            }

            var errors = sb.ToString();
            return !string.IsNullOrEmpty(errors) ? Result.Error<IReadOnlyCollection<T>>(errors) : Result.Ok<IReadOnlyCollection<T>>(oks);
        }

        public static async Task<Result<IReadOnlyCollection<T>>> Aggregate<T>(
            this Task<IEnumerable<Result<T>>> results,
            string? errorSeparator = null) 
            => (await results.ConfigureAwait(false))
                .Aggregate(errorSeparator);

        public static async Task<Result<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<Result<T>>> results,
            string? errorSeparator = null) 
            => (await results.SelectAsync(e => e).ConfigureAwait(false))
                .Aggregate(errorSeparator);

        public static async Task<Result<IReadOnlyCollection<T>>> Aggregate<T>(
            this IEnumerable<Task<Result<T>>> results,
            int maxDegreeOfParallelism,
            string? errorSeparator = null)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .Aggregate(errorSeparator);

        public static async Task<Result<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<Result<T>>>> results,
            string? errorSeparator = null)
            => (await results.SelectAsync(e => e).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate(errorSeparator);

        public static async Task<Result<IReadOnlyCollection<T>>> AggregateMany<T>(
            this IEnumerable<Task<IEnumerable<Result<T>>>> results,
            int maxDegreeOfParallelism,
            string? errorSeparator = null)
            => (await results.SelectAsync(e => e, maxDegreeOfParallelism).ConfigureAwait(false))
                .SelectMany(e => e)
                .Aggregate(errorSeparator);

        #endregion

        #region choose

        public static IEnumerable<T1> Choose<T, T1>(
            this IEnumerable<T> items,
            Func<T, Result<T1>> choose,
            Action<string> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static IEnumerable<T> Choose<T>(
            this IEnumerable<Result<T>> results,
            Action<string> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        #endregion

        #region helpers

        static string JoinErrorMessages(
            this IEnumerable<Result> results,
            string? errorSeparator = null) 
            => results
                .Where(r => r.IsError)
                .Select(r => r.GetErrorOrDefault()!)
                .JoinErrors(errorSeparator);

        static string JoinErrors(this IEnumerable<string> errors, string? errorSeparator = null) =>
            string.Join(errorSeparator ?? DefaultErrorSeparator, errors);

        #endregion

        public static Result<T> Flatten<T>(this Result<Result<T>> result) => result.Bind(r => r);

        public static Result<T1> As<T, T1>(this Result<T> result) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return Result.Error<T1>($"Could not convert '{r?.GetType().Name}' to type {typeof(T1)}");
            });

        public static Result<T1> As<T1>(this Result<object> result) => result.As<object, T1>();

        public static Result<T> As<T>(this object item, Func<string> error) =>
            !(item is T t) ? Result.Error<T>(error()) : t;

        public static Result<T> NotNull<T>(this T? item, Func<string> error) =>
            item ?? Result.Error<T>(error());

        public static Result<string> NotNullOrEmpty(this string s, Func<string> error)
            => string.IsNullOrEmpty(s) ? Result.Error<string>(error()) : s;

        public static Result<string> NotNullOrWhiteSpace(this string s, Func<string> error)
            => string.IsNullOrWhiteSpace(s) ? Result.Error<string>(error()) : s;

        public static Result<T> FirstOk<T>(this IEnumerable<T> candidates, Validate<T, string> validate, Func<string>? onEmpty = null, string? errorSeparator = null) =>
            candidates
                .Select(r => r.Validate(validate, errorSeparator))
                .FirstOk(onEmpty, errorSeparator);

        public static Result<T> First<T>(this IEnumerable<T> candidates, Func<T, bool> predicate, Func<string> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);

        public static Result<T> FirstOk<T>(this IEnumerable<Result<T>> results, Func<string>? onEmpty = null, string? errorSeparator = null)
        {
            List<string> errors = new();
            foreach (var result in results)
            {
                if (result.IsError)
                    errors.Add(result.GetErrorOrDefault()!);
                else
                    return result;
            }

            if (!errors.Any())
                errors.Add(onEmpty?.Invoke() ?? "No result candidates for FirstOk");

            return Result.Error<T>(string.Join(errorSeparator ?? DefaultErrorSeparator, errors));
        }

        public static Result<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<T> candidates, Validate<T, string> validate, string? errorSeparator = null) =>
            candidates
                .Select(c => c.Validate(validate, errorSeparator))
                .Aggregate(errorSeparator);

        public static Result<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<Result<T>> candidates,
            Validate<T, string> validate, string? errorSeparator = null) =>
            candidates
                .Bind(items => items.AllOk(validate, errorSeparator));

        public static Result<T> Validate<T>(this Result<T> item, Validate<T, string> validate, string? errorSeparator = null) => item.Bind(i => i.Validate(validate, errorSeparator));

        public static Result<T> Validate<T>(this T item, Validate<T, string> validate, string? errorSeparator = null)
        {
            var errors = validate(item).JoinErrors(errorSeparator);
            return !string.IsNullOrEmpty(errors) ? Result.Error<T>(errors) : item;
        }
    }

    public delegate IEnumerable<TError> Validate<in T, out TError>(T item);
}