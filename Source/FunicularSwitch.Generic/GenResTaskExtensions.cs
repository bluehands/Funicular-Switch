using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenResTaskExtensions
{
    [Pure]
    public static async Task<bool> IsOk<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.IsOk();
    }

    [Pure]
    public static async Task<bool> IsError<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.IsError();
    }

    [Pure]
    public static async Task<TOk> GetValueOrThrow<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrThrow();

    [Pure]
    public static async Task<TError> GetErrorOrThrow<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrThrow();

    [Pure]
    public static async Task<TOk> GetValueOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        TOk defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async Task<TOk> GetValueOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk> defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async Task<TOk> GetValueOrDefaultAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<Task<TOk>> defaultValue) =>
        await (await genResTask.ConfigureAwait(false)).GetValueOrDefaultAsync(defaultValue).ConfigureAwait(false);

    [Pure]
    public static async Task<TError> GetErrorOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        TError defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async Task<TError> GetErrorOrDefault<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError> defaultValue) =>
        (await genResTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async Task<TError> GetErrorOrDefaultAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<Task<TError>> defaultValue) =>
        await (await genResTask.ConfigureAwait(false)).GetErrorOrDefaultAsync(defaultValue).ConfigureAwait(false);

    [Pure]
    public static async Task<Option<TOk>> ToOption<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToOption();

    [Pure]
    public static async Task<Option<TError>> ToErrorOption<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToErrorOption();

    [Pure]
    public static async Task<(Option<TOk>, Option<TError>)> ToOptions<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask) =>
        (await genResTask.ConfigureAwait(false)).ToOptions();

    [Pure]
    public static async Task<GenRes<TOk, TError>> Do<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Action<TOk> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Do(action);
    }

    [Pure]
    public static async Task<GenRes<TOk, TError>> DoAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.DoAsync(action).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenRes<TOk, TError>> DoOnError<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Action<TError> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.DoOnError(action);
    }

    [Pure]
    public static async Task<GenRes<TOk, TError>> DoOnErrorAsync<TOk, TError>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError, Task> action)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.DoOnErrorAsync(action).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Match(ok, error);
    }

    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TReturn>> ok,
        Func<TError, Task<TReturn>> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TReturn>> ok,
        Func<TError, TReturn> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TReturn> ok,
        Func<TError, Task<TReturn>> error)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Match(ok, error).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> bind)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Bind(bind);
    }

    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> bind)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Bind(bind).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TOkReturn> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Map(map);
    }

    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TOkReturn>> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Map(map).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenRes<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError, TErrorReturn> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.MapError(map);
    }

    [Pure]
    public static async Task<GenRes<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TError, Task<TErrorReturn>> map)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.MapError(map).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, TOkReturn> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Map(selector);
    }

    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<TOkReturn>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Map(selector).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.Bind(selector);
    }

    [Pure]
    public static async Task<GenRes<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.Bind(selector).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return genRes.SelectMany(selector, resultSelector);
    }

    [Pure]
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
    
    [Pure]
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, GenRes<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenRes<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenRes<TOk, TError>> genResTask,
        Func<TOk, Task<GenRes<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        var genRes = await genResTask.ConfigureAwait(false);
        return await genRes.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
}