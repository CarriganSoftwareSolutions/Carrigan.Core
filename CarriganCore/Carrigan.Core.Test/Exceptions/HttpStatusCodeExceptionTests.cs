using System.Net;
using Carrigan.Core.Exceptions;

namespace Carrigan.Core.Test.Exceptions;

public class HttpStatusCodeExceptionTests
{
    [Fact]
    public void Constructor_DefaultMessage_ContainsHttpStatusCode()
    {
        HttpStatusCodeException exception = new(HttpStatusCode.Forbidden);

        Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
        Assert.Equal("HTTP Status Code: 403", exception.Message);
        Assert.Null(exception.UserMessage);
        Assert.Null(exception.Action);
    }

    [Fact]
    public void Constructor_WithInnerException_PreservesAllData()
    {
        Exception inner = new("Inner");

        HttpStatusCodeException exception = new(HttpStatusCode.BadRequest, "Log message", inner, "User message", "Try again");

        Assert.Same(inner, exception.InnerException);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Equal("Log message", exception.Message);
        Assert.Equal("User message", exception.UserMessage);
        Assert.Equal("Try again", exception.Action);
    }

    [Fact]
    public void HttpNotFoundException_DefaultsToNotFound()
    {
        HttpNotFoundException exception = new();

        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.Equal("Resource not found.", exception.Message);
    }

    [Fact]
    public void HttpNotFoundException_InnerException_PreservesValues()
    {
        Exception inner = new("Inner");

        HttpNotFoundException exception = new("Missing record", inner, "Not found", "Choose another");

        Assert.Same(inner, exception.InnerException);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.Equal("Not found", exception.UserMessage);
        Assert.Equal("Choose another", exception.Action);
    }

    [Fact]
    public void ThrowIfNull_RejectsNullAndAcceptsValues()
    {
        string? missing = null;
        string present = "hello";

        Assert.Throws<HttpNotFoundException>(() => HttpNotFoundException.ThrowIfNull(missing));
        HttpNotFoundException.ThrowIfNull(present);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", false)]
    [InlineData("value", false)]
    public void ThrowIfNullOrEmpty_RejectsOnlyNullAndEmptyStrings(string? value, bool shouldThrow)
    {
        if (shouldThrow)
            Assert.Throws<HttpNotFoundException>(() => HttpNotFoundException.ThrowIfNullOrEmpty(value));
        else
            HttpNotFoundException.ThrowIfNullOrEmpty(value);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("   ", true)]
    [InlineData("value", false)]
    public void ThrowIfNullOrWhiteSpace_RejectsWhitespaceStrings(string? value, bool shouldThrow)
    {
        if (shouldThrow)
            Assert.Throws<HttpNotFoundException>(() => HttpNotFoundException.ThrowIfNullOrWhiteSpace(value));
        else
            HttpNotFoundException.ThrowIfNullOrWhiteSpace(value);
    }

    [Fact]
    public void ThrowIfNullOrEmpty_GuidRejectsEmptyAndNull()
    {
        Guid? missing = null;
        Guid? empty = Guid.Empty;
        Guid? present = Guid.NewGuid();

        Assert.Throws<HttpNotFoundException>(() => HttpNotFoundException.ThrowIfNullOrEmpty(missing));
        Assert.Throws<HttpNotFoundException>(() => HttpNotFoundException.ThrowIfNullOrEmpty(empty));
        HttpNotFoundException.ThrowIfNullOrEmpty(present);
    }
}
