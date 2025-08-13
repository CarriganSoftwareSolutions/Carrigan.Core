namespace Carrigan.Core.Enums.Bootstrap.Alignments;
//ignore spelling: mx
public enum HorizontalAlignmentEnum
{
    Left,    // Corresponds to Bootstrap's "text-start" or "float-start"
    Center,  // Corresponds to Bootstrap's "text-center" or "mx-auto"
    Right    // Corresponds to Bootstrap's "text-end" or "float-end"
}
public static class HorizontalAlignmentExtensions
{
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
