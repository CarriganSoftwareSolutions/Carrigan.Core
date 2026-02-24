using System;
using System.Linq;

namespace Carrigan.Core.DataTypes;

public sealed class UsaPhoneNumber : StringWrapper
{
    private const int UsaLocalDigitsLength = 10;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsaPhoneNumber"/> class.
    /// The underlying stored value is normalized to exactly 10 digits (NPA-NXX-XXXX).
    /// </summary>
    /// <param name="phoneNumber">
    /// A phone number in any common format (e.g., "615-555-1212", "(615)555-1212", "+1 615 555 1212").
    /// </param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="phoneNumber"/> is null/white-space or cannot be normalized to 10 digits.</exception>
    public UsaPhoneNumber(string? phoneNumber)
        : base(NormalizeToTenDigits(phoneNumber), StringComparison.Ordinal)
    {
    }

    /// <summary>
    /// Gets the normalized 10-digit value (digits only).
    /// </summary>
    public string Digits =>
        _value;

    /// <summary>
    /// Formats this number as E.164 (+1NPA-NXX-XXXX).
    /// </summary>
    public string ToE164() =>
        $"+1{_value}";

    /// <summary>
    /// Formats this number as xxx.xxx.xxxx.
    /// </summary>
    public string ToDotNotation() =>
        $"{_value[..3]}.{_value.Substring(3, 3)}.{_value[6..]}";

    /// <summary>
    /// Formats this number as (xxx)xxx-xxxx.
    /// </summary>
    public string ToParensDashNotation() =>
        $"({_value[..3]}){_value.Substring(3, 3)}-{_value[6..]}";

    private static string NormalizeToTenDigits(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or white space.", nameof(phoneNumber));

        // Remove all non-digit characters
        string digits = new([.. phoneNumber.Where(char.IsDigit)]);

        // If the number starts with '1' and has 11 digits, remove the '1'
        if (digits.Length == 11 && digits.StartsWith('1'))
        {
            digits = digits[1..];
        }

        // Ensure the number has exactly 10 digits after processing
        if (digits.Length != UsaLocalDigitsLength)
            throw new ArgumentException("Phone number must be a valid US phone number with exactly 10 digits (optionally prefixed with country code 1).", nameof(phoneNumber));

        return digits;
    }
}
