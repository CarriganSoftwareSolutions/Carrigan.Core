using Carrigan.Core.DataTypes;
using Xunit;

namespace Carrigan.Core.Test.DataTypeTests;

public sealed class BootstrapBreakPointsTests
{
    [Fact]
    public void ToHtml_WhenAllValuesAreNull_ReturnsEmptyString()
    {
        BootstrapBreakPoints breakPoints = new();

        string actual = breakPoints.ToHtml();

        Assert.Equal(string.Empty, actual);
    }

    [Fact]
    public void ToHtml_WhenOnlyColIsSet_ReturnsColNWithLeadingAndTrailingSpaces()
    {
        BootstrapBreakPoints breakPoints = new()
        {
            Col = 6
        };

        string actual = breakPoints.ToHtml();

        Assert.Equal(" col-6 ", actual);
    }

    [Fact]
    public void ToHtml_WhenOnlySmIsSet_ReturnsOnlySmClassWithLeadingAndTrailingSpaces()
    {
        BootstrapBreakPoints breakPoints = new()
        {
            Sm = 4
        };

        string actual = breakPoints.ToHtml();

        Assert.Equal(" col-sm-4 ", actual);
    }

    [Fact]
    public void ToHtml_WhenMultipleBreakpointsAreSet_ReturnsAllInCorrectOrderWithSingleSpaces()
    {
        BootstrapBreakPoints breakPoints = new()
        {
            Col = 12,
            Sm = 6,
            Md = 4,
            Lg = 3,
            Xl = 2,
            Xxl = 1
        };

        string actual = breakPoints.ToHtml();

        Assert.Equal(" col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2 col-xxl-1 ", actual);
    }

    [Fact]
    public void ToHtml_WhenSomeBreakpointsAreNull_OmitsThem()
    {
        BootstrapBreakPoints breakPoints = new()
        {
            Col = 12,
            Md = 6,
            Xxl = 3
        };

        string actual = breakPoints.ToHtml();

        Assert.Equal(" col-12 col-md-6 col-xxl-3 ", actual);
    }

    [Fact]
    public void ToHtml_WhenColIsNullButOthersAreSet_DoesNotReturnColAuto()
    {
        BootstrapBreakPoints breakPoints = new()
        {
            Sm = 12
        };

        string actual = breakPoints.ToHtml();

        Assert.NotEqual(" col-auto ", actual);
        Assert.Equal(" col-sm-12 ", actual);
    }
}
