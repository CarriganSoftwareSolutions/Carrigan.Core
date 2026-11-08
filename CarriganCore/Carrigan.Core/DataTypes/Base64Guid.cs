namespace Carrigan.Core.DataTypes;

/// <summary>
/// Represents a GUID encoded as a standard Base64 string.
/// </summary>
/// <remarks>
/// GUID bytes use the default .NET mixed-endian layout for compatibility with
/// previously generated values. Base64 string sorting does not preserve GUIDv7 order.
/// </remarks>
public class Base64Guid : Base64Data, IEquatable<Base64Guid>
{
    private readonly Guid _guid;

    /// <summary>
    /// Creates a Base64-encoded version 7 GUID.
    /// </summary>
    public Base64Guid() : this(true)
    {
    }

    /// <summary>
    /// Creates a Base64-encoded version 7 or version 4 GUID.
    /// </summary>
    /// <param name="useGuid7">True for version 7; false for version 4.</param>
    public Base64Guid(bool useGuid7) : this(useGuid7 ? Guid.CreateVersion7() : Guid.NewGuid())
    {
    }

    /// <summary>
    /// Encodes an existing GUID as Base64.
    /// </summary>
    /// <param name="guid">The GUID to encode.</param>
    public Base64Guid(Guid guid) : base(guid.ToByteArray()) =>
        _guid = guid;

    /// <summary>
    /// Decodes a Base64-encoded GUID.
    /// </summary>
    /// <param name="base64">A Base64 string encoding exactly 16 bytes.</param>
    /// <exception cref="ArgumentNullException">The input is null.</exception>
    /// <exception cref="ArgumentException">The input is invalid Base64 or does not encode 16 bytes.</exception>
    public Base64Guid(string base64) : this(ParseBase64Guid(base64))
    {
    }

    /// <summary>
    /// Creates a Base64 GUID from a conventional GUID string.
    /// </summary>
    /// <param name="guidString">A GUID in a format supported by <see cref="Guid.Parse(string)"/>.</param>
    public static Base64Guid FromGuidString(string guidString) =>
        new(Guid.Parse(guidString));

    /// <summary>
    /// Converts a Base64 string to a <see cref="Base64Guid"/>.
    /// </summary>
    public static implicit operator Base64Guid(string base64) =>
        new(base64);

    /// <summary>
    /// Converts a <see cref="Base64Guid"/> to its underlying GUID.
    /// </summary>
    public static implicit operator Guid(Base64Guid base64) =>
        base64._guid;

    /// <inheritdoc/>
    public bool Equals(Base64Guid? other) =>
        other is not null && GetType() == other.GetType() && _guid.Equals(other._guid);

    /// <inheritdoc/>
    public override bool Equals(Base64Data? other) => 
        other is Base64Guid guid && Equals(guid);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(GetType(), _guid);

    private static Guid ParseBase64Guid(string base64)
    {
        ArgumentNullException.ThrowIfNull(base64);

        byte[] data;

        try
        {
            data = Convert.FromBase64String(base64);
        }
        catch (FormatException exception)
        {
            throw new ArgumentException("Invalid Base64 string.", nameof(base64), exception);
        }

        if (data.Length != 16)
            throw new ArgumentException("A Base64 GUID must represent exactly 16 bytes.", nameof(base64));

        return new Guid(data);
    }

    public override bool Equals(object? obj) =>
        Equals(obj as Base64Guid);
}
