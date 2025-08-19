using Carrigan.Core.Base64;

namespace Carrigan.Core.Test.Base64Tests;

//IGNORE SPELLING: Foo Foob abc abcd
public class Base64DataTests
{
    [Fact]
    public void ImplicitConversionFromString_ValidBase64String_CreatesBase64Object()
    {
        // Arrange: Create a valid Base64 string from a byte array.
        string validBase64 = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 });

        // Act: Use the implicit conversion from string to Base64.
        Base64Data base64 = validBase64;

        // Assert: The Base64 object's string representation should equal the original string.
        Assert.Equal(validBase64, base64.ToString());
    }

    [Fact]
    public void ImplicitConversionToString_ReturnsValidBase64String()
    {
        // Arrange: Create a valid Base64 string.
        string validBase64 = Convert.ToBase64String(new byte[] { 10, 20, 30 });
        Base64Data base64 = new(validBase64);

        // Act: Use the implicit conversion from Base64 to string.
        string result = base64;

        // Assert: The resulting string should equal the original valid Base64 string.
        Assert.Equal(validBase64, result);
    }

    [Fact]
    public void ByteArrayConstructor_AndToByteArray_ReturnsOriginalArray()
    {
        // Arrange: Create a byte array.
        byte[] originalData = [1, 2, 3, 4, 5, 6, 7];

        // Act: Construct a Base64 object from the byte array, then convert it back.
        Base64Data base64 = new(originalData);
        byte[] resultData = base64.ToByteArray();

        // Assert: The byte array retrieved should match the original.
        Assert.Equal(originalData, resultData);
    }

    [Fact]
    public void Constructor_InvalidBase64String_ThrowsArgumentException()
    {
        // Arrange: An invalid Base64 string.
        string invalidBase64 = "This is not valid base64!!!";

        // Act & Assert: Creating a Base64 object with an invalid string should throw an ArgumentException.
        Assert.Throws<ArgumentException>(() =>
        {
            Base64Data base64 = new(invalidBase64);
        });
    }


    [Theory]
    [InlineData("Zm9v", true)]          // "foo"
    [InlineData("Zm9vYg==", true)]      // "foob"
    [InlineData("", true)]              // Empty string is valid Base64
    [InlineData("hello", false)]        // Not valid Base64; wrong length and invalid characters
    [InlineData("abc", false)]          // Length not a multiple of 4
    [InlineData("abcd", true)]          
    public void IsBase64String_ReturnsExpectedResult(string input, bool expected)
    {
        // Act
        bool result = Base64Data.IsBase64String(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsBase64String_NullInput_ThrowsNullReferenceException()
    {
        // Since the method doesn't check for null, a NullReferenceException is expected.
        Assert.Throws<NullReferenceException>(() => Base64Data.IsBase64String(null!));
    }
}
