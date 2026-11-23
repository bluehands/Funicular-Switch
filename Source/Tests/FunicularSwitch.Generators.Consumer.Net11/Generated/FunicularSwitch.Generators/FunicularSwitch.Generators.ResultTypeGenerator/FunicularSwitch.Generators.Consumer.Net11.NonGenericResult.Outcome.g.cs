#nullable enable

using global::System.Linq;
using System;
using FunicularSwitch.Generic;

namespace FunicularSwitch.Generators.Consumer.Net11.NonGenericResult
{
#pragma warning disable 1591
    public partial class Outcome
    {
        [global::System.Diagnostics.DebuggerStepThrough]
        [global::System.Diagnostics.Contracts.PureAttribute]
        public static Outcome<T> Error<T>(String details) => new Outcome<T>.Error_(details);
        [global::System.Diagnostics.DebuggerStepThrough]
        [global::System.Diagnostics.Contracts.PureAttribute]
        public static OutcomeError Error(String details) => new(details);
        [global::System.Diagnostics.DebuggerStepThrough]
        [global::System.Diagnostics.Contracts.PureAttribute]
        public static Outcome<T> Ok<T>(T value) => new Outcome<T>.Ok_(value);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T> Try<T>( global::System.Func<T> action,  global::System.Func<global::System.Exception, String> formatError)
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
        
        public static async global::System.Threading.Tasks.Task<Outcome<T>> Try<T>( global::System.Func<global::System.Threading.Tasks.Task<T>> action,  global::System.Func<global::System.Exception, String> formatError)
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
        
        public static Outcome<T> Try<T>( global::System.Func<Outcome<T>> action,  global::System.Func<global::System.Exception, String> formatError)
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
        
        public static async global::System.Threading.Tasks.Task<Outcome<T>> Try<T>( global::System.Func<global::System.Threading.Tasks.Task<Outcome<T>>> action,  global::System.Func<global::System.Exception, String> formatError)
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

