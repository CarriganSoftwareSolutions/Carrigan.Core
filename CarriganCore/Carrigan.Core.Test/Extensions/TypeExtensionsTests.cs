using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class TypeExtensionsTests
{    private enum ByteEnum : byte { A = 1 }
    private enum LongEnum : long { A = 1 }

    #region IsBoolType Tests

    /// <summary>
    /// Tests the IsBoolType extension method for non-nullable and nullable bool types.
    /// </summary>
    [Theory]
    [InlineData(typeof(bool), true)]
    [InlineData(typeof(bool?), true)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(object), false)]
    [InlineData(typeof(DateTime), false)]
    [InlineData(typeof(DateTime?), false)]
    public void IsBoolType_Tests(Type type, bool expected)
    {
        // Act
        bool result = type.IsBoolType();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IsDateOnlyType Tests

    /// <summary>
    /// Tests the IsDateOnlyType extension method for non-nullable and nullable DateOnly types.
    /// </summary>
    [Theory]
    [InlineData(typeof(DateOnly), true)]
    [InlineData(typeof(DateOnly?), true)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(object), false)]
    [InlineData(typeof(TimeOnly), false)]
    [InlineData(typeof(TimeOnly?), false)]
    public void IsDateOnlyType_Tests(Type type, bool expected)
    {
        // Act
        bool result = type.IsDateOnlyType();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IsFloatingPointType Tests

    /// <summary>
    /// Tests the IsFloatingPointType extension method for various floating-point and non-floating-point types.
    /// </summary>
    [Theory]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(float?), true)]
    [InlineData(typeof(double), true)]
    [InlineData(typeof(double?), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(decimal?), true)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(bool), false)]
    [InlineData(typeof(bool?), false)]
    [InlineData(typeof(object), false)]
    public void IsFloatingPointType_Tests(Type type, bool expected)
    {
        // Act
        bool result = type.IsFloatingPointType();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsFloatingPointType extension method when the type is null.
    /// Should throw ArgumentNullException.
    /// </summary>
    [Fact]
    public void IsFloatingPointType_NullType_ThrowsArgumentNullException()
    {
        // Arrange
        Type? type = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => type!.IsFloatingPointType());
    }

    #endregion

    #region IsIntegerType Tests

    /// <summary>
    /// Tests the IsIntegerType extension method for various integer and non-integer types.
    /// </summary>
    [Theory]
    [InlineData(typeof(sbyte), true)]
    [InlineData(typeof(sbyte?), true)]
    [InlineData(typeof(byte), true)]
    [InlineData(typeof(byte?), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(short?), true)]
    [InlineData(typeof(ushort), true)]
    [InlineData(typeof(ushort?), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(uint), true)]
    [InlineData(typeof(uint?), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(long?), true)]
    [InlineData(typeof(ulong), true)]
    [InlineData(typeof(ulong?), true)]
    [InlineData(typeof(float), false)]
    [InlineData(typeof(double), false)]
    [InlineData(typeof(decimal), false)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(bool), false)]
    [InlineData(typeof(bool?), false)]
    [InlineData(typeof(object), false)]
    public void IsIntegerType_Tests(Type type, bool expected)
    {
        // Act
        bool result = type.IsIntegerType();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsIntegerType extension method when the type is null.
    /// Should throw ArgumentNullException.
    /// </summary>
    [Fact]
    public void IsIntegerType_NullType_ThrowsArgumentNullException()
    {
        // Arrange
        Type? type = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => type!.IsIntegerType());
    }

    #endregion

    #region IsStringType Tests

    /// <summary>
    /// Tests the IsStringType extension method for string and non-string types.
    /// </summary>
    [Theory]
    [InlineData(typeof(string), true)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(bool), false)]
    [InlineData(typeof(bool?), false)]
    [InlineData(typeof(DateTime), false)]
    [InlineData(typeof(object), false)]
    public void IsStringType_Tests(Type type, bool expected)
    {
        // Act
        bool result = type.IsStringType();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IsTimeOnlyType Tests

    /// <summary>
    /// Tests the IsTimeOnlyType extension method for non-nullable and nullable TimeOnly types.
    /// </summary>
    [Theory]
    [InlineData(typeof(TimeOnly), true)]
    [InlineData(typeof(TimeOnly?), true)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(DateTime), false)]
    [InlineData(typeof(DateTime?), false)]
    [InlineData(typeof(object), false)]
    public void IsTimeOnlyType_Tests(Type type, bool expected)
    {
        // Act
        bool result = type.IsTimeOnlyType();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Get Underlying Type Tests

    [Fact]
    public void GetUnderlyingType_NullException() => 
        Assert.Throws<ArgumentNullException>(() => ((Type?)null)!.GetUnderlyingType());

    [Theory]
    [InlineData(typeof(int?), typeof(int))]
    [InlineData(typeof(double?), typeof(double))]
    [InlineData(typeof(DateTime?), typeof(DateTime))]
    [InlineData(typeof(int), typeof(int))]
    [InlineData(typeof(string), typeof(string))]
    [InlineData(typeof(decimal), typeof(decimal))]
    [InlineData(typeof(Guid), typeof(Guid))]
    [InlineData(typeof(ConsoleColor), typeof(int))]
    [InlineData(typeof(ByteEnum), typeof(byte))]
    [InlineData(typeof(LongEnum), typeof(long))]
    public void GetUnderlyingType(Type input, Type expected)
    {
        Type result = input.GetUnderlyingType();
        Assert.Equal(expected, result);
    }
    #endregion
}
