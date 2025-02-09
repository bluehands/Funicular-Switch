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


    public bool IsOk()
    {
        if (_value.IsSome()) return true;
        if (_error.IsSome()) return false;
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public bool IsError()
    {
        if (_error.IsSome()) return true;
        if (_value.IsSome()) return false;
        throw new InvalidOperationException("Neither Ok nor Error");
    }

    public TOk GetValueOrThrow() => _value.GetValueOrThrow();
    public TError GetErrorOrThrow() => _error.GetValueOrThrow();


    public TOk GetValueOrDefault(TOk defaultValue) => _value.GetValueOrDefault(defaultValue);
    public TOk GetValueOrDefault(Func<TOk> defaultValue) => _value.GetValueOrDefault(defaultValue);
    public Task<TOk> GetValueOrDefaultAsync(Func<Task<TOk>> defaultValue) => _value.GetValueOrDefault(defaultValue);


    public TError GetErrorOrDefault(TError defaultValue) => _error.GetValueOrDefault(defaultValue);
    public TError GetErrorOrDefault(Func<TError> defaultValue) => _error.GetValueOrDefault(defaultValue);

    public Task<TError> GetErrorOrDefaultAsync(Func<Task<TError>> defaultValue) =>
        _error.GetValueOrDefault(defaultValue);


    #region Option interop

    public Option<TOk> ToOption() => _value;
    public Option<TError> ToErrorOption() => _error;
    public (Option<TOk>, Option<TError>) ToOptions() => (_value, _error);

    #endregion


    public Task<GenRes<TOk, TError>> ToTask() => Task.FromResult(this);


    public GenRes<TOk, TError> Do(Action<TOk> action)
    {
        if (IsOk()) action(GetValueOrThrow());
        return this;
    }

    public async Task<GenRes<TOk, TError>> DoAsync(Func<TOk, Task> action)
    {
        if (IsOk()) await action(GetValueOrThrow());
        return this;
    }

    public GenRes<TOk, TError> DoOnError(Action<TError> action)
    {
        if (IsError()) action(GetErrorOrThrow());
        return this;
    }

    public async Task<GenRes<TOk, TError>> DoOnErrorAsync(Func<TError, Task> action)
    {
        if (IsError()) await action(GetErrorOrThrow());
        return this;
    }


    public TReturn Match<TReturn>(
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error) =>
        IsOk()
            ? ok(GetValueOrThrow())
            : error(GetErrorOrThrow());

    public async Task<TReturn> Match<TReturn>(
        Func<TOk, Task<TReturn>> ok,
        Func<TError, TReturn> error) =>
        IsOk()
            ? await ok(GetValueOrThrow()).ConfigureAwait(false)
            : error(GetErrorOrThrow());

    public async Task<TReturn> Match<TReturn>(
        Func<TOk, TReturn> ok,
        Func<TError, Task<TReturn>> error) =>
        IsOk()
            ? ok(GetValueOrThrow())
            : await error(GetErrorOrThrow()).ConfigureAwait(false);

    public async Task<TReturn> Match<TReturn>(
        Func<TOk, Task<TReturn>> ok,
        Func<TError, Task<TReturn>> error) =>
        IsOk()
            ? await ok(GetValueOrThrow()).ConfigureAwait(false)
            : await error(GetErrorOrThrow()).ConfigureAwait(false);

    public GenRes<TOkReturn, TError> Bind<TOkReturn>(
        Func<TOk, GenRes<TOkReturn, TError>> bind) =>
        IsOk()
            ? bind(GetValueOrThrow())
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    public async Task<GenRes<TOkReturn, TError>> Bind<TOkReturn>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> bind) =>
        IsOk()
            ? await bind(GetValueOrThrow()).ConfigureAwait(false)
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    public GenRes<TOkReturn, TError> Map<TOkReturn>(
        Func<TOk, TOkReturn> map) =>
        IsOk()
            ? GenRes<TOkReturn, TError>.Ok(map(GetValueOrThrow()))
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    public async Task<GenRes<TOkReturn, TError>> Map<TOkReturn>(
        Func<TOk, Task<TOkReturn>> map) =>
        IsOk()
            ? GenRes<TOkReturn, TError>.Ok(await map(GetValueOrThrow()).ConfigureAwait(false))
            : GenRes<TOkReturn, TError>.Error(GetErrorOrThrow());

    public GenRes<TOk, TErrorReturn> MapError<TErrorReturn>(
        Func<TError, TErrorReturn> map) =>
        IsOk()
            ? GenRes<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenRes<TOk, TErrorReturn>.Error(map(GetErrorOrThrow()));

    public async Task<GenRes<TOk, TErrorReturn>> MapError<TErrorReturn>(
        Func<TError, Task<TErrorReturn>> map) =>
        IsOk()
            ? GenRes<TOk, TErrorReturn>.Ok(GetValueOrThrow())
            : GenRes<TOk, TErrorReturn>.Error(await map(GetErrorOrThrow()).ConfigureAwait(false));

    public GenRes<TOkReturn, TError> Select<TOkReturn>(
        Func<TOk, TOkReturn> selector) => Map(selector);

    public GenRes<TOkReturn, TError> SelectMany<TOkReturn>(
        Func<TOk, GenRes<TOkReturn, TError>> selector) => Bind(selector);

    public GenRes<TSelect, TError> SelectMany<TOkReturn, TSelect>(
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        if (IsError()) return GenRes<TSelect, TError>.Error(GetErrorOrThrow());
        var okValue = GetValueOrThrow();
        var intermediateResult = selector(okValue);
        if (intermediateResult.IsError())
            return GenRes<TSelect, TError>.Error(intermediateResult.GetErrorOrThrow());
        var intermediateValue = intermediateResult.GetValueOrThrow();
        return GenRes<TSelect, TError>.Ok(resultSelector(okValue, intermediateValue));
    }

    public Task<GenRes<TOkReturn, TError>> SelectMany<TOkReturn>(
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector) => Bind(selector);

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

