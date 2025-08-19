namespace Carrigan.Core.Base64;

/// <summary>
/// An object oriented wrapper around a Base64 string that guarantees validity and type safety.
/// </summary>
/// <remarks>
/// The idea here is to have a <see cref="Base64Data"/> object, 
/// you can trust that it’s a proper Base64 string.
/// No invalid input gets through the constructor.
/// </remarks>

public readonly struct Base64Data
{
    private readonly string _encoded;

    /// <summary>
    /// Build from an existing Base64 string.
    /// Throws an  exception if it’s not actually valid.
    /// </summary>
    /// <param name="base64">A base 64 string to be validated before being stored.</param>
    /// <exception cref="ArgumentException">Thrown if the string does not represent a valid base 64 number.</exception>
    public Base64Data(string base64)
    {
        try
        {
            Convert.FromBase64String(base64);
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("Invalid Base64 string.", nameof(base64), ex);
        }
        _encoded = base64;
    }

    /// <summary>
    /// Build directly from a byte array.
    /// </summary>
    /// <param name="data">A byte array to be converted to a base 64 string.</param>
    public Base64Data(byte[] data)
    {
        _encoded = Convert.ToBase64String(data);
    }

    /// <summary>
    /// Convert back to a byte array.
    /// </summary>
    /// <returns>Returns a byte array represented by the base 64 number this object represents.</returns>
    public byte[] ToByteArray() => Convert.FromBase64String(_encoded);


    /// <summary>
    /// Returns the Base64 string as-is.
    /// </summary>
    /// <returns>Returns the base 64 string this object represents.</returns>
    public override string ToString() => _encoded;

    /// <summary>
    /// Implicit conversion from string to Base64Data.
    /// This validates the input before accepting it.
    /// </summary>
    /// <param name="base64">A string that represents a valid base 64 number.</param>
    public static implicit operator Base64Data(string base64) => new(base64);

    /// <summary>
    /// Implicit conversion from Base64 to string
    /// </summary>
    /// <param name="base64">Represents a valid base 64 number that represents a byte array.</param>
    public static implicit operator string(Base64Data base64) => base64._encoded;

    /// <summary>
    /// Check if a string looks like valid Base64.
    /// </summary>
    /// <param name="base64"></param>
    /// <returns>True if is the string represents a valid base 64 number</returns>
    public static bool IsBase64String(string base64)
    {// Check if the length is a multiple of 4
        if (base64.Length % 4 != 0)
            return false;

        try
        {
            // Attempt to convert the string from Base64
            Convert.FromBase64String(base64);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
