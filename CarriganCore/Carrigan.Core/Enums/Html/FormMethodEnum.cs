namespace Carrigan.Core.Enums.Html;

/// <summary>
/// An enum meant to represent form method types options in HTML for type safety for use in data models and CSHTML along with .ToHtmlString();
/// </summary>
public enum FormMethodEnum
{
    Null,
    Get,
    Post
}

/// <summary>
/// Extension methods for FormMethodEnum.
/// </summary>
public static class FormMethodEnumExtension
{
    [Obsolete("Use ToHtml instead.", false)]
    public static string ToString(this FormMethodEnum formMethodType) =>
        ToHtml(formMethodType) ?? string.Empty;

    /// <summary>
    /// Convert the enum to an HTML string for use in CSHTML
    /// </summary>
    /// <param name="formMethodType">this variable for extension methods</param>
    /// <returns><see cref="string"/> to represent an HTML string</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// This occurs when a <see cref="FormMethodEnum"/> is being used that was not coded for in this method.
    /// Fix this by adding the new enum value to the code in the method.
    /// </exception>
    public static string? ToHtml(this FormMethodEnum formMethodType)
    {
        return formMethodType switch
        {
            FormMethodEnum.Null => null,
            FormMethodEnum.Get => "get",
            FormMethodEnum.Post => "post",
            _ => throw new ArgumentOutOfRangeException(nameof(formMethodType), formMethodType, null)
        };
    }
}
