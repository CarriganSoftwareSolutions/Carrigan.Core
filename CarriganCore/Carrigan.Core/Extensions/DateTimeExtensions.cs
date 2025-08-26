using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;
//IGNORE SPELLING: dddd mm yyyy utc

/// <summary>
/// Extension methods for DateTime, TimeOnly and DateOnly
/// </summary>
public static class DateTimeExtensions
{
    #region ToAmPmString

    /// <summary>
    /// Converts a TimeOnly value to a string formatted as "h:mm tt" (12-hour format with AM/PM).
    /// </summary>
    /// <param name="time">The TimeOnly value to format.</param>
    /// <returns>A string in the format "h:mm tt".</returns>
    /// <example>
    /// For example, a TimeOnly value of "14:30" would be formatted as "2:30 PM".
    /// </example>
    public static string ToAmPmString(this TimeOnly time)
    {
        return time.ToString("h:mm tt");
    }

    /// <summary>
    /// Converts a nullable TimeOnly value to a string formatted as "h:mm tt" (12-hour format with AM/PM), or an empty string if the value is null.
    /// </summary>
    /// <param name="time">The nullable TimeOnly value to format.</param>
    /// <returns>A string in the format "h:mm tt" or an empty string if null.</returns>
    /// <example>
    /// For example, a nullable TimeOnly value of "14:30" would be formatted as "2:30 PM".
    /// A null value would return an empty string.
    /// </example>
    public static string ToAmPmString(this TimeOnly? time)
    {
        return time?.ToString("h:mm tt") ?? string.Empty;
    }
    #endregion

    #region ToDateOnlyExtension

    /// <summary>
    /// Extracts the Date from a DateTime object.
    /// </summary>
    /// <param name="date">date as a <see cref="DateTime"/></param>
    /// <returns>The date as a <see cref="DateOnly"/></returns>
    public static DateOnly ToDateOnly(this DateTime date) =>
        DateOnly.FromDateTime(date);
    /// <summary>
    /// Extracts the Date from a DateTime object.
    /// </summary>
    /// <param name="date">date as a <see cref="DateTime"/></param>
    /// <returns>The date as a <see cref="DateOnly"/></returns>
    public static DateOnly? ToDateOnly(this DateTime? date) =>
        date?.ToDateOnly();
    #endregion

    #region ToDayOfWeekExtension
    /// <summary>
    /// Converts a DateTime value to a string representing the full day of the week.
    /// </summary>
    /// <param name="date">The DateTime value to format.</param>
    /// <returns>A string representing the day of the week, in the format "dddd".</returns>
    /// <example>
    /// For example, a DateTime value of "2024-09-29" (Sunday) would be formatted as "Sunday".
    /// </example>
    public static string ToDayOfWeek(this DateTime date)
    {
        return date.ToString("dddd");
    }

    /// <summary>
    /// Converts a nullable DateTime value to a string representing the full day of the week, or an empty string if the value is null.
    /// </summary>
    /// <param name="date">The nullable DateTime value to format.</param>
    /// <returns>A string representing the day of the week, in the format "dddd", or an empty string if null.</returns>
    /// <example>
    /// For example, a DateTime value of "2024-09-29" (Sunday) would be formatted as "Sunday".
    /// A null value would return an empty string.
    /// </example>
    public static string ToDayOfWeek(this DateTime? date)
    {
        return date?.ToString("dddd") ?? string.Empty;
    }

    /// <summary>
    /// Converts a DateOnly value to a string representing the full day of the week.
    /// </summary>
    /// <param name="date">The DateOnly value to format.</param>
    /// <returns>A string representing the day of the week, in the format "dddd".</returns>
    /// <example>
    /// For example, a DateOnly value of "2024-09-29" (Sunday) would be formatted as "Sunday".
    /// </example>
    public static string ToDayOfWeek(this DateOnly date)
    {
        return date.ToString("dddd");
    }

