using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenericResultOptionInteropExtensions
{
    [Pure]
    public static GenericResult<TOk, TError> ToGenRes<TOk, TError>(
        this Option<TOk> option,
        Func<TError> onNone) =>
        option.IsSome()
            ? GenericResult<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenericResult<TOk, TError>.Error(onNone());

    [Pure]
    public static async Task<GenericResult<TOk, TError>> ToGenRes<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenRes(onNone);

    [Pure]
    public static async Task<GenericResult<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Option<TOk> option,
        Func<Task<TError>> onNone) =>
        option.IsSome()
            ? GenericResult<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenericResult<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    [Pure]
    public static async Task<GenericResult<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<Task<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenResAsync(onNone).ConfigureAwait(false);
    
    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> ToGenRes<TOk, TError>(
        this ValueTask<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenRes(onNone);

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Option<TOk> option,
        Func<ValueTask<TError>> onNone) =>
        option.IsSome()
            ? GenericResult<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenericResult<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> ToGenResAsync<TOk, TError>(
        this ValueTask<Option<TOk>> option,
        Func<ValueTask<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenResAsync(onNone).ConfigureAwait(false);
}