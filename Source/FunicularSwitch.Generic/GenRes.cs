using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public readonly partial record struct GenOk<TOk>
{
    private Option<TOk> Value { get; }

    internal GenOk(TOk value) => Value = value;
    
    [Pure]
    public GenRes<TOk, TError>WithError<TError>() =>
        GenRes<TOk, TError>.Ok(Value.GetValueOrThrow());

    [Pure]
    public GenRes<TOkReturn, TError> Bind<TOkReturn, TError>(
        Func<TOk, GenRes<TOkReturn, TError>> bind) =>
        bind(Value.GetValueOrThrow());

    [Pure]
    public async Task<GenRes<TOkReturn, TError>> BindAsync<TOkReturn, TError>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> bind) =>
        await bind(Value.GetValueOrThrow()).ConfigureAwait(false);

    #region LinqQueryExpressionInterop
    
    [Pure]
    public GenRes<TOkReturn, TError> Select<TOkReturn, TError>(
        Func<TOk, TOkReturn> selector) => Bind(value => GenRes<TOkReturn, TError>.Ok(selector(value)));

    [Pure]
    public GenRes<TOkReturn, TError> SelectMany<TOkReturn, TError>(
        Func<TOk, GenRes<TOkReturn, TError>> selector) => Bind(selector);

    [Pure]
    public GenRes<TOkReturn, TError> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, GenRes<TOkSelectReturn, TError>> selector,
        Func<TOk, TOkSelectReturn, TOkReturn> resultSelector)
    {
        var intermediateResult = selector(Value.GetValueOrThrow());
        if (intermediateResult.IsError())
            return GenRes<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TOkReturn, TError>.Ok(resultSelector(Value.GetValueOrThrow(), intermediateValue));
    }

    [Pure]
    public async Task<GenRes<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, Task<GenRes<TOkSelectReturn, TError>>> selector,
        Func<TOk, TOkSelectReturn, TOkReturn> resultSelector)
    {
        var intermediateResult = await selector(Value.GetValueOrThrow()).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenRes<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TOkReturn, TError>.Ok(resultSelector(Value.GetValueOrThrow(), intermediateValue));
    }

    [Pure]
    public async Task<GenRes<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, GenRes<TOkSelectReturn, TError>> selector,
        Func<TOk, TOkSelectReturn, Task<TOkReturn>> resultSelector)
    {
        var intermediateResult = selector(Value.GetValueOrThrow());
        if (intermediateResult.IsError())
            return GenRes<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TOkReturn, TError>.Ok(await resultSelector(Value.GetValueOrThrow(), intermediateValue).ConfigureAwait(false));
    }

    [Pure]
    public async Task<GenRes<TOkReturn, TError>> SelectMany<TOkReturn, TOkSelectReturn, TError>(
        Func<TOk, Task<GenRes<TOkSelectReturn, TError>>> selector,
        Func<TOk, TOkSelectReturn, Task<TOkReturn>> resultSelector)
    {
        var intermediateResult = await selector(Value.GetValueOrThrow()).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenRes<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TOkReturn, TError>.Ok(await resultSelector(Value.GetValueOrThrow(), intermediateValue).ConfigureAwait(false));
    }

    #endregion
}

public readonly partial record struct GenError<TError>
{
    private Option<TError> Value { get; }

    internal GenError(TError value) => Value = value;
    
    [Pure]
    public GenRes<TOk, TError>WithOk<TOk>() =>
        GenRes<TOk, TError>.Error(Value.GetValueOrThrow());
}

public static partial class GenRes
{
    [Pure]
    public static GenOk<TOk> Ok<TOk>(TOk value) => new(value);
    
    [Pure]
    public static GenRes<TOk, TError> Ok<TOk, TError>(TOk value) => GenRes<TOk, TError>.Ok(value);
    
    [Pure]
    public static GenError<TError> Error<TError>(TError error) => new(error);
    
    [Pure]
    public static GenRes<TOk, TError> Error<TOk, TError>(TError error) => GenRes<TOk, TError>.Error(error);
}

