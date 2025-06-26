using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenericResultValueTaskExtensions
{
    [Pure]
    public static async ValueTask<bool> IsOk<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsOk();
    }

    [Pure]
    public static async ValueTask<bool> IsError<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsError();
    }

    [Pure]
    public static async ValueTask<TOk> GetValueOrThrow<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).GetValueOrThrow();

    [Pure]
    public static async ValueTask<TError> GetErrorOrThrow<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).GetErrorOrThrow();

    [Pure]
    public static async ValueTask<TOk> GetValueOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        TOk defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TOk> GetValueOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk> defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TOk> GetValueOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<ValueTask<TOk>> defaultValue) =>
        await (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue).ConfigureAwait(false);

    [Pure]
    public static async ValueTask<TError> GetErrorOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        TError defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TError> GetErrorOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TError> defaultValue) =>
        (await resultTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue);

    [Pure]
    public static async ValueTask<TError> GetErrorOrDefault<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<ValueTask<TError>> defaultValue) =>
        await (await resultTask.ConfigureAwait(false)).GetErrorOrDefault(defaultValue).ConfigureAwait(false);

    [Pure]
    public static async ValueTask<Option<TOk>> ToOption<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).ToOption();

    [Pure]
    public static async ValueTask<Option<TError>> ToErrorOption<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).ToErrorOption();

    [Pure]
    public static async ValueTask<(Option<TOk>, Option<TError>)> ToOptions<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask) =>
        (await resultTask.ConfigureAwait(false)).ToOptions();

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> Do<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Action<TOk> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Do(action);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> Do<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Do(action).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> DoOnError<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Action<TError> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.DoOnError(action);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> DoOnError<TOk, TError>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TError, ValueTask> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.DoOnError(action).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TReturn> ok,
        Func<TError, TReturn> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Match(ok, error);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<TReturn>> ok,
        Func<TError, ValueTask<TReturn>> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<TReturn>> ok,
        Func<TError, TReturn> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<TReturn> Match<TOk, TError, TReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TReturn> ok,
        Func<TError, ValueTask<TReturn>> error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Match(ok, error).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> bind)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Bind(bind);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Bind<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> bind)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Bind(bind).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TOkReturn> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Map(map);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Map<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<TOkReturn>> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Map(map).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TError, TErrorReturn> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.MapError(map);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOk, TErrorReturn>> MapError<TOk, TError, TErrorReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TError, ValueTask<TErrorReturn>> map)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.MapError(map).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, TOkReturn> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Map(selector);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> Select<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<TOkReturn>> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Map(selector).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.Bind(selector);
    }

    [Pure]
    public static async ValueTask<GenericResult<TOkReturn, TError>> SelectMany<TOk, TError, TOkReturn>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.Bind(selector).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.SelectMany(selector, resultSelector);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, TSelect> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, GenericResult<TOkReturn, TError>> selector,
        Func<TOk, TOkReturn, ValueTask<TSelect>> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }

    [Pure]
    public static async ValueTask<GenericResult<TSelect, TError>> SelectMany<TOk, TError, TOkReturn, TSelect>(
        this ValueTask<GenericResult<TOk, TError>> resultTask,
        Func<TOk, ValueTask<GenericResult<TOkReturn, TError>>> selector,
        Func<TOk, TOkReturn, ValueTask<TSelect>> resultSelector)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SelectMany(selector, resultSelector).ConfigureAwait(false);
    }
}