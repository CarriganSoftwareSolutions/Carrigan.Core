using Carrigan.Core.Exceptions;

namespace Carrigan.Core.Test.Exceptions;

public class NullOrEmptyExceptionTests
{
    [Fact]
    public void Constructor_Default_SetsMessage()
    {
        NullOrEmptyException exception = new();

        Assert.Equal("Value cannot be null or empty.", exception.Message);
    }

    [Fact]
    public void Constructor_WithExpression_SetsMessage()
    {
        NullOrEmptyException exception = new("value");

        Assert.Equal("Value 'value' cannot be null or empty.", exception.Message);
    }

    [Fact]
    public void Constructor_WithDescription_IncludesExpressionAndReason()
    {
        NullOrEmptyException exception = new("items", "contains disallowed null or empty elements.");

        Assert.Equal("'items' contains disallowed null or empty elements.", exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ThrowIfNullOrEmpty_InvalidValue_ThrowsNullOrEmptyException(string? value)
    {
        NullOrEmptyException exception = Assert.Throws<NullOrEmptyException>(() => NullOrEmptyException.ThrowIfNullOrEmpty(value));

        Assert.Contains(nameof(value), exception.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData(" Test ")]
    [InlineData("Test")]
    public void ThrowIfNullOrEmpty_ValidValue_ReturnsValue(string value)
    {
        string result = NullOrEmptyException.ThrowIfNullOrEmpty(value);

        Assert.Same(value, result);
    }

    [Fact]
    public void ThrowIfNullOrEmpty_ExplicitExpression_UsesExpression()
    {
        string? value = null;

        NullOrEmptyException exception = Assert.Throws<NullOrEmptyException>(() => NullOrEmptyException.ThrowIfNullOrEmpty(value, "customExpression"));

        Assert.Equal("Value 'customExpression' cannot be null or empty.", exception.Message);
    }
}
