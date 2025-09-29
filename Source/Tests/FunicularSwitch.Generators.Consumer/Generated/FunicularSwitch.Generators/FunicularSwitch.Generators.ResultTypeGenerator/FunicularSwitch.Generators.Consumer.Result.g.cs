#nullable enable

using global::System.Linq;
using System;
using FunicularSwitch.Generic;

namespace FunicularSwitch.Generators.Consumer
{
#pragma warning disable 1591
    public abstract partial class Result
    {
        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> Error<T>(String details) => new Result<T>.Error_(details);
        [global::System.Diagnostics.DebuggerStepThrough]
        public static ResultError Error(String details) => new(details);
        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> Ok<T>(T value) => new Result<T>.Ok_(value);
        public bool IsError => GetType().GetGenericTypeDefinition() == typeof(Result<>.Error_);
        public bool IsOk => !IsError;
        public abstract String? GetErrorOrDefault();

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> Try<T>(global::System.Func<T> action, global::System.Func<global::System.Exception, String> formatError)
        {
            try
            {
                return action();
            }
            catch (global::System.Exception e)
            {
                return Error<T>(formatError(e));
            }
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<Result<T>> Try<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> action, global::System.Func<global::System.Exception, String> formatError)
        {
            try
            {
                return await action();
            }
            catch (global::System.Exception e)
            {
                return Error<T>(formatError(e));
            }
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> Try<T>(global::System.Func<Result<T>> action, global::System.Func<global::System.Exception, String> formatError)
        {
            try
            {
                return action();
            }
            catch (global::System.Exception e)
            {
                return Error<T>(formatError(e));
            }
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<Result<T>> Try<T>(global::System.Func<global::System.Threading.Tasks.Task<Result<T>>> action, global::System.Func<global::System.Exception, String> formatError)
        {
            try
            {
                return await action();
            }
            catch (global::System.Exception e)
            {
                return Error<T>(formatError(e));
            }
        }
    }

    public abstract partial class Result<T> : Result, global::System.Collections.Generic.IEnumerable<T>
    {
        
        [global::System.Diagnostics.DebuggerNonUserCode]
        public static new Result<T> Error(String message) => Error<T>(message);
        
        [global::System.Diagnostics.DebuggerNonUserCode]
        public static Result<T> Ok(T value) => Ok<T>(value);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static implicit operator Result<T>(T value) => Result.Ok(value);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static implicit operator Result<T>(ResultError myResultError) => myResultError.WithOk<T>();

        public static bool operator true(Result<T> result) => result.IsOk;
        public static bool operator false(Result<T> result) => result.IsError;

        public static bool operator !(Result<T> result) => result.IsError;

        //just here to suppress warning, never called because all subtypes (Ok_, Error_) implement Equals and GetHashCode
        bool Equals(Result<T> other) => this switch
        {
            Ok_ ok => ok.Equals((object)other),
            Error_ error => error.Equals((object)other),
            _ => throw new global::System.InvalidOperationException($"Unexpected type derived from {nameof(Result<T>)}")
        };

        public override int GetHashCode() => this switch
        {
            Ok_ ok => ok.GetHashCode(),
            Error_ error => error.GetHashCode(),
            _ => throw new global::System.InvalidOperationException($"Unexpected type derived from {nameof(Result<T>)}")
        };

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Result<T>)obj);
        }

        public static bool operator ==(Result<T>? left, Result<T>? right) => Equals(left, right);

        public static bool operator !=(Result<T>? left, Result<T>? right) => !Equals(left, right);

