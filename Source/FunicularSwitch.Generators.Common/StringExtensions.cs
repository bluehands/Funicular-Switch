namespace FunicularSwitch.Generators.Common;

public static class StringExtension
{
    public static string ToSeparatedString<T>(this IEnumerable<T> items, string separator = ", ") => string.Join(separator, items);
    public static string FirstToLower(this string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);
    public static string FirstToUpper(this string name) => char.ToUpperInvariant(name[0]) + name.Substring(1);
}