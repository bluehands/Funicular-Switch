﻿#nullable enable

using global::System.Diagnostics.Contracts;
using global::System.Linq;
using System;
using FunicularSwitch.Generic;

namespace FunicularSwitch.Generators.Consumer
{
#pragma warning disable 1591
    public abstract partial class CustomResult
    {
        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T> Error<T>(Guid details) => new CustomResult<T>.Error_(details);
        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResultError Error(Guid details) => new(details);
        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T> Ok<T>(T value) => new CustomResult<T>.Ok_(value);
        public bool IsError => GetType().GetGenericTypeDefinition() == typeof(CustomResult<>.Error_);
        public bool IsOk => !IsError;
        public abstract Guid? GetErrorOrDefault();

        public static CustomResult<T> Try<T>(global::System.Func<T> action, global::System.Func<global::System.Exception, Guid> formatError)
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

        public static async global::System.Threading.Tasks.Task<CustomResult<T>> Try<T>(global::System.Func<global::System.Threading.Tasks.Task<T>> action, global::System.Func<global::System.Exception, Guid> formatError)
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

        public static CustomResult<T> Try<T>(global::System.Func<CustomResult<T>> action, global::System.Func<global::System.Exception, Guid> formatError)
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

        public static async global::System.Threading.Tasks.Task<CustomResult<T>> Try<T>(global::System.Func<global::System.Threading.Tasks.Task<CustomResult<T>>> action, global::System.Func<global::System.Exception, Guid> formatError)
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

    public abstract partial class CustomResult<T> : CustomResult, global::System.Collections.Generic.IEnumerable<T>
    {
        
        [global::System.Diagnostics.DebuggerNonUserCode]
        public static new CustomResult<T> Error(Guid message) => Error<T>(message);
        
        [global::System.Diagnostics.DebuggerNonUserCode]
        public static CustomResult<T> Ok(T value) => Ok<T>(value);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static implicit operator CustomResult<T>(T value) => CustomResult.Ok(value);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static implicit operator CustomResult<T>(CustomResultError myResultError) => myResultError.WithOk<T>();

        public static bool operator true(CustomResult<T> result) => result.IsOk;
        public static bool operator false(CustomResult<T> result) => result.IsError;

        public static bool operator !(CustomResult<T> result) => result.IsError;

        //just here to suppress warning, never called because all subtypes (Ok_, Error_) implement Equals and GetHashCode
        bool Equals(CustomResult<T> other) => this switch
        {
            Ok_ ok => ok.Equals((object)other),
            Error_ error => error.Equals((object)other),
            _ => throw new global::System.InvalidOperationException($"Unexpected type derived from {nameof(CustomResult<T>)}")
        };

