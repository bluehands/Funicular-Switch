using System.Diagnostics.Contracts;

namespace FunicularSwitch.Generic;

public static class GenericResultOptionExtensions
{
    [Pure]
    public static GenericResult<TOk, TError> ToGenericResult<TOk, TError>(
        this Option<TOk> option,
        Func<TError> onNone) =>
        option.IsSome()
            ? GenericResult<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenericResult<TOk, TError>.Error(onNone());

    [Pure]
    public static async Task<GenericResult<TOk, TError>> ToGenericResult<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenericResult(onNone);

    [Pure]
    public static async Task<GenericResult<TOk, TError>> ToGenericResult<TOk, TError>(
        this Option<TOk> option,
        Func<Task<TError>> onNone) =>
        option.IsSome()
            ? GenericResult<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenericResult<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    [Pure]
    public static async Task<GenericResult<TOk, TError>> ToGenericResult<TOk, TError>(
        this Task<Option<TOk>> option,
        Func<Task<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenericResult(onNone).ConfigureAwait(false);

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> ToGenericResult<TOk, TError>(
        this ValueTask<Option<TOk>> option,
        Func<TError> onNone) =>
        (await option.ConfigureAwait(false)).ToGenericResult(onNone);

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> ToGenericResult<TOk, TError>(
        this Option<TOk> option,
        Func<ValueTask<TError>> onNone) =>
        option.IsSome()
            ? GenericResult<TOk, TError>.Ok(option.GetValueOrThrow())
            : GenericResult<TOk, TError>.Error(await onNone().ConfigureAwait(false));

    [Pure]
    public static async ValueTask<GenericResult<TOk, TError>> ToGenericResult<TOk, TError>(
        this ValueTask<Option<TOk>> option,
        Func<ValueTask<TError>> onNone) =>
        await (await option.ConfigureAwait(false)).ToGenericResult(onNone).ConfigureAwait(false);


    [Pure]
    public static GenericResult<Option<TOk>, TError> UnboxResult<TOk, TError>(Option<GenericResult<TOk, TError>> maybeResult) =>
        maybeResult.Match(
            static some => some.Map(Option<TOk>.Some),
            static () => GenericResult<Option<TOk>, TError>.Ok(Option<TOk>.None));

    [Pure]
    public static Task<GenericResult<Option<TOk>, TError>> UnboxResult<TOk, TError>(Task<Option<GenericResult<TOk, TError>>> maybeResult) =>
        maybeResult.Match(
            static some => some.Map(Option<TOk>.Some),
            static () => GenericResult<Option<TOk>, TError>.Ok(Option<TOk>.None));
    
    [Pure]
    public static async ValueTask<GenericResult<Option<TOk>, TError>> UnboxResult<TOk, TError>(ValueTask<Option<GenericResult<TOk, TError>>> maybeResult) =>
        (await maybeResult.ConfigureAwait(false)).Match(
            static some => some.Map(Option<TOk>.Some),
            static () => GenericResult<Option<TOk>, TError>.Ok(Option<TOk>.None));
    
    
    [Pure]
    public static Option<GenericResult<TOk, TError>> UnboxOption<TOk, TError>(GenericResult<Option<TOk>, TError> result) =>
        result.Match(
            static ok => ok.Map(GenericResult<TOk, TError>.Ok),
            static error => Option.Some(GenericResult<TOk, TError>.Error(error)));
    
    [Pure]
    public static Task<Option<GenericResult<TOk, TError>>> UnboxOption<TOk, TError>(Task<GenericResult<Option<TOk>, TError>> result) =>
        result.Match(
            static ok => ok.Map(GenericResult<TOk, TError>.Ok),
            static error => Option.Some(GenericResult<TOk, TError>.Error(error)));
    
    [Pure]
    public static ValueTask<Option<GenericResult<TOk, TError>>> UnboxOption<TOk, TError>(ValueTask<GenericResult<Option<TOk>, TError>> result) =>
        result.Match(
            static ok => ok.Map(GenericResult<TOk, TError>.Ok),
            static error => Option.Some(GenericResult<TOk, TError>.Error(error)));
}