//HintName: FunicularSwitch.Test.OperationResult.g.cs
#nullable enable
using global::System.Linq;
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

        public static OperationResult<T> Try<T>(global::System.Func<T> action, global::System.Func<global::System.Exception, MyError> formatError)
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

        public static async global::System.Threading.Tasks.Task<OperationResult<T>> Try<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> action, global::System.Func<global::System.Exception, MyError> formatError)
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

        public static OperationResult<T> Try<T>(global::System.Func<OperationResult<T>> action, global::System.Func<global::System.Exception, MyError> formatError)
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

        public static async global::System.Threading.Tasks.Task<OperationResult<T>> Try<T>(global::System.Func<global::System.Threading.Tasks.Task<OperationResult<T>>> action, global::System.Func<global::System.Exception, MyError> formatError)
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

    public abstract partial class OperationResult<T> : OperationResult, global::System.Collections.Generic.IEnumerable<T>
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
            _ => throw new global::System.InvalidOperationException($"Unexpected type derived from {nameof(OperationResult<T>)}")
        };

        public override int GetHashCode() => this switch
        {
            Ok_ ok => ok.GetHashCode(),
            Error_ error => error.GetHashCode(),
            _ => throw new global::System.InvalidOperationException($"Unexpected type derived from {nameof(OperationResult<T>)}")
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

        public void Match(global::System.Action<T> ok, global::System.Action<MyError>? error = null) => Match(
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

        public T1 Match<T1>(global::System.Func<T, T1> ok, global::System.Func<MyError, T1> error)
        {
            return this switch
            {
                Ok_ okOperationResult => ok(okOperationResult.Value),
                Error_ errorOperationResult => error(errorOperationResult.Details),
                _ => throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        public async global::System.Threading.Tasks.Task<T1> Match<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok, global::System.Func<MyError, global::System.Threading.Tasks.Task<T1>> error)
        {
            return this switch
            {
                Ok_ okOperationResult => await ok(okOperationResult.Value).ConfigureAwait(false),
                Error_ errorOperationResult => await error(errorOperationResult.Details).ConfigureAwait(false),
                _ => throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        public global::System.Threading.Tasks.Task<T1> Match<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok, global::System.Func<MyError, T1> error) =>
            Match(ok, e => global::System.Threading.Tasks.Task.FromResult(error(e)));

        public async global::System.Threading.Tasks.Task Match(global::System.Func<T, global::System.Threading.Tasks.Task> ok)
        {
            if (this is Ok_ okOperationResult) await ok(okOperationResult.Value).ConfigureAwait(false);
        }

        public T Match(global::System.Func<MyError, T> error) => Match(v => v, error);

        public OperationResult<T1> Bind<T1>(global::System.Func<T, OperationResult<T1>> bind)
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
		                throw; //createGenericErrorResult
	                }
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        public async global::System.Threading.Tasks.Task<OperationResult<T1>> Bind<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<OperationResult<T1>>> bind)
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
		                throw; //createGenericErrorResult
	                }
                case Error_ error:
                    return error.Convert<T1>();
                default:
                    throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}");
            }
        }

        public OperationResult<T1> Map<T1>(global::System.Func<T, T1> map)
            => Bind(value => Ok(map(value)));

        public global::System.Threading.Tasks.Task<OperationResult<T1>> Map<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> map)
            => Bind(async value => Ok(await map(value).ConfigureAwait(false)));

        public T? GetValueOrDefault()
	        => Match(
		        v => (T?)v,
		        _ => default
	        );

        public T GetValueOrDefault(global::System.Func<T> defaultValue)
	        => Match(
		        v => v,
		        _ => defaultValue()
	        );

        public T GetValueOrDefault(T defaultValue)
	        => Match(
		        v => v,
		        _ => defaultValue
	        );

        public T GetValueOrThrow()
            => Match(
                v => v,
                details => throw new global::System.InvalidOperationException($"Cannot access error result value. Error: {details}"));

        public global::System.Collections.Generic.IEnumerator<T> GetEnumerator() => Match(ok => new[] { ok }, _ => Enumerable.Empty<T>()).GetEnumerator();

        public override string ToString() => Match(ok => $"Ok {ok?.ToString()}", error => $"Error {error}");
        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public sealed partial class Ok_ : OperationResult<T>
        {
            public T Value { get; }

            public Ok_(T value) => Value = value;

            public override MyError? GetErrorOrDefault() => null;

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

        public static async global::System.Threading.Tasks.Task<OperationResult<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, OperationResult<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        public static async global::System.Threading.Tasks.Task<OperationResult<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<OperationResult<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        public static async global::System.Threading.Tasks.Task<OperationResult<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        public static global::System.Threading.Tasks.Task<OperationResult<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> bind)
            => Bind(result, async v => OperationResult.Ok(await bind(v).ConfigureAwait(false)));

        public static OperationResult<T> MapError<T>(this OperationResult<T> result, global::System.Func<MyError, MyError> mapError)
        {
            if (result is OperationResult<T>.Error_ e)
                return OperationResult.Error<T>(mapError(e.Details));
            return result;
        }

        public static async global::System.Threading.Tasks.Task<OperationResult<T>> MapError<T>(this global::System.Threading.Tasks.Task<OperationResult<T>> result, global::System.Func<MyError, MyError> mapError) => (await result.ConfigureAwait(false)).MapError(mapError);

        #endregion

        #region match

        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
            global::System.Func<MyError, global::System.Threading.Tasks.Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
            global::System.Func<MyError, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<OperationResult<T>> result,
            global::System.Func<T, T1> ok,
            global::System.Func<MyError, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);

        #endregion

        public static OperationResult<T> Flatten<T>(this OperationResult<OperationResult<T>> result) => result.Bind(r => r);

        public static OperationResult<T1> As<T, T1>(this OperationResult<T> result, global::System.Func<MyError> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return OperationResult.Error<T1>(errorTIsNotT1());
            });

        public static OperationResult<T1> As<T1>(this OperationResult<object> result, global::System.Func<MyError> errorIsNotT1) =>
            result.As<object, T1>(errorIsNotT1);
        
        #region query-expression pattern
        
        public static OperationResult<T1> Select<T, T1>(this OperationResult<T> result, global::System.Func<T, T1> selector) => result.Map(selector);
        public static global::System.Threading.Tasks.Task<OperationResult<T1>> Select<T, T1>(this global::System.Threading.Tasks.Task<OperationResult<T>> result, global::System.Func<T, T1> selector) => result.Map(selector);
        
        public static OperationResult<T2> SelectMany<T, T1, T2>(this OperationResult<T> result, global::System.Func<T, OperationResult<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        public static global::System.Threading.Tasks.Task<OperationResult<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<OperationResult<T>> result, global::System.Func<T, global::System.Threading.Tasks.Task<OperationResult<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        public static global::System.Threading.Tasks.Task<OperationResult<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<OperationResult<T>> result, global::System.Func<T, OperationResult<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        public static global::System.Threading.Tasks.Task<OperationResult<T2>> SelectMany<T, T1, T2>(this OperationResult<T> result, global::System.Func<T, global::System.Threading.Tasks.Task<OperationResult<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        #endregion
    }
}

