using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Carrigan.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNotNullOrEmpty ([NotNullWhen(true)] this string? value) =>
        !value.IsNullOrEmpty();

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value) =>
        string.IsNullOrWhiteSpace(value);

    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? value) =>
        !value.IsNullOrWhiteSpace();


    public static IEnumerable<string> SplitNewLines(this string input) =>
        input.IsNotNullOrEmpty() ? input.ReplaceLineEndings().Split(Environment.NewLine) : [string.Empty];

    public static string JoinAnd(this IEnumerable<string> strings)
    {
        if (strings.IsNullOrEmpty())
            return string.Empty;
        else if (strings.Count() == 1)
            return strings.Single();
        else if (strings.Count() == 2)
            return string.Join(" and ", strings);
        else
            return string.Concat(string.Join(", ", strings.Take(strings.Count() - 1)), " and ", strings.TakeLast(1).First());
    }
    public static string JoinOr(this IEnumerable<string> strings)
    {
        if (strings.IsNullOrEmpty())
            return string.Empty;
        else if (strings.Count() == 1)
            return strings.Single();
        else if (strings.Count() == 2)
            return string.Join(" or ", strings);
        else
            return string.Concat(string.Join(", ", strings.Take(strings.Count() - 1)), " or ", strings.TakeLast(1).First());
    }



    public static string Coalesce(this string? value, params IEnumerable<string?> strings)
    {
        if (value.IsNotNullOrWhiteSpace())
            return value;
        else
            return strings.Where(aString => aString.IsNotNullOrWhiteSpace()).FirstOrDefault() ?? string.Empty;
    }


    [return: NotNullIfNotNull(nameof(input))]
    public static string? SplitCamelPascalCase(this string? input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // This regex inserts a space before each uppercase letter that is not at the start of the string
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
        return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
#pragma warning restore IDE0079 // Remove unnecessary suppression
    }
}