public readonly partial record struct GenRes<TOk, TError>
{
    private readonly Option<TOk> _value;
    private readonly Option<TError> _error;


    private GenRes(Option<TOk> value, Option<TError> error)
    {
        _value = value;
        _error = error;
    }

    [Pure]
    public static GenRes<TOk, TError> Ok(TOk value) => new(Option.Some(value), Option.None<TError>());
    [Pure]
    public static GenRes<TOk, TError> Error(TError error) => new(Option.None<TOk>(), Option.Some(error));

    [Pure]
    public static implicit operator GenRes<TOk, TError>(GenOk<TOk> ok) => ok.WithError<TError>();
    
    [Pure]
    public static implicit operator GenRes<TOk, TError>(TOk value) => Ok(value);

    [Pure]
    public static implicit operator GenRes<TOk, TError>(GenError<TError> error) => error.WithOk<TOk>();


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
    public TOk GetValueOrDefault(TOk defaultValue) => _value.GetValueOrDefault(defaultValue);
    [Pure]
    public TOk GetValueOrDefault(Func<TOk> defaultValue) => _value.GetValueOrDefault(defaultValue);
    [Pure]
    public Task<TOk> GetValueOrDefaultAsync(Func<Task<TOk>> defaultValue) => _value.GetValueOrDefault(defaultValue);


    [Pure]
    public TError GetErrorOrDefault(TError defaultValue) => _error.GetValueOrDefault(defaultValue);
    [Pure]
    public TError GetErrorOrDefault(Func<TError> defaultValue) => _error.GetValueOrDefault(defaultValue);
    [Pure]
    public Task<TError> GetErrorOrDefaultAsync(Func<Task<TError>> defaultValue) =>
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
    public Task<GenRes<TOk, TError>> ToTask() => Task.FromResult(this);


    [Pure]
    public GenRes<TOk, TError> Do(Action<TOk> action)
    {
        if (IsOk()) action(GetValueOrThrow());
        return this;
    }
    
    [Pure]
    public async Task<GenRes<TOk, TError>> DoAsync(Func<TOk, Task> action)
    {
        if (IsOk()) await action(GetValueOrThrow());
        return this;
    }
    
    [Pure]
    public GenRes<TOk, TError> DoOnError(Action<TError> action)
    {
        if (IsError()) action(GetErrorOrThrow());
        return this;
    }

    [Pure]
    public async Task<GenRes<TOk, TError>> DoOnErrorAsync(Func<TError, Task> action)
    {
        if (IsError()) await action(GetErrorOrThrow());
        return this;
    }


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
    public GenRes<TOkReturn, TError> Bind<TOkReturn>(
        Func<TOk, GenRes<TOkReturn, TError>> bind) =>
        IsOk()
            ? bind(GetValueOrThrow())
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public async Task<GenRes<TOkReturn, TError>> Bind<TOkReturn>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> bind) =>
        IsOk()
            ? await bind(GetValueOrThrow()).ConfigureAwait(false)
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public GenRes<TOkReturn, TError> Map<TOkReturn>(
        Func<TOk, TOkReturn> map) =>
        IsOk()
            ? GenRes<TOkReturn, TError>.Ok(map(GetValueOrThrow()))
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public async Task<GenRes<TOkReturn, TError>> Map<TOkReturn>(
        Func<TOk, Task<TOkReturn>> map) =>
        IsOk()
            ? GenRes<TOkReturn, TError>.Ok(await map(GetValueOrThrow()).ConfigureAwait(false))
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    [Pure]
    public GenRes<TOk, TErrorReturn> MapError<TErrorReturn>(
        Func<TError, TErrorReturn> map) =>
        IsOk()
            ? GenRes<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenRes<TOk, TErrorReturn>.Error(map(GetErrorOrThrow()));

    [Pure]
    public async Task<GenRes<TOk, TErrorReturn>> MapError<TErrorReturn>(
        Func<TError, Task<TErrorReturn>> map) =>
        IsOk()
            ? GenRes<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenRes<TOk, TErrorReturn>.Error(await map(GetErrorOrThrow()).ConfigureAwait(false));

    [Pure]
    public GenRes<TOkReturn, TError> Select<TOkReturn>(
        Func<TOk, TOkReturn> selector) => Map(selector);

    [Pure]
    public GenRes<TOkReturn, TError> SelectMany<TOkReturn>(
        Func<TOk, GenRes<TOkReturn, TError>> selector) => Bind(selector);

    [Pure]
    public GenRes<TOkReturn, TError> SelectMany<TOkOtherReturn, TOkReturn>(
        Func<TOk, GenRes<TOkOtherReturn, TError>> selector,
        Func<TOk, TOkOtherReturn, TOkReturn> resultSelector)
    {
        if (IsError()) return GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = selector(okValue);
        if (intermediateResult.IsError())
            return GenRes<TOkReturn, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TOkReturn, TError>.Ok(resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public Task<GenRes<TOkReturn, TError>> SelectMany<TOkReturn>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector) => Bind(selector);

    [Pure]
    public async Task<GenRes<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        if (IsError()) return GenRes<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = await selector(okValue).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenRes<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TSelect, TError>.Ok(resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public async Task<GenRes<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        if (IsError()) return GenRes<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = selector(okValue);
        if (intermediateResult.IsError())
            return GenRes<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue));
    }

    [Pure]
    public async Task<GenRes<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        if (IsError()) return GenRes<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = await selector(okValue).ConfigureAwait(false);
        if (intermediateResult.IsError())
            return GenRes<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue));
    }
}