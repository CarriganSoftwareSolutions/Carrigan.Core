using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class DateTimeExtensionsTests
{
    #region ToAmPmString Tests

    /// <summary>
    /// Tests the ToAmPmString extension method for non-nullable TimeOnly values.
    /// </summary>
    [Theory]
    [InlineData("14:30", "2:30 PM")]
    [InlineData("00:00", "12:00 AM")]
    [InlineData("12:00", "12:00 PM")]
    [InlineData("23:59", "11:59 PM")]
    [InlineData("01:15", "1:15 AM")]
    public void ToAmPmString_TimeOnly_ReturnsCorrectFormat(string timeString, string expected)
    {
        // Arrange
        TimeOnly time = TimeOnly.Parse(timeString);

        // Act
        string result = time.ToAmPmString();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToAmPmString extension method for nullable TimeOnly values, including null.
    /// </summary>
    [Theory]
    [InlineData("14:30", "2:30 PM")]
    [InlineData("00:00", "12:00 AM")]
    [InlineData("12:00", "12:00 PM")]
    [InlineData("23:59", "11:59 PM")]
    [InlineData("01:15", "1:15 AM")]
    [InlineData(null, "")]
    public void ToAmPmString_NullableTimeOnly_ReturnsCorrectFormat(string? timeString, string expected)
    {
        // Arrange
        TimeOnly? time = timeString != null ? TimeOnly.Parse(timeString) : null;

        // Act
        string result = time.ToAmPmString();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region ToDateOnly Extension Tests

    /// <summary>
    /// Tests the ToDateOnly extension method for non-nullable DateTime values.
    /// </summary>
    [Fact]
    public void ToDateOnly_DateTime_ReturnsDateOnly()
    {
        // Arrange
        DateTime dateTime = new(2024, 09, 29, 14, 30, 00);
        DateOnly expected = new(2024, 09, 29);

        // Act
        DateOnly result = dateTime.ToDateOnly();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToDateOnly extension method for nullable DateTime values with a value.
    /// </summary>
    [Fact]
    public void ToDateOnly_NullableDateTime_ReturnsDateOnly()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2024, 09, 29, 14, 30, 00);
        DateOnly? expected = new DateOnly(2024, 09, 29);

        // Act
        DateOnly? result = dateTime.ToDateOnly();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToDateOnly extension method for nullable DateTime values with null.
    /// </summary>
    [Fact]
    public void ToDateOnly_NullableDateTime_Null_ReturnsNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        DateOnly? result = dateTime.ToDateOnly();

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region ToDayOfWeek Tests

    /// <summary>
    /// Tests the ToDayOfWeek extension method for non-nullable DateTime values.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "Sunday")]
    [InlineData("2024-10-01", "Tuesday")]
    [InlineData("2024-12-25", "Wednesday")]
    [InlineData("2025-01-01", "Wednesday")]
    public void ToDayOfWeek_DateTime_ReturnsCorrectDay(string dateString, string expectedDay)
    {
        // Arrange
        DateTime dateTime = DateTime.Parse(dateString);

        // Act
        string result = dateTime.ToDayOfWeek();

        // Assert
        Assert.Equal(expectedDay, result);
    }

    /// <summary>
    /// Tests the ToDayOfWeek extension method for nullable DateTime values with a value and null.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "Sunday")]
    [InlineData("2024-10-01", "Tuesday")]
    [InlineData("2024-12-25", "Wednesday")]
    [InlineData("2025-01-01", "Wednesday")]
    [InlineData(null, "")]
    public void ToDayOfWeek_NullableDateTime_ReturnsCorrectDay(string? dateString, string expectedDay)
    {
        // Arrange
        DateTime? dateTime = dateString != null ? DateTime.Parse(dateString) : null;

        // Act
        string result = dateTime.ToDayOfWeek();

        // Assert
        Assert.Equal(expectedDay, result);
    }

    /// <summary>
    /// Tests the ToDayOfWeek extension method for non-nullable DateOnly values.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "Sunday")]
    [InlineData("2024-10-01", "Tuesday")]
    [InlineData("2024-12-25", "Wednesday")]
    [InlineData("2025-01-01", "Wednesday")]
    public void ToDayOfWeek_DateOnly_ReturnsCorrectDay(string dateString, string expectedDay)
    {
        // Arrange
        DateOnly dateOnly = DateOnly.Parse(dateString);

        // Act
        string result = dateOnly.ToDayOfWeek();

        // Assert
        Assert.Equal(expectedDay, result);
    }

    /// <summary>
    /// Tests the ToDayOfWeek extension method for nullable DateOnly values with a value and null.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "Sunday")]
    [InlineData("2024-10-01", "Tuesday")]
    [InlineData("2024-12-25", "Wednesday")]
    [InlineData("2025-01-01", "Wednesday")]
    [InlineData(null, "")]
    public void ToDayOfWeek_NullableDateOnly_ReturnsCorrectDay(string? dateString, string expectedDay)
    {
        // Arrange
        DateOnly? dateOnly = dateString != null ? DateOnly.Parse(dateString) : null;

        // Act
        string result = dateOnly.ToDayOfWeek();

        // Assert
        Assert.Equal(expectedDay, result);
    }

    #endregion

    #region ToTimeOnly Extension Tests

    /// <summary>
    /// Tests the ToTimeOnly extension method for non-nullable DateTime values.
    /// </summary>
    [Fact]
    public void ToTimeOnly_DateTime_ReturnsTimeOnly()
    {
        // Arrange
        DateTime dateTime = new(2024, 09, 29, 14, 30, 00);
        TimeOnly expected = new(14, 30);

        // Act
        TimeOnly result = dateTime.ToTimeOnly();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToTimeOnly extension method for nullable DateTime values with a value.
    /// </summary>
    [Fact]
    public void ToTimeOnly_NullableDateTime_ReturnsTimeOnly()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2024, 09, 29, 14, 30, 00);
        TimeOnly? expected = new TimeOnly(14, 30);

        // Act
        TimeOnly? result = dateTime.ToTimeOnly();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToTimeOnly extension method for nullable DateTime values with null.
    /// </summary>
    [Fact]
    public void ToTimeOnly_NullableDateTime_Null_ReturnsNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        TimeOnly? result = dateTime.ToTimeOnly();

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region ToMonthDayYearLong Tests

    /// <summary>
    /// Tests the ToMonthDayYearLong extension method for non-nullable DateTime values.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "September 29, 2024")]
    [InlineData("2025-01-01", "January 1, 2025")]
    [InlineData("2023-12-25", "December 25, 2023")]
    public void ToMonthDayYearLong_DateTime_ReturnsCorrectFormat(string dateString, string expected)
    {
        // Arrange
        DateTime dateTime = DateTime.Parse(dateString);

        // Act
        string result = dateTime.ToMonthDayYearLong();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToMonthDayYearLong extension method for nullable DateTime values with a value and null.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "September 29, 2024")]
    [InlineData("2025-01-01", "January 1, 2025")]
    [InlineData("2023-12-25", "December 25, 2023")]
    [InlineData(null, "")]
    public void ToMonthDayYearLong_NullableDateTime_ReturnsCorrectFormat(string? dateString, string expected)
    {
        // Arrange
        DateTime? dateTime = dateString != null ? DateTime.Parse(dateString) : null;

        // Act
        string result = dateTime.ToMonthDayYearLong();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToMonthDayYearLong extension method for non-nullable DateOnly values.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "September 29, 2024")]
    [InlineData("2025-01-01", "January 1, 2025")]
    [InlineData("2023-12-25", "December 25, 2023")]
    public void ToMonthDayYearLong_DateOnly_ReturnsCorrectFormat(string dateString, string expected)
    {
        // Arrange
        DateOnly dateOnly = DateOnly.Parse(dateString);

        // Act
        string result = dateOnly.ToMonthDayYearLong();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToMonthDayYearLong extension method for nullable DateOnly values with a value and null.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29", "September 29, 2024")]
    [InlineData("2025-01-01", "January 1, 2025")]
    [InlineData("2023-12-25", "December 25, 2023")]
    [InlineData(null, "")]
    public void ToMonthDayYearLong_NullableDateOnly_ReturnsCorrectFormat(string? dateString, string expected)
    {
        // Arrange
        DateOnly? dateOnly = dateString != null ? DateOnly.Parse(dateString) : null;

        // Act
        string result = dateOnly.ToMonthDayYearLong();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region ToMonthDayYearAmPm Tests

    /// <summary>
    /// Tests the ToMonthDayYearAmPm extension method for non-nullable DateTime values.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29 14:30", "September 29, 2024 2:30 PM")]
    [InlineData("2025-01-01 00:00", "January 1, 2025 12:00 AM")]
    [InlineData("2023-12-25 23:59", "December 25, 2023 11:59 PM")]
    [InlineData("2024-07-04 09:15", "July 4, 2024 9:15 AM")]
    public void ToMonthDayYearAmPm_DateTime_ReturnsCorrectFormat(string dateTimeString, string expected)
    {
        // Arrange
        DateTime dateTime = DateTime.Parse(dateTimeString);

        // Act
        string result = dateTime.ToMonthDayYearAmPm();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToMonthDayYearAmPm extension method for nullable DateTime values with a value and null.
    /// </summary>
    [Theory]
    [InlineData("2024-09-29 14:30", "September 29, 2024 2:30 PM")]
    [InlineData("2025-01-01 00:00", "January 1, 2025 12:00 AM")]
    [InlineData("2023-12-25 23:59", "December 25, 2023 11:59 PM")]
    [InlineData("2024-07-04 09:15", "July 4, 2024 9:15 AM")]
    [InlineData(null, "")]
    public void ToMonthDayYearAmPm_NullableDateTime_ReturnsCorrectFormat(string? dateTimeString, string expected)
    {
        // Arrange
        DateTime? dateTime = dateTimeString != null ? DateTime.Parse(dateTimeString) : null;

        // Act
        string result = dateTime.ToMonthDayYearAmPm();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region To Date /Time Order 
    [Fact]
    public void ToDateOrderBy_DateOnly_NonNull_ReturnsCorrectValue()
    {
        DateOnly date = new(2023, 4, 5);
        long expected = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long actual = date.ToDateOrderBy();
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void ToDateOrderBy_NullableDateOnly_NonNull_ReturnsCorrectValue()
    {
        DateOnly? date = new DateOnly(2023, 4, 5);
        long expected = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long actual = date.ToDateOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateOrderBy_DateOnly_Null_ReturnsCorrectValue()
    {
        DateOnly? date = null;
        long expected = 9999L * 100_000_000L + 13L * 1_000_000L + 32L * 10_000L;
        long actual = date.ToDateOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateOrderBy_DateTime_NonNull_ReturnsCorrectValue()
    {
        DateTime dt = new(2023, 4, 5, 14, 30, 15);
        long expected = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long actual = dt.ToDateOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateOrderBy_NullableDateTime_NonNull_ReturnsCorrectValue()
    {
        DateTime? dt = new DateTime(2023, 4, 5, 14, 30, 15);
        long expected = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long actual = dt.ToDateOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateOrderBy_DateTime_Null_ReturnsCorrectValue()
    {
        DateTime? dt = null;
        long expected = 9999L * 100_000_000L + 13L * 1_000_000L + 32L * 10_000L;
        long actual = dt.ToDateOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToTimeOrderBy_TimeOnly_NonNull_ReturnsCorrectValue()
    {
        TimeOnly time = new(14, 30, 15);
        long expected = 14L * 100L + 30L;
        long actual = time.ToTimeOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToTimeOrderBy_NullableTimeOnly_NonNull_ReturnsCorrectValue()
    {
        TimeOnly? time = new TimeOnly(14, 30, 15);
        long expected = 14L * 100L + 30L;
        long actual = time.ToTimeOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToTimeOrderBy_Null_ReturnsCorrectValue()
    {
        TimeOnly? time = null;
        long expected = 0L;
        long actual = time.ToTimeOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_Date_And_Time_NonNull_ReturnsCorrectValue()
    {
        DateOnly date = new(2023, 4, 5);
        TimeOnly time = new(14, 30, 15);
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_Date_And_NullableTime_NonNull_ReturnsCorrectValue()
    {
        DateOnly date = new(2023, 4, 5);
        TimeOnly? time = new TimeOnly(14, 30, 15);
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_NullableDate_And_Time_NonNull_ReturnsCorrectValue()
    {
        DateOnly? date = new DateOnly(2023, 4, 5);
        TimeOnly time = new(14, 30, 15);
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_NullableDate_And_NullableTime_NonNull_ReturnsCorrectValue()
    {
        DateOnly? date = new DateOnly(2023, 4, 5);
        TimeOnly? time = new TimeOnly(14, 30, 15);
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_TimeNonNull_NullDate_ReturnsCorrectValue()
    {
        DateOnly? date = null;
        TimeOnly time = new(14, 30, 15);
        long expectedDate = 9999L * 100_000_000L + 13L * 1_000_000L + 32L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_NullableTimeNonNull_NullDate_ReturnsCorrectValue()
    {
        DateOnly? date = null;
        TimeOnly? time = new TimeOnly(14, 30, 15);
        long expectedDate = 9999L * 100_000_000L + 13L * 1_000_000L + 32L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_DateNonNull_NullTime_ReturnsCorrectValue()
    {
        DateOnly date = new(2023, 4, 5);
        TimeOnly? time = null;
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 0L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_NullableDateNonNull_NullTime_ReturnsCorrectValue()
    {
        DateOnly? date = new DateOnly(2023, 4, 5);
        TimeOnly? time = null;
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 0L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_BothNull_ReturnsCorrectValue()
    {
        DateOnly? date = null;
        TimeOnly? time = null;
        long expectedDate = 9999L * 100_000_000L + 13L * 1_000_000L + 32L * 10_000L;
        long expectedTime = 0L;
        long expected = expectedDate + expectedTime;
        long actual = date.ToDateTimeOrderBy(time);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_DateTime_NonNull_ReturnsCorrectValue()
    {
        DateTime dateTime = new(2023, 4, 5, 14, 30, 15);
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = dateTime.ToDateTimeOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_NullableDateTime_NonNull_ReturnsCorrectValue()
    {
        DateTime? dateTime = new DateTime(2023, 4, 5, 14, 30, 15);
        long expectedDate = 2023L * 100_000_000L + 4L * 1_000_000L + 5L * 10_000L;
        long expectedTime = 14L * 100L + 30L;
        long expected = expectedDate + expectedTime;
        long actual = dateTime.ToDateTimeOrderBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDateTimeOrderBy_DateTime_Null_ReturnsCorrectValue()
    {
        DateTime? dt = null;
        long expectedDate = 9999L * 100_000_000L + 13L * 1_000_000L + 32L * 10_000L;
        long expectedTime = 0L;
        long expected = expectedDate + expectedTime;
        long actual = dt.ToDateTimeOrderBy();
        Assert.Equal(expected, actual);
    }
    #endregion

    #region IsNullOrEmpty IsNotNullOrEmpty

    [Fact]
    public void DateTime_IsNullOrEmpty_NewIsTrue()
    {
        DateTime value = new();

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateTime_IsNullOrEmpty_DefaultIsTrue()
    {
        DateTime value = default;

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateTime_IsNullOrEmpty_ValueIsFalse()
    {
        DateTime value = DateTime.Today;

        Assert.False(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateTime_IsNotNullOrEmpty_NewIsFalse()
    {
        DateTime value = new();

        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateTime_IsNotNullOrEmpty_ValueIsTrue()
    {
        DateTime value = DateTime.Today;

        Assert.True(value.IsNotNullOrEmpty());
    }


    [Fact]
    public void DateOnly_IsNullOrEmpty_NewIsTrue()
    {
        DateOnly value = new();

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateOnly_IsNullOrEmpty_DefaultIsTrue()
    {
        DateOnly value = default;

        Assert.True(value.IsNullOrEmpty());
    }


    [Fact]
    public void DateOnly_IsNullOrEmpty_ValueIsFalse()
    {
        // Using today's date for a valid value.
        DateOnly value = DateOnly.FromDateTime(DateTime.Today);
        Assert.False(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateOnly_IsNotNullOrEmpty_NewIsFalse()
    {
        // default(DateOnly) represents an "empty" value.
        DateOnly value = new();
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateOnly_IsNotNullOrEmpty_DefaultIsFalse()
    {
        // default(DateOnly) represents an "empty" value.
        DateOnly value = default;
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateOnly_IsNotNullOrEmpty_ValueIsTrue()
    {
        DateOnly value = DateOnly.FromDateTime(DateTime.Today);
        Assert.True(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void TimeOnly_IsNullOrEmpty_NewIsTrue()
    {
        TimeOnly value = new();

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void TimeOnly_IsNullOrEmpty_DefaultIsTrue()
    {
        TimeOnly value = default;

        Assert.True(value.IsNullOrEmpty());
    }
    [Fact]
    public void TimeOnly_IsNullOrEmpty_ValueIsFalse()
    {
        // Use the current time for a valid TimeOnly value.
        TimeOnly value = TimeOnly.FromDateTime(DateTime.Now);
        Assert.False(value.IsNullOrEmpty());
    }


    [Fact]
    public void TimeOnly_IsNotNullOrEmpty_NewIsFalse()
    {
        // default(TimeOnly) acts as the empty value.
        TimeOnly value = new();
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void TimeOnly_IsNotNullOrEmpty_DefaultIsFalse()
    {
        // default(TimeOnly) acts as the empty value.
        TimeOnly value = default;
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void TimeOnly_IsNotNullOrEmpty_ValueIsTrue()
    {
        TimeOnly value = TimeOnly.FromDateTime(DateTime.Now);
        Assert.True(value.IsNotNullOrEmpty());
    }
    #endregion

    #region IsNullOrEmpty IsNotNullOrEmpty for nullables

    [Fact]
    public void DateTimeNullable_IsNullOrEmpty_NullIsTrue()
    {
        DateTime? value = null;

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateTimeNullable_IsNullOrEmpty_NewIsTrue()
    {
        DateTime? value = new DateTime();

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateTimeNullable_IsNullOrEmpty_DefaultIsTrue()
    {
        DateTime? value = default;

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateTimeNullable_IsNullOrEmpty_ValueIsFalse()
    {
        DateTime? value = DateTime.Today;

        Assert.False(value.IsNullOrEmpty());
    }


    [Fact]
    public void DateTimeNullable_IsNotNullOrEmpty_NullIsFalse()
    {
        DateTime? value = null;

        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateTimeNullable_IsNotNullOrEmpty_NewIsFalse()
    {
        DateTime? value = new DateTime();

        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateTimeNullable_IsNotNullOrEmpty_ValueIsTrue()
    {
        DateTime? value = DateTime.Today;

        Assert.True(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNullOrEmpty_NullIsTrue()
    {
        DateOnly? value = null;

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNullOrEmpty_NewIsTrue()
    {
        DateOnly? value = new DateOnly();

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNullOrEmpty_DefaultIsTrue()
    {
        DateOnly? value = default;

        Assert.True(value.IsNullOrEmpty());
    }


    [Fact]
    public void DateOnlyNullable_IsNullOrEmpty_ValueIsFalse()
    {
        // Using today's date for a valid value.
        DateOnly? value = DateOnly.FromDateTime(DateTime.Today);
        Assert.False(value.IsNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNotNullOrEmpty_NullIsFalse()
    {
        DateOnly? value = null;
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNotNullOrEmpty_NewIsFalse()
    {
        // default(DateOnly) represents an "empty" value.
        DateOnly? value = new DateOnly();
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNotNullOrEmpty_DefaultIsFalse()
    {
        // default(DateOnly) represents an "empty" value.
        DateOnly? value = default;
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void DateOnlyNullable_IsNotNullOrEmpty_ValueIsTrue()
    {
        DateOnly? value = DateOnly.FromDateTime(DateTime.Today);
        Assert.True(value.IsNotNullOrEmpty());
    }





    [Fact]
    public void TimeOnlyNullable_IsNullOrEmpty_NullIsTrue()
    {
        TimeOnly? value = null;

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void TimeOnlyNullable_IsNullOrEmpty_NewIsTrue()
    {
        TimeOnly? value = new TimeOnly();

        Assert.True(value.IsNullOrEmpty());
    }

    [Fact]
    public void TimeOnlyNullable_IsNullOrEmpty_DefaultIsTrue()
    {
        TimeOnly? value = default;

        Assert.True(value.IsNullOrEmpty());
    }
    [Fact]
    public void TimeOnlyNullable_IsNullOrEmpty_ValueIsFalse()
    {
        // Use the current time for a valid TimeOnly value.
        TimeOnly? value = TimeOnly.FromDateTime(DateTime.Now);
        Assert.False(value.IsNullOrEmpty());
    }

    [Fact]
    public void TimeOnlyNullable_IsNotNullOrEmpty_NullIsFalse()
    {
        TimeOnly? value = null;
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void TimeOnlyNullable_IsNotNullOrEmpty_NewIsFalse()
    {
        // default(TimeOnly) acts as the empty value.
        TimeOnly? value = new TimeOnly();
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void TimeOnlyNullable_IsNotNullOrEmpty_DefaultIsFalse()
    {
        // default(TimeOnly) acts as the empty value.
        TimeOnly? value = default;
        Assert.False(value.IsNotNullOrEmpty());
    }

    [Fact]
    public void TimeOnlyNullable_IsNotNullOrEmpty_ValueIsTrue()
    {
        TimeOnly? value = TimeOnly.FromDateTime(DateTime.Now);
        Assert.True(value.IsNotNullOrEmpty());
    }
    #endregion
}
