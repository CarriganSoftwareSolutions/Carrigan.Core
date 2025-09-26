using Carrigan.Core.Extensions;
using Carrigan.Core.Test.Extensions.ObjectExtensionTestsSupport;

namespace Carrigan.Core.Test.Extensions;

public class IEmptyAndIWhiteSpaceExtensionTests
{    
    #region IsEmpty Tests

    /// <summary>
    /// Tests the IsEmpty extension method with various string inputs.
    /// </summary>
    /// 
    [Theory]
    [InlineData("", true)]
    [InlineData("   ", false)] // Whitespace characters are not considered empty
    [InlineData("Hello, World!", false)]
    [InlineData("Test", false)]
    public void IsEmpty_Tests(string input, bool expected)
    {
        WhiteSpaceTestClass testClass = new(input);
        // Act
        bool result = testClass.IsEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IsWhiteSpace Tests
    [Theory]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("   ", true)]
    [InlineData("\t", true)]
    [InlineData("\n", true)]
    [InlineData("Hello", false)]
    [InlineData(" Hello ", false)]
    public void IsWhiteSpace_ShouldReturnExpectedResult(string input, bool expected)
    {
        WhiteSpaceTestClass testClass = new(input);
        // Act
        bool result = testClass.IsWhiteSpace();

        // Assert
        Assert.Equal(expected, result);
    }
    #endregion

    #region IsNullOrEmpty Tests

    /// <summary>
    /// Tests the IsNullOrEmpty extension method with various string inputs.
    /// </summary>
    [Theory]
    [InlineData("", true)]
    [InlineData("   ", false)] // Whitespace characters are not considered empty
    [InlineData("Hello, World!", false)]
    [InlineData("Test", false)]
    public void IsNullOrEmpty_Tests(string input, bool expected)
    {
        WhiteSpaceTestClass testClass = new(input);
        // Act
        bool result = testClass.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNullOrEmpty when class is null.
    /// </summary>
    [Fact]
    public void IsNull_Tests()
    {
        WhiteSpaceTestClass? testClass = null;
        // Act
        bool result = testClass.IsNullOrEmpty();

        // Assert
        Assert.True(result);
    }



    #endregion

    #region IsNotNullNorEmpty Tests

    /// <summary>
    /// Tests the IsNotNullOrEmpty extension method with various string inputs.
    /// </summary>
    [Theory]
    [InlineData("", false)]
    [InlineData("   ", true)] // Whitespace characters are not considered empty
    [InlineData("Hello, World!", true)]
    [InlineData("Test", true)]
    public void IsNotNullOrEmpty_Tests(string input, bool expected)
    {
        WhiteSpaceTestClass testClass = new(input);
        // Act
        bool result = testClass.IsNotNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the IsNotNullOrEmpty when class is null.
    /// </summary>
    [Fact]
    public void IsNotNull_Test2()
    {
        WhiteSpaceTestClass? testClass = null;
        // Act
        bool result = testClass.IsNotNullOrEmpty();

        // Assert
        Assert.False(result);
    }


    #endregion

    #region IsNullOrWhiteSpace Tests
    [Theory]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("   ", true)]
    [InlineData("\t", true)]
    [InlineData("\n", true)]
    [InlineData("Hello", false)]
    [InlineData(" Hello ", false)]
    public void IsNullOrWhiteSpace_ShouldReturnExpectedResult(string input, bool expected)
    {
        WhiteSpaceTestClass testClass = new(input);
        // Act
        bool result = testClass.IsNullOrWhiteSpace();

        // Assert
        Assert.Equal(expected, result);
    }
    [Fact]
    public void IsNull_Test2()
    {
        WhiteSpaceTestClass? testClass = null;
        // Act
        bool result = testClass.IsNullOrWhiteSpace();

        // Assert
        Assert.True(result);
    }
    #endregion

    #region IsNotNullOrWhiteSpace Tests
    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("   ", false)]
    [InlineData("\t", false)]
    [InlineData("\n", false)]
    [InlineData("Hello", true)]
    [InlineData(" Hello ", true)]
    public void IsNotNullNorWhiteSpace_ShouldReturnExpectedResult(string input, bool expected)
    {
        WhiteSpaceTestClass testClass = new(input);

        // Act
        bool result = testClass.IsNotNullOrWhiteSpace();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsNotNull_Tests()
    {
        WhiteSpaceTestClass? testClass = null;

        // Act
        bool result = testClass.IsNotNullOrWhiteSpace();

        // Assert
        Assert.False(result);
    }
    #endregion
}
