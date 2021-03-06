﻿using System;
using System.Collections.Generic;

namespace FunicularSwitch.Extensions
{
    public static class DictionaryExtension
    {
        public static Option<T> TryGetValue<TKey, T>(this IReadOnlyDictionary<TKey, T> dictionary, TKey key) =>
            dictionary.TryGetValue(key, out var value) ? value : Option<T>.None;

        public static Result<T> TryGetValue<TKey, T>(this IReadOnlyDictionary<TKey, T> dictionary, TKey key, Func<string> notFound) =>
            dictionary.TryGetValue(key, out var value) ? value : Result.Error<T>(notFound());
    }
}
