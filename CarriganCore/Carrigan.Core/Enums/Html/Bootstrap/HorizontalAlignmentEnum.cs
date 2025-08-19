namespace Carrigan.Core.Enums.Html.Bootstrap;
//ignore spelling: mx

/// <summary>
/// An enum meant to represent Horizontal alignment options in bootstrap for type safety for use in data models and CSHTML along with .ToBootstrapClass();
/// </summary>
public enum HorizontalAlignmentEnum
{
    //Ensure all new values are added to the extension method below

    Left,    // Corresponds to Bootstrap's "text-start" or "float-start"
    Center,  // Corresponds to Bootstrap's "text-center" or "mx-auto"
    Right    // Corresponds to Bootstrap's "text-end" or "float-end"
}

/// <summary>
/// Extension methods for HorizontalAlignmentEnum.
/// </summary>
public static class HorizontalAlignmentExtensions
{
    /// <summary>
    /// Convert the enum to a bootstrap for use in CSHTML
    /// </summary>
    /// <param name="alignment">this variable for extension methods</param>
    /// <returns><see cref="string"/> to represent a bootstrap alignment</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// This occurs when a <sse cref="HorizontalAlignmentEnum"/> is being used that was not coded for in this method.
    /// Fix this by adding the new enum value to the code in the method.
    /// </exception>
    public static string ToBootstrapClass(this HorizontalAlignmentEnum alignment)
    {
        return alignment switch
        {
            HorizontalAlignmentEnum.Left => "text-start",
            HorizontalAlignmentEnum.Center =>"text-center",
            HorizontalAlignmentEnum.Right => "text-end",
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
    }
}
