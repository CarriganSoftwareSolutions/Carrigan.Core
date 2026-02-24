using Carrigan.Core.DataTypes;

namespace Carrigan.Core.Test.DataTypeTests;

public sealed class UsaPhoneNumberTests
{
    [Theory]
    [InlineData("6155551212", "6155551212")]
    [InlineData("615-555-1212", "6155551212")]
    [InlineData("(615)555-1212", "6155551212")]
    [InlineData("(615) 555-1212", "6155551212")]
    [InlineData("615.555.1212", "6155551212")]
    [InlineData("+1 (615) 555-1212", "6155551212")]
    [InlineData("1-615-555-1212", "6155551212")]
    [InlineData("16155551212", "6155551212")]
    public void Constructor_Normalizes_ToTenDigits(string input, string expectedDigits)
    {
        UsaPhoneNumber phoneNumber = new(input);

        Assert.Equal(expectedDigits, phoneNumber.Digits);
        Assert.Equal(expectedDigits, phoneNumber.ToString());
    }

    [Fact]
    public void Constructor_Throws_WhenNull()
    {
        string? input = null;

        ArgumentException exception = Assert.Throws<ArgumentException>(() => new UsaPhoneNumber(input));

        Assert.Equal("phoneNumber", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\r\n")]
    public void Constructor_Throws_WhenWhiteSpace(string input)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new UsaPhoneNumber(input));

        Assert.Equal("phoneNumber", exception.ParamName);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("123456789")]
    [InlineData("142345678901")]     // 11 digits, not starting with 1 (after stripping non-digits)
    [InlineData("26155551212")]     // 11 digits, starts with 2
    [InlineData("123456789012")]    // 12 digits
    [InlineData("1-800-FLOWERS")]   // becomes 1800
    public void Constructor_Throws_WhenCannotNormalizeToTenDigits(string input)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new UsaPhoneNumber(input));

        Assert.Equal("phoneNumber", exception.ParamName);
    }

    [Fact]
    public void ToE164_ReturnsExpectedFormat()
    {
        UsaPhoneNumber phoneNumber = new("(615) 555-1212");

        string formatted = phoneNumber.ToE164();

        Assert.Equal("+16155551212", formatted);
    }

    [Fact]
    public void ToDotNotation_ReturnsExpectedFormat()
    {
        UsaPhoneNumber phoneNumber = new("615-555-1212");

        string formatted = phoneNumber.ToDotNotation();

        Assert.Equal("615.555.1212", formatted);
    }

    [Fact]
    public void ToParensDashNotation_ReturnsExpectedFormat()
    {
        UsaPhoneNumber phoneNumber = new("6155551212");

        string formatted = phoneNumber.ToParensDashNotation();

        Assert.Equal("(615)555-1212", formatted);
    }

    [Fact]
    public void ImplicitStringConversion_ReturnsDigits()
    {
        UsaPhoneNumber phoneNumber = new("+1 615 555 1212");

        string value = phoneNumber;

        Assert.Equal("6155551212", value);
    }
}
