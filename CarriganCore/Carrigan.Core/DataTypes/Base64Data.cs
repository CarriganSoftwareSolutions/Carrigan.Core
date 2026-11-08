namespace Carrigan.Core.DataTypes;

/// <summary>
/// Represents binary data encoded as a valid Base64 string.
/// </summary>
/// <remarks>
/// Input is normalized to canonical Base64, so equivalent encodings compare equally.
/// </remarks>
public class Base64Data : IEquatable<Base64Data>
{
    private readonly string _encoded;

    /// <summary>
    /// Creates a Base64 value from an existing encoded string.
    /// </summary>
    /// <param name="base64">A valid Base64-encoded string.</param>
    /// <exception cref="ArgumentNullException">The input is null.</exception>
    /// <exception cref="ArgumentException">The input is not valid Base64.</exception>
    public Base64Data(string base64)
    {
        ArgumentNullException.ThrowIfNull(base64);

        try
        {
            byte[] data = Convert.FromBase64String(base64);
            _encoded = Convert.ToBase64String(data);
        }
        catch (FormatException exception)
        {
            throw new ArgumentException("Invalid Base64 string.", nameof(base64), exception);
        }
    }

    /// <summary>
    /// Creates a Base64 value from binary data.
    /// </summary>
    /// <param name="data">The bytes to encode.</param>
    /// <exception cref="ArgumentNullException">The input is null.</exception>
    public Base64Data(byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        _encoded = Convert.ToBase64String(data);
    }

    /// <summary>
    /// Decodes the Base64 value into a new byte array.
    /// </summary>
    public byte[] ToByteArray() => Convert.FromBase64String(_encoded);

    /// <summary>
    /// Returns the canonical Base64 string.
    /// </summary>
    public override string ToString() => _encoded;

    /// <summary>
    /// Compares the encoded data with another instance of the same runtime type.
    /// </summary>
    public virtual bool Equals(Base64Data? other) =>
        other is not null &&  GetType() == other.GetType() && string.Equals(_encoded, other._encoded, StringComparison.Ordinal);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Base64Data other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(GetType(), _encoded);

    /// <summary>
    /// Converts a Base64-encoded string to a validated <see cref="Base64Data"/>.
    /// </summary>
    public static implicit operator Base64Data(string base64) => new(base64);

    /// <summary>
    /// Converts a <see cref="Base64Data"/> value to a Base64-encoded string.
    /// </summary>
    public static implicit operator string(Base64Data base64) => base64._encoded;

    /// <summary>
    /// Returns whether a string contains valid Base64 data.
    /// </summary>
    /// <param name="base64">The string to validate, or null.</param>
    /// <returns>True for valid Base64 data; otherwise, false.</returns>
    public static bool IsBase64String(string? base64)
    {
        if (base64 is null)
            return false;

        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
