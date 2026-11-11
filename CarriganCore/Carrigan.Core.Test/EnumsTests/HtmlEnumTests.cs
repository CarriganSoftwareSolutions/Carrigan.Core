using Carrigan.Core.DataTypes;
using Carrigan.Core.Enums.Html;
using Carrigan.Core.Enums.Html.Bootstrap;

namespace Carrigan.Core.Test.EnumsTests;

public class HtmlEnumTests
{
    [Theory]
    [InlineData(ButtonTypeEnum.Button, "button")]
    [InlineData(ButtonTypeEnum.Submit, "submit")]
    [InlineData(ButtonTypeEnum.Reset, "reset")]
    public void ButtonType_ToHtml_ReturnsHtmlValue(ButtonTypeEnum value, string expected) =>
        Assert.Equal(expected, value.ToHtml());

    [Theory]
    [InlineData(FormMethodEnum.Null, null)]
    [InlineData(FormMethodEnum.Get, "get")]
    [InlineData(FormMethodEnum.Post, "post")]
    public void FormMethod_ToHtml_ReturnsHtmlValue(FormMethodEnum value, string? expected) =>
        Assert.Equal(expected, value.ToHtml());

    [Theory]
    [InlineData(ButtonColorsEnum.Primary, "btn-primary")]
    [InlineData(ButtonColorsEnum.Danger, "btn-danger")]
    [InlineData(ButtonColorsEnum.Link, "btn-link")]
    public void ButtonColor_ToBootStrapClass_ReturnsExpectedClass(ButtonColorsEnum value, string expected) =>
        Assert.Equal(expected, value.ToBootStrapClass());

    [Theory]
    [InlineData(HorizontalAlignmentEnum.Left, "text-start")]
    [InlineData(HorizontalAlignmentEnum.Center, "text-center")]
    [InlineData(HorizontalAlignmentEnum.Right, "text-end")]
    public void HorizontalAlignment_ToBootstrapClass_ReturnsExpectedClass(HorizontalAlignmentEnum value, string expected) =>
        Assert.Equal(expected, value.ToBootstrapClass());

    [Theory]
    [InlineData(BreakPointsEnum.Default, "")]
    [InlineData(BreakPointsEnum.Small, "sm")]
    [InlineData(BreakPointsEnum.Medium, "md")]
    [InlineData(BreakPointsEnum.Large, "lg")]
    [InlineData(BreakPointsEnum.ExtraLarge, "xl")]
    [InlineData(BreakPointsEnum.ExtraExtraLarge, "xxl")]
    public void BreakPoints_ToBootstrapToken_ReturnsExpectedToken(BreakPointsEnum value, string expected) =>
        Assert.Equal(expected, value.ToBootstrapToken());

    [Theory]
    [InlineData(BreakPointsEnum.Default, 5)]
    [InlineData(BreakPointsEnum.Small, 6)]
    [InlineData(BreakPointsEnum.Medium, 7)]
    [InlineData(BreakPointsEnum.ExtraExtraLarge, 10)]
    public void GetColWidthAtBreakPoint_SelectsConfiguredWidth(BreakPointsEnum value, int expected)
    {
        BootstrapBreakPoints widths = new() { Col = 5, Sm = 6, Md = 7, Xxl = 10 };

        Assert.Equal(expected, value.GetColWidthAtBreakPoint(widths));
    }

    [Theory]
    [InlineData(-3, 1)]
    [InlineData(30, 12)]
    public void GetColWidthAtBreakPoint_ClampsWidthToBootstrapRange(int width, int expected)
    {
        BootstrapBreakPoints widths = new() { Col = width };

        Assert.Equal(expected, BreakPointsEnum.Default.GetColWidthAtBreakPoint(widths));
    }

    [Fact]
    public void GetColWidthAtBreakPoint_NullBreakPoints_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => BreakPointsEnum.Default.GetColWidthAtBreakPoint(null!));

    [Fact]
    public void InvalidEnumValues_ThrowWhereExpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ((ButtonTypeEnum)99).ToHtml());
        Assert.Throws<ArgumentOutOfRangeException>(() => ((FormMethodEnum)99).ToHtml());
        Assert.Throws<ArgumentOutOfRangeException>(() => ((ButtonColorsEnum)99).ToBootStrapClass());
        Assert.Throws<ArgumentOutOfRangeException>(() => ((HorizontalAlignmentEnum)99).ToBootstrapClass());
    }
}