        [global::System.Diagnostics.DebuggerStepThrough]
        public void Match(global::System.Action<T> ok, global::System.Action<String>? error = null) => Match(
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

        [global::System.Diagnostics.DebuggerStepThrough]
        public T1 Match<T1>(global::System.Func<T, T1> ok, global::System.Func<String, T1> error)
        {
            return this switch
            {
                Ok_ okResult => ok(okResult.Value),
                Error_ errorResult => error(errorResult.Details),
                _ => throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task<T1> Match<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok, global::System.Func<String, global::System.Threading.Tasks.Task<T1>> error)
        {
            return this switch
            {
                Ok_ okResult => await ok(okResult.Value).ConfigureAwait(false),
                Error_ errorResult => await error(errorResult.Details).ConfigureAwait(false),
                _ => throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public global::System.Threading.Tasks.Task<T1> Match<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok, global::System.Func<String, T1> error) =>
            Match(ok, e => global::System.Threading.Tasks.Task.FromResult(error(e)));

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task Match(global::System.Func<T, global::System.Threading.Tasks.Task> ok)
        {
            if (this is Ok_ okResult) await ok(okResult.Value).ConfigureAwait(false);
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public T Match(global::System.Func<String, T> error) => Match(v => v, error);

        [global::System.Diagnostics.DebuggerStepThrough]
        public Result<T1> Bind<T1>(global::System.Func<T, Result<T1>> bind)
        {
            switch (this)
            {
                case Ok_ ok:
	                try
	                {
		                return bind(ok.Value);
	                }
	                // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168 // Variable is declared but never used
	                catch (global::System.Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
	                {
		                return Result.Error<T1>(FunicularSwitch.Generators.Consumer.ErrorExtension.UnexpectedToStringError(e));
	                }
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task<Result<T1>> Bind<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<Result<T1>>> bind)
        {
            switch (this)
            {
                case Ok_ ok:
	                try
	                {
		                return await bind(ok.Value).ConfigureAwait(false);
	                }
	                // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168 // Variable is declared but never used
	                catch (global::System.Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
	                {
		                return Result.Error<T1>(FunicularSwitch.Generators.Consumer.ErrorExtension.UnexpectedToStringError(e));
	                }
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public Result<T1> Map<T1>(global::System.Func<T, T1> map)
        {
            switch (this)
            {
                case Ok_ ok:
                    try
                    {
                        return Result.Ok(map(ok.Value));
                    }
                    // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (global::System.Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
                    {
                        return Result.Error<T1>(FunicularSwitch.Generators.Consumer.ErrorExtension.UnexpectedToStringError(e));
                    }
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task<Result<T1>> Map<T1>(
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> map)
        {
            switch (this)
            {
                case Ok_ ok:
                    try
                    {
                        return Result.Ok(await map(ok.Value).ConfigureAwait(false));
                    }
                    // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (global::System.Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
                    {
                        return Result.Error<T1>(FunicularSwitch.Generators.Consumer.ErrorExtension.UnexpectedToStringError(e));
                    }
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }
        
        
        public static implicit operator global::FunicularSwitch.Generic.GenericResult<T, String>(Result<T> result) => 
            result.Match(
                global::FunicularSwitch.Generic.GenericResult<T, String>.Ok,
                global::FunicularSwitch.Generic.GenericResult<T, String>.Error);
        
        public static implicit operator Result<T>(global::FunicularSwitch.Generic.GenericResult<T, String> result) =>
            result.Match<Result<T>>(
                Result<T>.Ok,
                Result<T>.Error);
        
        public global::FunicularSwitch.Generic.GenericResult<T, String> ToGenericResult() =>
            Match(
                global::FunicularSwitch.Generic.GenericResult<T, String>.Ok,
                global::FunicularSwitch.Generic.GenericResult<T, String>.Error);

        [global::System.Diagnostics.DebuggerStepThrough]
        public T? GetValueOrDefault()
	        => Match(
		        v => (T?)v,
		        _ => default
	        );

        [global::System.Diagnostics.DebuggerStepThrough]
        public T GetValueOrDefault(global::System.Func<T> defaultValue)
	        => Match(
		        v => v,
		        _ => defaultValue()
	        );

        [global::System.Diagnostics.DebuggerStepThrough]
        public T GetValueOrDefault(T defaultValue)
	        => Match(
		        v => v,
		        _ => defaultValue
	        );

        [global::System.Diagnostics.DebuggerStepThrough]
        public T GetValueOrThrow()
            => Match(
                v => v,
                details => throw new global::System.InvalidOperationException($"Cannot access error result value. Error: {details}"));

        public global::System.Collections.Generic.IEnumerator<T> GetEnumerator() => Match(ok => new[] { ok }, _ => Enumerable.Empty<T>()).GetEnumerator();

        public override string ToString() => Match(ok => $"Ok {ok?.ToString()}", error => $"Error {error}");
        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public sealed partial class Ok_ : Result<T>
        {
            public T Value { get; }

            [global::System.Diagnostics.DebuggerStepThrough]
            public Ok_(T value) => Value = value;

            [global::System.Diagnostics.DebuggerStepThrough]
            public override String? GetErrorOrDefault() => null;

            public bool Equals(Ok_? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(Value, other.Value);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is Ok_ other && Equals(other);
            }

            public override int GetHashCode() => Value == null ? 0 : global::System.Collections.Generic.EqualityComparer<T>.Default.GetHashCode(Value);

            public static bool operator ==(Ok_ left, Ok_ right) => Equals(left, right);

            public static bool operator !=(Ok_ left, Ok_ right) => !Equals(left, right);
        }

        public sealed partial class Error_ : Result<T>
        {
            public String Details { get; }

            [global::System.Diagnostics.DebuggerStepThrough]
            public Error_(String details) => Details = details;

            [global::System.Diagnostics.DebuggerStepThrough]
            public Result<T1>.Error_ Convert<T1>() => new Result<T1>.Error_(Details);

            [global::System.Diagnostics.DebuggerStepThrough]
            public override String? GetErrorOrDefault() => Details;

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

    public readonly partial struct ResultError : global::System.IEquatable<ResultError>
    {
        readonly String _details;

        internal ResultError(String details) => _details = details;

        [global::System.Diagnostics.Contracts.Pure]
        public Result<T> WithOk<T>() => Result.Error<T>(_details);

        public bool Equals(ResultError other) => _details.Equals(other._details);

        public override bool Equals(object? obj) => obj is ResultError other && Equals(other);

        public override int GetHashCode() => _details.GetHashCode();

        public static bool operator ==(ResultError left, ResultError right) => left.Equals(right);

        public static bool operator !=(ResultError left, ResultError right) => !left.Equals(right);
    }

    public static partial class ResultExtension
    {
        #region bind

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<Result<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, Result<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<Result<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<Result<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<Result<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<Result<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> bind)
            => Bind(result, async v => Result.Ok(await bind(v).ConfigureAwait(false)));

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> MapError<T>(this Result<T> result, global::System.Func<String, String> mapError)
        {
            if (result is Result<T>.Error_ e)
                return Result.Error<T>(mapError(e.Details));
            return result;
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<Result<T>> MapError<T>(this global::System.Threading.Tasks.Task<Result<T>> result, global::System.Func<String, String> mapError) => (await result.ConfigureAwait(false)).MapError(mapError);

        #endregion

        #region match

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
            global::System.Func<String, global::System.Threading.Tasks.Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
            global::System.Func<String, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<Result<T>> result,
            global::System.Func<T, T1> ok,
            global::System.Func<String, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);

        #endregion

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> Flatten<T>(this Result<Result<T>> result) => result.Bind(r => r);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T1> As<T, T1>(this Result<T> result, global::System.Func<String> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return Result.Error<T1>(errorTIsNotT1());
            });

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T1> As<T1>(this Result<object> result, global::System.Func<String> errorIsNotT1) =>
            result.As<object, T1>(errorIsNotT1);

        #region query-expression pattern

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T1> Select<T, T1>(this Result<T> result, global::System.Func<T, T1> selector) => result.Map(selector);
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<Result<T1>> Select<T, T1>(this global::System.Threading.Tasks.Task<Result<T>> result, global::System.Func<T, T1> selector) => result.Map(selector);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T2> SelectMany<T, T1, T2>(this Result<T> result, global::System.Func<T, Result<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<Result<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<Result<T>> result, global::System.Func<T, global::System.Threading.Tasks.Task<Result<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<Result<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<Result<T>> result, global::System.Func<T, Result<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<Result<T2>> SelectMany<T, T1, T2>(this Result<T> result, global::System.Func<T, global::System.Threading.Tasks.Task<Result<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        #endregion
        
        
        public static Result<T> ToResult<T>(
            this global::FunicularSwitch.Generic.GenericResult<T, String> result) =>
                result.Match<Result<T>>(
                    Result<T>.Ok,
                    Result<T>.Error);
        
        public static global::System.Threading.Tasks.Task<Result<T>> ToResult<T>(
            this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generic.GenericResult<T, String>> result) =>
                result.Match(
                    Result<T>.Ok,
                    Result<T>.Error);
        
        public static global::System.Threading.Tasks.Task<global::FunicularSwitch.Generic.GenericResult<T, String>> ToGenericResult<T>(
            this global::System.Threading.Tasks.Task<Result<T>> result) =>
                result.Match(
                    global::FunicularSwitch.Generic.GenericResult<T, String>.Ok,
                    global::FunicularSwitch.Generic.GenericResult<T, String>.Error);
    }
}

namespace FunicularSwitch.Generators.Consumer.Extensions
{
    public static partial class ResultExtension
    {
        public static global::System.Collections.Generic.IEnumerable<T1> Choose<T, T1>(
            this global::System.Collections.Generic.IEnumerable<T> items,
            global::System.Func<T, Result<T1>> choose,
            global::System.Action<String> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static global::System.Collections.Generic.IEnumerable<T> Choose<T>(
            this global::System.Collections.Generic.IEnumerable<Result<T>> results,
            global::System.Action<String> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> As<T>(this object? item, global::System.Func<String> error) =>
            !(item is T t) ? Result.Error<T>(error()) : t;

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<T> NotNull<T>(this T? item, global::System.Func<String> error) =>
            item ?? Result.Error<T>(error());

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<string> NotNullOrEmpty(this string? s, global::System.Func<String> error)
            => string.IsNullOrEmpty(s) ? Result.Error<string>(error()) : s!;

        [global::System.Diagnostics.DebuggerStepThrough]
        public static Result<string> NotNullOrWhiteSpace(this string? s, global::System.Func<String> error)
            => string.IsNullOrWhiteSpace(s) ? Result.Error<string>(error()) : s!;

        public static Result<T> First<T>(this global::System.Collections.Generic.IEnumerable<T> candidates, global::System.Func<T, bool> predicate, global::System.Func<String> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
#pragma warning restore 1591
}
