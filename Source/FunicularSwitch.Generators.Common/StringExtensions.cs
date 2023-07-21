namespace FunicularSwitch.Generators.Common;

public static class StringExtension
{
    public static string ToSeparatedString<T>(this IEnumerable<T> items, string separator = ", ") => string.Join(separator, items);
}