using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public readonly partial record struct GenericOk<TOk>
{
    private Option<TOk> Value { get; }

    internal GenericOk(TOk value) => Value = value;

    [Pure]
    public GenericResult<TOk, TError> WithError<TError>() =>
        GenericResult<TOk, TError>.Ok(Value.GetValueOrThrow());

    [Pure]
    public GenericResult<TOkReturn, TError> Bind<TOkReturn, TError>(
        Func<TOk, GenericResult<TOkReturn, TError>> bind) =>
        bind(Value.GetValueOrThrow());

    [Pure]
    public async Task<GenericResult<TOkReturn, TError>> Bind<TOkReturn, TError>(
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> bind) =>
        await bind(Value.GetValueOrThrow()).ConfigureAwait(false);

    [Pure]
    public async ValueTask<GenericResult<TOkReturn, TError>> Bind<TOkReturn, TError>(
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> bind) =>
        await bind(Value.GetValueOrThrow()).ConfigureAwait(false);

    #region LinqQueryExpressionInterop

    [Pure]
    public GenericResult<TOkReturn, TError> Select<TOkReturn, TError>(
        Func<TOk, TOkReturn> selector) => Bind(value => GenericResult<TOkReturn, TError>.Ok(selector(value)));

    [Pure]
    public GenericResult<TOkReturn, TError> SelectMany<TOkReturn, TError>(
        Func<TOk, GenericResult<TOkReturn, TError>> selector) => Bind(selector);

    [Pure]
    public GenericResult<TOkReturn, TError> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, GenericResult<TOkSelectReturn, TError>> selector,
        Func<TOk, TOkSelectReturn, TOkReturn> resultSelector)
    {
        var intermediateResult = selector(Value.GetValueOrThrow());
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(resultSelector(Value.GetValueOrThrow(), intermediateValue));
    }

    [Pure]
    public async Task<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, Task<GenericResult<TOkSelectReturn, TError>>> selector,
        Func<TOk, TOkSelectReturn, TOkReturn> resultSelector)
    {
        var intermediateResult = await selector(Value.GetValueOrThrow()).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(resultSelector(Value.GetValueOrThrow(), intermediateValue));
    }

    [Pure]
    public async Task<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, GenericResult<TOkSelectReturn, TError>> selector,
        Func<TOk, TOkSelectReturn, Task<TOkReturn>> resultSelector)
    {
        var intermediateResult = selector(Value.GetValueOrThrow());
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(await resultSelector(Value.GetValueOrThrow(), intermediateValue).ConfigureAwait(false));
    }

    [Pure]
    public async Task<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, Task<GenericResult<TOkSelectReturn, TError>>> selector,
        Func<TOk, TOkSelectReturn, Task<TOkReturn>> resultSelector)
    {
        var intermediateResult = await selector(Value.GetValueOrThrow()).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(await resultSelector(Value.GetValueOrThrow(), intermediateValue).ConfigureAwait(false));
    }

    [Pure]
    public async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, ValueTask<GenericResult<TOkSelectReturn, TError>>> selector,
        Func<TOk, TOkSelectReturn, TOkReturn> resultSelector)
    {
        var intermediateResult = await selector(Value.GetValueOrThrow()).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(resultSelector(Value.GetValueOrThrow(), intermediateValue));
    }

    [Pure]
    public async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, GenericResult<TOkSelectReturn, TError>> selector,
        Func<TOk, TOkSelectReturn, ValueTask<TOkReturn>> resultSelector)
    {
        var intermediateResult = selector(Value.GetValueOrThrow());
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(await resultSelector(Value.GetValueOrThrow(), intermediateValue).ConfigureAwait(false));
    }

    [Pure]
    public async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, ValueTask<GenericResult<TOkSelectReturn, TError>>> selector,
        Func<TOk, TOkSelectReturn, ValueTask<TOkReturn>> resultSelector)
    {
        var intermediateResult = await selector(Value.GetValueOrThrow()).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(await resultSelector(Value.GetValueOrThrow(), intermediateValue).ConfigureAwait(false));
    }

    #endregion
}

