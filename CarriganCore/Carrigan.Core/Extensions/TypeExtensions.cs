namespace Carrigan.Core.Extensions;

/// <summary>
/// <see cref="Type"/> extension methods
/// </summary>
public static class TypeExtensions
{
    #region IsBoolType
    /// <summary>
    /// Returns true if the <see cref="Type"/> is <see cref="bool"/> else false
    /// </summary>
    /// <param name="type">the type being tested</param>
    /// <returns>
    /// Returns true if the <see cref="Type"/> is <see cref="bool"/> else false
    /// </returns>
    public static bool IsBoolType(this Type type) =>
        type == typeof(bool) || type == typeof(bool?);
    #endregion

    #region IsDateOnlyType
    /// <summary>
    /// Returns true if the <see cref="Type"/> is <see cref="DateOnly"/> else false
    /// </summary>
    /// <param name="type">the type being tested</param>
    /// <returns>
    /// Returns true if the <see cref="Type"/> is <see cref="DateOnly"/> else false
    /// </returns>
    public static bool IsDateOnlyType(this Type type) =>
        type == typeof(DateOnly) || type == typeof(DateOnly?);
    #endregion

    #region IsFloatingPointType
    /// <summary>
    /// Determines whether the specified type is a floating-point type (including nullable floating-point types).
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is a floating-point type or a nullable floating-point type; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsFloatingPointType(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        return Type.GetTypeCode(underlyingType) switch
        {
            // float
            TypeCode.Single or TypeCode.Double or TypeCode.Decimal => true,
            _ => false,
        };
    }
    #endregion

    #region IsIntegerType
    /// <summary>
    /// Determines whether the specified type is an integer type (including nullable integer types).
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>
    ///   <c>true</c> if the specified type is an integer type or a nullable integer type ; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsIntegerType(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        return Type.GetTypeCode(underlyingType) switch
        {
            TypeCode.SByte or TypeCode.Byte or TypeCode.Int16 or TypeCode.UInt16 or TypeCode.Int32 or TypeCode.UInt32 or TypeCode.Int64 or TypeCode.UInt64 => true,
            _ => false,
        };
    }
    #endregion

    /// <summary>
    /// Returns true if the <see cref="Type"/> is <see cref="string"/> else false
    /// </summary>
    /// <param name="type">the type being tested</param>
    /// <returns>
    /// Returns true if the <see cref="Type"/> is <see cref="string"/> else false
    /// </returns>
    #region IsStringType
    public static bool IsStringType(this Type type) =>
        type == typeof(string);
    #endregion

    #region IsTimeOnlyType
    /// <summary>
    /// Returns true if the <see cref="Type"/> is <see cref="TimeOnly"/> else false
    /// </summary>
    /// <param name="type">the type being tested</param>
    /// <returns>
    /// Returns true if the <see cref="Type"/> is <see cref="TimeOnly"/> else false
    /// </returns>
    public static bool IsTimeOnlyType(this Type type) =>
        type == typeof(TimeOnly) || type == typeof(TimeOnly?);
    #endregion



    /// <summary>
    /// Returns the underlying type for a given <see cref="Type"/>.
    /// </summary>
    /// <remarks>
    /// Behavior:
    /// <list type="bullet">
    /// <item><description><b>Nullable&lt;T&gt;</b>: returns <c>T</c>.</description></item>
    /// <item><description><b>Enum</b>: returns the enum's underlying integral type.</description></item>
    /// <item><description>Anything else: returns the type itself.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="type">The input type.</param>
    /// <returns>
    /// The "underlying" type as defined above.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="type"/> is <c>null</c>.
    /// </exception>
    public static Type GetUnderlyingType(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Type? nullableUnderlying = Nullable.GetUnderlyingType(type);
        if (nullableUnderlying is not null)
        {
            return nullableUnderlying;
        }

        if (type.IsEnum)
        {
            return Enum.GetUnderlyingType(type);
        }

        return type;
    }
}
