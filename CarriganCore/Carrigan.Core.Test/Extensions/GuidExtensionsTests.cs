using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class GuidExtensionsTests
{
    #region IsNullOrEmpty Tests

    /// <summary>
    /// Tests the IsNullOrEmpty extension method for non-nullable Guid values.
    /// </summary>
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", true)] // Guid.Empty
    [InlineData("d3d94468-2f7b-4a6a-8f1b-1b2a9d5e3b4c", false)] // Non-empty Guid
    [InlineData("11111111-1111-1111-1111-111111111111", false)] // Another non-empty Guid
    public void IsNullOrEmpty_NonNullableGuid_ReturnsExpected(string guidString, bool expected)
    {
        // Arrange
        Guid guid = Guid.Parse(guidString);

        // Act
        bool result = guid.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNullOrEmpty extension method for nullable Guid values with a value.
    /// </summary>
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", true)] // Guid.Empty
    [InlineData("d3d94468-2f7b-4a6a-8f1b-1b2a9d5e3b4c", false)] // Non-empty Guid
    [InlineData("11111111-1111-1111-1111-111111111111", false)] // Another non-empty Guid
    public void IsNullOrEmpty_NullableGuid_WithValue_ReturnsExpected(string guidString, bool expected)
    {
        // Arrange
        Guid? guid = Guid.Parse(guidString);

        // Act
        bool result = guid.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNullOrEmpty extension method for nullable Guid values with null.
    /// </summary>
    [Fact]
    public void IsNullOrEmpty_NullableGuid_WithNull_ReturnsTrue()
    {
        // Arrange
        Guid? guid = null;

        // Act
        bool result = guid.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }

    #endregion

    #region IsNotNullOrEmpty Tests

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method for non-nullable Guid values.
    /// </summary>
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", false)] // Guid.Empty
    [InlineData("d3d94468-2f7b-4a6a-8f1b-1b2a9d5e3b4c", true)] // Non-empty Guid
    [InlineData("11111111-1111-1111-1111-111111111111", true)] // Another non-empty Guid
    public void IsNotNullOrEmpty_NonNullableGuid_ReturnsExpected(string guidString, bool expected)
    {
        // Arrange
        Guid guid = Guid.Parse(guidString);

        // Act
        bool result = guid.IsNotNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method for nullable Guid values with a value.
    /// </summary>
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", false)] // Guid.Empty
    [InlineData("d3d94468-2f7b-4a6a-8f1b-1b2a9d5e3b4c", true)] // Non-empty Guid
    [InlineData("11111111-1111-1111-1111-111111111111", true)] // Another non-empty Guid
    public void IsNotNullOrEmpty_NullableGuid_WithValue_ReturnsExpected(string guidString, bool expected)
    {
        // Arrange
        Guid? guid = Guid.Parse(guidString);

        // Act
        bool result = guid.IsNotNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method for nullable Guid values with null.
    /// </summary>
    [Fact]
    public void IsNotNullOrEmpty_NullableGuid_WithNull_ReturnsFalse()
    {
        // Arrange
        Guid? guid = null;

        // Act
        bool result = guid.IsNotNullOrEmpty();

        // Assert
        Assert.False(result);
    }

    #endregion
}
