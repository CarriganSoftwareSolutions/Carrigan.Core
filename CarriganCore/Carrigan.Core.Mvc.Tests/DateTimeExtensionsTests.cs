using Carrigan.Core.Mvc.Extensions;

namespace Carrigan.Core.Mvc.Tests;

public class DateTimeExtensionsTests
{
    [Theory]
    [InlineData(0, 0, "00:00")]
    [InlineData(14, 5, "14:05")]
    [InlineData(23, 59, "23:59")]
    public void TimeOnly_ToHtmlValue_ReturnsHtmlTime(int hour, int minute, string expected)
    {
        TimeOnly value = new(hour, minute);

        Assert.Equal(expected, value.ToHtmlValue());
        Assert.Equal(expected, ((TimeOnly?)value).HtmlValue());
    }

    [Fact]
    public void NullableTimeOnly_HtmlValue_NullReturnsEmptyString() =>
        Assert.Equal(string.Empty, ((TimeOnly?)null).HtmlValue());

    [Theory]
    [InlineData(2024, 2, 29, "2024-02-29")]
    [InlineData(2026, 11, 5, "2026-11-05")]
    public void DateOnly_ToHtmlValue_ReturnsIsoDate(int year, int month, int day, string expected)
    {
        DateOnly value = new(year, month, day);

        Assert.Equal(expected, value.ToHtmlValue());
        Assert.Equal(expected, ((DateOnly?)value).HtmlValue());
    }

    [Fact]
    public void NullableDateOnly_HtmlValue_NullReturnsEmptyString() =>
        Assert.Equal(string.Empty, ((DateOnly?)null).HtmlValue());

    [Fact]
    public void DateTime_ToHtmlValue_ReturnsDateTimeLocalWithoutSeconds()
    {
        DateTime value = new(2026, 11, 5, 14, 7, 59);

        Assert.Equal("2026-11-05T14:07", value.ToHtmlValue());
        Assert.Equal("2026-11-05T14:07", ((DateTime?)value).HtmlValue());
    }

    [Fact]
    public void NullableDateTime_HtmlValue_NullReturnsEmptyString() =>
        Assert.Equal(string.Empty, ((DateTime?)null).HtmlValue());
}