namespace FunicularSwitch.Test.Extensions
{
    public static partial class OperationResultExtension
    {
        public static global::System.Collections.Generic.IEnumerable<T1> Choose<T, T1>(
            this global::System.Collections.Generic.IEnumerable<T> items,
            global::System.Func<T, OperationResult<T1>> choose,
            global::System.Action<MyError> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static global::System.Collections.Generic.IEnumerable<T> Choose<T>(
            this global::System.Collections.Generic.IEnumerable<OperationResult<T>> results,
            global::System.Action<MyError> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        public static OperationResult<T> As<T>(this object item, global::System.Func<MyError> error) =>
            !(item is T t) ? OperationResult.Error<T>(error()) : t;

        public static OperationResult<T> NotNull<T>(this T? item, global::System.Func<MyError> error) =>
            item ?? OperationResult.Error<T>(error());

        public static OperationResult<string> NotNullOrEmpty(this string? s, global::System.Func<MyError> error)
            => string.IsNullOrEmpty(s) ? OperationResult.Error<string>(error()) : s!;

        public static OperationResult<string> NotNullOrWhiteSpace(this string? s, global::System.Func<MyError> error)
            => string.IsNullOrWhiteSpace(s) ? OperationResult.Error<string>(error()) : s!;

        public static OperationResult<T> First<T>(this global::System.Collections.Generic.IEnumerable<T> candidates, global::System.Func<T, bool> predicate, global::System.Func<MyError> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
#pragma warning restore 1591
}
