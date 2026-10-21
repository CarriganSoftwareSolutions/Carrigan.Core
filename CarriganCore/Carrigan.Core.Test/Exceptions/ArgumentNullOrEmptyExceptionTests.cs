using Carrigan.Core.Exceptions;

namespace Carrigan.Core.Test.Exceptions;

public class ArgumentNullOrEmptyExceptionTests
{
    [Fact]
    public void ThrowIfNullOrEmpty_Null_ThrowsArgumentNullOrEmptyException()
    {
        string? value = null;

        ArgumentNullOrEmptyException exception =
            Assert.Throws<ArgumentNullOrEmptyException>(
                () => ArgumentNullOrEmptyException.ThrowIfNullOrEmpty(value));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Empty_ThrowsArgumentNullOrEmptyException()
    {
        string value = string.Empty;

        ArgumentNullOrEmptyException exception =
            Assert.Throws<ArgumentNullOrEmptyException>(
                () => ArgumentNullOrEmptyException.ThrowIfNullOrEmpty(value));

        Assert.Equal(nameof(value), exception.ParamName);
    }

    [Fact]
    public void ThrowIfNullOrEmpty_NonEmpty_DoesNotThrow()
    {
        string value = "Test";

        ArgumentNullOrEmptyException.ThrowIfNullOrEmpty(value);
    }

    [Fact]
    public void ThrowIfNullOrEmpty_Whitespace_DoesNotThrow()
    {
        string value = " ";

        ArgumentNullOrEmptyException.ThrowIfNullOrEmpty(value);
    }

    [Fact]
    public void ThrowIfNullOrEmpty_ExplicitParameterName_UsesParameterName()
    {
        string? value = null;

        ArgumentNullOrEmptyException exception =
            Assert.Throws<ArgumentNullOrEmptyException>(
                () => ArgumentNullOrEmptyException.ThrowIfNullOrEmpty(
                    value,
                    "customParameter"));

        Assert.Equal("customParameter", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithParameterName_SetsParameterName()
    {
        ArgumentNullOrEmptyException exception = new("value");

        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithParameterName_SetsMessage()
    {
        ArgumentNullOrEmptyException exception = new("value");

        Assert.Contains("Value cannot be null or empty.", exception.Message);
        Assert.Contains("value", exception.Message);
    }

    [Fact]
    public void DefaultConstructor_CanBeConstructed()
    {
        ArgumentNullOrEmptyException exception = new();

        Assert.NotNull(exception);
    }
}