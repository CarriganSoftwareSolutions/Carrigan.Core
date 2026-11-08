using Carrigan.Core.DataTypes;

namespace Carrigan.Core.Test.DataTypeTests;

//IGNORE SPELLING: Foo Foob abc abcd aabbccddeeff
public class Base64DataTests
{
    [Fact]
    public void ImplicitConversionFromString_ValidBase64String_CreatesBase64Object()
    {
        string validBase64 = Convert.ToBase64String([1, 2, 3, 4, 5]);

        Base64Data base64 = validBase64;

        Assert.Equal(validBase64, base64.ToString());
    }

    [Fact]
    public void ImplicitConversionToString_ReturnsValidBase64String()
    {
        string validBase64 = Convert.ToBase64String([10, 20, 30]);
        Base64Data base64 = new(validBase64);

        string result = base64;

        Assert.Equal(validBase64, result);
    }

    [Fact]
    public void ByteArrayConstructor_AndToByteArray_ReturnsOriginalArray()
    {
        byte[] originalData = [1, 2, 3, 4, 5, 6, 7];

        Base64Data base64 = new(originalData);
        byte[] resultData = base64.ToByteArray();

        Assert.Equal(originalData, resultData);
    }

    [Fact]
    public void ToByteArray_ReturnsIndependentArray()
    {
        byte[] originalData = [1, 2, 3];
        Base64Data base64 = new(originalData);

        byte[] result = base64.ToByteArray();
        result[0] = 99;
        originalData[1] = 99;

        Assert.Equal(new byte[] { 1, 2, 3 }, base64.ToByteArray());
    }

    [Fact]
    public void StringConstructor_Whitespace_NormalizesBase64()
    {
        Base64Data base64 = new(" T Q== \r\n");

        Assert.Equal("TQ==", base64.ToString());
        Assert.Equal("M"u8.ToArray(), base64.ToByteArray());
    }

    [Fact]
    public void Constructor_InvalidBase64String_ThrowsArgumentException()
    {
        string invalidBase64 = "This is not valid base64!!!";

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Base64Data(invalidBase64));

        Assert.Equal("base64", exception.ParamName);
    }

    [Fact]
    public void StringConstructor_Null_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => new Base64Data((string)null!));

    [Fact]
    public void ByteArrayConstructor_Null_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => new Base64Data((byte[])null!));

    [Theory]
    [InlineData("Zm9v", true)]
    [InlineData("Zm9vYg==", true)]
    [InlineData("", true)]
    [InlineData("hello", false)]
    [InlineData("abc", false)]
    [InlineData("abcd", true)]
    [InlineData(" T Q== \n", true)]
    [InlineData("A?==", false)]
    [InlineData(null, false)]
    public void IsBase64String_ReturnsExpectedResult(string? input, bool expected)
    {
        bool result = Base64Data.IsBase64String(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Equals_SameDataWithDifferentFormatting_ReturnsTrue()
    {
        Base64Data first = new("TQ==");
        Base64Data second = new(" T Q== \n");

        Assert.True(first.Equals(second));
        Assert.Equal(first, second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void Equals_DifferentData_ReturnsFalse()
    {
        Base64Data first = new([1, 2, 3]);
        Base64Data second = new([1, 2, 4]);

        Assert.False(first.Equals(second));
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        Base64Data base64 = new([1, 2, 3]);

        Assert.False(base64.Equals((Base64Data?)null));
        Assert.False(base64.Equals((object?)null));
    }

    [Fact]
    public void Equals_DerivedTypeWithSameBytes_ReturnsFalse()
    {
        Guid guid = Guid.Parse("00112233-4455-6677-8899-aabbccddeeff");
        Base64Data data = new(guid.ToByteArray());
        Base64Guid encodedGuid = new(guid);

        Assert.False(data.Equals(encodedGuid));
        Assert.False(encodedGuid.Equals(data));
    }
}
