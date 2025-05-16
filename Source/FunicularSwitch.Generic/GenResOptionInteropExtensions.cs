using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenResOptionInteropExtensions
{
    [Pure]
    public static GenRes<TOk, TError> ToGenRes<TOk, TError>(
        this Option<TOk> option,
        Func<TError> onNone) =>
        option.IsSome()
            ? GenRes<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenRes<TOk, TError>.Error(onNone());

    [Pure]
    public static async Task<GenRes<TOk, TError>> ToGenRes<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenRes(onNone);

    [Pure]
    public static async Task<GenRes<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Option<TOk> option,
        Func<Task<TError>> onNone) =>
        option.IsSome()
            ? GenRes<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenRes<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    [Pure]
    public static async Task<GenRes<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<Task<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenResAsync(onNone).ConfigureAwait(false);
    
    [Pure]
    public static async ValueTask<GenRes<TOk, TError>> ToGenRes<TOk, TError>(
        this ValueTask<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenRes(onNone);

    [Pure]
    public static async ValueTask<GenRes<TOk, TError>> ToGenResAsync<TOk, TError>(
        this Option<TOk> option,
        Func<ValueTask<TError>> onNone) =>
        option.IsSome()
            ? GenRes<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenRes<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    [Pure]
    public static async ValueTask<GenRes<TOk, TError>> ToGenResAsync<TOk, TError>(
        this ValueTask<Option<TOk>> option,
        Func<ValueTask<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenResAsync(onNone).ConfigureAwait(false);
}