namespace Carrigan.Core.Enums.Bootstrap;

public enum ButtonTypeEnum
{
    /// <summary>
    /// Clickable (Use for Javascript events)
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
public static class ButtonTypeEnumExtension
{
    public static string ToString(this ButtonTypeEnum color)
    {
        return color switch
        {
            ButtonTypeEnum.Button => "button",
            ButtonTypeEnum.Submit => "submit",
            ButtonTypeEnum.Reset => "reset",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}