    /// <summary>
    /// Converts a nullable DateOnly value to a string representing the full day of the week, or an empty string if the value is null.
    /// </summary>
    /// <param name="date">The nullable DateOnly value to format.</param>
    /// <returns>A string representing the day of the week, in the format "dddd", or an empty string if null.</returns>
    /// <example>
    /// For example, a DateOnly value of "2024-09-29" (Sunday) would be formatted as "Sunday".
    /// A null value would return an empty string.
    /// </example>
    public static string ToDayOfWeek(this DateOnly? date)
    {
        return date?.ToString("dddd") ?? string.Empty;
    }
    #endregion

    #region ToTimeOnlyExtension
    /// <summary>
    /// Extracts the Time as a <see cref="TimeOnly"/> from a <see cref="DateTime"/>
    /// </summary>
    /// <param name="time"></param>
    /// <returns>the Time as a <see cref="TimeOnly"/> from a <see cref="DateTime"/></returns>
    public static TimeOnly ToTimeOnly(this DateTime time) =>
        TimeOnly.FromDateTime(time);

    /// <summary>
    /// Extracts the Time as a <see cref="TimeOnly"/> from a <see cref="DateTime"/>
    /// </summary>
    /// <param name="time"></param>
    /// <returns>the Time as a <see cref="TimeOnly"/> from a <see cref="DateTime"/></returns>
    public static TimeOnly? ToTimeOnly(this DateTime? time) =>
        time?.ToTimeOnly();
    #endregion

    #region ToMonthDayYearLongExtension
    /// <summary>
    /// Converts a DateTime to a string formatted as "MMMM, d, yyyy".
    /// </summary>
    /// <param name="date">The DateTime value to format.</param>
    /// <returns>A string in the format "Month day, year".</returns>
    /// <example>
    /// For example, a DateTime value of "2024-09-29" would be formatted as "September 29, 2024".
    /// </example>
    public static string ToMonthDayYearLong(this DateTime date)
    {
        return date.ToString("MMMM d, yyyy");
    }

    /// <summary>
    /// Converts a nullable DateTime to a string formatted as "MMMM, d, yyyy", or an empty string if the value is null.
    /// </summary>
    /// <param name="date">The nullable DateTime value to format.</param>
    /// <returns>A string in the format "Month day, year" or an empty string if null.</returns>
    /// <example>
    /// For example, a DateTime value of "2024-09-29" would be formatted as "September 29, 2024".
    /// A null value would return an empty string.
    /// </example>
    public static string ToMonthDayYearLong(this DateTime? date)
    {
        return date?.ToString("MMMM d, yyyy") ?? string.Empty;
    }

    /// <summary>
    /// Converts a DateOnly to a string formatted as "MMMM, d, yyyy".
    /// </summary>
    /// <param name="date">The DateOnly value to format.</param>
    /// <returns>A string in the format "Month day, year".</returns>
    /// <example>
    /// For example, a DateOnly value of "2024-09-29" would be formatted as "September 29, 2024".
    /// </example>
    public static string ToMonthDayYearLong(this DateOnly date)
    {
        return date.ToString("MMMM d, yyyy");
    }

    /// <summary>
    /// Converts a nullable DateOnly to a string formatted as "MMMM, d, yyyy", or an empty string if the value is null.
    /// </summary>
    /// <param name="date">The nullable DateOnly value to format.</param>
    /// <returns>A string in the format "Month day, year" or an empty string if null.</returns>
    /// <example>
    /// For example, a DateOnly value of "2024-09-29" would be formatted as "September 29, 2024".
    /// A null value would return an empty string.
    /// </example>
    public static string ToMonthDayYearLong(this DateOnly? date)
    {
        return date?.ToString("MMMM d, yyyy") ?? string.Empty;
    }
    #endregion

