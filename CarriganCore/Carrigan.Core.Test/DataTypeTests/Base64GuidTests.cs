using Carrigan.Core.DataTypes;

namespace Carrigan.Core.Test.DataTypeTests;
//Ignore Spelling: aabbccddeeff 
public class Base64GuidTests
{
    [Fact]
    public void DefaultConstructor_CreatesVersion7Guid()
    {
        Base64Guid base64 = new();

        Guid guid = base64;

        Assert.Equal('7', guid.ToString("D")[14]);
        Assert.Equal(Convert.ToBase64String(guid.ToByteArray()), base64.ToString());
    }

    [Theory]
    [InlineData(true, '7')]
    [InlineData(false, '4')]
    public void BooleanConstructor_CreatesExpectedGuidVersion(bool useGuid7, char expectedVersion)
    {
        Base64Guid base64 = new(useGuid7);

        Guid guid = base64;

        Assert.Equal(expectedVersion, guid.ToString("D")[14]);
    }

    [Fact]
    public void GuidConstructor_EncodesOriginalGuidBytes()
    {
        Guid original = Guid.Parse("00112233-4455-6677-8899-aabbccddeeff");

        Base64Guid base64 = new(original);

        Assert.Equal(Convert.ToBase64String(original.ToByteArray()), base64.ToString());
        Assert.Equal(original.ToByteArray(), base64.ToByteArray());
    }

    [Fact]
    public void StringConstructor_ValidBase64_RoundTripsGuid()
    {
        Guid original = Guid.Parse("00112233-4455-6677-8899-aabbccddeeff");
        string encoded = Convert.ToBase64String(original.ToByteArray());

        Base64Guid restored = new(encoded);
        Guid result = restored;

        Assert.Equal(original, result);
        Assert.Equal(encoded, restored.ToString());
    }

    [Fact]
    public void StringConstructor_Base64WithWhitespace_NormalizesRepresentation()
    {
        Guid original = Guid.NewGuid();
        string encoded = Convert.ToBase64String(original.ToByteArray());

        Base64Guid restored = new($"  {encoded[..8]} \n {encoded[8..]}  ");

        Assert.Equal(encoded, restored.ToString());
        Assert.Equal(original, (Guid)restored);
    }

    [Fact]
    public void ImplicitConversionFromString_ValidBase64_CreatesBase64Guid()
    {
        Guid original = Guid.NewGuid();
        string encoded = Convert.ToBase64String(original.ToByteArray());

        Base64Guid base64 = encoded;

        Assert.Equal(original, (Guid)base64);
    }

    [Fact]
    public void ImplicitConversionToString_UsesBaseClassOperator()
    {
        Guid original = Guid.NewGuid();
        Base64Guid base64 = new(original);

        string result = base64;

        Assert.Equal(Convert.ToBase64String(original.ToByteArray()), result);
    }

    [Fact]
    public void ImplicitConversionToGuid_ReturnsOriginalGuid()
    {
        Guid original = Guid.NewGuid();
        Base64Guid base64 = new(original);

        Guid result = base64;

        Assert.Equal(original, result);
    }

    [Fact]
    public void FromGuidString_ValidGuid_CreatesBase64Guid()
    {
        Guid original = Guid.Parse("00112233-4455-6677-8899-aabbccddeeff");

        Base64Guid base64 = Base64Guid.FromGuidString(original.ToString("D"));

        Assert.Equal(original, (Guid)base64);
        Assert.Equal(Convert.ToBase64String(original.ToByteArray()), base64.ToString());
    }

    [Fact]
    public void FromGuidString_InvalidGuid_ThrowsFormatException() =>
        Assert.Throws<FormatException>(() => Base64Guid.FromGuidString("not-a-guid"));

    [Fact]
    public void FromGuidString_Null_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => Base64Guid.FromGuidString(null!));

    [Fact]
    public void StringConstructor_InvalidBase64_ThrowsArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Base64Guid("invalid!?"));

        Assert.Equal("base64", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(17)]
    [InlineData(32)]
    public void StringConstructor_IncorrectByteCount_ThrowsArgumentException(int byteCount)
    {
        string encoded = Convert.ToBase64String(new byte[byteCount]);

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Base64Guid(encoded));

        Assert.Equal("base64", exception.ParamName);
    }

    [Fact]
    public void StringConstructor_Null_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => new Base64Guid((string)null!));

    [Fact]
    public void Equals_SameGuid_ReturnsTrue()
    {
        Guid guid = Guid.NewGuid();
        Base64Guid first = new(guid);
        Base64Guid second = new(first.ToString());

        Assert.True(first.Equals(second));
        Assert.Equal(first, second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void Equals_SameGuidAsBase64Data_ReturnsTrue()
    {
        Guid guid = Guid.NewGuid();
        Base64Guid first = new(guid);
        Base64Data second = new Base64Guid(first.ToString());

        Assert.True(first.Equals(second));
        Assert.True(second.Equals(first));
    }

    [Fact]
    public void Equals_DifferentGuids_ReturnsFalse()
    {
        Base64Guid first = new(Guid.Empty);
        Base64Guid second = new(Guid.Parse("00112233-4455-6677-8899-aabbccddeeff"));

        Assert.False(first.Equals(second));
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        Base64Guid base64 = new(Guid.Empty);

        Assert.False(base64.Equals((Base64Guid?)null));
        Assert.False(base64.Equals((Base64Data?)null));
        Assert.False(base64.Equals((object?)null));
    }
}
