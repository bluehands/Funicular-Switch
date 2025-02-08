using System;
using System.Threading.Tasks;

namespace FunicularSwitch;

public partial record struct GenOk<TOk>
{
    public TOk Value { get; }

    internal GenOk(TOk value) => Value = value;
}

public partial record struct GenError<TError>
{
    public TError Value { get; }

    internal GenError(TError value) => Value = value;
}

public static partial class GenRes
{
    public static GenOk<TOk> Ok<TOk>(TOk value) => new(value);
    public static GenError<TError> Error<TError>(TError error) => new(error);
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

    public static GenRes<TOk, TError> Ok(TOk value) => new(Option.Some(value), Option.None<TError>());
    public static GenRes<TOk, TError> Error(TError error) => new(Option.None<TOk>(), Option.Some(error));

    public static implicit operator GenRes<TOk, TError>(GenOk<TOk> ok) => Ok(ok.Value);
    public static implicit operator GenRes<TOk, TError>(GenError<TError> error) => Error(error.Value);

    public TReturn Match<TReturn>(Func<TOk, TReturn> ok, Func<TError, TReturn> error)
    {
        if (_value.IsSome()) return ok(_value.GetValueOrThrow());
        if (_error.IsSome()) return error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }
    
    public GenRes<TOkReturn, TError> Bind<TOkReturn>(Func<TOk, GenRes<TOkReturn, TError>> bind)
    {
        if (_value.IsSome()) return bind(_value.GetValueOrThrow());
        if (_error.IsSome()) return GenRes<TOkReturn, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }
    
    public GenRes<TOkReturn, TError> Map<TOkReturn>(Func<TOk, TOkReturn> map)
    {
        if (_value.IsSome()) return GenRes<TOkReturn, TError>.Ok(map(_value.GetValueOrThrow()));
        if (_error.IsSome()) return GenRes<TOkReturn, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }
    
