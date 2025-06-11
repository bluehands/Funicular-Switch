using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenericResultTaskExtensions
{
    [Pure]
    public static async Task<bool> IsOk<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsOk();
    }

    [Pure]
    public static async Task<bool> IsError<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsError();
    }

    [Pure]
    public static async Task<TOk> GetValueOrThrow<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).GetValueOrThrow();

    [Pure]
    public static async Task<TError> GetErrorOrThrow<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).GetErrorOrThrow();

    [Pure]
    public static async Task<TOk> GetValueOrDefault<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        TOk defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async Task<TOk> GetValueOrDefault<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk> defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async Task<TOk> GetValueOrDefaultAsync<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<Task<TOk>> defaultValue) =>
        await (await resultTask.ConfigureAwait(false)).GetValueOrDefaultAsync(defaultValue).ConfigureAwait(false);

    [Pure]
    public static async Task<TError> GetErrorOrDefault<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        TError defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async Task<TError> GetErrorOrDefault<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TError> defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async Task<TError> GetErrorOrDefaultAsync<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<Task<TError>> defaultValue) =>
        await (await resultTask.ConfigureAwait(false)).GetErrorOrDefaultAsync(defaultValue).ConfigureAwait(false);

    [Pure]
    public static async Task<Option<TOk>> ToOption<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).ToOption();

    [Pure]
    public static async Task<Option<TError>> ToErrorOption<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).ToErrorOption();

    [Pure]
    public static async Task<(Option<TOk>, Option<TError>)> ToOptions<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).ToOptions();

    [Pure]
    public static async Task<GenericResult<TOk, TError>> Do<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Action<TOk> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Do(action);
    }

    [Pure]
    public static async Task<GenericResult<TOk, TError>> DoAsync<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.DoAsync(action).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenericResult<TOk, TError>> DoOnError<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Action<TError> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.DoOnError(action);
    }

    [Pure]
    public static async Task<GenericResult<TOk, TError>> DoOnErrorAsync<TOk, TError>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TError, Task> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.DoOnErrorAsync(action).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Match(ok, error);
    }

    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<TReturn>> ok,
        Func<TError, Task<TReturn>> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<TReturn>> ok,
        Func<TError, TReturn> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<TReturn> Match<TOk, TError, TReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TReturn> ok,
        Func<TError, Task<TReturn>> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Match(ok, error).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> bind)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Bind(bind);
    }

    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> bind)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Bind(bind).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TOkReturn> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Map(map);
    }

    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<TOkReturn>> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Map(map).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenericResult<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TError, TErrorReturn> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.MapError(map);
    }

    [Pure]
    public static async Task<GenericResult<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TError, Task<TErrorReturn>> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.MapError(map).ConfigureAwait(false);
    }


    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TOkReturn> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Map(selector);
    }

    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<TOkReturn>> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Map(selector).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Bind(selector);
    }

    [Pure]
    public static async Task<GenericResult<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Bind(selector).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.SelectMany(selector, resultSelector);
    }

    [Pure]
    public static async Task<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
    
    [Pure]
    public static async Task<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }

    [Pure]
    public static async Task<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this Task<GenericResult<TOk, TError>> resultTask,
        Func<TOk, Task<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, Task<TSelect>> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
}