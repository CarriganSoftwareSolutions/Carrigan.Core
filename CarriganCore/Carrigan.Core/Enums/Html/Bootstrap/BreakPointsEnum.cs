using Carrigan.Core.DataTypes;

namespace Carrigan.Core.Enums.Html.Bootstrap;

public enum BreakPointsEnum
{
    Default = 0,
    Small = 1,
    Medium = 2,
    Large = 3,
    ExtraLarge = 4,
    ExtraExtraLarge = 5
}

public static class BreakPointsEnumExtensions
{
    public static int GetColWidthAtBreakpoint(this BreakPointsEnum at, BootstrapBreakPoints breakPoint, int defaultWidth = 4)
    {
        ArgumentNullException.ThrowIfNull(breakPoint);

        int? pick = at switch
        {
            BreakPointsEnum.Small => breakPoint.Sm,
            BreakPointsEnum.Medium => breakPoint.Md,
            BreakPointsEnum.Large => breakPoint.Lg,
            BreakPointsEnum.ExtraLarge => breakPoint.Xl,
            BreakPointsEnum.ExtraExtraLarge => breakPoint.Xxl,
            _ => breakPoint.Col
        };

        int width = pick ?? breakPoint.Col ?? defaultWidth;
        return width < 1 ? 1 : (width > 12 ? 12 : width);
    }
}