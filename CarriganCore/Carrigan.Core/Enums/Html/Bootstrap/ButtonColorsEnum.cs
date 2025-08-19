namespace Carrigan.Core.Enums.Html.Bootstrap;

/// <summary>
/// An enum meant to represent button color options in bootstrap for type safety for use in data models and CSHTML along with .ToBootstrapClass();
/// I never use the outline only color options, so those are not represented in the enum
/// </summary>
public enum ButtonColorsEnum
{
    //Ensure all new values are added to the extension method below

    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info,
    Light,
    Dark,
    Link
}
/// <summary>
/// Extension methods for ButtonColorsEnum.
/// </summary>
public static class ButtonColorsEnumExtension
{
    /// <summary>
    /// Convert the enum to a bootstrap for use in CSHTML
    /// </summary>
    /// <param name="color">this variable for extension methods</param>
    /// <returns><see cref="string"/> to represent a bootstrap alignment</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// This occurs when a <see cref="ButtonColorsEnum"/> is being used that was not coded for in this method.
    /// Fix this by adding the new enum value to the code in the method.
    /// </exception>
    public static string ToBootStrapClass(this ButtonColorsEnum color)
    {
        return color switch
        {
            ButtonColorsEnum.Primary => "btn-primary",
            ButtonColorsEnum.Secondary => "btn-secondary",
            ButtonColorsEnum.Success => "btn-success",
            ButtonColorsEnum.Danger => "btn-danger",
            ButtonColorsEnum.Warning => "btn-warning",
            ButtonColorsEnum.Info => "btn-info",
            ButtonColorsEnum.Light => "btn-light",
            ButtonColorsEnum.Dark => "btn-dark",
            ButtonColorsEnum.Link => "btn-link",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}