    #region ToMonthDayYearAmPmExtension
    /// <summary>
    /// Converts a DateTime to a string formatted as "MMMM, d, yyyy h:mm tt".
    /// </summary>
    /// <param name="date">The DateTime value to format.</param>
    /// <returns>A string in the format "Month day, year hour:minute AM/PM".</returns>
    /// <example>
    /// For example, a DateTime value of "2024-09-29 14:30:00" would be formatted as "September 29, 2024 2:30 PM".
    /// </example>
    public static string ToMonthDayYearAmPm(this DateTime date)
    {
        return date.ToString("MMMM d, yyyy h:mm tt");
    }

    /// <summary>
    /// Converts a nullable DateTime to a string formatted as "MMMM, d, yyyy h:mm tt", or an empty string if the value is null.
    /// </summary>
    /// <param name="date">The nullable DateTime value to format.</param>
    /// <returns>A string in the format "Month day, year hour:minute AM/PM" or an empty string if null.</returns>
    /// <example>
    /// For example, a DateTime value of "2024-09-29 14:30:00" would be formatted as "September 29, 2024 2:30 PM".
    /// A null value would return an empty string.
    /// </example>
    public static string ToMonthDayYearAmPm(this DateTime? date)
    {
        return date?.ToString("MMMM d, yyyy h:mm tt") ?? string.Empty;
    }
    #endregion

    #region Covert to long Order value
    /// <summary>
    /// Converts a <see cref="DateOnly"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="DateOnly"/> as a sortable <see cref="long"/></returns>
    public static long ToDateOrderBy(this DateOnly value)
    {
        return    value.Year *  1_00_00_00_00L +            // Multiplier for year:   1 00, 00 0,0 00
                  value.Month * 1_00_00_00L +               // Multiplier for month:     1, 00 0,0 00
                  value.Day *   1_00_00L;                   // Multiplier for day:           1 0,0 00
    }
    /// <summary>
    /// Converts a <see cref="DateOnly"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="DateOnly"/> as a sortable <see cref="long"/></returns>
    public static long ToDateOrderBy(this DateOnly? value)
    {
        if (value is null)
            return 9999_13_32_00_00L;                       // 9999 years, 13 months, 32 days, 00 hours, 00 minutes
                                                            //                    999,9 13, 32 0,0 00                  
        else
            return value.Value.Year *  1_00_00_00_00L +     // Multiplier for year:   1 00, 00 0,0 00
                   value.Value.Month * 1_00_00_00L +        // Multiplier for month:     1, 00 0,0 00
                   value.Value.Day *   1_00_00L;            // Multiplier for day:           1 0,0 00
    }

    /// <summary>
    /// Converts a date as a <see cref="DateTime"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the Date as a sortable <see cref="long"/></returns>
    public static long ToDateOrderBy(this DateTime value)
    {
         return    value.Year *  1_00_00_00_00L +           // Multiplier for year:   1 00, 00 0,0 00
                   value.Month * 1_00_00_00L +              // Multiplier for month:     1, 00 0,0 00
                   value.Day *   1_00_00L;                  // Multiplier for day:           1 0,0 00
    }

    /// <summary>
    /// Converts a date as a <see cref="DateTime"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the Date as a sortable <see cref="long"/></returns>
    public static long ToDateOrderBy(this DateTime? value)
    {
        if (value is null)
            return 9999_13_32_00_00L;                       // 9999 years, 13 months, 32 days, 00 hours, 00 minutes
                                                            //                    999,9 13, 32 0,0 00                  
        else
            return value.Value.Year *  1_00_00_00_00L +     // Multiplier for year:   1 00, 00 0,0 00
                   value.Value.Month * 1_00_00_00L +        // Multiplier for month:     1, 00 0,0 00
                   value.Value.Day *   1_00_00L;            // Multiplier for day:           1 0,0 00

    }