    public GenRes<TOk, TErrorReturn> MapError<TErrorReturn>(Func<TError, TErrorReturn> map)
    {
        if (_value.IsSome()) return GenRes<TOk, TErrorReturn>.Ok(_value.GetValueOrThrow());
        if (_error.IsSome()) return GenRes<TOk, TErrorReturn>.Error(map(_error.GetValueOrThrow()));
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<TReturn> Match<TReturn>(Func<TOk, Task<TReturn>> ok, Func<TError, Task<TReturn>> error)
    {
        if (_value.IsSome()) return await ok(_value.GetValueOrThrow()).ConfigureAwait(false);
        if (_error.IsSome()) return await error(_error.GetValueOrThrow()).ConfigureAwait(false);
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<TReturn> Match<TReturn>(
        Func<TOk, Task<TReturn>> ok,
        Func<TError, TReturn> error)
    {
        if (_value.IsSome()) return await ok(_value.GetValueOrThrow()).ConfigureAwait(false);
        if (_error.IsSome()) return error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<TReturn> Match<TReturn>(
        Func<TOk, TReturn> ok,
        Func<TError, Task<TReturn>> error)
    {
        if (_value.IsSome()) return ok(_value.GetValueOrThrow());
        if (_error.IsSome()) return await error(_error.GetValueOrThrow()).ConfigureAwait(false);
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<GenRes<TOkReturn, TError>> Bind<TOkReturn>(Func<TOk, Task<GenRes<TOkReturn, TError>>> bind)
    {
        if (_value.IsSome()) return await bind(_value.GetValueOrThrow()).ConfigureAwait(false);
        if (_error.IsSome()) return GenRes<TOkReturn, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<GenRes<TOkReturn, TError>> Map<TOkReturn>(Func<TOk, Task<TOkReturn>> map)
    {
        if (_value.IsSome())
            return GenRes<TOkReturn, TError>.Ok(await map(_value.GetValueOrThrow()).ConfigureAwait(false));
        if (_error.IsSome()) return GenRes<TOkReturn, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<GenRes<TOk, TErrorReturn>> MapError<TErrorReturn>(Func<TError, Task<TErrorReturn>> map)
    {
        if (_value.IsSome()) return GenRes<TOk, TErrorReturn>.Ok(_value.GetValueOrThrow());
        if (_error.IsSome())
            return GenRes<TOk, TErrorReturn>.Error(await map(_error.GetValueOrThrow()).ConfigureAwait(false));
        throw new InvalidOperationException("Neither Ok nor Error");
    }
    
    public GenRes<TOkReturn, TError> Select<TOkReturn>(Func<TOk, TOkReturn> selector) => Map(selector);

    public GenRes<TOkReturn, TError> SelectMany<TOkReturn>(Func<TOk, GenRes<TOkReturn, TError>> selector) => Bind(selector);

    public GenRes<TSelect, TError> SelectMany<TOkReturn, TSelect>(
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        if (_value.IsSome())
        {
            var okValue = _value.GetValueOrThrow();
            var intermediateResult = selector(okValue);
            if (intermediateResult._value.IsSome())
            {
                var intermediateValue = intermediateResult._value.GetValueOrThrow();
                return GenRes<TSelect, TError>.Ok(resultSelector(okValue, intermediateValue));
            }
            if (intermediateResult._error.IsSome())
            {
                return GenRes<TSelect, TError>.Error(intermediateResult._error.GetValueOrThrow());
            }
        }
        if (_error.IsSome()) return GenRes<TSelect, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }
    
     public Task<GenRes<TOkReturn, TError>> SelectMany<TOkReturn>(Func<TOk, Task<GenRes<TOkReturn, TError>>> selector) => Bind(selector);

     public async Task<GenRes<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        if (_value.IsSome())
        {
            var okValue = _value.GetValueOrThrow();
            var intermediateResult = await selector(okValue).ConfigureAwait(false);
            if (intermediateResult._value.IsSome())
            {
                var intermediateValue = intermediateResult._value.GetValueOrThrow();
                return GenRes<TSelect, TError>.Ok(resultSelector(okValue, intermediateValue));
            }
            if (intermediateResult._error.IsSome())
            {
                return GenRes<TSelect, TError>.Error(intermediateResult._error.GetValueOrThrow());
            }
        }
        if (_error.IsSome()) return GenRes<TSelect, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<GenRes<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        if (_value.IsSome())
        {
            var okValue = _value.GetValueOrThrow();
            var intermediateResult = selector(okValue);
            if (intermediateResult._value.IsSome())
            {
                var intermediateValue = intermediateResult._value.GetValueOrThrow();
                return GenRes<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue).ConfigureAwait(false));
            }
            if (intermediateResult._error.IsSome())
            {
                return GenRes<TSelect, TError>.Error(intermediateResult._error.GetValueOrThrow());
            }
        }
        if (_error.IsSome()) return GenRes<TSelect, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public async Task<GenRes<TSelect, TError>> SelectMany<TOkReturn, TSelect>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        if (_value.IsSome())
        {
            var okValue = _value.GetValueOrThrow();
            var intermediateResult = await selector(okValue).ConfigureAwait(false);
            if (intermediateResult._value.IsSome())
            {
                var intermediateValue = intermediateResult._value.GetValueOrThrow();
                return GenRes<TSelect, TError>.Ok(await resultSelector(okValue, intermediateValue).ConfigureAwait(false));
            }
            if (intermediateResult._error.IsSome())
            {
                return GenRes<TSelect, TError>.Error(intermediateResult._error.GetValueOrThrow());
            }
        }
        if (_error.IsSome()) return GenRes<TSelect, TError>.Error(_error.GetValueOrThrow());
        throw new InvalidOperationException("Neither Ok nor Error");
    }
}

public static class GenResTaskExtensions
{
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Match(ok, error);
    }

    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TReturn>> ok,
        Func<TError, Task<TReturn>> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, error).ConfigureAwait(false);
    }

    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TReturn>> ok,
        Func<TError, TReturn> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, error).ConfigureAwait(false);
    }

    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TReturn> ok,
        Func<TError, Task<TReturn>> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, error).ConfigureAwait(false);
    }

    public static async Task<GenRes<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> bind)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Bind(bind);
    }

    public static async Task<GenRes<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> bind)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Bind(bind).ConfigureAwait(false);
    }

    public static async Task<GenRes<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TOkReturn> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Map(map);
    }

    public static async Task<GenRes<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TOkReturn>> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Map(map).ConfigureAwait(false);
    }

    public static async Task<GenRes<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError, TErrorReturn> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.MapError(map);
    }

    public static async Task<GenRes<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError, Task<TErrorReturn>> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.MapError(map).ConfigureAwait(false);
    }
    
    
    public static async Task<GenRes<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TOkReturn> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Map(selector);
    }

    public static async Task<GenRes<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TOkReturn>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Map(selector).ConfigureAwait(false);
    }

    public static async Task<GenRes<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Bind(selector);
    }

    public static async Task<GenRes<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Bind(selector).ConfigureAwait(false);
    }

    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.SelectMany(selector, resultSelector);
    }
    
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
    
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
    
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
}