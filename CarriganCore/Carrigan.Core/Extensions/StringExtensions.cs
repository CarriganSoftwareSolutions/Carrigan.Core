using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Carrigan.Core.Extensions;

/// <summary>
/// <see cref="string"/> extension methods
/// </summary>
public static class StringExtensions
{

    /// <summary>
    /// return true if empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true if empty</returns>
    public static bool IsEmpty(this string value) =>
        value == string.Empty;

    /// <summary>
    /// return true if empty or whitespace
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true if empty or whitespace</returns>
    public static bool IsWhiteSpace(this string value)
    {
        if (value.All(char.IsWhiteSpace))
            return true;
        else
            return false;
    }

    /// <summary>
    /// return true if null or empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true if null or empty</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    /// <summary>
    /// return false if empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return false if null or empty</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? value) =>
        !value.IsNullOrEmpty();

    /// <summary>
    /// return true if null, empty or whitespace
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true if null, empty or whitespace</returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value) =>
        string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// return false if null, empty or whitespace
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return false if null, empty or whitespace</returns>
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? value) =>
        !value.IsNullOrWhiteSpace();

    /// <summary>
    /// Splits a <see cref="string"/> into an <see cref="IEnumerable{T}"/> along Enivronment.NewLine
    /// </summary>
    /// <param name="input">input string</param>
    /// <returns>an <see cref="IEnumerable{T}"/> of each line in input</returns>
    public static IEnumerable<string> SplitNewLines(this string input) =>
        input.IsNotNullOrEmpty() ? input.ReplaceLineEndings().Split(Environment.NewLine) : [string.Empty];

    /// <summary>
    /// Joins an <see cref="IEnumerable{string}"/> together into a English AP style series with an and.
    /// </summary>
    /// <param name="strings">a enumeration of strings</param>
    /// <returns>English AP style series with an and</returns>
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

    /// <summary>
    /// Joins an <see cref="IEnumerable{string}"/> together into a English AP style series with an or.
    /// </summary>
    /// <param name="strings">a enumeration of strings</param>
    /// <returns>English AP style series with an or</returns>
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
    /// <summary>
    /// Returns the first non-null, non-whitespace string among <paramref name="value"/> and the
    /// provided candidates; if none qualify, returns <see cref="string.Empty"/>.
    /// </summary>
    /// /// <param name="value">
    /// The primary string to check first. If it is not null/whitespace, it is returned.
    /// </param>
    /// <param name="strings">
    /// Additional candidate strings to consider, evaluated in order until the first non-null,
    /// non-whitespace value is found.
    /// </param>
    /// /// <returns>
    /// The first non-null, non-whitespace string from the inputs; otherwise <see cref="string.Empty"/>.
    /// </returns>
    public static string Coalesce(this string? value, params IEnumerable<string?> strings)
    {
        if (value.IsNotNullOrWhiteSpace())
            return value;
        else
            return strings.Where(aString => aString.IsNotNullOrWhiteSpace()).FirstOrDefault() ?? string.Empty;
    }

    /// <summary>
    /// Inserts spaces between words in a PascalCase or camelCase identifier.
    /// </summary>
    /// <param name="input">
    /// The text to transform. If <paramref name="input"/> is null or empty,
    /// it is returned unchanged.
    /// </param>
    /// <returns>
    /// The input with a space inserted before each uppercase letter that is not at
    /// the start of the string; otherwise null when <paramref name="input"/> is null.
    /// </returns>
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
