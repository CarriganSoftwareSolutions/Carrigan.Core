using Carrigan.Core.Exceptions;

namespace Carrigan.Core.Test.Exceptions;

public class ArgumentNullOrWhiteSpaceExceptionTests
{
    [Fact]
    public void ThrowIfNullOrWhiteSpace_Null_ThrowsArgumentNullOrWhiteSpaceException()
    {
        string? value = null;

        ArgumentNullOrWhiteSpaceException exception =
            Assert.Throws<ArgumentNullOrWhiteSpaceException>(
                () => ArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_Empty_ThrowsArgumentNullOrWhiteSpaceException()
    {
        string value = string.Empty;

        ArgumentNullOrWhiteSpaceException exception =
            Assert.Throws<ArgumentNullOrWhiteSpaceException>(
                () => ArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\r")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    [InlineData(" \t\r\n ")]
    public void ThrowIfNullOrWhiteSpace_WhiteSpace_ThrowsArgumentNullOrWhiteSpaceException(
        string value)
    {
        ArgumentNullOrWhiteSpaceException exception =
            Assert.Throws<ArgumentNullOrWhiteSpaceException>(
                () => ArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Theory]
    [InlineData("Test")]
    [InlineData(" Test ")]
    [InlineData("a")]
    [InlineData("0")]
    public void ThrowIfNullOrWhiteSpace_NonWhiteSpace_DoesNotThrow(string value) =>
        ArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value);

    [Fact]
    public void ThrowIfNullOrWhiteSpace_ExplicitParameterName_UsesParameterName()
    {
        string? value = null;

        ArgumentNullOrWhiteSpaceException exception =
            Assert.Throws<ArgumentNullOrWhiteSpaceException>(
                () => ArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(
                    value,
                    "customParameter"));

        Assert.Equal("customParameter", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithParameterName_SetsParameterName()
    {
        ArgumentNullOrWhiteSpaceException exception = new("value");

        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithParameterName_SetsMessage()
    {
        ArgumentNullOrWhiteSpaceException exception = new("value");

        Assert.Contains(
            "Value cannot be null, empty, or white space.",
            exception.Message);

        Assert.Contains("value", exception.Message);
    }

    [Fact]
    public void DefaultConstructor_CanBeConstructed()
    {
        ArgumentNullOrWhiteSpaceException exception = new();

        Assert.NotNull(exception);
    }
}