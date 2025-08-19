namespace Carrigan.Core.Mvc.Extensions;

//IGNORE SPELLING: yyyy dd mm datetime

/// <summary>
/// Provides extension methods for formatting TimeOnly, DateOnly, and DateTime values into HTML-compatible string formats.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a TimeOnly value to a string formatted as "HH:mm" for HTML input fields.
    /// </summary>
    /// <param name="value">The TimeOnly value to format.</param>
    /// <returns>A string in the format "HH:mm".</returns>
    /// <example>
    /// For example, a TimeOnly value of "2:30 PM" would be formatted as "14:30".
    /// </example>
    public static string ToHtmlValue(this TimeOnly value) =>
        value.ToString("HH:mm");

    /// <summary>
    /// Converts a nullable TimeOnly value to a string formatted as "HH:mm", or an empty string if the value is null.
    /// </summary>
    /// <param name="value">The nullable TimeOnly value to format.</param>
    /// <returns>A string in the format "HH:mm" or an empty string if null.</returns>
    /// <example>
    /// For example, a TimeOnly value of "2:30 PM" would be formatted as "14:30".
    /// A null value would return an empty string.
    /// </example>
    public static string HtmlValue(this TimeOnly? value) =>
        value?.ToHtmlValue() ?? string.Empty;

    /// <summary>
    /// Converts a DateOnly value to a string formatted as "yyyy-MM-dd" for HTML date input fields.
    /// </summary>
    /// <param name="value">The DateOnly value to format.</param>
    /// <returns>A string in the format "yyyy-MM-dd".</returns>
    /// <example>
    /// For example, a DateOnly value of "September 29 2024" would be formatted as "2024-09-29".
    /// </example>
    public static string ToHtmlValue(this DateOnly value) =>
        value.ToString("yyyy-MM-dd");

    /// <summary>
    /// Converts a nullable DateOnly value to a string formatted as "yyyy-MM-dd", or an empty string if the value is null.
    /// </summary>
    /// <param name="value">The nullable DateOnly value to format.</param>
    /// <returns>A string in the format "yyyy-MM-dd" or an empty string if null.</returns>
    /// <example>
    /// For example, a DateOnly value of "September 29 2024" would be formatted as "2024-09-29".
    /// A null value would return an empty string.
    /// </example>
    public static string HtmlValue(this DateOnly? value) =>
        value?.ToHtmlValue() ?? string.Empty;

    /// <summary>
    /// Converts a DateTime value to a string formatted as "yyyy-MM-ddTHH:mm" for HTML datetime-local input fields.
    /// </summary>
    /// <param name="value">The DateTime value to format.</param>
    /// <returns>A string in the format "yyyy-MM-ddTHH:mm".</returns>
    /// <example>
    /// For example, a DateTime value of "September 29 2024 2:30 PM" would be formatted as "2024-09-29T14:30".
    /// </example>
    public static string ToHtmlValue(this DateTime value) =>
        value.ToString("yyyy-MM-ddTHH:mm");

    /// <summary>
    /// Converts a nullable DateTime value to a string formatted as "yyyy-MM-ddTHH:mm", or an empty string if the value is null.
    /// </summary>
    /// <param name="value">The nullable DateTime value to format.</param>
    /// <returns>A string in the format "yyyy-MM-ddTHH:mm" or an empty string if null.</returns>
    /// <example>
    /// For example, a DateTime value of "September 29 2024 2:30 PM" would be formatted as "2024-09-29T14:30".
    /// A null value would return an empty string.
    /// </example>
    public static string HtmlValue(this DateTime? value) =>
        value?.ToHtmlValue() ?? string.Empty;
}