    public closed partial class Outcome<T> : global::System.Collections.Generic.IEnumerable<T>
    {

        [global::System.Diagnostics.DebuggerNonUserCode]
        
        public static Outcome<T> Error(String message) => Outcome.Error<T>(message);

        [global::System.Diagnostics.DebuggerNonUserCode]
        
        public static Outcome<T> Ok(T value) => Outcome.Ok<T>(value);

        [global::System.Diagnostics.Contracts.PureAttribute]
        public abstract String? GetErrorOrDefault();

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static implicit operator Outcome<T>(T value) => Outcome.Ok(value);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static implicit operator Outcome<T>(OutcomeError myResultError) => myResultError.WithOk<T>();

        //just here to suppress warning, never called because all subtypes (Ok_, Error_) implement Equals and GetHashCode
        bool Equals(Outcome<T> other) => this switch
        {
            Ok_ ok => ok.Equals((object)other),
            Error_ error => error.Equals((object)other),
        };

        public override int GetHashCode() => this switch
        {
            Ok_ ok => ok.GetHashCode(),
            Error_ error => error.GetHashCode(),
        };

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Outcome<T>)obj);
        }
        [global::System.Diagnostics.Contracts.PureAttribute]
        public static bool operator ==(Outcome<T>? left, Outcome<T>? right) => Equals(left, right);
        [global::System.Diagnostics.Contracts.PureAttribute]
        public static bool operator !=(Outcome<T>? left, Outcome<T>? right) => !Equals(left, right);

        [global::System.Diagnostics.DebuggerStepThrough]
        public void Match( global::System.Action<T> ok,  global::System.Action<String>? error = null) => Match(
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
        
        public T1 Match<T1>( global::System.Func<T, T1> ok,  global::System.Func<String, T1> error)
        {
            return this switch
            {
                Ok_ okOutcome => ok(okOutcome.Value),
                Error_ errorOutcome => error(errorOutcome.Details),
            };
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public async global::System.Threading.Tasks.Task<T1> Match<T1>( global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,  global::System.Func<String, global::System.Threading.Tasks.Task<T1>> error)
        {
            return this switch
            {
                Ok_ okOutcome => await ok(okOutcome.Value).ConfigureAwait(false),
                Error_ errorOutcome => await error(errorOutcome.Details).ConfigureAwait(false),
            };
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public global::System.Threading.Tasks.Task<T1> Match<T1>( global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,  global::System.Func<String, T1> error) =>
            Match(ok, e => global::System.Threading.Tasks.Task.FromResult(error(e)));

        [global::System.Diagnostics.DebuggerStepThrough]
        public async global::System.Threading.Tasks.Task Match( global::System.Func<T, global::System.Threading.Tasks.Task> ok)
        {
            if (this is Ok_ okOutcome) await ok(okOutcome.Value).ConfigureAwait(false);
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public T Match( global::System.Func<String, T> error) => Match(v => v, error);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public Outcome<T1> Bind<T1>( global::System.Func<T, Outcome<T1>> bind)
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
        
        public async global::System.Threading.Tasks.Task<Outcome<T1>> Bind<T1>( global::System.Func<T, global::System.Threading.Tasks.Task<Outcome<T1>>> bind)
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
        
        public Outcome<T1> Map<T1>( global::System.Func<T, T1> map)
        {
            switch (this)
            {
                case Ok_ ok:
                    try
                    {
                        return Outcome.Ok(map(ok.Value));
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
        
        public async global::System.Threading.Tasks.Task<Outcome<T1>> Map<T1>(
             global::System.Func<T, global::System.Threading.Tasks.Task<T1>> map)
        {
            switch (this)
            {
                case Ok_ ok:
                    try
                    {
                        return Outcome.Ok(await map(ok.Value).ConfigureAwait(false));
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

        
        public static implicit operator global::FunicularSwitch.Generic.GenericResult<T, String>(Outcome<T> result) => 
            result.Match(
                global::FunicularSwitch.Generic.GenericResult<T, String>.Ok,
                global::FunicularSwitch.Generic.GenericResult<T, String>.Error);
        
        public static implicit operator Outcome<T>(global::FunicularSwitch.Generic.GenericResult<T, String> result) =>
            result.Match<Outcome<T>>(
                Outcome<T>.Ok,
                Outcome<T>.Error);
        
        public global::FunicularSwitch.Generic.GenericResult<T, String> ToGenericResult() =>
            Match(
                global::FunicularSwitch.Generic.GenericResult<T, String>.Ok,
                global::FunicularSwitch.Generic.GenericResult<T, String>.Error);

        [global::System.Diagnostics.DebuggerStepThrough]
        [global::System.Diagnostics.Contracts.PureAttribute]
        public T? GetValueOrDefault()
            => Match(
                v => (T?)v,
                _ => default
            );

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public T GetValueOrDefault( global::System.Func<T> defaultValue)
            => Match(
                v => v,
                _ => defaultValue()
            );

        [global::System.Diagnostics.DebuggerStepThrough]
        [global::System.Diagnostics.Contracts.PureAttribute]
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

        public sealed partial class Ok_ : Outcome<T>
        {
            public T Value { get; }

            [global::System.Diagnostics.DebuggerStepThrough]
            public Ok_(T value) => Value = value;

            [global::System.Diagnostics.DebuggerStepThrough]
            [global::System.Diagnostics.Contracts.PureAttribute]
            public override String? GetErrorOrDefault() => null;

            [global::System.Diagnostics.Contracts.PureAttribute]
            public bool Equals(Ok_? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(Value, other.Value);
            }

            [global::System.Diagnostics.Contracts.PureAttribute]
            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is Ok_ other && Equals(other);
            }

            [global::System.Diagnostics.Contracts.PureAttribute]
            public override int GetHashCode() => Value == null ? 0 : global::System.Collections.Generic.EqualityComparer<T>.Default.GetHashCode(Value);

            [global::System.Diagnostics.Contracts.PureAttribute]
            public static bool operator ==(Ok_ left, Ok_ right) => Equals(left, right);

            [global::System.Diagnostics.Contracts.PureAttribute]
            public static bool operator !=(Ok_ left, Ok_ right) => !Equals(left, right);
        }

        public sealed partial class Error_ : Outcome<T>
        {
            public String Details { get; }

            [global::System.Diagnostics.DebuggerStepThrough]
            public Error_(String details) => Details = details;

            [global::System.Diagnostics.DebuggerStepThrough]
            [global::System.Diagnostics.Contracts.PureAttribute]
            public Outcome<T1>.Error_ Convert<T1>() => new Outcome<T1>.Error_(Details);

            [global::System.Diagnostics.DebuggerStepThrough]
            [global::System.Diagnostics.Contracts.PureAttribute]
            public override String? GetErrorOrDefault() => Details;

            [global::System.Diagnostics.Contracts.PureAttribute]
            public bool Equals(Error_? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(Details, other.Details);
            }

            [global::System.Diagnostics.Contracts.PureAttribute]
            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is Error_ other && Equals(other);
            }

            [global::System.Diagnostics.Contracts.PureAttribute]
            public override int GetHashCode() => Details.GetHashCode();

            [global::System.Diagnostics.Contracts.PureAttribute]
            public static bool operator ==(Error_ left, Error_ right) => Equals(left, right);

            [global::System.Diagnostics.Contracts.PureAttribute]
            public static bool operator !=(Error_ left, Error_ right) => !Equals(left, right);
        }
    }

    public readonly partial struct OutcomeError : global::System.IEquatable<OutcomeError>
    {
        readonly String _details;

        internal OutcomeError(String details) => _details = details;

        [global::System.Diagnostics.Contracts.Pure]
        public Outcome<T> WithOk<T>() => Outcome.Error<T>(_details);

        [global::System.Diagnostics.Contracts.PureAttribute]
        public bool Equals(OutcomeError other) => _details.Equals(other._details);

        [global::System.Diagnostics.Contracts.PureAttribute]
        public override bool Equals(object? obj) => obj is OutcomeError other && Equals(other);

        [global::System.Diagnostics.Contracts.PureAttribute]
        public override int GetHashCode() => _details.GetHashCode();

        [global::System.Diagnostics.Contracts.PureAttribute]
        public static bool operator ==(OutcomeError left, OutcomeError right) => left.Equals(right);

        [global::System.Diagnostics.Contracts.PureAttribute]
        public static bool operator !=(OutcomeError left, OutcomeError right) => !left.Equals(right);
    }

    public static partial class OutcomeExtension
    {
        #region bind

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<Outcome<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, Outcome<T1>> bind)
            => (await result.ConfigureAwait(false)).Bind(bind);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<Outcome<T1>> Bind<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, global::System.Threading.Tasks.Task<Outcome<T1>>> bind)
            => await (await result.ConfigureAwait(false)).Bind(bind).ConfigureAwait(false);

        #endregion

        #region map

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<Outcome<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, T1> map)
            => (await result.ConfigureAwait(false)).Map(map);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static global::System.Threading.Tasks.Task<Outcome<T1>> Map<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, global::System.Threading.Tasks.Task<T1>> bind)
            => Bind(result, async v => Outcome.Ok(await bind(v).ConfigureAwait(false)));

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T> MapError<T>(this Outcome<T> result,  global::System.Func<String, String> mapError)
        {
            if (result is Outcome<T>.Error_ e)
                return Outcome.Error<T>(mapError(e.Details));
            return result;
        }

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<Outcome<T>> MapError<T>(this global::System.Threading.Tasks.Task<Outcome<T>> result,  global::System.Func<String, String> mapError) => (await result.ConfigureAwait(false)).MapError(mapError);

        #endregion

        #region match

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
             global::System.Func<String, global::System.Threading.Tasks.Task<T1>> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, global::System.Threading.Tasks.Task<T1>> ok,
             global::System.Func<String, T1> error)
            => await (await result.ConfigureAwait(false)).Match(ok, error).ConfigureAwait(false);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static async global::System.Threading.Tasks.Task<T1> Match<T, T1>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result,
             global::System.Func<T, T1> ok,
             global::System.Func<String, T1> error)
            => (await result.ConfigureAwait(false)).Match(ok, error);

        #endregion

        [global::System.Diagnostics.DebuggerStepThrough]
        [global::System.Diagnostics.Contracts.PureAttribute]
        public static Outcome<T> Flatten<T>(this Outcome<Outcome<T>> result) => result.Bind(r => r);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T1> As<T, T1>(this Outcome<T> result,  global::System.Func<String> errorTIsNotT1) =>
            result.Bind(r =>
            {
                if (r is T1 converted)
                    return converted;
                return Outcome.Error<T1>(errorTIsNotT1());
            });

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T1> As<T1>(this Outcome<object> result,  global::System.Func<String> errorIsNotT1) =>
            result.As<object, T1>(errorIsNotT1);

        #region query-expression pattern

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T1> Select<T, T1>(this Outcome<T> result, global::System.Func<T, T1> selector) => result.Map(selector);
        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static global::System.Threading.Tasks.Task<Outcome<T1>> Select<T, T1>(this global::System.Threading.Tasks.Task<Outcome<T>> result, global::System.Func<T, T1> selector) => result.Map(selector);

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T2> SelectMany<T, T1, T2>(this Outcome<T> result, global::System.Func<T, Outcome<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static global::System.Threading.Tasks.Task<Outcome<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<Outcome<T>> result, global::System.Func<T, global::System.Threading.Tasks.Task<Outcome<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static global::System.Threading.Tasks.Task<Outcome<T2>> SelectMany<T, T1, T2>(this global::System.Threading.Tasks.Task<Outcome<T>> result, global::System.Func<T, Outcome<T1>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));
        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static global::System.Threading.Tasks.Task<Outcome<T2>> SelectMany<T, T1, T2>(this Outcome<T> result, global::System.Func<T, global::System.Threading.Tasks.Task<Outcome<T1>>> selector, global::System.Func<T, T1, T2> resultSelector) => result.Bind(t => selector(t).Map(t1 => resultSelector(t, t1)));

        #endregion

        
        public static Outcome<T> ToOutcome<T>(
            this global::FunicularSwitch.Generic.GenericResult<T, String> result) =>
                result.Match<Outcome<T>>(
                    Outcome<T>.Ok,
                    Outcome<T>.Error);
        
        public static global::System.Threading.Tasks.Task<Outcome<T>> ToOutcome<T>(
            this global::System.Threading.Tasks.Task<global::FunicularSwitch.Generic.GenericResult<T, String>> result) =>
                result.Match(
                    Outcome<T>.Ok,
                    Outcome<T>.Error);
        
        public static global::System.Threading.Tasks.Task<global::FunicularSwitch.Generic.GenericResult<T, String>> ToGenericResult<T>(
            this global::System.Threading.Tasks.Task<Outcome<T>> result) =>
                result.Match(
                    global::FunicularSwitch.Generic.GenericResult<T, String>.Ok,
                    global::FunicularSwitch.Generic.GenericResult<T, String>.Error);
    }
}

