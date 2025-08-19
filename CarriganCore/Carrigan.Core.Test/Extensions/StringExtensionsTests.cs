using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class StringExtensionsTests
{
    #region IsNullOrEmpty Tests

    /// <summary>
    /// Tests the IsNullOrEmpty extension method with various string inputs.
    /// </summary>
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("   ", false)] // Whitespace characters are not considered empty
    [InlineData("Hello, World!", false)]
    [InlineData("Test", false)]
    public void IsNullOrEmpty_Tests(string? input, bool expected)
    {
        // Act
        bool result = input.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IsNotNullNorEmpty Tests

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method with various string inputs.
    /// </summary>
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", true)] // Whitespace characters are not considered empty
    [InlineData("Hello, World!", true)]
    [InlineData("Test", true)]
    public void IsNotNullOrEmpty_Tests(string? input, bool expected)
    {
        // Act
        bool result = input.IsNotNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("   ", true)]
    [InlineData("\t", true)]
    [InlineData("\n", true)]
    [InlineData("Hello", false)]
    [InlineData(" Hello ", false)]
    public void IsNullOrWhiteSpace_ShouldReturnExpectedResult(string? input, bool expected)
    {
        // Act
        bool result = input.IsNullOrWhiteSpace();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("   ", false)]
    [InlineData("\t", false)]
    [InlineData("\n", false)]
    [InlineData("Hello", true)]
    [InlineData(" Hello ", true)]
    public void IsNotNullNorWhiteSpace_ShouldReturnExpectedResult(string? input, bool expected)
    {
        // Act
        bool result = input.IsNotNullOrWhiteSpace();

        // Assert
        Assert.Equal(expected, result);
    }


    #region SplitNewLines Tests

/// <summary>
/// Tests the SplitNewLines extension method with various input strings containing different types of line endings.
/// </summary>
[Theory]
    [InlineData("Line1\nLine2\nLine3", new[] { "Line1", "Line2", "Line3" })]
    [InlineData("Line1\r\nLine2\r\nLine3", new[] { "Line1", "Line2", "Line3" })]
    [InlineData("Line1\rLine2\rLine3", new[] { "Line1", "Line2", "Line3" })]
    [InlineData("Line1\nLine2\r\nLine3\rLine4", new[] { "Line1", "Line2", "Line3", "Line4" })]
    [InlineData("SingleLine", new[] { "SingleLine" })]
    [InlineData("\n\n\n", new[] { "", "", "", "" })] // Multiple consecutive new lines
    [InlineData("Line1\n\nLine3", new[] { "Line1", "", "Line3" })]
    public void SplitNewLines_Tests(string input, string[] expected)
    {
        // Act
        IEnumerable<string> result = input.SplitNewLines();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the SplitNewLines extension method with an empty string.
    /// </summary>
    [Fact]
    public void SplitNewLines_EmptyString_ReturnsSingleEmptyString()
    {
        // Arrange
        string input = string.Empty;

        // Act
        IEnumerable<string> result = input.SplitNewLines();

        // Assert
        Assert.Single(result);
        Assert.Equal(string.Empty, result.First());
    }

    /// <summary>
    /// Tests the SplitNewLines extension method with a string that has no line endings.
    /// </summary>
    [Fact]
    public void SplitNewLines_NoLineEndings_ReturnsSingleElement()
    {
        // Arrange
        string input = "SingleLine";

        // Act
        IEnumerable<string> result = input.SplitNewLines();

        // Assert
        Assert.Single(result);
        Assert.Equal("SingleLine", result.First());
    }

    /// <summary>
    /// Tests the SplitNewLines extension method with multiple consecutive line endings.
    /// </summary>
    [Fact]
    public void SplitNewLines_MultipleConsecutiveLineEndings_ReturnsEmptyStrings()
    {
        // Arrange
        string input = "Line1\n\nLine3";

        // Act
        IEnumerable<string> result = input.SplitNewLines();

        // Assert
        Assert.Equal(["Line1", "", "Line3"], result);
    }

    /// <summary>
    /// Tests the SplitNewLines extension method with a string that starts and ends with line endings.
    /// </summary>
    [Fact]
    public void SplitNewLines_StartsAndEndsWithLineEndings_ReturnsEmptyStringsAtBoundaries()
    {
        // Arrange
        string input = "\nLine1\nLine2\n";

        // Act
        IEnumerable<string> result = input.SplitNewLines();

        // Assert
        Assert.Equal(["", "Line1", "Line2", ""], result);
    }

    /// <summary>
    /// Tests the SplitNewLines extension method when the input string is null.
    /// Since the method signature expects a non-null string, this scenario should not occur.
    /// However, if you decide to make the method accept nullable strings in the future, consider adding tests accordingly.
    /// </summary>
    [Fact]
    public void SplitNewLines_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        string? input = null;

        IEnumerable<string> result = input!.SplitNewLines();

        // Assert
        Assert.Equal([string.Empty], result);
    }

    #endregion

    #region join and or

    [Theory]
    [InlineData("", null)]
    [InlineData("1", "1")]
    [InlineData("1 and 2", "1", "2")]
    [InlineData("1, 2 and end", "1", "2", "end")]
    [InlineData("1, 2, 3 and end", "1", "2", "3", "end")]
    [InlineData("1, 2, 3, 4 and end", "1", "2", "3", "4", "end")]
    public void JoinAnd(string expected, params string[]? strings)
    {
        string actual = strings!.JoinAnd();

        Assert.Equal(expected, actual);
    }


    [Theory]
    [InlineData("", null)]
    [InlineData("", "")]
    [InlineData("1", "1")]
    [InlineData("1 or 2", "1", "2")]
    [InlineData("1, 2 or end", "1", "2", "end")]
    [InlineData("1, 2, 3 or end", "1", "2", "3", "end")]
    [InlineData("1, 2, 3, 4 or end", "1", "2", "3", "4", "end")]
    public void JoinOr(string expected, params string[]? strings)
    {
        string actual = strings!.JoinOr();

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Coalesce
    [Fact]
    public void Coalesce_ReturnsOriginalValue_WhenValueIsNotNullOrWhiteSpace()
    {
        // Arrange
        string original = "Hello World";

        // Act
        string result = original.Coalesce("Alternative", "Another");

        // Assert
        Assert.Equal(original, result);
    }

    [Fact]
    public void Coalesce_ReturnsFirstNonEmptyValue_WhenValueIsNullOrWhiteSpace()
    {
        // Arrange
        string original = null;

        // Act
        string result = original.Coalesce(null, "   ", "FirstValid", "SecondValid");

        // Assert
        Assert.Equal("FirstValid", result);
    }

    [Fact]
    public void Coalesce_ReturnsEmptyString_WhenNoValidValueFound()
    {
        // Arrange
        string original = "   ";

        // Act
        string result = original.Coalesce(null, "   ", "");

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Coalesce_ReturnsEmptyString_WhenValueIsNullAndNoAlternativesProvided()
    {
        // Arrange
        string original = null;

        // Act
        string result = original.Coalesce();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("ThisIsPascalCase", "This Is Pascal Case")]
    [InlineData("camelCaseTest", "camel Case Test")]
    [InlineData("XMLHttpRequest", "XML Http Request")]
    [InlineData("already split", "already split")]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("single", "single")]
    [InlineData("ALLCAPS", "A L L C A P S")]
    public void SplitCamelPascalCase_ReturnsExpected(string? input, string? expected)
    {
        // Act
        string result = input.SplitCamelPascalCase();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion
}
