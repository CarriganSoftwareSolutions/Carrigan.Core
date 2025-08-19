namespace Carrigan.Core.Enums.Html;

/// <summary>
/// An enum meant to represent button type options in HTML for type safety for use in data models and CSHTML along with .ToHtmlString();
/// </summary>
public enum ButtonTypeEnum
{
    //Ensure all new values are added to the extension method below

    /// <summary>
    /// Clickable (Use for JavaScript events)
    /// </summary>
    Button,
    /// <summary>
    /// submit button (submits form data)
    /// </summary>
    Submit,
    /// <summary>
    /// reset form data
    /// </summary>
    Reset
}

/// <summary>
/// Extension methods for ButtonTypeEnum.
/// </summary>
public static class ButtonTypeEnumExtension
{
    /// <summary>
    /// Convert the enum to an HTML string for use in CSHTML
    /// </summary>
    /// <param name="buttonType">this variable for extension methods</param>
    /// <returns><see cref="string"/> to represent an HTML string</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// This occurs when a <see cref="ButtonTypeEnum"/> is being used that was not coded for in this method.
    /// Fix this by adding the new enum value to the code in the method.
    /// </exception>
    public static string ToHtml(this ButtonTypeEnum buttonType)
    {
        return buttonType switch
        {
            ButtonTypeEnum.Button => "button",
            ButtonTypeEnum.Submit => "submit",
            ButtonTypeEnum.Reset => "reset",
            _ => throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null)
        };
    }

    [Obsolete("Use ToHtml instead.", false)]
    public static string ToString(this ButtonTypeEnum buttonType) =>
        ToHtml(buttonType);
}