    /// <summary>
    /// Converts a <see cref="TimeOnly"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="TimeOnly"/> as a sortable <see cref="long"/></returns>
    public static long ToTimeOrderBy(this TimeOnly value)
    {
         return    
                   value.Hour *  1_00L +                    // Multiplier for hour:              1 00
                   value.Minute;                            // Multiplier for minute:               1

    }

    /// <summary>
    /// Converts a <see cref="TimeOnly"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="TimeOnly"/> as a sortable <see cref="long"/></returns>
    public static long ToTimeOrderBy(this TimeOnly? value)
    {
        if (value is null)
            return 0L;                                      // 0 hours, 0 minutes
                                                            //                                  0,0 0                  
        else
            return value.Value.Hour *  1_00L +              // Multiplier for hour:              1 00
                   value.Value.Minute;                      // Multiplier for minute:               1

    }

    /// <summary>
    /// Converts a <see cref="DateOnly"/> and a <see cref="TimeOnly"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="DateOnly"/> and a <see cref="TimeOnly"/> as a sortable <see cref="long"/></returns>
    public static long ToDateTimeOrderBy(this DateOnly dateValue, TimeOnly? timeValue)
    {
        if (timeValue is not null)
            return dateValue.Year *  1_00_00_00_00L +       // Multiplier for year:   1 00, 00 0,0 00
                   dateValue.Month * 1_00_00_00L +          // Multiplier for month:     1, 00 0,0 00
                   dateValue.Day *   1_00_00L +             // Multiplier for day:           1 0,0 00
                   timeValue.Value.Hour *  1_00L +          // Multiplier for hour:              1 00
                   timeValue.Value.Minute;                  // Multiplier for minute:               1 

        else //time is null
            return                                      // 0 hours, 0 minutes                0 00    
                   dateValue.Year *  1_00_00_00_00L +       // Multiplier for year:   1 00, 00 0,0 00
                   dateValue.Month * 1_00_00_00L +          // Multiplier for month:     1, 00 0,0 00
                   dateValue.Day *   1_00_00L ;             // Multiplier for day:           1 0,0 00   
    }