        public override int GetHashCode() => this switch
        {
            Ok_ ok => ok.GetHashCode(),
            Error_ error => error.GetHashCode(),
            _ => throw new global::System.InvalidOperationException($"Unexpected type derived from {nameof(CustomResult<T>)}")
        };

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomResult<T>)obj);
        }

        public static bool operator ==(CustomResult<T>? left, CustomResult<T>? right) => Equals(left, right);

        public static bool operator !=(CustomResult<T>? left, CustomResult<T>? right) => !Equals(left, right);

        [global::System.Diagnostics.DebuggerStepThrough]
        public void Match(global::System.Action<T> ok, global::System.Action<Guid>? error = null) => Match(
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
        public T1 Match<T1>(global::System.Func<T, T1> ok, global::System.Func<Guid, T1> error)
        {
            return this switch
            {
                Ok_ okCustomResult => ok(okCustomResult.Value),
                Error_ errorCustomResult => error(errorCustomResult.Details),
                _ => throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task<T1> Match<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok, global::System.Func<Guid, global::System.Threading.Tasks.Task<T1>> error)
        {
            return this switch
            {
                Ok_ okCustomResult => await ok(okCustomResult.Value).ConfigureAwait(false),
                Error_ errorCustomResult => await error(errorCustomResult.Details).ConfigureAwait(false),
                _ => throw new global::System.InvalidOperationException($"Unexpected derived result type: {GetType()}")
            };
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public global::System.Threading.Tasks.Task<T1> Match<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok, global::System.Func<Guid, T1> error) =>
            Match(ok, e => global::System.Threading.Tasks.Task.FromResult(error(e)));

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task Match(global::System.Func<T, global::System.Threading.Tasks.Task> ok)
        {
            if (this is Ok_ okCustomResult) await ok(okCustomResult.Value).ConfigureAwait(false);
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public T Match(global::System.Func<Guid, T> error) => Match(v => v, error);

        [global::System.Diagnostics.DebuggerStepThrough]
        public CustomResult<T1> Bind<T1>(global::System.Func<T, CustomResult<T1>> bind)
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

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task<CustomResult<T1>> Bind<T1>(global::System.Func<T, global::System.Threading.Tasks.Task<CustomResult<T1>>> bind)
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

        [global::System.Diagnostics.DebuggerStepThrough]
        public CustomResult<T1> Map<T1>(global::System.Func<T, T1> map)
        {
            switch (this)
            {
                case Ok_ ok:
                    try
                    {
                        return CustomResult.Ok(map(ok.Value));
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

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task<CustomResult<T1>> Map<T1>(
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> map)
        {
            switch (this)
            {
                case Ok_ ok:
                    try
                    {
                        return CustomResult.Ok(await map(ok.Value).ConfigureAwait(false));
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
        
        
        public static implicit operator global::FunicularSwitch.Generic.GenericResult<T, Guid>(CustomResult<T> result) => 
            result.Match(
                global::FunicularSwitch.Generic.GenericResult<T, Guid>.Ok,
                global::FunicularSwitch.Generic.GenericResult<T, Guid>.Error);
        
        public static implicit operator CustomResult<T>(global::FunicularSwitch.Generic.GenericResult<T, Guid> result) =>
            result.Match<CustomResult<T>>(
                CustomResult<T>.Ok,
                CustomResult<T>.Error);
        
        public global::FunicularSwitch.Generic.GenericResult<T, Guid> ToGenericResult() =>
            Match(
                global::FunicularSwitch.Generic.GenericResult<T, Guid>.Ok,
                global::FunicularSwitch.Generic.GenericResult<T, Guid>.Error);

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

        public sealed partial class Ok_ : CustomResult<T>
        {
            public T Value { get; }

            [global::System.Diagnostics.DebuggerStepThrough]
            public Ok_(T value) => Value = value;

            [global::System.Diagnostics.DebuggerStepThrough]
            public override Guid? GetErrorOrDefault() => null;

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

        public sealed partial class Error_ : CustomResult<T>
        {
            public Guid Details { get; }

            [global::System.Diagnostics.DebuggerStepThrough]
            public Error_(Guid details) => Details = details;

            [global::System.Diagnostics.DebuggerStepThrough]
            public CustomResult<T1>.Error_ Convert<T1>() => new CustomResult<T1>.Error_(Details);

            [global::System.Diagnostics.DebuggerStepThrough]
            public override Guid? GetErrorOrDefault() => Details;

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

    public readonly partial struct CustomResultError : global::System.IEquatable<CustomResultError>
    {
        readonly Guid _details;

        internal CustomResultError(Guid details) => _details = details;

        [Pure]
        public CustomResult<T> WithOk<T>() => CustomResult.Error<T>(_details);

        public bool Equals(CustomResultError other) => _details.Equals(other._details);

        public override bool Equals(object? obj) => obj is CustomResultError other && Equals(other);

        public override int GetHashCode() => _details.GetHashCode();

        public static bool operator ==(CustomResultError left, CustomResultError right) => left.Equals(right);

        public static bool operator !=(CustomResultError left, CustomResultError right) => !left.Equals(right);
    }

    public static partial class CustomResultExtension
    {
        #region bind

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<CustomResult<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, CustomResult<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<CustomResult<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<CustomResult<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<CustomResult<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<CustomResult<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> bind)
            => Bind(result, async v => CustomResult.Ok(await bind(v).ConfigureAwait(false)));

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T> MapError<T>(this CustomResult<T> result, global::System.Func<Guid, Guid> mapError)
        {
            if (result is CustomResult<T>.Error_ e)
                return CustomResult.Error<T>(mapError(e.Details));
            return result;
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<CustomResult<T>> MapError<T>(this global::System.Threading.Tasks.Task<CustomResult<T>> result, global::System.Func<Guid, Guid> mapError) => (await result.ConfigureAwait(false)).MapError(mapError);

        #endregion

        #region match

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
            global::System.Func<Guid, global::System.Threading.Tasks.Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
            global::System.Func<Guid, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result,
            global::System.Func<T, T1> ok,
            global::System.Func<Guid, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);

        #endregion

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T> Flatten<T>(this CustomResult<CustomResult<T>> result) => result.Bind(r => r);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T1> As<T, T1>(this CustomResult<T> result, global::System.Func<Guid> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return CustomResult.Error<T1>(errorTIsNotT1());
            });

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T1> As<T1>(this CustomResult<object> result, global::System.Func<Guid> errorIsNotT1) =>
            result.As<object, T1>(errorIsNotT1);

        #region query-expression pattern

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T1> Select<T, T1>(this CustomResult<T> result, global::System.Func<T, T1> selector) => result.Map(selector);
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<CustomResult<T1>> Select<T, T1>(this global::System.Threading.Tasks.Task<CustomResult<T>> result, global::System.Func<T, T1> selector) => result.Map(selector);

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T2> SelectMany<T, T1, T2>(this CustomResult<T> result, global::System.Func<T, CustomResult<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<CustomResult<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<CustomResult<T>> result, global::System.Func<T, global::System.Threading.Tasks.Task<CustomResult<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<CustomResult<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<CustomResult<T>> result, global::System.Func<T, CustomResult<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        public static global::System.Threading.Tasks.Task<CustomResult<T2>> SelectMany<T, T1, T2>(this CustomResult<T> result, global::System.Func<T, global::System.Threading.Tasks.Task<CustomResult<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        #endregion
        
        
        public static CustomResult<T> ToCustomResult<T>(
            this global::FunicularSwitch.Generic.GenericResult<T, Guid> result) =>
                result.Match<CustomResult<T>>(
                    CustomResult<T>.Ok,
                    CustomResult<T>.Error);
        
        public static global::System.Threading.Tasks.Task<CustomResult<T>> ToCustomResult<T>(
            this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generic.GenericResult<T, Guid>> result) =>
                result.Match(
                    CustomResult<T>.Ok,
                    CustomResult<T>.Error);
        
        public static global::System.Threading.Tasks.Task<global::FunicularSwitch.Generic.GenericResult<T, Guid>> ToGenericResult<T>(
            this global::System.Threading.Tasks.Task<CustomResult<T>> result) =>
                result.Match(
                    global::FunicularSwitch.Generic.GenericResult<T, Guid>.Ok,
                    global::FunicularSwitch.Generic.GenericResult<T, Guid>.Error);
    }
}

namespace FunicularSwitch.Generators.Consumer.Extensions
{
    public static partial class CustomResultExtension
    {
        public static global::System.Collections.Generic.IEnumerable<T1> Choose<T, T1>(
            this global::System.Collections.Generic.IEnumerable<T> items,
            global::System.Func<T, CustomResult<T1>> choose,
            global::System.Action<Guid> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        public static global::System.Collections.Generic.IEnumerable<T> Choose<T>(
            this global::System.Collections.Generic.IEnumerable<CustomResult<T>> results,
            global::System.Action<Guid> onError)
            => results
                .Where(r =>
                    r.Match(_ => true, error =>
                    {
                        onError(error);
                        return false;
                    }))
                .Select(r => r.GetValueOrThrow());

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T> As<T>(this object? item, global::System.Func<Guid> error) =>
            !(item is T t) ? CustomResult.Error<T>(error()) : t;

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<T> NotNull<T>(this T? item, global::System.Func<Guid> error) =>
            item ?? CustomResult.Error<T>(error());

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<string> NotNullOrEmpty(this string? s, global::System.Func<Guid> error)
            => string.IsNullOrEmpty(s) ? CustomResult.Error<string>(error()) : s!;

        [global::System.Diagnostics.DebuggerStepThrough]
        public static CustomResult<string> NotNullOrWhiteSpace(this string? s, global::System.Func<Guid> error)
            => string.IsNullOrWhiteSpace(s) ? CustomResult.Error<string>(error()) : s!;

        public static CustomResult<T> First<T>(this global::System.Collections.Generic.IEnumerable<T> candidates, global::System.Func<T, bool> predicate, global::System.Func<Guid> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
#pragma warning restore 1591
}