public readonly partial record struct GenericError<TError>
{
    private Option<TError> Value { get; }

    internal GenericError(TError value) => Value = value;

    [Pure]
    public GenericResult<TOk, TError> WithOk<TOk>() =>
        GenericResult<TOk, TError>.Error(Value.GetValueOrThrow());
}

public static partial class GenericResult
{
    [Pure]
    public static GenericOk<TOk> Ok<TOk>(TOk value) => new(value);

    [Pure]
    public static GenericResult<TOk, TError> Ok<TOk, TError>(TOk value) => GenericResult<TOk, TError>.Ok(value);

    [Pure]
    public static GenericError<TError> Error<TError>(TError error) => new(error);

    [Pure]
    public static GenericResult<TOk, TError> Error<TOk, TError>(TError error) => GenericResult<TOk, TError>.Error(error);
}

public readonly partial record struct GenericResult<TOk, TError>
{
    private readonly Option<TOk> _value;
    private readonly Option<TError> _error;


    private GenericResult(Option<TOk> value, Option<TError> error)
    {
        _value = value;
        _error = error;
    }

    [Pure]
    public static GenericResult<TOk, TError> Ok(TOk value) => new(Option.Some(value), Option.None<TError>());

    [Pure]
    public static GenericResult<TOk, TError> Error(TError error) => new(Option.None<TOk>(), Option.Some(error));

    [Pure]
    public static implicit operator GenericResult<TOk, TError>(GenericOk<TOk> ok) => ok.WithError<TError>();

    [Pure]
    public static implicit operator GenericResult<TOk, TError>(TOk value) => Ok(value);

    [Pure]
    public static implicit operator GenericResult<TOk, TError>(GenericError<TError> error) => error.WithOk<TOk>();


    [Pure]
    public bool IsOk()
    {
        if (_value.IsSome()) return true;
        if (_error.IsSome()) return false;
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    [Pure]
    public bool IsError()
    {
        if (_error.IsSome()) return true;
        if (_value.IsSome()) return false;
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    [Pure]
    public TOk GetValueOrThrow() => _value.GetValueOrThrow();

    [Pure]
    public TError GetErrorOrThrow() => _error.GetValueOrThrow();


    [Pure]
    public TOk GetValueOrDefault(TOk defaultValue) =>
        _value.GetValueOrDefault(defaultValue);

    [Pure]
    public async Task<TOk> GetValueOrDefault(Task<TOk> defaultValue) =>
        _value.GetValueOrDefault(await defaultValue.ConfigureAwait(false));

    [Pure]
    public async ValueTask<TOk> GetValueOrDefault(ValueTask<TOk> defaultValue) =>
        _value.GetValueOrDefault(await defaultValue.ConfigureAwait(false));

    [Pure]
    public TOk GetValueOrDefault(Func<TOk> defaultValue) =>
        _value.GetValueOrDefault(defaultValue);

    [Pure]
    public Task<TOk> GetValueOrDefault(Func<Task<TOk>> defaultValue) =>
        _value.GetValueOrDefault(defaultValue);

    [Pure]
    public ValueTask<TOk> GetValueOrDefault(Func<ValueTask<TOk>> defaultValue) =>
        _value.GetValueOrDefault(defaultValue);


    [Pure]
    public TError GetErrorOrDefault(TError defaultValue) =>
        _error.GetValueOrDefault(defaultValue);

    [Pure]
    public async Task<TError> GetErrorOrDefault(Task<TError> defaultValue) =>
        _error.GetValueOrDefault(await defaultValue.ConfigureAwait(false));

    [Pure]
    public async ValueTask<TError> GetErrorOrDefault(ValueTask<TError> defaultValue) =>
        _error.GetValueOrDefault(await defaultValue.ConfigureAwait(false));

    [Pure]
    public TError GetErrorOrDefault(Func<TError> defaultValue) =>
        _error.GetValueOrDefault(defaultValue);

    [Pure]
    public Task<TError> GetErrorOrDefault(Func<Task<TError>> defaultValue) =>
        _error.GetValueOrDefault(defaultValue);

    [Pure]
    public ValueTask<TError> GetErrorOrDefault(Func<ValueTask<TError>> defaultValue) =>
        _error.GetValueOrDefault(defaultValue);


    #region Option interop

    [Pure]
    public Option<TOk> ToOption() => _value;

    [Pure]
    public Option<TError> ToErrorOption() => _error;

    [Pure]
    public (Option<TOk>, Option<TError>) ToOptions() => (_value, _error);

    #endregion


    [Pure]
    public Task<GenericResult<TOk, TError>> ToTask() => Task.FromResult(this);

    [Pure]
    public ValueTask<GenericResult<TOk, TError>> ToValueTask() => new(this);


    [Pure]
    public GenericResult<TOk, TError> Do(Action<TOk> action)
    {
        if (IsOk()) action(GetValueOrThrow());
        return this;
    }

    [Pure]
    public async Task<GenericResult<TOk, TError>> Do(Func<TOk, Task> action)
    {
        if (IsOk()) await action(GetValueOrThrow());
        return this;
    }

    [Pure]
    public async ValueTask<GenericResult<TOk, TError>> Do(Func<TOk, ValueTask> action)
    {
        if (IsOk()) await action(GetValueOrThrow());
        return this;
    }

    [Pure]
    public GenericResult<TOk, TError> DoOnError(Action<TError> action)
    {
        if (IsError()) action(GetErrorOrThrow());
        return this;
    }

    [Pure]
    public async Task<GenericResult<TOk, TError>> DoOnError(Func<TError, Task> action)
    {
        if (IsError()) await action(GetErrorOrThrow());
        return this;
    }

    [Pure]
    public async ValueTask<GenericResult<TOk, TError>> DoOnError(Func<TError, ValueTask> action)
    {
        if (IsError()) await action(GetErrorOrThrow());
        return this;
    }

    #region Match

    [Pure]
    public TReturn Match<TReturn>(
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error) =>
        IsOk()
            ? ok(GetValueOrThrow())
            : error(GetErrorOrThrow());

    [Pure]
    public async Task<TReturn> Match<TReturn>(
        Func<TOk, Task<TReturn>> ok,
        Func<TError, TReturn> error) =>
        IsOk()
            ? await ok(GetValueOrThrow()).ConfigureAwait(false)
            : error(GetErrorOrThrow());

    [Pure]
    public async Task<TReturn> Match<TReturn>(
        Func<TOk, TReturn> ok,
        Func<TError, Task<TReturn>> error) =>
        IsOk()
            ? ok(GetValueOrThrow())
            : await error(GetErrorOrThrow()).ConfigureAwait(false);

    [Pure]
    public async Task<TReturn> Match<TReturn>(
        Func<TOk, Task<TReturn>> ok,
        Func<TError, Task<TReturn>> error) =>
        IsOk()
            ? await ok(GetValueOrThrow()).ConfigureAwait(false)
            : await error(GetErrorOrThrow()).ConfigureAwait(false);

    [Pure]
    public async ValueTask<TReturn> Match<TReturn>(
        Func<TOk, ValueTask<TReturn>> ok,
        Func<TError, TReturn> error) =>
        IsOk()
            ? await ok(GetValueOrThrow()).ConfigureAwait(false)
            : error(GetErrorOrThrow());

    [Pure]
    public async ValueTask<TReturn> Match<TReturn>(
        Func<TOk, TReturn> ok,
        Func<TError, ValueTask<TReturn>> error) =>
        IsOk()
            ? ok(GetValueOrThrow())
            : await error(GetErrorOrThrow()).ConfigureAwait(false);

    [Pure]
    public async ValueTask<TReturn> Match<TReturn>(
        Func<TOk, ValueTask<TReturn>> ok,
        Func<TError, ValueTask<TReturn>> error) =>
        IsOk()
            ? await ok(GetValueOrThrow()).ConfigureAwait(false)
            : await error(GetErrorOrThrow()).ConfigureAwait(false);

    #endregion

    #region Bind

    [Pure]
    public GenericResult<TOkReturn, TError> Bind<TOkReturn>(
        Func<TOk, GenericResult<TOkReturn, TError>> bind) =>
        IsOk()
            ? bind(GetValueOrThrow())
            : GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public async Task<GenericResult<TOkReturn, TError>> Bind<TOkReturn>(
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> bind) =>
        IsOk()
            ? await bind(GetValueOrThrow()).ConfigureAwait(false)
            : GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public async ValueTask<GenericResult<TOkReturn, TError>> Bind<TOkReturn>(
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> bind) =>
        IsOk()
            ? await bind(GetValueOrThrow()).ConfigureAwait(false)
            : GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());

    #endregion


    #region Map

    [Pure]
    public GenericResult<TOkReturn, TError> Map<TOkReturn>(
        Func<TOk, TOkReturn> map) =>
        IsOk()
            ? GenericResult<TOkReturn, TError>.Ok(map(GetValueOrThrow()))
            : GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public async Task<GenericResult<TOkReturn, TError>> Map<TOkReturn>(
        Func<TOk, Task<TOkReturn>> map) =>
        IsOk()
            ? GenericResult<TOkReturn, TError>.Ok(await map(GetValueOrThrow()).ConfigureAwait(false))
            : GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public async ValueTask<GenericResult<TOkReturn, TError>> Map<TOkReturn>(
        Func<TOk, ValueTask<TOkReturn>> map) =>
        IsOk()
            ? GenericResult<TOkReturn, TError>.Ok(await map(GetValueOrThrow()).ConfigureAwait(false))
            : GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());

    #endregion


    #region MapError

    [Pure]
    public GenericResult<TOk, TErrorReturn> MapError<TErrorReturn>(
        Func<TError, TErrorReturn> map) =>
        IsOk()
            ? GenericResult<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenericResult<TOk, TErrorReturn>.Error(map(GetErrorOrThrow()));

    [Pure]
    public async Task<GenericResult<TOk, TErrorReturn>> MapError<TErrorReturn>(
        Func<TError, Task<TErrorReturn>> map) =>
        IsOk()
            ? GenericResult<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenericResult<TOk, TErrorReturn>.Error(await map(GetErrorOrThrow()).ConfigureAwait(false));

    [Pure]
    public async ValueTask<GenericResult<TOk, TErrorReturn>> MapError<TErrorReturn>(
        Func<TError, ValueTask<TErrorReturn>> map) =>
        IsOk()
            ? GenericResult<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenericResult<TOk, TErrorReturn>.Error(await map(GetErrorOrThrow()).ConfigureAwait(false));

    #endregion


    [Pure]
    public GenericResult<TOkReturn, TError> Select<TOkReturn>(
        Func<TOk, TOkReturn> selector) => Map(selector);

    [Pure]
    public GenericResult<TOkReturn, TError> SelectMany<TOkReturn>(
        Func<TOk, GenericResult<TOkReturn, TError>> selector) => Bind(selector);

    [Pure]
    public GenericResult<TOkReturn, TError> SelectMany<TOkOtherReturn, TOkReturn>(
        Func<TOk, GenericResult<TOkOtherReturn, TError>> selector,
        Func<TOk, TOkOtherReturn, TOkReturn> resultSelector)
    {
        if (IsError()) return GenericResult<TOkReturn, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = selector(okValue);
        if (intermediateResult.IsError())
            return GenericResult<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TOkReturn, TError>.Ok(resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public Task<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn>(
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> selector) => Bind(selector);

    [Pure]
    public async Task<GenericResult<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        if (IsError()) return GenericResult<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = await selector(okValue).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TSelect, TError>.Ok(resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public async Task<GenericResult<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        if (IsError()) return GenericResult<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = selector(okValue);
        if (intermediateResult.IsError())
            return GenericResult<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public async Task<GenericResult<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        if (IsError()) return GenericResult<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = await selector(okValue).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOkReturn>(
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector) => Bind(selector);

    [Pure]
    public async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        if (IsError()) return GenericResult<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = await selector(okValue).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TSelect, TError>.Ok(resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, ValueTask<TSelect>> resultSelector)
    {
        if (IsError()) return GenericResult<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = selector(okValue);
        if (intermediateResult.IsError())
            return GenericResult<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue).ConfigureAwait(false));
    }

    [Pure]
    public async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, ValueTask<TSelect>> resultSelector)
    {
        if (IsError()) return GenericResult<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = await selector(okValue).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenericResult<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenericResult<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue).ConfigureAwait(false));
    }
}