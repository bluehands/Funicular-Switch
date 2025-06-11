using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenResValueTaskExtensions
{
    [Pure]
    public static async ValueTask<bool> IsOk<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.IsOk();
    }

    [Pure]
    public static async ValueTask<bool> IsError<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.IsError();
    }

    [Pure]
    public static async ValueTask<TOk> GetValueOrThrow<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrThrow();

    [Pure]
    public static async ValueTask<TError> GetErrorOrThrow<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrThrow();

    [Pure]
    public static async ValueTask<TOk> GetValueOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        TOk defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TOk> GetValueOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk> defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TOk> GetValueOrDefaultAsync<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<ValueTask<TOk>> defaultValue) =>
        await (await genResTask.ConfigureAwait(false)).GetValueOrDefaultAsync(() => defaultValue().AsTask()).ConfigureAwait(false);

    [Pure]
    public static async ValueTask<TError> GetErrorOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        TError defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TError> GetErrorOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TError> defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TError> GetErrorOrDefaultAsync<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<ValueTask<TError>> defaultValue) =>
        await (await genResTask.ConfigureAwait(false)).GetErrorOrDefaultAsync(() => defaultValue().AsTask()).ConfigureAwait(false);

    [Pure]
    public static async ValueTask<Option<TOk>> ToOption<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToOption();

    [Pure]
    public static async ValueTask<Option<TError>> ToErrorOption<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToErrorOption();

    [Pure]
    public static async ValueTask<(Option<TOk>, Option<TError>)> ToOptions<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToOptions();

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> Do<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Action<TOk> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Do(action);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> DoAsync<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.DoAsync(x => action(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> DoOnError<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Action<TError> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.DoOnError(action);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> DoOnErrorAsync<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TError, ValueTask> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.DoOnErrorAsync(x => action(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Match(ok, error);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<TReturn>> ok,
        Func<TError, ValueTask<TReturn>> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(x => ok(x).AsTask(), x => error(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<TReturn>> ok,
        Func<TError, TReturn> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(x => ok(x).AsTask(), error).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, TReturn> ok,
        Func<TError, ValueTask<TReturn>> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, x => error(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, GenericResult<TOkReturn, TError>> bind)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Bind(bind);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> bind)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Bind(x => bind(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, TOkReturn> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Map(map);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<TOkReturn>> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Map(x => map(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TError, TErrorReturn> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.MapError(map);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TError, ValueTask<TErrorReturn>> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.MapError(x => map(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, TOkReturn> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Map(selector);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<TOkReturn>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Map(x => selector(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Bind(selector);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Bind(x => selector(x).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.SelectMany(selector, resultSelector);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(x => selector(x).AsTask(), resultSelector).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, ValueTask<TSelect>> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, (x, y) => resultSelector(x, y).AsTask()).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> genResTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, ValueTask<TSelect>> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(x => selector(x).AsTask(), (x, y) => resultSelector(x, y).AsTask()).ConfigureAwait(false);
    }
}