    /// <summary>
    /// Converts a <see cref="DateOnly"/> and a <see cref="TimeOnly"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="DateOnly"/> and a <see cref="TimeOnly"/> as a sortable <see cref="long"/></returns>
    public static long ToDateTimeOrderBy(this DateOnly? dateValue, TimeOnly? timeValue)
    {             
        if (dateValue is not null && timeValue is not null)
            return dateValue.Value.Year *  1_00_00_00_00L + // Multiplier for year:   1 00, 00 0,0 00
                   dateValue.Value.Month * 1_00_00_00L +    // Multiplier for month:     1, 00 0,0 00
                   dateValue.Value.Day *   1_00_00L +       // Multiplier for day:           1 0,0 00
                   timeValue.Value.Hour *  1_00L +          // Multiplier for hour:              1 00
                   timeValue.Value.Minute;                  // Multiplier for minute:               1 
        else if(dateValue is null && timeValue is not null)
            return 9999_13_32_00_00L +                      // 9999 years, 13 months, 32 days, 00 hours, 00 minutes
                                                            //                    999,9 13, 32 0,0 00     
                   timeValue.Value.Hour *  1_00L +          // Multiplier for hour:              1 00
                   timeValue.Value.Minute;                  // Multiplier for minute:               1

        else if (dateValue is not null && timeValue is null)
            return                                     
                   dateValue.Value.Year *  1_00_00_00_00L + // Multiplier for year:   1 00, 00 0,0 00
                   dateValue.Value.Month * 1_00_00_00L +    // Multiplier for month:     1, 00 0,0 00
                   dateValue.Value.Day *   1_00_00L ;       // Multiplier for day:           1 0,0 00
        else //both are null
            return 9999_13_32_00_00L;                       // 9999 years, 13 months, 32 days, 25 hours, 61 minutes
                                                            //                    999,9 13, 32 2,5 61    


    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="DateTime"/> as a sortable <see cref="long"/></returns>
    public static long ToDateTimeOrderBy(this DateTime value)
    {
         return    value.Year *  1_00_00_00_00L +           // Multiplier for year:   1 00, 00 0,0 00
                   value.Month * 1_00_00_00L +              // Multiplier for month:     1, 00 0,0 00
                   value.Day *   1_00_00L +                 // Multiplier for day:           1 0,0 00
                   value.Hour *  1_00L +                    // Multiplier for hour:              1 00
                   value.Minute;                            // Multiplier for minute:               1

    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a sortable <see cref="long"/>.
    /// This is not likely to useful to any but me, and the one very specific use case I had.
    /// </summary
    /// <param name="value"> the date</param>
    /// <returns>the <see cref="DateTime"/> as a sortable <see cref="long"/></returns>
    public static long ToDateTimeOrderBy(this DateTime? value)
    {
        if (value is null)
            return 9999_13_32_00_00L;                       // 9999 years, 13 months, 32 days, 0 hours, 0 minutes
                                                            //                    999,9 13, 32 0,0 00                  
        else
            return value.Value.Year *  1_00_00_00_00L +     // Multiplier for year:   1 00, 00 0,0 00
                   value.Value.Month * 1_00_00_00L +        // Multiplier for month:     1, 00 0,0 00
                   value.Value.Day *   1_00_00L +           // Multiplier for day:           1 0,0 00
                   value.Value.Hour *  1_00L +              // Multiplier for hour:              1 00
                   value.Value.Minute;                      // Multiplier for minute:               1

    }

    #endregion

    #region IsNullOrEmpty, IsNotNullOrEmpty

    /// <summary>
    /// Extension method to determine if a <see cref="DateTime"/> is Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if the <see cref="DateTime"/> is null or Empty, else false</returns>
    public static bool IsNullOrEmpty(this DateTime? value) =>
        value == null || value.Equals(new DateTime());

    /// <summary>
    /// Extension method to determine if a <see cref="DateTime"/> is Not Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>false if the <see cref="DateTime"/> is null or Empty, else true</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this DateTime? value) =>
        !value.IsNullOrEmpty();

    /// <summary>
    /// Extension method to determine if a <see cref="DateTime"/> is Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if the <see cref="DateTime"/> is null or Empty, else false</returns>
    public static bool IsNullOrEmpty(this DateTime value) =>
        value == null || value.Equals(new DateTime());

    /// <summary>
    /// Extension method to determine if a <see cref="DateTime"/> is Not Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>false if the <see cref="DateTime"/> is null or Empty, else true</returns>
    public static bool IsNotNullOrEmpty  ([NotNullWhen(true)] this DateTime value) =>
        !value.IsNullOrEmpty();


    /// <summary>
    /// Extension method to determine if a <see cref="DateOnly"/> is Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if the <see cref="DateOnly"/> is null or Empty, else false</returns>
    public static bool IsNullOrEmpty(this DateOnly? value) =>
        value == null || value.Equals(new DateOnly());

    /// <summary>
    /// Extension method to determine if a <see cref="DateOnly"/> is Not Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>false if the <see cref="DateOnly"/> is null or Empty, else true</returns>
    public static bool IsNotNullOrEmpty ([NotNullWhen(true)] this DateOnly? value) =>
        !value.IsNullOrEmpty();

    /// <summary>
    /// Extension method to determine if a <see cref="DateOnly"/> is Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if the <see cref="DateOnly"/> is null or Empty, else false</returns>
    public static bool IsNullOrEmpty(this DateOnly value) =>
        value == null || value.Equals(new DateOnly());

    /// <summary>
    /// Extension method to determine if a <see cref="DateOnly"/> is Not Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>false if the <see cref="DateOnly"/> is null or Empty, else true</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this DateOnly value) =>
        !value.IsNullOrEmpty();

    /// <summary>
    /// Extension method to determine if a <see cref="TimeOnly"/> is Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if the <see cref="TimeOnly"/> is null or Empty, else false</returns>
    public static bool IsNullOrEmpty(this TimeOnly? value) =>
        value == null || value.Equals(new TimeOnly());

    /// <summary>
    /// Extension method to determine if a <see cref="TimeOnly"/> is Not Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>false if the <see cref="TimeOnly"/> is null or Empty, else true</returns>
    public static bool IsNotNullOrEmpty ([NotNullWhen(true)] this TimeOnly? value) =>
        !value.IsNullOrEmpty();

    /// <summary>
    /// Extension method to determine if a <see cref="TimeOnly"/> is Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if the <see cref="TimeOnly"/> is null or Empty, else false</returns>
    public static bool IsNullOrEmpty(this TimeOnly value) =>
        value == null || value.Equals(new TimeOnly());

    /// <summary>
    /// Extension method to determine if a <see cref="TimeOnly"/> is Not Null Or Empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>false if the <see cref="TimeOnly"/> is null or Empty, else true</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this TimeOnly value) =>
        !value.IsNullOrEmpty();
    #endregion

    #region to filename format
    /// <summary>
    /// Creates a string for a filename of a <see cref="DateTime"/> object in a format that is date sortable.
    /// Intended to use either as the full filename or the prefix for a filename.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string ToFileName(this DateTime dateTime)
    {
        // Ensure we use the current local time
        DateTime localDateTime = dateTime.ToLocalTime();
        // Format: yyyy-MM-dd'T'HH_mm_ss
        return localDateTime.ToString("yyyy-MM-dd'T'HH_mm_ss");
    }
    #endregion

    /// <summary>
    /// Formats a <see cref="TimeSpan"/> as human-readable text including only non-zero
    /// days, hours, and minutes. If all are zero, returns empty string.
    /// </summary>
    /// <remarks>
    /// Examples:
    /// -  /// 1 day, 2 hours, 5 minutes
    /// -  /// 2 hours, 1 minute
    /// -  /// 3 minutes
    /// -  /// 0 minutes
    /// -  /// -1 day, 3 hours
    /// </remarks>
    /// <param name="Duration">The time span to format.</param>
    /// <returns>A string like "1 day, 2 hours, 5 minutes".</returns>
    public static string ToHumanString(this TimeSpan Duration)
    {
        bool isNegative = Duration.Ticks < 0;
        TimeSpan absoluteDuration = Duration.Duration();

        List<string> subtext = [];
        if (absoluteDuration.Days != 0)
            subtext.Add($"{absoluteDuration.Days} {(absoluteDuration.Days == 1 ? "day" : "days")}");
        if (absoluteDuration.Hours != 0)
            subtext.Add($"{absoluteDuration.Hours} {(absoluteDuration.Hours == 1 ? "hour" : "hours")}");
        if (absoluteDuration.Minutes != 0)
            subtext.Add($"{absoluteDuration.Minutes} {(absoluteDuration.Minutes == 1 ? "minute" : "minutes")}");

        return isNegative ? "-" + subtext.JoinAnd() : subtext.JoinAnd();
    }


    /// <summary>
    /// Formats a <see cref="TimeSpan"/> as human-readable text including only non-zero
    /// days, hours, and minutes. If all are zero, returns empty string.
    /// </summary>
    /// <remarks>
    /// Examples:
    /// -  /// 1 day, 2 hours, 5 minutes
    /// -  /// 2 hours, 1 minute
    /// -  /// 3 minutes
    /// -  /// 0 minutes
    /// -  /// -1 day, 3 hours
    /// </remarks>
    /// <param name="Duration">The time span to format.</param>
    /// <returns>A string like "1 day, 2 hours, 5 minutes".</returns>
    public static string ToHumanString(this TimeSpan? Duration)
    {
        return Duration is null ? string.Empty : Duration.Value.ToHumanString();
    }
}
