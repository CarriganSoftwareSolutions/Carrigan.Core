using Carrigan.Core.Extensions;
using Carrigan.Core.Test.Extensions.PropertyInfoExtensionsTestsSupport;
using System.Reflection;

namespace Carrigan.Core.Test.Extensions;

public class PropertyInfoExtensionsTests
{
    #region GetDisplayName Tests




    /// <summary>
    /// Tests the GetDisplayName method for properties with a valid DisplayAttribute.
    /// </summary>
    [Theory]
    [InlineData("Id", "Identifier")]
    [InlineData("Name", "Full Name")]
    public void GetDisplayName_PropertyWithDisplayAttribute_ReturnsDisplayName(string propertyName, string expectedDisplayName)
    {
        // Arrange
        PropertyInfo property = typeof(SampleClass).GetProperty(propertyName);
        Assert.NotNull(property);

        // Act
        string displayName = property.GetDisplayName();

        // Assert
        Assert.Equal(expectedDisplayName, displayName);
    }

    /// <summary>
    /// Tests the GetDisplayName method for properties without a DisplayAttribute.
    /// </summary>
    [Theory]
    [InlineData("Description")]
    public void GetDisplayName_PropertyWithoutDisplayAttribute_ReturnsPropertyName(string propertyName)
    {
        // Arrange
        PropertyInfo property = typeof(SampleClass).GetProperty(propertyName);
        Assert.NotNull(property);

        // Act
        string displayName = property.GetDisplayName();

        // Assert
        Assert.Equal(propertyName, displayName);
    }

    /// <summary>
    /// Tests the GetDisplayName method for properties with an empty DisplayAttribute Name.
    /// Should fallback to property name.
    /// </summary>
    [Theory]
    [InlineData("EmptyDisplayName")]
    public void GetDisplayName_PropertyWithEmptyDisplayName_ReturnsPropertyName(string propertyName)
    {
        // Arrange
        PropertyInfo property = typeof(SampleClass).GetProperty(propertyName);
        Assert.NotNull(property);

        // Act
        string displayName = property.GetDisplayName();

        // Assert
        Assert.Equal(propertyName, displayName);
    }

    /// <summary>
    /// Tests the GetDisplayName method for properties with a null DisplayAttribute Name.
    /// Should fallback to property name.
    /// </summary>
    [Theory]
    [InlineData("NullDisplayName")]
    public void GetDisplayName_PropertyWithNullDisplayName_ReturnsPropertyName(string propertyName)
    {
        // Arrange
        PropertyInfo property = typeof(SampleClass).GetProperty(propertyName);
        Assert.NotNull(property);

        // Act
        string displayName = property.GetDisplayName();

        // Assert
        Assert.Equal(propertyName, displayName);
    }

    #endregion
}
