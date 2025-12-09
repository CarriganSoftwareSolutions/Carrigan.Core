using Carrigan.Core.Extensions;
using Carrigan.Core.Test.Extensions.ObjectExtensionTestsSupport;

namespace Carrigan.Core.Test.Extensions;

public class ObjectExtensionsTests
{
    private enum ByteEnum : byte
    {
        A = 1
    }

    #region HasValue Tests

    /// <summary>
    /// Tests the HasValue extension method with a null object.
    /// </summary>
    [Fact]
    public void HasValue_NullObject_ReturnsFalse()
    {
        // Arrange
        object? obj = null;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Tests the HasValue extension method with various string inputs.
    /// </summary>
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("Hello, World!", true)]
    [InlineData("Test", true)]
    public void HasValue_StringTests(string? input, bool expected)
    {
        // Arrange
        object? obj = input;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData(null, false)]
    [InlineData(new int[] { }, false)]
    [InlineData(new[] { 1, 2, 3 }, true)]
    public void HasValue_IEnumerableTests<T>(T[]? input, bool expected)
    {
        // Arrange
        object? obj = input;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the HasValue extension method with various nullable value types.
    /// </summary>
    [Theory]
    [InlineData(null, false)]
    [InlineData(123, true)]
    [InlineData(0, true)] // Even zero is a valid value
    public void HasValue_NullableValueTypesTests(int? input, bool expected)
    {
        // Arrange
        object? obj = input;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the HasValue extension method with non-nullable value types.
    /// </summary>
    [Theory]
    [InlineData(123, true)]
    [InlineData(0, true)]
    [InlineData(-456, true)]
    public void HasValue_NonNullableValueTypesTests(int input, bool expected)
    {
        // Arrange
        object obj = input;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the HasValue extension method with custom reference types.
    /// </summary>
    [Theory]
    [InlineData(null, false)]
    [InlineData(typeof(TestClass), true)]
    public void HasValue_ReferenceTypeTests(Type? type, bool expected)
    {
        // Arrange
        object? obj = type != null ? Activator.CreateInstance(type) : null;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the HasValue extension method with nullable types that contain strings or IEnumerables.
    /// </summary>
    [Theory]
    [InlineData(null, false)]
    [InlineData("Hello", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData(new int[] { 1, 2 }, true)]
    [InlineData(new int[] { }, false)]
    public void HasValue_NullableTypesWithInnerValuesTests(object? input, bool expected)
    {
        // Arrange
        object? obj = input;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the HasValue extension method with nested nullable types.
    /// </summary>
    [Fact]
    public void HasValue_NestedNullableTypes_ReturnsExpected()
    {
        // Arrange
        string? innerString = "Nested";
        int? innerNullableInt = 10;
        object obj = new Tuple<string?, int?>(innerString, innerNullableInt);

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the HasValue extension method with nested collections.
    /// </summary>
    [Fact]
    public void HasValue_NestedCollections_ReturnsExpected()
    {
        // Arrange
        List<List<int>> nestedList =
        [
            [1, 2],
            [3]
        ];
        object obj = nestedList;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the HasValue extension method with collections containing null elements.
    /// </summary>
    //When I originally wrote the method being test I had intended on it just having a value and value.
    //I did not consider that the value might be null.
    //After consideration, I decided that list with a null value is a meaningful value?
    //However, this is a coin toss. Now that it is in use, changing it is risky.
    //Given that changing it is risky, it is now the law of the land.
    //Next time maybe I will say what I meant and not mean what I said.
    [Theory]
    [InlineData(new string[] { null!, null! }, true)]
    [InlineData(new string[] { null!, "apple" }, true)]
    [InlineData(new object[] { null!, null! }, true)]
    public void HasValue_CollectionsWithNullElements_ReturnsExpected(object[] input, bool expected)
    {
        // Arrange
        object obj = input;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the HasValue extension method with DateTime object.
    /// </summary>
    [Fact]
    public void HasValue_DateTime_ReturnsTrue()
    {
        // Arrange
        object obj = DateTime.Now;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.True(result);
    }

    /// <summary>
    /// Tests the HasValue extension method with custom nullable types.
    /// </summary>
    [Fact]
    public void HasValue_CustomNullableType_ReturnsExpected()
    {
        // Arrange
        CustomNullableType? customNullable = new CustomNullableType { Value = "Test" };
        object? obj = customNullable;

        // Act
        bool result = obj.HasValue();

        // Assert
        Assert.True(result);

        // Arrange with null value
        customNullable = null;
        obj = customNullable;

        // Act
        result = obj.HasValue();

        // Assert
        Assert.False(result);
    }

    #endregion

    #region Get Underlying Type tests

    [Fact]
    public void GetUnderlyingType_WhenInstanceIsNull()
    {
        object? value = null;

        Type? result = value.GetUnderlyingType();

        Assert.Null(result);
    }

    [Fact]
    public void GetUnderlyingType_ForReferenceType()
    {
        string value = "hello";

        Type? result = value.GetUnderlyingType();

        Assert.Equal(typeof(string), result);
    }

    [Fact]
    public void GetUnderlyingType_ForValueType()
    {
        int value = 123;

        Type? result = value.GetUnderlyingType();

        Assert.Equal(typeof(int), result);
    }

    [Fact]
    public void GetUnderlyingType_ForNullableInt()
    {
        int? value = 42;

        Type? result = value.GetUnderlyingType();

        Assert.Equal(typeof(int), result);
    }

    [Fact]
    public void GetUnderlyingType_ForNullableDateTime()
    {
        DateTime? value = DateTime.UtcNow;

        Type? result = value.GetUnderlyingType();

        Assert.Equal(typeof(DateTime), result);
    }

    [Fact]
    public void GetUnderlyingType_ForEnum()
    {
        ConsoleColor value = ConsoleColor.Red;

        Type? result = value.GetUnderlyingType();

        Assert.Equal(typeof(int), result);
    }

    [Fact]
    public void GetUnderlyingType_ForNullableEnum()
    {
        ByteEnum? value = ByteEnum.A;

        Type? result = value.GetUnderlyingType();

        Assert.Equal(typeof(byte), result);
    }
    #endregion
}
