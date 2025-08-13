namespace Carrigan.Core.Enums.Bootstrap;

public enum ButtonColorsEnum
{
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
public static class ButtonColorsEnumExtension
{
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