public static class OptionInteropExtensions
{
    public static GenRes<TOk, TError> ToGenRes<TOk, TError>(
        this Option<TOk> option,
        Func<TError> onNone) =>
        option.IsSome()
            ? GenRes<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenRes<TOk, TError>.Error(onNone());

    public static async Task<GenRes<TOk, TError>> ToGenRes<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenRes(onNone);

    public static async Task<GenRes<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Option<TOk> option,
        Func<Task<TError>> onNone) =>
        option.IsSome()
            ? GenRes<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenRes<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    public static async Task<GenRes<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<Task<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenResAsync(onNone).ConfigureAwait(false);
}

public static class GenResTaskExtensions
{
    public static async Task<bool> IsOk<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.IsOk();
    }

    public static async Task<bool> IsError<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.IsError();
    }

    public static async Task<TOk> GetValueOrThrow<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrThrow();

    public static async Task<TError> GetErrorOrThrow<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrThrow();

    public static async Task<TOk> GetValueOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        TOk defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    public static async Task<TOk> GetValueOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk> defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    public static async Task<TOk> GetValueOrDefaultAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<Task<TOk>> defaultValue) =>
        await (await genResTask.ConfigureAwait(false)).GetValueOrDefaultAsync(defaultValue).ConfigureAwait(false);

    public static async Task<TError> GetErrorOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        TError defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    public static async Task<TError> GetErrorOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError> defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    public static async Task<TError> GetErrorOrDefaultAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<Task<TError>> defaultValue) =>
        await (await genResTask.ConfigureAwait(false)).GetErrorOrDefaultAsync(defaultValue).ConfigureAwait(false);

    public static async Task<Option<TOk>> ToOption<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToOption();

    public static async Task<Option<TError>> ToErrorOption<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToErrorOption();

    public static async Task<(Option<TOk>, Option<TError>)> ToOptions<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToOptions();

    public static async Task<GenRes<TOk, TError>> Do<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Action<TOk> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Do(action);
    }

    public static async Task<GenRes<TOk, TError>> DoAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.DoAsync(action).ConfigureAwait(false);
    }

    public static async Task<GenRes<TOk, TError>> DoOnError<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Action<TError> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.DoOnError(action);
    }

    public static async Task<GenRes<TOk, TError>> DoOnErrorAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError, Task> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.DoOnErrorAsync(action).ConfigureAwait(false);
    }


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