namespace FunicularSwitch.Generators.Consumer.Net11.NonGenericResult.Extensions
{
    public static partial class OutcomeExtension
    {
        
        public static global::System.Collections.Generic.IEnumerable<T1> Choose<T, T1>(
            this global::System.Collections.Generic.IEnumerable<T> items,
            global::System.Func<T, Outcome<T1>> choose,
            global::System.Action<String> onError)
            => items
                .Select(i => choose(i))
                .Choose(onError);

        
        public static global::System.Collections.Generic.IEnumerable<T> Choose<T>(
            this global::System.Collections.Generic.IEnumerable<Outcome<T>> results,
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
        
        public static Outcome<T> As<T>(this object? item,  global::System.Func<String> error) =>
            !(item is T t) ? Outcome.Error<T>(error()) : t;

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<T> NotNull<T>(this T? item,  global::System.Func<String> error) =>
            item ?? Outcome.Error<T>(error());

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<string> NotNullOrEmpty(this string? s,  global::System.Func<String> error)
            => string.IsNullOrEmpty(s) ? Outcome.Error<string>(error()) : s!;

        [global::System.Diagnostics.DebuggerStepThrough]
        
        public static Outcome<string> NotNullOrWhiteSpace(this string? s,  global::System.Func<String> error)
            => string.IsNullOrWhiteSpace(s) ? Outcome.Error<string>(error()) : s!;

        
        public static Outcome<T> First<T>(this global::System.Collections.Generic.IEnumerable<T> candidates,  global::System.Func<T, bool> predicate,  global::System.Func<String> noMatch) =>
            candidates
                .FirstOrDefault(i => predicate(i))
                .NotNull(noMatch);
    }
#pragma warning restore 1591
}
