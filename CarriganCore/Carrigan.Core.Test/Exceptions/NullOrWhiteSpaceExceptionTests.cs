using Carrigan.Core.Exceptions;

namespace Carrigan.Core.Test.Exceptions;

public class NullOrWhiteSpaceExceptionTests
{
    [Fact]
    public void Constructor_Default_SetsMessage()
    {
        NullOrWhiteSpaceException exception = new();

        Assert.Equal("Value cannot be null, empty, or white space.", exception.Message);
    }

    [Fact]
    public void Constructor_WithExpression_SetsMessage()
    {
        NullOrWhiteSpaceException exception = new("value");

        Assert.Equal("Value 'value' cannot be null, empty, or white space.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\r")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    [InlineData(" \t\r\n ")]
    public void ThrowIfNullOrWhiteSpace_InvalidValue_ThrowsNullOrWhiteSpaceException(string? value)
    {
        NullOrWhiteSpaceException exception = Assert.Throws<NullOrWhiteSpaceException>(() => NullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value));

        Assert.Contains(nameof(value), exception.Message);
    }

    [Theory]
    [InlineData("Test")]
    [InlineData(" Test ")]
    [InlineData("0")]
    public void ThrowIfNullOrWhiteSpace_ValidValue_ReturnsValue(string value)
    {
        string result = NullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value);

        Assert.Same(value, result);
    }

    [Fact]
    public void ThrowIfNullOrWhiteSpace_ExplicitExpression_UsesExpression()
    {
        string? value = null;

        NullOrWhiteSpaceException exception = Assert.Throws<NullOrWhiteSpaceException>(() => NullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(value, "customExpression"));

        Assert.Equal("Value 'customExpression' cannot be null, empty, or white space.", exception.Message);
    }
}
